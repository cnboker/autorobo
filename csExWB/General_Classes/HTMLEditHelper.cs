using System;
using System.Windows.Forms;
using System.Diagnostics;
using mshtml;

namespace IfacesEnumsStructsClasses
{
    /// <summary>
    /// Helper class to perform HTML editing functions
    /// For example of usage,  please refer to frmHTMLEditor.cs
    /// </summary>
    public class HTMLEditHelper
    {
        private object m_pDOM = null;
        private IHTMLDocument2 m_pDoc2 = null;
        private IHTMLDocument3 m_pDoc3 = null;
        private IHTMLDocument4 m_pDoc4 = null;
        private IDisplayServices m_pDisplayServices = null;
        private IMarkupServices m_pMarkupServices = null;
        private IMarkupContainer m_pMarkupContainer = null;
        private IHighlightRenderingServices m_pHighlightRenderingServices = null;
        private IHTMLRenderStyle m_pHTMLRenderStyle = null;

        public enum DocumentColors
        {
            Backcolor = 0,
            Forecolor = 1,
            Linkcolor = 2,
            ALinkcolor = 3,
            VLinkcolor = 4
        }

        public enum ContentType
        {
            character,
            word,
            sentence
        }

        public string HtmlSpace = "&nbsp;";  // space
        public string HtmlTagOpen = "&lt;";  // <
        public string HtmlTagClose = "&gt;"; // >
        public string HtmlAmp = "&amp;";     // &

        public HTMLEditHelper()
        {

        }

        #region Properties
        
