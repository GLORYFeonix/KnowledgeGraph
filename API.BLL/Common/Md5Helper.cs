using System.Security.Cryptography;
using System.Text;

namespace API.BLL
{
    public static class Md5Helper
    {
        public static string ToMd5(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(str));
            var md5Str = BitConverter.ToString(output).Replace("-", "");
            return md5Str;
        }
    }
}