using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class PerfilDepartamentoDAO : AbstractDAO, IDAO<PerfilDepartamentoVO>
    {
        public PerfilDepartamentoDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(PerfilDepartamentoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdPerfilDepartamento = GetCodigoSequece("DBAthon.dbo.SeqPerfilDepartamento   ");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.PerfilDepartamento              ");
                objSbInsert.AppendLine(@"(                                                             ");
                objSbInsert.AppendLine(@"             IdPerfilDepartamento                             ");
                objSbInsert.AppendLine(@"          ,  IdPerfil                                         ");
                objSbInsert.AppendLine(@"          ,  IdDepartamento                                   ");
                objSbInsert.AppendLine(@"          ,  Ativar                                           ");
                objSbInsert.AppendLine(@")                                                             ");
                objSbInsert.AppendLine(@"VALUES                                                        ");
                objSbInsert.AppendLine(@"(                                                             ");
                objSbInsert.AppendLine(@"             @IdPerfilDepartamento                            ");
                objSbInsert.AppendLine(@"          ,  @IdPerfil                                        ");
                objSbInsert.AppendLine(@"          ,  @IdDepartamento                                  ");
                objSbInsert.AppendLine(@"          ,  @Ativar                                          ");
                objSbInsert.AppendLine(@")                                                             ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilDepartamento", SqlDbType.Int).Value = IdPerfilDepartamento;
                GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Departamento.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().ExecuteNonQuery();
                return IdPerfilDepartamento;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }

        public long Alterar(PerfilDepartamentoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilDepartamento       ");
                objSbUpdate.AppendLine("   SET IdPerfil       = @IdPerfil               ");
                objSbUpdate.AppendLine("     , IdDepartamento = @IdDepartamento         ");
                objSbUpdate.AppendLine("     , Ativar         = @Ativar                 ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdPerfilDepartamento = @IdPerfilDepartamento");
                }
                else
                {
                    objSbUpdate.AppendLine(where);
                }

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPerfilDepartamento", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                    GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Departamento.Id;
                    GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                }
                GetSqlCommand().ExecuteNonQuery();
                return objVO.Id;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }

        public void Deletar(PerfilDepartamentoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.PerfilDepartamento      ");
                objSbDelete.AppendLine(" WHERE IdPerfilDepartamento = @IdPerfilDepartamento");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilDepartamento", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                {
                    objSbDelete = null;
                }
            }
        }

        public List<PerfilDepartamentoVO> Selecionar(PerfilDepartamentoVO perfilDepartamentoVo = null, int top = 0)
        {
            PerfilDepartamentoVO perfilDepartamento = null;
            List<PerfilDepartamentoVO> lstPerfilDepartamento = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstPerfilDepartamento = new List<PerfilDepartamentoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"  SELECT                                                                                                  ").Append(varTop);
                objSbSelect.AppendLine(@"          IdPerfilDepartamento                                                                            ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Perfil.IdPerfil                                                                ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Perfil.Descricao                                                               ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.IdDepartamento                                                        ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.IdDepartamentoPai AS IdDepartamentoPai                                ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.Nome AS NomeDepartamento                                              ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.PerfilDepartamento.Ativar                                                      ");
                objSbSelect.AppendLine(@"     FROM  DBAthon.dbo.PerfilDepartamento                                                             ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Perfil                                                                       ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Perfil.IdPerfil = DBAthon.dbo.PerfilDepartamento.IdPerfil                  ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Departamento                                                                     ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Departamento.IdDepartamento = DBAthon.dbo.PerfilDepartamento.IdDepartamento    ");
                objSbSelect.AppendLine(@"  WHERE 1 = 1                                                                                             ");

                if (perfilDepartamentoVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (perfilDepartamentoVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilDepartamento.IdPerfilDepartamento= @IdPerfilDepartamento");
                        GetSqlCommand().Parameters.Add("IdPerfilDepartamento", SqlDbType.Int).Value = perfilDepartamentoVo.Id;
                    }
                    if (perfilDepartamentoVo.Perfil.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Perfil.IdPerfil = @IdPerfil");
                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = perfilDepartamentoVo.Perfil.Id;
                    }
                    if (perfilDepartamentoVo.Departamento.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento = @IdDepartamento");
                        GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = perfilDepartamentoVo.Departamento.Id;
                    }

                    if (perfilDepartamentoVo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilDepartamento.Ativar = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = perfilDepartamentoVo.Ativar;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY  DBAthon.dbo.Departamento.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    perfilDepartamento = new PerfilDepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfilDepartamento"))))
                        perfilDepartamento.Id = Convert.ToInt32(GetSqlDataReader()["IdPerfilDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfil"))))
                        perfilDepartamento.Perfil.Id = Convert.ToInt32(GetSqlDataReader()["IdPerfil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        perfilDepartamento.Perfil.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        perfilDepartamento.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamentoPai"))))
                        perfilDepartamento.Departamento.IdDepartamentoPai = Convert.ToInt32(GetSqlDataReader()["IdDepartamentoPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeDepartamento"))))
                        perfilDepartamento.Departamento.Nome = Convert.ToString(GetSqlDataReader()["NomeDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        perfilDepartamento.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    lstPerfilDepartamento.Add(perfilDepartamento);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }

            return lstPerfilDepartamento;
        }

        public PerfilDepartamentoVO Consultar(PerfilDepartamentoVO perfilDepartamentoVo)
        {
            try
            {
                List<PerfilDepartamentoVO> lstPerfilDepartamento = Selecionar(perfilDepartamentoVo);

                return lstPerfilDepartamento.Count() > 0 ? (PerfilDepartamentoVO)lstPerfilDepartamento.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilDepartamentoVO> Listar(PerfilDepartamentoVO perfilDepartamentoVo)
        {
            try
            {
                List<PerfilDepartamentoVO> lstPerfilDepartamento = Selecionar(perfilDepartamentoVo);

                return lstPerfilDepartamento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<PerfilDepartamentoVO>> Paginar(PerfilDepartamentoVO objVO)
        {
            Dictionary<int, List<PerfilDepartamentoVO>> dictionary = null;
            try
            {
                List<PerfilDepartamentoVO> lstPerfilDepartamentoVO;
                dictionary = new Dictionary<int, List<PerfilDepartamentoVO>>();
                var sbPaginar = new StringBuilder();
                lstPerfilDepartamentoVO = Selecionar(objVO);
                dictionary.Add(lstPerfilDepartamentoVO.Count, lstPerfilDepartamentoVO);
                return dictionary;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}