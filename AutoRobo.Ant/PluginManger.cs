using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoRobo.Core.Plugins;
using AutoRobo.ClientLib.Properties;
using System.Linq;

namespace AutoRobo.ClientLib
{
    public class PluginManger
    {
        private IPluginHost host = null;

        public List<IPlugin> plugins = new List<IPlugin>();

        public PluginManger(IPluginHost host)
        {
            this.host = host;
            host.SendKeyHandler += new SendKeysEvent(host_SendKeyHandler);
        }

        public IPlugin Register(string pluginName)
        {
            IPlugin plugin = QueryOrCreate(pluginName);
            InstallStripItem(pluginName, plugin.StripItemText, plugin.StripItemImage, false);
            return plugin;
        }

        private IPlugin QueryOrCreate(string pluginName)
        {
            IPlugin plugin = null;
            if (!plugins.Any(c => c.GetType().Name == pluginName))
            {
                plugin = PluginLoader.Loader.Query(pluginName);
                plugin.Host = this.host;
                Control c = plugin as Control;
                if (c != null)
                {
                    c.Disposed += new EventHandler(c_Disposed);
                }
                plugins.Add(plugin);
            }
            else {
                plugin = plugins.SingleOrDefault(c => c.GetType().Name == pluginName);
            }
            if (plugin == null) throw new ApplicationException(string.Format("{0}对于的插件未找到", pluginName));
            return plugin;
        }

        public void InstallStripItem(string pluginName, string text, System.Drawing.Bitmap image, bool check)
        {
            if (image == null) return;
            ToolStripButton  btn = new System.Windows.Forms.ToolStripButton();
            btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btn.Image = image;
            btn.Checked = check;
            btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            btn.Name = pluginName;
            btn.Size = new System.Drawing.Size(28, 28);
            btn.Text = text;
            host.Strip.Items.Add(btn);
            btn.Click += new EventHandler(btn_Click);
        }

        void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            foreach (IPlugin entity in plugins)
            {
                if (entity.GetType().Name == btn.Name)
                {
                    entity.Show();
                }
            }     
        }
   
        void c_Disposed(object sender, EventArgs e)
        {
            IPlugin plugin = sender as IPlugin;
            plugins.Remove(plugin);
            QueryOrCreate(plugin.GetType().Name);           
        }

        void host_SendKeyHandler(Keys keyData)
        {
            foreach (IPlugin entity in plugins)
            {
                if (entity.DataKey == keyData) {
                    entity.Show();
                }
            }     
        }
      
    }
}
