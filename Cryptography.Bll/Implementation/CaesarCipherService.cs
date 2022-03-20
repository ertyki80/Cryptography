using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Cryptography.Bll.Models;

namespace Cryptography.Bll.Implementation
{
    public class CaesarCipherService : ICaesarCipherService
    {
        
        private ICaesarCipher _caesarCipher;
        
        public CaesarCipherService(ICaesarCipher caesarCipher)
        {
            _caesarCipher = caesarCipher;
        }

        public async Task<string> Encode(string fileName, string webRootPath, int key)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            string encryptedText = _caesarCipher.Encipher(text, key);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> Decode(string fileName, string webRootPath, int key)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            string encryptedText = _caesarCipher.Decipher(text, key);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> BruteForce(string fileName, string webRootPath)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            List<BruteForceModel> bruteForceModels = await _caesarCipher.BruteForce(text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            string encryptedText = null;
            foreach (BruteForceModel item in bruteForceModels)
            {
                encryptedText += "Shift: "+item.ShiftChar+'\n';
                encryptedText += "Content:\n"+item.Content+'\n';
            }
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }
    }
}