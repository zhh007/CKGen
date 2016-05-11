using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CKGen.DBLoader;
using System.Windows.Forms;
using CKGen.DBSchema;
using System.ComponentModel.Composition;
using CKGen.Base;
using CKGen.Events;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

namespace CKGen
{
    public class App
    {
        private TreeNode _selectNode = null;
        public IEventAggregator Events = new EventAggregator();
        private static App instance = new App();

        private App()
        {
        }

        public static App Instance
        {
            get
            {
                return instance;
            }
        }

        public DatabaseLink DBLink { get; set; }
        public ServerInfo SrvInfo { get; set; }
        public string DBName { get; set; }
        [Export("Database")]
        public IDatabaseInfo Database { get; set; }

        public TreeNode SelectedNode
        {
            get { return _selectNode; }
            set
            {
                _selectNode = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(""));
            }
        }

        #region 

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion

        public void Subscribe<T>(Action<T> handler)
            where T : CompositePresentationEvent<T>, new()
        {
            Events.GetEvent<T>().Subscribe(handler);
        }

        public void Publish<T>(T parameter)
            where T : CompositePresentationEvent<T>, new()
        {
            Events.GetEvent<T>().Publish(parameter);
        }

        public void LoadDatabaseSchema(bool force)
        {
            Stopwatch _stopWatch = new Stopwatch();
            _stopWatch.Start();
            LoadData(force);

            int size = App.Instance.Database.Views.Count;
            for (int i = 0; i < size; i++)
            {
                IViewInfo vw = App.Instance.Database.Views[i];
                vw.LoadColumns(force);
            }

            size = App.Instance.Database.Procedures.Count;
            for (int i = 0; i < size; i++)
            {
                IProcedureInfo proc = App.Instance.Database.Procedures[i];
                proc.LoadParameters(force);
            }

            _stopWatch.Stop();
            Debug.WriteLine("执行时间:" + (_stopWatch.Elapsed.TotalMilliseconds * 1.0 / 1000).ToString(CultureInfo.InvariantCulture) + "秒");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            DatabaseSchemaSetting.SyncToLocal(App.Instance.Database);
            _stopWatch.Stop();
            Debug.WriteLine("执行时间:" + (_stopWatch.Elapsed.TotalMilliseconds * 1.0 / 1000).ToString(CultureInfo.InvariantCulture) + "秒");

        }

        private void LoadData(bool force)
        {
            int size = App.Instance.Database.Tables.Count;
            int N = size;
            int P = Environment.ProcessorCount; // assume twice the procs for 
                                                // good work distribution
            int Chunk = (int)Math.Round((double)N / (double)P, 0);// size of a work chunk
            AutoResetEvent signal = new AutoResetEvent(false);
            int counter = P;                        // use a counter to reduce 
                                                    // kernel transitions    

            Debug.WriteLine("size:{0}", size);
            Debug.WriteLine("P:{0}", P);
            Debug.WriteLine("Chunk:{0}", Chunk);

            List<Thread> list = new List<Thread>();
            List<int> args = new List<int>();

            for (int c = 0; c < P; c++)
            {           // for each chunk
                Thread th = new Thread(new ParameterizedThreadStart((o) =>
                {
                    int lc = (int)o;
                    int start = lc * Chunk;// iterate through a work chunk
                    int end = (lc + 1 == P ? N : (lc + 1) * Chunk);// respect upper bound
                    Debug.WriteLine("{0} : {1} ~ {2}", lc, start, end);

                    for (int i = start; i < end; i++)
                    {
                        if (i < App.Instance.Database.Tables.Count)
                        {
                            ITableInfo tb = App.Instance.Database.Tables[i];
                            tb.LoadColumns(force);
                        }
                    }
                    if (Interlocked.Decrement(ref counter) == 0)
                    { // use efficient 
                      // interlocked 
                      // instructions      
                        signal.Set();  // and kernel transition only when done
                    }
                }));
                th.IsBackground = true;
                list.Add(th);
                args.Add(c);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Start(args[i]);
            }

            signal.WaitOne();
        }
    }
}
