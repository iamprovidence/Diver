using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Diver.Common;
using Diver.Utilities;
using Driver.Application;
using Driver.Application.Images.Dtos;

namespace Diver.Pages.Main
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ImageAppService _imageAppService;

        public ObservableCollection<ImageDto> Images { get; } = new();

        public MainWindowViewModel(ImageAppService imageAppService)
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
    }
}
