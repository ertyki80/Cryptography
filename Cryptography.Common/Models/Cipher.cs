using Cryptography.Common.Enums;

namespace Cryptography.Common.Models
{
    public class Cipher
    {
        public RotationMethod RotationMethod { get; set; }
        public int[,] CipherGrid { get; set; }
        public GridType GridType { get; set; }
    }
}