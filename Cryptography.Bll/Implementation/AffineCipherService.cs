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
            AffineCipher affineCipher = new AffineCipher();
            affineCipher.Initialize(keyOne,keyTwo);
            string encryptedText = affineCipher.Encode(text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> Decode(string fileName, string webRootPath, int keyOne, int keyTwo)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            AffineCipher affineCipher = new AffineCipher();
            affineCipher.Initialize(keyOne,keyTwo);
            string encryptedText = affineCipher.Decode(text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> BruteForce(string fileName, string webRootPath)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            AffineCipher affineCipher = new AffineCipher();
            affineCipher.Initialize(0,0);
            List<AffineCipherBruteForce> bruteForceModels = await affineCipher.BruteForce(text);
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