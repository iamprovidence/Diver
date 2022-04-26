using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Application.FileStructure.Dtos;
using Diver.Domain.Interfaces;

namespace Diver.Application.FileStructure
{
    public class FileStructureAppService
    {
        private readonly IFileStructureRepository _fileStructureRepository;

        public FileStructureAppService(IFileStructureRepository fileStructureRepository)
        {
            _fileStructureRepository = fileStructureRepository;
        }

        public async Task<IReadOnlyCollection<BreadcrumbItemDto>> GetBreadcrumbs(string imageId)
        {
            var breadcrumbs = new BreadcrumbItemDto[]
            {
                new BreadcrumbItemDto()
                {
                    Title = "a",
                },
                new BreadcrumbItemDto()
                {
                    Title = "b",
                },
                new BreadcrumbItemDto()
                {
                    Title = "c",
                },
                new BreadcrumbItemDto()
                {
                    Title = "d",
                },
                new BreadcrumbItemDto()
                {
                    Title = "e",
                },
            };

            var random = new Random();
            var amount = random.Next(1, breadcrumbs.Length + 1);
            breadcrumbs = breadcrumbs.Take(amount).ToArray();

            var displayedBreadcrumbs = breadcrumbs
                .TakeLast(3)
                .ToArray();

            displayedBreadcrumbs[0].IsHidden = breadcrumbs.Length >= 3;
            displayedBreadcrumbs[^1].IsLast = true;

            return displayedBreadcrumbs;
        }

        public async Task<IReadOnlyCollection<FileListItemDto>> GetFileStructure(string imageId)
        {
            var files = await _fileStructureRepository.GetImageFiles(imageId);

            return files
                .Select(x => new FileListItemDto
                {
                    IsDirectory = x.IsDirectory,
                    Name = x.FileName,
                })
                .ToList();
        }

    }
}
