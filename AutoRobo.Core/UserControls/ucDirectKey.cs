using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucDirectKey : ucBaseEditor
    {
      
        public ucDirectKey(IEditorAction editorAction):base(editorAction)
        {
            InitializeComponent();           
        }

        public static List<Keys> sKeys = new List<Keys>(
           new Keys[]
            { // Alphanumeric keys.
              Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H,
              Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P,
              Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X,
              Keys.Y, Keys.Z,
              Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6,
              Keys.D7, Keys.D8, Keys.D9, Keys.Tab,
           
              // Punctuation and other keys
              Keys.Enter, Keys.Space,
              Keys.Subtract, Keys.Add, Keys.Divide, Keys.Multiply,
              // Editing keys
              Keys.Delete, Keys.Back,
              Keys.LShiftKey, Keys.RShiftKey, Keys.Shift, Keys.ShiftKey,

              // Navigation keys
              Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.PageUp, Keys.PageDown,
              Keys.Home, Keys.End,
          });
     
        protected override void OnLoad(EventArgs e)
        {
            foreach (var key in sKeys) {
                keysList.Items.Add(key);
            }
            base.OnLoad(e);
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionDirectionKey)base.Action;
                action.DirectionKey = keysList.Text;                
                return base.Action;
            }
            set
            {
                var action = (ActionDirectionKey)value;
                keysList.SelectedText = action.DirectionKey;                
                base.Action = action;
            }
        }

   
    }
}
