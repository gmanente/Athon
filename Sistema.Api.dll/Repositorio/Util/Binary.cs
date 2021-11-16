using System;
using System.IO;
using System.Web;
//using Org.BouncyCastle.Asn1.Ocsp;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class Binary 
    {
        //  Le a arquivo em um array de bytes a partir do sistema de arquivos
        public static byte[] GetBinary(string caminhoArquivoFoto)
        {
            string path = HttpContext.Current != null ? HttpContext.Current.Server.MapPath(caminhoArquivoFoto) : Path.GetFullPath(caminhoArquivoFoto);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] arquivo = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return arquivo;
        }

        public static byte[] GetBinaryOutsiteProject(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] arquivo = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return arquivo;
        }

        // Escrever arquivo de saida retorna o camino do arquivo
        public static string SetBinary(byte[] arquivoArray, string extensao , string nomeArquivo)
        {
            string nomePasta = GetPasta();
            string folder = HttpContext.Current.Server.MapPath("~/Uploads/temp/" + nomePasta);
            bool isExists = System.IO.Directory.Exists(folder);
            if (!isExists)
                System.IO.Directory.CreateDirectory(folder);
            string nome = nomePasta + "/" + nomeArquivo + extensao;
            string caminho = folder + "/" + nomeArquivo + extensao;
            string caminhoRetorno = "/Uploads/temp/" + nome;
            FileStream fsw = new FileStream(caminho, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fsw);
            bw.Write(arquivoArray);
            bw.Close();
            fsw.Close();
            return caminhoRetorno;

        }

        public static string SetBinaryWithPath(string filePath, byte[] arquivoArray, string extensao, string nomeArquivo = "")
        {
            string nomePasta = GetPasta();
            string folder = HttpContext.Current.Server.MapPath(filePath);
            bool isExists = Directory.Exists(folder);
            if (!isExists)
                System.IO.Directory.CreateDirectory(folder);
            string nome = nomePasta + extensao;
            string caminho = folder + "/" + nomePasta + extensao;
            string caminhoRetorno = filePath.Replace("~", "") + nome.Replace("~", "");
            FileStream fsw = new FileStream(caminho, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fsw);
            bw.Write(arquivoArray);
            bw.Close();
            fsw.Close();
            string baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            return baseUrl + caminhoRetorno;

        }

        private static string GetPasta()
        {
            return Criptografia.MD5(DateTime.Now.ToString());
        }
    }
}