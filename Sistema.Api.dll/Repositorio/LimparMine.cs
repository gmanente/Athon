using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sistema.Api.dll.Repositorio
{
    public class LimparMine
    {
        public static void Iniciar(string caminhoArq, string versaoArquivoSVN)
        {
            StringBuilder Sb = new StringBuilder();
            try
            {
                //string caminhoArq = "C:/Test/GradeConsepeRel2.txt";
                StreamReader SrArquivo = new StreamReader(caminhoArq, ASCIIEncoding.UTF8, true);
                String LinhaArquivo = SrArquivo.ReadLine();

                List<string> lstLinhas = new List<string>();
                List<int> lstPositLinhasErradas = new List<int>();

                while (!SrArquivo.EndOfStream)
                {
                    if (!LinhaArquivo.Contains("<<<<<<< .mine"))
                        lstLinhas.Add(LinhaArquivo);

                    LinhaArquivo = SrArquivo.ReadLine();
                }

                if (!LinhaArquivo.Contains("<<<<<<< .mine"))
                    lstLinhas.Add(LinhaArquivo);

                int cont = 0, fim;
                for (var i = 0; i < lstLinhas.Count; i++)
                {
                    var txt = lstLinhas[i];
                    if (txt.Contains("======="))
                    {
                        cont = i;
                    }
                    else if (txt.Contains(">>>>>>> .r" + versaoArquivoSVN))
                    {
                        fim = i;
                        for (; cont < (fim + 1); cont++)
                        {
                            lstPositLinhasErradas.Add(cont);
                        }
                    }
                }

                for (var i = 0; i < lstLinhas.Count; i++)
                {
                    if (!lstPositLinhasErradas.Contains(i))
                    {
                        var txt = lstLinhas[i];
                        Sb.AppendLine(txt);
                    }
                }

                string TxtDocument = Sb.ToString().Trim();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}