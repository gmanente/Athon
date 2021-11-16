using System;
using System.Drawing.Imaging;
using Sistema.Api.dll.Repositorio.Util;

/// <summary>
/// Autor: Evander Costa
/// Data: 10.10.2014
/// Descrição: Classe para gerar a imagem do Captcha
/// </summary>
public partial class CImage : System.Web.UI.Page
{
    /// <summary>
    /// Autor: Evander Costa
    /// Data: 10.10.2014
    /// Descrição: Método Construtor
    /// </summary>
    /// <param></param>
    /// <returns>Retorna o arquivo da imagem Jpeg</returns> 
    protected void Page_Load(object sender, EventArgs e)
    {
        // Cria um texto randomico para o Captcha.
        Random rnd = new Random();

        string n1 = rnd.Next(1, 9).ToString();
        string n2 = rnd.Next(1, 9).ToString();
        string n3 = rnd.Next(1, 9).ToString();
        string n4 = rnd.Next(1, 9).ToString();
        string n5 = rnd.Next(1, 9).ToString();

        string captchaTexto = n1 + " " + n2 + " " + n3 + " " + n4 + " " + n5;
        string captchaImagem = n1 + n2 + n3 + n4 + n5;

        // Grava os textos em um objetos de Sessão.
        this.Session["CaptchaAudioText"] = captchaTexto;
        this.Session["CaptchaImageText"] = captchaImagem;

        // Cria uma imagem CAPTCHA usando o texto gerado
        // Utiliza a classe utilitaria: RandomImage.
        RandomImage ci = new RandomImage(captchaImagem, 348, 75);

        // Altera a resposta do cabeçalho para a saída da imagem em JPEG.
        this.Response.Clear();
        this.Response.ContentType = "image/jpeg";

        // Escreve a imagem de resposta no formato JPEG.
        ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

        // Dispose do objeto imagem Captcha.
        ci.Dispose();
    }
}