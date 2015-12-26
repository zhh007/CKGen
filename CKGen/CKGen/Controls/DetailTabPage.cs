using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;

namespace CKGen.Controls
{
    public class DetailTabPage : TabPage
    {
        private DetailBase innerControl = null;
        public DetailTabPage()
        {
            this.Text = "详细信息";
            App.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(App.Instance.SelectedNode == null)
            {
                this.Controls.Clear();
                return;
            }

            if(App.Instance.SelectedNode.Tag is ITableInfo)
            {
                if (!(innerControl is TableDetail))
                {
                    this.Controls.Clear();
                    innerControl = new TableDetail();
                    innerControl.Dock = DockStyle.Fill;
                    this.Controls.Add(innerControl);
                }
                innerControl.LoadDetail();
            }
            else if(App.Instance.SelectedNode.Tag is IViewInfo)
            {
                if (!(innerControl is ViewDetail))
                {
                    this.Controls.Clear();
                    innerControl = new ViewDetail();
                    innerControl.Dock = DockStyle.Fill;
                    this.Controls.Add(innerControl);
                }
                innerControl.LoadDetail();
            }
            else if(App.Instance.SelectedNode.Tag is IProcedureInfo)
            {
                if (!(innerControl is ProcedureDetail))
                {
                    this.Controls.Clear();
                    innerControl = new ProcedureDetail();
                    innerControl.Dock = DockStyle.Fill;
                    this.Controls.Add(innerControl);
                }
                innerControl.LoadDetail();
            }
            else
            {
                this.Controls.Clear();
            }
        }
    }
}
