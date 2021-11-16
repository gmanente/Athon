using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Newtonsoft.Json;

namespace Sistema.Web.RepositorioWebAPI.Tools
{
    public class AngularTools
    {
        public class AngularSession
        {
            public string ControllerName { get; set; }
            public string ModuleName { get; set; }
            public string SubModuloUrl { get; set; }
        }

        public static class Modules
        {
            public const string Compra = "appCompras";
            public const string Datanorte = "appDatanorte";
            public const string Sistema = "appSistema";
            public const string Productive = "appProductive";
        }

        public class AngularComponents
        {
            public string[] Inject { get; set; }
            public List<string> Import { get; set; }
        }

        public class MyComponents
        {
            private const string Initial = "Angular158 | Routes | Animate | Messages | Growl | {module} {plugins}";
            private const string Filters = "{filters}";
            private const string Default = "Breadcumb | AuthorizationInterceptor | ErrorInterceptor | InterceptorsConfig | ErrorRequest | Seguranca | {factory} | {controller}";
            //private const string Default = "{module} {filters} | Breadcumb | ErrorRequest | Seguranca | {factory} | {controller}";
            public const string LocalePtBr = "LocalePtBr";
            public const string SmartTable = "SmartTable";
            public const string Summernote = "Summernote";
            public const string SummernoteManual = "SummernoteManual";
            public const string FabForm = "FabForm";
            public const string AngularStrap = "AngularStrap | AngularStrapTPL ";
            public const string Sanitize = "Sanitize";
            public const string SweetAlert = "SweetAlert";
            public const string SweetAlert2 = "SweetAlert2";
            public const string JQueryAndBootstrap = "JQuery | Bootstrap";
            public const string JQueryMaskedInput = "JQueryMaskedInput";
            public const string MomentWithLocales = "MomentWithLocales";
            public const string UiSelect = "UiSelect";
            public const string FiltersBR = "FiltersBR";
            public const string MassAutoComplete = "MassAutoComplete";
            public const string Currency = "Currency";
            public const string CapitalizeLetter = "CapitalizeLetter@";
            public const string Trim = "Trim@";
            public const string JStore = "JStore";
            public const string AngularCookie = "AngularCookie";
            public const string BootstrapSwitch = "BootstrapSwitch";
            public const string AngularBootstrapSwitch = "AngularBootstrapSwitch";
            public const string UiBootstrap = "UiBootstrap | UiBootstrapTypeahead";
            public const string UiMasks = "UiMasks";
            public const string UiMask = "UiMask";
            public const string Mask = "Mask";
            public const string TagsInput = "TagsInput";
            public const string AngularFileUpload = "AngularFileUpload";
            public const string AngularFilter = "AngularFilter";
            public const string NgCrossfilter = "NgCrossfilter";
            public const string Moment = "Moment";
            public const string Charts = "ChartJs | Chart";
            public const string TcCharts = "ChartJs | Chart | TcChart";
            public const string CleaveJs = "CleaveJs";
            public const string AngularMomentPicker = "AngularMomentPicker";
            public const string AngularFullScreen = "AngularFullScreen";
            public const string ngImgCrop = "ngImgCrop";
            public const string FroalaEditor = "froala";
            public const string IconPicker = "iconpicker";
            public const string Slim = "Slim";

