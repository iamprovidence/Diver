using System.Collections.Concurrent;
using System.Threading.Tasks;
using Diver.Domain.Services;

namespace Diver.Domain.Interfaces
{
    internal class FileStructureNavigationServiceFactory : IFileStructureNavigationServiceFactory
    {
        private readonly ConcurrentDictionary<string, Task<IFileStructureNavigationService>> _volumeNavigation = new();

        private readonly IFileStructureRepository _fileStructureRepository;

        public FileStructureNavigationServiceFactory(IFileStructureRepository fileStructureRepository)
        {
            _fileStructureRepository = fileStructureRepository;
        }

        public Task<IFileStructureNavigationService> GetOrCreate(string volumeId)
        {
            return _volumeNavigation.GetOrAdd(volumeId, async (key) =>
            {
                var currentWorkingDirectory = await _fileStructureRepository.GetWorkingDirectory(volumeId);

                return new FileStructureNavigationService(currentWorkingDirectory);
            });
        }
    }
}
