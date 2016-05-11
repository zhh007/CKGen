using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;
using CKGen.Events;

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
                innerControl = null;
                this.Text = "详细信息";
                return;
            }

            if(App.Instance.SelectedNode.Tag is ITableInfo)
            {
                var tbInfo = App.Instance.SelectedNode.Tag as ITableInfo;
                this.Text = tbInfo.RawName;
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
                var vwInfo = App.Instance.SelectedNode.Tag as IViewInfo;
                this.Text = vwInfo.RawName;
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
                var procInfo = App.Instance.SelectedNode.Tag as IProcedureInfo;
                this.Text = procInfo.Name;
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
                innerControl = null;
                this.Text = "详细信息";
            }
        }
    }
}
