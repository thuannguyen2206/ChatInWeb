using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Constants;

namespace WebChat.Common.Utilities.Storage
{
    public class FileStorage : IFileStorage
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER = "user-content";

        public FileStorage(IWebHostEnvironment webHostEnviroment)
        {
            _userContentFolder = Path.Combine(webHostEnviroment.WebRootPath, USER_CONTENT_FOLDER);
        }
        public async Task DeleteFileAsync(string path)
        {
            var filePath = Path.Combine(_userContentFolder, path);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string path)
        {
            return $"\\{USER_CONTENT_FOLDER}{path}";
        }

        public async Task SavaFileAsync(Stream mediaBinaryStream, string filePath)
        {
            var path = _userContentFolder + filePath;
            using (var output = new FileStream(path, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            }
        }

        public int GetTypeFile(string path)
        {
            string extension = Path.GetExtension(path).Trim().ToLower();
            if (SystemConstant.ImageExtension.Contains(extension))
            {
                return 1;
            }
            else if (SystemConstant.VideoExtension.Contains(extension))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
