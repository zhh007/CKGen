﻿using System;
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
    public class SystemConfig //: INotifyPropertyChanged
    {
        private SystemConfig()
        {
        }
        private static SystemConfig instance = new SystemConfig();

        public static SystemConfig Instance
        {
            get
            {
                return instance;
            }
        }

        public DatabaseLink DBLink = null;
        public ServerInfo SrvInfo = null;
        public string DBName = "";
        private TreeNode _selectNode = null;
        public TreeNode SelectedNode
        {
            get { return _selectNode; }
            set
            {
                _selectNode = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(""));
            }
        }

        [Export("ModuleName")]
        public IDatabaseInfo Database { get; set; }

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
