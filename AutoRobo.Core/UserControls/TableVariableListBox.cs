using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.UserControls
{
    public partial class TableVariableListBox : CheckedListBox
    {

        public void Set(ActionVariable[] vars, ActionVariable[] selectedItems)
        {
            DataSource = vars;
            DisplayMember = "Name";
            ValueMember = "Name";
            for (int i = 0; i < Items.Count; i++)
            {
                if (selectedItems == null) break;
                if (selectedItems.Contains(Items[i]))
                {
                    SetItemChecked(i, true);
                }
            }
        }

        /// <summary>
        /// 获取勾选变量名称，多个以逗号分割
        /// </summary>
        /// <returns></returns>
        public string Get() {
            List<string> vars = new List<string>();
            for (int i = 0; i < CheckedItems.Count; i++)
            {
                ActionVariable var = CheckedItems[i] as ActionVariable;
                vars.Add(var.Name);
            }
            return string.Join(",", vars);
        }
    }
}
