using Sistema.Api.dll.Src;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class PdfGenerator
    {

        public static string HtmlToPdf(string pdfOutputLocation, string outputFilenamePrefix, string htmlString, string[] options = null, bool noHash = false, bool caminhoNovo = false)
        {
            string urlsSeparatedBySpaces = string.Empty;
            string httpContextPath = "~/Temp/";
            try
            {
                string pdfHtmlToPdfExePath = "";
                if (Dominio.AppState == Dominio.ApplicationState.Debug)
                {
                    string raiz = "";
                    if (HttpContext.Current != null)
                    {
                        var raizArr = HttpContext.Current.Server.MapPath("~").Split('\\');
                        for (var i = 0; i < raizArr.Length - 2; i++)
                        {
                            if (i > 0)
                                raiz += "\\" + raizArr[i];
                            else
                                raiz = raizArr[i];
                        }
                    }
                    else
                        raiz = "D:\\Net_v4.6\\Projetos\\Univag.Sisger";

                    pdfHtmlToPdfExePath = raiz + "\\Sistema.Lib.WkHtmltoPdf\\bin\\wkhtmltopdf.exe";
                }
                else
                {
                    pdfHtmlToPdfExePath = "C:\\Sistema.Lib.WkHtmltoPdf\\bin\\wkhtmltopdf.exe";
                }

                var data = DateTime.Now;
                string path = Criptografia.MD5(DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")) + ".html";
                string folder = "";
                if (HttpContext.Current != null)
                {
                    folder = HttpContext.Current.Server.MapPath(httpContextPath);
                }
                else
                {
                    httpContextPath = "../../Temp/";
                    folder = Path.GetFullPath(httpContextPath);
                }

                if (!Directory.Exists(folder))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(folder);

                }
                string completePath = folder + path;

                if (!File.Exists(completePath))
                {
                    File.Create(completePath).Dispose();
                    using (TextWriter tw = new StreamWriter(completePath))
                    {
                        tw.Write("<meta charset='UTF-8' /> " + htmlString);
                        tw.Close();
                        tw.Dispose();
                    }
                }

                string[] urls = new string[1];
                urls[0] = completePath;
                //Determine inputs
                if ((urls == null) || (urls.Length == 0))
                    throw new Exception("No input URLs provided for HtmlToPdf");
                else
                    urlsSeparatedBySpaces = String.Join(" ", urls); //Concatenate URLs

                #region Codigo Atualizado
                var timeout = 60000;
                int returnCode = 0;
                //string outputFolder = pdfOutputLocation;
                string outputFilename = outputFilenamePrefix + (noHash ? "" : "_" + Criptografia.MD5(DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff"))) + ".pdf"; // assemble destination PDF file name;

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = pdfHtmlToPdfExePath;
                    process.StartInfo.Arguments = ((options == null) ? "" : String.Join(" ", options)) + " " + urlsSeparatedBySpaces + " " + outputFilename;

                    //string teste;
                    //if (noHash)
                    //    teste = outputFolder;
                    //else if (HttpContext.Current != null)
                    //    teste = HttpContext.Current.Server.MapPath(outputFolder);
                    //else
                    //    teste = Path.GetFullPath(outputFolder);

                    //process.StartInfo.WorkingDirectory = (noHash ? outputFolder : (HttpContext.Current != null ? HttpContext.Current.Server.MapPath(outputFolder) : Path.GetFullPath(outputFolder)));
                    process.StartInfo.WorkingDirectory = noHash ? pdfOutputLocation : folder;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    //process.StartInfo.RedirectStandardInput = true; // redirect all 3, as it should be all 3 or none

                    StringBuilder output = new StringBuilder();
                    StringBuilder error = new StringBuilder();

                    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                output.AppendLine(e.Data);
                            }
                        };
                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                error.AppendLine(e.Data);
                            }
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        if (process.WaitForExit(timeout) &&
                            outputWaitHandle.WaitOne(timeout) &&
                            errorWaitHandle.WaitOne(timeout))
                        {
                            // Process completed. Check process.ExitCode here.
                        }
                        else
                        {
                            // Timed out.
                        }
                    }

                    returnCode = process.ExitCode;
                }

                #endregion

                if (File.Exists(completePath))
                {
                    File.Delete(completePath);
                }

                // if 0 or 2, it worked so return path of pdf
                if (returnCode == 0 || returnCode == 2)
                    return (noHash ? pdfOutputLocation : httpContextPath) + outputFilename;
                //return (!string.IsNullOrEmpty(pdfOutputLocation) ? pdfOutputLocation : httpContextPath) + outputFilename;
                else
                    throw new Exception("Erro no output");
            }
            catch (Exception exc)
            {
                throw new Exception("Problem generating PDF from HTML, URLs: " + urlsSeparatedBySpaces + ", outputFilename: " + outputFilenamePrefix + ", path: " + pdfOutputLocation + " | " + httpContextPath, exc);
            }
        }
    }
}
