using CKGen.DBLoader;
using CKGen.DBSchema;
using ScintillaNET;
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

namespace CKGen
{
    public partial class FrmCodeBuild : Form
    {
        private Binding _tableNameBinding = null;
        private string folderPath = "";
        private TreeNode firstFileTreeNode = null;
        public FrmCodeBuild()
        {
            InitializeComponent();
            btnView.Hide();
            scintilla1.Margins[0].Type = MarginType.Number;
            scintilla1.Margins[0].Width = 35;

            // Display whitespace in orange
            scintilla1.WhitespaceSize = 2;
            scintilla1.ViewWhitespace = WhitespaceMode.VisibleAlways;
            scintilla1.SetWhitespaceForeColor(true, Color.FromArgb(43, 145, 175));

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 14;
            scintilla1.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            scintilla1.Lexer = Lexer.Cpp;

            // Set the keywords
            scintilla1.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            scintilla1.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");

            scintilla1.CharAdded += Scintilla1_CharAdded;
        }

        private void Scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            var currentPos = scintilla1.CurrentPosition;
            var wordStartPos = scintilla1.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (e.Char == '.')
            {
                scintilla1.CallTipCancel();
                
                var leftPos = scintilla1.WordStartPosition(currentPos - 1, true);


                string word = scintilla1.GetWordFromPosition(leftPos);
                if (string.IsNullOrEmpty(word))
                    return;

               var type = Type.GetType("System.String");
                System.Reflection.MemberInfo[] memberInfos = type.GetMembers(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                List<String> proposals = new List<string>();
                foreach (System.Reflection.MemberInfo mi in memberInfos)
                {
                    string member = mi.ToString();
                    
                        proposals.Add(mi.ToString().Split(" ".ToCharArray(), 2)[1].Replace(" ", ""));
                }
                proposals.Sort();
                scintilla1.AutoCShow(lenEntered, string.Join(" ", proposals.ToArray()));


            }
            else if (e.Char == '(')
            {
                //scintilla1.CallTipShow(currentPos, "Your smart Tooltip functionality");
            }
            else
            {
                //scintilla1.CallTipCancel();
                //if (lenEntered > 0)
                //{
                //    //scintilla1.CallTipShow
                //    //scintilla1.AutoCComplete();
                //    scintilla1.AutoCShow(lenEntered, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
                //}
            }

            //ScintillaNET.Scintilla editor = sender as ScintillaNET.Scintilla;

            //if (e.Char == '.')
            //{
            //    Timer t = new Timer();

            //    t.Interval = 10;
            //    t.Tag = editor;
            //    t.Tick += new EventHandler((obj, ev) =>
            //    {
            //        // make a new autocomplete list if needed
            //        List<string> s = new List<string>();
            //        s.Add("test");
            //        s.Add("test2");
            //        s.Add("test3");
            //        s.Sort(); // don't forget to sort it

            //        editor.AutoComplete.ShowUserList(0, s);

            //        t.Stop();
            //        t.Enabled = false;
            //        t.Dispose();
            //    });
            //    t.Start();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SystemConfig.Instance.CurrentTableName))
            {
                return;
            }

            txtNamespace.Enabled = false;
            txtWebProjNameSpace.Enabled = false;
            button1.Enabled = false;
            button1.Text = "正在生成...";
            btnView.Hide();
            lblTable.DataBindings.Remove(_tableNameBinding);
            firstFileTreeNode = null;

            BackgroundWorker _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerSupportsCancellation = false;
            _bgWorker.WorkerReportsProgress = false;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }

        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtNamespace.Enabled = true;
            txtWebProjNameSpace.Enabled = true;
            button1.Enabled = true;
            button1.Text = "生成";
            btnView.Show();
            _tableNameBinding = lblTable.DataBindings.Add("Text", SystemConfig.Instance, "CurrentTableName");

            if (!string.IsNullOrEmpty(folderPath))
            {
                BuildTreeview(folderPath);
                showStep2();
            }
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AspnetMVCSetting.Save(txtNamespace.Text, txtWebProjNameSpace.Text);

            string dbConnStr = SystemConfig.DBLink.ConnectionString;
            string dbname = SystemConfig.DBName;
            ServerInfo serverInfo = new ServerInfo(dbConnStr, "ceshi");
            IDatabaseInfo database = serverInfo.GetDatabase(dbname);

            string tableName = SystemConfig.Instance.CurrentTableName;
            string nsString = txtNamespace.Text;
            string webNSString = txtWebProjNameSpace.Text;
            CKGen.Temp.AspnetMVC.CodeBuilder mvcBuilder = new CKGen.Temp.AspnetMVC.CodeBuilder(
                database, Path.Combine(Environment.CurrentDirectory, dbname), tableName
                , nsString, webNSString);

            folderPath = mvcBuilder.Build();
        }

        private void FrmCodeBuild_Load(object sender, EventArgs e)
        {
            showStep1();
        }

        private void BuildTreeview(string filepath)
        {
            tvRoot.Nodes.Clear();
            scintilla1.Text = "";
            if (!Directory.Exists(filepath))
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(filepath);
            TreeNode first = BuildDirForTreeview(dir);
            first.ExpandAll();
            tvRoot.Nodes.Add(first);

            if (firstFileTreeNode != null)
            {
                tvRoot.SelectedNode = firstFileTreeNode;
            }
        }

        private TreeNode BuildDirForTreeview(DirectoryInfo dir)
        {
            TreeNode tn = new TreeNode(dir.Name);

            foreach (var item in dir.GetDirectories())
            {
                tn.Nodes.Add(BuildDirForTreeview(item));
            }

            foreach (var file in dir.GetFiles())
            {
                TreeNode fileNode = new TreeNode(file.Name);
                fileNode.Tag = file.FullName;
                if (firstFileTreeNode == null)
                {
                    firstFileTreeNode = fileNode;
                }
                tn.Nodes.Add(fileNode);
            }

            return tn;
        }

        private void showStep1()
        {
            step2Panel.Hide();
            step1Panel.Show();

            txtNamespace.Text = AspnetMVCSetting.NameSpace;
            txtWebProjNameSpace.Text = AspnetMVCSetting.WebProjNameSpace;
            _tableNameBinding = lblTable.DataBindings.Add("Text", SystemConfig.Instance, "CurrentTableName");
        }

        private void showStep2()
        {
            step1Panel.Hide();
            step2Panel.Dock = DockStyle.Fill;
            step2Panel.Show();

            lblTable.DataBindings.Remove(_tableNameBinding);

            if (firstFileTreeNode != null)
            {
                string txt = File.ReadAllText(firstFileTreeNode.Tag as string);
                scintilla1.Text = txt;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            showStep1();
        }

        private void tvRoot_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is string)
            {
                string txt = File.ReadAllText(e.Node.Tag as string);
                scintilla1.Text = txt;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            showStep2();
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                Process.Start(folderPath);
            }
        }
    }
}
