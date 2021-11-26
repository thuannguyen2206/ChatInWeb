using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebChat.Common.Utilities.Storage
{
    public interface IFileStorage
    {
        string GetFileUrl(string path);

        Task SavaFileAsync(Stream mediaBinaryStream, string filePath);

        Task DeleteFileAsync(string path);

        /// <summary>
        /// File : 0
        /// Image : 1
        /// Video : 2
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        int GetTypeFile(string path);

        string GetFileName(string path);

    }
}
