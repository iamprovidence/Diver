using System;

namespace Diver.Common
{
    public abstract class ViewModelBase : IDisposable
    {
        protected NavigationManager NavigationManager { get; }

        public ViewModelBase(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public virtual void Activated()
        {
            return;
        }

        public virtual void Dispose()
        {
            return;
        }
    }

    public abstract class ViewModelBase<TParams> : ViewModelBase
        where TParams : IViewModelParams
    {
        public TParams Params { get; }

        protected ViewModelBase(NavigationManager navigationManager, TParams @params)
            : base(navigationManager)
        {
            Params = @params;
        }
    }
}
