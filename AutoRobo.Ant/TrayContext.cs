using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace AutoRobo.ClientLib
{
    public class TrayContext 
    {
        private static readonly string IconFileName = "up.ico";
        private static readonly string DefaultTooltip = "搜索引擎优化器";
        private Form singleManager = null;
        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        private NotifyIcon notifyIcon;				            // the icon that sits in the system tray
        private bool initailze = false;

        /// <summary>
        /// This class should be created and passed into Application.Run( ... )
        /// </summary>
        public TrayContext(Form singleManager)
        {
            this.singleManager = singleManager;
            singleManager.Resize += new EventHandler(singleManager_Resize);
            singleManager.FormClosing += new FormClosingEventHandler(singleManager_FormClosing);
            if (!initailze)
            {
                InitializeContext();
                initailze = true;
            }          
        }

        void singleManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                singleManager.Visible = false;
                notifyIcon.Visible = true;
            }
        }

        void singleManager_Resize(object sender, EventArgs e)
        {
            if (singleManager.WindowState == FormWindowState.Minimized)
            {
                singleManager.Hide();
            }
        }
            
        private void InitializeContext()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(GetIconFileName(IconFileName)),
                Text = DefaultTooltip,
                Visible = true
            };
            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.MouseUp += notifyIcon_MouseUp;
        }

        public static string GetIconFileName(string Filename)
        {
            Filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Filename);            
            return Filename;
        }
       
        /// <summary>
        /// If we are presently showing a form, clean it up.
        /// </summary>
        protected  void Exit()
        {
          
            notifyIcon.Visible = false; // should remove lingering tray icon
            Application.Exit();

            //处理程序不能正常退出问题
            Process.GetCurrentProcess().Kill();
            
        }

        // From http://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) { 
            
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            notifyIcon.ContextMenuStrip.Items.Clear();            
            notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("显示", showItem_Click));
            //notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("配置", showHelpItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());            
            notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("退出", exitItem_Click));
        }

        // attach to context menu items
        private void showHelpItem_Click(object sender, EventArgs e) {
           
        }
        
        private void showItem_Click(object sender, EventArgs e) {
            singleManager.Show();
            singleManager.WindowState = FormWindowState.Maximized;
            singleManager.Activate();
        }


        private ToolStripMenuItem ToolStripMenuItemWithHandler(
            string displayText, int enabledCount, int disabledCount, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null) { item.Click += eventHandler; }

            //item.Image = (enabledCount > 0 && disabledCount > 0) ? Properties.Resources.signal_yellow
            //             : (enabledCount > 0) ? Properties.Resources.signal_green
            //             : (disabledCount > 0) ? Properties.Resources.signal_red
            //             : null;
            item.ToolTipText = (enabledCount > 0 && disabledCount > 0) ?
                                                 string.Format("{0} enabled, {1} disabled", enabledCount, disabledCount)
                         : (enabledCount > 0) ? string.Format("{0} enabled", enabledCount)
                         : (disabledCount > 0) ? string.Format("{0} disabled", disabledCount)
                         : "";
            return item;
        }

        public ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
        {
            return ToolStripMenuItemWithHandler(displayText, 0, 0, eventHandler);
        }
        /// <summary>
        /// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        #region refresh icontray area
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        public static void RefreshTaskbarNotificationArea()
        {
            IntPtr systemTrayContainerHandle = FindWindow("Shell_TrayWnd", null);
            IntPtr systemTrayHandle = FindWindowEx(systemTrayContainerHandle, IntPtr.Zero, "TrayNotifyWnd", null);
            IntPtr sysPagerHandle = FindWindowEx(systemTrayHandle, IntPtr.Zero, "SysPager", null);
            IntPtr notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "用户升级的通知区域");
            if (notificationAreaHandle == IntPtr.Zero)
            {
                notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "用户升级的通知区域");
                IntPtr notifyIconOverflowWindowHandle = FindWindow("NotifyIconOverflowWindow", null);
                IntPtr overflowNotificationAreaHandle = FindWindowEx(notifyIconOverflowWindowHandle, IntPtr.Zero, "ToolbarWindow32", "溢出通知区域");
                RefreshTaskbarNotificationArea(overflowNotificationAreaHandle);
            }
            RefreshTaskbarNotificationArea(notificationAreaHandle);
        }
        private static void RefreshTaskbarNotificationArea(IntPtr windowHandle)
        {
            const uint wmMousemove = 0x0200;
            RECT rect;
            GetClientRect(windowHandle, out rect);
            for (var x = 0; x < rect.right; x += 5)
                for (var y = 0; y < rect.bottom; y += 5)
                    SendMessage(windowHandle, wmMousemove, 0, (y << 16) + x);
        }
        #endregion

    }
}
