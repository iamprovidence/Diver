using System;

namespace Diver.Domain.Models
{
    public class FileStructureItem
    {
        private string _attributes;

        public string Attributes
        {
            get
            {
                return _attributes;
            }
            init
            {
                if (value.Length != 10) throw new ArgumentException("Value has incorrect length");

                _attributes = value;
            }
        }

        public int LinkCount { get; init; }
        public string Owner { get; init; }
        public string Group { get; init; }
        public long FileSizeInBytes { get; init; }
        public DateTimeOffset LastAccess { get; init; }
        public string FileName { get; init; }

        public bool IsDirectory => Attributes[0] == 'd';

        public bool CanUserRead => Attributes[1] == 'r';
        public bool CanUserWrite => Attributes[2] == 'w';
        public bool CanUserExecute => Attributes[3] == 'x';

        public bool CanGroupRead => Attributes[4] == 'r';
        public bool CanGroupWrite => Attributes[5] == 'w';
        public bool CanGroupExecute => Attributes[6] == 'x';

        public bool CanOtherRead => Attributes[7] == 'r';
        public bool CanOtherWrite => Attributes[8] == 'w';
        public bool CanOtherExecute => Attributes[9] == 'x';
    }
}
