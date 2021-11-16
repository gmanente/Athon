using Sistema.Api.dll.Repositorio.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class ReflectionHandler
    {
        public static CustomDictionary<int, Dictionary<TableAttribute, List<FieldAttribute>>> GetAttributes(object obj, bool join = false)
        {
            Dictionary<TableAttribute, List<FieldAttribute>> Dcionry =
                new Dictionary<TableAttribute, List<FieldAttribute>>();
            CustomDictionary<int, Dictionary<TableAttribute, List<FieldAttribute>>> resultado =
                new CustomDictionary<int, Dictionary<TableAttribute, List<FieldAttribute>>>();
            List<FieldAttribute> ListFields = new List<FieldAttribute>();

            TableAttribute tblAtt = null;
            Type type = obj.GetType();
            tblAtt = type.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault() as TableAttribute;

            try
            {
                ListFields.Clear();
                foreach (var propertyInfo in type.GetProperties())
                {
                    foreach (FieldAttribute customAttribute in propertyInfo.GetCustomAttributes(typeof(FieldAttribute), false))
                    {
                        if (IsPrimitive(propertyInfo.PropertyType) || IsNullable(propertyInfo.PropertyType))
                        {
                            if (join == false || customAttribute.Key)
                            {
                                customAttribute.Value = propertyInfo.GetValue(obj);
                            }
                            ListFields.Add(customAttribute);
                        }
                        else
                        {
                            object o = propertyInfo.GetValue(obj, null);
                            if (o != null)
                            {
                                resultado.AddRange(GetAttributes(o, true));
                            }
                            else
                            {
                                ListFields.Add(customAttribute);
                            }
                        }
                    }
                }

                Dcionry.Add(tblAtt, ListFields);
                resultado.Add(resultado.Count(), Dcionry);
            }
            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        public static bool IsPrimitive(Type type)
        {
            TypeCode tipo = Type.GetTypeCode(type);
            switch (tipo)
            {
                case TypeCode.String:   return true;
                case TypeCode.Int32:    return true;
                case TypeCode.Int64:    return true;
                case TypeCode.Double:   return true;
                case TypeCode.DateTime: return true;
                case TypeCode.Boolean:  return true;
                case TypeCode.DBNull:   return true;
                case TypeCode.Decimal:  return true;
                default: return false;
            }
        }

        public static bool IsNullable(Type type)
        {
            return (Nullable.GetUnderlyingType(type) != null) ? true : false;
        }

    }
}