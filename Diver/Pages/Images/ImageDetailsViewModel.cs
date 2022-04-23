using System.Windows.Input;
using Diver.Common;

namespace Diver.Pages.Images
{
    public class ImageDetailsViewModelParams : IViewModelParams
    {
        public string ImageId { get; init; }
    }
    class ImageHistoryDto
    {
        public string Command { get; set; }
        public string Id { get; set; }
        public string Size { get; set; }
    }

    class FileDto
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; } = false;
    }
    public class ImageDetailsViewModel : ViewModelBase<ImageDetailsViewModelParams>
    {

        public ImageDetailsViewModel(
            NavigationManager navigationManager,
            ImageDetailsViewModelParams @params)
            : base(navigationManager, @params)
        {
        }

        public ICommand GoBackCommand => new RelayCommand(sender =>
        {
            _navigationManager.Navigate<ImageList>();
        });
    }
}
