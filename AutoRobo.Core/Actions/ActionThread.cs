
using System;
using System.Xml;
using AutoRobo.UserControls;
using AutoRobo.Core.UserControls;
using System.Threading;
using System.Collections;
namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 线程活动
    /// </summary>
    [Serializable]   
    public class ActionThread : ActionDataBlock
    {
        public ActionThread() {            
            ThreadCount = 1;
        }
        /// <summary>
        /// 线程数目
        /// </summary>
        public int ThreadCount { get; set; }

        public override string ActionDisplayName
        {
            get { return "线程"; }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("thread.png");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucThread(editorAction);
        }

        /// <summary>
        /// 多线程并发执行任务
        /// </summary>
        public override void Perform() 
        {
            if (ThreadCount == 0) ThreadCount = 1;
            ArrayList list = new ArrayList();
            for (int i = 0; i < ThreadCount; i++)
            {
                Thread thread = new Thread(() =>
                {
                    base.Perform();
                });
                list.Add(thread);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();                
            }
            for (int i = 0; i < list.Count; i++) {
                Thread thread = list[i] as Thread;
                thread.Join();
            }
        }

        public override string GetDescription()
        {
            return "多线程";
        }



        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            ThreadCount = Convert.ToInt32(GetAttribute(node,"ThreadCount"));
        }

  

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("ThreadCount", ThreadCount.ToString());
            base.WriterAddAttribute(writer);
        }
    }
}
