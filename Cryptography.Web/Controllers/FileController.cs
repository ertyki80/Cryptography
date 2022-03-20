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
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public FileController( IFileService fileService,IWebHostEnvironment webHostEnvironment)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("file/details/{name}")]
        [HttpGet]
        public async Task<IActionResult> FileDetails([FromRoute] string name)
        {
            FileModel fileModel = new()
            {
                Name = name,
                Content = await _fileService.GetFileContent(name, _webHostEnvironment.WebRootPath)
            };
            
            return View(fileModel);
        }
        
        
        [HttpPost]
        [Route("file/upload")]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            await _fileService.SaveFile(uploadedFile,_webHostEnvironment.WebRootPath);
            return Redirect("Index");
        }
        
    }
}

