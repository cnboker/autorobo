using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceGrid.Cells;
using AutoRobo.Core.Actions;

namespace AutoRobo.Makers.Views
{
    public class VariableActionGrid : ActionGrid
    {
        const int rowIndex = 0;
        
        protected override void SetHeader()
        {
            //多行选择         
            this[rowIndex, 0] = new ColumnHeader("");
            ((ColumnHeader)this[rowIndex, 0]).AutomaticSortEnabled = false;
            this[rowIndex, 0].Column.Width = 35;

            this[rowIndex, 1] = new ColumnHeader("类型");
            ((ColumnHeader)this[rowIndex, 1]).AutomaticSortEnabled = false;
            this[rowIndex, 1].Column.Width = 45;

            this[rowIndex, 2] = new ColumnHeader("变量名称");
            this[rowIndex, 2].Column.Width = 100;
            ((ColumnHeader)this[rowIndex, 2]).AutomaticSortEnabled = false;

            this[rowIndex, 3] = new ColumnHeader("变量值");
            ((ColumnHeader)this[rowIndex, 3]).AutomaticSortEnabled = false;
            this[rowIndex, 3].Column.Width = 60;

            this[rowIndex, 4] = new ColumnHeader("说明");
            this[rowIndex, 4].Column.Width = 80;
            ((ColumnHeader)this[rowIndex, 4]).AutomaticSortEnabled = false;
        }

        /// <summary>
        /// 更新变量活动行内容
        /// </summary>
        protected override void UpdateRow(int rowIndex, ActionBase actionObject)
        {            
            //变量名称
            string elementValue = actionObject.ElementName;
            this[rowIndex, 2] = new SourceGrid.Cells.Cell(elementValue);
            //变量值
            this[rowIndex, 3] = new SourceGrid.Cells.Cell(actionObject.ToString());
        }
    }


}
