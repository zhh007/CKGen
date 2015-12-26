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
            SystemConfig.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(SystemConfig.Instance.SelectedNode == null)
            {
                this.Controls.Clear();
                return;
            }

            if(SystemConfig.Instance.SelectedNode.Tag is ITableInfo)
            {
                if (!(innerControl is TableDetail))
                {
                    innerControl = new TableDetail();
                    innerControl.Dock = DockStyle.Fill;
                    this.Controls.Add(innerControl);
                }
                innerControl.LoadDetail();
            }
            else if(SystemConfig.Instance.SelectedNode.Tag is IViewInfo)
            {

            }
            else if(SystemConfig.Instance.SelectedNode.Tag is IProcedureInfo)
            {

            }
            else
            {
                this.Controls.Clear();
            }
        }
    }
}
