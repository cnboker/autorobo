using AutoRobo.Core.Actions;
using AutoRobo.Core.ns;
using Jint;
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoRobo.Core.UserControls
{
    public partial class StringRegexForm : Form
    {
        private StringPattern stringPattern;
        private string testData;
        private NameValueCollection replaceList = new NameValueCollection();
           
        public StringRegexForm()
        {
            InitializeComponent();
            listBox1.KeyDown += listBox1_KeyDown;
            regexRadio.CheckedChanged += CheckedChanged;
            scriptRadio.CheckedChanged += CheckedChanged;
            replaceRadioButton.CheckedChanged += CheckedChanged;
        }

        void CheckedChanged(object sender, EventArgs e)
        {            
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    UpdateTabPage(GetRadioChecked());
                }
            }
        }

        void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) {
                if (listBox1.SelectedIndex != -1)
                {
                    replaceList.Remove(replaceList.Keys[listBox1.SelectedIndex]);
                }
                UpdateListBox();
            }
        }
    
        public StringRegexForm(StringPattern stringPattern, string testData):this()
        {
       
            // TODO: Complete member initialization
            this.stringPattern = stringPattern;
            this.testData = testData;
            
            UpdateTabPage(stringPattern.Pattern);
            if (stringPattern.Pattern == StringMode.Javascript)
            {
                scriptRadio.Checked = true;
                textBox.Text = stringPattern.Expression;              
            }
            else if(stringPattern.Pattern == StringMode.Regex){
                regexRadio.Checked = true;
                regexTextbox.Text = stringPattern.Expression;
            }
            else if (stringPattern.Pattern == StringMode.Replace) {              
                replaceRadioButton.Checked = true;
                string[] arr = stringPattern.Expression.Split(",".ToCharArray());
                replaceList.Clear();
                for (int i = 0; i < arr.Length; i++)
                {                    
                    replaceList.Add(arr[i], arr[i+1]);
                    i++;
                }
                UpdateListBox();
            }
            
        }

        private void UpdateTabPage(StringMode model) {
            tabControl1.Controls.Clear();
            if (model == StringMode.Javascript)
            {
                tabControl1.Controls.Add(javascriptPage);     
            }
            else if (model == StringMode.Regex)
            {
                tabControl1.Controls.Add(regxPage);               
            }
            else if (model == StringMode.Replace)
            {
                tabControl1.Controls.Add(replacePage);                
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (stringPattern.Pattern == StringMode.Javascript)
            {
                if (string.IsNullOrEmpty(stringPattern.Expression))
                {
                    textBox.Text = @"//if(arguments[0].indexOf('abc') > 0){
//    return 'ok';
//}
//return arguments[0];
                ";
                }

                javascriptTipLabel.Visible = true;
            }
            textBox.Text = stringPattern.Expression;
            base.OnLoad(e);
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            string result = ShowDialog(testData, "请输入测试字符串");
            if (stringPattern.Pattern == StringMode.Javascript)
            {
                JintEngine engine = JintCreator.Create();
                try
                {
                    //处理新行参数
                    //ref:http://stackoverflow.com/questions/9449181/new-line-character-as-function-parameter-in-javascript
                    result = result.Replace("\r\n","\n").Replace("\n","\\n").Replace("'","\\'");
                    var val = engine.Run("(function(){" + textBox.Text + "})('" + result + "')");
                    MessageBox.Show(val == null ? "" : val.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (stringPattern.Pattern == StringMode.Regex)
            {
                Regex reg = new Regex(textBox.Text);
                Match match = reg.Match(result);
                if (match.Success)
                {
                    MessageBox.Show(match.Value);
                }
                else {
                    MessageBox.Show("匹配不成功");
                }
            }
            else if (stringPattern.Pattern == StringMode.Replace)
            {
                result = result.Replace(" ", "");
                for (int i = 0; i < replaceList.Count; i++) {
                    result = result.Replace(replaceList.Keys[i], replaceList[i]);
                }
                MessageBox.Show(result);
            }
        }

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 280;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = caption };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Multiline=true, Height=120, Text=text };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 180 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }
     

        private void regexHelper_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = regexHelper.SelectedItem.ToString();
            this.regexTextbox.Text = str.Substring(str.IndexOf(" ") + 1);
        }

        public StringPattern Pattern { get; set; }

        private StringMode GetRadioChecked() {
            StringMode pattern = StringMode.None;
            if (scriptRadio.Checked) pattern = StringMode.Javascript;
            if (regexRadio.Checked) pattern = StringMode.Regex;
            if (replaceRadioButton.Checked) pattern = StringMode.Replace;
            return pattern;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            StringMode pattern = GetRadioChecked();
           
            string expression = textBox.Text;
            if (pattern == StringMode.Javascript) {
                expression = textBox.Text;
            }
            else if (pattern == StringMode.Regex) {
                expression = regexTextbox.Text;
            }
            if (pattern == StringMode.Replace)
            {               
                for (int i = 0; i < replaceList.Count; i++)
                {
                    if (i == 0)
                    {
                        expression = "";
                    }
                    else {
                        expression += ",";
                    }
                    expression += replaceList.GetKey(i) + "," + replaceList.Get(i);
                }
            }
            Pattern = new StringPattern()
            {
                Expression = expression,
                Pattern = pattern
            };
           
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!replaceRadioButton.Checked) return;
            if (string.IsNullOrEmpty(sourceTextbox.Text)) return;
            replaceList.Add(sourceTextbox.Text, targetTextbox.Text);
            UpdateListBox();
        }

        private void UpdateListBox() {
            listBox1.Items.Clear();
            for(int i = 0; i < replaceList.Count; i++){
                listBox1.Items.Add(string.Format("'{0}'替换为'{1}'",replaceList.GetKey(i), replaceList.Get(i)));
            }
        }
    }
}
