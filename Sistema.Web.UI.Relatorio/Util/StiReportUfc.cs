using Sistema.Api.dll.Src.Comum.Util;
using Stimulsoft.Report.Dictionary;

namespace Sistema.Web.UI.Relatorio.Util
{
    /**
     *    Funções do Banco de Dados transcritas para código C#
     *
     *    Usar como expressão no relatório
     * */
    public class StiReportUfc
    {
        private const string Categoria = "StiReportUfc";

        // Exemplo de uso: { UfcFormatarData(Aluno.DataPublicacao) }
        public static string UfcFormatarData(string Data)
        {
            return Tools.UfcFormatarData(Data);
        }

        // Exemplo de uso: { UfcFormatarCpf(Aluno.Cpf) }
        public static string UfcFormatarCpf(string Cpf)
        {
            return Tools.UfcFormatarCpf(Cpf);
        }

        // Exemplo de uso: { UfcFormatarNome(Aluno.NomeAluno) }
        public static string UfcFormatarNome(string Nome)
        {
            return Tools.UfcFormatarNome(Nome);
        }

        // Exemplo de uso: { UfcFormatarNomeSobrenome(Aluno.NomeAluno) }
        public static string UfcFormatarNomeSobrenome(string Nome)
        {
            return Tools.UfcFormatarNomeSobrenome(Nome);
        }

        // Exemplo de uso: { UfcFormatarTelefone(Aluno.Telefone) }
        public static string UfcFormatarTelefone(string Telefone)
        {
            return Tools.UfcFormatarTelefone(Telefone);
        }

        // Exemplo de uso: { UfcValidaNomeSocial(Aluno.NomeAluno, Aluno.NomeSocial) }
        public static string UfcValidaNomeSocial(string NomeAluno, string NomeSocial)
        {
            return Tools.UfcValidaNomeSocial(NomeAluno, NomeSocial);
        }

        // Exemplo de uso: { UfcValidaNomeSocialNomeCivil(Aluno.NomeAluno, Aluno.NomeSocial) }
        public static string UfcValidaNomeSocialNomeCivil(string NomeAluno, string NomeSocial)
        {
            return Tools.UfcValidaNomeSocialNomeCivil(NomeAluno, NomeSocial);
        }


        // Exemplo de uso: { UfcRegexReplace(DB.NroLivro, "\s+", " ") }
        public static string UfcRegexReplace(string Texto, string RegEx, string Replace)
        {
            return Tools.UfcRegexReplace(Texto, RegEx, Replace);
        }


        // Exemplo de uso: { UfcPrint(DB.NroRegistroDiploma, 'Nro de REgistro: ' + DB.NroRegistroDiploma) }
        public static string UfcPrint(string Coluna, string Texto)
        {
            return Tools.UfcPrint(Coluna, Texto);
        }

        // Exemplo de uso: { UfcPadLeft(DB.NroSerieDiploma, 10, '#') }
        public static string UfcPadLeft(string Caracter, int Comprimento = 7, char Mascara = '0')
        {
            return Tools.UfcPadLeft(Caracter, Comprimento, Mascara);
        }

        // Exemplo de uso: { UfcPadRight(DB.NroSerieDiploma, 10, '#') }
        public static string UfcPadRight(string Caracter, int Comprimento = 7, char Mascara = '0')
        {
            return Tools.UfcPadRight(Caracter, Comprimento, Mascara);
        }


        // Exemplo de uso: { UfcToExtenso(DB.Total, false) }
        public static string UfcToExtenso(decimal Valor, bool ExibirCentavos = true)
        {
            return Tools.UfcToExtenso(Valor, ExibirCentavos);
        }


        /**
         *  Registra os nomes de cada função
         *
         * */
        public static void RegisterFunctions()
        {
            StiFunctions.AddFunction(Categoria, "UfcFormatarData", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcFormatarCpf", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcFormatarNome", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcFormatarNomeSobrenome", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcFormatarTelefone", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcValidaNomeSocial", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcValidaNomeSocialNomeCivil", typeof(StiReportUfc), typeof(string));

            StiFunctions.AddFunction(Categoria, "UfcRegexReplace", typeof(StiReportUfc), typeof(string));

            StiFunctions.AddFunction(Categoria, "UfcPrint", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcPadLeft", typeof(StiReportUfc), typeof(string));
            StiFunctions.AddFunction(Categoria, "UfcPadRight", typeof(StiReportUfc), typeof(string));

            StiFunctions.AddFunction(Categoria, "UfcToExtenso", typeof(StiReportUfc), typeof(string));
        }
    }
}