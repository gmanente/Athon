using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Sistema.Api.dll.Repositorio.Util
{
    [Serializable]
    public class Ajax
    {
        public bool StatusOperacao { get; set; }
        public string ObjMensagem { get; set; }
        public string TextoMensagem { get; set; }
        public string TipoMensagem { get; set; }
        public string UrlRetorno { get; set; }
        public string UrlDownload { get; set; }
        private string Lista { get; set; }
        public string Variante { get; set; }
        public bool SweetAlert { get; set; }
        private Mensagem InsMensagem { get; set; }

        public Ajax()
        {
            SweetAlert = false;
            StatusOperacao = true;
            ObjMensagem = null;
            InsMensagem = new Mensagem();
            Lista = null;
            Variante = null;
            UrlRetorno = "NO";
            UrlDownload = "NO";
        }

        public void SetMessageSweetAlert(string title, string text, string tipo)
        {
            SweetAlert = true;
            TextoMensagem = title + "|" + text + "|" + tipo;
        }

        public void SetMessage(string mensagem, string tipo)
        {
            ObjMensagem = Mensagem.Show(mensagem, tipo);
            TextoMensagem = mensagem;
        }

        public void AddMessage(string mensagem, string tipo)
        {
            InsMensagem.Add(mensagem, tipo);
            ObjMensagem = InsMensagem.ShowAdded();
        }

        public void AddLista<T>(List<T> lista)
        {
            Lista = Json.Serialize(lista);
        }

        public static dynamic JsonToDynamic(string strJson)
        {
            return JObject.Parse(strJson);
        }

        public string GetAjaxJson()
        {
            InsMensagem.ClearAdded();
            return Json.Serialize<Ajax>(this);
        }

        public dynamic GetValueObjJson(string field, object obj)
        {
            if (obj != null)
            {

                var dictionary = (Dictionary<string, object>) obj;
                return dictionary.FirstOrDefault(d => d.Key == field).Value;
            }
            return null;
        }


        public dynamic ToDynamic(object obj)
        {
            if (obj != null)
            {

                dynamic dy = new DynamicAjax(obj);
                return dy;

            }
            return null;
        }      
    }

    public class DynamicAjax : DynamicObject
    {
        Dictionary<string, object> dictionary;
        public DynamicAjax(object obj)
            : base()
        {
            dictionary = (Dictionary<string, object>)obj;

        }

        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        // If you try to get a value of a property
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name;

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dictionary.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
    }
}
