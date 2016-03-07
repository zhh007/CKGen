using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class FrmShowCode : Form
    {
        public FrmShowCode()
        {
            InitializeComponent();
        }

        public void SetCode(string code)
        {
            this.codeView1.Show(code);
        }
    }
}
