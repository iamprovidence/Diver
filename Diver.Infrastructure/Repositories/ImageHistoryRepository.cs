using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;

namespace Diver.Infrastructure.Repositories
{
    public class ImageHistoryRepository : RepositoryBase, IImageHistoryRepository
    {
        public async Task<IReadOnlyCollection<ImageHistory>> GetHistory(string imageRepository)
        {
            var result = await ReadConsoleOutput($"docker image history {imageRepository} --format \"{{{{json . }}}}\"");

            var imageHistory = DeserializeJsonl<ImageHistory>(result);

            return imageHistory
                .Reverse()
                .ToList();
        }

        public Task BuildImageFromDockerfile(string imageRepository)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c DOCKER_BUILDKIT = 0 && docker build -t {imageRepository}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };

            throw new NotImplementedException();
        }

        public Task GetImageFiles(string volumeId)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c docker run --rm -it {volumeId} sh -c 'ls -l'",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };

            throw new NotImplementedException();
        }
    }
}
