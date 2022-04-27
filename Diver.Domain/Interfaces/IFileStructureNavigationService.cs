using System.Collections.Generic;
using Diver.Domain.Models;

namespace Diver.Domain.Interfaces
{
    public interface IFileStructureNavigationService
    {
        void Open(string name);
        void GoBackTo(string name);

        IReadOnlyCollection<WorkingDirectory> GetCurrentWorkingDirectory();
    }
}
