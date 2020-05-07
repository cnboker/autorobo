using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Models;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.UserControls
{
    
    public partial class VarManager : Form
    {
        public enum VarType { 
            Int,
            String,
            Array,
            Table
        }

        public enum Mode { 
            Add,
            Edit
        }

        private VariableActionModel<ActionVariable> variableActionModel;
        private ActionVariable currentVariable;

        private Mode model = Mode.Add;

        public VarManager(VariableActionModel<ActionVariable> variableActionModel)
        {
            this.variableActionModel = variableActionModel;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            listView1.Columns.Add("变量名称", 80);
            listView1.Columns.Add("变量类型", 90);
            ListViewBind();
            base.OnLoad(e);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            model = Mode.Add;
            typeComboBox.Enabled = true;
            namebox.Focus();
            namebox.Enabled = true;
            Reset();
        }

        private void ListViewBind() {
            listView1.Items.Clear();
            foreach (ActionVariable item in variableActionModel) {
                
                listView1.Items.Add(new ListViewItem(new string[] { item.Name, item.GetTypeName() }));
                if (item == currentVariable)
                {
                    listView1.Items[listView1.Items.Count - 1].Selected = true;
                }
            }
            
        }

        private ActionVariable CreateVariable(VarType varType) {
            if (varType == VarType.Int) {
                return new ActionIntegerVariable();
            }
            else if (varType == VarType.String) {
                return new ActionStringVariable();
            }
            else if (varType == VarType.Array) {
                return new ActionArrayVariable();
            }
            else if (varType == VarType.Table)
            {
                return new ActionTableVariable();
            }
            else {
                throw new NotImplementedException();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            if (MessageBox.Show("确实要删除选中变量吗?", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    variableActionModel.Remove(item.Text);
                }
                ListViewBind();
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {            
            if (model == Mode.Add)
            {
                if (string.IsNullOrEmpty(namebox.Text))
                {
                    MessageBox.Show("变量名称不能为空");
                    return;
                }
                if (variableActionModel.Any(c => c.ElementName == namebox.Text))
                {
                    MessageBox.Show(string.Format("已包含变量{0},变量名称不能重复", namebox.Text));
                    return;
                }
                if (typeComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择变量类型");
                    return;
                }
                if (directComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择变量方向");
                    return;
                }
                
                currentVariable = CreateVariable((VarType)typeComboBox.SelectedIndex);                
            }
            else {
                currentVariable = variableActionModel.Find(namebox.Text);
            }
            GuiToObject(currentVariable);
            if (model == Mode.Add) {
                variableActionModel.Add(currentVariable);
                ListViewBind();
                Reset();
            }
            MessageBox.Show("操作成功");
        }

        private void GuiToObject(ActionVariable var ) {
            var.Direction = (VariableDirection)directComboBox.SelectedIndex;
            var.Name = namebox.Text;
            var.DefaultValue = defaultValueTextBox.Text;                 
        }

        private void Reset() {
            namebox.Text = "";
            typeComboBox.SelectedIndex = -1;
            directComboBox.SelectedIndex = -1;
            defaultValueTextBox.Text = "";
            listView1.SelectedItems.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                string name = listView1.SelectedItems[0].Text;
                ActionVariable var = variableActionModel.Find(name);
                namebox.Text = var.Name;
                namebox.Enabled = false;
                typeComboBox.SelectedIndex = GetTypeVale(var);
                typeComboBox.Enabled = false;
                directComboBox.SelectedIndex = (int)var.Direction;
                defaultValueTextBox.Text = var.DefaultValue;
                model = Mode.Edit;
            }
        }

        private int GetTypeVale(ActionVariable var)
        {
            if (var is ActionIntegerVariable) {
                return 0;
            }
            else if (var is ActionStringVariable) {
                return 1;
            }
            else if (var is ActionArrayVariable) {
                return 2;
            }
            else if (var is ActionTableVariable)
            {
                return 3;
            }
            else {
                throw new NotImplementedException();
            }
        }
    }
}
