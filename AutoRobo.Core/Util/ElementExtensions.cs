using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using WatiN.Core.Constraints;
using System.Text.RegularExpressions;
using WatiN.Core.UtilityClasses;
using AutoRobo.Core.Actions;
using WatiN.Core.Native.InternetExplorer;
using mshtml;

namespace AutoRobo.Core
{
    public static class ElementExtensions
    {
        /// <summary>
        /// Checks whether the element exists, if so performs the specified action,
        /// otherwise just returns the current element object.
        ///   
        /// usage:ie.TextField(Find.ById("MyTextField")).IfExists<TextField>(e => e.TypeText("AValue"));
        /// </summary>
        /// <typeparam name="T">The type of element to check for existence.</typeparam>
        /// <param name="element">The element.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static T IfExists<T>(this Element element, Action<T> action) where T : class
        {
            if (element.Exists)
            {
                action(element as T);
            }

            return element as T;
        }
        /// <summary>
        /// 检查element是否是parent的子元素
        /// </summary>
        /// <param name="element"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool IsChildOf(this IHTMLElement element, IHTMLElement parent) {
            IHTMLElement p = element.parentElement;
            if (p == null) return false;
            if (p != parent) {
                return IsChildOf(p, parent);
            }
            return true;
        }
        /// <summary>
        /// Gets an attribute constraint using a partial id.
        /// </summary>
        /// <param name="partialElementId">The partial element id.</param>
        /// <returns></returns>
        public static AttributeConstraint ByPartialId(string partialElementId)
        {
            return Find.ById(new Regex(".*" + partialElementId + "$"));
        }

        public static IHTMLElement GetHtmlElement(this Element element) {
            var nativeElement = element.NativeElement as IEElement;
            return nativeElement.AsHtmlElement as IHTMLElement;
        }
        /// <summary>
        /// Waits until the specified element is enabled within the web page.
        /// </summary>
        /// <param name="element">The element.</param>
        public static T WaitUntilEnabled<T>(this Element element) where T : class
        {
            var timeout = TimeSpan.FromSeconds(WatiN.Core.Settings.WaitForCompleteTimeOut);
            var timer = new SimpleTimer(timeout);

            do
            {
                var value = element.GetAttributeValue("disabled");

                if (value != "True" &&
                    value != "true")
                {
                    return element as T;
                }
                System.Threading.Thread.Sleep(200);
            }
            while (!timer.Elapsed);

            throw new TimeoutException("Waited too long for " + element.Id + " to become enabled");
        }

            /// <summary>
        /// Safely enumerates over the TableRow elements contained inside an elements container.
        /// </summary>
        /// <param name="container">The IElementsContainer to enumerate</param>
        /// <remarks>
        /// This is neccesary because calling an ElementsContainer TableRows property can be an
        /// expensive operation.  I'm assuming because it's going out and creating all of the
        /// table rows right when the property is accessed.  Using the itterator pattern below
        /// to prevent creating the whole table row hierarchy up front.
        /// usage:foreach ( TableRow tr in ie.Table( "myTable" ).TableRowEnumerator() )
        //{
        //    //Do Someting with tr
        //}
        /// </remarks>
        public static IEnumerable<TableRow> TableRowEnumerator(this IElementContainer container)
        {
            //Searches for the first table row child with out calling the TableRows property
            // This appears to be a lot faster plus it eliminates any tables that may appear
            // nested inside of the parent table

            var tr = container.TableRow(Find.ByIndex(0));
            while (true)
            {
                if (tr.Exists)
                {
                    yield return tr;
                }
                //Moves to the next row with out searching any nested tables.
                tr = tr.NextSibling as TableRow;
                if (tr == null || !tr.Exists)
                {
                    break;
                }
            }
        }


        /// <summary>
        /// 获取子元素
        ///usage: ElementCollection results = ie.Element(Find.ById("search-results")).Children();
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static ElementCollection Children(this Element self)
        {           
            return ((IElementContainer)self).Children();
        }

        public static void Show(this Element element) {
            IEElement el = element.NativeElement as IEElement;
            el.AsHtmlElement.style.display = "block";
        }

        public static void Hide(this Element element)
        {
            IEElement el = element.NativeElement as IEElement;
            el.AsHtmlElement.style.display = "none";
        }

     
    }
}
