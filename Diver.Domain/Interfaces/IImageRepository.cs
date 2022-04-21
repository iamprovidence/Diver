using System.Collections.Generic;
using System.Threading.Tasks;
using Diver.Domain.Models;

namespace Diver.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task<IReadOnlyCollection<Image>> GetAll();
    }
}
