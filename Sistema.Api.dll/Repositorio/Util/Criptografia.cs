using System.Security.Cryptography;
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class Criptografia
    {
        // MD5
        public static string MD5(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sbString = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));

            return sbString.ToString();
        }

        // HashSHA1
        public static string HashSHA1(string valor)
        {
            SHA1 hasher = SHA1.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] array = encoding.GetBytes(valor);
            array = hasher.ComputeHash(array);

            StringBuilder strHexa = new StringBuilder();

            foreach (byte item in array)
                strHexa.Append(item.ToString("x2"));

            return strHexa.ToString();
        }

        // HashSHA256
        public static string HashSHA256(string valor)
        {
            SHA256 hasher = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] array = encoding.GetBytes(valor);
            array = hasher.ComputeHash(array);

            StringBuilder strHexa = new StringBuilder();

            foreach (byte item in array)
                strHexa.Append(item.ToString("x2"));

            return strHexa.ToString();
        }

        // Base64Encode
        static public string Base64Encode(string toEncode)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(toEncode);

            return System.Convert.ToBase64String(plainTextBytes);
        }

        // Base64Decode
        static public string Base64Decode(string encodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedData);

            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // Cesar
        private static string Cesar(string mensagem, int chave)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < mensagem.Length; i++)
            {
                char c = (char)(mensagem[i] + chave);
                builder.Append(c);
            }

            return builder.ToString();
        }

        // CifrarCesar
        public static string CifrarCesar(string mensagem, int chave)
        {
            return Base64Encode(Cesar(mensagem, chave));
        }

        // DecifraCesar
        public static string DecifraCesar(string mensagem, int chave)
        {
            return Cesar(Base64Decode(mensagem), -chave);
        }

    }
}