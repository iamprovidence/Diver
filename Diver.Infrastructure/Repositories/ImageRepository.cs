using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;
using Newtonsoft.Json;

namespace Diver.Infrastructure.Repositories
{
    public class ImageRepository : RepositoryBase, IImageRepository
    {
        public async Task<IReadOnlyCollection<Image>> GetAll()
        {
            var result = await ReadConsoleOutput("docker images --format \"{{ json . }}\"");

            return result
                .Select(x => JsonConvert.DeserializeObject<Image>(x, JsonSerializerSettings))
                .ToList();
        }
    }
}
