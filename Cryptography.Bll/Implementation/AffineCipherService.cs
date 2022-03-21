using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Cryptography.Bll.Models;

namespace Cryptography.Bll.Implementation
{
    public class AffineCipherService:IAffineCipherService
    {
        public async Task<string> Encode(string fileName, string webRootPath, int keyOne, int keyTwo)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            string encryptedText = AffineCipher.Encode(text, keyOne,keyTwo);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> Decode(string fileName, string webRootPath, int keyOne, int keyTwo)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            string encryptedText = AffineCipher.Decode(text, keyOne,keyTwo);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> BruteForce(string fileName, string webRootPath)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            List<AffineCipherBruteForce> bruteForceModels = AffineCipher.BruteForce(text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            string encryptedText = null;
            foreach (AffineCipherBruteForce item in bruteForceModels)
            {
                encryptedText += $"Key first: {item.FirstKey} Key second: {item.SecondKey} \n'";
                encryptedText += $"Content:\n {item.Content}\n";
            }
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }
    }
}