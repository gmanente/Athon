using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Coordenacao.BE;
using Sistema.Api.dll.Src.Coordenacao.VO;
using Sistema.Api.dll.Src.Financeiro.BE;
using Sistema.Api.dll.Src.Financeiro.VO;
using Sistema.Api.dll.Src.Financeiro.VO.MatriculaRematriculaVO.SerializacaoVO;
using Sistema.Api.dll.Src.Matricula.VO;
using Sistema.Api.dll.Src.SecretariaAcademica.BE;
using Sistema.Api.dll.Src.SecretariaAcademica.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

#pragma warning disable CS0168, CS0162
namespace Sistema.Api.dll.Repositorio.Util
{
    public static class Funcoes
    {
        private static HttpContext _context;
        private static HttpBrowserCapabilities _browser;


        public static string limparCpf(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");
        }


        public static bool IsValidCpf(string cpf)
        {
            // Limpa a string do CPF de caracteres utilizados na formatação da máscara
            string CPF = cpf.ToString().Trim().Replace(".", String.Empty).Replace("-", String.Empty).Replace("/", String.Empty).Replace(" ", String.Empty);

            // Variáveis utilizadas no cálculo
            int soma1 = 0;
            int soma2 = 0;
            List<string> valoresinvalidos = new List<string>();

            // Lista de CPF's inválidos
            valoresinvalidos.Add("11111111111");
            valoresinvalidos.Add("22222222222");
            valoresinvalidos.Add("33333333333");
            valoresinvalidos.Add("44444444444");
            valoresinvalidos.Add("55555555555");
            valoresinvalidos.Add("66666666666");
            valoresinvalidos.Add("77777777777");
            valoresinvalidos.Add("88888888888");
            valoresinvalidos.Add("99999999999");
            valoresinvalidos.Add("00000000000");

            // Avalia se o CPF possui o tamanho padrão de 11 caracteres e
            // ainda se possui um valor inválido de acordo com a lista de CPF's inválidos
            if (CPF.Length != 11) return false;
            else if (valoresinvalidos.IndexOf(CPF) >= 0) return false;

            // Avalia a existência de caracteres diferentes dos numerais
            // Calcula a 1ª e 2ª soma no mesmo laço
            for (int i = 0; i < CPF.Length; i++)
            {
                if ("0123456789".IndexOf(CPF.Substring(i, 1)) == -1) return false;
                if (i < 9) soma1 += (int.Parse(CPF.Substring(i, 1))) * (10 - i);
                if (i < 10) soma2 += (int.Parse(CPF.Substring(i, 1))) * (11 - i);
            }

            // Calcula e critica o 1º dígito
            if ((soma1 % 11) <= 1)
            {
                if (int.Parse(CPF.Substring(9, 1)) != 0) return false;
            }
            else
            {
                if ((11 - (soma1 % 11)) != (int.Parse(CPF.Substring(9, 1)))) return false;
            }

            // Calcula e critica o 2º dígito
            if ((soma2 % 11) <= 1)
            {
                if (int.Parse(CPF.Substring(10, 1)) != 0) return false;
            }
            else
            {
                if ((11 - (soma2 % 11)) != (int.Parse(CPF.Substring(10, 1)))) return false;
            }

            return true;
        }


        public static string limparCep(string cep)
        {
            return cep.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");
        }


        public static string limparTel(string tel)
        {
            return tel.Replace(".", "").Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", "");
        }


        public static string limparString(string input, string substituirPor = "")
        {
            // Limpa caracteres especiais e remove excesso de espaços
            if (!string.IsNullOrEmpty(input))
                return Regex.Replace(Regex.Replace(input, @"[^a-z^0-9^'áéíóúàèìòùâêîôûãõç\s]", substituirPor, RegexOptions.IgnoreCase), @"\s{2,}", " ");
            else
                return null;
        }


        public static string SetMaskCpf(string cpf)
        {
            string cpfComMascara = string.Empty;
            string[] cpfSeparado;
            cpf = limparCpf(cpf);

            cpfSeparado = Regex.Replace(cpf, ".{3}", "$0,").Split(',');

            for (int i = 0; i < cpfSeparado.Count(); i++)
            {
                if (i == cpfSeparado.Count() - 1)
                {
                    cpfComMascara = cpfComMascara.Substring(0, cpfComMascara.Length - 1);
                    cpfComMascara += "-" + cpfSeparado[i];
                }
                else
                    cpfComMascara += cpfSeparado[i] + ".";
            }

            return cpfComMascara;
        }


        public static string SetMaskCnpj(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }


        public static string SetMaskCep(string cep)
        {
            string cepComMascara = string.Empty;
            string[] cepSeparado = new string[3];
            cep = limparCep(cep);

            cepSeparado[0] = cep.Substring(0, 2);
            cepSeparado[1] = cep.Substring(2, 3);
            cepSeparado[2] = cep.Substring(5, 3);

            for (int i = 0; i < cepSeparado.Count(); i++)
            {
                if (i == cepSeparado.Count() - 1)
                {
                    cepComMascara = cepComMascara.Substring(0, cepComMascara.Length - 1);
                    cepComMascara += "-" + cepSeparado[i];
                }
                else
                    cepComMascara += cepSeparado[i] + ".";
            }

            return cepComMascara;
        }


        public static string SetMaskTel(string tel)
        {
            string telComMascara = string.Empty;
            string[] telSeparado = new string[3];
            tel = limparTel(tel);

            if (tel.PadLeft(1) == "0")
                tel = tel.Substring(1);

            telSeparado[0] = tel.Substring(0, 2);
            if (tel.Substring(2).Length == 8)
            {
                telSeparado[1] = tel.Substring(2, 4);
                telSeparado[2] = tel.Substring(5, 4);
            }
            else
            {
                telSeparado[1] = tel.Substring(2, 5);
                telSeparado[2] = tel.Substring(6, 4);
            }

            for (int i = 0; i < telSeparado.Count(); i++)
            {
                if (i == 0)
                    telComMascara += "(" + telSeparado[i] + ") ";
                else if (i == 1)
                    telComMascara += telSeparado[i] + "-";
                else
                    telComMascara += telSeparado[i];
            }

            return telComMascara;
        }


        public static string limparCpfCnpj(string cpfCnpj)
        {
            return cpfCnpj.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");
        }


        public static string Capitalize(string text, bool ignorePreposi = true)
        {
            var arr = text.Trim().Split(' ');
            string newstr = "";
            foreach (var item in arr)
            {
                if (ignorePreposi && (item.ToLower().Equals("de")
                    || item.ToLower().Equals("da")
                    || item.ToLower().Equals("das")
                    || item.ToLower().Equals("do")
                    || item.ToLower().Equals("dos")
                    || item.ToLower().Equals("no")
                    || item.ToLower().Equals("na")
                    || item.ToLower().Equals("em")
                    ))
                {
                    newstr += item.ToLower() + " ";
                }
                else
                {
                    var fisrt = item.Substring(0, 1).ToUpper();
                    newstr += fisrt + item.Substring(1).ToLower() + " ";
                }
            }
            string result = newstr.Substring(0, newstr.Length - 1);
            return result;
        }


        public static string limparTelefone(string telefone)
        {
            return telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        }


        public static byte[] ReadAllBytes(BinaryReader reader)
        {
            const int bufferSize = 32768;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }
        }


