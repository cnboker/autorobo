using System;
using System.Xml;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;
using AutoRobo.Core.Interface;

namespace AutoRobo.Core.Actions
{
    public class ActionBrowser : ActionDataBlock
    {
        public bool DownloadImages { get; set; }
        public bool DownloadSounds { get; set; }
        public bool DownloadVideo { get; set; }
        public bool DownloadActivex { get; set; }
        public bool DownloadFlash { get; set; }
        public bool DownloadScript { get; set; }
             
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visibility { get; set; }

        //多线程状态下，不同线程读取私有数据
        //ref:http://www.hanselman.com/blog/StoringThingsOnThreads.aspx
        private System.LocalDataStoreSlot slot = System.Threading.Thread.AllocateDataSlot();
        
        public override string ActionDisplayName
        {
            get { return "浏览器"; }
        }
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("browser.png");
        }
        /// <summary>
        /// 构造新浏览器页面
        /// </summary>
        /// <param name="host">页面驻留位置</param>
        public ActionBrowser()
        {            
            DownloadScript = true;
            Visibility = true;       
        }

        public override void PrePerform(ActionBase action)
        {         
            //运行活动由新的浏览器代替
            //不同线程可以读取自己创建的浏览器对象
           // action.CoreBrowser = System.Threading.Thread.GetData(slot) as MyBrowser;           
        }

        public override void PostPerform(ActionBase action)
        {
            //还原主浏览器
           // action.CoreBrowser = AppContext.Browser;
        }

        public override void Perform()
        {            
            MyBrowser newBrowser = System.Threading.Thread.GetData(slot) as MyBrowser;
            if (newBrowser == null)
            {
                newBrowser = CreateBrowser();
                System.Threading.Thread.SetData(slot, newBrowser); 
            }
            base.Perform();           
            //WatinContextFactory.Remove(newBrowser);
            //将运行浏览器加载到浏览器
            IViewArea view = Ioc.Container.Get<IViewArea>();            
            view.Clear();
        }

        private MyBrowser CreateBrowser()
        {
            if (((MyBrowser)AppContext.Browser).InvokeRequired)
            {
                return (MyBrowser)((MyBrowser)AppContext.Browser).Invoke(new Func<MyBrowser>(CreateBrowser));                
            }
            MyBrowser newBrowser = new MyBrowser()
            {
                DownloadActiveX = this.DownloadActivex,
                DownloadFlash = this.DownloadFlash,
                DownloadScripts = this.DownloadScript,
                DownloadImages = this.DownloadImages,
                DownloadSounds = this.DownloadSounds,
                DownloadVideo = this.DownloadVideo,
                Visible = this.Visibility
            };

            IViewArea view = Ioc.Container.Get<IViewArea>();            
            //加载浏览器
            view.AddControl(newBrowser);

            float zoomvalue = (view.Width * 100 / ((MyBrowser)AppContext.Browser).FindForm().Width);
            logger.Info("zoomvalue :" + zoomvalue);
            if (zoomvalue < 10)
            {
                zoomvalue = 10;
            }
            newBrowser.SetOpticalZoomValue((int)zoomvalue);
            return newBrowser;
        }

        public override string GetDescription()
        {
            return "增加新页面，同时设置浏览器参数"; 
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucBrowser(editorAction);
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            DownloadActivex = GetBooleanAttribute(node, "DownloadActivex");
            DownloadFlash = GetBooleanAttribute(node, "DownloadFlash");
            DownloadScript = GetBooleanAttribute(node, "DownloadScript");
            DownloadImages = GetBooleanAttribute(node, "DownloadImages");
            DownloadSounds = GetBooleanAttribute(node, "DownloadSounds");
            DownloadVideo = GetBooleanAttribute(node, "DownloadVideo");
            Visibility = GetBooleanAttribute(node, "Visibility");
        }

   
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("DownloadActivex", DownloadActivex ? "1" : "0");
            writer.WriteAttributeString("DownloadFlash", DownloadFlash ? "1" : "0");
            writer.WriteAttributeString("DownloadImages", DownloadImages ? "1" : "0");
            writer.WriteAttributeString("DownloadSounds", DownloadSounds ? "1" : "0");
            writer.WriteAttributeString("DownloadVideo", DownloadVideo ? "1" : "0");
            writer.WriteAttributeString("Visibility", Visibility ? "1" : "0");
            base.WriterAddAttribute(writer);
        }

    
    }
}
