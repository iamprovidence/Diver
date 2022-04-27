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
            var workingDirectory = await _fileStructureRepository.GetWorkingDirectory(imageId);
            var breadcrumbs = workingDirectory
                .Select((item, index) => new BreadcrumbItemDto
                {
                    Title = item.Name,
                })
                .TakeLast(3)
                .ToList();

            if (breadcrumbs.Any())
            {
                breadcrumbs[0].IsHidden = workingDirectory.Count >= 3;
                breadcrumbs[^1].IsLast = true;
            }

            return breadcrumbs;
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
