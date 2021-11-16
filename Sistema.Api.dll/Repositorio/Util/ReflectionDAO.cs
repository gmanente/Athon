using Sistema.Api.dll.Repositorio.Util.Attributes;
using Sistema.Api.dll.Repositorio.Util.Helpers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class ReflectionDAO
    {
        public static SqlCommand Select(object obj, SqlCommand cmd, int top = 0,
            string structPaginacao = "", TipoConsulta tipoConsulta = TipoConsulta.Exata, string order = "")
        {
            CustomDictionary<int, Dictionary<TableAttribute, List<FieldAttribute>>> get = ReflectionHandler.GetAttributes(obj);
            StringBuilder sbSelect = new StringBuilder();
            StringBuilder sbJoin = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            sbSelect.Append("SELECT ").AppendLine(top > 0 ? "TOP " + top.ToString() : "");
            sbWhere.AppendLine(" WHERE 1 = 1 ");
            int aux = 0;
            string IdKey = "";
            string tableName = "";
            foreach (KeyValuePair<int, Dictionary<TableAttribute, List<FieldAttribute>>> ent in get)
            {
                foreach (KeyValuePair<TableAttribute, List<FieldAttribute>> entry in ent.Value)
                {
                    foreach (var att in entry.Value)
                    {
                        if (ent.Key == get.Count() - 1)
                        {
                            tableName = entry.Key.TableName;
                            if (att.Key)
                            {
                                IdKey = att.FieldName;
                            }
                        }


                        if (att.JoinType != null)
                        {
                            sbJoin.Append(att.JoinType).Append(" JOIN").Append(att.TableJoin).AppendLine(" ON");
                            sbJoin.Append(entry.Key.TableName + "." + att.FieldName).Append(" = ").AppendLine(att.TableJoin + "." + att.JoinReferenceId);
                        }

                        if (aux == 0)
                        {
                            aux++;
                            sbSelect.AppendLine("              " + entry.Key.TableName + "." + att.FieldName);
                        }
                        else
                        {
                            sbSelect.AppendLine("             ," + entry.Key.TableName + "." + att.FieldName);
                        }
                        if (att.Value != null)
                        {
                            if (tipoConsulta == TipoConsulta.Exata)
                            {
                                sbWhere.Append("      AND ").Append(entry.Key.TableName + "." + att.FieldName + " = ").AppendLine("@" + att.FieldName + "_" + ent.Key);
                            }
                            else if (tipoConsulta == TipoConsulta.PacialInicio)
                            {
                                sbWhere.Append("      AND ").Append(att.FieldName + " LIKE ").AppendLine("@" + att.FieldName + "_" + ent.Key + " + '%'");
                            }
                            else if (tipoConsulta == TipoConsulta.PacialFim)
                            {
                                sbWhere.Append("      AND ").Append(att.FieldName + " LIKE '%'+ ").AppendLine("@" + att.FieldName + "_" + ent.Key);
                            }
                            else if (tipoConsulta == TipoConsulta.Contem)
                            {
                                sbWhere.Append("      AND ").Append(att.FieldName + " LIKE '%'+ ").AppendLine("@" + att.FieldName + "_" + ent.Key + " +'%'");
                            }
                            cmd.Parameters.Add(ent.Key + "_" + att.FieldName, att.FieldType).Value = att.Value;
                        }

                    }

                    if (entry.Key.JoinType != null)
                    {
                        sbJoin.Append(entry.Key.JoinType).Append(" JOIN ").Append(entry.Key.TableJoin).AppendLine(" ON ");
                        sbJoin.Append(entry.Key.TableName + "." + entry.Key.JoinId).Append(" = ").AppendLine(entry.Key.TableJoin + "." + entry.Key.JoinReferenceId);
                    }
                }
            }
            sbSelect.Append("   FROM ").AppendLine(tableName);
            sbSelect.Append(sbJoin.ToString());
            sbSelect.Append(sbWhere.ToString());
            sbSelect.Append(" ORDER BY ").AppendLine(order != "" ? order : IdKey + " ");
            sbSelect.Append(structPaginacao);
            cmd.CommandText = sbSelect.ToString();
            return null;
        }
    }
}