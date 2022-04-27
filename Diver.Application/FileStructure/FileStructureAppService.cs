using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Application.FileStructure.Dtos;
using Diver.Domain.Interfaces;

namespace Diver.Application.FileStructure
{
    public class FileStructureAppService
    {
        private readonly IFileStructureNavigationServiceFactory _factory;
        private readonly IFileStructureRepository _fileStructureRepository;

        public FileStructureAppService(
            IFileStructureNavigationServiceFactory factory,
            IFileStructureRepository fileStructureRepository)
        {
            _factory = factory;
            _fileStructureRepository = fileStructureRepository;
        }

        public async Task OpenDirectory(string volumeId, string directoryName)
        {
            var navigationService = await _factory.GetOrCreate(volumeId);

            navigationService.Open(directoryName);
        }

        public async Task GoBackToDirectory(string volumeId, string directoryName)
        {
            var navigationService = await _factory.GetOrCreate(volumeId);

            navigationService.GoBackTo(directoryName);
        }
        public async Task<IReadOnlyCollection<BreadcrumbItemDto>> GetCurrentBreadcrumbs(string volumeId)
        {
            var navigationService = await _factory.GetOrCreate(volumeId);

            var workingDirectory = navigationService.GetCurrentWorkingDirectory();

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

        public async Task<IReadOnlyCollection<FileListItemDto>> GetCurrentFileStructure(string volumeId)
        {
            var navigationService = await _factory.GetOrCreate(volumeId);
            var workingDirectory = navigationService.GetCurrentWorkingDirectory();

            var files = await _fileStructureRepository.GetImageFiles(volumeId, workingDirectory);

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
