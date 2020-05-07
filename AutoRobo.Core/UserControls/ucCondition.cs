using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using Util;

namespace AutoRobo.Core.UserControls
{
    public partial class ucCondition : ucBaseElement
    {
        public ucCondition(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionCondition)base.Action;
                action.FindMechanism = GuiToObject();
                action.Mode = matchModeList.SelectedIndex == 0 ? ActionCondition.MatchMode.Exist : ActionCondition.MatchMode.Content;
                if (action.Mode == ActionCondition.MatchMode.Content) {
                    action.Ignorecase = ignorecaseCheckbox.Checked;
                    action.Pattern = patternTextbox.Text;
                    action.TextFormat = (ActionCondition.AvailableTextFormat)Enum.Parse(typeof(ActionCondition.AvailableTextFormat), ddlFormat.SelectedItem.ToString());
                }
                return action;
            }
            set
            {
                var action = (ActionCondition)value;
                if (action.Mode == ActionCondition.MatchMode.Content)
                {
                    ignorecaseCheckbox.Checked = action.Ignorecase;
                    ddlFormat.SelectedItem = action.TextFormat.ToString();
                    patternTextbox.Text = action.Pattern;
                    gb.Visible = true;
                }
                matchModeList.SelectedIndex = (action.Mode == ActionCondition.MatchMode.Exist ? 0 : 1);
                base.Action = value;
                ObjectToGui((ActionElementBase)value);
            }
        }

        protected override void OnValidate()
        {
            if (string.IsNullOrEmpty(patternTextbox.Text) && gb.Visible) {
                throw new ApplicationException("表达式匹配不能为空");
            }
        }

        private void helpBtn_Click(object sender, EventArgs e)
        {
            StringHelper.OpenIE("http://baike.baidu.com/view/94238.htm");
        }

        private void matchModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            gb.Visible = !(matchModeList.SelectedIndex == 0);
        }
    }
}
