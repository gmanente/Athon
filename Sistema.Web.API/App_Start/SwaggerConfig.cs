using Sistema.Web.API.SwaggerExt;
using Swashbuckle.Application;
using Swashbuckle.Examples;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sistema.Web.API
{
    public class SwaggerConfig
    {
        static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\" + Assembly.GetExecutingAssembly().GetName().Name + ".XML", AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void Register()
        {
            // Swagger referencia
            var swaggerAssembly = typeof(SwaggerConfig).Assembly;

            // Swagger configuration
            GlobalConfiguration.Configuration
                .EnableSwagger("docs/{apiVersion}", c =>
                {
                    // Config
                    c.IgnoreObsoleteActions();
                    c.DescribeAllEnumsAsStrings();
                    //c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());
                    //c.OrderActionGroupsBy(new DescAlphaComparer());

                    // API Versions
                    // see: https://stackoverflow.com/questions/44376295/how-to-configure-multipleapiversions-in-swashbuckle-using-aspnet-apiversioning
                    var apiExplorer = GlobalConfiguration.Configuration.AddVersionedApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        options.SubstituteApiVersionInUrl = true;
                    });

                    if (AppSettings.EnableMultipleApiVersions == true)
                    {
                        // MultipleApiVersions
                        c.MultipleApiVersions(
                            (apiDesc, version) => apiDesc.GetGroupName() == version,
                            info =>
                            {
                                foreach (var group in apiExplorer.ApiDescriptions)
                                {
                                    var description = "" +
                                        "Serviços REST para gerenciar seu ecossistema API.<br /><br />" +
                                        "Acesso e autenticação<br />" +
                                        "- O acesso às APIs é feito com o uso de um Token, gerado após validação do seu login e senha com o serviço <a><strong>Authorize</strong></a>.<br />" +
                                        "- Após a validação do acesso o Token gerado deve ser incorporado no HEADER da requisição.<br />";
                                    if (group.IsDeprecated)
                                        description += "<br />" + " ** <b>ESTA VERSÃO DA API FOI DESCONTINUADA</b>.";

                                    info.Version(group.Name, $"API Repositório { group.ApiVersion }")
                                        .Description(description)
                                        //.TermsOfService("Shareware")
                                        //.License(lc => lc.Name("MIT").Url("https://opensource.org/licenses/MIT"))
                                        .Contact(cc => cc.Name("NTIC - Núcleo de Tecnologia de Informação e Comunicações.").Email("univagnpd@gmail.com"));
                                }
                            });
                    }
                    else
                    {
                        // SingleApiVersion
                        c.SingleApiVersion("v1", "API Repositório")
                            .Description("" +
                                "Serviços REST para gerenciar seu ecossistema API.<br /><br />" +
                                "Acesso e autenticação<br />" +
                                "- O acesso às APIs é feito com o uso de um Token, gerado após validação do seu login e senha com o serviço <a><strong>Authorize</strong></a>.<br />" +
                                "- Após a validação do acesso o Token gerado deve ser incorporado no HEADER da requisição.<br />" +
                                "")
                            .Contact(cc => cc.Name("NTIC - Núcleo de Tecnologia de Informação e Comunicações.").Email("univagnpd@gmail.com"));
                    }


                    // Integrate xml comments
                    c.IncludeXmlComments(GetXmlCommentsPath());

                    // Swashbuckle Operation Filters
                    c.OperationFilter<ExamplesOperationFilter>();              // Enable Swagger examples
                    c.OperationFilter<DescriptionOperationFilter>();           // Enable Swagger response descriptions
                    c.OperationFilter<AddResponseHeadersFilter>();             // Enable Swagger response headers

                    // Custom Operation Filters
                    c.OperationFilter<AddCustomTypes>();                       // Resolve request/response types
                    c.OperationFilter<AddDefaultValues>();                     // Resolve default values

                    // Custom Document Filters
                    c.DocumentFilter<FilterDocSortOperations>();


                    // OAuth2 Security
                    //c.OperationFilter<AddOperationSecurity>();

                    // Token Authorization
                    if (AppSettings.EnableTokenAuthorization == true)
                    {
                        c.ApiKey("Authorization")
                            .Description("Authorization format : Bearer {token}")
                            .Name("Authorization")
                            .In("header");
                    }
                })
                .EnableSwaggerUi("sandbox/{*assetPath}", c =>
                {
                    c.DisableValidator();
                    c.EnableDiscoveryUrlSelector();
                    //c.DocExpansion(DocExpansion.List);

                    c.InjectStylesheet(swaggerAssembly, "Sistema.Web.API.sandbox.ui.css.main.css");
                    c.CustomAsset("my_lang_translator", swaggerAssembly, "Sistema.Web.API.sandbox.ui.lang.translator.js");
                    c.CustomAsset("my_lang_pt", swaggerAssembly, "Sistema.Web.API.sandbox.ui.lang.pt.js");
                    c.CustomAsset("my_logo", swaggerAssembly, "Sistema.Web.API.sandbox.ui.img.logo.png");
                    c.CustomAsset("my_swagger_js", swaggerAssembly, "Sistema.Web.API.sandbox.ui.lib.swagger.min.js");
                    c.CustomAsset("index", swaggerAssembly, "Sistema.Web.API.sandbox.ui.index.html");


                    // Token Authorization
                    if (AppSettings.EnableTokenAuthorization == true)
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    }
                });
        }
    }
}