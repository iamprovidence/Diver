using System.Collections.ObjectModel;
using System.Windows.Input;
using Diver.Application.FileStructure;
using Diver.Application.FileStructure.Dtos;
using Diver.Application.ImageHistoryData;
using Diver.Application.ImageHistoryData.Dtos;
using Diver.Application.Images;
using Diver.Application.Images.Dtos;
using Diver.Common;
using Diver.Utilities;

namespace Diver.Pages.Images
{

    public class ImageDetailsViewModelParams : IViewModelParams
    {
        public string ImageId { get; init; }
    }

    public class ImageDetailsViewModel : ViewModelBase<ImageDetailsViewModelParams>
    {
        private readonly ImageAppService _imageAppService;
        private readonly ImageHistoryAppService _imageHistoryAppService;
        private readonly FileStructureAppService _fileStructureAppService;

        public ObservableData<ImageDto> Image { get; } = new();
        public ObservableData<ImageHistoryListItemDto> SelectedHistoryItem { get; } = new();
        public ObservableCollection<ImageHistoryListItemDto> ImageHistory { get; } = new();
        public ObservableCollection<BreadcrumbItemDto> Breadcrumbs { get; } = new();
        public ObservableCollection<FileListItemDto> Files { get; } = new();

        public ImageDetailsViewModel(
            NavigationManager navigationManager,
            ImageAppService imageAppService,
            ImageHistoryAppService imageHistoryAppService,
            FileStructureAppService fileStructureAppService,
            ImageDetailsViewModelParams @params)
            : base(navigationManager, @params)
        {
            _imageAppService = imageAppService;
            _imageHistoryAppService = imageHistoryAppService;
            _fileStructureAppService = fileStructureAppService;
        }

        public override async void Activated()
        {
            Image.Data = await _imageAppService.GetImage(Params.ImageId);

            var imageHistory = await _imageHistoryAppService.GetHistory(Params.ImageId);
            ImageHistory.ClearAdd(imageHistory);
        }

        public ICommand GoBackCommand => new RelayCommand(sender =>
        {
            _navigationManager.Navigate<ImageList>();
        });

        public ICommand SelectImageHistoryCommand => new RelayCommand<ImageHistoryListItemDto>(async data =>
        {
            var files = await _fileStructureAppService.GetFileStructure(data.VolumeId);
            Files.ClearAdd(files);

            var breadcrumbs = await _fileStructureAppService.GetBreadcrumbs(data.VolumeId);
            Breadcrumbs.ClearAdd(breadcrumbs);
        });

        public ICommand OpenDirectoryCommand => new RelayCommand<FileListItemDto>(async data =>
        {
            if (!data.IsDirectory)
            {
                return;
            }

            if (SelectedHistoryItem.Data is null)
            {
                return;
            }

            var files = await _fileStructureAppService.GetFileStructure(SelectedHistoryItem.Data.VolumeId);
            Files.ClearAdd(files);

            var breadcrumbs = await _fileStructureAppService.GetBreadcrumbs(SelectedHistoryItem.Data.VolumeId);
            Breadcrumbs.ClearAdd(breadcrumbs);
        });

        public ICommand NavigateDirectoryCommand => new RelayCommand<BreadcrumbItemDto>(async data =>
        {
            if (SelectedHistoryItem.Data is null)
            {
                return;
            }

            var files = await _fileStructureAppService.GetFileStructure(SelectedHistoryItem.Data.VolumeId);
            Files.ClearAdd(files);

            var breadcrumbs = await _fileStructureAppService.GetBreadcrumbs(SelectedHistoryItem.Data.VolumeId);
            Breadcrumbs.ClearAdd(breadcrumbs);
        });
    }
}
