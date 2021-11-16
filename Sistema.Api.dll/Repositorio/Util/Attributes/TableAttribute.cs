using System;

namespace Sistema.Api.dll.Repositorio.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        public string TableName { get; set; }
        public string JoinType { get; set; }
        public string TableJoin { get; set; }
        public string JoinReferenceId { get; set; }
        public string JoinId { get; set; }

    }
}