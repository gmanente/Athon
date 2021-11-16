using System;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services;
using Sistema.Api.dll.Repositorio.Util;

/// <summary>
/// Autor: Evander Costa
/// Data: 28/07/2015
/// Descrição: Classe para gerar o audio do Captcha
/// Novo método usando referencia para System.Speech.dll
/// </summary>
public partial class CAudio : System.Web.UI.Page
{
    /// <summary>
    /// Autor: Evander Costa
    /// Data: 10.10.2014
    /// Descrição: Método Construtor
    /// </summary>
    /// <param></param>
    /// <returns></returns> 
    protected void Page_Load(object sender, EventArgs e)
    {
            
    }


    // WebMétodo Recuperar
    /// Autor: Evander Costa
    /// Data: 30/07/2015
    /// Descrição: Retorna a string do o audio do captcha
    /// </summary>
    /// <param></param>
    /// <returns></returns> 
    // 
    [WebMethod]
    public static string Recuperar()
    {
        var ajax = new Ajax();

        try
        {
            string texto = "";

            // Se a Sessão CaptchaAudioText não for nulo
            if (HttpContext.Current.Session["CaptchaAudioText"] != null)
            {
                // Recupera o texto do Captcha
                texto = HttpContext.Current.Session["CaptchaAudioText"].ToString();
            }

            // Sucesso
            ajax.StatusOperacao = true;
            ajax.Variante = Criptografia.Base64Encode(texto);
        }
        catch (Exception ex)
        {
            // Erro
            ajax.StatusOperacao = false;
            ajax.ObjMensagem = ex.Message;
        }

        return ajax.GetAjaxJson();
    }
    
}