using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Diver.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly NavigationManager _navigationManager;

        public ViewModelBase(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Activated()
        {
            return;
        }
    }
}
