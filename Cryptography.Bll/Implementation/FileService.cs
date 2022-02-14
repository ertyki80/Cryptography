using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Cryptography.Bll.Models;
using Microsoft.AspNetCore.Http;

namespace Cryptography.Bll.Implementation
{
    public class FileService: IFileService
    {
        public async Task SaveFile(IFormFile uploadedFile, string webRootPath)
        {
            string path = "/Files/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
        }

        public List<FileModel> GetFiles( string webRootPath)
        {
            string path = webRootPath + "/Files/";
            string[] filePaths = Directory.GetFiles(path);
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel { Name = Path.GetFileName(filePath) });
            }

            return files;
        }

        public string GetFile(string fileName, string webRootPath)
        {
            string path = webRootPath + "/Files/" + fileName;
            string text = File.ReadAllText(path);
            return text;
        }
    }
}