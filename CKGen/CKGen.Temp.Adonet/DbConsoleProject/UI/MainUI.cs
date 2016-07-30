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
using System.Diagnostics;
using CKGen.Base.Events;

namespace CKGen.Temp.Adonet.DbConsoleProject.UI
{
    [Export("GenTemplate", typeof(UserControl))]
    public partial class MainUI : UserControl
    {
        private readonly object token = new object();
        //[Import("Database")]
        public IDatabaseInfo Database { get; set; }

        private int _tableCount = 0;
        private int _viewCount = 0;
        private int _procCount = 0;
        private string _projName = "";

        private List<ITableInfo> selTables = new List<ITableInfo>();
        private List<IViewInfo> selViews = new List<IViewInfo>();

        private Action<GetDbInstanceResponseEvent> dbResponseEventHandler = null;
        private Action<DatabaseRefreshEvent> dbRefreshEventHandler = null;

        public MainUI()
        {
            InitializeComponent();

            dbResponseEventHandler = p =>
            {
                if (p.Token == token)
                {
                    BindUI(p.Database);
                }
            };
            dbRefreshEventHandler = p =>
            {
                BindUI(p.Database);
            };
            AppEvent.Subscribe(dbResponseEventHandler);
            AppEvent.Subscribe(dbRefreshEventHandler);
            AppEvent.Publish(new GetDbInstanceRequestEvent() { Token = token });
            this.Disposed += MainUI_Disposed;
        }

        private void MainUI_Disposed(object sender, EventArgs e)
        {
            AppEvent.UnSubscribe(dbResponseEventHandler);
            AppEvent.UnSubscribe(dbRefreshEventHandler);
        }

        private void BindUI(IDatabaseInfo db)
        {
            this.Database = db;
            lvMain.Items.Clear();
            lvMain.Groups.Add("table", "表");
            lvMain.Groups.Add("view", "视图");
            lvMain.Groups.Add("proc", "存储过程");

            lvMain.Columns.Add(new ColumnHeader() { Name = "Name", Width = 150 });
            lvMain.HeaderStyle = ColumnHeaderStyle.Clickable;
            lvMain.Sorting = SortOrder.Ascending;

            if (this.Database != null)
            {
                txtProjName.Text = string.Format("{0}_{1:yyyyMMddHHmmss}", this.Database.Name, DateTime.Now);
            }
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            
        }

        public override string ToString()
        {
            return "测试项目（Ado.net）";
        }

        private void MainUI_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void MainUI_DragDrop(object sender, DragEventArgs e)
        {
            errorProvider1.SetError(groupBox1, "");

            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null)
            {
                if (draggedNode.Nodes != null && draggedNode.Nodes.Count > 0)
                {
                    foreach (TreeNode item in draggedNode.Nodes)
                    {
                        AddTreeNode(item);
                    }
                }
                else
                {
                    AddTreeNode(draggedNode);
                }

                UpdateGroupTitle();
            }
        }

        private void AddTreeNode(TreeNode node)
        {
            if (node.Tag is ITableInfo)
            {
                ITableInfo tinfo = node.Tag as ITableInfo;
                if (!lvMain.Items.ContainsKey(tinfo.Name))
                {
                    _tableCount++;

                    ListViewItem lvitem = new ListViewItem(tinfo.Name);
                    lvitem.Tag = tinfo;
                    lvitem.Group = lvMain.Groups[0];
                    lvMain.Items.Add(lvitem);
                }
            }
            else if (node.Tag is IViewInfo)
            {
                IViewInfo vinfo = node.Tag as IViewInfo;
                if (!lvMain.Items.ContainsKey(vinfo.Name))
                {
                    _viewCount++;

                    ListViewItem lvitem = new ListViewItem(vinfo.Name);
                    lvitem.Tag = vinfo;
                    lvitem.Group = lvMain.Groups[1];
                    lvMain.Items.Add(lvitem);
                }
            }
            else if (node.Tag is IProcedureInfo)
            {
                IProcedureInfo procinfo = node.Tag as IProcedureInfo;
                if (!lvMain.Items.ContainsKey(procinfo.Name))
                {
                    _procCount++;

                    ListViewItem lvitem = new ListViewItem(procinfo.Name);
                    lvitem.Tag = procinfo;
                    lvitem.Group = lvMain.Groups[2];
                    lvMain.Items.Add(lvitem);
                }
            }
        }

        private void lvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem listViewItem in ((ListView)sender).SelectedItems)
                {
                    if (listViewItem.Tag is ITableInfo)
                    {
                        _tableCount--;
                    }
                    else if (listViewItem.Tag is IViewInfo)
                    {
                        _viewCount--;
                    }
                    else if (listViewItem.Tag is IProcedureInfo)
                    {
                        _procCount--;
                    }
                    listViewItem.Remove();
                }

                UpdateGroupTitle();
            }
        }

        private void UpdateGroupTitle()
        {
            lvMain.Groups[0].Header = string.Format("表 ({0})", _tableCount);
            lvMain.Groups[1].Header = string.Format("视图 ({0})", _viewCount);
            lvMain.Groups[2].Header = string.Format("存储过程 ({0})", _procCount);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProjName.Text))
            {
                this._projName = txtProjName.Text.Trim();
            }
            else
            {
                errorProvider1.SetError(txtProjName, "项目名称不能为空。");
                return;
            }

            if (!char.IsLetter(this._projName[0]))
            {
                errorProvider1.SetError(txtProjName, "项目名称必须以字母开头。");
                return;
            }

            selTables.Clear();
            selViews.Clear();
            foreach (ListViewItem item in lvMain.Items)
            {
                if (item.Tag is ITableInfo)
                {
                    selTables.Add(item.Tag as ITableInfo);
                }
                else if (item.Tag is IViewInfo)
                {
                    selViews.Add(item.Tag as IViewInfo);
                }
            }

            if (selTables.Count == 0 && selViews.Count == 0)
            {
                errorProvider1.SetError(groupBox1, "选择数据库对象不能为空。");
                return;
            }

            this.AllowDrop = false;
            lvMain.Enabled = false;
            btnBuild.Enabled = false;
            btnBuild.Text = "正在生成...";
            txtProjName.Enabled = false;

            BackgroundWorker _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerSupportsCancellation = false;
            _bgWorker.WorkerReportsProgress = false;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }

        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.AllowDrop = true;
            lvMain.Enabled = true;
            btnBuild.Enabled = true;
            btnBuild.Text = "生成";
            txtProjName.Enabled = true;
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DbConsoleProjectBuilder builder = new DbConsoleProjectBuilder();
            string folder = builder.Build(selTables, selViews, this.Database.Server.Connection, this._projName);
            Process.Start(folder);
        }

        private void txtProjName_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProjName.Text))
            {
                this._projName = txtProjName.Text.Trim();
            }
            else
            {
                errorProvider1.SetError(txtProjName, "项目名称不能为空。");
                return;
            }

            if (!char.IsLetter(this._projName[0]))
            {
                errorProvider1.SetError(txtProjName, "项目名称必须以字母开头。");
                return;
            }

            errorProvider1.SetError(txtProjName, "");
        }
    }
}
