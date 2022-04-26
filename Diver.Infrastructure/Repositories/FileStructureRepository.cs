using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;

namespace Diver.Infrastructure.Repositories
{
    public class FileStructureRepository : RepositoryBase, IFileStructureRepository
    {
        public async Task<IReadOnlyCollection<FileStructureItem>> GetImageFiles(string volumeId)
        {
            var template = @"
            {
                ""Attributes"" = $1,
                ""LinkCount"" = $2,
                ""Owner"" = $3,
                ""Group"" = $4,
                ""FileSizeInBytes"" = $5,
                ""LastAccess"" = $6,
                ""FileName"" = $7
            }";

            var content = await ReadConsoleOutput($"docker run --rm --interactive --tty {volumeId} sh -c \"ls -ld * .*\"");

            var test = content
                .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => Regex.Replace(item, @"([-drwx]{10})\s+(\d+)\s+(\w+)\s+(\w+)\s+(\d+)\s+(\w+\s+\d+\s+\d+:\d+)\s(.*)", template))
                .ToArray();

            var list = new FileStructureItem[]
            {
                new FileStructureItem()
                {
                    Attributes = "dxxxxxxxxx",
                    FileName = "bin",
                },
            };

            return list;
        }
    }
}
