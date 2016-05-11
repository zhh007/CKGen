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
    }
}
