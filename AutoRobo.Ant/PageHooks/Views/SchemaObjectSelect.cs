using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoRobo.WebApi;
using Newtonsoft.Json;
using System.Linq;
using AutoRobo.WebApi.Entities;
using Util;

namespace AutoRobo.ClientLib.PageHooks.Views
{
    public partial class SchemaObjectSelect : Form
    {
       
        private string oldSelect;
        private List<SchemaObject> source = null;
        private string modelId = null;

        public FakeHttpContext Context { get; set; }
        public bool AppendOutLink { get { return checkBox1.Checked; } }

        public string SchemaObjectId {
            get {
                return comboBox1.SelectedValue.ToString();
            }
        }

        public SchemaObjectSelect(string modelId, List<SchemaObject> source)
        {
            this.source = source;
            this.modelId = modelId;
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            okBtn.Click += new EventHandler(okBtn_Click);
         
            comboBox1.DataSource = source;
            comboBox1.DisplayMember = "Title";
            comboBox1.ValueMember = "Id";
            oldSelect = source.Where(c => c.IsDefault ?? false).Select(c => c.Id).FirstOrDefault();
           
            if (string.IsNullOrEmpty(oldSelect))
            {
                if (source.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
            else {
                comboBox1.SelectedValue = oldSelect;
            }
            base.OnLoad(e);
        }

        void okBtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("oldId", oldSelect == null ? "0" : oldSelect.ToString());
            dict.Add("newId", comboBox1.SelectedValue.ToString());
            HttpRequestWapper.Post_Data(ServerApiInvoker.APIRoot + "setSchemaObjectDefault", dict);
        }

        private void contentBtn_Click(object sender, EventArgs e)
        {
            Context.GoTo(StringHelper.Domain + "/portal/thread/index/" + modelId, "_blank");
        }
    }
}
