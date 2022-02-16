using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptography.Bll.Models;
using Microsoft.AspNetCore.Http;

namespace Cryptography.Bll.Interfaces
{
    public interface IFileService
    {
        Task SaveFile(IFormFile uploadedFile,string webRootPath);
        List<FileModel> GetFiles(string webRootPath);
        Task<string> GetFileContent(string fileName,string webRootPath);
        Task<string> GetCaesarCipherEncryptedFile(string fileName,string webRootPath,int key);
        Task<string> GetCaesarCipherDecryptedFile(string fileName,string webRootPath,int key);
        Task<string> GetBruteForceCaesarCipherDecryptedFile(string fileName,string webRootPath);

    }
}