namespace Diver.Common
{
    public abstract class ViewModelBase
    {
        protected readonly NavigationManager _navigationManager;

        public ViewModelBase(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public virtual void Activated()
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
