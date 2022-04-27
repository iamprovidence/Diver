using System.Threading.Tasks;

namespace Diver.Domain.Interfaces
{
    public interface IFileStructureNavigationServiceFactory
    {
        Task<IFileStructureNavigationService> GetOrCreate(string volumeId);
    }
}
