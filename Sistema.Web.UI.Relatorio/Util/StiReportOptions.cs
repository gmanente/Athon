using System;
using Stimulsoft.Report;

namespace Sistema.Web.UI.Relatorio.Util
{
    /**
     *    Funções úteis para geração de um relatório
     *
     * */
    public class StiReportOptions
    {
        // Função para definir o caminho absoluto da logo nos relatórios
        public static void SetAbsolutePath(long idCampus)
        {
            var imagePath = "";
            switch (idCampus)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    imagePath = "Univag"; break;
                case 5:
                    imagePath = "Famec"; break;
            }

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            StiOptions.Engine.Image.AbsolutePathOfImages = basePath + @"\Util\Imagens\" + imagePath;
        }

    }
}