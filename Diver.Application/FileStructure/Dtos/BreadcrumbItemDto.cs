namespace Diver.Application.FileStructure.Dtos
{
    public class BreadcrumbItemDto
    {
        public string Title { get; init; }
        public bool IsHidden { get; set; }
        public bool IsLast { get; set; }
    }
}
