using Microsoft.AspNetCore.Http;
using Core.Utilities.Helper.GuidHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helper.FileHelper
{
    public class FileHelper : IFileHelper
    {
        public string? Upload(IFormFile file, string root)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                string extension = Path.GetExtension(file.FileName);
                string guid = GuidHelper.GuidHelper.CreateGuid();
                string filepath = guid+ extension;

                using (FileStream fileStream = File.Create(root + filepath))
                {
                    fileStream.CopyTo(fileStream);
                    fileStream.Flush();
                    return filepath;
                }


            }

            return null;

        }

        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string Update(IFormFile file, string filePath,  string root)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Upload(file, root);
        }
    }
}
