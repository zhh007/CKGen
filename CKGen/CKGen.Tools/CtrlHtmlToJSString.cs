using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace CKGen.Tools
{
    [Export("Tool", typeof(UserControl))]
    public partial class CtrlHtmlToJSString : UserControl
    {
        public CtrlHtmlToJSString()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "HTML转JS代码";
        }

        private void txtHTML_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
