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
        string GetFile(string fileName,string webRootPath);
    }
}