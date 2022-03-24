using System.IO;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;

namespace Cryptography.Bll.Implementation
{
    public class VigenereCipherService:IVigenereCipherService
    {
        public async Task<string> Encode(string fileName, string webRootPath, string keyOne)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            VigenereCipher vigenereCipher = new VigenereCipher();
            string encryptedText = vigenereCipher.Encode(keyOne,text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }

        public async Task<string> Decode(string fileName, string webRootPath, string keyOne)
        {
            string inputPath = webRootPath + "/UploadedFiles/" + fileName;
            string text = await File.ReadAllTextAsync(inputPath);
            VigenereCipher vigenereCipher = new VigenereCipher();
            string encryptedText = vigenereCipher.Decode(keyOne,text);
            string outPath = webRootPath + "/EncryptedFiles/" + fileName;
            
            await File.WriteAllTextAsync(outPath, encryptedText);
            return  webRootPath + "\\EncryptedFiles\\" + fileName;
        }
    }
}