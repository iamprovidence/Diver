using System;

namespace Diver.Domain.Models
{
    public class Image
    {
        public string Id { get; init; }
        public string Tag { get; init; }
        public string Repository { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string CreatedSince { get; init; }
        public string Size { get; init; }
    }
}
