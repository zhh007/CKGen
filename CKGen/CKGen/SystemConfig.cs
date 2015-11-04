using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CKGen.DBLoader;
using System.Windows.Forms;
using CKGen.DBSchema;
using System.ComponentModel.Composition;

namespace CKGen
{
    public class SystemConfig : INotifyPropertyChanged
    {
        private static SystemConfig instance = new SystemConfig();

        private SystemConfig()
        {
        }

        public static SystemConfig Instance
        {
            get
            {
                return instance;
            }
        }

        public static DatabaseLink DBLink = null;
        public static ServerInfo SrvInfo = null;
        public static string DBName = "";
        private string _currentTableName = "";
        public TreeNode SelectedNode { get; set; }

        [Export("ModuleName")]
        public IDatabaseInfo Database { get; set; }

        public string CurrentTableName
        {
            get { return _currentTableName; }
            set
            {
                _currentTableName = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("CurrentTableName"));
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