            public List<Component> LstComponent { get; set; }
            public MyComponents()
            {
                LstComponent = new List<Component>();
                /* APP MODULES */
                NewComponent("appDatanorte", null, "View/js/angular/appdatanorte.module.js", true);
                NewComponent("appProductive", null, "View/js/angular/appproductive.module.js", true);
                NewComponent("appSistema", null, "View/js/angular/appsistema.module.js", true);

                /* DEAFULT COMPONENTS */
                NewComponent("Angular158", null, "View/js/angular/lib/angular-1.5.8/angular.min.js");
                NewComponent("Angular167", null, "View/js/angular/lib/angular-1.6.7/angular.min.js");
                NewComponent("ErrorRequest", null, "View/Js/angular/services/errorrequest.service.js");
                NewComponent("Seguranca", null, "View/Js/angular/factorys/seguranca.factory.js");
                NewComponent("Breadcumb", null, "View/Js/angular/directives/breadcumb.directive.js");
                NewComponent("AuthorizationInterceptor", null, "View/Js/angular/interceptors/authorizationInterceptor.js");
                NewComponent("ErrorInterceptor", null, "View/Js/angular/interceptors/errorInterceptor.js");
                NewComponent("InterceptorsConfig", null, "View/Js/angular/config/interceptorsConfig.js");

                /* PLUGINS */
                NewComponent("LocalePtBr", "ngLocale", "View/js/angular/modules/angular-locale_pt-br.js");
                NewComponent("Routes", "ngRoute", "View/js/angular/modules/angular-route.min.js");
                NewComponent("Messages", "ngMessages", "View/js/angular/modules/angular-messages.min.js");
                NewComponent("Growl", "angular-growl", "View/js/angular/modules/angular-growl.min.js");
                NewComponent("Animate", "ngAnimate", "View/js/angular/modules/angular-animate.min.js");
                NewComponent("Summernote", "summernote", "View/js/angular/modules/angular-summernote.min.js");
                NewComponent("SummernoteManual", "summernote", "View/js/angular/modules/angular-summernote-new.js");
                NewComponent("SummernoteJQuery", null, "View/SmartAdmin/Js/plugin/summernote/summernote.min.js");
                NewComponent("SmartTable", "smart-table", "View/js/angular/modules/smart-table.min.js");
                NewComponent("FabForm", "ngFabForm", "View/js/angular/modules/ng-fab-form.js");
                NewComponent("AngularStrap", "mgcrea.ngStrap", "View/js/angular/modules/angular-strap.js");
                NewComponent("AngularStrapTPL", null, "View/js/angular/modules/angular-strap.tpl.js");
                NewComponent("Sanitize", "ngSanitize", "View/js/angular/modules/angular-sanitize.js");
                NewComponent("SweetAlert", null, "View/Js/sweet-alert.min.js");
                NewComponent("SweetAlert2", null, "View/Js/sweetalert2.min.js");
                NewComponent("JQuery", null, "View/Js/jquery.min.js");
                NewComponent("JQueryMaskedInput", null, "View/Js/jquery.maskedinput.min.js");
                NewComponent("MomentWithLocales", null, "View/Js/moment-with-locales.js");
                NewComponent("Bootstrap", null, "View/SmartAdmin/Js/bootstrap/bootstrap.min.js");
                NewComponent("UiSelect", "ui.select", "View/js/angular/modules/angular-ui-select.js");
                NewComponent("FiltersBR", "brasil.filters", "View/js/angular/filters/angular-ng-filters-br.js");
                NewComponent("AngularFilter", "angular.filter", "View/js/angular/modules/angular-filter.js");
                NewComponent("MassAutoComplete", "MassAutoComplete", "View/js/angular/modules/massautocomplete.min.js");
                NewComponent("Currency", "ng-currency", "View/js/angular/modules/ng-currency.min.js");
                NewComponent("CapitalizeLetter", null, "View/js/angular/filters/primeiraletramaiuscula.filter.js");
                NewComponent("Trim", null, "View/js/angular/filters/trim.filter.js");
                NewComponent("JStore", "angular-jstore", "View/Js/angular/modules/angular-jstore.min.js");
                NewComponent("AngularCookie", "ngCookies", "View/js/angular/modules/angular-cookies.min.js");
                NewComponent("BootstrapSwitch", null, "View/js/bootstrap-switch.js");
                NewComponent("AngularBootstrapSwitch", "frapontillo.bootstrap-switch", "View/js/angular/modules/angular-bootstrap-switch.js");
                NewComponent("UiBootstrap", "ui.bootstrap", "View/js/angular/modules/ui-bootstrap-custom-2.5.0.min.js");
                NewComponent("UiBootstrapTypeahead", "ui.bootstrap.typeahead", "View/js/angular/modules/ui-bootstrap-custom-tpls-2.5.0.min.js");
                NewComponent("UiMasks", "ui.utils.masks", "View/js/angular/modules/angular-input-masks-standalone.min.js");
                NewComponent("UiMask", "ui.mask", "View/js/angular/modules/mask.min.js");
                NewComponent("Mask", "ngMask", "View/js/angular/modules/ngMask.min.js");
                NewComponent("TagsInput", "ngTagsInput", "View/js/angular/modules/ng-tags-input.min.js");
                NewComponent("AngularFileUpload", "angularFileUpload", "View/js/angular/modules/angular-file-upload.min.js");
                NewComponent("NgCrossfilter", "ngCrossfilter", "View/js/angular/filters/ng-crossfilter.js");
                NewComponent("Moment", null, "View/Js/moment.js");
                NewComponent("Chart", "chart.js", "View/js/angular/modules/angular-chart.js");
                NewComponent("ChartJs", null, "View/js/Chart.js");
                NewComponent("CleaveJs", "cleave.js", "View/js/angular/modules/cleave-angular.min.js");
                NewComponent("Slim", "slim", "View/js/lib/slim-image-cropper-v521/slim.angular.js");

                NewComponent("TcChart", "tc.chartjs", "View/js/angular/modules/tc-angular-chartjs.js");
                NewComponent("AngularMomentPicker", "moment-picker", "View/js/angular/modules/angular-moment-picker.min.js");
                NewComponent("froala", "froala", "View/js/angular/modules/ng-ckeditor.js");
                NewComponent("AngularFullScreen", "FBAngular", "View/js/angular/modules/angular-fullscreen.js");
                NewComponent("ngImgCrop", "ngImgCrop", "View/js/angular/modules/ng-img-crop.js");
                NewComponent("iconpicker", null, "View/Js/fontawesome-iconpicker.js");
            }

