using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using System.IO;
using System.Linq;
using AutoRobo.Core.Actions.InOut;
using System.Text.RegularExpressions;

namespace AutoRobo.Core.UserControls
{
    public partial class ucFileWriter : ucBaseEditor
    {
        public ucFileWriter(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionFileWriter action = (ActionFileWriter)base.Action;                
                action.VariableName = sourceCheckedListBox.Get();
                action.FileName = fileTextbox.Text;
                action.HasHeaderRecord = headerCheckbox.Checked;                
                action.FileType = (FileType)comboBoxFormat.SelectedIndex;
                action.Reset();
                return action;
            }
            set
            {
                var action = (ActionFileWriter)value;
                var source = value.VariableModel.OfType<ActionTableVariable>();
                var selectedItems = value.VariableModel.GetVariableObjectsByName(action.VariableName.Split(",".ToCharArray()));

                sourceCheckedListBox.Set(source.ToArray(), selectedItems);
                headerCheckbox.Checked = action.HasHeaderRecord;
                if (string.IsNullOrEmpty(action.FileName))
                {
                    fileTextbox.Text = action.ActionModel.ProjectName;
                }
                else
                {
                    fileTextbox.Text = action.FileName;
                }
                comboBoxFormat.SelectedIndex = (int)action.FileType;
                base.Action = action;
            }
        }

        protected override void OnValidate()
        {
            if (sourceCheckedListBox.SelectedItems.Count == 0)
            {
                throw new ApplicationException("请选择要保存的内容");
            }
            RequiredValidate(fileTextbox.Text, "文件名不能为空");

            string path = Path.Combine(AppSettings.Instance.LibraryPath, "data");
            //校验文件名是否合法
            if (IsValidFilename(fileTextbox.Text)) {
                throw new ApplicationException("文件名无效");
            }
           // FileInfo info = new System.IO.FileInfo(Path.Combine(path, fileTextbox.Text));
        }

        bool IsValidFilename(string testName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            return invalidChars.Any(t=>testName.Contains(t));
        }

        private void browserBtn_Click(object sender, EventArgs e)
        {           
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = false;
            switch (this.comboBoxFormat.SelectedIndex)
            {
                case 0:
                    dialog.DefaultExt = "csv";
                    dialog.Filter = "CSV file (*.csv)|*.csv| All files (*.*)|*.*";
                    break;

                case 1:
                    dialog.DefaultExt = "tsv";
                    dialog.Filter = "TSV file (*.tsv)|*.tsv| All files (*.*)|*.*";
                    break;
                case 2:
                    dialog.DefaultExt = "xls";
                    dialog.Filter = "XLS file (*.xls)|*.xls| All files (*.*)|*.*";
                    break;
                case 3:
                    dialog.DefaultExt = "xml";
                    dialog.Filter = "XML file (*.xml)|*.xml| All files (*.*)|*.*";
                    break;

                case 4:
                    dialog.DefaultExt = "json";
                    dialog.Filter = "JSON file (*.json)|*.json| All files (*.*)|*.*";
                    break;
            }
            dialog.InitialDirectory = Path.Combine(AppSettings.Instance.CurrentExecutePath, "data");
            if (DialogResult.OK == dialog.ShowDialog())
            {
                fileTextbox.Text = dialog.FileName.Replace(dialog.InitialDirectory,"");
            }
        }
     
    }
}
