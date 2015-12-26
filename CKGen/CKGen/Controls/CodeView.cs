using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;

namespace CKGen.Controls
{
    public partial class CodeView : UserControl
    {
        private ScintillaNET.Scintilla scintilla1;
        public CodeView()
        {
            InitializeComponent();
        }

        private void UCCodeShow_Load(object sender, EventArgs e)
        {
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.Controls.Add(this.scintilla1);
            this.scintilla1.Dock = DockStyle.Fill;
            this.scintilla1.TabIndex = 1;
            this.scintilla1.Text = "scintilla1";

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

        public void Show(string code)
        {
            this.scintilla1.Text = code;
        }
    }
}
