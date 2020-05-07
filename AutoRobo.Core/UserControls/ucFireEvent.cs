using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;
using System;

namespace AutoRobo.UserControls
{
    public partial class ucFireEvent : ucBaseElement
    {
        public ucFireEvent(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var _event = (ActionFireEvent) base.Action;
                _event.FindMechanism = GuiToObject();
                _event.EventName = txtEventName.Text;
                _event.NoWait = chkNoWait.Checked;
                foreach (var item in lbParameters.Items)
                {
                    string[] arrItem = item.ToString().Split("=".ToCharArray());
                    if (_event.EventParameters == null) {
                        _event.EventParameters = new System.Collections.Specialized.NameValueCollection();
                    }
                    _event.EventParameters.Add(arrItem[0], arrItem[1]);
                }
                return base.Action;
            }
            set
            {
                var action = (ActionFireEvent) value;
                txtEventName.Text = action.EventName;
                chkNoWait.Checked = action.NoWait;
                if (action.EventParameters != null)
                {
                    for (int i = 0; i < action.EventParameters.Count; i++)
                    {
                        string key = action.EventParameters.GetKey(i);
                        lbParameters.Items.Add(key + "=" + action.EventParameters[key]);
                    }
                }
                base.Action = action;
                ObjectToGui(action);
            }
        }

        protected override void OnValidate()
        {
            if (string.IsNullOrEmpty(txtEventName.Text)) {
                throw new ApplicationException("事件名称不能为空");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtParameterName.Text))
            {
                MessageBox.Show("参数名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(txtParameterValue.Text))
            {
                MessageBox.Show("参数值不能为空");
                return;
            }
            lbParameters.Items.Add(txtParameterName.Text + "=" + txtParameterValue.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbParameters.SelectedIndex != -1) {
                lbParameters.Items.RemoveAt(lbParameters.SelectedIndex);
            }
        }
  
    }
}
