using JWT;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Sistema.Web.API
{
    public class AuthHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            var authHeader = request.Headers.Authorization;
            var methodName = Path.GetFileName(request.RequestUri.ToString());

            if (authHeader == null || methodName == "Authorize")
                return base.SendAsync(request, cancellationToken);

            string token;
            if (!TryRetrieveToken(request, out token))
                return base.SendAsync(request, cancellationToken);

            try
            {
                // Current Principal
                Thread.CurrentPrincipal = GetPrincipal(ReadToken(token));

                // Current User
                if (HttpContext.Current != null)
                    HttpContext.Current.User = Thread.CurrentPrincipal;
            }
            catch (SignatureVerificationException ex)
            {
                response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response != null
                ? Task.FromResult(response)
                : base.SendAsync(request, cancellationToken);
        }


        private static string ReadToken(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                    throw new ArgumentException("O Token deve consistir em 3 partes delimitadas por ponto.");

                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                // Decode JWT payload
                var payloadJson = decoder.Decode(token, AppSettings.SecretKey, verify: true);

                return payloadJson;
            }
            catch (TokenExpiredException)
            {
                throw new TokenExpiredException("O Token fornecido expirou.");
            }
            catch (SignatureVerificationException)
            {
                throw new SignatureVerificationException("O Token tem assinatura inválida.");
            }
        }

        private static ClaimsPrincipal GetPrincipal(string payloadJson)
        {
            try
            {
                // Subject
                var subject = new ClaimsIdentity("JWT", ClaimTypes.Name, ClaimTypes.Role);

                // Claims
                var claims = new List<Claim>();

                // Payload Data
                var payloadData = JsonConvert.DeserializeObject<Dictionary<string, object>>(payloadJson);
                if (payloadData != null)
                    foreach (var pair in payloadData)
                    {
                        var claimType = pair.Key;

                        var source = pair.Value as ArrayList;
                        if (source != null)
                        {
                            claims.AddRange(from object item in source select new Claim(claimType, item.ToString(), ClaimValueTypes.String));
                            continue;
                        }

                        switch (pair.Key)
                        {
                            case "name":
                                claims.Add(new Claim(ClaimTypes.Name, pair.Value.ToString(), ClaimValueTypes.String));
                                break;
                            case "surname":
                                claims.Add(new Claim(ClaimTypes.Surname, pair.Value.ToString(), ClaimValueTypes.String));
                                break;
                            case "email":
                                claims.Add(new Claim(ClaimTypes.Email, pair.Value.ToString(), ClaimValueTypes.Email));
                                break;
                            case "role":
                                claims.Add(new Claim(ClaimTypes.Role, pair.Value.ToString(), ClaimValueTypes.String));
                                break;
                            case "roles":
                                dynamic lstRoles = pair.Value;
                                foreach (var role in lstRoles)
                                    claims.Add(new Claim(ClaimTypes.Role, role.Value, ClaimValueTypes.String));
                                break;
                            case "userId":
                                claims.Add(new Claim(ClaimTypes.UserData, pair.Value.ToString(), ClaimValueTypes.Integer));
                                break;
                            default:
                                claims.Add(new Claim(claimType, pair.Value.ToString(), ClaimValueTypes.String));
                                break;
                        }
                    }

                subject.AddClaims(claims);

                return new ClaimsPrincipal(subject);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}