using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptography.Bll.Models;

namespace Cryptography.Bll.Interfaces
{
    public interface ICaesarCipher
    {
        string Encipher(string input, int key);
        string Decipher(string input, int key);
        Task<List<BruteForceModel>>  BruteForce(string input);
    }
}