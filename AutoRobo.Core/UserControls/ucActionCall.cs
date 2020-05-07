using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    public partial class ucActionCall : ucBaseEditor
    {
        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;                
                if (scriptListBox.SelectedItem != null)
                {
                    if (base.Action is ActionCall)
                    {
                        ((ActionCall)base.Action).FunName = scriptListBox.SelectedItem.ToString();
                    }
                    else if (base.Action is ModuleCall) {
                        ((ModuleCall)base.Action).FunName = scriptListBox.SelectedItem.ToString();
                    }
                }
                return base.Action;
            }
            set
            {
              
                if (value is ModuleCall) {
                    var action = (ModuleCall)value;
                    IActionRepository repository = ServiceLocator.Instance.GetService<IActionRepository>();
                    var list = repository.GetActionModulers();
                    scriptListBox.DataSource = list;
                    scriptListBox.SelectedItem = action.FunName;
                    base.Action = action;
                }
                else if (value is ActionCall) {
                    var action = (ActionCall)value;
                    var list = action.AppContext.ActionModel.SubActionModel.Select(c => ((ActionScriptPart)c).Name).ToArray();
                    scriptListBox.DataSource = list;
                    scriptListBox.SelectedItem = action.FunName;
                    base.Action = action;
                }
                
            }
        }

        public ucActionCall(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        protected override void OnValidate()
        {
            if (scriptListBox.SelectedItem == null)
            {
                throw new ApplicationException("请选择调用对象");
            }           
        }      
    }
}
