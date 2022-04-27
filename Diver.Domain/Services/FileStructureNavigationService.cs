using System.Collections.Generic;
using System.Linq;
using Diver.Domain.Interfaces;
using Diver.Domain.Models;

namespace Diver.Domain.Services
{
    internal class FileStructureNavigationService : IFileStructureNavigationService
    {
        private readonly Stack<WorkingDirectory> _workingDirectories;

        public FileStructureNavigationService(IEnumerable<WorkingDirectory> workingDirectories)
        {
            _workingDirectories = new Stack<WorkingDirectory>(workingDirectories);
        }

        public void Open(string name)
        {
            _workingDirectories.Push(new WorkingDirectory
            {
                Name = name,
            });
        }

        public void GoBackTo(string name)
        {
            while (_workingDirectories.Peek().Name != name)
            {
                _workingDirectories.Pop();
            }
            _workingDirectories.Pop();
        }

        public IReadOnlyCollection<WorkingDirectory> GetCurrentWorkingDirectory()
        {
            return _workingDirectories.Reverse().ToList();
        }
    }
}
