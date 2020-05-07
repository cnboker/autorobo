using System;
using System.Collections.Generic;
using System.Text;
using MouseKeyboardLibrary;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace AutoRobo.Core.Macro
{
    public class Macror
    {

        List<MacroEvent> events = new List<MacroEvent>();
        int lastTimeRecorded = 0;

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();

        public Macror()
        {
            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
        }

        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            events.Add(
                new MacroEvent(
                    MacroEventType.MouseMove,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;
        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        /// <summary>
        /// 启动宏
        /// </summary>
        public void StartRecord()
        {

            events.Clear();
            lastTimeRecorded = Environment.TickCount;

            keyboardHook.Start();
            mouseHook.Start();

        }

        /// <summary>
        /// 停止宏
        /// </summary>
        public void StopRecord()
        {
            keyboardHook.Stop();
            mouseHook.Stop();
        }

        /// <summary>
        /// 回放宏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Play()
        {
            foreach (MacroEvent macroEvent in events)
            {
                Thread.Sleep(macroEvent.TimeSinceLastEvent);

                switch (macroEvent.MacroEventType)
                {
                    case MacroEventType.MouseMove:
                        {

                            MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;

                            MouseSimulator.X = mouseArgs.X;
                            MouseSimulator.Y = mouseArgs.Y;

                        }
                        break;
                    case MacroEventType.MouseDown:
                        {

                            MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;

                            MouseSimulator.MouseDown(mouseArgs.Button);

                        }
                        break;
                    case MacroEventType.MouseUp:
                        {

                            MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;

                            MouseSimulator.MouseUp(mouseArgs.Button);

                        }
                        break;
                    case MacroEventType.KeyDown:
                        {

                            KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;

                            KeyboardSimulator.KeyDown(keyArgs.KeyCode);

                        }
                        break;
                    case MacroEventType.KeyUp:
                        {

                            KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;

                            KeyboardSimulator.KeyUp(keyArgs.KeyCode);

                        }
                        break;
                    default:
                        break;
                }

            }

        }

        /// <summary>
        /// 序列化宏
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MacroEvent macroEvent in events)
            {
                sb.AppendLine(macroEvent.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        public void Deserialize(string data)
        {

        }

        /// <summary>
        /// 移动光标到新的位置
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="steps"></param>
        static public void LinearSmoothMove(Point newPosition, int steps)
        {
            Cursor cursor = new Cursor(Cursor.Current.Handle);
            Point start = Cursor.Position;
            PointF iterPoint = start;
            
            // Find the slope of the line segment defined by start and newPosition
            PointF slope = new PointF(newPosition.X - start.X, newPosition.Y - start.Y);

            // Divide by the number of steps
            slope.X = slope.X / steps;
            slope.Y = slope.Y / steps;

            // Move the mouse to each iterative point.
            for (int i = 0; i < steps; i++)
            {
                iterPoint = new PointF(iterPoint.X + slope.X, iterPoint.Y + slope.Y);              
                Cursor.Position = Point.Round(iterPoint);
                Thread.Sleep(5);
            }

            // Move the mouse to the final destination.
            Cursor.Position = newPosition;
        }

    }
}
