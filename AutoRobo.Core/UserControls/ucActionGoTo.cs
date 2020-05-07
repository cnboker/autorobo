
using System.Linq;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.UserControls;

namespace AutoRobo.Core
{
    /// <summary>
    /// 选择跳转
    /// </summary>
    public partial class ucActionGoTo : ucBaseEditor
    {

        public ucActionGoTo(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var nav = (ActionGoTo)base.Action;

                if (skipElementList.SelectedItem != null)
                {
                    string acitonId = nav.AppContext.ActionModel.MainActionModel[skipElementList.SelectedIndex].ID.ToString();
                    nav.NActionID = acitonId;
                }
                return nav;
            }
            set
            {
                var action = (ActionGoTo)value;
                var list = action.AppContext.ActionModel.MainActionModel.Select(c => (c.ActionDisplayName + " " + c.ElementName)).ToArray();
                
                skipElementList.DataSource = list;
                skipElementList.SelectedIndex = action.AppContext.ActionModel.MainActionModel.GetActiveIndex(action.NActionID);
                base.Action = action;
            }
        }
   
    }
}
