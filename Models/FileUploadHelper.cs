using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadExample.Models
{
    public static class FileUploadHelper
    {
        public enum FileTypes
        {
            Photo,Model
        }

        public static bool IsFileEmpty(IFormFile file)
        {
            if(file is null || file.Length == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsValidExtension(IFormFile file, FileTypes type)
        {
            if (file is null)
			{
                return false;
			}
            string extension = Path.GetExtension(file.FileName).ToLower();
            switch (type)
            {
                case FileTypes.Photo:
                    string[] photoExtensions = { ".png", ".gif", ".jpg", ".jpeg" };
                    if (photoExtensions.Contains(extension))
                        return true;
                    return false;
                case FileTypes.Model:
                    string[] threeDExtensions = { ".stl" }; // only accpet stl for now
                    if (threeDExtensions.Contains(extension))
                        return true;
                    return false;
                default:
                    break;
            }
            return false;
        }
    }
}
