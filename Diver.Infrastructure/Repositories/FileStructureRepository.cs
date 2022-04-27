using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;
using Newtonsoft.Json;

namespace Diver.Infrastructure.Repositories
{
    public class FileStructureRepository : RepositoryBase, IFileStructureRepository
    {
        public async Task<IReadOnlyCollection<FileStructureItem>> GetImageFiles(string volumeId)
        {
            var containerCommand = "ls -ld * .* --full-time --color=never --group-directories-first";

            var content = await ReadConsoleOutput($"docker run --rm --interactive --tty {volumeId} sh -c \"{containerCommand}\"");

            return content
                .Select(item =>
                {
                    var attributesRegex = @"[-drwxt]{10}";
                    var linkCountRegex = @"\d+";
                    var ownerRegex = @"\w+";
                    var groupRegex = @"\w+";
                    var fileSizeRegex = @"\d+";
                    var lastAccessRegex = @"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2} \+\d{4}";
                    var fileNameRegex = @".*";

                    var pattern = $@"({attributesRegex})\s+({linkCountRegex})\s+({ownerRegex})\s+({groupRegex})\s+({fileSizeRegex})\s+({lastAccessRegex})\s({fileNameRegex})";

                    var template = @"
                    {
                        ""Attributes"" : ""$1"",
                        ""LinkCount"" : $2,
                        ""Owner"" : ""$3"",
                        ""Group"" : ""$4"",
                        ""FileSizeInBytes"" : $5,
                        ""LastAccess"" : ""$6"",
                        ""FileName"" : ""$7""
                    }";

                    return Regex.Replace(item, pattern, template);
                })
                .Select(x => JsonConvert.DeserializeObject<FileStructureItem>(x, JsonSerializerSettings))
                .ToList();
        }

        public async Task<IReadOnlyCollection<WorkingDirectory>> GetWorkingDirectory(string volumeId)
        {
            var containerCommand = "pwd";

            var content = await ReadConsoleOutput($"docker run --rm --interactive --tty {volumeId} sh -c \"{containerCommand}\"");

            var fullPath = content.SingleOrDefault() ?? string.Empty;

            return fullPath
                .Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new WorkingDirectory
                {
                    Name = x,
                })
                .ToList();
        }
    }
}
