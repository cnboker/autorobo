﻿using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native.Windows;
using WatiN.Core.Native.InternetExplorer;
using System.Linq;

namespace AutoRobo
{
    /// <summary>
    /// 处理alert消息框
    /// ie.AddDialogHandler(new OKDialogHandler());
    /// </summary>
    public class OKDialogHandler : BaseDialogHandler
    {
        public override bool HandleDialog(Window window)
        {
            var button = GetOKButton(window);
            if (button != null)
            {
                button.Click();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanHandleDialog(Window window)
        {
            return GetOKButton(window) != null;
        }

        private WinButton GetOKButton(Window window)
        {
            var windowButton = new WindowsEnumerator().GetChildWindows(window.Hwnd, w => w.ClassName == "Button" && new WinButton(w.Hwnd).Title == "OK").FirstOrDefault();
            if (windowButton == null)
                return null;
            else
                return new WinButton(windowButton.Hwnd);
        }
    }
}
