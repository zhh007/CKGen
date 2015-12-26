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
        [Export("ModuleName")]
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

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}
