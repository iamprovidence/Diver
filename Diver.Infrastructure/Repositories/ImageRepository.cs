using System.Collections.Generic;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;

namespace Diver.Infrastructure.Repositories
{
    public class ImageRepository : RepositoryBase, IImageRepository
    {
        public async Task<IReadOnlyCollection<Image>> GetAll()
        {
            var result = await ReadConsoleOutput("docker images --format \"{{ json . }}\"");

            return DeserializeJsonl<Image>(result);
        }
    }
}
