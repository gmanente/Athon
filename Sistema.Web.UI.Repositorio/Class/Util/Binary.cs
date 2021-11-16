using System;
using System.IO;
using System.Web;
using Sistema.Api.dll.Repositorio.Util;

namespace Sistema.Web.UI.Repositorio.Class.Util
{
    public class Binary 
    {
        //  Le a arquivo em um array de bytes a partir do sistema de arquivos
        public static byte[] GetBinary(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(caminhoArquivoFoto), FileMode.Open, FileAccess.Read);
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

        private static string GetPasta()
        {
            return Criptografia.MD5(DateTime.Now.ToString());

        }
    }
}