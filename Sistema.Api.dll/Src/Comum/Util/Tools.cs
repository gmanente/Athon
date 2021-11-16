using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Src.Comum.Util
{
    /// <summary>
    ///  Author......: Giovanni Ramos
    ///  Date........: 06-09-2018
    ///  Description.: Biblioteca utilizada para armazenar funções para tratamento de dados de consulta do Banco de Dados
    /// </summary>
    public class Tools
    {
        /**
         * Função para Formatar Data no formato BR
         * */
        public static string UfcFormatarData(string Data)
        {
            if (Data == null)
                return null;

            Data = Regex.Replace(Data, @"^([0-9]{2})/([0-9]{2})/([0-9]{4})(.+)$", "$1/$2/$3");
            Data = Regex.Replace(Data, @"^([0-9]{4})-([0-9]{2})-([0-9]{2})(.+)$", "$1/$2/$3");

            return Data;
        }

        /**
         * Função para Formatar CPF
         * */
        public static string UfcFormatarCpf(string Cpf)
        {
            if (Cpf == null)
                return null;

            Cpf = Regex.Replace(Cpf, @"[^0-9]", "");
            Cpf = Regex.Replace(Cpf, @"^([0-9]{3})([0-9]{3})([0-9]{3})([0-9]{2})$", "$1.$2.$3-$4");

            return Cpf;
        }

        /**
         * Função para Formatar Telefone com 8 ou 9 dígitos
         * */
        public static string UfcFormatarTelefone(string Telefone)
        {
            if (Telefone == null)
                return null;

            Telefone = Regex.Replace(Telefone, @"[^0-9]", "");
            Telefone = Regex.Replace(Telefone, @"^([0-9]{2})([9]{1})?([0-9]{4})([0-9]{4})$", "($1) $2$3-$4");  // Formata com DDD
            Telefone = Regex.Replace(Telefone, @"^([9]{1})?([0-9]{4})([0-9]{4})$", "$1$2-$3");                 // Formata sem DDD

            return Telefone;
        }

        /**
         * Função para Formatar Nomes próprios
         * https://pt.stackoverflow.com/a/248
         * */
        public static string UfcFormatarNome(string Nome)
        {
            if (Nome == null)
                return null;

            Nome = Nome.ToLower();
            Nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Nome);
            Nome = CapitalizarNome(Nome);

            return Nome;
        }

        /**
         * Função para Formatar Nomes muitos Extensos para somente Nome e Sobrenome
         * */
        public static string UfcFormatarNomeSobrenome(string Nome)
        {
            if (Nome == null)
                return null;

            Nome = Nome.Trim().ToLower();
            //Nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Nome);

            string[] partsNome = Nome.Split(' ');
            int total = partsNome.Length;

            if (total > 1)
            {
                if (partsNome[1] == "de" || partsNome[1] == "da" || partsNome[1] == "do")
                    Nome = partsNome[0] + " " + partsNome[1] + " " + partsNome[total-1];
                else
                    Nome = partsNome[0] + " " + partsNome[total-1];
            }

            Nome = CapitalizarNome(Nome);

            return Nome;
        }

        /**
         * Função para Capitalizar nomes
         * */
        static string CapitalizarNome(string nome)
        {
            string[] excecoes = new string[] { "e", "de", "da", "das", "do", "dos" };
            var palavras = new Queue<string>();
            foreach (var palavra in nome.Split(' '))
            {
                if (!string.IsNullOrEmpty(palavra))
                {
                    var emMinusculo = palavra.ToLower();
                    var letras = emMinusculo.ToCharArray();
                    if (!excecoes.Contains(emMinusculo)) letras[0] = char.ToUpper(letras[0]);
                    palavras.Enqueue(new string(letras));
                }
            }

            return string.Join(" ", palavras);
        }

        /**
         * Função para Validar Nome Social
         * */
        public static string UfcValidaNomeSocial(string NomeAluno, string NomeSocial)
        {
            NomeAluno = NomeAluno.Trim();
            NomeSocial = NomeSocial.Trim();

            string NomeValido;
            if (!string.IsNullOrEmpty(NomeSocial))
                NomeValido = NomeAluno.Equals(NomeSocial) ? NomeAluno : NomeSocial;
            else
                NomeValido = NomeAluno;

            return NomeValido;
        }

        /**
         * Função para Validar Nome Civil
         * */
        public static string UfcValidaNomeSocialNomeCivil(string NomeAluno, string NomeSocial)
        {
            NomeAluno = NomeAluno.Trim();
            NomeSocial = NomeSocial.Trim();

            string NomeValido;
            if (!string.IsNullOrEmpty(NomeSocial))
                NomeValido = NomeAluno.Equals(NomeSocial) ? NomeAluno : string.Concat(NomeSocial, " (Nome Civil: ", NomeAluno, ")");
            else
                NomeValido = NomeAluno;

            return NomeValido;
        }




        /**
         * Função para usar Expressão Regular com Replace
         * */
        public static string UfcRegexReplace(string Texto, string RegEx, string Replace)
        {
            if (string.IsNullOrEmpty(Texto) || string.IsNullOrEmpty(RegEx))
                return "";

            Texto = Regex.Replace(Texto, RegEx, Replace);

            return Texto;
        }

        /**
         * Função para Exibir um Texto, somente se a coluna informada NÃO for (nulo ou vazio)
         * */
        public static string UfcPrint(string Coluna, string Texto)
        {
            return !string.IsNullOrEmpty(Coluna) ? Texto : "";
        }

        /**
         * Função para completar com Máscara à esquerda, caso o Caracter não atinja o limite de Comprimento
         * */
        public static string UfcPadLeft(string Caracter, int Comprimento = 7, char Mascara = '0')
        {
            Caracter = Caracter.PadLeft(Comprimento, Mascara);

            return Caracter;
        }

        /**
         * Função para completar com Máscara à direita, caso o Caracter não atinja o limite de Comprimento
         * */
        public static string UfcPadRight(string Caracter, int Comprimento = 7, char Mascara = '0')
        {
            Caracter = Caracter.PadRight(Comprimento, Mascara);

            return Caracter;
        }



        /**
         * Função para Converter um Valor para o Extenso
         * https://ivanmeirelles.wordpress.com/2012/10/27/escrever-valores-por-extenso-em-c/
         * */
        public static string UfcToExtenso(decimal Valor, bool ExibirCentavos = true)
        {
            if (Valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else if (Valor <= 0)
                return "Zero";
            else
            {
                string strValor = Valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += EscrevaParte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);


                    if (ExibirCentavos)
                    {
                        if (i == 12)
                        {
                            if (valor_por_extenso.Length > 8)
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                    valor_por_extenso += " DE";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                    valor_por_extenso += " DE";
                                else
                                        if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                    valor_por_extenso += " DE";

                            if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                                valor_por_extenso += " REAL";
                            else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                                valor_por_extenso += " REAIS";

                            if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                                valor_por_extenso += " E ";
                        }
                    }

                    if (ExibirCentavos) {
                        if (i == 15)
                            if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                                valor_por_extenso += " CENTAVO";
                            else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                                valor_por_extenso += " CENTAVOS";
                    }
                }

                var valor_capitular = valor_por_extenso.First().ToString().ToUpper() + valor_por_extenso.ToLower().Substring(1);

                return valor_capitular;
            }
        }

        static string EscrevaParte(decimal Valor)
        {
            if (Valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (Valor > 0 & Valor < 1)
                {
                    Valor *= 100;
                }
                string strValor = Valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }



    }
}