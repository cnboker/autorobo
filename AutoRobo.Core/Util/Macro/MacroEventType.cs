using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Macro
{
    [Serializable]
    public enum MacroEventType
    {
        MouseMove,
        MouseDown,
        MouseUp,
        MouseWheel,
        KeyDown,
        KeyUp
    }
}
