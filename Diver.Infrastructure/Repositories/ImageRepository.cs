using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;

namespace Diver.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public async Task<IReadOnlyCollection<Image>> GetAll()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c docker images --format \"{{json . }},\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };
            process.Start();

            var result = await process.StandardOutput.ReadToEndAsync();

            await process.WaitForExitAsync();
            process.Close();

            return JsonSerializer.Deserialize<IReadOnlyCollection<Image>>($"[{result}]", new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}
