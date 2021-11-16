using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Sistema.Web.API
{
    public class Util
    {
        /// <summary>
        /// Faz um tratamento para consulta de: Nome, Matricula ou CPF
        /// </summary>
        /// <param name="str">String de entrada</param>
        /// <returns></returns>
        public static dynamic SanitizeIdentity(string str)
        {
            if (str == null) return null;

            str = Regex.Replace(str, "[._-]", "");    // Limpa o CPF
            str = Regex.Replace(str, "'", "''");      // Escapa aspas simples do nome
            str = Regex.Replace(str, @"\s{2,}", " "); // Remove excesso de espaços

            return str;
        }


        /// <summary>
        /// Remove espaços em branco no ínico e fim de uma String
        /// </summary>
        /// <param name="str">String de entrada</param>
        /// <returns></returns>
        public static string TrimLR(string str)
        {
            if (str == null) return null;

            Regex trimL = new Regex(@"^\s+");
            Regex trimR = new Regex(@"\s+$");

            str = trimL.Replace(str, "");
            str = trimR.Replace(str, "");

            return str;
        }

        /// <summary>
        /// Remove espaços em branco de uma String
        /// </summary>
        /// <param name="str">String de entrada</param>
        /// <returns></returns>
        public static string CleanSpaces(string str)
        {
            if (str == null) return null;

            str = Regex.Replace(str, @"\s{1,}", "");

            return str;
        }



        /// <summary>
        /// Formata número de CPF
        /// </summary>
        /// <param name="str">Número de CPF</param>
        /// <returns></returns>
        public static string FormatarCpf(string str)
        {
            if (str == null) return null;

            str = Regex.Replace(str, @"[^0-9]", "");
            str = Regex.Replace(str, @"^([0-9]{3})([0-9]{3})([0-9]{3})([0-9]{2})$", "$1.$2.$3-$4");

            return str;
        }

        /// <summary>
        /// Formata número de telefone com Nro de 8-9 dígitos
        /// </summary>
        /// <param name="str">Número de Telefone</param>
        /// <returns></returns>
        public static string FormatarTelefone(string str)
        {
            if (str == null) return null;

            str = Regex.Replace(str, @"[^0-9]", "");
            str = Regex.Replace(str, @"^([0-9]{2})([9]{1})?([0-9]{4})([0-9]{4})$", "($1) $2$3-$4");  // Formata com DDD
            str = Regex.Replace(str, @"^([9]{1})?([0-9]{4})([0-9]{4})$", "$1$2-$3");                 // Formata sem DDD

            return str;
        }

        /// <summary>
        /// Converte e Formata uma entrada do tipo DateTime para uma String de data no formato BR
        /// </summary>
        /// <param name="val">Valor DateTime</param>
        /// <returns></returns>
        public static string FormatarDataBR(DateTime? val)
        {
            val = Convert.ToDateTime(val);

            string str = string.Format("{0:dd/MM/yyyy}", val);

            return str;
        }

        /// <summary>
        /// Converte e Formata uma entrada do tipo DateTime para uma String de data/hora no formato BR
        /// </summary>
        /// <param name="val">Valor DateTime</param>
        /// <returns></returns>
        public static string FormatarDataHoraBR(DateTime? val)
        {
            val = Convert.ToDateTime(val);

            string str = string.Format("{0:dd/MM/yyyy HH:mm}", val);

            return str;
        }



        /// <summary>
        /// <c>FetchObj</c> irá retornar o valor de um objeto aninhado
        /// </summary>
        /// <example>
        /// Exemplos de verificação nula profunda (Deep null checking)
        /// <code>
        /// Forma com Reflection (Usando este método)
        /// string TurnoDescricao = Util.fetchObj(alunoSemestreVO, "GradeLetivaTurma.GradeLetivaTurno.Turno.Descricao");
        /// OU
        /// Forma com Null-conditional (Suportada a partir VS2015)
        /// string TurnoDescricao = alunoSemestreVO.GradeLetivaTurma?.GradeLetivaTurno?.Turno?.Descricao;
        /// </code>
        /// </example>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static dynamic FetchObj(dynamic obj, string name)
        {
            var parts = Regex.Split(name, @"\??\.");
            foreach (String part in parts)
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }


    }
}