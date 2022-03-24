using System.Threading.Tasks;

namespace Cryptography.Bll.Interfaces
{
    public interface ICardanoCipherService
    {
        Task<string> Encode(string fileName, string webRootPath, string key);
        Task<string> Decode(string fileName, string webRootPath, string key);
        Task<string> BruteForce(string fileName, string webRootPath);
    }
}