using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Cryptography.Web.Controllers
{
    public class CryptographyController : Controller
    {
        private readonly ICaesarCipherService _caesarCipherService;
        private readonly IAffineCipherService _affineCipherService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IVigenereCipherService _vigenereCipherService;
        private readonly ICardanoCipherService _cardanoCipherService;
        
        public CryptographyController(
            IWebHostEnvironment webHostEnvironment, 
            ICaesarCipherService caesarCipherService,
            IAffineCipherService affineCipherService,
            ICardanoCipherService cardanoCipherService, IVigenereCipherService vigenereCipherService)
        {
            _webHostEnvironment = webHostEnvironment;
            _caesarCipherService = caesarCipherService;
            _affineCipherService = affineCipherService;
            _cardanoCipherService = cardanoCipherService;
            _vigenereCipherService = vigenereCipherService;
        }

        #region caesar
        [HttpPost]
        [Route("file/caesar-cipher/encipher/{inputFileName}/key/{key}")]
        public async Task<ActionResult > CaesarEncrypt([FromRoute] string inputFileName,[FromRoute] int key)
        {
            string filepath =
                await _caesarCipherService.Encode(inputFileName, _webHostEnvironment.WebRootPath, key);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/caesar-cipher/decipher/{inputFileName}/key/{key}")]
        public async Task<ActionResult > CaesarDecipher([FromRoute] string inputFileName, int key)
        {
            string filepath =
                await _caesarCipherService.Decode(inputFileName , _webHostEnvironment.WebRootPath, key);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/caesar-cipher/brute-force/{inputFileName}")]
        public async Task<ActionResult> CaesarBruteForce([FromRoute] string inputFileName)
        {
            string filepath =
                await _caesarCipherService.BruteForce(inputFileName , _webHostEnvironment.WebRootPath);
            MemoryStream memory = new();
            await using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        #endregion
        
        #region affine
        [HttpPost]
        [Route("file/affine-cipher/encipher/{inputFileName}/keys/{keyone}/{keytwo}")]
        public async Task<ActionResult > AffineEncrypt([FromRoute] string inputFileName,[FromRoute] int keyone,[FromRoute] int keytwo)
        {
            string filepath =
                await _affineCipherService.Encode(inputFileName, _webHostEnvironment.WebRootPath, keyone,keytwo);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/affine-cipher/decipher/{inputFileName}/keys/{keyone}/{keytwo}")]
        public async Task<ActionResult > AffineDecipher([FromRoute] string inputFileName,[FromRoute] int keyone,[FromRoute] int keytwo)
        {
            string filepath =
                await _affineCipherService.Decode(inputFileName , _webHostEnvironment.WebRootPath, keyone,keytwo);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/affine-cipher/brute-force/{inputFileName}")]
        public async Task<ActionResult> AffineBruteForce([FromRoute] string inputFileName)
        {
            string filepath =
                await _affineCipherService.BruteForce(inputFileName , _webHostEnvironment.WebRootPath);
            MemoryStream memory = new();
            await using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        #endregion
        
        #region cardano
        [HttpPost]
        [Route("file/cardano-cipher/encipher/{inputFileName}/keys/{keyone}")]
        public async Task<ActionResult > CardanoEncrypt([FromRoute] string inputFileName,[FromRoute] string keyone)
        {
            string filepath =
                await _cardanoCipherService.Encode(inputFileName, _webHostEnvironment.WebRootPath, keyone);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/cardano-cipher/decipher/{inputFileName}/keys/{keyone}")]
        public async Task<ActionResult > CardanoDecipher([FromRoute] string inputFileName,[FromRoute] string keyone)
        {
            string filepath =
                await _cardanoCipherService.Decode(inputFileName , _webHostEnvironment.WebRootPath, keyone);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/cardano-cipher/brute-force/{inputFileName}")]
        public async Task<ActionResult> CardanBruteForce([FromRoute] string inputFileName)
        {
            string filepath =
                await _cardanoCipherService.BruteForce(inputFileName , _webHostEnvironment.WebRootPath);
            MemoryStream memory = new();
            await using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        #endregion
        
        #region vigenere
        [HttpPost]
        [Route("file/vigenere-cipher/encipher/{inputFileName}/keys/{keyone}")]
        public async Task<ActionResult > VigenereEncrypt([FromRoute] string inputFileName,[FromRoute] string keyone)
        {
            string filepath =
                await _vigenereCipherService.Encode(inputFileName, _webHostEnvironment.WebRootPath, keyone);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        
        [HttpPost]
        [Route("file/vigenere-cipher/decipher/{inputFileName}/keys/{keyone}")]
        public async Task<ActionResult > VigenereDecipher([FromRoute] string inputFileName,[FromRoute] string keyone)
        {
            string filepath =
                await _vigenereCipherService.Decode(inputFileName , _webHostEnvironment.WebRootPath, keyone);
            MemoryStream memory = new();
            using (FileStream stream = new(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(filepath).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(filepath));
        }
        #endregion
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
    }
}

