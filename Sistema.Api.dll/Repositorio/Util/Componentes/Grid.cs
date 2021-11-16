using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Grid : AbstractComponent
    {
        public string paginaAtual { get; set; }
        public string Tema { get; set; }
        public string Ordenacao { get; set; }
        public string Condencado { get; set; }
        public string Zebrado { get; set; }
        public string Limpo { get; set; }
        public string Borda { get; set; }
        public string AjaxCall { get; set; }
        public string Paginacao { get; set; }
        public long TotalRegistro { get; set; }
        public string DescricaoTotalResgistro { get; set; }
        public string InjectHeader { get; set; }
        private BtnDropDown btnDrop;
        public string ClassName { get; set; }
        public GroupComponent Content { get; set; }

        public Grid()
            : base()
        {
            Id = "grid";
            Borda = "true";
            Tema = "stats";
            Condencado = "true";
            Zebrado = "true";
            Ordenacao = "true";
            Limpo = "true";

            Content = new GroupComponent();
        }


        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }

        // MontarGrid
        /// <summary>
        /// Componente Grid pode ser usado de 3 maneiras...
        /// </summary>
        ///
        /// <example>
        /// Exemplo de uso:
        /// - Pessoa
        /// 1° Simples:  "Nome Pessoa:nome" => retorno Titulo= "Nome Pessoa", Valor = "Michael S. lopes";
        /// 2° Encadeada:"Descricao Endereco:Pessoa:Endereco->descricao" => retorno Titulo= "Descricao Endereco", Valor = "Nova Esperança";
        /// 3° Com máscara : "Data de Nascimento:dataNascimento[{0:M/d/yyyy}]" => retorno Titulo= "Data de Nascimento", Valor = "28/11/1989";
        ///
        /// A máscara pode ser utilizada para formatar moeda, data e o que for encontrado na documentação abaixo;
        /// URL:https://sites.google.com/site/tecguia/formatar-string-c-string-format
        /// Mascaras mais ultilizadas
        /// Moeda : "{0:C}" retorno R$ 1,00
        /// Data : "{0:dd/MM/yyyy}" retorno 08/02/2014
        /// </example>
        ///
        /// <typeparam name="T"></typeparam>
        /// <param name="lista"></param>
        /// <param name="binding"></param>
        /// <param name="className">String Acrescenta um nome de classe para a grid (OPCIONAL)</param>
        /// <param name="resizer">Bool Ativa o uso do plugin jQuery Resizer na grid. Padrão=True (OPCIONAL)</param>
        public void MontarGrid<T>(List<T> lista, string[] binding, string className = null, bool resizer = true)
        {
            var tHead = new StringBuilder();
            var tBody = new StringBuilder();

            if (className != null)
                ClassName = className;

            //Monta cabecalho
            Type tp = typeof(T);
            tHead.AppendLine("<thead>");
            if (btnDrop != null)
            {
                if (resizer == true)
                    tHead.AppendLine("<th data-resizable-column-id='#' style='width:5%'>Funções</th>");
                else
                    tHead.AppendLine("<th style='width:5%'>Funções</th>");
            }

            int rescol = 0;
            foreach (string b in binding)
            {
                rescol++;
                var bdg = GetBinding(b);
                if (!bdg.Coluna.ToLower().Equals("id") && bdg.Injetion.Equals(""))
                {
                    if (resizer == true)
                        tHead.AppendLine(@"<th data-resizable-column-id='rescol" + rescol + "' style='text-align:center;'>" + bdg.Coluna + "</th>");
                    else
                        tHead.AppendLine(@"<th style='text-align:center;'>" + bdg.Coluna + "</th>");
                }
            }
            tHead.AppendLine("</tr>");
            tHead.AppendLine("</thead>");
            //fim monta cabecalho

            //Monta corpo
            tBody.AppendLine(@"<tbody>");

            if (lista.Count == 0)
            {
                tBody.AppendLine(@"<tr id='table-row-%rowId%' %Injection%>");
                tBody.AppendLine("<td colspan=\"100\" style=\"padding: 20px !important;\"><i class=\"fa fa-info-circle\"></i>&nbsp;Não foram encontrados registros para esta consulta.</td>");
                tBody.AppendLine(@"</tr>");
            }
            else
            {
                foreach (var coluna in lista)
                {
                    int id = 0;
                    tBody.AppendLine(@"<tr id='table-row-%rowId%' %Injection%>");

                    if (btnDrop != null)
                        tBody.AppendLine("<td>%btnDrop%</td>");

                    foreach (string b in binding)
                    {
                        var bdg = GetBinding(b);
                        if (!bdg.Coluna.ToLower().Equals("id"))
                        {
                            var objValue = GetValue(bdg, coluna);
                            if (bdg.Injetion.Equals(""))
                            {
                                if (bdg.Mask != "")
                                    tBody.AppendLine(@"<td style='text-align:center;'>" + GetMask(objValue, bdg.Mask) + "</td>");
                                else if (bdg.Component != "")
                                    tBody.AppendLine(@"<td style='text-align:center;'>" + GetComponent(bdg.Component) + "</td>");
                                else if (bdg.Badge != "")
                                {
                                    var arrBadge = bdg.Badge.Split(new string[] { "->" }, StringSplitOptions.None);
                                    var bdgAux = new Binding()
                                    {
                                        Referencia = arrBadge[0],
                                        SubReferencia = arrBadge
                                    };
                                    var badgeValue = GetValue(bdgAux, coluna);
                                    tBody.AppendLine(@"<td style='text-align:center;'>" + GetBadge(objValue, badgeValue.ToString()) + "</td>");
                                }
                                else
                                    tBody.AppendLine(@"<td style='text-align:center;'>" + CheckBoolean(Convert.ToString(objValue)) + "</td>");
                            }
                            else
                            {
                                tBody.Replace("%Injection%", bdg.Injetion + "='" + objValue + "' %Injection%");
                            }

                        }
                        else if (btnDrop != null)
                        {
                            id = Convert.ToInt32(GetValue(bdg, coluna));
                            SetIdListaItemMenu(Convert.ToInt32(GetValue(bdg, coluna)));
                        }
                    }

                    if (tBody.ToString().Contains("Injection"))
                        tBody.Replace("%Injection%", "");

                    tBody.Replace("%rowId%", id.ToString());
                    tBody.AppendLine(@"</tr>");

                    if (btnDrop != null)
                        tBody.Replace("%btnDrop%", btnDrop.ToString());
                }
            }

            tBody.AppendLine(@"</tbody>");
            //fim monta corpo

            SbComponent = new StringBuilder();
            if (TotalRegistro > 0)
                SbComponent.AppendLine("<br/><span class='label label-primary'>" + DescricaoTotalResgistro + ": " + TotalRegistro + "</span>");

            if (!string.IsNullOrEmpty(InjectHeader))
                SbComponent.AppendLine(InjectHeader);

            SbComponent.Append("<div class='table-responsive'>");
            SbComponent.Append("<table ");
            SbComponent.Append("id='").Append(Id).Append("' ");
            SbComponent.Append("data-resizable-columns-id='").Append(ClassName).Append("' ");
            SbComponent.Append("class='table table-hover table-striped grid-" + ClassName + "").Append(Class).Append("'> ");
            SbComponent.AppendLine(tHead.ToString());
            SbComponent.AppendLine(tBody.ToString());
            SbComponent.AppendLine("</table>");
            SbComponent.AppendLine("</div>");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.AppendLine(Paginacao);

            SbComponent.AppendLine("<script>if (typeof afterGridLoaded == 'function') afterGridLoaded()</script>");
            SbComponent.AppendLine(GetScript(resizer));
        }

        private string CheckBoolean(string param)
        {

            if (param.Equals("True"))
            {
                return "<i class='fa fa-check-square'></i>";
            }
            else if (param.Equals("False"))
            {
                return "<i class='fa fa-square-o'></i>";
            }
            else
            {
                return param;
            }
        }

        private string GetBadge(object obj, string color)
        {
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(color))
                    color = "bg-color-" + color;
                return "<span class='badge " + color + "'>" + obj + "</span>";
            }
            else
            {
                return "";
            }
        }

        private string GetMask(object obj, string mask)
        {
            if (obj != null)
            {
                try
                {
                    return String.Format(mask, Convert.ToDecimal(obj));
                }
                catch
                {
                    return String.Format(mask, obj);
                }
            }
            else
            {
                return " - ";
            }
        }

        private string GetComponent(string componentName)
        {
            if (componentName.ToLower().Equals("checkbox"))
            {
                return "<input type='checkbox' class='checkbox-grid'/>";
            }

            return "";
        }

        private string GetScript(bool resizer)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.AppendLine("<script type='text/javascript'>");
            sbScript.AppendLine("$( document ).ready(function() {");
            sbScript.AppendLine("$('#" + Id + "').tablecloth({");
            sbScript.Append("theme: '").Append(Tema).Append("',");
            sbScript.Append("bordered:").Append(Borda).Append(",");
            sbScript.Append("condensed:").Append(Condencado).Append(",");
            sbScript.Append("striped:").Append(Zebrado).Append(",");
            sbScript.Append("sortable:").Append(Ordenacao).Append(",");
            sbScript.Append("cleanElements: 'th td'");
            sbScript.Append("});");

            //Rezizeble
            if (resizer == true)
            {
                sbScript.Append(" $('.grid-" + ClassName + "').resizableColumns(); ");
            }

            sbScript.Append("});");

            sbScript.Append("</script>");
            if (!string.IsNullOrEmpty(AjaxCall))
            {
                sbScript.Append(AjaxCall);
            }
            return sbScript.ToString();
        }

        public void SetBtnFuncoes(BtnDropDown btn)
        {
            btnDrop = btn;
        }

        private void SetIdListaItemMenu(long id)
        {
            foreach (var i in btnDrop.ListaItemMenu())
            {
                i.DataId = id.ToString();
            }
        }


        private Binding GetBinding(string str)
        {
            var obj = str.Split(new char[] { '[', ']' });
            var arrAux = obj[0].Split(new char[] { '(', ')' });
            var arrStr = arrAux[0].Split(':');
            var arr = arrStr[1].Split(new string[] { "->" }, StringSplitOptions.None);
            var arrComponente = arrStr[1].Split(new char[] { '{', '}' });
            var injetion = "";
            var component = "";
            var badge = "";

            if (obj.Length > 1)
            {
                var arrInjetion = obj[1].Split(':');
                if (arrInjetion.Length > 1)
                {
                    switch (arrInjetion[0])
                    {
                        case "injection":
                            injetion = arrInjetion[1];
                            break;
                    }
                }
            }

            if (arrAux.Length > 1)
            {
                var arrBadge = arrAux[1].Split(':');
                if (arrBadge.Length > 1)
                {
                    switch (arrBadge[0])
                    {
                        case "badge":
                            badge = arrBadge[1];
                            break;
                    }
                }
            }

            if (arrComponente.Length > 0 && arrComponente[0].Equals("Component"))
            {
                component = arrComponente[1];
            }

            var b = new Binding()
            {
                Coluna = arrStr[0],
                Referencia = arr[0],
                SubReferencia = arr,
                Mask = obj.Length > 1 ? obj[1] : "",
                Injetion = injetion,
                Component = component,
                Badge = badge
            };

            return b;
        }

        private class Binding
        {
            public string Coluna { get; set; }
            public string[] SubReferencia { get; set; }
            public string Referencia { get; set; }
            public string Mask { get; set; }
            public string Injetion { get; set; }
            public string Component { get; set; }
            public string Badge { get; set; }
        }

        private object GetValue(Binding b, object obj)
        {
            object objRetorno = new object();
            objRetorno = "-";
            if (obj != null)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(b.Referencia);
                if (propertyInfo != null)
                {
                    if (ReflectionHandler.IsPrimitive(propertyInfo.PropertyType) ||
                        ReflectionHandler.IsNullable(propertyInfo.PropertyType))
                    {
                        objRetorno = propertyInfo.GetValue(obj);
                    }
                    else
                    {
                        objRetorno = GetValue(b.SubReferencia, obj);
                    }
                }
            }
            return objRetorno;
        }

        private object GetValue(string[] nameProperty, object obj)
        {
            object objLocal = obj;
            object objRetorno = new object();
            objRetorno = "-";
            foreach (string name in nameProperty)
            {
                if (objLocal != null)
                {
                    PropertyInfo propertyInfo = objLocal.GetType().GetProperty(name);
                    if (propertyInfo != null)
                    {
                        if (ReflectionHandler.IsPrimitive(propertyInfo.PropertyType) ||
                            ReflectionHandler.IsNullable(propertyInfo.PropertyType))
                        {
                            if (objLocal != null && propertyInfo != null)
                                objRetorno = propertyInfo.GetValue(objLocal);
                        }
                        else
                        {
                            objLocal = propertyInfo.GetValue(objLocal);
                        }
                    }
                }
            }
            return objRetorno;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public virtual void Render()
        {
            base.Render();
        }
    }

    public static class Tema
    {
        public static string Default { get { return "default"; } }
        public static string Dark { get { return "dark"; } }
        public static string Stats { get { return "stats"; } }
        public static string Paper { get { return "paper"; } }
    }

}
