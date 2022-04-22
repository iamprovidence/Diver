using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Diver.Application.Images;
using Diver.Application.Images.Dtos;
using Diver.Common;
using Diver.Utilities;

namespace Diver.Pages.Images
{
    public class ImageListViewModel : ViewModelBase
    {
        private readonly ImageAppService _imageAppService;

        public ObservableCollection<ImageDto> Images { get; } = new();

        public ImageListViewModel(
            NavigationManager navigationManager,
            ImageAppService imageAppService)
            : base(navigationManager)
        {
            _imageAppService = imageAppService;
        }

        public async override void Activated()
        {
            await LoadImages();
        }

        private async Task LoadImages()
        {
            var images = await _imageAppService.GetImages();

            Images.ClearAdd(images);
        }

        public ICommand InspectImageCommand => new RelayCommand<ImageDto>(image =>
        {
            _navigationManager.Navigate<ImageDetails>();
        });
    }
}
