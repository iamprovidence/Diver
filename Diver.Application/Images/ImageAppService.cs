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

        public async Task<IReadOnlyCollection<ImageDto>> GetImages()
        {
            var images = await _imageRepository.GetAll();

            return images
                .Select(i => new ImageDto
                {
                    ImageId = i.ImageId,
                    Repository = i.Repository,
                    Tag = i.Tag,
                })
                .ToList();
        }
    }
}
