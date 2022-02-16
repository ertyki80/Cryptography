using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Cryptography.Bll.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cryptography.Web.Controllers
{
    public class HomeController : Controller
    { 
        IFileService _fileService;
        private IWebHostEnvironment _webHostEnvironment;
        
        public HomeController( IFileService fileService,IWebHostEnvironment webHostEnvironment)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
 
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProcessFileCaesarCipherDecipher(string name)
        {
            FileModel fileModel = new FileModel()
            {
                Name = name,
                Content = await _fileService.GetFileContent(name, _webHostEnvironment.WebRootPath)
            };
            
            return View(fileModel);
        }
        
        public async Task<IActionResult> ProcessFileCaesarCipherEncipher(string name)
        {
            FileModel fileModel = new FileModel()
            {
                Name = name,
                Content = await _fileService.GetFileContent(name, _webHostEnvironment.WebRootPath)
            };
            
            return View(fileModel);
        }
        
        public async Task<IActionResult> ProcessFileBruteForceCaesarCipherDecipher(string name)
        {
            FileModel fileModel = new FileModel()
            {
                Name = name,
                Content = await _fileService.GetFileContent(name, _webHostEnvironment.WebRootPath)
            };
            
            return View(fileModel);
        }

        [HttpPost]
        public async Task<ActionResult > EncryptAndDownload(string inputFileName, int key)
        {
            string filepath =
                await _fileService.GetCaesarCipherEncryptedFile(inputFileName, _webHostEnvironment.WebRootPath, key);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        public async Task<ActionResult > DecipherAndDownload(string inputFileName, int key)
        {
            string filepath =
                await _fileService.GetCaesarCipherEncryptedFile(inputFileName , _webHostEnvironment.WebRootPath, key);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        public async Task<ActionResult> BruteForceDecipherAndDownload(string inputFileName)
        {
            string filepath =
                await _fileService.GetBruteForceCaesarCipherDecryptedFile(inputFileName , _webHostEnvironment.WebRootPath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"}
            };
        }


        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            await _fileService.SaveFile(uploadedFile,_webHostEnvironment.WebRootPath);
            return Redirect("Index");
        }
    }
}

