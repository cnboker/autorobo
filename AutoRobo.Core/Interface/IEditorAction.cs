using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using System.Windows.Forms;

namespace AutoRobo.Core
{
    public interface IEditorAction
    {  
        /// <summary>
        /// 显示Action编辑器
        /// </summary>
        /// <param name="action"></param>
        void ShowEditAction(ucBaseEditor UCEditor);
        /// <summary>
        /// 关闭Action编辑器
        /// </summary>
        /// <param name="result"></param>
        void CloseEditAction(ucBaseEditor UCEditor, DialogResult result);
    }
}
