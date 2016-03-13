using CKGen.Base;
using CKGen.Base.CodeModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class SQLQueryCodeGenWizard : Form
    {
        public string SQL { get; set; }
        public MethodDefine MethodInfo = new MethodDefine();
        public List<ClassDefine> ResultTypeList = new List<ClassDefine>();
        public SQLQueryCodeGenWizard()
        {
            InitializeComponent();

            //if (MethodInfo == null || MethodInfo.Count == 0)
            //{
            //    QueryParamPage.Suppress = true;//隐藏参数页
            //}
        }

        private void SQLQueryCodeGenWizard_Load(object sender, EventArgs e)
        {
            
        }
    }
}
