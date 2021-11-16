using Newtonsoft.Json.Linq;
using Sistema.Api.dll.Repositorio.Util;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Sistema.Api.dll.Src
{
    [Serializable]
    public abstract class AbstractVO
    {
        public long Id { get; set; }
        public string ListaId { get; set; }
        public string NotInId { get; set; }
        public string HashCode { get; set; }
        public string Rf { get; set; }

        public static string NomeTabela { get; set; }

        public T Clone<T>() 
        {
            JObject obj = Json.DeSerialize<JObject>(Json.Serialize(this));
            return obj.ToObject<T>();
        }

        protected bool IsInstantiable()
        {
            var st = new StackTrace();
            var methodInfo = (MethodInfo)st.GetFrame(1).GetMethod();
            return (st.GetFrame(2).GetMethod().DeclaringType != null
                && !(methodInfo.ReturnType.FullName == st.GetFrame(2).GetMethod().DeclaringType.FullName)
                && methodInfo.ReturnType != typeof(System.Object));
        }

    }
}