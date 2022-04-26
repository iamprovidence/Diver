using System;

namespace Diver.Domain.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string Tag { get; set; }
        public string Repository { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedSince { get; set; }
        public string Size { get; set; }
    }
}
