using System.ComponentModel;

namespace Diver.Common
{
    public class ObservableData<T> : INotifyPropertyChanged
    {
        private T _data;

        public event PropertyChangedEventHandler PropertyChanged;

        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }
    }
}
