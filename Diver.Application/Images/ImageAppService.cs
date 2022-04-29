using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diver.Application.Common;
using Diver.Application.Images.Dtos;
using Diver.Domain.Interfaces;

namespace Diver.Application.Images
{
    public class ImageAppService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IDockerFileBuilder _dockerFileBuilder;

        public ImageAppService(
            IImageRepository imageRepository,
            IDockerFileBuilder dockerFileBuilder)
        {
            _imageRepository = imageRepository;
            _dockerFileBuilder = dockerFileBuilder;
        }

        public async Task<string> BuildImage(string dockerfilePath, IProgress<string> progress, CancellationToken cancellationToken)
        {
            var images = await _imageRepository.GetAll();

            var randomName = string.Empty;
            do
            {
                randomName = RandomNameGenerator.Generate();
            }
            while (images.Any(i => i.Repository == randomName));

            await _dockerFileBuilder.Build(randomName, dockerfilePath, progress, cancellationToken);

            return randomName;
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

        public async Task<ImageDto> GetImage(string imageId, string repository)
        {
            var images = await _imageRepository.GetAll();

            return images
                .Where(i => i.Id == imageId)
                .Where(i => i.Repository == repository)
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
