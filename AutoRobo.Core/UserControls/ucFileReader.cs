using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using System.IO;
using AutoRobo.Core.Actions.InOut;

namespace AutoRobo.Core.UserControls
{
    public partial class ucFileReader : ucBaseEditor
    {
        public ucFileReader(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionFileReader action = (ActionFileReader)base.Action;
                action.VariableName = varComboBox.SelectedItem.ToString();
                action.FileName = fileNameTextBox.Text;
                action.HasHeaderRecord = includeHeaderCheckBox.Checked;
                action.FileType = (FileType)comboBoxFormat.SelectedIndex;
                return action;
            }
            set
            {
                var action = (ActionFileReader)value;
                
                List<string> variables = new List<string>();
                variables.AddRange(value.VariableModel.OfType<ActionArrayVariable>().Select(c => c.Name).ToArray());
                variables.AddRange(value.VariableModel.OfType<ActionTableVariable>().Select(c => c.Name).ToArray());

                includeHeaderCheckBox.Checked = action.HasHeaderRecord;
                fileNameTextBox.Text = action.FileName;
                varComboBox.DataSource = variables;
                varComboBox.SelectedItem = action.VariableName;
                comboBoxFormat.SelectedIndex = (int)action.FileType;
                base.Action = action;
            }
        }

        private void fileBtn_Click(object sender, EventArgs e)
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
                fileNameTextBox.Text = dialog.FileName;
            }
        }

    }
}
