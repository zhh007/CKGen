using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;

namespace CKGen
{
    public partial class FrmMain : Form
    {
        private IDatabaseInfo DB = null;
        private UCDetail DetailPage = null;
        private ContextMenuStrip TableMenu = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (SystemConfig.SrvInfo == null)
            {
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
            }

            IDatabaseInfo dbi = SystemConfig.SrvInfo.GetDatabase(SystemConfig.DBName);
            this.DB = DatabaseSchemaSetting.Compute(dbi);
            SystemConfig.Instance.Database = this.DB;

            //详细信息
            this.DetailPage = new UCDetail();
            this.DetailPage.Dock = DockStyle.Fill;
            TabPage tab1 = new TabPage("详细信息");
            tab1.Controls.Add(this.DetailPage);
            this.tabControl1.TabPages.Add(tab1);

            //
            FrmCodeBuild frm = new FrmCodeBuild();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            TabPage tab = new TabPage(frm.Text);
            tab.Controls.Add(frm);
            this.tabControl1.TabPages.Add(tab);
            frm.Show();

            //
            InitTree();

            //this.WindowState = FormWindowState.Maximized;
        }

        private void InitTree()
        {
            TreeNode tbNode = new TreeNode("表");
            tbNode.ImageIndex = 1;
            tbNode.SelectedImageIndex = 1;
            foreach (ITableInfo item in this.DB.Tables)
            {
                TreeNode node = new TreeNode(item.RawName);
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = item;
                tbNode.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(tbNode);
            tbNode.Expand();
            if (tbNode.Nodes.Count > 0)
            {
                this.tvSchema.SelectedNode = tbNode.Nodes[0];
            }

            //菜单
            this.TableMenu = new ContextMenuStrip();
            this.TableMenu.Items.Add("查看前 100 行", null, (s, e) =>
            {
                if (this.tvSchema.SelectedNode.Tag is ITableInfo)
                {
                    ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                    string sql = string.Format("select top 100 * from {0}", tbInfo.RawName);

                    var query = new UCQuery();
                    query.Dock = DockStyle.Fill;
                    TabPage tab1 = new TabPage("查询");
                    tab1.Controls.Add(query);
                    this.tabControl1.TabPages.Add(tab1);
                    this.tabControl1.SelectTab(tab1);
                    query.Query(sql);
                }
            });
            ToolStripSeparator spliter = new ToolStripSeparator();
            this.TableMenu.Items.Add(spliter);
            this.TableMenu.Items.Add("生成代码 - Insert", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Update", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Delete", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Save", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Exist", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Get", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Top", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Select", null, (s, e) =>
            {

            });
            this.TableMenu.Items.Add("生成代码 - Paged", null, (s, e) =>
            {

            });
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is ITableInfo)
            {
                ITableInfo tbInfo = e.Node.Tag as ITableInfo;
                SystemConfig.Instance.SelectedNode = e.Node;
                SystemConfig.Instance.CurrentTableName = tbInfo.RawName;
            }
        }

        /// <summary>
        /// 保存说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            this.DetailPage.Save();
        }

        /// <summary>
        /// 重新加载元数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnReloadSchema_Click(object sender, EventArgs e)
        {

        }

        private void tvSchema_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.tvSchema.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right && e.Node.Tag is ITableInfo)
            {
                this.TableMenu.Show(this.tvSchema, e.Location);
            }
        }
        
    }
}
