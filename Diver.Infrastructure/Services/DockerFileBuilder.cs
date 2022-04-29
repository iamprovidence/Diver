using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Diver.Domain.Interfaces
{
    internal class DockerFileBuilder : IDockerFileBuilder
    {
        public async Task Build(string name, string dockerfilePath, IProgress<string> progress, CancellationToken cancellationToken)
        {
            var buildingContextPath = Directory.GetParent(dockerfilePath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"/c $env:DOCKER_BUILDKIT = 0; cd {buildingContextPath}; docker build . --tag {name.ToLower()}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };
            process.Start();

            if (cancellationToken.IsCancellationRequested)
            {
                process.Kill();
                return;
            }

            while (!process.StandardOutput.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var output = process.StandardOutput.ReadLine();

                await Task.Delay(1_000);

                if (output.Contains("STEP", StringComparison.InvariantCultureIgnoreCase))
                {
                    progress.Report(output);
                }
            }

            if (cancellationToken.IsCancellationRequested)
            {
                process.Kill();
                return;
            }

            await process.WaitForExitAsync(cancellationToken);
            process.Close();
        }
    }
}
