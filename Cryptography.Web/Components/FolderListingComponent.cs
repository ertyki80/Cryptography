using Cryptography.Bll.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cryptography.Web.Components
{
    [ViewComponent]
    public class FolderListingComponent:ViewComponent
    {
        IFileService _fileService;
        private IWebHostEnvironment _webHostEnvironment;
        
        public FolderListingComponent( IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public ViewViewComponentResult Invoke()
        {
            var files = _fileService.GetFiles(_webHostEnvironment.WebRootPath);
            return View(files);
        }
    }
}