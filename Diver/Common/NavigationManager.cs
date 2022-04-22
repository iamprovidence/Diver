using System;
using System.Windows.Controls;
using Autofac;

namespace Diver.Common
{
    public class NavigationManager
    {
        private readonly ILifetimeScope _lifetimeScope;

        public NavigationManager(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Navigate<TControl>()
            where TControl : UserControl
        {
            var control = _lifetimeScope.Resolve<TControl>();
            var viewModel = TryResolveViewModel<TControl>();

            if (viewModel is not null)
            {
                control.DataContext = viewModel;
            }

            _lifetimeScope.Resolve<IContentPresenter>().SetContent(control);
        }

        private ViewModelBase TryResolveViewModel<TControl>()
            where TControl : UserControl
        {
            var typeName = typeof(TControl).FullName;
            var viewModelName = $"{typeName}ViewModel";
            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType is null)
            {
                return null;
            }

            return _lifetimeScope.Resolve(viewModelType) as ViewModelBase;
        }
    }
}
