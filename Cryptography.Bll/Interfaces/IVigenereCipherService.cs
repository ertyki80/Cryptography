using System.Threading.Tasks;

namespace Cryptography.Bll.Interfaces
{
    public interface IVigenereCipherService
    {
        Task<string> Encode(string fileName, string webRootPath, string keyOne);
        Task<string> Decode(string fileName, string webRootPath, string keyOne);
    }
}