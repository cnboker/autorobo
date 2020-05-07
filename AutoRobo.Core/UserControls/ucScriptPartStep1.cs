using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    public partial class ucScriptPartStep1 : ucBaseEditor
    {
        public ucScriptPartStep1(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionScriptPart action = (ActionScriptPart)base.Action;
                action.Name = nameTextBox.Text;
                return action;
            }
            set
            {
                ActionScriptPart action = (ActionScriptPart)value;
                nameTextBox.Text = action.Name;
                base.Action = action;
            }
        }

        protected override void OnValidate()
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                throw new ApplicationException("必须输入活动逻辑名称");                
            }
            Action.AppContext.ActionModel.Switch(Models.StatementType.Sub);
            Action.ParentTest = Action.AppContext.ActionModel.SubActionModel;
            var any = Action.ParentTest.Where(c => ((ActionScriptPart)c).Name == nameTextBox.Text);
            if (Action.RowStatue == ActionStatue.New)
            {
                if (any.Count() > 0)
                {
                    throw new ApplicationException("名称不能重复");
                }
            }
            else {
                if (any.Count() > 1)
                {
                    throw new ApplicationException("名称不能重复");
                }
            }
           
        }
       
    }
}
