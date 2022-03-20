using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;

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

        public Task<string> BruteForce(string fileName, string webRootPath)
        {
            throw new System.NotImplementedException();
        }
    }
}