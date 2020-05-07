using System;
using System.Drawing;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using SourceGrid;
using SourceGrid.Cells;
using AutoRobo.Core;

namespace AutoRobo.Makers.Views
{
    public partial class ActionGrid : SourceGrid.Grid
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ActionGrid");
        const int rowIndex = 0;
       
        public DataMethod Method { get; set; }

        public ActionGrid() {
            SetGridHeader();            
        }

        void SetGridHeader()
        {
            //多行选择
            this.Selection.EnableMultiSelection = true;
            RowsCount = 0;
            ColumnsCount = 5;
            FixedRows = 1;
            Rows.Insert(0);
            SetHeader();
        }

        protected virtual void SetHeader() {
            this[rowIndex, 0] = new ColumnHeader("");
            ((ColumnHeader)this[rowIndex, 0]).AutomaticSortEnabled = false;
            this[rowIndex, 0].Column.Width = 35;

            this[rowIndex, 1] = new ColumnHeader("类型");
            ((ColumnHeader)this[rowIndex, 1]).AutomaticSortEnabled = false;
            this[rowIndex, 1].Column.Width = 45;

            this[rowIndex, 2] = new ColumnHeader("缺省值");
            this[rowIndex, 2].Column.Width = 100;
            ((ColumnHeader)this[rowIndex, 2]).AutomaticSortEnabled = false;
            
            this[rowIndex, 3] = new ColumnHeader("数据绑定");
            ((ColumnHeader)this[rowIndex, 3]).AutomaticSortEnabled = false;
            this[rowIndex, 3].Column.Width = 60;

            this[rowIndex, 4] = new ColumnHeader("说明");
            this[rowIndex, 4].Column.Width = 80;
            ((ColumnHeader)this[rowIndex, 4]).AutomaticSortEnabled = false;

            if (Method == DataMethod.Collect)
            {
                this[rowIndex, 2].Column.Visible = false;
                this[rowIndex, 3].Column.Visible = false;
            }
            else {
                this[rowIndex, 2].Column.Visible = true;
                this[rowIndex, 3].Column.Visible = true;
            }
        }

        //protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        //{
        //    //WatinContext context = WatinContextFactory.GetContext(browser);
        //    SelectRowIndex = Selection.ActivePosition.Row;
        //    //ActionBase action = GetSelectAction();
        //    //if (action == null) return;
        //    //try
        //    //{
        //    //    ActionElementBase ae = action as ActionElementBase;
        //    //    if (ae == null) return;
        //    //    if (ae.IsExist())
        //    //    {
        //    //        WatiN.Core.Element element = ae.GetElement();
        //    //        if (element == null) return;
        //    //        var nativeElement = element.NativeElement as IEElement;
        //    //        IHTMLElement el = (IHTMLElement)nativeElement.AsHtmlElement;
        //    //        //nativeElement.AsHtmlElement.scrollIntoView();                
        //    //        browser.HighlightElement(el);
        //    //        int x = 0, y = 0, width = 0, height = 0;

        //    //        ElementHelper.GetAbsolutePosition((IHTMLDOMNode)el, out x, out y, out width, out height);

        //    //        mshtml.IHTMLDocument2 htmldoc = (mshtml.IHTMLDocument2)el.document;
        //    //        htmldoc.parentWindow.scrollTo(0, y);
        //    //        ((IProvider)browser).Host.Loader.FinderProvider(ProviderEnum.HtmlTree).Update(el);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    //System.Windows.Forms.MessageBox.Show(ex.Message);
        //    //    context.AttachToIE();
        //    //}
        //    base.OnMouseClick(e);
        //}


        internal void AddGridRowItem(ActionBase actionObject)
        {
            InsertGridRowItem(RowsCount, actionObject);
        }

   
        internal void InsertGridRowItem(int rowIndex, ActionBase actionObject)
        {
            if (RowsCount <= rowIndex)
            {
                RowsCount++;
            }
            else
            {
                Rows.Insert(rowIndex);
            }
            UpdateGridRowView(rowIndex, actionObject); 
        }

