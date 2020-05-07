using System;
using System.Collections.Generic;
using System.Linq;
using AutoRobo.Core.Actions;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace AutoRobo.Core.ns
{
    /// <summary>
    /// autorobo javascript api
    /// </summary>
    public class Q
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ns.Q");
        private IAppContext context = null;
        private Browser _browser = null;

        private Browser Browser {
            get {   
                return _browser;
            }
        }

        public Q(IAppContext context, Browser browser)
        {
            this.context = context;
            this._browser = browser;           
        }

        public object GetVariable(string name) {
            return context.ActionModel.VariableActionModel.Find(name);
        }
               
        /// <summary>
        /// 调用脚本块
        /// </summary>
        /// <param name="actionBlockName"></param>
        public void call(string actionBlockName) {
            
            ActionBase action = context.ActionModel.FindAction(Models.StatementType.Sub, actionBlockName);
            
            if (action is ActionScriptPart) {
                ActionScriptPart scriptPart = action as ActionScriptPart;
                scriptPart.Perform();
            }
        }
      
        /// <summary>
        /// css3 selector
        /// </summary>
        /// <param name="selector"></param>
        public ElementWrapper find(string selector)
        {
            return Method<ElementWrapper>.Watch("css Selector:" + selector, () =>
            {
                //使用内置selector
                return findMany(selector).FirstOrDefault();
            });
        }

        
        /// <summary>
        /// css3 selector
        /// </summary>
        /// <param name="selector"></param>
        private IEnumerable<ElementWrapper> findMany(string selector)
        {
            return Method<IEnumerable<ElementWrapper>>.Watch("css Selector:" + selector, () =>
           {
               return context.Browser.SelectMany(selector).Select(c => new ElementWrapper(Browser, c));
           });           
        }

        /// <summary>
        /// css3 selector many
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="action"></param>
        public void findMany(string selector, Jint.Delegates.Action<int, ElementWrapper> action)
        {
            IEnumerable<ElementWrapper> elements = findMany(selector);
            int index = 0;

            foreach (var item in elements)
            {
                try
                {
                    action(index++, item);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }
            }
        }

        public void findMany(string tagName, string className, Jint.Delegates.Action<int, ElementWrapper> action)
        {
            DateTime now = DateTime.Now;
            var elements = Browser.ElementsWithTag(tagName).Where(c => c.ClassName == className);
            int count = elements.Count();
            logger.Info("findMany className:" + DateTime.Now.Subtract(now).TotalMilliseconds);
            int index = 0;

            foreach (var item in elements)
            {
                try
                {
                    action(index++, new ElementWrapper(Browser, item));
                }
                catch (Exception ex) {
                    logger.Fatal(ex);
                }
            }
        }
    
      
        /// <summary>
        /// 通过xpath查找页面元素，包括多个元素的情况
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="action"></param>
        public void findManyByXPath(string xpath, Jint.Delegates.Action<int, ElementWrapper> action) {

            ElementCollection elements = Browser.GetElementsByXPath(xpath);
            int index = 0;
            foreach (Element el in elements) {
                try
                {
                    action(index++, new ElementWrapper(Browser, el));
                }
                catch (Exception ex) {
                    logger.Fatal(ex);
                }
            }
           
        }
        /// <summary>
        /// 通过属性"class"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byClass(string tagName, string value)
        {
            Constraint c = FindByAttr("Class", value);
            return find(tagName, c);
        }
        public ElementWrapper byId(string value)
        {
            Constraint c = FindByAttr("Id", value);
            return find("", c);
        }
        /// <summary>
        /// 通过属性"Id"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byId(string tagName, string value)
        {
            Constraint c = FindByAttr("Id", value);
            return find(tagName, c);
        }
        /// <summary>
        /// 通过属性"name"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byName(string tagName, string value)
        {
            Constraint c = FindByAttr("Name", value);
            return find(tagName, c);
        }

        /// <summary>
        /// 缺省查找图片标签
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper bySrc(string value)
        {
            Constraint c = FindByAttr("Src", value);
            return find("img", c);
        }
        /// <summary>
        /// 查找图片标签
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper bySrc(string tagName, string value)
        {
            Constraint c = FindByAttr("Src", value);
            return find(tagName, c);
        }
        /// <summary>
        /// 通过属性"href"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byHref(string value)
        {
            Constraint c = FindByAttr("Href", value);
            return find("a", c);
        }
        /// <summary>
        /// 通过属性"innertext"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byText(string tagName, string value)
        {
            Constraint c = FindByAttr("Text", value);
            return find(tagName, c);
        }
        /// <summary>
        /// 通过属性"value"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byValue(string tagName, string value)
        {
            Constraint c = FindByAttr("Value", value);
            return find(tagName, c);
        }

     
        /// <summary>
        /// 通过属性"xpath"查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byXPath(string xpath)
        {
            return Method<ElementWrapper>.Watch("byXPath," + xpath, () =>
            {
                Constraint c = FindByAttr("XPath", xpath);
                return find(BrowserExtensions.GetLastTag(xpath), c);
            });
        }
        /// <summary>
        /// 通过最近标签查找元素
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ElementWrapper byProximityText(string tagName, string value)
        {
            Constraint c = FindByAttr("ProximityText", value);
            return find(tagName, c);
        }

        public Constraint FindByAttr(string attr, string value)
        {
            return ActionElementBase.GetConstraint(new FindAttribute()
            {
                FindMethod = (FindMethods)Enum.Parse(typeof(FindMethods), attr, true),
                FindValue = value,
                Regex = true,
            });
        }

        private ElementWrapper find(string tagName, Constraint constraint)
        {
            try
            {
                var elements = findMany(tagName, constraint);
                return elements.FirstOrDefault();
            }
            catch (Exception ex) {
                logger.Fatal(ex);                
            }
            return null;
        }

        private IEnumerable<ElementWrapper> findMany(string tagName, Constraint constraint) {         
            ElementCollection elements = null;
            if (!string.IsNullOrEmpty(tagName))
            {
                elements = Browser.GetElementsByTag(tagName, constraint);
            }
            else {
                elements = Browser.Elements.Filter(constraint);
            }
            return elements.Select(c => new ElementWrapper(Browser, c));
        }

     
        /// <summary>
        /// 目前不支持iframe处理
        /// </summary>
        /// <returns></returns>
        private Document GetWindow()
        {
            return Browser;
        }

    }
}