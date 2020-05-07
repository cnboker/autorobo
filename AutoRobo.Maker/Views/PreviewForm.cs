using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using AutoRobo.Core.IO;
using AutoRobo.DB;
using WeifenLuo.WinFormsUI.Docking;

namespace AutoRobo.Makers.Views
{
    public partial class PreviewForm : ViewBase
    {
        public PreviewForm()
        {
            this.InitializeComponent();
            this.Initialized += PreviewForm_Initialized;                     
            CloseButtonVisible = false;
            listView1.KeyDown += listView1_KeyDown;
        }

        void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C) {
                if (listView1.SelectedItems.Count > 0) {
                    Clipboard.SetText(listView1.SelectedItems[0].Text);
                }
            }
        }

        void PreviewForm_Initialized(object sender, EventArgs e)
        {
            Context.PropertyChanged += Context_PropertyChanged;
        }

        void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActionModel")
            {
                //变量容器有变化，则更新变量列表             
                Context.ActionModel.VariableActionModel.CollectionChanged -= VariableActionModel_CollectionChanged;
                Context.ActionModel.VariableActionModel.CollectionChanged += VariableActionModel_CollectionChanged;
            }
        }

      

        void VariableActionModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            VariableBinding();
        }
        void ActionModel_DataLoaded(object sender, EventArgs e)
        {
            VariableBinding();
        }

        private void VariableBinding()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(VariableBinding));
                return;
            }
            
            var vars = Context.ActionModel.VariableActionModel.OfType<ActionTableVariable>().ToArray();

            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.AddRange(vars);
            toolStripComboBox1.ComboBox.ValueMember = "Name";
            toolStripComboBox1.Tag = vars;

            if (vars.Length > 0)
            {
                toolStripComboBox1.SelectedIndex = 0;
            }
        }

      

        private void CustomBind() {
            
           ActionTableVariable variable = Context.ActionModel.VariableActionModel.OfType<ActionTableVariable>().FirstOrDefault(c => c.Name == toolStripComboBox1.Text);
           DataTable sourceTable = variable.Data as DataTable;
            if (sourceTable == null) return;
            listView1.Clear();
            foreach (DataColumn c in sourceTable.Columns)
            {
                listView1.Columns.Add(c.ColumnName);
            }
            int maxRow = sourceTable.Rows.Count > 30 ? 30 : sourceTable.Rows.Count;
            for (int i = 0; i < maxRow; i++)
            {
                listView1.Items.Add(new ListViewItem(sourceTable.Rows[i].ItemArray.Select(c => c == null ? "" : c.ToString()).ToArray()));
            }   
        }

        private void seachBtn_Click(object sender, EventArgs e)
        {
            CustomBind();
        }

      
    }
}
