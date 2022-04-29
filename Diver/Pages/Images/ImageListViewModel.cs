using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Diver.Application.Images;
using Diver.Application.Images.Dtos;
using Diver.Common;
using Diver.Utilities;
using Microsoft.Win32;

namespace Diver.Pages.Images
{
    public class ImageListViewModel : ViewModelBase
    {
        private readonly ImageAppService _imageAppService;

        public ObservableCollection<ImageListItemDto> Images { get; } = new();

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

        public ICommand InspectImageCommand => new RelayCommand<ImageListItemDto>(image =>
        {
            NavigationManager.Navigate<ImageDetails>(new ImageDetailsViewModelParams
            {
                ImageId = image.ImageId,
            });
        });

        public ICommand BuildImageCommand => new RelayCommand(data =>
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Docker Files|Dockerfile",
            };

            if (openFileDialog.ShowDialog() == true)
            {
                NavigationManager.Navigate<BuildImage>(new BuildImageViewModelParams
                {
                    DockerfilePath = openFileDialog.FileName,
                });
            }
        });
    }
}
