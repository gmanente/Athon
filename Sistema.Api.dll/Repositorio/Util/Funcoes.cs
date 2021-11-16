using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
        private const string tableStyle = @"<style>
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
                                </style>";

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

        public static long GetUsuarioAuditoria()
        {
            return CommonPage.GetSessao().IdUsuario;
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

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public static byte[] StrToBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string BytesToStr(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string limparTelefone(string telefone)
        {
            return telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        }

        public static string limparVirgula(string virgula)
        {
            return virgula.Replace(",", "");
        }

        public static dynamic trocarPontoPorVirgula(string ponto)
        {
            return ponto.Replace(".", ",");
        }

        public static string AdicionarEspaco(string valor, int qtdEspaco)
        {
            return valor.PadLeft(qtdEspaco);
        }

        public static string CompletarZerosDireita(string valor, int qtdEspaco)
        {
            return valor.PadRight(qtdEspaco, '0');
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

        public static string CompletarEspacoDireita(string valor, int qtdEspaco)
        {
            string vl = "";
            if (valor != null)
                vl = valor;
            if (valor != null && valor.Length > qtdEspaco)
            {
                vl = valor.Substring(0, qtdEspaco);
            }
            return vl.PadRight(qtdEspaco, ' ');
        }

        public static string CompletarEspacoEsquerda(string valor, int qtdEspaco)
        {
            string vl = "";
            if (valor != null)
                vl = valor;
            return vl.PadLeft(qtdEspaco, ' ');
        }

        public static string CompletarZerosEsquerda(string valor, int qtdEspaco)
        {
            return valor.PadLeft(qtdEspaco, '0');
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

        public static Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
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

                    string dirpath = string.Concat(appCurrent.Substring(0, appCurrent.IndexOf("Sistema.")), "Sistema.Web.UI.Sistema\\", caminho);
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

        public static string ImportarSmartAdmin()
        {
            StringBuilder sbImport = new StringBuilder();

            var noCache = "?nocache=" + CacheHandler();

            var URL_FILE = GetUrlRepositorio();

            var PATH_SMART = URL_FILE + "View/SmartAdmin/";

            // CSS
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/bootstrap.min.css" + noCache));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-production.min.css" + noCache));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-skins.min.css" + noCache));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-rtl.min.css" + noCache));

            // JS
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/smart-util.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/app.config.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/app.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/bootstrap/bootstrap.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/notification/SmartNotification.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/smartwidgets/jarvis.widget.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/sparkline/jquery.sparkline.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/bootstrap-slider/bootstrap-slider.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/clockpicker/clockpicker.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/jquery-form/jquery-form.min.js" + noCache));

            // JS Data Tables
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/jquery.dataTables.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/dataTables.colVis.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/dataTables.tableTools.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/dataTables.bootstrap.min.js" + noCache));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatable-responsive/datatables.responsive.min.js" + noCache));

            return sbImport.ToString();
        }

        public static string ImportarSmartAdminCore()
        {
            StringBuilder sbImport = new StringBuilder();

            var URL_FILE = GetUrlRepositorio();

            var PATH_SMART = URL_FILE + "View/SmartAdmin/";

            // CSS
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/bootstrap.min.css"));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-production.min.css"));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-skins.min.css"));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-rtl.min.css"));

            // JS
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/smart-util.js"));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/app.config.js"));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/app.min.js"));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/bootstrap/bootstrap.min.js"));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/notification/SmartNotification.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/smartwidgets/jarvis.widget.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/sparkline/jquery.sparkline.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/bootstrap-slider/bootstrap-slider.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/clockpicker/clockpicker.min.js"));
            sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/jquery-form/jquery-form.min.js"));

            // JS Data Tables
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/jquery.dataTables.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/dataTables.colVis.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/dataTables.tableTools.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatables/dataTables.bootstrap.min.js"));
            //sbImport.AppendLine(MontarTagImport("js", PATH_SMART + "Js/plugin/datatable-responsive/datatables.responsive.min.js"));

            return sbImport.ToString();
        }

        public static string ImportarSmartAdminLight()
        {
            StringBuilder sbImport = new StringBuilder();

            var URL_FILE = GetUrlRepositorio();

            var PATH_SMART = URL_FILE + "View/SmartAdmin/";

            // CSS
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/bootstrap.min.css"));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-production.min.css"));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-skins.min.css"));
            sbImport.AppendLine(MontarTagImport("css", PATH_SMART + "Css/smartadmin-rtl.min.css"));

            return sbImport.ToString();
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


        public static void HtmlToWord(string html, string nomeDocumento)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
            HttpContext.Current.Response.ContentType = "application/wsword";
            var strNomeArquivo = nomeDocumento + ".doc";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strNomeArquivo);

            var strHTMLContent = new StringBuilder();

            strHTMLContent.Append("<html " +
                                  "xmlns:o='urn:schemas-microsoft-com:office:office' " +
                                  "xmlns:w='urn:schemas-microsoft-com:office:word'" +
                                  "xmlns='http://www.w3.org/TR/REC-html40'>" +
                                  "<head><title>Time</title>");
            //<div style="margin:0;">
            // strHTMLContent.Append("<div style='margin:0;'>");
            strHTMLContent.Append(html);
            //strHTMLContent.Append("</div>");
            HttpContext.Current.Response.Write(strHTMLContent);
            //HttpContext.Current.Response.Redirect("http://localhost:26080/View/Page/NossaBolsaBrasil/Impressao.aspx", false);

            // HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
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

        public static decimal ConverterParaMoeda(decimal valor)
        {
            var valorRetorno = valor.ToString().Replace(",", "");
            valorRetorno = valorRetorno.Replace(".", ",");
            return Convert.ToDecimal(valorRetorno);
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

        public static string PorcentagemPorExtenso(decimal valor)
        {
            string montagem = String.Empty;
            bool menorQueUm = false;
            if (valor > 0 & valor < 1)
            {
                valor *= 100;
                montagem = "ZERO VÍRGULA ";
                menorQueUm = true;
            }
            string strValor = "";
            int a;
            int b;
            string valorComVirgula = valor.ToString();
            int posVirgula = valorComVirgula.IndexOf(',') > 0 ? int.Parse(valorComVirgula.Split(',')[1]) : 0;
            if (valorComVirgula.IndexOf(',') > 0 && !menorQueUm)
            {
                var arrStr = valorComVirgula.Split(',');
                strValor = Convert.ToInt32(arrStr[0]).ToString("00");
                a = Convert.ToInt32(strValor.Substring(0, 1));
                b = Convert.ToInt32(strValor.Substring(1, 1));

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

                if (posVirgula > 0)
                    montagem += " VÍRGULA ";

                strValor = Convert.ToInt32(arrStr[1]).ToString("00");
                a = Convert.ToInt32(strValor.Substring(0, 1));
                b = Convert.ToInt32(strValor.Substring(1, 1));

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
            }
            else
            {
                strValor = valor.ToString("00");
                a = Convert.ToInt32(strValor.Substring(0, 1));
                b = Convert.ToInt32(strValor.Substring(1, 1));

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
            }

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

        //Binary to base64
        /// <summary>
        /// Autor: Leandro Curiso
        /// Data: 02.02.2015
        /// Descrição: Resonsavel pela conversão de Binary para string base64
        /// </summary>
        /// <param name="binary">Byte[] binary</param>
        /// <returns>Retorna uma string em base64</returns>
        public static string BinaryToBase64(byte[] binary)
        {
            return Convert.ToBase64String(binary);
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

        public static string MontarTagImport(string tag, string arq)
        {
            if (tag == "css")
                return "<link rel='stylesheet' href='" + arq + "'/> \n\t";
            else if (tag == "js")
                return "<script type='text/javascript' charset='utf-8' src='" + arq + "'></script> \n\t";
            else
                return "";
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
        /// Descricao: Converte valor decimal para formato de apresentação em horas. Ex.: 25,5 => 25:30
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecimalToTime(decimal value)
        {
            decimal hourNum = Math.Floor(value);
            string hour = hourNum < 10 ? "0" + hourNum.ToString() : hourNum.ToString();
            var deciString = "000";

            if (value.ToString().Contains(','))
                deciString = value.ToString().Split(',')[1];

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
        //public static string DecimalToTime(decimal value)
        //{
        //    return value < 24 ? (value < 10 ? "0" + TimeSpan.FromHours((double)value).ToString("h\\:mm") : TimeSpan.FromHours((double)value).ToString("h\\:mm")) : Math.Floor(value) + ":" + TimeSpan.FromHours((Math.Abs((double)value) % 1)).ToString("h\\:mm").Split(':')[1];
        //}

        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
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
            string computerName = "";

            computerName = Dns.GetHostName();

            return computerName;
        }

        /// <summary>
        /// Autor: Gustavo Martins
        /// Data: 19.04.2016
        /// Descrição: Pega o nome do computador do Usuário de acordo com o seu Ip
        /// </summary>
        /// <returns>Retorna uma string contendo o nome do computador</returns>
        private static string GetMachineNameFromIPAddress()
        {

            _context = HttpContext.Current;

            string ipAdress = _context.Request.ServerVariables["REMOTE_HOST"];

            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found...
            }
            return machineName;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega as informações do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo dados de navegador</returns>
        public static string GetBrowserInfo()
        {
            _context = HttpContext.Current;
            _browser = _context.Request.Browser;

            string userAgent = _context.Request.ServerVariables["HTTP_USER_AGENT"];

            string browserInfo = ""
                + "" + GetBrowserName()
                + " v." + GetBrowserVersion()
                + " - " + GetBrowserPlatform()
                ;

            return browserInfo;
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
        /// Descrição: Detecta a versão do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo a Versão do Navegador</returns>
        public static string GetBrowserVersion()
        {
            _browser = HttpContext.Current.Request.Browser;

            return _browser.Version.ToString();
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
        /// Descrição: Detecta o tipo de plataforma do navegador do Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Tipo de Plataforma</returns>
        private static string GetBrowserPlatform()
        {
            _browser = HttpContext.Current.Request.Browser;

            return _browser.Platform.ToString();
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Detecta se o Usuário está acessando via SmartPhone
        /// </summary>
        /// <returns>Retorna uma string dizendo se é ou não usuário de SmartPhone</returns>
        public static string GetIsMobileUser()
        {
            _browser = HttpContext.Current.Request.Browser;

            return (_browser.IsMobileDevice) ? "SIM" : "NÃO";
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
        /// Descrição: Detecta o nome e a porta do servidor acessada pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo o Nome/Porta do Servidor</returns>
        public static string GetServerNameAndPort()
        {
            _context = HttpContext.Current;

            string serverNameAndPort = _context.Request.ServerVariables["HTTP_HOST"];

            return serverNameAndPort;
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega as informações da Plataforma em que roda o Sistema Operacional
        /// </summary>
        /// <returns>Retorna uma string contendo a plataforma do SO</returns>
        public static string GetWinOS()
        {
            string SO = "";

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    SO = "Em execução no Windows NT ou Windows 2000";
                    break;
                case PlatformID.Win32S:
                    SO = "Execução sob Win32s";
                    break;
                case PlatformID.Win32Windows:
                    SO = "Execução sob win9x";
                    break;
            }

            return SO;
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
        /// Autor: Giovanni Ramos
        /// Data: 28.10.2015
        /// Descrição: Pega a data/hora local do servidor acessado pelo Usuário
        /// </summary>
        /// <returns>Retorna uma string contendo a Data/Hora Local do Servidor</returns>
        public static string GetLocalDate()
        {
            DateTime localDate = DateTime.Now;

            var culture = new CultureInfo("pt-BR");
            var regiaoDataInfo = localDate.ToString(culture);

            return regiaoDataInfo;
        }


        //
        // GerarSenha - Padrão 8 caracteres aleatorios
        //
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

        public static string SetMaskCnpj(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
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