        /// <summary>
        /// IWebbrowser2::Document
        /// </summary>
        public object DOM
        {
            get { return m_pDOM; }
            set
            {
                m_pDOM = value;
                if (m_pDOM != null)
                {
                    //Cache interfaces
                    m_pDoc2 = m_pDOM as IHTMLDocument2;
                    m_pDoc3 = m_pDOM as IHTMLDocument3;
                    m_pDoc4 = m_pDOM as IHTMLDocument4;
                    if (m_pDoc4 != null)
                    {
                        m_pDisplayServices = m_pDoc4 as IDisplayServices;
                        m_pMarkupServices = m_pDoc4 as IMarkupServices;
                        m_pMarkupContainer = m_pDoc4 as IMarkupContainer;
                        //Get the Highlight Rendering Services
                        m_pHighlightRenderingServices = m_pDoc4 as IHighlightRenderingServices;
                        //Create a default highlight rendering style
                        m_pHTMLRenderStyle = m_pDoc4.createRenderStyle("");
                        if (m_pHTMLRenderStyle != null)
                        {
                            //Setup defaults
                            // Must set this, despite it supposedly being the default setting!
                            m_pHTMLRenderStyle.defaultTextSelection = "false";
                            m_pHTMLRenderStyle.textBackgroundColor = "White";
                            m_pHTMLRenderStyle.textColor = "Black";
                            m_pHTMLRenderStyle.textDecoration = "underline";
                            m_pHTMLRenderStyle.textDecorationColor = "red";
                            m_pHTMLRenderStyle.textUnderlineStyle = "wave";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// IHTMLRenderStyle.textUnderlineStyle
        /// Used for highlighting
        /// One of:
        /// undefined = Text decoration has no specified style. The value is set automatically; for example, by default or inheritance. 
        /// single = Text has one line drawn below it. 
        /// dotted = Text has a dotted line drawn below it. 
        /// wave = Default. Text has a wavy line drawn below it. 
        /// thick-dash = Text has a dashed line drawn below it that has thicker width. 
        /// </summary>
        public string HTMLRenderStyle_textUnderlineStyle
        {
            get { return m_pHTMLRenderStyle.textUnderlineStyle; }
            set { m_pHTMLRenderStyle.textUnderlineStyle = value; }
        }

        /// <summary>
        /// IHTMLRenderStyle.textDecoration
        /// Used for highlighting
        /// One of:
        /// auto = Style of the decoration is determined by the browser; for example, by default or inheritance. 
        /// none = Text has no decoration. 
        /// underline = Default. Text has a line drawn below it. 
        /// overline = Text has a line drawn over it. 
        /// line-through = Text has a line drawn through it. 
        /// </summary>
        public string HTMLRenderStyle_textDecoration
        {
            get { return m_pHTMLRenderStyle.textDecoration; }
            set { m_pHTMLRenderStyle.textDecoration = value; }
        }

        /// <summary>
        /// IHTMLRenderStyle.textDecorationColor
        /// Used for highlighting
        /// One of HTML color values.
        /// White, Black, Red, ...
        /// </summary>
        public string HTMLRenderStyle_textDecorationColor
        {
            get { return (string)m_pHTMLRenderStyle.textDecorationColor; }
            set { m_pHTMLRenderStyle.textDecorationColor = value; }
        }

        /// <summary>
        /// IHTMLRenderStyle.textColor
        /// Used for highlighting
        /// One of HTML color values.
        /// White, Black, Red, ...
        /// </summary>
        public string HTMLRenderStyle_textColor
        {
            get { return (string)m_pHTMLRenderStyle.textColor; }
            set { m_pHTMLRenderStyle.textColor = value; }
        }

        /// <summary>
        /// IHTMLRenderStyle.defaultTextSelection
        /// Used for highlighting
        /// true or false.
        /// Default = false
        /// </summary>
        public string HTMLRenderStyle_defaultTextSelection
        {
            get { return m_pHTMLRenderStyle.defaultTextSelection; }
            set { m_pHTMLRenderStyle.defaultTextSelection = value; }
        }

        /// <summary>
        /// HTMLRenderStyle.textBackgroundColor
        /// Used for highlighting
        /// One of HTML color values.
        /// White, Black, Red, ...
        /// </summary>
        public string HTMLRenderStyle_textBackgroundColor
        {
            get { return (string)m_pHTMLRenderStyle.textBackgroundColor; }
            set { m_pHTMLRenderStyle.textBackgroundColor = value; }
        }

        public IMarkupContainer MarkupContainer
        {
            get { return m_pMarkupContainer; }
            set { m_pMarkupContainer = value; }
        }

        public IMarkupServices MarkupServices
        {
            get { return m_pMarkupServices; }
            set { m_pMarkupServices = value; }
        }

        public IDisplayServices DisplayServices
        {
            get { return m_pDisplayServices; }
            set { m_pDisplayServices = value; }
        }

        public IHTMLDocument4 Document4
        {
            get { return m_pDoc4; }
            set { m_pDoc4 = value; }
        }

        public IHTMLDocument3 Document3
        {
            get { return m_pDoc3; }
            set { m_pDoc3 = value; }
        }

        public IHTMLDocument2 Document2
        {
            get { return m_pDoc2; }
            set { m_pDoc2 = value; }
        } 
        
        #endregion

        #region General

        /// <summary>
        /// Attempts to set the cursor at the start of the given html element
        /// The element must be a textual element such as P
        /// </summary>
        /// <param name="elem"></param>
        /// <returns>True for success else False</returns>
        public bool SetCursorAtElementStart(IHTMLElement elem)
        {
            bool success = false;

            if (m_pDoc2 == null)
                return success;
            
            IHTMLSelectionObject selobj = m_pDoc2.selection as IHTMLSelectionObject;
            if (selobj == null)
                return success;
            if ( (string.IsNullOrEmpty(selobj.type)) ||
                (selobj.type.ToLower() == "control"))
                return success;

            IHTMLTxtRange range = selobj.createRange() as IHTMLTxtRange;

            if (range == null)
                return success;

            int len = 0;
            if (elem == null)
                return success;
            //Moves the text range so that the start and
            //end positions of the range encompass the text in the given element.
            range.moveToElementText(elem);

            if (!string.IsNullOrEmpty(range.text))
                len = range.text.Length + 1;
            if(len == 0)
                return success;

            //Move end to the beginning
            range.moveEnd("character", -len);
            //Select
            range.select();

            return success;
        }

        /// <summary>
        /// Attempts to retreive char index from given X and Y coordinates
        /// relative to the body content.
        /// The point must be in client coordinates
        /// </summary>
        /// <param name="clientX"></param>
        /// <param name="clientY"></param>
        /// <returns>Char index or 0 for failure</returns>
        public int GetCharIndexFromPos(int clientX, int clientY)
        {
            int pos = 0;

            if (m_pDoc2 == null)
                return pos;

            IHTMLBodyElement body = m_pDoc2.body as IHTMLBodyElement;
            if (body == null)
                return pos;

            IHTMLTxtRange range = body.createTextRange();
            if (range == null)
                return pos;
            
            //Moves the start and end positions of the text range to the given point.
            range.moveToPoint(clientX, clientY);
            //Move start to the beginning of the text range
            pos = range.moveStart("textedit", -1);
            //If failed, pos == 0
            //The length of the text is the index of the character under mouse
            if( (pos == -1) && (!string.IsNullOrEmpty( range.text)) )
                pos = range.text.Length;
            return pos;
        }

        /// <summary>
        /// Attempts to retreive a character position relative to the body
        /// content based on the character index.
        /// </summary>
        /// <param name="charindex"></param>
        /// <returns>Left and top coordinates of the character or -1,-1</returns>
        public tagPOINT GetPosFromCharIndex(int charindex)
        {
            IMarkupPointer impStart;
            IDisplayPointer idpCur;
            tagPOINT point = new tagPOINT();
            point.x = -1;
            point.y = -1;

            if (m_pDoc2 == null)
                return point;

            if (m_pDisplayServices == null)
                return point;

            IHTMLElement elem = m_pDoc2.body;
            if (elem == null)
                return point;

            //Setup markup pointer
            m_pMarkupServices.CreateMarkupPointer(out impStart);

            //Position the markup pointer after beginning of the body tag
            impStart.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpCur);

            //Move markup pointer accoring to charindex value
            for (int i = 0; i < charindex; i++)
            {
                impStart.MoveUnit(_MOVEUNIT_ACTION.MOVEUNIT_NEXTCHAR);
            }
            //Move current display pointer to the beginning of target char
            idpCur.MoveToMarkupPointer(impStart, null);

            ILineInfo info = null;
            idpCur.GetLineInfo(out info);
            if (info != null)
            {
                point.x = info.x;
                point.y = info.baseLine;
            }




            return point;
        }

        /// <summary>
        /// Attempts to retreive Caret position
        /// </summary>
        /// <returns></returns>
        public tagPOINT GetCaretPosition()
        {
            IHTMLCaret pcaret = null;
            tagPOINT point = new tagPOINT();
            point.x = -1;
            point.y = -1;

            if (m_pDisplayServices == null)
                return point;

            m_pDisplayServices.GetCaret(out pcaret);
            if (pcaret != null)
            {
                //get the current caret position so we can reset it later
                pcaret.GetLocation(out point, 1);
                //pcaret.MoveDisplayPointerToCaret(idpOriginal);
                //Hide caret
                //pcaret.Hide();
                //Moves Caret to a given displaypointer
                //pcaret.MoveCaretToPointer(idpCur, false, 1);
            }

            return point;
        }

        /// <summary>
        /// Attempts to retrive a word located under mouse pointer
        /// relative to body content
        /// </summary>
        /// <param name="clientX">Left cocordinates based relative to client</param>
        /// <param name="clientY">Top cocordinates based relative to client</param>
        /// <returns></returns>
        public string GetWordUnderMouse(int clientX, int clientY)
        {
            string content = string.Empty;
            int icount = 0;
            if (m_pDoc2 == null)
                return content;

            IHTMLBodyElement body = m_pDoc2.body as IHTMLBodyElement;
            if (body == null)
                return content;

            IHTMLTxtRange range = body.createTextRange();
            if (range == null)
                return content;

            //Moves the start and end positions of the text range to the given point.
            range.moveToPoint(clientX, clientY);

            //Get another textrange for the element and see they are in range
            //Can pass the target element as a parameter to this method or make
            //an overload of this method with the extra parameter.
            IHTMLElement elem = m_pDoc2.elementFromPoint(clientX, clientY);
            if (elem != null)
            {
                IHTMLSelectionObject sel = m_pDoc2.selection as IHTMLSelectionObject;
                if (sel != null)
                {
                    IHTMLTxtRange range2 = sel.createRange() as IHTMLTxtRange;
                    if (range2 != null)
                    {
                        range2.moveToElementText(elem);
                        if (!range2.inRange(range))
                            return content;
                    }
                }
            }

            //Move start and end till we reach a space,...
            while (true)
            {
                icount = range.moveStart("character", -1);
                if (icount == 0)
                    break;

                if (!string.IsNullOrEmpty(range.text))
                {
                    //Check for (word | word). | char represents mouse position
                    if( (range.text.Length == 1) && 
                        (range.text == " ") 
                        )
                    {
                        //Move end one 1 forward
                        icount = range.moveEnd("character", 1);
                        //Only spaces then bail
                        if( (icount < 1) || (range.text == "  ") )
                            return content;
                    }
                    else if ((range.text.StartsWith(" ")) || 
                        (range.htmlText.IndexOf(Environment.NewLine) > -1) ||
                        (range.htmlText.IndexOf("<BR>", StringComparison.CurrentCultureIgnoreCase) > -1 ) )
                    {
                        //Reached a space/newline
                        range.moveStart("character", 1);
                        break;
                    }
                }
            }

            //iloop = 0;
            while (true)
            {
                icount = range.moveEnd("character", 1);
                if (icount < 1) //0 or -1 0r 1
                    break;

                if( (!string.IsNullOrEmpty(range.text)) && 
                    ( (range.text.EndsWith(" ")) || 
                    //To avoid
                    //<P>jksf+ksjfd</P>
                    //<P>skjf-kfjk</P>
                    (range.htmlText.IndexOf(Environment.NewLine) > -1) ||
                    (range.htmlText.IndexOf("<BR>", StringComparison.CurrentCultureIgnoreCase) > -1) )
                    )
                {
                    range.moveEnd("character", -1);
                    break;
                }
            }

            content = range.text; // +"\r\n" + range.htmlText;
            return content;
        }

        public IHighlightSegment UnderLineWord2(IHTMLTxtRange range)
        {
            IMarkupPointer impStart;
            IMarkupPointer impEnd;
            //Get body element
            IHTMLElement body = m_pDoc2.body;
            //Setup two markup pointers
            m_pMarkupServices.CreateMarkupPointer(out impStart);
            m_pMarkupServices.CreateMarkupPointer(out impEnd);
            //Move pointers to our range
            m_pMarkupServices.MovePointersToRange(range, impStart, impEnd);
            //Setup new element
            IHTMLElement ttelem = null;
            m_pMarkupServices.createElement(_ELEMENT_TAG_ID.TAGID_SPAN, 0, out ttelem);
            //Insert new element
            m_pMarkupServices.InsertElement(ttelem, impStart, impEnd);
            //Now underline
            return Underline2(ttelem);
        }

        /// <summary>
        /// Underline variation which returns IHighlightSegmentinstance
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public IHighlightSegment Underline2(IHTMLElement elem)
        {
            IHighlightSegment ihs = null;
            IMarkupPointer impStart;
            IMarkupPointer impEnd;
            IDisplayPointer idpStart;
            IDisplayPointer idpEnd;

            //---------------------------------------------
            // Create the start markup pointer and position
            // it after the beginning of the element
            //---------------------------------------------
            m_pMarkupServices.CreateMarkupPointer(out impStart);
            impStart.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpStart);
            idpStart.MoveToMarkupPointer(impStart, null);

            //---------------------------------------------
            // Create the end markup pointer and position
            // it before the end of the element
            //---------------------------------------------
            m_pMarkupServices.CreateMarkupPointer(out impEnd);
            impEnd.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_BeforeEnd);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpEnd);
            idpEnd.MoveToMarkupPointer(impEnd, null);

            m_pHighlightRenderingServices.AddSegment(
               idpStart, idpEnd, m_pHTMLRenderStyle, out ihs);
            
            m_pHighlightRenderingServices.MoveSegmentToPointers(ihs, idpStart, idpEnd);
            return ihs;
        }

