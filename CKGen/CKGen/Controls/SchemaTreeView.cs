using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;
using CKGen.Temp.Adonet;
using CKGen.Events;

namespace CKGen.Controls
{
    public partial class SchemaTreeView : UserControl
    {
        private IDatabaseInfo DB = null;
        private ContextMenuStrip TableMenu = null;
        private DbTableCodeGen gen = new DbTableCodeGen();
        public SchemaTreeView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.DB = App.Instance.Database;

            //
            InitTree();

            //菜单
            CreateMenu();

            App.Instance.Subscribe<SaveDescToDbEvent>(p => SaveDescToDb());
        }

        private void InitTree()
        {
            TreeNode tbNode = new TreeNode("表");
            tbNode.ImageIndex = 1;
            tbNode.SelectedImageIndex = 1;
            foreach (ITableInfo tbInfo in this.DB.Tables)
            {
                TreeNode node = new TreeNode(tbInfo.RawName);
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = tbInfo;

                var colHasNewDesc = (from p in tbInfo.Columns
                                     where !string.IsNullOrEmpty(p["new_desc"])
                                     select p).Count() > 0;
                if (colHasNewDesc || !string.IsNullOrEmpty(tbInfo["new_desc"]))
                {
                    node.Text = tbInfo.RawName + "(*)";
                    node.ForeColor = Color.Red;
                }

                tbNode.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(tbNode);
            tbNode.Expand();
            if (tbNode.Nodes.Count > 0)
            {
                this.tvSchema.SelectedNode = tbNode.Nodes[0];
            }

            //视图
            TreeNode vwRoot = new TreeNode("视图");
            vwRoot.ImageIndex = 1;
            vwRoot.SelectedImageIndex = 1;
            foreach (var vwInfo in this.DB.Views)
            {
                TreeNode node = new TreeNode(vwInfo.RawName);
                node.ImageIndex = 4;
                node.SelectedImageIndex = 4;
                node.Tag = vwInfo;

                var colHasNewDesc = (from p in vwInfo.Columns
                                     where !string.IsNullOrEmpty(p["new_desc"])
                                     select p).Count() > 0;
                if (colHasNewDesc || !string.IsNullOrEmpty(vwInfo["new_desc"]))
                {
                    node.Text = vwInfo.RawName + "(*)";
                    node.ForeColor = Color.Red;
                }

                vwRoot.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(vwRoot);

            //存储过程
            TreeNode procRoot = new TreeNode("存储过程");
            procRoot.ImageIndex = 1;
            procRoot.SelectedImageIndex = 1;
            foreach (var procInfo in this.DB.Procedures)
            {
                TreeNode node = new TreeNode(procInfo.Name);
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = procInfo;

                //var colHasNewDesc = (from p in procInfo.Columns
                //                     where !string.IsNullOrEmpty(p.Attributes["new_desc"])
                //                     select p).Count() > 0;
                //if (colHasNewDesc || !string.IsNullOrEmpty(procInfo.Attributes["new_desc"]))
                //{
                //    node.Text = procInfo.RawName + "(*)";
                //    node.ForeColor = Color.Red;
                //}

                procRoot.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(procRoot);
        }

        private void CreateMenu()
        {
            this.TableMenu = new ContextMenuStrip();
            this.TableMenu.Items.Add("查看前 100 行", null, (s, e) =>
            {
                if (this.tvSchema.SelectedNode.Tag is ITableInfo)
                {
                    ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                    string sql = string.Format("select top 100 * from {0}", tbInfo.RawName);
                    ShowSQLQuery(sql);
                }
            });
            ToolStripSeparator spliter = new ToolStripSeparator();
            this.TableMenu.Items.Add(spliter);
            this.TableMenu.Items.Add("生成 - Model", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenModelCode(tbInfo);
                ShowCode(string.Format("{0}.cs", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - DAL", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenDataAccessCode("Test", tbInfo);
                ShowCode(string.Format("{0}Access.cs", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Insert", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenInsertCode(tbInfo);
                ShowCode(string.Format("{0} - Insert", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Update", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenUpdateCode(tbInfo);
                ShowCode(string.Format("{0} - Update", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Delete", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenDeleteCode(tbInfo);
                ShowCode(string.Format("{0} - Delete", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Save", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenSaveCode(tbInfo);
                ShowCode(string.Format("{0} - Save", tbInfo.PascalName), code);
            });

            this.TableMenu.Items.Add("生成 - Query", null, (s, e) =>
            {
                FrmSnippetGenQuery frm = new FrmSnippetGenQuery();
                frm.Table = this.tvSchema.SelectedNode.Tag as ITableInfo;
                frm.ShowDialog();
            });
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            App.Instance.SelectedNode = e.Node;
        }

        private void tvSchema_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.tvSchema.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right && e.Node.Tag is ITableInfo)
            {
                this.TableMenu.Show(this.tvSchema, e.Location);
            }
        }

        private void ShowCode(string title, string code)
        {
            ShowCodeEvent evt = new ShowCodeEvent();
            evt.Title = title;
            evt.Code = code;
            App.Instance.Publish<ShowCodeEvent>(evt);
        }

        private void ShowSQLQuery(string sql)
        {
            ShowSQLQueryEvent evt = new ShowSQLQueryEvent();
            evt.SQL = sql;
            App.Instance.Publish<ShowSQLQueryEvent>(evt);
        }

        private void SaveDescToDb()
        {
            foreach (TreeNode node in tvSchema.Nodes)
            {
                UpdateNode(node);
            }
        }

        private void UpdateNode(TreeNode node)
        {
            if (node.Tag is ITableInfo)
            {
                ITableInfo tbInfo = node.Tag as ITableInfo;
                node.Text = tbInfo.RawName;
                node.ForeColor = Color.Black;
            }
            foreach (TreeNode item in node.Nodes)
            {
                UpdateNode(item);
            }
        }

        private void tvSchema_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }
    }
}
