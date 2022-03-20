using System.Threading.Tasks;

namespace Cryptography.Bll.Interfaces
{
    public interface ICaesarCipherService
    {
        Task<string> Encode(string fileName,string webRootPath,int key);
        Task<string> Decode(string fileName,string webRootPath,int key);
        Task<string> BruteForce(string fileName,string webRootPath);
    }
}