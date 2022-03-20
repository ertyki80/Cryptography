using System.Threading.Tasks;

namespace Cryptography.Bll.Interfaces
{
    public interface IAffineCipherService
    {
        Task<string> Encode(string fileName,string webRootPath,int keyOne,int keyTwo);
        Task<string> Decode(string fileName,string webRootPath,int keyOne,int keyTwo);
        Task<string> BruteForce(string fileName,string webRootPath);
    }
}