using System.Collections.Generic;
using System.Threading.Tasks;
using Diver.Domain.Models;

namespace Diver.Domain.Interfaces
{
    public interface IFileStructureRepository
    {
        public Task<IReadOnlyCollection<FileStructureItem>> GetImageFiles(string volumeId, IEnumerable<WorkingDirectory> workingDirectory);

        internal protected Task<IReadOnlyCollection<WorkingDirectory>> GetWorkingDirectory(string volumeId);
    }
}
