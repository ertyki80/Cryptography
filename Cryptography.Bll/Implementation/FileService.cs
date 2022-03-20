using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Cryptography.Bll.Models;
using Microsoft.AspNetCore.Http;

namespace Cryptography.Bll.Implementation
{
    public class FileService : IFileService
    {
        public async Task SaveFile(IFormFile uploadedFile, string webRootPath)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new();
            if (Path.GetExtension(uploadedFile.FileName) != ".txt")
            {
                return;
            }
            var fileName =Path.GetFileNameWithoutExtension(uploadedFile.FileName)+"__V1"+chars[random.Next(chars.Length)];
            fileName += Path.GetExtension(uploadedFile.FileName);
            string path = "/UploadedFiles/" +fileName;
            await using FileStream fileStream = new(webRootPath + path, FileMode.Create);
            await uploadedFile.CopyToAsync(fileStream);
        }

        public List<FileModel> GetFiles(string webRootPath)
        {
            string path = webRootPath + "/UploadedFiles/";
            string[] filePaths = Directory.GetFiles(path);
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel {Name = Path.GetFileName(filePath)});
            }

            return files;
        }

        public async Task<string> GetFileContent(string fileName, string webRootPath)
        {
            string path = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(path);
            return text;
        }
    }
}