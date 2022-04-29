using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;
using Newtonsoft.Json;

namespace Diver.Infrastructure.Repositories
{
    public class ImageHistoryRepository : RepositoryBase, IImageHistoryRepository
    {
        public async Task<IReadOnlyCollection<ImageHistory>> GetHistory(string imageRepository)
        {
            var result = await ReadConsoleOutput($"docker image history {imageRepository} --format \"{{{{json . }}}}\"");

            return result
                .Select(x => JsonConvert.DeserializeObject<ImageHistory>(x, JsonSerializerSettings))
                .Reverse()
                .ToList();
        }
    }
}
