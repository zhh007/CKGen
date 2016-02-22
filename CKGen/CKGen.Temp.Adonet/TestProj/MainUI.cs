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

namespace CKGen.Temp.Adonet.TestProj
{
    [Export("UserControl", typeof(UserControl))]
    public partial class MainUI : UserControl
    {
        [Import("ModuleName")]
        public IDatabaseInfo Database { get; set; }

        private int _tableCount = 0;
        private int _viewCount = 0;
        private int _procCount = 0;

        public MainUI()
        {
            InitializeComponent();
        }

        private void btnBuild_Click(object sender, EventArgs e)
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

        private void MainUI_Load(object sender, EventArgs e)
        {
            lvMain.Groups.Add("table", "表");
            lvMain.Groups.Add("view", "视图");
            lvMain.Groups.Add("proc", "存储过程");

            lvMain.Columns.Add(new ColumnHeader() { Name = "Name", Width = 150 });
            lvMain.HeaderStyle = ColumnHeaderStyle.Clickable;
            lvMain.Sorting = SortOrder.Ascending;
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
    }
}
