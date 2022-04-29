using System;
using System.Threading;
using System.Threading.Tasks;

namespace Diver.Domain.Interfaces
{
    public interface IDockerFileBuilder
    {
        Task Build(string name, string dockerfilePath, IProgress<string> progress, CancellationToken cancellationToken);
    }
}
