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

        public void Navigate<TControl>(IViewModelParams @params = null)
            where TControl : UserControl
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var control = scope.Resolve<TControl>();
                var viewModel = TryResolveViewModel<TControl>(scope, @params);

                if (viewModel is not null)
                {
                    control.DataContext = viewModel;
                }

                _container.Resolve<IContentPresenter>().SetContent(control);
            }
        }

        private ViewModelBase TryResolveViewModel<TControl>(ILifetimeScope scope, IViewModelParams @params)
            where TControl : UserControl
        {
            var typeName = typeof(TControl).FullName;
            var viewModelName = $"{typeName}ViewModel";
            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType is null)
            {
                return null;
            }

            if (@params is not null)
            {
                var parameter = new TypedParameter(@params.GetType(), @params);

                return scope.Resolve(viewModelType, parameter) as ViewModelBase;
            }

            return scope.Resolve(viewModelType) as ViewModelBase;
        }
    }
}
