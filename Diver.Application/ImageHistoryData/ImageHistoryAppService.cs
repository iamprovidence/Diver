using System.Collections.Generic;
using System.Threading.Tasks;
using Diver.Application.ImageHistoryData.Dtos;

namespace Diver.Application.ImageHistoryData
{
    public class ImageHistoryAppService
    {
        public async Task<IReadOnlyCollection<ImageHistoryListItemDto>> GetHistory(string imageId)
        {
            return new ImageHistoryListItemDto[]
            {
                new ImageHistoryListItemDto
                {
                    Index= 0,
                    Command ="FROM MyBaseImage",
                    Size = "5.57mb",
                },
            };
        }
    }
}
