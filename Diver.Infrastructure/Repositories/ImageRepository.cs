﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diver.Domain;

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
                    Arguments = "/c docker images",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };
            process.Start();

            var result = await process.StandardOutput.ReadToEndAsync();

            var images = result
                .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(imageLine =>
                {
                    var imageData = imageLine.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    return new Image
                    {
                        Repository = imageData[0],
                        Tag = imageData[1],
                        ImageId = imageData[2],
                    };
                })
                .ToList();

            await process.WaitForExitAsync();
            process.Close();

            return images;
        }
    }
}
