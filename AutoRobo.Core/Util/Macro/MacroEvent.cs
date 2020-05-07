using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.Macro
{
    /// <summary>
    /// Series of events that can be recorded any played back
    /// </summary>
    [Serializable]
    public class MacroEvent
    {

        public MacroEventType MacroEventType;
        public EventArgs EventArgs;
        public int TimeSinceLastEvent;

        public MacroEvent(MacroEventType macroEventType, EventArgs eventArgs, int timeSinceLastEvent)
        {

            MacroEventType = macroEventType;
            EventArgs = eventArgs;
            TimeSinceLastEvent = timeSinceLastEvent;

        }

        public override string ToString()
        {
            string argString = "";
            if (EventArgs is MouseEventArgs)
            {
                MouseEventArgs arg = EventArgs as MouseEventArgs;
                argString = string.Format("{0} {1} {2} {3} {4}", arg.Button.ToString(), arg.Clicks.ToString(),
                    arg.Delta.ToString(), arg.X.ToString(), arg.Y.ToString());

            }
            else if (EventArgs is KeyEventArgs)
            {
                KeyEventArgs arg1 = EventArgs as KeyEventArgs;
                argString = string.Format("{0} {1}", arg1.KeyCode, arg1.Modifiers);
            }
            return string.Format("{0} {1} {2}", MacroEventType.ToString(),
                TimeSinceLastEvent, argString);
        }


    }
}
