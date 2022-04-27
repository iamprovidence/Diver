using System.Collections.Generic;
using System.Threading.Tasks;
using Diver.Domain.Models;

namespace Diver.Domain.Interfaces
{
    public interface IFileStructureRepository
    {
        Task<IReadOnlyCollection<WorkingDirectory>> GetWorkingDirectory(string volumeId);
        Task<IReadOnlyCollection<FileStructureItem>> GetImageFiles(string volumeId);
    }
}