        public static string RemoveCaracteresEspeciais(string texto, bool aceitaEspaco, bool substituiAcentos)
        {
            string ret = texto;

            if (String.IsNullOrEmpty(ret))
                return ret;

            if (substituiAcentos)
                ret = RemoveAcentos(ret);

            if (aceitaEspaco)
                ret = Regex.Replace(ret,
                    @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", String.Empty);
            else
                ret = Regex.Replace(ret,
                    @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", String.Empty);

            return ret;
        }


        public static string RemoveAcentos(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
                sb.Append(s_Accents[text[i]]);

            return sb.ToString();
        }


        private static readonly char[] s_Accents = GetAccents();

        private static char[] GetAccents()
        {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] =
                accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] =
                accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] =
                accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] =
                accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';

            return accents;
        }


        //Chache handler
        public static int CacheHandler()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 9999);
            return randomNumber;
        }


        //UrlRepositorio
        public static string InvocarTagArquivo(string caminho, bool? isLocal = false, bool noCache = false)
        {
            var returnString = "";

            //Extensão do arquivo
            var extensaoArquivo = caminho.Split(Convert.ToChar(".")).Last();

            // No Cache do arquivo
            if (noCache == true)
            {
                caminho = caminho + "?nocache=" + CacheHandler();
            }
            else if (isLocal == true)
            {
                string dirpath = AppDomain.CurrentDomain.BaseDirectory + caminho;

                DateTime dataModificacao = File.GetLastWriteTime(dirpath);

                caminho += "?cache=" + dataModificacao.ToString("yyyyMMdd-HHmmss");
            }

            if (!(bool)isLocal && !noCache)
            {
                try
                {
                    string appCurrent = AppDomain.CurrentDomain.BaseDirectory;

                    string dirpath = string.Concat(appCurrent.Substring(0, appCurrent.IndexOf("Sistema.")), "Sistema.Web.UI.Repositorio\\", caminho);
                    DateTime dataModificacao = File.GetLastWriteTime(dirpath);

                    caminho += "?cache=" + dataModificacao.ToString("yyyyMMdd-HHmmss");
                }
                catch { }

            }

            if (isLocal == false)
            {
                returnString = GetUrlRepositorio() + caminho;
            }
            else
            {
                if (Dominio.AppState == Dominio.ApplicationState.Debug || Dominio.AppState == Dominio.ApplicationState.Producao)
                {
                    returnString = "//" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/" + caminho;
                }
                else
                {
                    var url = HttpContext.Current.Request.Url.ToString().Split('/');
                    returnString = url[0] + "//" + url[2] + "/" + caminho;

                }
            }





            //Adiciona marcação para gerenciamento de chache
            //    returnString = returnString + "?nocache=" + CacheHandler();

            //Montar a tag de chamada de acordo com a estensão
            //Javascript
            if (extensaoArquivo == "js")
            {
                returnString = "<script type='text/javascript' charset='utf-8' src='" + returnString + "'></script>";
            }
            //Cascading style sheet
            else if (extensaoArquivo == "css")
            {
                returnString = "<link rel='stylesheet' href='" + returnString + "'/>";
            }
            return returnString + "\n\t";
        }


        public static string GetUrlRepositorio()
        {
            // Regras para definição de URL do arquivo
            var _url = Switch.On(Dominio.AppState)
            .Case(Dominio.ApplicationState.Debug).Then(Dominio.UrlRepositorioLocal)
            .Case(Dominio.ApplicationState.Teste).Then(Dominio.UrlRepositorioTeste)
            .Case(Dominio.ApplicationState.Homologacao).Then(Dominio.UrlRepositorioHomologacao)
            .Default(Dominio.UrlRepositorioRemoto);

            return _url;
        }


        public static string GerarContrato(string htmlText, string filePrefix = null, string[] options = null)
        {
            try
            {
                if (options == null)
                    options = new string[] { "-L 8mm", "-R 8mm", "-T 8mm", "-B 8mm" };
                var pdfUrl = PdfGenerator.HtmlToPdf(pdfOutputLocation: "~/Temp/",
                    outputFilenamePrefix: filePrefix ?? "GeneratedPDF_",
                   htmlString: htmlText, options: options);

                return pdfUrl;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public static string ReplaceDocumento(string documento, MatriculaVO matricula, SqlCommand sqlComm = null)
        {
            string documentoReplaced = "";
            DateTime dataAtual = DateTime.Now;
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            decimal valorParcela = matricula.AproveitamentoEstudo != null && matricula.AproveitamentoEstudo.ValorTotalPagar > 0 ? matricula.AproveitamentoEstudo.ValorTotalPagar : matricula.Curso.CursoMensalidade.ValorMensalidade;

            int qtdParcelas = matricula.Curso.CursoMensalidade.QuantidadeParcela;

            if (qtdParcelas <= 0)
                throw new Exception("Não foi encontrada quantidade de parcelas para a mensalidade do curso selecionado.\nFavor entrar em contato com o Financeiro.");

            decimal valorSemestral = valorParcela * qtdParcelas;
            try
            {
                if (matricula.Campus.Nome.Contains("Famec") || matricula.Campus.Nome.Contains("FAMEC"))
                {
                    documentoReplaced = documento.Replace("[PreposicaoDo]", "das");
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Univag]", "FAMEC - Faculdades Metropolitanas de Cuiabá");
                    documento = documentoReplaced;
                }
                else
                {
                    documentoReplaced = documento.Replace("[PreposicaoDo]", "do");
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Univag]", "UNIVAG - Centro Universitário");
                    documento = documentoReplaced;
                }

                documentoReplaced = documento.Replace("[QtdParcelas]", qtdParcelas.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QtdParcelasExtenso]", Funcoes.EscreverParte(qtdParcelas));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[DiaVencimento]", matricula.Curso.CursoMensalidade.DiaVencimento.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcelaCorrigido]", string.Format("{0:c2}", valorParcela));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcelaCorrigidoExtenso]", Funcoes.DecimalToExtenso(valorParcela));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivo]", matricula.PeriodoLetivoModalidade.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[AnoPeriodoLetivo]", matricula.PeriodoLetivoModalidade.DataInicioCalendario.Value.Year.ToString());
                documento = documentoReplaced;


                if (matricula.Curso.CursoMensalidade.ValorMensalidade > 0)
                {
                    documentoReplaced = documento.Replace("[ValorMensalidade]", "R$ " + matricula.Curso.CursoMensalidade.ValorMensalidade.ToString("N2"));
                    documento = documentoReplaced;
                }
                else
                    throw new Exception("Não foi encontrado o Valor da Mensalidade.\nFavor entrar em contato com o Financeiro.");


                documentoReplaced = documento.Replace("[PeriodoLetivoInicio]", matricula.PeriodoLetivoModalidade.DataInicioContrato.Value.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoFim]", Convert.ToDateTime(matricula.PeriodoLetivoModalidade.DataTerminoContrato).ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorSemestral]", string.Format("{0:c2}", valorSemestral));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcela]", string.Format("{0:c2}", valorParcela));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorSemestralExtenso]", Funcoes.DecimalToExtenso(valorSemestral));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcelaExtenso]", Funcoes.DecimalToExtenso(valorParcela));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Matricula]", matricula.Matricula.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Campus]", matricula.Campus.Nome.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Curso]", matricula.Curso.Descricao.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Turno]", matricula.Turno.Descricao.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nome]", matricula.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cpf]", Funcoes.SetMaskCpf(matricula.Cpf));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Rg]", matricula.MatriculaDadosPessoais.Rg);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[RgOrgaoEmissor]", matricula.MatriculaDadosPessoais.RgOrgaoEmissor);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[UfRgOrgaoEmissor]", matricula.MatriculaDadosPessoais.RgUf.Sigla);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[RgDataEmissao]", matricula.MatriculaDadosPessoais.RgData.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Endereco]", matricula.MatriculaContatoEndereco.Endereco);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Bairro]", matricula.MatriculaContatoEndereco.Bairro);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Estado]", matricula.MatriculaContatoEndereco.Estado.Sigla);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cidade]", matricula.MatriculaContatoEndereco.Cidade.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cep]", Funcoes.SetMaskCep(matricula.MatriculaContatoEndereco.Cep));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[EstadoCivil]", matricula.MatriculaDadosPessoais.EstadoCivil.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nacionalidade]", matricula.MatriculaDadosPessoais.Nacionalidade.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[UsuarioImpressao]", matricula.Usuario.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[NomeDoPolo]", matricula.Campus.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[NrMatricula]", matricula.Matricula);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[FoneCelular]", SetMaskTel(matricula.MatriculaContatoEndereco.TelCelular1));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[EmailAluno]", matricula.MatriculaContatoEndereco.Email1);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[DataMatricula]", string.Concat(dataAtual.Day, " de ", culture.TextInfo.ToTitleCase(dtfi.GetMonthName(dataAtual.Month)), " de ", dataAtual.Year));
                documento = documentoReplaced;

                if (!string.IsNullOrEmpty(matricula.MatriculaResponsavel.Cpf))
                {
                    var resp = @"neste ato representado por seu representante legal " + matricula.MatriculaResponsavel.Nome + " " + matricula.MatriculaResponsavel.Nacionalidade.Descricao + " com CPF: " + Funcoes.SetMaskCpf(matricula.MatriculaResponsavel.Cpf);

                    documentoReplaced = documento.Replace("[Responsavel]", resp);
                    documento = documentoReplaced;

                }
                else
                {
                    documentoReplaced = documento.Replace("[Responsavel]", "");
                    documento = documentoReplaced;
                }

                if (matricula.Convenio.Id == 31)
                {
                    documento = ParcelamentoUnivagQuadroResumno(documento, matricula, sqlComm);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documento;
        }


        public static string ReplaceDocumento(string documento, AlunoDadosImpressaoVO objVO, PeriodoLetivoModalidadeVO periodoModalidade, SqlCommand sqlComm = null)
        {
            PeriodoLetivoBE periodoLetivoBE = null;
            string documentoReplaced = "";
            DateTime dataAtual = DateTime.Now;
            DateTime dataInicioContrato = Convert.ToDateTime(periodoModalidade.DataInicioContrato);
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            decimal valorSemestral = Math.Round(objVO.ValorSemestral, 2);
            decimal valorParcela = Math.Round(objVO.ValorParcela, 2);

            if (valorParcela <= 0)
                throw new Exception("Não foi encontrad o valor da parcela para o curso selecionado.\nFavor entrar em contato com o Financeiro.");

            if (valorSemestral == 0)
            {
                valorSemestral = Math.Round(objVO.Curso.CursoMensalidade.ValorMensalidade * objVO.Curso.CursoMensalidade.QuantidadeParcela, 2);
                valorParcela = Math.Round(objVO.Curso.CursoMensalidade.ValorMensalidade, 2);
            }

            int qtdParcelas = objVO.Curso.CursoMensalidade.QuantidadeParcela;

            if (qtdParcelas <= 0)
                throw new Exception("Não foi encontrada quantidade de parcelas para a mensalidade do curso selecionado.\nFavor entrar em contato com o Financeiro.");

            int qtdParcelasAux = qtdParcelas;
                        
            decimal valorParcelaCorrigido = valorParcela;

            if (!objVO.Fies && !objVO.ParcelamentoUnivagMais)
            {
                if (periodoModalidade.PeriodoLetivo.DataInicio.Value.Year >= 2017)
                {
                    if (dataAtual.Year == periodoModalidade.PeriodoLetivo.DataInicio.Value.Year)
                    {
                        qtdParcelasAux = periodoModalidade.DataTerminoContrato.Value.Month - dataAtual.Month;
                        qtdParcelas = qtdParcelasAux + 1;
                    }

                    qtdParcelasAux = qtdParcelasAux < 1 ? 1 : qtdParcelasAux;
                    valorParcelaCorrigido = (valorSemestral - valorParcela) / qtdParcelasAux;
                }
            }

            try
            {

                periodoLetivoBE = new PeriodoLetivoBE(sqlComm);
                var idPeriodoLetivo = Convert.ToInt64(Dominio.GetParametro(Dominio.Financeiro.PeriodoLetivoMatriculaRematricula, objVO.Aluno.Campus.Id).Valor);
                var periodoLetivo = periodoLetivoBE.Consultar(new PeriodoLetivoVO() { Id = idPeriodoLetivo });
                objVO.PeriodoLetivo = periodoLetivo;

                documentoReplaced = documento.Replace("[QtdParcelas]", qtdParcelas.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QtdParcelasExtenso]", Funcoes.EscreverParte(qtdParcelas));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcelaCorrigido]", string.Format("{0:c2}", valorParcelaCorrigido));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcelaCorrigidoExtenso]", Funcoes.DecimalToExtenso(valorParcelaCorrigido));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoInicio]", dataInicioContrato.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoFim]", Convert.ToDateTime(periodoModalidade.DataTerminoContrato).ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorSemestral]", string.Format("{0:c2}", valorSemestral));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcela]", string.Format("{0:c2}", valorParcela));
                documento = documentoReplaced;

                if (objVO.Curso.CursoMensalidade.ValorMensalidade > 0)
                {
                    documentoReplaced = documento.Replace("[ValorMensalidade]", string.Format("{0:c2}", objVO.Curso.CursoMensalidade.ValorMensalidade));
                    documento = documentoReplaced;
                }
                else
                    throw new Exception("Não foi encontrado valor para a mensalidade do curso selecionado.\nFavor entrar em contato com o Financeiro.");

                documentoReplaced = documento.Replace("[ValorSemestralExtenso]", Funcoes.DecimalToExtenso(valorSemestral));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorParcelaExtenso]", Funcoes.DecimalToExtenso(valorParcela));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Matricula]", objVO.Aluno.Matricula);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Campus]", objVO.Aluno.Campus.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivo]", objVO.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Turma]", objVO.GradeLetivaTurma.Sigla);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Modalidade]", objVO.GradeLetivaTurma.GradeLetiva.GradeConsepe.Modalidade.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Semestre]", objVO.Aluno.AlunoSemestre.GradeLetivaSemestre.GradeConsepeSemestre.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Curso]", objVO.Curso.Descricao.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Turno]", objVO.Turno.Descricao.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nome]", objVO.DadoPessoal.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cpf]", objVO.DadoPessoal.Cpf);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Rg]", objVO.DadoPessoal.RgNumero);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[NrMatricula]", objVO.Aluno.Matricula);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nascimento]", objVO.DadoPessoal.DataNascimento.Value.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[DataNascimento]", objVO.DadoPessoal.DataNascimento.Value.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Naturalidade]", objVO.DadoPessoal.Cidade.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[NomePai]", objVO.DadoPessoal.NomePai);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[NomeMae]", objVO.DadoPessoal.NomeMae);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[TituloEleitor]", objVO.DadoPessoal.TituloEleitor);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[CertificadoMilitar]", objVO.DadoPessoal.CertificadoMilitar);
                documento = documentoReplaced;

                if (objVO.DadoPessoal.DadoPessoalVestibular.DataVestibular != null)
                {
                    documentoReplaced = documento.Replace("[AnoVestibular]", objVO.DadoPessoal.DadoPessoalVestibular.DataVestibular.Value.Year.ToString("dd/MM/yyyy"));
                    documento = documentoReplaced;
                }
                else
                {
                    documentoReplaced = documento.Replace("[AnoVestibular]", "");
                    documento = documentoReplaced;
                }

                if (objVO.DadoPessoal.DadoPessoalVestibular.NotaVestibular != null)
                {
                    documentoReplaced = documento.Replace("[NotaVestibular]", objVO.DadoPessoal.DadoPessoalVestibular.NotaVestibular);
                    documento = documentoReplaced;
                }
                else
                {
                    documentoReplaced = documento.Replace("[NotaVestibular]", "");
                    documento = documentoReplaced;
                }

                documentoReplaced = documento.Replace("[Sexo]", objVO.DadoPessoal.Sexo.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Telefone]", objVO.DadoPessoal.DadoPessoalTelefone.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[FoneCelular]", Funcoes.SetMaskTel(objVO.DadoPessoal.Celular.Descricao));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[EmailAluno]", objVO.DadoPessoal.DadoPessoalEnderecoDigital.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[RgOrgaoEmissor]", objVO.DadoPessoal.RgOrgao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[UfRgOrgaoEmissor]", objVO.DadoPessoal.RgOrgaoEstado.Sigla);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[RgDataEmissao]", objVO.DadoPessoal.RgDataEmissao.Value.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                string complemento = !string.IsNullOrEmpty(objVO.DadoPessoal.DadoPessoalEndereco.Complemento) ? ", " + objVO.DadoPessoal.DadoPessoalEndereco.Complemento : "";
                documentoReplaced = documento.Replace("[Endereco]", objVO.DadoPessoal.DadoPessoalEndereco.Logradouro + ", N° " + objVO.DadoPessoal.DadoPessoalEndereco.Numero + complemento);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Bairro]", objVO.DadoPessoal.DadoPessoalEndereco.Bairro);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Estado]", objVO.DadoPessoal.DadoPessoalEndereco.Cidade.Estado.Sigla);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cidade]", objVO.DadoPessoal.DadoPessoalEndereco.Cidade.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cep]", objVO.DadoPessoal.DadoPessoalEndereco.Cep);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[EstadoCivil]", objVO.DadoPessoal.EstadoCivil.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nacionalidade]", objVO.DadoPessoal.Nacionalidade.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[UsuarioImpressao]", objVO.Usuario.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[DataMatricula]", string.Concat(dataAtual.Day, " de ", culture.TextInfo.ToTitleCase(dtfi.GetMonthName(dataAtual.Month)), " de ", dataAtual.Year));
                documento = documentoReplaced;

                if (objVO.Resaponsavel != null && !string.IsNullOrEmpty(objVO.Resaponsavel.Cpf))
                {
                    var resp = @"neste ato representado por seu representante legal " + objVO.Resaponsavel.Nome + " " + objVO.Resaponsavel.Nacionalidade.Descricao +
                                                                  " com CPF: " + objVO.Resaponsavel.Cpf;

                    documentoReplaced = documento.Replace("[Responsavel]", resp);
                    documento = documentoReplaced;

                }
                else
                {
                    documentoReplaced = documento.Replace("[Responsavel]", "");
                    documento = documentoReplaced;
                }

                if (objVO.CursoMensalidadeDesconto != null && objVO.CursoMensalidadeDesconto.TaxaAdesao > 0)
                {
                    decimal valorTaxa = Math.Round(((objVO.CursoMensalidadeDesconto.TaxaAdesao / 100) * objVO.ValorParcela), 2);

                    documentoReplaced = documento.Replace("[ValorTaxaExtenso]", Funcoes.DecimalToExtenso(valorTaxa));
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[ValorTaxa]", string.Format("{0:c}", valorTaxa));
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[PercentualExtenso]", Funcoes.EscreverParte(objVO.CursoMensalidadeDesconto.Percentual));
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Percentual]", Convert.ToString(objVO.CursoMensalidadeDesconto.Percentual));
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[PercentualTaxa]", Convert.ToString(objVO.CursoMensalidadeDesconto.TaxaAdesao));
                    documento = documentoReplaced;
                }

                string texto = "";
                if (objVO.ConvenioPreCadastro != null && objVO.ConvenioPreCadastro.Id > 0)
                {
                    texto = "<b>a)</b> ";
                    if (objVO.ConvenioPreCadastro.Professor)
                    {
                        texto += " Em sendo o <b>CONTRATANTE</b> colaborador <b>DOCENTE</b> da <b>CONTRATADA</b>, terá " + objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual.ToString() + "% (" + Funcoes.EscreverPorcentagemMenorZero(objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual) + ") de desconto para pagamento até o dia 5 (cinco) de cada mês.";
                    }
                    else
                    {
                        if (objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresa.Id == 6)
                        {
                            texto += " Em sendo o <b>CONTRATANTE</b> aluno (a) <b>EGRESSO</b> (A) da <b>CONTRATADA</b>, terá " + objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual.ToString() + "% (" + Funcoes.EscreverPorcentagemMenorZero(objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual) + ") de desconto para pagamento até o dia 5 (cinco) de cada mês.";
                        }
                        else if (objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresa.Regra.Id == 15)
                        {
                            texto += " Em sendo o <b>CONTRATANTE</b> colaborador integrante da <b>CARREIRA TÉCNICO – ADMINISTRATIVA</b> da <b>CONTRATADA</b>, terá " + objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual + "% (" + Funcoes.EscreverPorcentagemMenorZero(objVO.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual) + ") de desconto para pagamento até o dia 10 (dez) de cada mês.";
                        }
                        else
                        {
                            texto = "";
                        }
                    }
                }

                if (objVO.ParcelamentoUnivagMais)
                {
                    documento = ParcelamentoUnivagQuadroResumnoConfiguracao(documento, objVO, sqlComm);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (periodoLetivoBE != null && sqlComm == null)
                    periodoLetivoBE.FecharConexao();
            }

            return documento;
        }


        public static string ReplaceDocumentoMatriculaOnline(string documento, AlunoMatriculaOnlineVO alunoMatricula)
        {
            string documentoReplaced = "";
            DateTime dataAtual = DateTime.Now;
            //decimal valorParcela = matricula.AproveitamentoEstudo != null && matricula.AproveitamentoEstudo.ValorTotalPagar > 0 ? matricula.AproveitamentoEstudo.ValorTotalPagar : matricula.Curso.CursoMensalidade.ValorMensalidade;
            decimal valorParcela = alunoMatricula.Aluno.Curso.CursoMensalidade.ValorMensalidade;

            int qtdParcelas = alunoMatricula.Aluno.Curso.CursoMensalidade.QuantidadeParcela;

            decimal valorSemestral = valorParcela * qtdParcelas;
            valorSemestral += alunoMatricula.TaxaPreMatricula;

            DateTime dataRematricula = DateTime.Now;
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            try
            {
                if (alunoMatricula.Aluno.Campus.Nome.Contains("Famec") || alunoMatricula.Aluno.Campus.Nome.Contains("FAMEC"))
                {
                    documentoReplaced = documento.Replace("[PreposicaoDo]", "das");
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Univag]", "FAMEC - Faculdades Metropolitanas de Cuiabá");
                    documento = documentoReplaced;
                }
                else
                {
                    documentoReplaced = documento.Replace("[PreposicaoDo]", "do");
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Univag]", "UNIVAG - Centro Universitário");
                    documento = documentoReplaced;
                }

                documentoReplaced = documento.Replace("[DataMatricula]", $"{dataRematricula.Day} de {culture.TextInfo.ToTitleCase(dtfi.GetMonthName(dataRematricula.Month))} de {dataRematricula.Year}");
                documento = documentoReplaced;

                if (string.IsNullOrEmpty(alunoMatricula.Aluno.AlunoPos.Profissao))
                    throw new ArgumentException("O campo Profissão não foi preenchido. Favor preenche-lo para dar continuidade no procedimento.");

                documentoReplaced = documento.Replace("[Profissao]", alunoMatricula.Aluno.AlunoPos.Profissao);
                documento = documentoReplaced;

                if (alunoMatricula.PercentualMultaDesistencia <= 0)
                    throw new ArgumentException("Não foi encontrato o Percentual de Multa da Desistência. Favor entrar em contato com NTIC.");

                documentoReplaced = documento.Replace("[PercentualMultaDesistencia]", (alunoMatricula.PercentualMultaDesistencia / 100).ToString("P"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualMultaDesistenciaExtenso]", EscreverPorcentagemMenorZero(alunoMatricula.PercentualMultaDesistencia));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QtdParcelas]", qtdParcelas.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QtdParcelasExtenso]", EscreverParte(qtdParcelas));
                documento = documentoReplaced;

                if (alunoMatricula.Aluno.GradeConsepeSemestre.GradeConsepe.TotalMesIntegralizar <= 0)
                    throw new ArgumentException("A quantidade de mêses que o curso possui não foi encontrada. Favor entrar em contato com a Secretaria Acadêmica.");

                documentoReplaced = documento.Replace("[QtdMesesCurso]", alunoMatricula.Aluno.GradeConsepeSemestre.GradeConsepe.TotalMesIntegralizar.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QtdMesesCursoExtenso]", EscreverParte((alunoMatricula.Aluno.GradeConsepeSemestre.GradeConsepe.TotalMesIntegralizar)));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[DiaVencimento]", alunoMatricula.Aluno.Curso.CursoMensalidade.DiaVencimento.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[DiaVencimentoExtenso]", EscreverParte(alunoMatricula.Aluno.Curso.CursoMensalidade.DiaVencimento));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivo]", alunoMatricula.Aluno.AlunoSemestre.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorMensalidade]", $"R$ {alunoMatricula.Aluno.Curso.CursoMensalidade.ValorMensalidade:N2}");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorMensalidadeExtenso]", DecimalToExtenso(alunoMatricula.Aluno.Curso.CursoMensalidade.ValorMensalidade));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoInicio]", alunoMatricula.Aluno.AlunoSemestre.PeriodoLetivo.PeriodoLetivoModalidade.DataInicioContrato.Value.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoFim]", Convert.ToDateTime(alunoMatricula.Aluno.AlunoSemestre.PeriodoLetivo.PeriodoLetivoModalidade.DataTerminoContrato).ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorCurso]", string.Format("{0:c2}", valorSemestral));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorCursoExtenso]", DecimalToExtenso(valorSemestral));
                documento = documentoReplaced;

                if (alunoMatricula.TaxaPreMatricula <= 0)
                    throw new ArgumentException("Não foi encontrato o valor da Taxa de Matrícula. Favor entrar em contato com NTIC.");

                documentoReplaced = documento.Replace("[ValorTaxaMatricula]", string.Format("{0:c2}", alunoMatricula.TaxaPreMatricula));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorTaxaMatriculaExtenso]", DecimalToExtenso(alunoMatricula.TaxaPreMatricula));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Matricula]", alunoMatricula.Aluno.Matricula.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Campus]", alunoMatricula.Aluno.Campus.Nome.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Curso]", alunoMatricula.Aluno.Curso.Descricao.Trim().ToUpper());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Modalidade]", alunoMatricula.Aluno.GradeConsepeSemestre.GradeConsepe.Modalidade.Descricao.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Turno]", alunoMatricula.Aluno.GradeLetivaTurma.GradeLetivaTurno.Turno.Descricao.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Turma]", alunoMatricula.Aluno.GradeLetivaTurma.Sigla.Trim());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nome]", alunoMatricula.Aluno.DadoPessoal.Nome);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Cpf]", SetMaskCpf(alunoMatricula.Aluno.DadoPessoal.Cpf));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Rg]", alunoMatricula.Aluno.DadoPessoal.RgNumero);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[RgOrgaoEmissor]", alunoMatricula.Aluno.DadoPessoal.RgOrgao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[UfRgOrgaoEmissor]", alunoMatricula.Aluno.DadoPessoal.RgOrgaoEstado.Sigla);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[RgDataEmissao]", alunoMatricula.Aluno.DadoPessoal.RgDataEmissao.Value.ToString("dd/MM/yyyy"));
                documento = documentoReplaced;

                if (alunoMatricula.Aluno.DadoPessoal.ListaEnderecos.Count > 0)
                {
                    DadoPessoalEnderecoVO endereco = new DadoPessoalEnderecoVO();
                    if (alunoMatricula.Aluno.DadoPessoal.ListaEnderecos.Any(x => x.EnderecoPrincipal == true))
                        endereco = alunoMatricula.Aluno.DadoPessoal.ListaEnderecos.FirstOrDefault(x => x.EnderecoPrincipal == true);
                    else
                        endereco = alunoMatricula.Aluno.DadoPessoal.ListaEnderecos.FirstOrDefault();

                    documentoReplaced = documento.Replace("[Endereco]", $"{endereco.Logradouro}, {endereco.Numero}");
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Bairro]", endereco.Bairro);
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Estado]", endereco.Cidade.Estado.Sigla);
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Cidade]", endereco.Cidade.Nome);
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[Uf]", endereco.Cidade.Estado.Sigla);
                    documento = documentoReplaced;

                    string cep = "Não informado";
                    if (!string.IsNullOrEmpty(endereco.Cep))
                        cep = Funcoes.SetMaskCep(endereco.Cep);

                    documentoReplaced = documento.Replace("[Cep]", cep);
                    documento = documentoReplaced;
                }
                else
                {
                    throw new ArgumentException("Os dados do endereço não estão preenchidos. Favor preenche-lo para dar continuidade no procedimento.");
                }

                documentoReplaced = documento.Replace("[EstadoCivil]", alunoMatricula.Aluno.DadoPessoal.EstadoCivil.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[Nacionalidade]", alunoMatricula.Aluno.DadoPessoal.Nacionalidade.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[UsuarioImpressao]", alunoMatricula.Aluno.Usuario.Nome);
                documento = documentoReplaced;

                string telefone = "Não informado";
                if (alunoMatricula.Aluno.DadoPessoal.ListaTelefones.Any())
                {
                    telefone = "";
                    foreach (var item in alunoMatricula.Aluno.DadoPessoal.ListaTelefones)
                    {
                        telefone += $"<strong>{item.DadoPessoalTelefoneTipo.Nome}:</strong> {SetMaskTel(item.Descricao)} ";
                    }
                }

                documentoReplaced = documento.Replace("[Telefone]", telefone);
                documento = documentoReplaced;

                string email = "Não informado";
                if (alunoMatricula.Aluno.DadoPessoal.ListaEnderecosDigitais.Any())
                    email = alunoMatricula.Aluno.DadoPessoal.ListaEnderecosDigitais.FirstOrDefault().Descricao;

                documentoReplaced = documento.Replace("[Email]", email);
                documento = documentoReplaced;

                string resp = "";
                if (!string.IsNullOrEmpty(alunoMatricula.Aluno.AlunoResponsavel.Responsavel.Cpf))
                    resp = $"neste ato representado por seu representante legal {alunoMatricula.Aluno.AlunoResponsavel.Responsavel.Nome} {alunoMatricula.Aluno.AlunoResponsavel.Responsavel.Nacionalidade.Descricao} com CPF: {Funcoes.SetMaskCpf(alunoMatricula.Aluno.AlunoResponsavel.Responsavel.Cpf)}";

                documentoReplaced = documento.Replace("[Responsavel]", resp);
                documento = documentoReplaced;

                string texto = "";
                if (alunoMatricula.ConvenioPreCadastro != null && alunoMatricula.ConvenioPreCadastro.Id > 0)
                {
                    texto = "<b>a)</b> Em sendo o <b>CONTRATANTE</b> ";
                    if (alunoMatricula.ConvenioPreCadastro.Professor)
                    {
                        texto += $"colaborador <b>DOCENTE</b> da <b>CONTRATADA</b>, ter&aacute; {alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual}% ({Funcoes.EscreverPorcentagemMenorZero(alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual)}) de desconto para pagamento at&eacute; o dia 10 (dez) de cada m&ecirc;s.";
                    }
                    else
                    {
                        if (alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresa.Id == 6)
                        {
                            texto += $"colaborador integrante da <b>CARREIRA T&Eacute;CNICO – ADMINISTRATIVA</b> da <b>CONTRATADA</b>, ter&aacute; {alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual}% ({Funcoes.EscreverPorcentagemMenorZero(alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual)}) de desconto para pagamento at&eacute; o dia 10 (dez) de cada m&ecirc;s.";
                        }
                        else if (alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresa.Regra.Id == 15)
                        {
                            texto += $"aluno (a) <b>EGRESSO</b> (A) da <b>CONTRATADA</b>, ter&aacute; {alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual}% ({Funcoes.EscreverPorcentagemMenorZero(alunoMatricula.ConvenioPreCadastro.ConvenioEmpresaAluno.ConvenioEmpresaAlunoRenovacao.Percentual)}) de desconto para pagamento at&eacute; o dia 10 (dez) de cada m&ecirc;s.";
                        }
                        else
                        {
                            texto = "";
                        }
                    }
                }

                documentoReplaced = documento.Replace("[DescontoPreCadastro]", texto);
                documento = documentoReplaced;

                if (documento.Contains("[ChaveValidacao]") && string.IsNullOrEmpty(alunoMatricula.ChaveValidacao))
                    throw new Exception("Não foi gerada uma Chave de Validação do Contrato para o documento.");

                documentoReplaced = documento.Replace("[ChaveValidacao]", alunoMatricula.ChaveValidacao);
                documento = documentoReplaced;
            }
            catch (ArgumentException argEx)
            {
                throw argEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documento;
        }


        public static string ParcelamentoUnivagQuadroResumno(string documento, MatriculaVO matricula, SqlCommand sqlComm = null)
        {
            decimal valorPrimeiraParcela = 0;

            ParcelamentoUnivagBE parcelamentoUnivagBE = null;

            try
            {
                parcelamentoUnivagBE = new ParcelamentoUnivagBE(sqlComm);

                ParcelamentoUnivagConfiguracaoVO parcelamentoUnivagConfiguracao = parcelamentoUnivagBE.CarregarConfiguracao(matricula.Aluno, matricula.IdParcelamentoUnivagTipo, matricula.AproveitamentoEstudo);

                StringBuilder tabela = new StringBuilder();
                tabela.Append(@"<style>
                                    table {
                                        border-collapse: collapse;
                                    }

                                    td p {
                                        margin-top:0;
                                        margin-bottom:0;
                                    }

                                    .border {
                                        border: 1px solid black;
                                    }
                                    .border-left {
                                        border-left: 1px solid black;
                                    }
                                    .border-right {
                                        border-right: 1px solid black;
                                    }
                                    .border-bottom {
                                        border-bottom: 1px solid black;
                                    }
                                </style>");

                tabela.Append("<table border='0' cellspacing='0' style='width: 100%; margin-top:30px; margin-bottom:10px; border: 1px solid #000; text-align: center; vertical-align: text-top;'>");
                tabela.Append("    <thead> ");
                tabela.Append("        <tr>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENSALIDADE ESCOLAR DO CURSO " + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.4)" : "(Item I.7)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENS.ESCOLAR COM BENEFICIO INCLUSÃO " + String.Format("{0:0.00}", parcelamentoUnivagConfiguracao.PercentualDesconto) + @"% DESC. " + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.6)" : "(Item I.9)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENSALIDADE UNIVAG MAIS " + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.9)" : " TRANSF. (Item I.12)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left border-bottom' colspan='" + parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.Count() + @"'>SEMESTRE</th>");
                tabela.Append("        </tr>");
                tabela.Append("    </thead>");

                tabela.Append("<tbody>");
                tabela.Append("    <tr> ");
                tabela.Append("        <th class='border-right border-left'></th>");
                tabela.Append("        <th class='border-right border-left'></th>");
                tabela.Append("        <th class='border-right border-left'></th>");

                int contSemestreAux = 0;
                int semestre = 0;
                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    semestre = item.Semestre;
                    if (matricula.AproveitamentoEstudo != null && matricula.AproveitamentoEstudo.Id > 0)
                    {
                        semestre = matricula.AproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                        contSemestreAux += 1;
                    }
                    tabela.Append("<th class='border' style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;'>" + semestre + "</th>");
                }

                tabela.Append("</tr>");
                tabela.Append("<tr>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'></td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'></td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left border-bottom'>" + matricula.PeriodoLetivo.DataInicio.Value.Year + "</td>");

                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    if (valorPrimeiraParcela == 0)
                        valorPrimeiraParcela = item.ValorParcela;

                    tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border'>" + String.Format("{0:0.00}", item.PercentualCobranca) + "%</td>");
                }

                decimal valorCemPorcentoComDesconto = Math.Round(parcelamentoUnivagConfiguracao.ValorCemPorcento * (1 - (parcelamentoUnivagConfiguracao.PercentualDesconto / 100)), 2);
                tabela.Append("</tr>");
                tabela.Append("<tr>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + parcelamentoUnivagConfiguracao.ValorMensalidade.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + parcelamentoUnivagConfiguracao.ValorComDesconto.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + valorCemPorcentoComDesconto.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold; vertical-align: text-top !important;' class='border'>" + valorPrimeiraParcela.ToString("N2") + @"</td>");

                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    if (item.Semestre > 1)
                        tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border'><p>" + item.ValorParcela.ToString("N2") + "</p><p>+</p><p>Reajuste</p></td>");
                }

                tabela.Append("</tr>");
                tabela.Append("    </tbody>");
                tabela.Append("</table>");

                contSemestreAux = 0;
                semestre = 0;
                string paragrafos = "";
                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    semestre = item.Semestre;
                    if (item.Semestre == 1)
                    {
                        if (matricula.AproveitamentoEstudo != null && matricula.AproveitamentoEstudo.Id > 0)
                        {
                            semestre = matricula.AproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }
                        paragrafos += "<p style='text-align:justify'>";
                        paragrafos += "<span>";
                        paragrafos += "<strong>Par&aacute;grafo " + EscreverParteOrdinal(item.Semestre).ToLower().Trim() + ":</strong>";
                        paragrafos += "&nbsp;Para o " + semestre + ".&ordm; semestre o <strong>ALUNO</strong> pagar&aacute; a <strong>IES</strong>";
                        paragrafos += "&nbsp;o valor correspondente a <strong> " + String.Format("{0:0.00}", item.PercentualCobranca) + "% </strong>(" + EscreverParte(item.PercentualCobranca).Trim() + " porcento)";
                        paragrafos += "&nbsp;do valor da mensalidade para o <strong>" + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "UNIVAG MAIS" : "UNIVAG MAIS TRANSFERÊNCIA").Trim() + "</strong>.";
                        paragrafos += "</span>";
                        paragrafos += "</p>";
                    }
                    else
                    {
                        if (matricula.AproveitamentoEstudo != null && matricula.AproveitamentoEstudo.Id > 0)
                        {
                            semestre = matricula.AproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }
                        paragrafos += "<p style='text-align:justify'>";
                        paragrafos += "<span >";
                        paragrafos += "<strong>Par&aacute;grafo " + EscreverParteOrdinal(item.Semestre).ToLower().Trim() + ":</strong>";
                        paragrafos += "&nbsp;Para o " + semestre + ".&ordm; semestre o <strong>ALUNO</strong> pagar&aacute; a <strong>IES</strong>";
                        paragrafos += "&nbsp;o valor correspondente a <strong> " + String.Format("{0:0.00}", item.PercentualCobranca) + "% </strong>(" + EscreverParte(item.PercentualCobranca).Trim() + " porcento)";
                        paragrafos += "&nbsp;do valor da mensalidade para o <strong>" + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "UNIVAG&nbsp;MAIS" : "UNIVAG&nbsp;MAIS&nbsp;TRANSFERÊNCIA").Trim() + "</strong>,";
                        paragrafos += "&nbsp;devidamente corrigido/reajustado na forma da Cl&aacute;usula 8.&ordf;.";
                        paragrafos += "</span>";
                        paragrafos += "</p>";
                    }
                }
                paragrafos = paragrafos.Trim();
                tabela.Append(paragrafos);

                string documentoReplaced = documento.Replace("[QuadroResumo]", tabela.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualDesconto]", String.Format("{0:0.00}", parcelamentoUnivagConfiguracao.PercentualDesconto) + "%");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualDescontoExtenso]", EscreverParte(parcelamentoUnivagConfiguracao.PercentualDesconto) + " porcento");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[AnoPeriodoLetivo]", matricula.PeriodoLetivo.DataInicio.Value.Year.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorMensalidadeComBeneficio]", "R$ " + parcelamentoUnivagConfiguracao.ValorComDesconto.ToString("N2"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorCalculadoAPagar]", "R$ " + valorCemPorcentoComDesconto.ToString("N2"));
                documento = documentoReplaced;

                int mesesProrrogacao = (parcelamentoUnivagConfiguracao.QuantidadeSemestreGrade + parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao) * 6;
                int mesesGrade = parcelamentoUnivagConfiguracao.QuantidadeSemestreGrade * 6;

                documentoReplaced = documento.Replace("[QuantidadeSemestreGrade]", parcelamentoUnivagConfiguracao.QuantidadeSemestreGrade.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QuantidadeMesesGrade]", mesesGrade.ToString());
                documento = documentoReplaced;

                if (matricula.AproveitamentoEstudo != null && matricula.AproveitamentoEstudo.Id > 0)
                {
                    //documentoReplaced = documento.Replace("[CargaHorariaGrade]", gradeLetiva.GradeConsepe.TotalCargaHorariaIntegralizar.ToString());
                    documentoReplaced = documento.Replace("[CargaHorariaGrade]", matricula.GradeLetivaTurma.GradeLetiva.GradeConsepe.TotalCargaHorariaIntegralizar.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[SemestreIngresso]", matricula.AproveitamentoEstudo.UnivagMaisSemestre.ToString());
                    documento = documentoReplaced;

                    //documentoReplaced = documento.Replace("[CargaHorariaCursar]", cargaHorariaCursarAE.ToString());
                    //documentoReplaced = documento.Replace("[CargaHorariaCursar]", matricula.AproveitamentoEstudo.TotalCargaHoraria.ToString());
                    documentoReplaced = documento.Replace("[CargaHorariaCursar]", parcelamentoUnivagConfiguracao.CargaHorariaCursar.ToString());
                    
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeSemestreCursar]", parcelamentoUnivagConfiguracao.QtdSemestreCursar.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesCursar]", (parcelamentoUnivagConfiguracao.QtdSemestreCursar * 6).ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeSemestreProrrogacao]", parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesProrrogacao]", (parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao * 6).ToString());
                    documento = documentoReplaced;

                    //documentoReplaced = documento.Replace("[QuantidadeMesesComProrrogacao]", mesesProrrogacao.ToString());
                    documentoReplaced = documento.Replace("[QuantidadeMesesComPrrogacao]", ((parcelamentoUnivagConfiguracao.QtdSemestreCursar * 6) + (parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao * 6)).ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesProrrogacaoMetade]", ((parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao * 6) / 2).ToString());
                    documento = documentoReplaced;
                }

                documentoReplaced = documento.Replace("[QuantidadeSemestreProrrogacao]", mesesProrrogacao.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[AnoIngresso]", matricula.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[VencimentoBeneficio]", matricula.PeriodoLetivo.DataInicio.Value.ToString("MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoIngresso]", matricula.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlComm == null && parcelamentoUnivagBE != null)
                    parcelamentoUnivagBE.FecharConexao();
            }
        }


        public static string ParcelamentoUnivagQuadroResumno(string documento, AlunoDadosImpressaoVO objVO, PeriodoLetivoModalidadeVO periodoLetivoModalidade, SqlCommand sqlComm = null)
        {
            ParcelamentoUnivagBE parcelamentoUnivagBE = null;
            ParcelamentoUnivagSemestreBE parcelamentoUnivagSemestreBE = null;
            PlanoEstudoAproveitamentoBE planoEstudoAproveitamentoBE = null;
            AproveitamentoEstudoVO aproveitamentoEstudo = null;

            GradeLetivaBE gradeLetivaBE = null;
            decimal valorPrimeiraParcela = 0;
            decimal valorCemPorcento = 0;
            decimal valorComDesconto = 0;

            try
            {
                parcelamentoUnivagBE = new ParcelamentoUnivagBE(sqlComm);
                parcelamentoUnivagSemestreBE = new ParcelamentoUnivagSemestreBE(parcelamentoUnivagBE.GetSqlCommand());
                planoEstudoAproveitamentoBE = new PlanoEstudoAproveitamentoBE(parcelamentoUnivagBE.GetSqlCommand());

                gradeLetivaBE = new GradeLetivaBE(parcelamentoUnivagBE.GetSqlCommand());

                var planoEstudoAproveitamento = planoEstudoAproveitamentoBE.Consultar(new PlanoEstudoAproveitamentoVO()
                {
                    PlanoEstudo =
                    {
                        Aluno = { Id = objVO.Aluno.Id },
                        PeriodoLetivo = { Id = objVO.PeriodoLetivo.Id }
                    }
                });

                ParcelamentoUnivagVO parcelamentoUnivag = null;
                List<ParcelamentoUnivagSemestreVO> lstParcelamentoUnivagSemestre = new List<ParcelamentoUnivagSemestreVO>();

                var gradeLetiva = gradeLetivaBE.Consultar(new GradeLetivaVO() { Id = objVO.GradeLetivaTurma.GradeLetiva.Id });

                decimal valorMensalidade = objVO.Curso.CursoMensalidade.ValorMensalidade;
                int qtdParcelas = objVO.Curso.CursoMensalidade.QuantidadeParcela;
                int qtdSemestre = gradeLetiva.GradeConsepe.TotalSemestreIntegralizar;
                int qtdSemestreCursar = qtdSemestre;
                int cargaHorariaCursarAE = 0;

                parcelamentoUnivag = parcelamentoUnivagBE.ConsultarAluno(new ParcelamentoUnivagVO() { Aluno = { Id = objVO.Aluno.Id } });

                lstParcelamentoUnivagSemestre = parcelamentoUnivagSemestreBE.Listar(new ParcelamentoUnivagSemestreVO() { ParcelamentoUnivag = { Id = parcelamentoUnivag.Id } });

                //decimal valorBaseSemestre = Math.Round(((((valorMensalidade - (valorMensalidade * (parcelamentoUnivag.PercentualDesconto / 100))) * (100 / 100)) * qtdParcelas) * qtdSemestre) / ((qtdSemestre + parcelamentoUnivag.QuantidadeSemestreProrrogacao) * qtdParcelas), 2);
                decimal valorBaseSemestre = Math.Round((valorMensalidade * qtdParcelas * parcelamentoUnivag.QuantidadeSemestreGrade)/(qtdParcelas * (parcelamentoUnivag.QuantidadeSemestreGrade + parcelamentoUnivag.QuantidadeSemestreProrrogacao)), 2);
                if (planoEstudoAproveitamento != null && planoEstudoAproveitamento.AproveitamentoEstudo.Id > 0)
                {
                    AproveitamentoEstudoBE aproveitamentoEstudoBE = new AproveitamentoEstudoBE(parcelamentoUnivagBE.GetSqlCommand());
                    AproveitamentoEstudoDisciplinaBE aproveitamentoEstudoDisciplinaBE = new AproveitamentoEstudoDisciplinaBE(parcelamentoUnivagBE.GetSqlCommand());
                    aproveitamentoEstudo = aproveitamentoEstudoBE.ConsultarAproveitamentoEstudo(planoEstudoAproveitamento.AproveitamentoEstudo.Id);

                    var lstDisciplinaAproveitada = aproveitamentoEstudoDisciplinaBE.ListarDisciplinasAproveitadasConsepe(aproveitamentoEstudo.Id);

                    qtdSemestreCursar = qtdSemestreCursar - aproveitamentoEstudo.UnivagMaisSemestre + 1;
                    cargaHorariaCursarAE = gradeLetiva.GradeConsepe.TotalCargaHorariaIntegralizar - lstDisciplinaAproveitada.Sum(x => x.GradeConsepeDisciplina.CargaHorariaPratica + x.GradeConsepeDisciplina.CargaHorariaTeorica);
                    decimal valorTotalCurso = Math.Round(((valorMensalidade * qtdParcelas * qtdSemestre) / gradeLetiva.GradeConsepe.TotalCargaHorariaIntegralizar) * cargaHorariaCursarAE, 2);
                    valorBaseSemestre = Math.Round(valorTotalCurso / ((qtdSemestreCursar + parcelamentoUnivag.QuantidadeSemestreProrrogacao) * qtdParcelas), 2);
                }

                if (valorCemPorcento == 0)
                    valorCemPorcento = valorBaseSemestre;

                valorComDesconto = valorMensalidade - (valorMensalidade * (parcelamentoUnivag.PercentualDesconto / 100));

                StringBuilder tabela = new StringBuilder();
                tabela.Append(@"<style>
                                    table {
                                        border-collapse: collapse;
                                    }

                                    td p {
                                        margin-top:0;
                                        margin-bottom:0;
                                    }

                                    .border {
                                        border: 1px solid black;
                                    }
                                    .border-left {
                                        border-left: 1px solid black;
                                    }
                                    .border-right {
                                        border-right: 1px solid black;
                                    }
                                    .border-bottom {
                                        border-bottom: 1px solid black;
                                    }
                                </style>");

                tabela.Append("<table border='0' cellspacing='0' style='width: 100%; margin-top:30px; margin-bottom:10px; border: 1px solid #000; text-align: center; vertical-align: text-top;'>");
                tabela.Append("    <thead> ");
                tabela.Append("        <tr>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENSALIDADE ESCOLAR DO CURSO " + (parcelamentoUnivag.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.4)" : "(Item I.7)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENS.ESCOLAR COM BENEFICIO INCLUSÃO " + String.Format("{0:0.00}", parcelamentoUnivag.PercentualDesconto) + @"% DESC. " + (parcelamentoUnivag.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.6)" : "(Item I.9)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENSALIDADE UNIVAG MAIS " + (parcelamentoUnivag.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.9)" : " TRANSF. (Item I.12)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left border-bottom' colspan='" + lstParcelamentoUnivagSemestre.Count() + @"'>SEMESTRE</th>");
                tabela.Append("        </tr>");
                tabela.Append("    </thead>");

                tabela.Append("<tbody>");
                tabela.Append("    <tr> ");
                tabela.Append("        <th class='border-right border-left'></th>");
                tabela.Append("        <th class='border-right border-left'></th>");
                tabela.Append("        <th class='border-right border-left'></th>");

                int contSemestreAux = 0;
                int semestre = 0;
                foreach (var item in lstParcelamentoUnivagSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    semestre = item.Semestre;
                    if (aproveitamentoEstudo != null && aproveitamentoEstudo.Id > 0)
                    {
                        semestre = aproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                        contSemestreAux += 1;
                    }
                    tabela.Append("<th class='border' style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;'>" + semestre + "</th>");
                }

                tabela.Append("</tr>");
                tabela.Append("<tr>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'></td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'></td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left border-bottom'>" + objVO.PeriodoLetivo.DataInicio.Value.Year + "</td>");

                foreach (var item in lstParcelamentoUnivagSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    if (valorPrimeiraParcela == 0)
                        valorPrimeiraParcela = (item.ValorContratado - (item.ValorContratado * (parcelamentoUnivag.PercentualDesconto / 100)));

                    tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border'>" + String.Format("{0:0.00}", item.PercentualCobranca) + "%</td>");
                }

                decimal valorCemPorcentoComDesconto = Math.Round(valorCemPorcento * (1 - (parcelamentoUnivag.PercentualDesconto / 100)), 2);
                tabela.Append("</tr>");
                tabela.Append("<tr>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + valorMensalidade.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + valorComDesconto.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + valorCemPorcentoComDesconto.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold; vertical-align: text-top !important;' class='border'>" + valorPrimeiraParcela.ToString("N2") + @"</td>");

                foreach (var item in lstParcelamentoUnivagSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    if (item.Semestre > 1)
                        tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border'><p>" + (item.ValorContratado - (item.ValorContratado * (parcelamentoUnivag.PercentualDesconto / 100))).ToString("N2") + "</p><p>+</p><p>Reajuste</p></td>");
                }

                tabela.Append("</tr>");
                tabela.Append("    </tbody>");
                tabela.Append("</table>");

                contSemestreAux = 0;
                semestre = 0;
                string paragrafos = "";
                foreach (var item in lstParcelamentoUnivagSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    semestre = item.Semestre;
                    if (item.Semestre == 1)
                    {
                        if (aproveitamentoEstudo != null && aproveitamentoEstudo.Id > 0)
                        {
                            semestre = aproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }
                        paragrafos += "<p style='text-align:justify'>";
                        paragrafos += "<span>";
                        paragrafos += "<strong>Par&aacute;grafo " + EscreverParteOrdinal(item.Semestre).ToLower().Trim() + ":</strong>";
                        paragrafos += "&nbsp;Para o " + semestre + ".&ordm; semestre o <strong>ALUNO</strong> pagar&aacute; a <strong>IES</strong>";
                        paragrafos += "&nbsp;o valor correspondente a <strong> " + String.Format("{0:0.00}", item.PercentualCobranca) + "% </strong>(" + EscreverParte(item.PercentualCobranca).Trim() + " porcento)";
                        paragrafos += "&nbsp;do valor da mensalidade para o <strong>" + (parcelamentoUnivag.ParcelamentoUnivagTipo.Id == 1 ? "UNIVAG MAIS" : "UNIVAG MAIS TRANSFERÊNCIA").Trim() + "</strong>.";
                        paragrafos += "</span>";
                        paragrafos += "</p>";
                    }
                    else
                    {
                        if (aproveitamentoEstudo != null && aproveitamentoEstudo.Id > 0)
                        {
                            semestre = aproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }
                        paragrafos += "<p style='text-align:justify'>";
                        paragrafos += "<span >";
                        paragrafos += "<strong>Par&aacute;grafo " + EscreverParteOrdinal(item.Semestre).ToLower().Trim() + ":</strong>";
                        paragrafos += "&nbsp;Para o " + semestre + ".&ordm; semestre o <strong>ALUNO</strong> pagar&aacute; a <strong>IES</strong>";
                        paragrafos += "&nbsp;o valor correspondente a <strong> " + String.Format("{0:0.00}", item.PercentualCobranca) + "% </strong>(" + EscreverParte(item.PercentualCobranca).Trim() + " porcento)";
                        paragrafos += "&nbsp;do valor da mensalidade para o <strong>" + (parcelamentoUnivag.ParcelamentoUnivagTipo.Id == 1 ? "UNIVAG&nbsp;MAIS" : "UNIVAG&nbsp;MAIS&nbsp;TRANSFERÊNCIA").Trim() + "</strong>,";
                        paragrafos += "&nbsp;devidamente corrigido/reajustado na forma da Cl&aacute;usula 8.&ordf;.";
                        paragrafos += "</span>";
                        paragrafos += "</p>";
                    }
                }
                paragrafos = paragrafos.Trim();
                tabela.Append(paragrafos);

                string documentoReplaced = documento.Replace("[QuadroResumo]", tabela.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualDesconto]", String.Format("{0:0.00}", parcelamentoUnivag.PercentualDesconto) + "%");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualDescontoExtenso]", EscreverParte(parcelamentoUnivag.PercentualDesconto) + " porcento");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[AnoPeriodoLetivo]", objVO.PeriodoLetivo.DataInicio.Value.Year.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorMensalidadeComBeneficio]", "R$ " + valorComDesconto.ToString("N2"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorCalculadoAPagar]", "R$ " + valorCemPorcentoComDesconto.ToString("N2"));
                documento = documentoReplaced;

                int mesesProrrogacao = (parcelamentoUnivag.QuantidadeSemestreGrade + parcelamentoUnivag.QuantidadeSemestreProrrogacao) * 6;
                int mesesGrade = parcelamentoUnivag.QuantidadeSemestreGrade * 6;

                documentoReplaced = documento.Replace("[QuantidadeSemestreGrade]", parcelamentoUnivag.QuantidadeSemestreGrade.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QuantidadeMesesGrade]", mesesGrade.ToString());
                documento = documentoReplaced;

                if (aproveitamentoEstudo != null && aproveitamentoEstudo.Id > 0)
                {
                    documentoReplaced = documento.Replace("[CargaHorariaGrade]", gradeLetiva.GradeConsepe.TotalCargaHorariaIntegralizar.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[SemestreIngresso]", aproveitamentoEstudo.UnivagMaisSemestre.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[CargaHorariaCursar]", cargaHorariaCursarAE.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeSemestreCursar]", qtdSemestreCursar.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesCursar]", (qtdSemestreCursar * 6).ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeSemestreProrrogacao]", parcelamentoUnivag.QuantidadeSemestreProrrogacao.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesProrrogacao]", (parcelamentoUnivag.QuantidadeSemestreProrrogacao * 6).ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesProrrogacaoMetade]", (parcelamentoUnivag.QuantidadeSemestreProrrogacao * 6 / 2).ToString());
                    documento = documentoReplaced;
                }

                documentoReplaced = documento.Replace("[QuantidadeMesesComProrrogacao]", mesesProrrogacao.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QuantidadeSemestreProrrogacao]", mesesProrrogacao.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[VencimentoBeneficio]", objVO.PeriodoLetivo.DataInicio.Value.ToString("MM/yyyy"));
                documento = documentoReplaced;

                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string ParcelamentoUnivagQuadroResumnoConfiguracao(string documento, AlunoDadosImpressaoVO objVO, SqlCommand sqlComm = null)
        {
            ParcelamentoUnivagBE parcelamentoUnivagBE = new ParcelamentoUnivagBE(sqlComm);
            try
            {
                decimal valorPrimeiraParcela = 0;

                ParcelamentoUnivagConfiguracaoVO parcelamentoUnivagConfiguracao = parcelamentoUnivagBE.CarregarConfiguracao(objVO.Aluno, objVO.IdParcelamentoUnivagTipo, objVO.AproveitamentoEstudo);

                StringBuilder tabela = new StringBuilder();

                tabela.Append(" <style>                                  ");
                tabela.Append("     table {                              ");
                tabela.Append("         border-collapse: collapse;       ");
                tabela.Append("     }                                    ");
                tabela.Append("                                          ");
                tabela.Append("     td p {                               ");
                tabela.Append("         margin-top:0;                    ");
                tabela.Append("         margin-bottom:0;                 ");
                tabela.Append("     }                                    ");
                tabela.Append("                                          ");
                tabela.Append("     .border {                            ");
                tabela.Append("         border: 1px solid black;         ");
                tabela.Append("     }                                    ");
                tabela.Append("     .border-left {                       ");
                tabela.Append("         border-left: 1px solid black;    ");
                tabela.Append("     }                                    ");
                tabela.Append("     .border-right {                      ");
                tabela.Append("         border-right: 1px solid black;   ");
                tabela.Append("     }                                    ");
                tabela.Append("     .border-bottom {                     ");
                tabela.Append("         border-bottom: 1px solid black;  ");
                tabela.Append("     }                                    ");
                tabela.Append(" </style>                                 ");

                tabela.Append("<table border='0' cellspacing='0' style='width: 100%; margin-top:30px; margin-bottom:10px; border: 1px solid #000; text-align: center; vertical-align: text-top;'>");
                tabela.Append("    <thead> ");
                tabela.Append("        <tr>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENSALIDADE ESCOLAR DO CURSO " + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.4)" : "(Item I.7)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENS.ESCOLAR COM BENEFICIO INCLUSÃO " + String.Format("{0:0.00}", parcelamentoUnivagConfiguracao.PercentualDesconto) + @"% DESC. " + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.6)" : "(Item I.9)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 10pt; font-weight: bold;' class='border-right border-left border-bottom'>VALOR MENSALIDADE UNIVAG MAIS " + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "(Item I.9)" : " TRANSF. (Item I.12)") + "</th>");
                tabela.Append("            <th style='background-color: lightblue; font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left border-bottom' colspan='" + parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.Count() + @"'>SEMESTRE</th>");
                tabela.Append("        </tr>");
                tabela.Append("    </thead>");

                tabela.Append("<tbody>");
                tabela.Append("    <tr> ");
                tabela.Append("        <th class='border-right border-left'></th>");
                tabela.Append("        <th class='border-right border-left'></th>");
                tabela.Append("        <th class='border-right border-left'></th>");
                
                int contSemestreAux = 0;
                int semestre = 0;
                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    semestre = item.Semestre;
                    if (objVO.AproveitamentoEstudo != null && objVO.AproveitamentoEstudo.Id > 0)
                    {
                        semestre = objVO.AproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                        contSemestreAux += 1;
                    }
                    else if (objVO.PlanoEstudo != null && objVO.PlanoEstudo.Id > 0)
                    {
                        semestre = objVO.PlanoEstudo.UnivagMaisSemestre + contSemestreAux;
                        contSemestreAux += 1;
                    }

                    tabela.Append("<th class='border' style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;'>" + semestre + "</th>");
                }

                tabela.Append("</tr>");
                tabela.Append("<tr>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'></td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'></td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left border-bottom'>" + objVO.PeriodoLetivo.DataInicio.Value.Year + "</td>");

                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    if (valorPrimeiraParcela == 0)
                        valorPrimeiraParcela = item.ValorParcela;

                    tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border'>" + String.Format("{0:0.00}", item.PercentualCobranca) + "%</td>");
                }

                decimal valorCemPorcentoComDesconto = Math.Round(parcelamentoUnivagConfiguracao.ValorCemPorcento * (1 - (parcelamentoUnivagConfiguracao.PercentualDesconto / 100)), 2);
                tabela.Append("</tr>");
                tabela.Append("<tr>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + parcelamentoUnivagConfiguracao.ValorMensalidade.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + parcelamentoUnivagConfiguracao.ValorComDesconto.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border-right border-left'>" + valorCemPorcentoComDesconto.ToString("N2") + @"</td>");
                tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold; vertical-align: text-top !important;' class='border'>" + valorPrimeiraParcela.ToString("N2") + @"</td>");

                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    if (item.Semestre > 1)
                        tabela.Append("<td style='font-family: \"Times New Roman\"; font-size: 12pt; font-weight: bold;' class='border'><p>" + item.ValorParcela.ToString("N2") + "</p><p>+</p><p>Reajuste</p></td>");
                }

                tabela.Append("</tr>");
                tabela.Append("    </tbody>");
                tabela.Append("</table>");

                contSemestreAux = 0;
                semestre = 0;
                string paragrafos = "";
                foreach (var item in parcelamentoUnivagConfiguracao.LstParcelamentoUnivagConfiguracaoSemestre.OrderBy(x => x.Semestre).ToList())
                {
                    semestre = item.Semestre;
                    if (item.Semestre == 1)
                    {
                        if (objVO.AproveitamentoEstudo != null && objVO.AproveitamentoEstudo.Id > 0)
                        {
                            semestre = objVO.AproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }
                        else if (objVO.PlanoEstudo != null && objVO.PlanoEstudo.Id > 0)
                        {
                            semestre = objVO.PlanoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }

                        paragrafos += "<p style='text-align:justify'>";
                        paragrafos += "<span>";
                        paragrafos += "<strong>Par&aacute;grafo " + EscreverParteOrdinal(item.Semestre).ToLower().Trim() + ":</strong>";
                        paragrafos += "&nbsp;Para o " + semestre + ".&ordm; semestre o <strong>ALUNO</strong> pagar&aacute; a <strong>IES</strong>";
                        paragrafos += "&nbsp;o valor correspondente a <strong> " + String.Format("{0:0.00}", item.PercentualCobranca) + "% </strong>(" + EscreverParte(item.PercentualCobranca).Trim() + " porcento)";
                        paragrafos += "&nbsp;do valor da mensalidade para o <strong>" + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "UNIVAG MAIS" : "UNIVAG MAIS TRANSFERÊNCIA").Trim() + "</strong>.";
                        paragrafos += "</span>";
                        paragrafos += "</p>";
                    }
                    else
                    {
                        if (objVO.AproveitamentoEstudo != null && objVO.AproveitamentoEstudo.Id > 0)
                        {
                            semestre = objVO.AproveitamentoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }
                        else if (objVO.PlanoEstudo != null && objVO.PlanoEstudo.Id > 0)
                        {
                            semestre = objVO.PlanoEstudo.UnivagMaisSemestre + contSemestreAux;
                            contSemestreAux += 1;
                        }

                        paragrafos += "<p style='text-align:justify'>";
                        paragrafos += "<span >";
                        paragrafos += "<strong>Par&aacute;grafo " + EscreverParteOrdinal(item.Semestre).ToLower().Trim() + ":</strong>";
                        paragrafos += "&nbsp;Para o " + semestre + ".&ordm; semestre o <strong>ALUNO</strong> pagar&aacute; a <strong>IES</strong>";
                        paragrafos += "&nbsp;o valor correspondente a <strong> " + String.Format("{0:0.00}", item.PercentualCobranca) + "% </strong>(" + EscreverParte(item.PercentualCobranca).Trim() + " porcento)";
                        paragrafos += "&nbsp;do valor da mensalidade para o <strong>" + (parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == 1 ? "UNIVAG&nbsp;MAIS" : "UNIVAG&nbsp;MAIS&nbsp;TRANSFERÊNCIA").Trim() + "</strong>,";
                        paragrafos += "&nbsp;devidamente corrigido/reajustado na forma da Cl&aacute;usula 8.&ordf;.";
                        paragrafos += "</span>";
                        paragrafos += "</p>";
                    }
                }
                paragrafos = paragrafos.Trim();
                tabela.Append(paragrafos);

                string documentoReplaced = documento.Replace("[QuadroResumo]", tabela.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualDesconto]", String.Format("{0:0.00}", parcelamentoUnivagConfiguracao.PercentualDesconto) + "%");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PercentualDescontoExtenso]", EscreverParte(parcelamentoUnivagConfiguracao.PercentualDesconto) + " porcento");
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[AnoPeriodoLetivo]", objVO.PeriodoLetivo.DataInicio.Value.Year.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorMensalidadeComBeneficio]", "R$ " + parcelamentoUnivagConfiguracao.ValorComDesconto.ToString("N2"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[ValorCalculadoAPagar]", "R$ " + valorCemPorcentoComDesconto.ToString("N2"));
                documento = documentoReplaced;

                int mesesProrrogacao = (parcelamentoUnivagConfiguracao.QuantidadeSemestreGrade + parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao) * 6;
                int mesesGrade = parcelamentoUnivagConfiguracao.QuantidadeSemestreGrade * 6;

                documentoReplaced = documento.Replace("[QuantidadeSemestreGrade]", parcelamentoUnivagConfiguracao.QuantidadeSemestreGrade.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[QuantidadeMesesGrade]", mesesGrade.ToString());
                documento = documentoReplaced;

                if ((objVO.AproveitamentoEstudo != null && objVO.AproveitamentoEstudo.Id > 0) || 
                    (objVO.PlanoEstudo != null && objVO.PlanoEstudo.Id > 0 && parcelamentoUnivagConfiguracao.ParcelamentoUnivagTipo.Id == (long)ParcelamentoUnivagBE.EnumParcelamentoUnivagTipo.Readequacao))
                {
                    if (objVO.AproveitamentoEstudo != null && objVO.AproveitamentoEstudo.Id > 0)
                    {
                        documentoReplaced = documento.Replace("[SemestreIngresso]", objVO.AproveitamentoEstudo.GradeLetivaSemestre.GradeConsepeSemestre.Semestre.ToString());
                        documento = documentoReplaced;
                    }
                    else
                    {
                        documentoReplaced = documento.Replace("[SemestreIngresso]", objVO.PlanoEstudo.GradeLetivaSemestre.GradeConsepeSemestre.Semestre.ToString());
                        documento = documentoReplaced;
                    }

                    documentoReplaced = documento.Replace("[CargaHorariaCursar]", parcelamentoUnivagConfiguracao.CargaHorariaCursar.ToString());
                    documento = documentoReplaced;

                    mesesProrrogacao = (parcelamentoUnivagConfiguracao.QtdSemestreCursar + parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao) * 6;

                    documentoReplaced = documento.Replace("[CargaHorariaGrade]", objVO.Aluno.GradeLetivaTurma.GradeLetiva.GradeConsepe.TotalCargaHorariaIntegralizar.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeSemestreCursar]", parcelamentoUnivagConfiguracao.QtdSemestreCursar.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesCursar]", (parcelamentoUnivagConfiguracao.QtdSemestreCursar * 6).ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeSemestreProrrogacao]", parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao.ToString());
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesProrrogacao]", (parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao * 6).ToString());
                    documento = documentoReplaced;

                    //documentoReplaced = documento.Replace("[QuantidadeMesesComProrrogacao]", mesesProrrogacao.ToString());
                    documentoReplaced = documento.Replace("[QuantidadeMesesComPrrogacao]", mesesProrrogacao.ToString());                    
                    documento = documentoReplaced;

                    documentoReplaced = documento.Replace("[QuantidadeMesesProrrogacaoMetade]", ((parcelamentoUnivagConfiguracao.QuantidadeSemestreProrrogacao * 6) / 2).ToString());
                    documento = documentoReplaced;
                }

                documentoReplaced = documento.Replace("[QuantidadeSemestreProrrogacao]", mesesProrrogacao.ToString());
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[VencimentoBeneficio]", objVO.PeriodoLetivo.DataInicio.Value.ToString("MM/yyyy"));
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[AnoIngresso]", objVO.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                documentoReplaced = documento.Replace("[PeriodoLetivoIngresso]", objVO.PeriodoLetivo.Descricao);
                documento = documentoReplaced;

                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlComm == null && parcelamentoUnivagBE != null)
                    parcelamentoUnivagBE.FecharConexao();
            }
        }


        public static string HtmlToPdfGeneretor(string htmlText, string urlHeader = null, string urlFooter = null, List<string> options = null, string filePrefix = null, string caminho = null)
        {
            try
            {
                string[] arrOptions = ConfigureOptionsHtmlToPdf(options, urlHeader, urlFooter);
                string caminhoOk = caminho ?? "~/Temp/";
                var pdfUrl = "";

                if (caminho != null)
                {
                    pdfUrl = PdfGenerator.HtmlToPdf(caminhoOk,
                    filePrefix ?? "GeneratedPDF_",
                   htmlText, arrOptions, true, true);
                }
                else
                {
                    pdfUrl = PdfGenerator.HtmlToPdf(caminhoOk,
                         filePrefix ?? "GeneratedPDF_",
                        htmlText, arrOptions, caminhoNovo: true);
                }

                return pdfUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private static string[] ConfigureOptionsHtmlToPdf(List<string> options, string urlHeader = null, string urlFooter = null)
        {
            try
            {
                if (options == null)
                    options = new List<string>() { "--disable-smart-shrinking", "-L 8mm", "-R 8mm", "-T 25mm", "-B 8mm" };
                else
                {
                    ConfigureHeaderHtmlToPdf(ref options, urlHeader);
                    ConfigureFooterHtmlToPdf(ref options, urlFooter);
                    options.Add("--disable-smart-shrinking");
                }
                return options.ToArray();
            }
            catch (Exception e)
            {
                throw;
            }
        }


        private static void ConfigureHeaderHtmlToPdf(ref List<string> options, string urlHeader)
        {
            try
            {
                if (!string.IsNullOrEmpty(urlHeader))
                {
                    int spacing = 5, num = 0;
                    string opt = "";
                    opt = options.FirstOrDefault(x => x.Contains("-T"));
                    num = Convert.ToInt16(Regex.Replace(opt, @"[^\d]", ""));
                    opt = "-T " + (num + spacing) + "mm";
                    options[options.FindIndex(x => x.Contains("-T"))] = opt;
                    options.Add("--header-spacing " + spacing);
                    options.Add("--header-html " + urlHeader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        private static void ConfigureFooterHtmlToPdf(ref List<string> options, string urlFooter)
        {
            try
            {
                if (!string.IsNullOrEmpty(urlFooter))
                {
                    int spacing = 5, num = 0;
                    string opt = "";
                    opt = options.FirstOrDefault(x => x.Contains("-B"));
                    num = Convert.ToInt16(Regex.Replace(opt, @"[^\d]", ""));
                    opt = "-B " + (num + spacing) + "mm";
                    options[options.FindIndex(x => x.Contains("-B"))] = opt;
                    options.Add("--footer-spacing " + spacing);
                    options.Add("--footer-html " + urlFooter);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public static string GetArquivoConexaoBanco()
        {

            XmlDocument xmlDocument = null;
            StringBuilder sb = null;
            try
            {
                xmlDocument = new XmlDocument();
                sb = new StringBuilder();
                xmlDocument.Load("C:/ConnectionString.xml");
                XmlNode xmlNode = xmlDocument.SelectSingleNode("Banco_Dados");
                XmlNode xmlNodeConexao = xmlNode.SelectSingleNode(Dominio.Conexoes.Comum);
                XmlNode xmlNodeServer = xmlNodeConexao.SelectSingleNode("Server");
                XmlNode xmlNodeUser = xmlNodeConexao.SelectSingleNode("User_Id");
                XmlNode xmlNodePassword = xmlNodeConexao.SelectSingleNode("Password");
                XmlNode xmlNodeDataBase = xmlNodeConexao.SelectSingleNode("Data_Base");

                sb.Append("Server=" + xmlNodeServer.InnerText + ";");
                sb.Append("User Id=" + xmlNodeUser.InnerText + ";");
                sb.Append("Password=" + xmlNodePassword.InnerText + ";");
                sb.Append("Database=" + xmlNodeDataBase.InnerText + ";");
                sb.Append("Persist Security Info=True");

                return sb.ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (xmlDocument != null)
                    xmlDocument = null;
            }
        }


        public static string SafeSubstring(this string value, int startIndex, int length)
        {
            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }


        // GerarChaveAutenticao
        public static string GerarChaveAutenticao(string idTitulos = "")
        {
            string chave = "";

            var sessao = CommonPage.GetSessao();

            if (idTitulos == "")
            {
                chave = "IdUsuario:" + sessao.IdUsuario + ",IdCampus:" + sessao.IdCampus + ",DataHora:" + DateTime.Now;
            }
            else
            {
                chave = "IdTitulos:" + idTitulos + ",IdUsuario:" + sessao.IdUsuario + ",IdCampus:" + sessao.IdCampus + ",DataHora:" + DateTime.Now;
            }

            return Criptografia.CifrarCesar(chave, 183);
        }


        // GerarChaveAutenticaoSistema
        public static string GerarChaveAutenticaoSistema(string idTitulos = "")
        {
            string chave = "";

            if (idTitulos != "")
            {
                chave = "IdTitulos:" + idTitulos;
            }

            chave = chave + "IdUsuario:1,IdCampus:1,DataHora:" + DateTime.Now;

            return Criptografia.CifrarCesar(chave, 183);
        }


        //ConverterParaMoedaReal
        /// <summary>
        /// Autor: Leandro Curiso
        /// Data: 26.11.2014
        /// Descrição: Resonsavel pelo conversão de string em moeda real
        /// </summary>
        /// <param name="valor">String com o valor</param>
        /// <returns>Retorna o valor em decimal da moeda em real</returns>
        public static decimal ConverterParaMoedaReal(dynamic valor)
        {
            return Math.Truncate((100 * Convert.ToDecimal(valor)) / 100);
        }


        //ConverterParaMoedaReal2
        public static decimal ConverterParaMoedaReal2(dynamic valor)
        {
            var arrValor = valor.Split(Convert.ToChar(","));
            var parsedValor = "";
            if (arrValor.Length > 1)
            {
                if (arrValor[1].Length > 2)
                {
                    valor = arrValor[0] + "," + arrValor[1].Substring(0, 2);
                }
            }
            return Convert.ToDecimal(string.Format("{0:C}", valor));
        }


        //DecimalToExtenso
        public static string DecimalToExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = String.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += EscreverParte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : String.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : String.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : String.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : String.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : String.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : String.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != String.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : String.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")

                                valor_por_extenso += " DE";

                            else if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" |
                                     valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" |
                                     valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != String.Empty)
                            valor_por_extenso += " E ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";

                        else
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "TRILHÕES")
                            valor_por_extenso += " DE";
                        else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "TRILHÕES")
                            valor_por_extenso += " DE";


                    if (strValor.IndexOf(".") == i)
                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                    if (strValor.Length == i)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != String.Empty)
                            valor_por_extenso += " E ";


                    //if (i == 15)
                    //    if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                    //        valor_por_extenso += " CENTAVO";
                    //    else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                    //        valor_por_extenso += " CENTAVOS";

                }
                return valor_por_extenso.ToLower();
            }
        }


        public static string IntegerToExtenso(int valor)
        {
            if (valor <= 0)
                return "zero";
            else
            {
                string strValor = valor.ToString("000000000000000");
                string valor_por_extenso = String.Empty;

                for (int i = 0; i <= 12; i += 3)
                {
                    valor_por_extenso += EscreverParte(Convert.ToInt32(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToInt32(strValor.Substring(3, 12)) > 0) ? " E " : String.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToInt32(strValor.Substring(3, 12)) > 0) ? " E " : String.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToInt32(strValor.Substring(6, 9)) > 0) ? " E " : String.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToInt32(strValor.Substring(6, 9)) > 0) ? " E " : String.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToInt32(strValor.Substring(9, 6)) > 0) ? " E " : String.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToInt32(strValor.Substring(9, 6)) > 0) ? " E " : String.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != String.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToInt32(strValor.Substring(12, 3)) > 0) ? " E " : String.Empty);
                    }
                }
                return valor_por_extenso.ToLower();
            }
        }


        public static string EscreverPorcentagemMenorZero(decimal valor)
        {
            string montagem = String.Empty;
            if (valor > 0 & valor < 1)
            {
                valor *= 100;
                montagem = "ZERO VÍRGULA ";
            }
            string strValor = valor.ToString("00");
            int a = Convert.ToInt32(strValor.Substring(0, 1));
            int b = Convert.ToInt32(strValor.Substring(1, 1));

            if (a == 1)
            {
                if (b == 0) montagem += "DEZ";
                else if (b == 1) montagem += "ONZE";
                else if (b == 2) montagem += "DOZE";
                else if (b == 3) montagem += "TREZE";
                else if (b == 4) montagem += "QUATORZE";
                else if (b == 5) montagem += "QUINZE";
                else if (b == 6) montagem += "DEZESSEIS";
                else if (b == 7) montagem += "DEZESSETE";
                else if (b == 8) montagem += "DEZOITO";
                else if (b == 9) montagem += "DEZENOVE";
            }
            else if (a == 2) montagem += "VINTE";
            else if (a == 3) montagem += "TRINTA";
            else if (a == 4) montagem += "QUARENTA";
            else if (a == 5) montagem += "CINQUENTA";
            else if (a == 6) montagem += "SESSENTA";
            else if (a == 7) montagem += "SETENTA";
            else if (a == 8) montagem += "OITENTA";
            else if (a == 9) montagem += "NOVENTA";

            if (a != 1 & b != 0 & montagem != String.Empty) montagem += " E ";

            if (a != 1)
                //if (b == 0) montagem += "ZERO";
                if (b == 1) montagem += "UM";
                else if (b == 2) montagem += "DOIS";
                else if (b == 3) montagem += "TRÊS";
                else if (b == 4) montagem += "QUATRO";
                else if (b == 5) montagem += "CINCO";
                else if (b == 6) montagem += "SEIS";
                else if (b == 7) montagem += "SETE";
                else if (b == 8) montagem += "OITO";
                else if (b == 9) montagem += "NOVE";

            montagem += " PORCENTO";

            return montagem.ToLower();
        }


        //EscreverParte
        public static string EscreverParte(decimal valor)
        {
            if (valor <= 0)
                return String.Empty;
            else
            {
                string montagem = String.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
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
                    if (c == 0) montagem += ((a > 0) ? " E " : String.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : String.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : String.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : String.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : String.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : String.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : String.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : String.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : String.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : String.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : String.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : String.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : String.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : String.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : String.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : String.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : String.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : String.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != String.Empty) montagem += " E ";

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

                return montagem.ToLower();
            }

            //Data de aniversário
            DateTime dt = Convert.ToDateTime("8/04/1984");
            //TimeSpan com a data atual menos data do niversário
            TimeSpan ts = DateTime.Today - dt;
            //Converter TimeSpan para DateTime
            //Como o new DateTime() retorna 01/01/0001 00:00:00
            //vou ter que remover um ano .AddYears(- 1) e um dia .AddDays(-1) para ter a data exata.
            DateTime idade = (new DateTime() + ts).AddYears(-1).AddDays(-1);

            //Idade em anos
            HttpContext.Current.Response.Write(idade.Year);

        }


        public static string EscreverParteOrdinal(decimal valor)
        {
            if (valor <= 0)
                return String.Empty;
            else
            {
                string montagem = String.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += "CENTÉSIMO";
                else if (a == 2) montagem += "DUCENTÉSIMO";
                else if (a == 3) montagem += "TRECENTÉSIMO";
                else if (a == 4) montagem += "QUADRINGENTÉSIMO";
                else if (a == 5) montagem += "QUINGENTÉSIMO";
                else if (a == 6) montagem += "SEXCENTÉSIMO";
                else if (a == 7) montagem += "SEPTINGENTÉSIMO";
                else if (a == 8) montagem += "OCTINGENTÉSIMO";
                else if (a == 9) montagem += "NONINGENTÉSIMO";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO";
                    else if (c == 1) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO PRIMEIRO";
                    else if (c == 2) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO SEGUNDO";
                    else if (c == 3) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO TERCEIRO";
                    else if (c == 4) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO QUARTO";
                    else if (c == 5) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO QUINTO";
                    else if (c == 6) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO SEXTO";
                    else if (c == 7) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO SÉTIMO";
                    else if (c == 8) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO OITVO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : String.Empty) + "DÉCIMO NONO";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : String.Empty) + "VIGÉSIMO";
                else if (b == 3) montagem += ((a > 0) ? " E " : String.Empty) + "TRIGÉSIMO";
                else if (b == 4) montagem += ((a > 0) ? " E " : String.Empty) + "QUADRAGÉSIMO";
                else if (b == 5) montagem += ((a > 0) ? " E " : String.Empty) + "QUINQUAGÉSIMO";
                else if (b == 6) montagem += ((a > 0) ? " E " : String.Empty) + "SEXAGÉSIMO";
                else if (b == 7) montagem += ((a > 0) ? " E " : String.Empty) + "SEPTUAGÉSIMO";
                else if (b == 8) montagem += ((a > 0) ? " E " : String.Empty) + "OCTÓGÉSIMO";
                else if (b == 9) montagem += ((a > 0) ? " E " : String.Empty) + "NONAGÉSIMO";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != String.Empty) montagem += " ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "PRIMEIRO";
                    else if (c == 2) montagem += "SEGUNDO";
                    else if (c == 3) montagem += "TERCEIRO";
                    else if (c == 4) montagem += "QUARTO";
                    else if (c == 5) montagem += "QUINTO";
                    else if (c == 6) montagem += "SEXTO";
                    else if (c == 7) montagem += "SÉTIMO";
                    else if (c == 8) montagem += "OITAVO";
                    else if (c == 9) montagem += "NONO";

                return montagem.ToLower();
            }

            //Data de aniversário
            DateTime dt = Convert.ToDateTime("8/04/1984");
            //TimeSpan com a data atual menos data do niversário
            TimeSpan ts = DateTime.Today - dt;
            //Converter TimeSpan para DateTime
            //Como o new DateTime() retorna 01/01/0001 00:00:00
            //vou ter que remover um ano .AddYears(- 1) e um dia .AddDays(-1) para ter a data exata.
            DateTime idade = (new DateTime() + ts).AddYears(-1).AddDays(-1);

            //Idade em anos
            HttpContext.Current.Response.Write(idade.Year);

        }


        public static int CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;
            int years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;

            return years;
        }


        public static bool IsExternalIp(string ipRequisicao, string ipCampus)
        {
            if (ipRequisicao.Equals(ipCampus))
            {
                return false;
            }
            else if (ipRequisicao.Substring(0, 2) == "10")
            {
                return false;
            }
            else if (ipRequisicao.Substring(0, 3) == "172")
            {
                string strIp = ipRequisicao.Substring(4, 3).Replace(".", "");
                int faxaIp = Convert.ToInt32(strIp);
                if (faxaIp >= 16 && faxaIp <= 31)
                {
                    int lastNumber = Convert.ToInt32(ipRequisicao.Split('.')[3]);
                    if (lastNumber > 0)
                        return false;
                }
            }
            else if (ipRequisicao.Substring(0, 3) == "192")
            {
                string strIp = ipRequisicao.Substring(4, 3).Replace(".", "");
                int faxaIp = Convert.ToInt32(strIp);
                if (faxaIp == 168)
                {
                    return false;
                }
            }
            else if (ipRequisicao.Substring(0, 3) == "127" || ipRequisicao.Equals("::1"))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Calcula a quantidade de dias entre as Datas
        /// </summary>
        /// <param name="dataInicial"></param>
        /// <param name="dataFinal"></param>
        /// <returns>Valor de diferença entre as datas</returns>
        public static int GetDiasEntreDatas(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                int dias;
                if (dataInicial.Date > dataFinal.Date)
                    dias = (int)dataInicial.Subtract(dataFinal).TotalDays;
                else
                    dias = (int)dataFinal.Subtract(dataInicial).TotalDays;

                return dias;
            }
            catch (Exception)
            {
                throw new Exception("Houve algum problema ao verificar a quantidade de dias entre datas.");
            }
        }


        public static int GetMesesEntreDatas(DateTime dataInicial, DateTime dataFinal)
        {
            int meses = 0;

            if (dataInicial.Date > dataFinal.Date)
                meses = (int)((dataInicial.Year - dataFinal.Year) * 12) + dataInicial.Month - dataFinal.Month;
            else
                meses = (int)((dataFinal.Year - dataInicial.Year) * 12) + dataFinal.Month - dataInicial.Month;

            return meses;
        }


        // Quantidade de Anos Entre Datas
        /// <summary>
        /// Autor: Marcelo Campaner
        /// Data: 20.01.2015
        /// Descrição: Resonsavel pelo calculo de anos entre Datas
        /// </summary>
        /// <param name="dataInicial"></param>
        /// <param name="dataFinal"></param>
        /// <returns></returns>
        public static int GetAnosEntreDatas(DateTime dataInicial, DateTime dataFinal)
        {
            int anos = 0;

            if (dataInicial.Date > dataFinal.Date)
                anos = (dataInicial.Year - dataFinal.Year);
            else
                anos = (dataFinal.Year - dataInicial.Year);

            return anos;
        }


        // Quantidade de Anos Entre Datas
        /// <summary>
        /// Autor: Luiz Henrique Sguarezi
        /// Data: 20.01.2015
        /// Descrição: Resonsavel pelo calculo entre Datas
        /// </summary>
        /// <param name="dataInicial"></param>
        /// <param name="dataFinal"></param>
        /// <returns>
        /// Lista de Inteiros
        /// [0] - Anos
        /// [1] - Meses
        /// [2] - Dias
        /// [3] - Horas
        /// [4] - Minutos
        /// [5] - Segundos
        /// </returns>
        public static List<int> GetDiferencaEntreDatas(DateTime dataInicial, DateTime dataFinal)
        {
            DateTime data_resultado = new DateTime();

            List<int> retorno = new List<int>();

            if (dataFinal <= dataInicial)
                throw new Exception("Data invalida.");

            data_resultado = new DateTime((dataFinal - dataInicial).Ticks);

            retorno.Add(data_resultado.Year);
            retorno.Add(data_resultado.Month);
            retorno.Add(data_resultado.Day);
            retorno.Add(data_resultado.Hour);
            retorno.Add(data_resultado.Minute);
            retorno.Add(data_resultado.Second);

            return retorno;
        }


        //Comparar string
        /// <summary>
        /// Autor: Leandro Curiso
        /// Data: 21.01.2015
        /// Descrição: Resonsavel pela comparação entre strings
        /// </summary>
        /// <param name="str1">String 1 com o valor</param>
        /// <param name="str2">String 2 com o valor</param>
        /// <returns>Retorna um valor booleano true caso as strings sejam iguais</returns>
        public static bool CompararString(string str1, string str2)
        {
            return RemoveCaracteresEspeciais(str1, true, true).ToLower() == RemoveCaracteresEspeciais(str2, true, true).ToLower() ? true : false;
        }


        //Consultar cep web service
        /// <summary>
        /// Autor: Leandro Curiso
        /// Data: 24.01.2015
        /// Descrição: Resonsavel pela consulta de cep
        /// </summary>
        /// <param name="cep">String com o cep a ser consultado</param>
        /// <returns>Retorna uma string json com as informações de endereço</returns>
        public static string ConsultarCepWebService(string cep)
        {
            try
            {
                string responseText = String.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://viacep.com.br/ws/" + cep.Replace(",", "").Replace(".", "") + "/json");
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseText = sr.ReadToEnd();
                }
                return responseText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Base64 to binary
        /// <summary>
        /// Autor: Leandro Curiso
        /// Data: 02.02.2015
        /// Descrição: Resonsavel pela conversão de stirng base 64 para byte[]
        /// </summary>
        /// <param name="base64String">String com o cep a ser consultado</param>
        /// <returns>Retorna um byte[]</returns>
        public static byte[] Base64ToBinary(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }


        /// <summary>
        /// Autor: Michael Lopes
        /// Data: 25.08.2014
        /// Descrição: Resonsavel por converter nota do enem
        /// </summary>
        /// <param name="nota"></param>
        /// <returns></returns>
        public static decimal NotaEnemToNotaNomal(string nota)
        {
            string nt = "0";
            if (nota != null && Convert.ToDecimal(nota) > 10)
            {
                decimal fP = Convert.ToDecimal(nota);
                string[] ntArr = Convert.ToString(fP / (decimal)100.0).Split('.');

                if (ntArr.Length > 1)
                    nt = ntArr[0].Substring(0, 2) + "." + ntArr[1].Substring(0, 2);
                else
                    nt = ntArr[0];
            }

            return Convert.ToDecimal(nt);
        }


        public static string GerarPdfBoleto(string htmlText, string filePrefix = null)
        {
            try
            {
                string[] arrOptions = { "-L 4mm", "-R 8mm", "-T 4mm", "-B 2mm" };

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp/Boleto/")))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp/Boleto/"));

                var pdfUrl = PdfGenerator.HtmlToPdf(pdfOutputLocation: "~/Temp/Boleto/",
                    outputFilenamePrefix: filePrefix ?? "GeneratedPDF_",
                   htmlString: htmlText, options: arrOptions);

                return pdfUrl;
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 14.10.2015
        /// Descrição: Remove caracteres especiais em trecho de sql que for relacionado ao campo CPF
        /// </summary>
        /// <param name="whereSql">String de consulta</param>
        /// <returns>Retorna uma string de consulta</returns>
        public static string SanitizeCPF(string whereSql)
        {
            Regex rgx = new Regex(@"(?<=.CPF\s=\s')[^']*(?=')", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            whereSql = rgx.Replace(whereSql, m => m.Value.Replace(".", String.Empty).Replace("-", String.Empty));

            return whereSql;
        }


        public static long CalcularRegimeProfessor(decimal ch, decimal chDs)
        {
            var profRegime = 1;
            var ch50 = (ch * (decimal)0.5);   //50% da Carga Horária Total
            var ch75 = (ch * (decimal)0.75);  //75% da Carga Horária Total

            if (ch >= 40) //Se a CH Total for maior ou igual 40h
            {
                if (chDs <= ch50) //Se Dentro de Sala for menor ou igual 20h
                {
                    profRegime = 3; //Tempo Integral (TI)
                }
                else if (chDs <= ch75) //Se Dentro de Sala for maior que 20h e menor que 30h
                {
                    profRegime = 2; //Tempo Parcial (TP)
                }
                else
                {
                    profRegime = 1; //Se Dentro de Sala for maior que 30h (HORISTA)
                }
            }
            else if ((ch >= 12) && (chDs <= ch75)) //Se a CH Total estiver entre 12h e 40h e, Dentro de Sala for menor ou igual a 75% da CH Total
            {
                profRegime = 2; //Tempo Parcial (TP)
            }
            else //Se CH Total for menor que 12h sempre será Horista
            {
                profRegime = 1; //Horista
            }

            return profRegime;
        }


        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 18/02/2016
        /// Descricao: Converte valor decimal para formato de apresentação em horas. Ex.: 25,5 => 25:45
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecimalToTime(decimal value)
        {
            decimal hourNum = Math.Floor(value);
            string hour = hourNum < 10 ? "0" + hourNum.ToString() : hourNum.ToString();
            var deciString = value.ToString().Split(',')[1];
            if (Convert.ToDouble(deciString) > 0)
            {
                decimal deciNumber = Convert.ToDecimal("0," + deciString);
                var minutosCorretosDecimal = Math.Round(deciNumber * 0.6m, 2);
                string minutosCorretos = minutosCorretosDecimal.ToString().Split(',')[1];
                return string.Format("{0}:{1}", hour, minutosCorretos);
            }

            string minutesTimeSpan = TimeSpan.FromHours((Math.Abs((double)value) % 1)).ToString("h\\:mm").Split(':')[1];
            return string.Format("{0}:{1}", hour, minutesTimeSpan);
        }


        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 18/08/2016
        /// Descrição Funcional: Define valores da Classe conforme objeto passado via javascript. * Importante: os atributos do objeto devem ser semelhante a classe.
        ///  Ex.:
        ///  --> C# Class
        ///  class CursoVO {
        ///     public string Descricao {get; set;}
        ///     public GpaVO Gpa {get; set;}
        ///  }
        ///
        ///  --> Javascript Object
        ///  var Object = {
        ///     Descricao: 'Nome do Curso'
        ///     Gpa: {
        ///         Id: 1
        ///     }
        ///  }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classe"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ObjectToClass<T>(T classe, object obj)
        {
            try
            {
                if (obj != null)
                {
                    var PropertiesClass = classe.GetType().GetProperties();
                    var dictionary = (Dictionary<string, object>)obj;
                    foreach (var prop in PropertiesClass)
                    {
                        SetValueClass(prop, classe, dictionary);
                    }
                }

                return classe;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static void SetValueClass(PropertyInfo prop, object classe, Dictionary<string, object> dictionary)
        {
            try
            {
                string field = prop.Name;
                var value = dictionary.FirstOrDefault(d => d.Key == field).Value;
                var propValue = prop.GetValue(classe);
                if (value != null)
                {
                    // Verifica se a propriedade é uma Class
                    if (propValue != null && propValue.GetType().IsClass)
                    {
                        ObjectToClass(propValue, value);
                    }
                    else
                    {
                        if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(long) || prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(long?))
                        {
                            prop.SetValue(classe, Convert.ToInt64(value));
                        }
                        else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                        {
                            prop.SetValue(classe, Convert.ToDateTime(value));
                        }
                        else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                        {
                            prop.SetValue(classe, Convert.ToBoolean(value));
                        }
                        else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                        {
                            prop.SetValue(classe, Convert.ToDecimal(value));
                        }
                        else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                        {
                            prop.SetValue(classe, Convert.ToDouble(value));
                        }
                        else if (prop.PropertyType == typeof(char) || prop.PropertyType == typeof(char?))
                        {
                            prop.SetValue(classe, Convert.ToChar(value));
                        }
                        else
                        {
                            prop.SetValue(classe, value);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Calcula qual a Idade em Anos, Meses e Dias passando a Data de Nascimento
        /// </summary>
        /// <param name="dataNascimento">Data de Nascimento</param>
        /// <param name="resumido">resumido = true: 27a4m5d/resumido = false: 27 anos, 4 meses e 5 dias; Valor default true</param>
        /// <returns>String com a Idade em Anos, Meses e Dias. Ex: 27a4m5d/27 anos, 4 meses e 5 dias</returns>
        public static string CalcularIdadeExtenso(DateTime dataNascimento, bool resumido = true)
        {
            try
            {
                DateTime dataAgora = DateTime.Today;
                int anos = new DateTime(dataAgora.Subtract(dataNascimento).Ticks).Year - 1;
                DateTime dataAniversarioNoAno = dataNascimento.AddYears(anos);
                int meses = 0;
                string retorno = "";
                for (int i = 1; i <= 12; i++)
                {
                    if (dataAniversarioNoAno.AddMonths(i) == dataAgora)
                    {
                        meses = i;
                        break;
                    }
                    else if (dataAniversarioNoAno.AddMonths(i) > dataAgora)
                    {
                        meses = i - 1;
                        break;
                    }
                }
                int dias = dataAgora.Subtract(dataAniversarioNoAno.AddMonths(meses)).Days;

                if (resumido)
                {
                    retorno = String.Format("{0}a{1}m{2}d", anos, meses, dias);
                }
                else
                {
                    if (anos > 0)
                    {
                        if (anos > 1)
                            retorno += string.Format("{0} anos", anos);
                        else
                            retorno += string.Format("{0} ano", anos);
                    }
                    if (meses > 0)
                    {
                        if (!string.IsNullOrEmpty(retorno))
                        {
                            if (dias > 0)
                                retorno += ", ";
                            else
                                retorno += " e ";
                        }

                        if (meses > 1)
                            retorno += string.Format("{0} meses", meses);
                        else
                            retorno += string.Format("{0} mês", meses);
                    }
                    if (dias > 0)
                    {
                        if (!string.IsNullOrEmpty(retorno))
                            retorno += " e ";

                        if (dias > 1)
                            retorno += string.Format("{0} dias", dias);
                        else
                            retorno += string.Format("{0} dia", dias);
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao gerar a idade por extenso.\n" + ex.Message);
            }
        }


        public static string GetEstacaoTrabalho()
        {
            try
            {
                //string userDomain = Environment.UserDomainName;
                //string userName = Environment.UserName;
                //string estacaoTrabalho = userDomain + "\\" + userName;
                string estacaoTrabalho = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

                return estacaoTrabalho;
            }
            catch (Exception)
            {

                return "";
            }

        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 29.10.2015
        /// Descrição: Pega o IP real do computador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o ip do computador</returns>
        public static string GetRealIpAddress()
        {
            try
            {
                string realIpAddress = "";

                IPHostEntry ipEntry = Dns.GetHostEntry(GetComputerName());
                IPAddress[] ip = ipEntry.AddressList;
                realIpAddress = ip[ip.Length - 1].ToString();

                return realIpAddress;
            }
            catch (Exception)
            {

                return "";
            }

        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega o Endereço Físico do adaptador de rede do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o endereço MAC</returns>
        public static string GetMACAddress()
        {

            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String macAddress = string.Empty;

                foreach (NetworkInterface adapter in nics)
                {
                    if (macAddress == String.Empty)
                    {
                        if (adapter.OperationalStatus == OperationalStatus.Up)
                        {
                            //IPInterfaceProperties properties = adapter.GetIPProperties();
                            //macAddress = adapter.GetPhysicalAddress().ToString();

                            macAddress += adapter.GetPhysicalAddress().ToString();
                            break;

                        }

                    }
                }
                if (!string.IsNullOrEmpty(macAddress))
                {
                    macAddress =
                 macAddress.Substring(0, 2) + '-' +
                 macAddress.Substring(2, 2) + '-' +
                 macAddress.Substring(4, 2) + '-' +
                 macAddress.Substring(6, 2) + '-' +
                 macAddress.Substring(8, 2) + '-' +
                 macAddress.Substring(10, 2)
                 ;
                }


                return macAddress;
            }
            catch (Exception)
            {
                return "";
            }



        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 29.10.2015
        /// Descrição: Pega o nome do computador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o nome do computador</returns>
        public static string GetComputerName()
        {

            string computerName = Dns.GetHostName();

            return computerName;
        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o nome do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Nome do Navegador</returns>
        public static string GetBrowserName()
        {
            try
            {
                _browser = HttpContext.Current.Request.Browser;

                return _browser.Browser.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o tipo do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Tipo do Navegador</returns>
        public static string GetBrowserType()
        {
            try
            {
                _browser = HttpContext.Current.Request.Browser;

                return _browser.Type.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o nome do servidor acessado pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Nome do Servidor</returns>
        public static string GetServerName()
        {
            try
            {
                _context = HttpContext.Current;

                string serverName = _context.Request.ServerVariables["SERVER_NAME"];

                return serverName.ToUpper();
            }
            catch (Exception)
            {
                return "";
            }
        }


        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta o IP do servidor acessado pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o IP do Servidor</returns>
        public static string GetIpAddress()
        {
            _context = HttpContext.Current;

            var ipAddress = (!String.IsNullOrEmpty(_context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                ? _context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                : _context.Request.ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Contains(","))
                ipAddress.Split(',').First().Trim();

            return ipAddress;
        }


        /// <summary>
        /// GerarSenha
        /// </summary>
        /// <param name="tamanho"></param>
        /// <returns></returns>
        public static string GerarSenha(int tamanho = 8)
        {
            string caracteres = "abcdefghijkmnpqrstuvwxyz23456789@";

            int valorMaximo = caracteres.Length;

            // Criamos um objeto do tipo randon
            Random random = new Random(DateTime.Now.Millisecond);

            // Criamos a string que montaremos a senha
            StringBuilder senha = new StringBuilder(tamanho);

            // Fazemos um for adicionando os caracteres a senha
            for (int i = 0; i < tamanho; i++)
            {
                senha.Append(caracteres[random.Next(0, valorMaximo)]);
            }

            return senha.ToString();
        }


        /// <summary>
        /// GerarUrlAmigavel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GerarUrlAmigavel(string value)
        {
            //First to lower case
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }


        /// <summary>
        /// SetMaskLinhaDigitavel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetMaskLinhaDigitavel(string value)
        {
            var value1 = value.Substring(0, 5);
            var value2 = value.Substring(5, 5);
            var value3 = value.Substring(10, 5);
            var value4 = value.Substring(15, 6);
            var value5 = value.Substring(21, 5);
            var value6 = value.Substring(26, 6);
            var value7 = value.Substring(32, 1);
            var value8 = value.Substring(33, 14);

            var valueFormat = value1 + "." + value2 + " " + value3 + "." + value4 + " " + value5 + "." + value6 + " " + value7 + " " + value8;

            return valueFormat;
        }
    }


    public static class Switch
    {

        public static SwitchBuilder<TElement>.CaseBuilder On<TElement>(TElement element)
        {
            return new SwitchBuilder<TElement>(element).Start();
        }

        public class SwitchBuilder<TElement>
        {
            TElement _element;
            TElement _firstCase;
            internal SwitchBuilder(TElement element) { _element = element; }
            internal CaseBuilder Start()
            {
                return new CaseBuilder() { Switch = this };
            }
            private ThenBuilder Case(TElement element)
            {
                _firstCase = element;
                return new ThenBuilder() { Switch = this };
            }
            private SwitchBuilder<TElement, TResult>.CaseBuilder Then<TResult>(TResult result)
            {
                return new SwitchBuilder<TElement, TResult>(
                  _element,
                  _firstCase,
                  result).Start();
            }
            public class CaseBuilder
            {
                internal SwitchBuilder<TElement> Switch { get; set; }
                public ThenBuilder Case(TElement element)
                {
                    return Switch.Case(element);
                }
            }
            public class ThenBuilder
            {
                internal SwitchBuilder<TElement> Switch { get; set; }
                public SwitchBuilder<TElement, TResult>.CaseBuilder Then<TResult>(TResult result)
                {
                    return Switch.Then(result);
                }
            }
        }

        public class SwitchBuilder<TElement, TResult>
        {
            TElement _element;
            TElement _currentCase;
            IDictionary<TElement, TResult> _map = new Dictionary<TElement, TResult>();
            internal SwitchBuilder(TElement element, TElement firstCase, TResult firstResult)
            {
                _element = element;
                _map.Add(firstCase, firstResult);
            }
            internal CaseBuilder Start()
            {
                return new CaseBuilder() { Switch = this };
            }
            private ThenBuilder Case(TElement element)
            {
                _currentCase = element;
                return new ThenBuilder() { Switch = this };
            }
            private CaseBuilder Then(TResult result)
            {
                _map.Add(_currentCase, result);
                return new CaseBuilder() { Switch = this };
            }
            private TResult Default(TResult defaultResult)
            {
                TResult result;
                if (_map.TryGetValue(_element, out result))
                {
                    return result;
                }
                return defaultResult;
            }
            public class CaseBuilder
            {
                internal SwitchBuilder<TElement, TResult> Switch { get; set; }
                public ThenBuilder Case(TElement element)
                {
                    return Switch.Case(element);
                }
                public TResult Default(TResult defaultResult)
                {
                    return Switch.Default(defaultResult);
                }
            }
            public class ThenBuilder
            {
                internal SwitchBuilder<TElement, TResult> Switch { get; set; }
                public CaseBuilder Then(TResult result)
                {
                    return Switch.Then(result);
                }
            }
        }

    }
}