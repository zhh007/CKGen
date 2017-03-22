using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

namespace CKGen.Temp.AspnetMVC2
{
    public partial class ViewResult : UserControl
    {
        private IGenUI parent;
        private string folderPath;
        private bool showCode = false;
        private TreeNode initSelectedNode = null;

        public ViewResult(IGenUI _parent)
        {
            parent = _parent;
            InitializeComponent();

            txtCode.Font = new Font("Consolas", 12);
        }

        public void Show(string folder)
        {
            tvRoot.Nodes.Clear();
            txtCode.Text = "";
            if (!Directory.Exists(folder))
            {
                return;
            }

            folderPath = folder;

            DirectoryInfo dir = new DirectoryInfo(folder);
            TreeNode root = BuildDirForTreeview(dir, "目录");
            root.ExpandAll();
            tvRoot.Nodes.Add(root);
            tvRoot.SelectedNode = initSelectedNode;
        }

        private TreeNode BuildDirForTreeview(DirectoryInfo dir, string showname)
        {
            TreeNode tn = new TreeNode(showname);
            tn.Tag = dir.FullName;

            foreach (var item in dir.GetDirectories())
            {
                tn.Nodes.Add(BuildDirForTreeview(item, item.Name));
            }

            foreach (var file in dir.GetFiles())
            {
                TreeNode fileNode = new TreeNode(file.Name);
                fileNode.Tag = file.FullName;
                if (!showCode)
                {
                    initSelectedNode = fileNode;
                    txtCode.Text = File.ReadAllText(file.FullName);
                    showCode = true;
                }
                tn.Nodes.Add(fileNode);
            }

            return tn;
        }

        private void tvRoot_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is string)
            {
                var str = e.Node.Tag as string;
                if (File.Exists(str))
                {
                    string txt = File.ReadAllText(str);
                    txtCode.Text = txt;
                }
            }
        }

        private void stBtnBack_Click(object sender, EventArgs e)
        {
            parent.ShowSettingView();
        }

        private void stBtnOpen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                Process.Start(folderPath);
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void tvRoot_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C)
            {
                if(tvRoot.SelectedNode != null && tvRoot.SelectedNode.Tag is string)
                {
                    string str = tvRoot.SelectedNode.Tag as string;
                    if(File.Exists(str))
                    {
                        StringCollection paths = new StringCollection();
                        paths.Add(str);
                        Clipboard.SetFileDropList(paths);
                    }
                    else if(Directory.Exists(str))
                    {
                        StringCollection paths = new StringCollection();
                        paths.Add(str);
                        Clipboard.SetFileDropList(paths);
                    }
                }
            }
        }
    }
}
