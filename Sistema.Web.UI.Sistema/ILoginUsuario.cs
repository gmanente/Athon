using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace Sistema.Web.UI.Sistema
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginUsuario" in both code and config file together.

    /// <summary>
    /// Autor: Evander Costa
    /// Data: 10.10.2014
    /// Descrição: Interface de Serviços de LoginUsuario.
    /// </summary>
    [ServiceContract]
    public interface ILoginUsuario
    {
        // Operação Validar
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "validar"
        )]
        string Validar(Stream s);


        // Operação Entrar
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "entrar"
        )]
        string Entrar(Stream s);


        // Operação Recuperar Senha
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "recuperar-senha"
        )]
        Autenticacao RecuperarSenha(Stream s);

    }
}
