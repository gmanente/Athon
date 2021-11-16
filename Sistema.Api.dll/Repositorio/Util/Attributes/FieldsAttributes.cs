using System;

namespace Sistema.Api.dll.Repositorio.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FieldAttribute : Attribute
    {
        public string FieldName { get; set; }
        public bool Key { get; set; }
        public System.Data.SqlDbType FieldType { get; set; }
        public int Length { get; set; }
        public object Value { get; set; }
        public string JoinType { get; set; }
        public string TableJoin { get; set; }
        public string JoinReferenceId { get; set; }
        
    }
}