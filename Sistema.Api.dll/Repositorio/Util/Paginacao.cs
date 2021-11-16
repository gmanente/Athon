using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util
{
    /// <summary>
    /// Classe respossavel por paginar registro da grid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paginacao<T>
    {
        public int TotalRegistro { get; set; }
        public int QtdRegistroPagina { get; set; }
        public string Pagina { get; set; }
        private int Registro { get; set; }
        private List<T> ListaObjeto { get; set; }
        private bool Lazy { get; set; }

        public Paginacao()
        { }

        /// <summary>
        /// Gera o html para ser injetado na grid
        /// </summary>
        /// <returns>html</returns>
        public string GetHtmlPaginacao()
        {

            var sb = new StringBuilder();
            int pag = Pagina == null ? 1 : Convert.ToInt32(Pagina);
            double db = (double)TotalRegistro / (double)QtdRegistroPagina;
            int subTotal = (int)Math.Ceiling((decimal)db);
            int ultimaPag = subTotal;
            int prox = pag + 1;
            int ant = pag - 1;
            int penultima = ultimaPag - 1;
            int adjacentes = 2;

            if (ultimaPag > 0)
            {
                sb.AppendLine("<ul class='pagination' >");

                if (pag > 1)
                {
                    sb.AppendLine("<li><a Title='Página: "+ ant +"' style='cursor:pointer;' class='pagination-pag' data-pag=" + ant + ">&laquo; Anterior</a></li>");
                }


                if (ultimaPag <= 5)
                {
                    for (int i = 1; i < ultimaPag + 1; i++)
                    {
                        if (i == pag)
                        {
                            sb.AppendLine("<li class='active'><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                        }
                        else
                        {
                            sb.AppendLine("<li><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                        }
                    }
                }
                if (ultimaPag > 5)
                {
                    if (pag < 1 + (2 * adjacentes))
                    {

                        for (int i = 1; i < 2 + (2 * adjacentes); i++)
                        {
                            if (i == pag)
                            {
                                sb.AppendLine("<li class='active'><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                            }
                            else
                            {
                                sb.AppendLine("<li><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                            }
                        }
                        sb.AppendLine("<li><a>...</a></li>");
                        sb.AppendLine("<li><a Title='Página: " + Convert.ToString(penultima) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(penultima) + ">" + Convert.ToString(penultima) + "</a></li>");
                        sb.AppendLine("<li><a Title='Página: " + Convert.ToString(ultimaPag) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(ultimaPag) + ">" + Convert.ToString(ultimaPag) + "</a></li>");
                    }
                    else if (pag > (2 * adjacentes) && pag < ultimaPag - 3)
                    {
                        sb.AppendLine("<li><a class='pagination-pag' data-pag=1>1</a></li>");
                        sb.AppendLine("<li><a class='pagination-pag' data-pag=1>2</a></li><li><a>...</a></li>");
                        for (int i = pag - adjacentes; i <= pag + adjacentes; i++)
                        {
                            if (i == pag)
                            {
                                sb.AppendLine("<li class='active'><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                            }
                            else
                            {
                                sb.AppendLine("<li><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                            }
                        }
                        sb.AppendLine("<li><a>...</a></li>");
                        sb.AppendLine("<li><a Title='Página: " + Convert.ToString(penultima) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(penultima) + ">" + Convert.ToString(penultima) + "</a></li>");
                        sb.AppendLine("<li><a Title='Página: " + Convert.ToString(ultimaPag) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(ultimaPag) + ">" + Convert.ToString(ultimaPag) + "</a></li>");

                    }
                    else
                    {
                        sb.AppendLine("<li><a Title='Página: 1' style='cursor:pointer;' class='pagination-pag' data-pag=1>1</a></li>");
                        sb.AppendLine("<li><a Title='Página: 1' style='cursor:pointer;' class='pagination-pag' data-pag=1>2</a></li><li><a>...</a></li>");
                        for (int i = ultimaPag - (4 + (2 * adjacentes)); i <= ultimaPag; i++)
                        {
                            if (i == pag)
                            {
                                sb.AppendLine("<li class='active'><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                            }
                            else if (i != 2 && i != 1 && i > 0)
                            {
                                sb.AppendLine("<li><a Title='Página: " + Convert.ToString(i) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(i) + ">" + Convert.ToString(i) + "</a></li>");
                            }
                        }
                    }

                }
            }

            if (prox <= ultimaPag && ultimaPag > 2)
            {
                sb.AppendLine("<li><a Title='Página: " + Convert.ToString(prox) + "' style='cursor:pointer;' class='pagination-pag' data-pag=" + Convert.ToString(prox) + ">Pr&oacute;xima &raquo;</a></li>");
            }
            return sb.AppendLine("</ul>").ToString();
        }

        private void SetLista(List<T> listaObjeto)
        {
            ListaObjeto = listaObjeto;
        }


        public void SetListaPaginacao(List<T> listaObjeto)
        {
            TotalRegistro = listaObjeto.Count();
            ListaObjeto = listaObjeto;
        }

        /// <summary>
        /// Deve ser ultilizado quando o metodo invocado ter 4 parametros
        /// Ex:"metodo(Pessoa pessoa, int limitInicio, int limitFim, bool isLike)"
        /// e deverá retornar  uma lista para ser paginada
        /// </summary>
        /// <typeparam name="Clazz"></typeparam>
        /// <param name="paramsContrutor"></param>
        /// <param name="metodoNome"></param>
        /// <param name="obj"></param>
        /// <param name="ordem"></param>
        public void SetPaginacao<Clazz>(object[] paramsContrutor, string metodoNome, string structs)
        {
            List<Type> objTypes = new List<Type>();
            foreach (object o in paramsContrutor)
            {
                objTypes.Add(o.GetType());
            }

            Type type = typeof(Clazz);
            Type[] argTypes = objTypes.ToArray();
            ConstructorInfo cInfo = type.GetConstructor(argTypes);
            object objeto = cInfo.Invoke(paramsContrutor);
            SetPaginacao(objeto, structs, metodoNome);
        }

        /// <summary>
        /// Deve ser ultilizado quando o metodo invocado ter 4 parametros
        /// Ex:"metodo(Pessoa pessoa, int limitInicio, int limitFim, bool isLike)"
        /// e deverá retornar  um Dictionary<total de registro, List<T>> para ser paginada
        /// </summary>
        /// <typeparam name="Clazz"></typeparam>
        /// <param name="metodoNome"></param>
        /// <param name="obj"></param>
        /// <param name="ordem"></param>
        public void SetPaginacao<Clazz>(string metodoNome, string structs)
        {
            Type type = typeof(Clazz);
            Lazy = true;
            object objeto = Activator.CreateInstance(type);
            SetPaginacao(objeto, structs, metodoNome);
        }

        private void SetPaginacao(object objeto, string structs, string metodoNome)
        {
            MethodInfo metodo = null;
            MethodInfo metodoFecharConexao = null;
            try
            {
                metodo = objeto.GetType().GetMethod(metodoNome);
                metodoFecharConexao = objeto.GetType().GetMethod("FecharConexao");

                int p = Pagina == null ? 1 : Convert.ToInt32(Pagina);
                Registro = (p * QtdRegistroPagina) - QtdRegistroPagina;

                var dLst = (Dictionary<int, List<T>>)metodo.Invoke(objeto, new object[] { structs, Registro, QtdRegistroPagina });
                foreach (KeyValuePair<int, List<T>> entry in dLst)
                {
                    TotalRegistro = entry.Key;
                    SetLista(entry.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                metodoFecharConexao.Invoke(objeto, new object[] { });
            }
        }

        public void SetPaginacao<Clazz>(string metodoNome, T obj)
        {
            Type type = typeof(Clazz);
            Lazy = false;
            object objeto = Activator.CreateInstance(type);
            SetPaginacao(objeto, obj, metodoNome);
        }


        private void SetPaginacao(object objeto, T obj, string metodoNome)
        {
            MethodInfo metodo = null;
            MethodInfo metodoFecharConexao = null;
            try
            {
                metodo = objeto.GetType().GetMethod(metodoNome);
                metodoFecharConexao = objeto.GetType().GetMethod("FecharConexao");
                int p = Pagina == null ? 1 : Convert.ToInt32(Pagina);
                Registro = (p * QtdRegistroPagina) - QtdRegistroPagina;

                Dictionary<int, List<T>> dLst = (Dictionary<int, List<T>>)metodo.Invoke(objeto, new object[] { obj });
                foreach (KeyValuePair<int, List<T>> entry in dLst)
                {
                    TotalRegistro = entry.Key;
                    SetLista(entry.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                metodoFecharConexao.Invoke(objeto, new object[] { });
            }
        }


        public List<T> GetLista()
        {
            if (Lazy)
            {
                return ListaObjeto;
            }
            else
            {
                if (ListaObjeto != null)
                {
                    IEnumerable<T> query = ListaObjeto;
                    return QtdRegistroPagina > 0
                                        ? query.Skip(Registro).Take(QtdRegistroPagina).ToList() // Paginacao
                                        : query.ToList();
                }
                else
                {
                    return ListaObjeto = new List<T>();
                }
            }

        }



        //public T GetObjetoPesquisa()
        //{
        //    string[] arr1 = Consulta.Split('-');
        //    Type type = typeof(T);
        //    object classT = Activator.CreateInstance(type);
        //    PropertyInfo[] properties = type.GetProperties();

        //    foreach (string arr2 in arr1)
        //    {
        //        string[] parm = arr2.Split(':');

        //        foreach (var propertyInfo in properties)
        //        {
        //            if (propertyInfo.Name == parm[0])
        //            {
        //                SetValue(propertyInfo.Name, parm[1], classT);
        //            }
        //        }
        //    }
        //    return (T)classT;
        //}

        private void SetValue(string nameProperty, object value, object obj)
        {
            PropertyInfo[] arrayPropertyInfo = obj.GetType().GetProperties();
            foreach (var propertyInfo in arrayPropertyInfo)
            {
                if (propertyInfo.Name == nameProperty)
                {
                    propertyInfo.SetValue(obj, value, null);
                }
            }
        }
    }
}
