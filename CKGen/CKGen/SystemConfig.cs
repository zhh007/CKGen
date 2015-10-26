﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CKGen.DBLoader;

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
