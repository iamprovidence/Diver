using System;
using System.Threading;
using System.Threading.Tasks;
using Diver.Application.Images;
using Diver.Common;

namespace Diver.Pages.Images
{
    public class BuildImageViewModelParams : IViewModelParams
    {
        public string DockerfilePath { get; init; }
    }

    public class BuildImageViewModel : ViewModelBase<BuildImageViewModelParams>
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly ImageAppService _imageAppService;

        public ObservableData<string> BuildStatus { get; } = new("Building...");

        public BuildImageViewModel(
            ImageAppService imageAppService,
            NavigationManager navigationManager,
            BuildImageViewModelParams @params)
            : base(navigationManager, @params)
        {
            _imageAppService = imageAppService;
        }

        public override async void Activated()
        {
            await StartBuildingImage();
        }

        public override void Dispose()
        {
            // _cancellationTokenSource.Cancel();
            // _cancellationTokenSource.Dispose();
        }

        private async Task StartBuildingImage()
        {
            var name = await _imageAppService
                .BuildImage(Params.DockerfilePath, new Progress<string>(UpdateBuildStatus), _cancellationTokenSource.Token);

            UpdateBuildStatus($"Done building {name}...");

            await Task.Delay(1000);

            NavigationManager.Navigate<ImageList>();
        }

        private void UpdateBuildStatus(string progress)
        {
            BuildStatus.Data = progress;
        }
    }
}