        public void RemoveUnderline(IHTMLElement elem, IHighlightSegment ihs)
        {
            IMarkupPointer impStart;
            IMarkupPointer impEnd;
            IDisplayPointer idpStart;
            IDisplayPointer idpEnd;

            //---------------------------------------------
            // Create the start markup pointer and position
            // it after the beginning of the element
            //---------------------------------------------
            m_pMarkupServices.CreateMarkupPointer(out impStart);
            impStart.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpStart);
            idpStart.MoveToMarkupPointer(impStart, null);

            //---------------------------------------------
            // Create the end markup pointer and position
            // it before the end of the element
            //---------------------------------------------
            m_pMarkupServices.CreateMarkupPointer(out impEnd);
            impEnd.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_BeforeEnd);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpEnd);
            idpEnd.MoveToMarkupPointer(impEnd, null);
            //m_pHighlightRenderingServices.AddSegment(
            //          idpStart, idpEnd, m_pHTMLRenderStyle, out ihs);
            m_pHighlightRenderingServices.MoveSegmentToPointers(ihs, idpStart, idpEnd);
            m_pHighlightRenderingServices.RemoveSegment(ihs);
        }

        /// <summary>
        /// Attempts to find and highlight a word
        /// based on it's index and wordlen
        /// within body element
        /// </summary>
        /// <param name="wordindex"></param>
        /// <param name="wordlen"></param>
        public void UnderLineWord(int wordindex, int wordlen)
        {
            IMarkupPointer impStart;
            IMarkupPointer impEnd;

            //Get body element
            IHTMLElement body = m_pDoc2.body;
            if (body == null)
                return;

            //Setup two markup pointers
            m_pMarkupServices.CreateMarkupPointer(out impStart);
            m_pMarkupServices.CreateMarkupPointer(out impEnd);

            //Position the markup pointers after beginning of the body tag
            impStart.MoveAdjacentToElement(body, _ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);
            impEnd.MoveAdjacentToElement(body, _ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);

            //Move markup pointers accoring to wordindex value
            for (int i = 0; i < wordindex; i++)
            {
                impStart.MoveUnit(_MOVEUNIT_ACTION.MOVEUNIT_NEXTWORDBEGIN);
                impEnd.MoveUnit(_MOVEUNIT_ACTION.MOVEUNIT_NEXTWORDBEGIN);
            }

            //Setup new element
            IHTMLElement ttelem = null;
            //ELEMENT_TAG_ID.TAGID_TT
            m_pMarkupServices.createElement(_ELEMENT_TAG_ID.TAGID_SPAN, 0, out ttelem);
            //To remove this later, we can assign a unique property to the element
            //so we can use m_pHighlightRenderingServices.Removesegment
            //and m_pMarkupServices.removeelement

            //Adjust the end
            for (int i = 0; i < wordlen; i++)
            {
                impEnd.MoveUnit(_MOVEUNIT_ACTION.MOVEUNIT_NEXTCHAR);
            }
            //Insert new element
            m_pMarkupServices.InsertElement(ttelem, impStart, impEnd);

            //Now highlight
            this.Underline(ttelem);
        }

        /// <summary>
        /// Attempts to underline a given element
        /// To change the highlighting style, please
        /// refer to HTMLRenderStyle_xxxxxxx properties
        /// </summary>
        /// <param name="elem"></param>
        public void Underline(IHTMLElement elem)
        {
            IMarkupPointer impStart;
            IMarkupPointer impEnd;
            IDisplayPointer idpStart;
            IDisplayPointer idpEnd;
            // Add the segment
            IHighlightSegment ihs;

            //---------------------------------------------
            // Create the start markup pointer and position
            // it after the beginning of the element
            //---------------------------------------------
            m_pMarkupServices.CreateMarkupPointer(out impStart);
            impStart.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpStart);
            idpStart.MoveToMarkupPointer(impStart, null);

            //---------------------------------------------
            // Create the end markup pointer and position
            // it before the end of the element
            //---------------------------------------------
            m_pMarkupServices.CreateMarkupPointer(out impEnd);
            impEnd.MoveAdjacentToElement(elem, _ELEMENT_ADJACENCY.ELEM_ADJ_BeforeEnd);

            // Create a display pointer from the markup pointer
            m_pDisplayServices.CreateDisplayPointer(out idpEnd);
            idpEnd.MoveToMarkupPointer(impEnd, null);

            m_pHighlightRenderingServices.AddSegment(
                idpStart, idpEnd, m_pHTMLRenderStyle, out ihs);
        }

        /// <summary>
        /// Searches for a parent (or grandparent) element with the given tag
        /// ParentTagName must be in the form "TD" "TR" "TABLE" (uppercase)
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="ParentTagName"></param>
        /// <returns></returns>
        public IHTMLElement FindParent(IHTMLElement elem, string ParentTagName)
        {
            IHTMLElement retelem = elem.parentElement;
            while ((retelem != null) && (retelem.tagName != ParentTagName))
            {
                retelem = retelem.parentElement;
            }
            return retelem;
        }

        /// <summary>
        /// Returns the right neighbour which is a IHTMLElement in the HTML hierarchy
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IHTMLElement NextSibiling(IHTMLDOMNode node)
        {
            IHTMLDOMNode pnode = node;
            while (true)
            {
                IHTMLDOMNode pnext = pnode.nextSibling;
                if (pnext == null) //No more
                    break;
                //See if this is an HTMLElement
                IHTMLElement elem = pnext as IHTMLElement;
                if (elem != null)
                    return elem;
                pnode = pnext;
            }
            return null;
        }

        /// <summary>
        /// Returns the left neighbour which is a IHTMLElement in the HTML hierarchy
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IHTMLElement PreviousSibling(IHTMLDOMNode node)
        {
            IHTMLDOMNode pnode = node;
            while (true)
            {
                IHTMLDOMNode pnext = pnode.previousSibling;
                if (pnext == null) //No more
                    break;
                //See if this is an HTMLElement
                IHTMLElement elem = pnext as IHTMLElement;
                if (elem != null)
                    return elem;
                pnode = pnext;
            }
            return null;
        }

        /// <summary>
        /// Removes node element
        /// If RemoveAllChildren == true, Removes this element and all it's children from the document
        /// else it just strips this element but does not remove its children
        /// E.g.  "<BIG><b>Hello World</b></BIG>"  ---> strip BIG tag --> "<b>Hello World</b>"
        /// </summary>
        /// <param name="node">element to remove</param>
        /// <param name="RemoveAllChildren"></param>
        public IHTMLDOMNode RemoveNode(IHTMLElement elem, bool RemoveAllChildren)
        {
            IHTMLDOMNode node = elem as IHTMLDOMNode;
            if (node != null)
                return node.removeNode(RemoveAllChildren);
            else
                return null;
        }

        /// <summary>
        /// Return TRUE if the element is empty inside (e.g. <a name="#Pos1"></a>)
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public bool IsElementEmpty(IHTMLElement elem)
        {
            string str = elem.innerHTML;
            char[] c = { '\r', '\n', '\t' };
            if (string.IsNullOrEmpty(str))
                return true;
            str.Trim(c);
            return (str.Length > 0);
        }

        /// <summary>
        /// 1) If nothing is selected returns false
        /// 2) If the user has selected text or multiple elements
        /// inserts s_BeginHtml before and s_EndHtml behind the current selection
        /// 3)If the user has selected a control returns false
        /// </summary>
        /// <param name="s_BeginHtml"></param>
        /// <param name="s_EndHtml"></param>
        /// <returns></returns>
        // Example s_BeginHtml = "<SUB>", s_EndHtml = "</SUB>" will subscript the selected text
        // Example s_BeginHtml = "", s_EndHtml = "<BR>" will add a BR to the end of the selected text
        //AddToSelection(string.Empty, "<br>");
        public bool AddToSelection(string s_BeginHtml, string s_EndHtml)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLSelectionObject sel = m_pDoc2.selection as IHTMLSelectionObject;
            if (sel == null)
                return false;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range == null)
                return false;
            string shtml = string.Empty;
            if (!string.IsNullOrEmpty(s_BeginHtml))
                shtml = s_BeginHtml + range.htmlText;
            if (!string.IsNullOrEmpty(s_EndHtml))
                shtml += s_EndHtml;
            range.pasteHTML(shtml);
            range.collapse(false);
            range.select();
            return true;
        }

        /// <summary>
        /// Attempts to set the selection text from clipboard
        /// </summary>
        /// <returns></returns>
        public bool SetSelectionTextFromClipboard()
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLSelectionObject sel = m_pDoc2.selection as IHTMLSelectionObject;
            if (sel == null)
                return false;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range == null)
                return false;

            if ((Clipboard.ContainsText(TextDataFormat.Html)) ||
                Clipboard.ContainsText(TextDataFormat.Text))
            {
                // range will be valid if nothing is selected or if text is selected
                range.text = Clipboard.GetText();
                range.collapse(false);
                range.select();
            }

            return true;
        }

        /// <summary>
        /// The currently selected text/controls will be replaced by the given HTML code.
        /// If nothing is selected, the HTML code is inserted at the cursor position
        /// </summary>
        /// <param name="s_Html"></param>
        /// <returns></returns>
        public bool PasteIntoSelection(string s_Html)
        {
            if (m_pDoc2 == null)
                return false;
            
            IHTMLSelectionObject sel = m_pDoc2.selection as IHTMLSelectionObject;
            if (sel == null)
                return false;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range == null)
                return false;
            //none
            //text
            //control
            if ((!String.IsNullOrEmpty(sel.type)) &&
                (sel.type == "control"))
            {
                IHTMLControlRange ctlrange = range as IHTMLControlRange;
                if (ctlrange != null) //Control(s) selected
                {
                    IHTMLElement elem = null;
                    int ctls = ctlrange.length;
                    for (int i = 0; i < ctls; i++)
                    {
                        //Remove all selected controls
                        elem = ctlrange.item(i);
                        if (elem != null)
                        {
                            RemoveNode(elem, true);
                        }
                    }
                    // Now get the textrange after deleting all selected controls
                    range = null;
                    range = sel.createRange() as IHTMLTxtRange;
                }
            }

            if (range != null)
            {
                // range will be valid if nothing is selected or if text is selected
                // or if multiple elements are seleted
                range.pasteHTML(s_Html);
                range.collapse(false);
                range.select();
            }
            return true;
        }

        /// <summary>
        /// Inserts the given HTML code inside or outside of this Html element
        /// There are 4 possible insert positions:
        /// Outside-Before<TAG>Inside-Before InnerHTML Inside-After</TAG>Ouside-After
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="s_Html"></param>
        /// <param name="b_AtBegin"></param>
        /// <param name="b_Inside"></param>
        public void InsertHTML(IHTMLElement elem, string s_Html, bool b_AtBegin, bool b_Inside)
        {
            if (elem == null)
                return;
            string bs_Where;
            if (b_Inside)
            {
                if (b_AtBegin) bs_Where = "afterBegin";
                else bs_Where = "beforeEnd";
            }
            else // Outside
            {
                if (b_AtBegin) bs_Where = "beforeBegin";
                else bs_Where = "afterEnd";
            }
            elem.insertAdjacentHTML(bs_Where, s_Html);
        }

        /// <summary>
        /// Creates and appends an HTMLElement to the end of the document DOM
        /// </summary>
        /// <param name="TagName">a, img, table,...</param>
        public IHTMLDOMNode AppendChild(string TagName)
        {
            if (m_pDoc2 == null)
                return null;
            IHTMLElement elem = m_pDoc2.createElement(TagName) as IHTMLElement;
            if (elem == null)
                return null;
            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)elem;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return body.appendChild(nd);
        }

        /// <summary>
        /// Creates and appends an HTMLElement to a parent element
        /// </summary>
        /// <param name="TagName">a, img, table,...</param>
        public IHTMLDOMNode AppendChild(IHTMLElement parent, string TagName)
        {
            if (m_pDoc2 == null)
                return null;
            IHTMLElement elem = m_pDoc2.createElement(TagName) as IHTMLElement;
            if (elem == null)
                return null;
            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)elem;
            IHTMLDOMNode body = (IHTMLDOMNode)parent;
            return body.appendChild(nd);
        }

        public System.Drawing.Color GetDocumentColor(DocumentColors whichcolor)
        {
            System.Drawing.Color cret = System.Drawing.Color.Empty;
            if (m_pDoc2 != null)
            {
                if ((whichcolor == DocumentColors.Backcolor) && (m_pDoc2.bgColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.bgColor.ToString());
                else if ((whichcolor == DocumentColors.Forecolor) && (m_pDoc2.fgColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.fgColor.ToString());
                else if ((whichcolor == DocumentColors.Linkcolor) && (m_pDoc2.linkColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.linkColor.ToString());
                else if ((whichcolor == DocumentColors.ALinkcolor) && (m_pDoc2.alinkColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.alinkColor.ToString());
                else if ((whichcolor == DocumentColors.VLinkcolor) && (m_pDoc2.vlinkColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.vlinkColor.ToString());
            }
            return cret;
        }

        /// <summary>
        /// Embeds a BR tag at the selection
        /// </summary>
        /// <returns></returns>
        public bool EmbedBr()
        {
            return AddToSelection(string.Empty, "<br>");
        }

        //<img border="2" src="file:///C:/csEXWB/csEXWB.gif" align="center" hspace="2" vspace="2" alt="hello there" lowsrc="file:///C:/Desktop/blank.gif" width="600" height="463">
        public bool AppendImage(string src, string width, string height, string border, string alignment, string alt, string hspace, string vspace, string lowsrc)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLElement elem = m_pDoc2.createElement("img") as IHTMLElement;

            if (elem == null)
                return false;

            IHTMLImgElement imgelem = elem as IHTMLImgElement;
            if (imgelem == null)
                return false;

            if (!string.IsNullOrEmpty(src))
                imgelem.src = src;
            if (!string.IsNullOrEmpty(width))
                imgelem.width = int.Parse(width);
            if (!string.IsNullOrEmpty(height))
                imgelem.height = int.Parse(height);
            if (!string.IsNullOrEmpty(border))
                imgelem.border = border;
            if (!string.IsNullOrEmpty(alignment))
                imgelem.align = alignment;
            if (!string.IsNullOrEmpty(hspace))
                imgelem.hspace = int.Parse(hspace);
            if (!string.IsNullOrEmpty(vspace))
                imgelem.vspace = int.Parse(vspace);
            if (!string.IsNullOrEmpty(alt))
                imgelem.alt = alt;
            if (!string.IsNullOrEmpty(lowsrc))
                imgelem.lowsrc = lowsrc;

            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)elem;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);
        }

        //<a href="http://www.google.com" target="_blank">google</a>
        //Uses selection text
        public bool AppendAnchor(string href, string target)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLElement elem = m_pDoc2.createElement("a") as IHTMLElement;

            if (elem == null)
                return false;

            IHTMLAnchorElement aelem = elem as IHTMLAnchorElement;
            if (aelem == null)
                return false;

            if (!string.IsNullOrEmpty(href))
                aelem.href = href;
            if (!string.IsNullOrEmpty(target))
                aelem.target = target;

            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)elem;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);
        }

        //editor.AppendHr("center", string.Empty, "300", false);
        //left, center, right, justify
        public bool AppendHr(string align, string hrcolor, string width, bool noshade)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLElement elem = m_pDoc2.createElement("hr") as IHTMLElement;

            if (elem == null)
                return false;

            IHTMLHRElement hrelem = elem as IHTMLHRElement;
            if (hrelem == null)
                return false;

            if (!string.IsNullOrEmpty(align))
                hrelem.align = align;
            if (!string.IsNullOrEmpty(hrcolor))
                hrelem.color = hrcolor;
            if (!string.IsNullOrEmpty(width))
                hrelem.width = width;
            hrelem.noShade = noshade;

            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)elem;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);
        }
        
        #endregion

        #region Table specific

        //bordersize 2 or "2"
        public bool AppendTable(int colnum, int rownum, int bordersize, string alignment, int cellpadding, int cellspacing, string widthpercentage, int widthpixel, string backcolor, string bordercolor, string lightbordercolor, string darkbordercolor)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLTable t = (IHTMLTable)m_pDoc2.createElement("table");
            //set the cols
            t.cols = colnum;
            t.border = bordersize;

            if (!string.IsNullOrEmpty(alignment))
                t.align = alignment; //"center"
            t.cellPadding = cellpadding; //1
            t.cellSpacing = cellspacing; //2

            if (!string.IsNullOrEmpty(widthpercentage))
                t.width = widthpercentage; //"50%"; 
            else if (widthpixel > 0)
                t.width = widthpixel; //80;

            if (!string.IsNullOrEmpty(backcolor))
                t.bgColor = backcolor;

            if (!string.IsNullOrEmpty(bordercolor))
                t.borderColor = bordercolor;

            if (!string.IsNullOrEmpty(lightbordercolor))
                t.borderColorLight = lightbordercolor;

            if (!string.IsNullOrEmpty(darkbordercolor))
                t.borderColorDark = darkbordercolor;

            //Insert rows and fill them with space
            int cells = colnum - 1;
            int rows = rownum - 1;

            CalculateCellWidths(colnum);
            for (int i = 0; i <= rows; i++)
            {
                IHTMLTableRow tr = (IHTMLTableRow)t.insertRow(-1);
                for (int j = 0; j <= cells; j++)
                {
                    IHTMLElement c = tr.insertCell(-1) as IHTMLElement;
                    if (c != null)
                    {
                        c.innerHTML = HtmlSpace;
                        IHTMLTableCell tcell = c as IHTMLTableCell;
                        if (tcell != null)
                        {
                            //set width so as user enters text
                            //the cell width would not adjust
                            if (j == cells) //last cell
                                tcell.width = m_lastcellwidth;
                            else
                                tcell.width = m_cellwidth;
                        }
                    }
                }
            }

            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)t;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);

        }

        /// <summary>
        /// Returns parent row of passed cell element
        /// </summary>
        /// <param name="cellelem"></param>
        /// <returns></returns>
        public IHTMLTableRow GetParentRow(IHTMLElement cellelem)
        {
            IHTMLElement elem = FindParent(cellelem, "TR");
            if (elem != null)
                return elem as IHTMLTableRow;
            else
                return null;
        }

        /// <summary>
        /// Returns parent table of passed cell element
        /// </summary>
        /// <param name="cellelem"></param>
        /// <returns></returns>
        public IHTMLTable GetParentTable(IHTMLElement cellelem)
        {
            IHTMLElement elem = FindParent(cellelem, "TABLE");
            if (elem != null)
                return elem as IHTMLTable;
            else
                return null;
        }

        /// <summary>
        /// Get the column of the current cell
        /// zero based
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public int GetColIndex(IHTMLTableCell cell)
        {
            if (cell == null)
                return 0;
            int iret = 0;
            IHTMLTableCell tcell = cell;
            IHTMLDOMNode node = null;
            IHTMLElement elem = null;
            while (true)
            {
                node = tcell as IHTMLDOMNode;
                if (node == null)
                    break;
                elem = PreviousSibling(node);
                if (elem == null)
                    break;
                tcell = elem as IHTMLTableCell;
                if (tcell == null)
                    break;
                iret += tcell.colSpan;
            }
            return iret;
        }

        // zero based
        public int GetRowIndex(IHTMLElement cellelem)
        {
            IHTMLTableRow row = GetParentRow(cellelem);
            if (row != null)
                return row.rowIndex;
            return 0;
        }

        /// <summary>
        /// Zero based
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        public IHTMLTableRow GetRow(IHTMLTable table, int rowindex)
        {
            IHTMLElementCollection rows = table.rows;
            if (rows == null)
                return null;
            object obj = rowindex;
            return rows.item(obj, obj) as IHTMLTableRow;
        }

        public int GetRowCount(IHTMLTable table)
        {
            IHTMLElementCollection rows = table.rows;
            if (rows != null)
                return rows.length;
            return 0;
        }

        /// <summary>
        /// Gets the column count of row(rowindex)
        /// Accounts for colSpan property
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colindex"></param>
        /// <returns></returns>
        public int GetColCount(IHTMLTable table, int rowindex)
        {
            IHTMLTableRow row = GetRow(table, rowindex);
            if (row == null)
                return 0;
            int counter = 0;
            int cols = 0;
            while (true)
            {
                IHTMLTableCell cell = Row_GetCell(row, counter);
                if (cell == null)
                    break;
                cols += cell.colSpan;
                counter++;
            }
            return cols;
        }

        /// <summary>
        /// Deletes a given rowindex in a given table
        /// zero based
        /// If the table has no rows after deletion anymore
        /// we delete it compeletely
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowindex"></param>
        public void DeleteRow(IHTMLTable table, int rowindex)
        {
            table.deleteRow(rowindex);
            if (GetRowCount(table) == 0)
                RemoveNode(table as IHTMLElement, true);
        }

        public void DeleteCol(IHTMLTable table, int colindex)
        {
            for (int i = 0; true; i++)
            {
                IHTMLTableRow row = GetRow(table, i);
                if (row == null)
                    break;

                IHTMLTableCell cell = Row_GetCell(row, colindex);
                if (cell == null)
                    continue;
                RemoveNode(cell as IHTMLElement, true);
                if (Row_GetCellCount(row) == 0)
                {
                    RemoveNode(table as IHTMLElement, true);
                    break;
                }

                //Accounts for colspan
                //Row_DeleteCol(row, colindex);

                IHTMLElementCollection cells = row.cells;
                CalculateCellWidths(cells.length);
                for (int j = 0; j < cells.length; j++)
                {
                    object obj = j;
                    IHTMLTableCell cella = cells.item(obj, obj) as IHTMLTableCell;
                    if (cella != null)
                    {
                        if ((j + 1) == cells.length)
                            cella.width = m_lastcellwidth;
                        else
                            cella.width = m_cellwidth;
                    }
                }
            }
        }

        public int Row_GetCellCount(IHTMLTableRow row)
        {
            if (row == null)
                return 0;
            IHTMLElementCollection cells = row.cells;
            if (cells != null)
                return cells.length;
            return 0;
        }

        public void Row_DeleteCol(IHTMLTableRow row, int index)
        {
            int col = 0;
            int span = 0;
            IHTMLTableCell cell = Row_GetCell(row, 0);
            IHTMLElement elem = null;
            while (true)
            {
                if (cell == null)
                    return;
                span = cell.colSpan;
                //cell.cellIndex
                if (span == 1)
                {
                    if (col == index)
                    {
                        RemoveNode(cell as IHTMLElement, true);
                        if (Row_GetCellCount(row) == 0)
                        {
                            IHTMLTable table = GetParentTable(row as IHTMLElement) as IHTMLTable;
                            if (table != null)
                                RemoveNode(table as IHTMLElement, true);
                            break;
                        }
                    }
                }
                else if (span > 1)// cell spans about multiple columns
                {
                    if (index >= col && index < col + span)
                    {
                        cell.colSpan = span - 1; // reduce cellspan
                        break;
                    }
                }
                col += span;
                //Get next sibiling
                elem = NextSibiling(cell as IHTMLDOMNode);
                if (elem != null)
                    cell = elem as IHTMLTableCell;
                else
                    cell = null;
            }
        }

        public IHTMLElement Row_InsertCell(IHTMLTableRow row, int index)
        {
            IHTMLElement elem = row.insertCell(index) as IHTMLElement;
            if (elem != null)
                elem.innerHTML = HtmlSpace;
            return elem;
        }

        public IHTMLElement Row_InsertCell(IHTMLTableRow row, int index, string cellwidth)
        {
            IHTMLElement elem = row.insertCell(index) as IHTMLElement;
            if (elem != null)
            {
                elem.innerHTML = HtmlSpace;
                if (!string.IsNullOrEmpty(cellwidth))
                {
                    IHTMLTableCell tcell = elem as IHTMLTableCell;
                    if (tcell != null)
                    {
                        tcell.width = cellwidth;
                    }
                }
            }
            return elem;
        }

        public IHTMLElement Row_InsertCol(IHTMLTableRow row, int index)
        {
            int col = 0;
            int span = 0;
            object obj = 0;
            IHTMLElementCollection cells = row.cells;
            IHTMLElement retelem = null;

            for (int i = 0; true; i++)
            {
                obj = i;
                IHTMLTableCell cell = cells.item(obj, obj) as IHTMLTableCell;
                if (cell == null) // insert behind the rightmost cell
                {
                    retelem = Row_InsertCell(row, i);
                    break;
                }
                span = cell.colSpan;
                if (span == col) // insert at the left of the specified cell
                {
                    retelem = Row_InsertCell(row, i);
                    break;
                }
                if ((index > col) && (index < (col + span)))
                {
                    cell.colSpan = span + 1; // increase cellspan of multi column cell
                    retelem = cell as IHTMLElement;
                    break;
                }
                col += span;
                retelem = null;
            }

            //Set width as evenly as possible
            CalculateCellWidths(cells.length);
            for (int i = 0; i < cells.length; i++)
            {
                obj = i;
                IHTMLTableCell cell = cells.item(obj, obj) as IHTMLTableCell;
                if (cell != null)
                {
                    if ((i + 1) == cells.length)
                        cell.width = m_lastcellwidth;
                    else
                        cell.width = m_cellwidth;
                }
            }
            return retelem;
        }

        public IHTMLTableCell Row_GetCell(IHTMLTableRow row, int index)
        {
            IHTMLElementCollection cells = row.cells;
            if (cells == null)
                return null;
            object obj = index;
            return cells.item(obj, obj) as IHTMLTableCell;
        }

        private string m_cellwidth = string.Empty;
        private string m_lastcellwidth = string.Empty;
        private void CalculateCellWidths(int numberofcols)
        {
            //Even numbers. for 2 cols; 50%, 50%
            //Odd numbers.  for 3 cols; 33%, 33%, 34%
            int cellwidth = (int)(100 / numberofcols);
            m_cellwidth = cellwidth.ToString() + "%";
            //modulus operator (%).
            //http://msdn2.microsoft.com/en-us/library/0w4e0fzs.aspx
            //http://msdn2.microsoft.com/en-us/library/6a71f45d.aspx
            cellwidth += 100 % numberofcols;
            m_lastcellwidth = cellwidth.ToString() + "%";
        }

        public void InsertRow(IHTMLTable table, int index, int numberofcells)
        {
            IHTMLTableRow row = table.insertRow(index) as IHTMLTableRow;
            if (row == null)
                return;

            CalculateCellWidths(numberofcells);
            for (int j = 0; j < numberofcells; j++)
            {
                if ((j + 1) == numberofcells)
                    Row_InsertCell(row, -1, m_lastcellwidth);
                else
                    Row_InsertCell(row, -1, m_cellwidth);
            }
        }

        public void InsertCol(IHTMLTable table, int index)
        {
            for (int j = 0; true; j++)
            {
                IHTMLTableRow row = GetRow(table, j);
                if (row == null)
                    return;
                if (Row_InsertCol(row, index) == null)
                    return;
            }
        }

        #endregion

    } 
}