        public void UpdateGridRowView(int rowIndex, ActionBase actionObject)
        {
            UpdateCellIcon(rowIndex, actionObject);
            //类型
            string actionType = actionObject.ActionDisplayName;
            this[rowIndex, 1] = new SourceGrid.Cells.Cell(actionType);
            AutoWidthColumn(actionType, rowIndex, 1);
            UpdateRow(rowIndex, actionObject);
            //说明            
            string desc = actionObject.Title;
            if (string.IsNullOrEmpty(desc))
            {
                desc = actionObject.GetDescription();
            }
            this[rowIndex, 4] = new SourceGrid.Cells.Cell(desc);
            AutoWidthColumn(desc, rowIndex, 4);

            Rows.SetHeight(rowIndex, 30);
            InvalidateRange(Rows[rowIndex].Range);            
        }

        /// <summary>
        /// 更新缺省活动行内容
        /// </summary>
        protected virtual void UpdateRow(int rowIndex, ActionBase actionObject)
        {           
            //缺省值
            string elementValue = actionObject.DefaultValue;
            this[rowIndex, 2] = new SourceGrid.Cells.Cell(elementValue);
            //AutoWidthColumn(elementValue, rowIndex, 2);
            //数据绑定
            ActionBase action = actionObject as ActionBase;
            if (!string.IsNullOrEmpty(action.MapName))
            {
                this[rowIndex, 3] = new SourceGrid.Cells.Cell(action.MapName);
                AutoWidthColumn(action.MapName, rowIndex, 3);
            }
            else
            {
                this[rowIndex, 3] = new SourceGrid.Cells.Cell("");
            }         

        }
     
          
        //列宽自适应
        private void AutoWidthColumn(string text, int rowIndex, int colIndex)
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                SizeF sz = g.MeasureString(text, Font);
                Int32 myMaxWidth = Convert.ToInt32(sz.Width) + 6;
                if (myMaxWidth > this[0, colIndex].Column.Width)
                {
                    this[rowIndex, colIndex].Column.Width = myMaxWidth;
                }
            }
        }

        public void UpdateCellIcon(int rowIndex, ActionBase actionObject)
        {
            this[rowIndex, 0] = new SourceGrid.Cells.Cell("");
            Bitmap breakBitmap = GetBreakPointIcon(actionObject);
            if (breakBitmap != null)
            {
                this[rowIndex, 0].Image = breakBitmap;
            }
            else
            {
                Bitmap statueIcon = GetStatueIcon(actionObject);
                AppContext context = actionObject.AppContext as AppContext;
                if (statueIcon != null && context.State.IsRunning)
                {
                    this[rowIndex, 0].Image = statueIcon;
                    this[rowIndex, 0].ToolTipText = actionObject.ErrorMessage;
                }
                else
                {
                    this[rowIndex, 0].Image = actionObject.GetIcon();
                }
            }
            InvalidateCell(new Position(rowIndex, 0));
        }

        private Bitmap GetStatueIcon(ActionBase actionObject)
        {
            string filename = "";
            StatusIndicators statusIndicators = actionObject.Status;
            if (statusIndicators == StatusIndicators.StepFailure)
            {
                filename = "error.png";
            }
            return GetImage(filename);
        }


        private Bitmap GetBreakPointIcon(ActionBase actionObject)
        {
            BreakpointIndicators breakpointType = actionObject.Breakpoint;
            string filename = "";
            if (breakpointType == BreakpointIndicators.ActiveBreakpoint)
            {
                filename = "breakpoint.bmp";
            }
            else if (breakpointType == BreakpointIndicators.InactiveBreakpoint)
            {
                filename = "inactivebreak.bmp";
            }
            return GetImage(filename);
        }

        Bitmap GetImage(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return null;          
            Bitmap bmp = new Bitmap(
           System.Reflection.Assembly.GetEntryAssembly().
             GetManifestResourceStream(string.Format("AutoRobo.Makers.Icons.{0}", filename)));
            bmp.MakeTransparent(Color.Fuchsia);
            return bmp;
        }

        public  void ClearGrid()
        {
            if(InvokeRequired){
                Invoke(new Action(ClearGrid));
                return;
            }
            Rows.Clear();
            SetGridHeader();
        }

        public  void DeleteGridRow(int index)
        {
            int row = index + 1;
            if (Rows.Count > row)
            {
                Rows.Remove(row);
            }
        }

    }
    
}
