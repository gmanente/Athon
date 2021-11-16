using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Web.Http;
using Newtonsoft.Json.Linq;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Web.API.SwaggerExt;
using Sistema.Web.API.SwaggerExt.Examples;
using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sistema.Web.API.Controllers
{
    /// <summary>
    /// Token
    /// </summary>
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/Token")]
    public class TokenController : ApiController
    {

        /// <summary>
        /// Valida o acesso de um cliente ativo e em caso de sucesso, fornece o Token (JWT)
        /// </summary>
        /// <param name="data">Login e Senha do cliente</param>
        /// <example>
        /// Exemplo de consulta
        /// <code>
        ///     { login: "03336874939", senha: "@univag" }
        /// </code>
        /// </example>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Authorize"), HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AuthorizeResponse), Description = "Operação bem sucedida")]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(AuthorizeResponseExample))]
        [SwaggerRequestExample(typeof(AuthorizeRequest), typeof(AuthorizeRequestExample))]
        //[SwaggerDefaultValue("data", "{ login: \"29298245149\", senha: \"@univag\" }")]
        public HttpResponseMessage Authorize(AuthorizeRequest data)
        {
            HttpResponseMessage response = null;

            UsuarioBE usuarioBE = null;

            try
            {
                usuarioBE = new UsuarioBE();

                string login = data?.Login;
                string senha = data?.Senha;

                // Verifica: Login e Senha
                if (login != null && senha != null)
                {
                    // Recupera os dados do usuario
                    var lstUsuario = usuarioBE.ApiAuthenticate(new UsuarioVO() { NomeLogin = login });

                    var usuario = lstUsuario.FirstOrDefault();
                    if (usuario != null)
                    {
                        var loginSucesso = string.Equals(usuario.UsuarioSenha.Senha, Criptografia.MD5(senha));
                        if (loginSucesso)
                        {
                            var usuarioAtivo = usuario.Ativo ?? false;
                            if (usuarioAtivo)
                            {
                                var dataCorrente = DateTime.Now.Date;
                                var dataTermino = (usuario.UsuarioSenha.DataTermino)?.Date;

                                if (dataCorrente <= dataTermino)
                                {
                                    // Somente perfis que são de "AcessoApi"
                                    var perfis = lstUsuario
                                        .Where(w => w.UsuarioPerfil.Ativar == true)
                                        .Where(w => w.UsuarioPerfil.Perfil.Regra.Contains("AcessoApi"))
                                        .Select(x => x.UsuarioPerfil.Perfil.Regra)
                                        .Distinct()
                                        .ToList();

                                    var acessoApi = perfis.Count > 0;
                                    if (acessoApi)
                                    {
                                        object jwtToken;

                                        // Gera o Token
                                        CreateToken(usuario, perfis, out jwtToken, 1500);

                                        // 200 - Success
                                        response = Request.CreateResponse(HttpStatusCode.OK, jwtToken);
                                    }
                                    else
                                    {
                                        // 401 - Unauthorized
                                        response = Request.CreateResponse(HttpStatusCode.Unauthorized, new { authenticated = false, message = "Cliente sem perfil de acesso a API." });
                                    }
                                }
                                else
                                {
                                    // 401 - Unauthorized
                                    var dataExp = string.Format("A sua data de acesso expirou em {0}.", dataTermino?.ToString("dd/MM/yyyy"));
                                    response = Request.CreateResponse(HttpStatusCode.Unauthorized, new { authenticated = false, message = dataExp });
                                }
                            }
                            else
                            {
                                // 401 - Unauthorized
                                response = Request.CreateResponse(HttpStatusCode.Unauthorized, new { authenticated = false, message = "Cliente com conta inativa." });
                            }
                        }
                        else
                        {
                            // 403 - Forbidden
                            response = Request.CreateResponse(HttpStatusCode.Forbidden, new { authenticated = false, message = "Acesso não autorizado!" });
                        }
                    }
                    else
                    {
                        // 404 - NotFound
                        response = Request.CreateResponse(HttpStatusCode.NotFound, new { authenticated = false, message = "Cliente não encontrado!" });
                    }
                }
                else
                {
                    // 400 - BadRequest
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, new { authenticated = false, message = "Forneça sua credenciais de autenticação." });
                }
            }
            catch (Exception)
            {
                // 417 - ExpectationFailed
                response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
            finally
            {
                usuarioBE?.FecharConexao();
            }

            return response;
        }

        /// <summary>
        /// Gera um novo Token JWT
        /// </summary>
        /// <param name="userData">Dados do usuário</param>
        /// <param name="userRole">Perfis do usuário</param>
        /// <param name="expireMinutes">Tempo de Expiração do Token em minutos</param>
        /// <param name="jwtToken">Token JWT</param>
        /// <returns>JWT token</returns>
        private static void CreateToken(UsuarioVO userData, object userRole, out object jwtToken, int expireMinutes = 1)
        {
            if (userData == null) throw new ArgumentNullException(nameof(userData));

            var dateTimeProvider = new UtcDateTimeProvider();
            var dtNow = dateTimeProvider.GetNow();
            var dtExp = dtNow.AddMinutes(expireMinutes);

            var sub = userData.NomeLogin;
            var jti = $"jwt-id-{ Guid.NewGuid() }";
            var aud = "http://api.univag.edu.br";
            var iss = "http://api.univag.edu.br";
            var iat = UnixEpoch.GetSecondsSince(dtNow);
            var exp = UnixEpoch.GetSecondsSince(dtExp);
            var nbf = UnixEpoch.GetSecondsSince(dtNow);


            // Payload
            var payload = new Dictionary<string, object>
            {
                { "jti", jti }, // jti (JWT ID)               Representa um identificador exclusivo do token
                { "aud", aud }, // aud (Audience)             Representa o servidor de destino no qual o token será usado
                { "iss", iss }, // iss (Issuer)               Representa o servidor de autenticação que emitiu o token
                { "iat", iat }, // iat (Issued At)            Representa o tempo de criação do token em formato UNIX
                { "exp", exp }, // exp (Expiration Time)      Representa o tempo de expiração do token em formato UNIX
                { "nbf", nbf }, // nbf (Not Before)           Representa o tempo antes do qual o token NÃO DEVE ser aceito
                { "sub", sub }, // sub (Subject)              Representa a entidade a quem o token pertence, normalmente o Id do usuário
                { "name", Util.TrimLR(userData.Nome) },   //  Public claim
                { "email", Util.TrimLR(userData.Email) }, //  Public claim
                { "roles", userRole },                    //  Public claim
            };

            // JWT-Algorithm
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            // Token
            var token = encoder.Encode(payload, AppSettings.SecretKey);

            // JWT-Token
            jwtToken = new
            {
                authenticated = true,
                created = dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dtExp.ToString("yyyy-MM-dd HH:mm:ss"),
                access_token = token,
                message = "OK"
            };
        }



        /// <summary>
        /// Verifica a assinatura no Token
        /// </summary>
        /// <param name="data">Token no padrão JWT</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        [Route("VerifySignature"), HttpPost]
        public HttpResponseMessage VerifySignature(JObject data)
        {
            HttpResponseMessage response;

            try
            {
                var _data = (dynamic)data;

                string jwt = _data?.token;

                // Verifica: Token
                if (jwt != null)
                {
                    string[] parts = jwt.Split(".".ToCharArray());

                    if (parts.Count() == 3)
                    {
                        var header = parts[0];
                        var payload = parts[1];
                        var signature = parts[2];

                        byte[] bytesToSign = getBytes(string.Join(".", header, payload));

                        byte[] secret = getBytes(AppSettings.SecretKey);

                        var hash = new HMACSHA256(secret).ComputeHash(bytesToSign);

                        var computedSignature = Base64UrlEncode(hash);

                        var isValidSignature = string.Equals(signature, computedSignature);

                        // Message
                        var message = new
                        {
                            header,
                            payload,
                            signature,
                            token = (header + ". " + payload + ". " + signature),
                            secret,
                            isValidSignature
                        };

                        // 200 - Success
                        response = Request.CreateResponse(HttpStatusCode.OK, message);
                    }
                    else
                    {
                        // 400 - BadRequest
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "O token informado não é um JWT válido!" });
                    }
                }
                else
                {
                    // 400 - BadRequest
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "O parâmetro 'token' não foi fornecido." });
                }
            }
            catch (Exception ex)
            {
                // 500 - InternalServerError
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        private static byte[] getBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        // from JWT spec
        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }


    }
}