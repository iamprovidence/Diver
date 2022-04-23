using Diver.Common;

namespace Diver.Pages.Images
{
    class ImageHistoryDto
    {
        public string Command { get; set; }
        public string Id { get; set; }
        public string Size { get; set; }
    }
    public class ImageDetailsViewModel : ViewModelBase
    {

        public ImageDetailsViewModel(NavigationManager navigationManager)
            : base(navigationManager)
        {
        }
    }
}
