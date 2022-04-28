using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Application.ImageHistoryData.Dtos;
using Diver.Domain.Interfaces;

namespace Diver.Application.ImageHistoryData
{
    public class ImageHistoryAppService
    {
        private readonly IImageHistoryRepository _imageHistoryRepository;

        public ImageHistoryAppService(IImageHistoryRepository imageHistoryRepository)
        {
            _imageHistoryRepository = imageHistoryRepository;
        }

        public async Task<IReadOnlyCollection<ImageHistoryListItemDto>> GetHistory(string imageId)
        {
            var imageHistory = await _imageHistoryRepository.GetHistory(imageId);

            return imageHistory
                .Select((x, index) => new ImageHistoryListItemDto
                {
                    Index = index,
                    VolumeId = x.Id,
                    Command = x.CreatedBy,
                    Size = x.Size,
                    IsAvailable = x.IsAvailable,
                })
                .ToList();
        }
    }
}
