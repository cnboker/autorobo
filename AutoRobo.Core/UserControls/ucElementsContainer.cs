using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Button = SourceGrid.Cells.Button;
using CheckBox = SourceGrid.Cells.CheckBox;
using ColumnHeader = SourceGrid.Cells.ColumnHeader;
using ComboBox = SourceGrid.Cells.Editors.ComboBox;
using TextBox = SourceGrid.Cells.Editors.TextBox;
using mshtml;
using SourceGrid.Cells;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.UserControls
{
    public partial class ucElementsContainer : ucBaseEditor
    {
        const int FIND_METHOD = 0;
        const int FIND_ID = 1;
        const int FIND_VALUE = 2;
        const int FIND_VALUE_REGEX = 3;
        const int ADD_REMOVE = 4;

        public ucElementsContainer()
        {
            InitializeComponent();
        }
        public ucElementsContainer(IEditorAction EditorAction):base(EditorAction)
        {
            InitializeComponent();
        }
     

        protected void SetHeader()
        {
            gridElement.RowsCount = 0;
            gridElement.ColumnsCount = 4;
            gridElement.FixedRows = 1;
            gridElement.Rows.Insert(0);
            const int RowIndex = 0;

            gridElement[RowIndex, 0] = new ColumnHeader("方法");
            ((ColumnHeader)gridElement[RowIndex, 0]).AutomaticSortEnabled = false;
            gridElement[RowIndex, 0].Column.Width = 70;

            gridElement[RowIndex, 1] = new ColumnHeader("Id");
            ((ColumnHeader)gridElement[RowIndex, 1]).AutomaticSortEnabled = false;
            gridElement[RowIndex, 1].Column.Width = 120;

            gridElement[RowIndex, 2] = new ColumnHeader("值");
            ((ColumnHeader)gridElement[RowIndex, 2]).AutomaticSortEnabled = false;
            gridElement[RowIndex, 2].Column.Width = 80;

            gridElement[RowIndex, 3] = new ColumnHeader("");
            ((ColumnHeader)gridElement[RowIndex, 3]).AutomaticSortEnabled = false;
            gridElement[RowIndex, 3].Column.Width = 25;

            gridElement.VerticalScroll.Visible = true;
        }

     

        internal FindAttribute GetRowValue(int RowIndex)
        {
            var attribute = new FindAttribute
            {
                FindName = gridElement[RowIndex, FIND_ID].Value.ToString()
            };
            attribute.FindMethod = FindMethods.XPath;
            attribute.FindValue = gridElement[RowIndex, FIND_VALUE].ToString();
            attribute.Regex =  false;
            return attribute;
        }

        private void AddGridRow(FindAttribute attribute)
        {
            int RowIndex = gridElement.RowsCount++;

            gridElement[RowIndex, 0] = new Cell(FindMethods.XPath.ToString(), new TextBox(typeof(string)));
            var id = attribute.FindName == null ? "" : attribute.FindName;
            gridElement[RowIndex, 1] = new Cell(id, new TextBox(typeof(string)));
            var val = attribute.FindValue;
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

        /// <summary>
        /// 校验ID必须唯一
        /// </summary>
        public void IdValidate() {            
            List<string> ids = new List<string>();

            for (int RowIndex = 1; RowIndex < gridElement.Rows.Count; RowIndex++)
            {
                var val = gridElement[RowIndex, FIND_ID].Value;
                if (val == null) {
                    throw new ApplicationException(string.Format("第{0}行，元素ID不能为空", RowIndex));
                }
                if (string.IsNullOrEmpty(val.ToString().Trim()))
                {
                    throw new ApplicationException(string.Format("第{0}行，元素ID不能为空", RowIndex));
                }
                if (ids.Contains(val.ToString()))
                {
                    throw new ApplicationException(string.Format("第{0}行，元素ID值重复", RowIndex));
                }
                ids.Add(val.ToString());        
            }
        }

        public virtual void AddGridRow(IHTMLElement element)
        {
            int RowIndex = gridElement.RowsCount++;        

            gridElement[RowIndex, 0] = new Cell(FindMethods.XPath.ToString(), new TextBox(typeof(string)));
            var id = element.getAttribute("id", 0) == null ? "" : element.getAttribute("id", 0).ToString();
            gridElement[RowIndex, 1] = new Cell(id, new TextBox(typeof(string)));
            var val = XPathFinder.GetXPath(element);
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

        protected void DeleteFindButton_Click(object sender, EventArgs e)
        {
            int Index = ((SourceGrid.CellContext)sender).Position.Row;
            gridElement.Rows.Remove(Index);
            //if (gridElement.RowsCount >= 1)
            //{
            //    gbFindElement.Height = (gridElement.RowsCount + 1) * gridElement.Rows.GetHeight(0);
            //}
        }

    }
}
