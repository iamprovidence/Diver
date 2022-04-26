using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Diver.Infrastructure.JsonConverters;
using Newtonsoft.Json;

namespace Diver.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            Converters = new[]
            {
                new DateTimeWithAbbreviationConverter(),
            }
        });

        protected async Task<string> GetCliResult(string arguments)
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

            return result;
        }

        public IReadOnlyCollection<T> DeserializeJsonl<T>(string content)
        {
            using (var stringReader = new StringReader(content))
            {
                var jsonReader = new JsonTextReader(stringReader)
                {
                    SupportMultipleContent = true,
                };

                var items = new List<T>();
                while (jsonReader.Read())
                {
                    var item = JsonSerializer.Deserialize<T>(jsonReader);
                    items.Add(item);
                }
                return items;
            }
        }
    }
}
