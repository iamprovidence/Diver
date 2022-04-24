using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diver.Application.FileStructure.Dtos;

namespace Diver.Application.FileStructure
{
    public class FileStructureAppService
    {

        public async Task<IReadOnlyCollection<BreadcrumbItemDto>> GetBreadcrumbs(string imageId)
        {
            var breadcrumbs = new BreadcrumbItemDto[]
            {
                new BreadcrumbItemDto()
                {
                    Title = "a",
                },
                new BreadcrumbItemDto()
                {
                    Title = "b",
                },
                new BreadcrumbItemDto()
                {
                    Title = "c",
                },
                new BreadcrumbItemDto()
                {
                    Title = "d",
                },
                new BreadcrumbItemDto()
                {
                    Title = "e",
                },
            };

            var random = new Random();
            var amount = random.Next(1, breadcrumbs.Length + 1);
            breadcrumbs = breadcrumbs.Take(amount).ToArray();

            var displayedBreadcrumbs = breadcrumbs
                .TakeLast(3)
                .ToArray();

            displayedBreadcrumbs[0].IsHidden = breadcrumbs.Length >= 3;
            displayedBreadcrumbs[^1].IsLast = true;

            return displayedBreadcrumbs;
        }

        public async Task<IReadOnlyCollection<FileListItemDto>> GetFileStructure(string imageId)
        {
            var list = new FileListItemDto[]
            {
                new FileListItemDto()
                {
                    IsDirectory = true,
                    Name = "bin",
                },
                new FileListItemDto()
                {
                    IsDirectory = true,
                    Name = "obj",
                },
                new FileListItemDto()
                {
                    Name = "script1.js",
                },
                new FileListItemDto()
                {
                    Name = "script2.js",
                },
                new FileListItemDto()
                {
                    Name = "script3.js",
                },
                new FileListItemDto()
                {
                    Name = "script4.js",
                },
                new FileListItemDto()
                {
                    Name = "script5.js",
                },
            };

            var random = new Random();
            var amount = random.Next(1, list.Length + 1);

            return list.Take(amount).ToList();
        }

    }
}
