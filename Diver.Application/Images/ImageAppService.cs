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
                    ImageId = i.Id,
                    Repository = i.Repository,
                    Tag = i.Tag,
                    Created = i.CreatedSince,
                    Size = i.Size,
                })
                .ToList();
        }

        public async Task<ImageDto> GetImage(string imageId)
        {
            var images = await _imageRepository.GetAll();

            return images
                .Where(i => i.Id == imageId)
                .Select(i => new ImageDto
                {
                    ImageId = i.Id,
                    Repository = i.Repository,
                    Created = i.CreatedSince,
                    Size = i.Size,
                })
                .SingleOrDefault();
        }
    }
}
