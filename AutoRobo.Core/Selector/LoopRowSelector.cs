using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native.InternetExplorer;
using System.Windows.Forms;
using WatiN.Core;
using mshtml;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Interface;

namespace AutoRobo.Core
{
    public class LoopRowSelector : ElementSelector
    {
        private ToolStripButton first;
        private ToolStripButton last;
        private ToolStripButton next;
        private ToolStripButton previous;
        private int position = 0;
        private int head;
        private int tail;
        private ActionForeach actionForeach;
        private IElementContainer contextElmenet = null;

        private static LoopRowSelector selector = null;

        LoopRowSelector()
        {
        }

        public static LoopRowSelector Instance
        {
            get
            {
                if (selector == null)
                {
                    selector = new LoopRowSelector();
                }
                return selector;
            }
        }

        public IElementContainer ContextElement
        {
            get
            {
                return contextElmenet;
            }
        }

        private List<IHTMLElement> elementRows
        {
            get {
                if (this.actionForeach == null) return null;
                return this.actionForeach.GetIHTMLElements(); 
            }
        }

        public override void BeforeHighlight(CssElement element)
        {
            element.OutlineColor = "#0082ff";
        }

        public void RegisterHandler(ToolStripButton first, ToolStripButton last, ToolStripButton next,
            ToolStripButton previous)
        {
            this.first = first;
            this.last = last;
            this.next = next;
            this.previous = previous;
            first.Enabled = false;
            last.Enabled = false;
            next.Enabled = false;
            previous.Enabled = false;
            first.Click += new EventHandler(first_Click);
            last.Click += new EventHandler(last_Click);
            next.Click += new EventHandler(next_Click);
            previous.Click += new EventHandler(previous_Click);
        }

        public void Enter(ActionForeach actionForeach)
        {
            this.actionForeach = actionForeach;
            head = actionForeach.FirstNumber;
            tail = this.elementRows.Count - actionForeach.LastNumber;
            position = head;
            UpdateStatus();
        }

        public void Exit()
        {
            this.actionForeach = null;
            UpdateStatus();
            Restore();
        }

        /// <summary>
        /// 检查是否在迭代活动加亮状态，如果是增加活动的元素必须是在加亮区域选择，如果不在则提示用户
        /// 
        /// </summary>
        /// <param name="child"></param>
        public bool Check(IHTMLElement child)
        {
            if (child == null) return false;
            if (!Highlighting) return false;
            return child.IsChildOf(SelectorElement);
        }

        /// <summary>
        /// 获取CSS Selector(当包含上下文的时候调用此方法)
        /// http://stackoverflow.com/questions/3390396/how-to-check-for-undefined-in-javascript
        /// </summary>
        /// <param name="element"></param>
        public void SetSelector(mshtml.IHTMLElement element)
        {
            var originalId = element.id;
            if (string.IsNullOrEmpty(originalId))
            {
                var id = "__" + IEUtils.IEVariableNameHelper.CreateVariableName();
                element.id = id;
            }
            mshtml.IHTMLWindow2 parentWindow = ((IHTMLDocument2)(element.document)).parentWindow as mshtml.IHTMLWindow2;
            IEUtils.RunScript(ScriptLoader.GetJquerySelector(""), parentWindow);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("var el = $jq('#{0}');", element.id));
            sb.AppendLine();

            sb.Append("if(typeof __context == 'undefined'){");
            sb.AppendLine();
            sb.Append("__context = '';");
            sb.AppendLine();
            sb.Append("}");
            sb.AppendLine();
            sb.Append(" var selector = el.getCssPath(__context);");
            sb.AppendLine();

            sb.Append("el.attr('__selector', selector);");
            //sb.Append("alert(el.attr('__selector'));");
            //sb.AppendLine();

            IEUtils.RunScript(sb.ToString(), parentWindow);
            element.id = originalId;

        }
        void previous_Click(object sender, EventArgs e)
        {
            position--;
            if (position < head)
            {
                position = head;
            }
            UpdateStatus();
        }

        void next_Click(object sender, EventArgs e)
        {
            position++;
            if (position >= tail)
            {
                position = tail - 1;
            }
            UpdateStatus();
        }

        void last_Click(object sender, EventArgs e)
        {
            position = tail - 1;
            UpdateStatus();
        }

        void first_Click(object sender, EventArgs e)
        {
            position = head;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (elementRows == null || elementRows.Count == 0)
            {
                first.Enabled = false;
                last.Enabled = false;
                next.Enabled = false;
                previous.Enabled = false;
            }
            else
            {
                if (position == head)
                {
                    first.Enabled = false;
                    previous.Enabled = false;
                    last.Enabled = true;
                    next.Enabled = true;
                }
                else if (position == tail - 1)
                {
                    first.Enabled = true;
                    previous.Enabled = true;
                    last.Enabled = false;
                    next.Enabled = false;
                }
                else
                {
                    first.Enabled = true;
                    previous.Enabled = true;
                    last.Enabled = true;
                    next.Enabled = true;
                }
                Restore();
                Highlight(elementRows[position]);
                contextElmenet = actionForeach.GetElements()[position] as IElementContainer;
                ///为遍历子活动设置上下文元素
                foreach (ActionBase action in this.actionForeach.Actions)
                {
                    if (action is IElementBinding)
                    {
                        ((IElementBinding)action).ContextElement = contextElmenet;
                    }
                }
            }

        }

    }
}
