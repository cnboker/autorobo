using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SourceGrid.Cells;
using Button = SourceGrid.Cells.Button;
using CheckBox = SourceGrid.Cells.CheckBox;
using ColumnHeader = SourceGrid.Cells.ColumnHeader;
using ComboBox = SourceGrid.Cells.Editors.ComboBox;
using TextBox = SourceGrid.Cells.Editors.TextBox;
using mshtml;
using System.IO;
using AutoRobo.Core;
using AutoRobo.Core.UserControls;
using AutoRobo.Core.Actions;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Models;
using SourceGrid;

namespace AutoRobo.UserControls
{
    /// <summary>
    /// 子元素编辑器
    /// </summary>
    public partial class ucSelector : ucElementsContainer
    {

        public ucSelector()
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                return base.Action;
            }
            set
            {
                base.Action = value;
                ActionElementBase action = value as ActionElementBase;
                //主元素不增加
                action.RowStatue = ActionStatue.Dirty;
                IHTMLDOMNode el = action.GetIHTMLElement() as IHTMLDOMNode;
                if (el != null)
                {
                    ParseNodes(el);
                }
            }
        }
        public ucSelector(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
            SetHeader();
        }

        private void ParseNodes(IHTMLDOMNode domnode)
        {
            if (domnode == null) return;
            string str = domnode.nodeName;
            if (domnode == null) return;

            if (str == "INPUT" || str == "TEXTAREA" || str == "SELECT")
            {
                AddGridRow((IHTMLElement)domnode);
            }

            var nds = domnode.childNodes as IHTMLDOMChildrenCollection;
            foreach (IHTMLDOMNode childnd in nds)
            {
                string strdom = childnd.nodeName;
                if (strdom == "#text" || strdom == "#comment") continue;
                ParseNodes(childnd);
            }
        }
        public override void AddGridRow(IHTMLElement element)
        {
            int RowIndex = gridElement.RowsCount++;

            //gbFindElement.Height = (gridElement.RowsCount + 1) * gridElement.Rows.GetHeight(RowIndex);

            gridElement.Rows[RowIndex].Tag = element;

            gridElement[RowIndex, 0] = new Cell(element.tagName, new TextBox(typeof(string)));
            var id = element.getAttribute("id", 0) == null ? "" : element.getAttribute("id", 0).ToString();
            gridElement[RowIndex, 1] = new Cell(id, new TextBox(typeof(string)));
            var val = element.getAttribute("value", 0) == null ? "" : element.getAttribute("value", 0).ToString();
            gridElement[RowIndex, 2] = new Cell(val, new TextBox(typeof(string)));

            var btnDelete = new Button("");
            gridElement[RowIndex, 3] = btnDelete;
            var bmpDelete = new Bitmap(ucBaseElement.GetIconFullPath("db-Delete.bmp"));
            bmpDelete.MakeTransparent(Color.Fuchsia);
            gridElement[RowIndex, 3].Image = bmpDelete;
            var buttonClickEvent = new SourceGrid.Cells.Controllers.Button();
            buttonClickEvent.Executed += DeleteFindButton_Click;
            gridElement[RowIndex, 3].Controller.AddController(buttonClickEvent);

        }

        private void AddActions()
        {
            BrowserWindow watinie = Action.Window;
            ModelSet wscript = Action.AppContext.ActionModel;            
            foreach (GridRow o in gridElement.Rows)
            {
                var element = o.Tag as IHTMLElement;
                if (element == null) continue;
                var val = element.getAttribute("value", 0);
                if (element.tagName == "SELECT")
                {
                    //wscript.AddClick(watinie, element);
                    wscript.AddAction(Action.AppContext, ActionEnum.ActionClick.ToString(), new ActionClickParameter()
                    {
                        Element = element,
                    });
                    wscript.AddAction(Action.AppContext, ActionEnum.ActionSelectList.ToString(),
                        new Core.ActionBuilder.SelectParameter() { ByValue = (val != null), Element = element });
                }
                else if (element.tagName == "INPUT")
                {
                    var type = element.getAttribute("type", 0).ToString().ToLower();
                    if (type == "checkbox" || type == "radio")
                    {
                        // wscript.AddClick(watinie, element, true);
                        wscript.AddAction(Action.AppContext, ActionEnum.ActionClick.ToString(), new ActionClickParameter()
                        {

                            Element = element,
                        });
                    }
                    else
                    {
                        //wscript.SbKeys = new StringBuilder(val == null ? "" : val.ToString());
                        //wscript.AddTyping(watinie, element, false, true);
                        wscript.AddAction(Action.AppContext, ActionEnum.ActionTypeText.ToString(),
                            new ActionTypingParameter()
                            {

                                Element = element,
                                XPathFinder = false,
                                SbKeys = (val == null ? "" : val.ToString())
                            });
                    }
                }
            }
        }

        protected override void OnOK()
        {
            AddActions();
            base.OnOK();
        }

    }
}
