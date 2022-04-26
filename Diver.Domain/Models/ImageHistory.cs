using System;
using System.ComponentModel;

namespace Diver.Domain.Models
{
    public class ImageHistory
    {
        [Description("VolumeId")]
        public string Id { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedSince { get; set; }
        [Description("Command that was used to create Volume")]
        public string CreatedBy { get; set; }
        public string Size { get; set; }
    }
}
