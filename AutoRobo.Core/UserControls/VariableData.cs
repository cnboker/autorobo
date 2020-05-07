using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.Core.UserControls.DTS;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    public partial class VariableData : Form
    {
        private ActionVariable variable;
        public VariableData(ActionVariable variable)
        {
            this.variable = variable;
            variable.DataUpdate -= new AutoRobo.Core.Actions.ActionVariable.VariableDataUpdate(rs_DataUpdate);
            variable.DataUpdate += new AutoRobo.Core.Actions.ActionVariable.VariableDataUpdate(rs_DataUpdate);
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            dataGridView1.Invalidate();
            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            dataGridView1.DataBind(GetDataSource());  
            base.OnLoad(e);
        }

        private DataTable GetDataSource() {
            object datasource = null;

            if (variable is ActionArrayVariable)
            {
                datasource = DataTableExtension.ConvertListToDataTable(variable.Name, (List<string>)variable.Data);
            }
            else if (variable is ActionTableVariable)
            {
                datasource = variable.Data;
            }
            else if (variable is ActionAttributeObject)
            {
                var data = ((ActionAttributeObject)variable).Data;
                if (data != null)
                {
                    datasource = ((AutoRobo.DataMapping.DataMap)data).ToDataTable();
                }
            }
            return datasource as DataTable;
        }
    

        void rs_DataUpdate(object sender, VariableDataArgs args)
        {
            if (InvokeRequired) {
                Invoke(new Action<object, VariableDataArgs>(rs_DataUpdate), sender, args);
                return;
            }
            dataGridView1.DataBind(GetDataSource());
        }
            
        protected override void OnClosed(EventArgs e)
        {            
            variable.DataUpdate -= new AutoRobo.Core.Actions.ActionVariable.VariableDataUpdate(rs_DataUpdate);
            base.OnClosed(e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataTable table = dataGridView1.DataSource as DataTable;
            
            if (table == null) return;
            DTSWizardBook book = new DTSWizardBook();
            book.Export(table);
        }
    }
}
