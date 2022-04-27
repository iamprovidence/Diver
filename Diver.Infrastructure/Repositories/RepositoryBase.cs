using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diver.Infrastructure.JsonConverters;
using Newtonsoft.Json;

namespace Diver.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        protected static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new[]
            {
                new DateTimeWithAbbreviationConverter(),
            }
        };

        protected async Task<IReadOnlyCollection<string>> ReadConsoleOutput(string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {arguments}",
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

            return result
                .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }
    }
}
