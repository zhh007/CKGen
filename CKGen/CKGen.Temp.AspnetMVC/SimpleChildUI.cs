using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using CKGen.DBSchema;
using CKGen.Base;

namespace CKGen.Temp.AspnetMVC
{
    [Export("UserControl", typeof(UserControl))]
    public partial class SimpleChildUI : UserControl, IGenUI
    {
        private readonly IResService resService = ServiceLocator.Instance.GetService<IResService>();
        [Import("ModuleName")]
        public IDatabaseInfo Database { get; set; }
        private ViewResult vResult = null;
        private ITableInfo SelectedChildTable;
        private ITableInfo SelectedParentTable;
        private SimpleChildGenModel GenModel = new SimpleChildGenModel();
        public SimpleChildUI()
        {
            InitializeComponent();
        }

        private void SimpleChildUI_Load(object sender, EventArgs e)
        {
            vResult = new ViewResult(this);
            this.Controls.Add(vResult);
            vResult.Hide();

            btnView.Hide();
            if (this.Database != null)
            {
                foreach (var table in this.Database.Tables)
                {
                    cbTablesForChild.Items.Add(table.Name);
                    cbTablesForParent.Items.Add(table.Name);
                }
            }
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamespace.Text) || string.IsNullOrWhiteSpace(txtNamespace.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtWebProjNameSpace.Text) || string.IsNullOrWhiteSpace(txtWebProjNameSpace.Text))
            {
                return;
            }
            bool hasChildTable = false;
            string tbChildName = cbTablesForChild.Text.Trim();
            if (string.IsNullOrEmpty(tbChildName))
            {
                return;
            }
            foreach (var item in Database.Tables)
            {
                if (item.Name == tbChildName)
                {
                    SelectedChildTable = item;
                    hasChildTable = true;
                    break;
                }
            }
            if (!hasChildTable)
            {
                return;
            }

            bool hasParentTable = false;
            string tbParentName = cbTablesForParent.Text.Trim();
            if (string.IsNullOrEmpty(tbParentName))
            {
                return;
            }
            foreach (var item in Database.Tables)
            {
                if (item.Name == tbParentName)
                {
                    SelectedParentTable = item;
                    hasParentTable = true;
                    break;
                }
            }
            if (!hasParentTable)
            {
                return;
            }

            string childCollectionName = txtChildName.Text.Trim();
            if (string.IsNullOrEmpty(childCollectionName))
            {
                return;
            }

            txtChildName.Enabled = false;
            txtNamespace.Enabled = false;
            txtWebProjNameSpace.Enabled = false;
            cbTablesForChild.Enabled = false;
            cbTablesForParent.Enabled = false;
            gvFields.Enabled = false;
            btnGen.Enabled = false;
            btnGen.Text = "正在生成...";
            btnView.Hide();

            //设置SimpleChildGenModel
            GenModel.NameSpacePR = txtNamespace.Text.Trim();
            GenModel.WebProjNameSpace = txtWebProjNameSpace.Text.Trim();
            GenModel.ChildCollectionName = childCollectionName;
            GenModel.ChildModel = this.SelectedChildTable;
            GenModel.ParentModel = this.SelectedParentTable;



            ShowResultView();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ShowResultView();
        }

        public void ShowSettingView()
        {
            this.MainPanel.Show();
            this.vResult.Hide();
        }

        public void ShowResultView()
        {
            this.MainPanel.Hide();
            this.vResult.Show();
            this.vResult.Dock = DockStyle.Fill;
        }

        public void GoResult(string dir)
        {
            vResult.Show(dir);
            ShowResultView();
        }

        public override string ToString()
        {
            return "简单子表编辑";
        }

        private void cbTablesForChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITableInfo selTable = null;
            string tbChildName = cbTablesForChild.Text.Trim();
            if (string.IsNullOrEmpty(tbChildName))
            {
                return;
            }
            foreach (var item in Database.Tables)
            {
                if (item.Name == tbChildName)
                {
                    selTable = item;
                    break;
                }
            }

            if(selTable == null)
            {
                this.SelectedChildTable = null;
                this.gvFields.Rows.Clear();
                return;
            }
            else
            {
                if(this.SelectedChildTable != selTable)
                {
                    this.gvFields.Rows.Clear();
                    List<DataGridViewRow> rlist = new List<DataGridViewRow>();
                    foreach (var item in selTable.Columns)
                    {
                        int rowIndex = gvFields.Rows.Add();
                        DataGridViewRow row = gvFields.Rows[rowIndex];
                        row.Cells[0].Value = item.RawName;
                        row.Cells[1].Value = SQLHelper.GetFullSqlType(item);
                        row.Cells[2].Value = item.Nullable ? true : false;
                        row.Cells[3].Value = item["local_desc"] ?? "";
                        row.Cells[4].Value = (item.IsPrimaryKey) ? "hidden" : "text";

                        //DataGridViewTextBoxCell col1 = new DataGridViewTextBoxCell();
                        //col1.Value = item.RawName;

                        //DataGridViewTextBoxCell col2 = new DataGridViewTextBoxCell();
                        //col2.Value = SQLHelper.GetFullSqlType(item);

                        //DataGridViewCheckBoxCell col3 = new DataGridViewCheckBoxCell();
                        //col3.Value = item.Nullable ? true : false;

                        //DataGridViewTextBoxCell col4 = new DataGridViewTextBoxCell();
                        //col4.Value = item["local_desc"] ?? "";

                        //DataGridViewComboBoxCell col5 = new DataGridViewComboBoxCell();
                        //col5.Items.Add("text");
                        //col5.Items.Add("hidden");
                        //col5.Items.Add("select");
                        //col5.Items.Add("radio");
                        //col5.Items.Add("checkbox");
                        //col5.Items.Add("textarea");
                        //col5.Value = (item.IsPrimaryKey) ? "hidden" : "text";

                        //DataGridViewTextBoxCell col6 = new DataGridViewTextBoxCell();

                        ////DataGridViewRow row = new DataGridViewRow();
                        //row.Tag = item.IsPrimaryKey;
                        //row.Cells.AddRange(new DataGridViewCell[] { col1, col2, col3, col4, col5, col6 });
                        rlist.Add(row);
                    }

                    //this.gvFields.Rows.AddRange(rlist.ToArray());
                    this.SelectedChildTable = selTable;
                }
            }
        }

        /// <summary>
        /// 显示主键图标
        /// </summary>
        private void gvFields_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewRow row = gvFields.Rows[e.RowIndex];

            if (row.Tag != null && row.Tag is bool && (bool)row.Tag)
            {
                Image img = resService.GetPrimaryKeyImage();
                Bitmap myBitmap = new Bitmap(img);
                Icon myIcon = Icon.FromHandle(myBitmap.GetHicon());

                Graphics graphics = e.Graphics;

                int iconHeight = 16;
                int iconWidth = 16;

                int xPosition = e.RowBounds.X + (gvFields.RowHeadersWidth / 2);
                int yPosition = e.RowBounds.Y + ((row.Height - iconHeight) / 2);

                Rectangle rectangle = new Rectangle(xPosition, yPosition, iconWidth, iconHeight);
                graphics.DrawIcon(myIcon, rectangle);
            }
        }
    }
}
