using System;
using System.ComponentModel;

namespace Diver.Domain.Models
{
    public class ImageHistory
    {
        [Description("VolumeId")]
        public string Id { get; init; }
        public string Comment { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string CreatedSince { get; init; }
        [Description("Command that was used to create Volume")]
        public string CreatedBy { get; init; }
        public string Size { get; init; }

        public bool IsAvailable => Id != "<missing>";
    }
}
