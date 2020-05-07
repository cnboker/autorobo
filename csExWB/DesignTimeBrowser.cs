using System;
using System.Collections.Generic;
using System.Text;
using IfacesEnumsStructsClasses;
using mshtml;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace csExWB
{
    public class DesignTimeBrowser : cEXWB, IDocHostUIHandler
    {
        public event csExWB.ContextMenuEventHandler WBContextMenu = null;
        private ContextMenuEventArgs ContextMenuEvent = new ContextMenuEventArgs();
        private DOCHOSTUIDBLCLK m_DocHostUiDbClkFlag = DOCHOSTUIDBLCLK.DEFAULT;
        public event csExWB.GetOptionKeyPathEventHandler WBGetOptionKeyPath = null;
        private WBKeyDownEventArgs WBKeyDownEvent = new WBKeyDownEventArgs();
        private WBKeyUpEventArgs WBKeyUpEvent = new WBKeyUpEventArgs();
        public event csExWB.WBKeyDownEventHandler WBKeyDown = null;
        public event csExWB.WBKeyUpEventHandler WBKeyUp = null;
        private GetOptionKeyPathEventArgs GetOptionKeyPathEvent = new GetOptionKeyPathEventArgs();
     

        [Category("DOCHOSTUIDBLCLK")]
        public DOCHOSTUIDBLCLK WBDOCHOSTUIDBLCLK
        {
            get { return m_DocHostUiDbClkFlag; }
            set { m_DocHostUiDbClkFlag = value; }
        }
        [Category("DOCHOSTUIFLAG")]
        public int WBDOCHOSTUIFLAG
        {
            get { return (int)m_DocHostUiFlags; }
            set { m_DocHostUiFlags = (DOCHOSTUIFLAG)value; }
        }

    

        #region IDocHostUIHandler Members

        int IDocHostUIHandler.ShowContextMenu(uint dwID, ref tagPOINT pt, object pcmdtReserved, object pdispReserved)
        {
            // return Hresults.S_OK; //Do not display context menu
            // return Hresults.S_FALSE; //Default IE ctxmnu

            //Raise event
            if (WBContextMenu != null)
            {
                //Screen coordinates
                Point outpt = new Point(pt.x, pt.y);
                //Client coordinates
                Point clientpt = this.PointToClient(outpt);
                IHTMLElement elem = this.ElementFromPoint(clientpt.X, clientpt.Y);

                ContextMenuEvent.SetParameters((WB_CONTEXTMENU_TYPES)dwID, outpt, clientpt, elem, elem);
                WBContextMenu(this, ContextMenuEvent);
                if (ContextMenuEvent.displaydefault == false) //Handled or don't display
                    return Hresults.S_OK;
                ContextMenuEvent.dispctxmenuobj = null;
                ContextMenuEvent.ctxmenuelem = null;
            }
            return Hresults.S_FALSE;
        }

        int IDocHostUIHandler.GetHostInfo(ref DOCHOSTUIINFO info)
        {
            //Default, selecttext+no3dborder+flatscrollbars+themes(xp look)
            //Size has be set
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.dwDoubleClick = (uint)m_DocHostUiDbClkFlag;
            info.dwFlags = (uint)m_DocHostUiFlags;
            //info.pchHostCss = "BODY {background-color:#ffcccc}";
            return Hresults.S_OK;
        }

        int IDocHostUIHandler.ShowUI(int dwID, IOleInPlaceActiveObject activeObject, IOleCommandTarget commandTarget, IOleInPlaceFrame frame, IOleInPlaceUIWindow doc)
        {
            //activeObject.GetWindow should return Internet Explorer_Server hwnd
            if (activeObject != null)
                activeObject.GetWindow(ref m_hWBServerHandle);
            return Hresults.S_OK;
        }

        int IDocHostUIHandler.HideUI()
        {
            return Hresults.S_OK;
        }

        int IDocHostUIHandler.UpdateUI()
        {
            return Hresults.S_OK;
        }

        int IDocHostUIHandler.EnableModeless(bool fEnable)
        {
            return Hresults.E_NOTIMPL;
        }

        int IDocHostUIHandler.OnDocWindowActivate(bool fActivate)
        {
            return Hresults.E_NOTIMPL;
        }

        int IDocHostUIHandler.OnFrameWindowActivate(bool fActivate)
        {
            return Hresults.E_NOTIMPL;
        }

        int IDocHostUIHandler.ResizeBorder(ref tagRECT rect, IOleInPlaceUIWindow doc, bool fFrameWindow)
        {
            return Hresults.E_NOTIMPL;
        }

        int IDocHostUIHandler.TranslateAccelerator(ref tagMSG msg, ref Guid group, uint nCmdID)
        {
            //    return Hresults.S_OK; //Cancel
            //    return Hresults.S_FALSE; //IE default action
            Keys nVirtExtKey = Keys.None; // (int)0;
            if ((ModifierKeys & Keys.Control) != 0)
                nVirtExtKey = Keys.ControlKey; //CONTROL 17
            if ((ModifierKeys & Keys.ShiftKey) != 0)
                nVirtExtKey = Keys.ShiftKey; //SHIFT 16
            if ((ModifierKeys & Keys.Menu) != 0)
                nVirtExtKey = Keys.Menu; //ALT 18
            Keys keyCode = (Keys)msg.wParam;

            if ((msg.message == WindowsMessages.WM_KEYDOWN) && (WBKeyDown != null))
            {
                WBKeyDownEvent.SetParameters(keyCode, nVirtExtKey);
                WBKeyDown(this, WBKeyDownEvent);
                if (WBKeyDownEvent.handled)
                    return Hresults.S_OK;
            }
            if ((msg.message == WindowsMessages.WM_KEYUP) && (WBKeyUp != null))
            {
                WBKeyUpEvent.SetParameters(keyCode, nVirtExtKey);
                WBKeyUp(this, WBKeyUpEvent);
                if (WBKeyUpEvent.handled == true)
                    return Hresults.S_OK;
            }
            //IE default action
            return Hresults.S_FALSE;
        }

        int IDocHostUIHandler.GetOptionKeyPath(out string pbstrKey, uint dw)
        {
            //pbstrKey[0] = null;
            pbstrKey = null;
            if (WBGetOptionKeyPath != null)
            {
                GetOptionKeyPathEvent.SetParameters();
                WBGetOptionKeyPath(this, GetOptionKeyPathEvent);
                if (GetOptionKeyPathEvent.handled == true)
                {
                    pbstrKey = GetOptionKeyPathEvent.keypath;
                    GetOptionKeyPathEvent.SetParameters();
                    return Hresults.S_OK;
                }
            }
            return Hresults.E_NOTIMPL;
        }

        int IDocHostUIHandler.GetDropTarget(IfacesEnumsStructsClasses.IDropTarget pDropTarget, out IfacesEnumsStructsClasses.IDropTarget ppDropTarget)
        {
            int hret = Hresults.E_NOTIMPL;
            ppDropTarget = null;
            if (m_bUseInternalDragDrop)
            {
                ppDropTarget = this as IfacesEnumsStructsClasses.IDropTarget;
                if (ppDropTarget != null)
                    hret = Hresults.S_OK;
            }
            return hret;
        }

        int IDocHostUIHandler.GetExternal(out object ppDispatch)
        {
            //Set to new Object() so as to avoid scripts checking for window.external
            //causing the javascript error "not implemented" being generated
            if (m_WinExternal == null)
                m_WinExternal = new object();
            //if (m_WinExternal != null)
            //{
            ppDispatch = m_WinExternal;
            return Hresults.S_OK;
            //}
            //else
            //{
            //    ppDispatch = null;
            //    return Hresults.E_NOTIMPL;
            //}
        }

        int IDocHostUIHandler.TranslateUrl(uint dwTranslate, string strURLIn, out string pstrURLOut)
        {
            pstrURLOut = null;
            return Hresults.E_NOTIMPL;
        }

        int IDocHostUIHandler.FilterDataObject(System.Runtime.InteropServices.ComTypes.IDataObject pDO, out System.Runtime.InteropServices.ComTypes.IDataObject ppDORet)
        {
            ppDORet = null;
            return Hresults.E_NOTIMPL;
        }

        #endregion
    }
}
