using System.Collections.Generic;
using System.Threading.Tasks;
using Diver.Domain;

namespace Diver.Infrastructure.Repositories
{
    public interface IImageRepository
    {
        Task<IReadOnlyCollection<Image>> GetAll();
    }
}
