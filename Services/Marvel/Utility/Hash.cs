using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace Marvel.Services.Marvel.Utility
{
    public class Hash
    {
        public static string CreateHash(string publicKey, string privateKey, string timestamp)
        {
            // Concatenate the inputs in the specified order
            string input = $"{timestamp}{privateKey}{publicKey}";

            // Convert the concatenated string to bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Initialize a new instance of SHA256
            using (MD5 md5 = MD5.Create()) {
                // Compute the hash value for the input bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the hash bytes to a string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++) {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
