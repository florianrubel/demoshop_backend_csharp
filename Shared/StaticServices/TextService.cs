using System.Security.Cryptography;
using System.Text;

namespace Shared.StaticServices
{
    public static class TextService
    {
        public static string GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)))
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        public static Guid[] GetGuidArray(string value, char seperator = ',')
        {
            return value.Split(seperator)
                    .Select(g => { Guid temp; return Guid.TryParse(g, out temp) ? temp : Guid.Empty; })
                    .Where(g => g != Guid.Empty)
                    .Distinct()
                    .ToArray();
        }
    }
}
