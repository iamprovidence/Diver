using System;
using System.Windows.Controls;
using Autofac;

namespace Diver.Common
{
    public class NavigationManager
    {
        private readonly ILifetimeScope _container;

        public NavigationManager(ILifetimeScope container)
        {
            _container = container;
        }

        public void Navigate<TControl>()
            where TControl : UserControl
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var control = _container.Resolve<TControl>();
                var viewModel = TryResolveViewModel<TControl>(scope);

                if (viewModel is not null)
                {
                    control.DataContext = viewModel;
                }

                _container.Resolve<IContentPresenter>().SetContent(control);
            }
        }

        private ViewModelBase TryResolveViewModel<TControl>(ILifetimeScope scope)
            where TControl : UserControl
        {
            var typeName = typeof(TControl).FullName;
            var viewModelName = $"{typeName}ViewModel";
            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType is null)
            {
                return null;
            }

            return scope.Resolve(viewModelType) as ViewModelBase;
        }
    }
}
