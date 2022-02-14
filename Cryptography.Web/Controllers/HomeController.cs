using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
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

        public IActionResult ProcessFile(string name)
        {
            string text = _fileService.GetFile(name,_webHostEnvironment.WebRootPath);
            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            await _fileService.SaveFile(uploadedFile,_webHostEnvironment.WebRootPath);
            return Redirect("Index");
        }
    }
}

