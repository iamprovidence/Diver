using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;

namespace Diver.Infrastructure.Repositories
{
    public class ImageHistoryRepository : IImageHistoryRepository
    {
        public Task<IReadOnlyCollection<ImageHistory>> GetHistory(string imageRepository)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c docker image history {imageRepository}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };

            throw new NotImplementedException();
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
