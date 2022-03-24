using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;

namespace Cryptography.Bll.Implementation
{
    public class CardanoCipherService:ICardanoCipherService
    {

        public async Task<string> Encode(string fileName, string webRootPath, string key)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            CardanoCipher.Initialize(key);
            string encryptedText = CardanoCipher.Encode(text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> Decode(string fileName, string webRootPath, string key)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            CardanoCipher.Initialize(key);
            string encryptedText = CardanoCipher.Decode(text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }
        
        public async Task<string> BruteForce(string fileName, string webRootPath)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            string result = "hello world";
            string bruteForceModels =CardanoCipher.BruteForce(text,result);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            await File.WriteAllTextAsync(outPath, bruteForceModels);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }
    }
}