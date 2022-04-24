namespace Diver.Application.FileStructure.Dtos
{
    public class FileListItemDto
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; } = false;
    }
}