            private void NewComponent(string name, string inject, string diretory, bool localDirectory = false)
            {
                try
                {
                    LstComponent.Add(new Component(name, inject, diretory, localDirectory));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static Component GetValue(string name, MyComponents myCompoments)
            {
                try
                {
                    var component = myCompoments.LstComponent.FirstOrDefault(x => x.Name == name);

                    if (component != null) return component;
                    else if (name.Contains("Controller"))
                    {
                        var nameImport = name.Replace("Controller", "").ToLower();
                        return new Component(name, null, string.Format("View/js/angular/controllers/{0}.controller.js", nameImport), true);
                    }
                    else if (name.Contains("Factory"))
                    {
                        var nameImport = name.Replace("Factory", "").ToLower();
                        return new Component(name, null, string.Format("View/js/angular/factorys/{0}.factory.js", nameImport), true);
                    }
                    else
                    {
                        return new Component(name, null, string.Format("View/js/{0}.js", name), true);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static List<Component> GetAllOut(List<string> names, out List<string> componentImports)
            {
                try
                {
                    //var itemJQuery = names.FirstOrDefault(x => x.Contains("JQuery"));
                    //names.Remove(itemJQuery);
                    string itemJQuery = "";
                    var itensJQuery = names.Where(x => x.Contains("JQuery")).ToList();

                    foreach (var item in itensJQuery)
                    {
                        itemJQuery += "| " + item;
                        names.Remove(item);
                    }

                    itemJQuery = itemJQuery.Substring(1);

                    var itemSweetAlert = names.FirstOrDefault(x => x.Contains("SweetAlert"));
                    names.Remove(itemSweetAlert);

                    var itemModule = names.FirstOrDefault(x => x.Substring(0, 3) == "app");
                    string ModuleName = itemModule;
                    names.Remove(itemModule);

                    var itemFilters = names.Where(x => x.Contains("@")).ToList();
                    //string Padrao = Default;
                    string injectF = "";

                    if (itemFilters != null && itemFilters.Any())
                    {
                        foreach (var item in itemFilters)
                        {
                            var it = names.FirstOrDefault(x => x == item);
                            if (it != null)
                                names.Remove(it);
                        }
                        itemFilters = itemFilters.Select(x => x.Replace("@", "")).ToList();
                        injectF = Filters.Replace("{filters}", string.Join("", itemFilters.Select(x => " | " + x)));
                    }
                    else injectF = Filters.Replace("{filters}", "");

                    string ControllerName = HttpContext.Current.Request.Path.Substring(HttpContext.Current.Request.Path.LastIndexOf("/") + 1).Replace(".aspx", "");
                    string injectPlugin = Initial.Replace("{module}", ModuleName).Replace("{plugins}", string.Join("", names.Select(x => " | " + x)));
                    string injectCF = Default.Replace("{controller}", string.Format("{0}{1}", ControllerName, "Controller"))
                                             .Replace("{factory}", string.Format("{0}{1}", ControllerName, "Factory"));

                    componentImports = new List<string>();

                    if (itemSweetAlert != null)
                    {
                        componentImports.Add(itemSweetAlert);
                    }
                    if (itemJQuery != null)
                    {
                        componentImports.Add(itemJQuery);
                        if (names.Contains("Summernote"))
                            componentImports.Add("SummernoteJQuery");
                    }
                    if (!string.IsNullOrEmpty(injectF))
                    {
                        injectPlugin += injectF;
                    }

                    componentImports.Add(injectPlugin);
                    componentImports.Add(injectCF);

                    return GetAll(componentImports);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public static List<Component> GetAll(List<string> names)
            {
                try
                {
                    var myCompoments = new MyComponents();
                    var LstComponents = new List<Component>();

                    foreach (string item in names)
                    {
                        foreach (string name in item.Split('|'))
                            LstComponents.Add(GetValue(name.Trim(), myCompoments));
                    }

                    return LstComponents;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public class Component
        {
            public Component(string name, string inject, string directory, bool localDirectory = false)
            {
                Name = name;
                Inject = inject;
                Directory = directory;
                LocalDirectory = localDirectory;
            }

            public string Name { get; set; }
            public string Inject { get; set; }
            public string Directory { get; set; }
            public bool LocalDirectory { get; set; }
        }

        [ComVisible(true)]
        [Flags]
        public enum RouteIds
        {
            /// <summary>
            /// WRF: WithoutRF
            /// </summary>
            WRF = 100,
            /// <summary>
            /// WC: WithoutCredentials
            /// </summary>
            WC = 200,
        }

        public static string EnumToJson(Type type)
        {
            if (!type.IsEnum)
                throw new InvalidOperationException("enum expected");

            var results =
                Enum.GetValues(type).Cast<object>()
                    .ToDictionary(enumValue => enumValue.ToString(), enumValue => (int)enumValue);

            return string.Format("{{ \"{0}\" : {1} }}", type.Name, JsonConvert.SerializeObject(results));
        }

        public static void SetAngularComponents(string[] components)
        {
            try
            {
                var import = components.ToList();
                var LstComponents = MyComponents.GetAllOut(import, out import);
                var inject = LstComponents.Where(x => !string.IsNullOrEmpty(x.Inject)).Select(x => x.Inject).ToArray();
                var angularComponents = new AngularComponents();

                angularComponents.Inject = inject;
                angularComponents.Import = import;

                WebTools.SetCookie("AngularComponents", angularComponents);
                HttpContext.Current.Session["AngularComponents"] = angularComponents;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}