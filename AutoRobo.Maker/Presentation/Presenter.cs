using System;
using AutoRobo.Core;
using AutoRobo.Core.Models;
using AutoRobo.Makers.Views;

namespace AutoRobo.Makers.Presentation
{
    public class Presenter<TView> where TView : class, IView
    {
        public TView View { get; private set; }
       
        protected log4net.ILog logger = log4net.LogManager.GetLogger("Presenter");

        public Presenter(TView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            View = view;
            View.Initialize += OnViewInitialize;
            View.Load += OnViewLoad;
        }

        public ModelSet Model {
            get {
                return Context.ActionModel;
            }
        }

        public AppContext Context { 
            get {
                ViewBase view = View as ViewBase;
                if (view == null) {
                    FormBase fb = View as FormBase;
                    return fb.Context;
                }
                return view.Context;
            }        
        }

        protected virtual void OnViewInitialize(object sender, EventArgs e) { }

        protected virtual void OnViewLoad(object sender, EventArgs e) { }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
