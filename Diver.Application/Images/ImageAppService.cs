using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Application.Images.Dtos;
using Diver.Domain.Interfaces;

namespace Diver.Application.Images
{
    public class ImageAppService
    {
        private readonly IImageRepository _imageRepository;

        public ImageAppService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<IReadOnlyCollection<ImageListItemDto>> GetImages()
        {
            var images = await _imageRepository.GetAll();

            return images
                .Select(i => new ImageListItemDto
                {
                    ImageId = i.ImageId,
                    Repository = i.Repository,
                    Tag = i.Tag,
                    Created = "5 days ago",
                    Size = "5 mb",
                })
                .ToList();
        }

        public async Task<ImageDto> GetImage(string imageId)
        {
            return new ImageDto()
            {
                ImageId = imageId,

                Repository = "Docker test image",
                Created = "5 days ago",
                Size = "5 mb",
            };
        }
    }
}
