using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using AutoRobo.Core.Models;
using AutoRobo.DataMapping;
using AutoRobo.Core.Actions;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    /// <summary>
    /// 输入参数绑定
    /// </summary>
    public partial class ActionBindingBridge : UserControl
    {

        private VariableActionModel<ActionVariable> variableActions;
        /// <summary>
        /// 获取绑定字段
        /// </summary>
        public string BindName {
            get {
                string cb2Name = cb2.SelectedItem == null ? "" : cb2.SelectedItem.ToString();
                if (cb1.SelectedItem == null)
                {
                    return cb2Name;
                }
                else {
                    return cb1.SelectedItem.ToString() + "." + cb2Name;
                }
            }
        }

        public ActionBindingBridge()
        {
            InitializeComponent();         
        }

        public void Initialize(VariableActionModel<ActionVariable> actions) {
            this.variableActions = actions;
            cb1.Items.Clear();
            var InputVariables = GetInputVariableName();
            foreach (var o in InputVariables)
            {
                cb1.Items.Add(o);
            }
            if (cb1.Items.Count > 0)
            {
                cb1.SelectedIndex = 0;
                cb1_SelectedIndexChanged(cb1, EventArgs.Empty);
            }
        }

        /// <summary>
        /// bindName format "varname.mapName"
        /// </summary>
        /// <param name="bindName"></param>
        public void Bind(string bindName) {
            if (string.IsNullOrEmpty(bindName)) return;
            string[] binds = bindName.Split(".".ToCharArray());
            if (binds.Length == 2)
            {
                string tableName = binds[0];
                string mapName = binds[1];
                if (variableActions == null) throw new ApplicationException("未初始化异常");
                cb1.SelectedItem = tableName;

                UpdateCB2();
                cb2.SelectedItem = mapName;
            }
            else { //兼容老版本
                cb2.Text = bindName;
            }
        }

        private void UpdateCB2() {
            cb2.Items.Clear();
            var InputVariables = GetInputVariableSchema(cb1.SelectedItem.ToString());
            foreach (var o in InputVariables)
            {
                cb2.Items.Add(o);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            cb1.SelectedIndexChanged += new EventHandler(cb1_SelectedIndexChanged);           
            base.OnLoad(e);
        }

        void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB2();
        }

        private string[] GetInputVariableName()
        {
            List<string> list = new List<string>();
            foreach (ActionVariable var in variableActions)
            {
                if (var.Direction == VariableDirection.InOut || var.Direction == VariableDirection.Input)
                {
                    if (var is ActionAttributeObject || var is ActionTableVariable || var is ActionArrayVariable)
                    {
                        list.Add(var.Name);
                    }
                }
            }
            return list.ToArray();
        }

        private string[] GetInputVariableSchema(string name)
        {
            ActionVariable var = variableActions.Find(name);
            if (var is ActionAttributeObject)
            {
                return ((IMapAttribute)var.Data).Fields.Select(c => c.DisplayName).ToArray();
            }
            else if (var is ActionTableVariable)
            {
                List<string> list = new List<string>();
                foreach (DataColumn c in ((DataTable)var.Data).Columns)
                {
                    list.Add(c.ColumnName);
                }
                return list.ToArray();
            }
            else if (var is ActionArrayVariable) {
                List<string> list = new List<string>();
                list.Add("DataItem");
                return list.ToArray();
            }
            else
            {
                throw new ApplicationException(string.Format("{0}对于变量数据类型错误", name));
            }
        }
    }
}
