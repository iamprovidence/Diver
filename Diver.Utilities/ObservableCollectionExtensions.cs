using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Diver.Utilities
{
    public static class ObservableCollectionExtensions
    {
        public static void ClearAdd<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> data)
        {
            observableCollection.Clear();
            foreach (var item in data)
            {
                observableCollection.Add(item);
            }
        }
    }
}
