using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using Diver.Application.FileStructure.Dtos;

namespace Diver.Pages.Images.Converters
{
    [ValueConversion(sourceType: typeof(FileListItemDto), targetType: typeof(string))]
    public class FileToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fileDto = (FileListItemDto)value;

            if (fileDto.IsDirectory)
            {
                return "/Resources/FileIcons/Directory.png";
            }

            return Path.GetExtension(fileDto.Name).ToLower() switch
            {
                ".js" => "/Resources/FileIcons/js.png",
                ".txt" => "/Resources/FileIcons/txt.png",

                ".mp3" => "/Resources/FileIcons/mp3.png",

                ".mp4" => "/Resources/FileIcons/mp4.png",
                ".avi" => "/Resources/FileIcons/mp4.png",

                ".pdf" => "/Resources/FileIcons/pdf.png",

                ".htm" => "/Resources/FileIcons/html.png",
                ".html" => "/Resources/FileIcons/html.png",

                ".jpg" => "/Resources/FileIcons/jpg.png",
                ".jpeg" => "/Resources/FileIcons/jpg.png",
                ".png" => "/Resources/FileIcons/jpg.png",

                _ => null,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
