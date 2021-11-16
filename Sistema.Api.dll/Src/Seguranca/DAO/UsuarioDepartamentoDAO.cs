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
    public class UsuarioDepartamentoDAO : AbstractDAO, IDAO<UsuarioDepartamentoVO>
    {
        public UsuarioDepartamentoDAO(SqlCommand sqlConn)
           : base(sqlConn)
        {
        }

        public long Inserir(UsuarioDepartamentoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdUsuarioDepartamento = GetCodigoSequece("DBAthon.dbo.SeqUsuarioDepartamento  ");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.UsuarioDepartamento              ");
                objSbInsert.AppendLine(@"(                                                             ");
                objSbInsert.AppendLine(@"             IdUsuarioDepartamento                            ");
                objSbInsert.AppendLine(@"          ,  IdUsuario                                        ");
                objSbInsert.AppendLine(@"          ,  IdDepartamento                                   ");
                objSbInsert.AppendLine(@"          ,  Ativar                                           ");
                objSbInsert.AppendLine(@")                                                             ");
                objSbInsert.AppendLine(@"VALUES                                                        ");
                objSbInsert.AppendLine(@"(                                                             ");
                objSbInsert.AppendLine(@"             @IdUsuarioDepartamento                           ");
                objSbInsert.AppendLine(@"          ,  @IdUsuario                                       ");
                objSbInsert.AppendLine(@"          ,  @IdDepartamento                                  ");
                objSbInsert.AppendLine(@"          ,  @Ativar                                          ");
                objSbInsert.AppendLine(@")                                                             ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioDepartamento", SqlDbType.Int).Value = IdUsuarioDepartamento;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Departamento.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().ExecuteNonQuery();
                return IdUsuarioDepartamento;

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

        public long Alterar(UsuarioDepartamentoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.UsuarioDepartamento      ");
                objSbUpdate.AppendLine("   SET IdUsuario      = @IdUsuario              ");
                objSbUpdate.AppendLine("     , IdDepartamento = @IdDepartamento         ");
                objSbUpdate.AppendLine("     , Ativar         = @Ativar                 ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdUsuarioDepartamento = @IdUsuarioDepartamento");
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
                    GetSqlCommand().Parameters.Add("IdUsuarioDepartamento", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
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

        public void Deletar(UsuarioDepartamentoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.UsuarioDepartamento", "IdUsuarioDepartamento", objVO.Id);
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.UsuarioDepartamento      ");
                objSbDelete.AppendLine(" WHERE IdUsuarioDepartamento = @IdUsuarioDepartamento");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioDepartamento", SqlDbType.Int).Value = objVO.Id;

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


        public List<UsuarioDepartamentoVO> Selecionar(UsuarioDepartamentoVO usuarioDepartamentoVo = null, int top = 0)
        {
            UsuarioDepartamentoVO usuarioDepartamento = null;
            List<UsuarioDepartamentoVO> lstUsuarioDepartamento = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioDepartamento = new List<UsuarioDepartamentoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"  SELECT                                                                                                  ").Append(varTop);
                objSbSelect.AppendLine(@"          IdUsuarioDepartamento                                                                           ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.IdUsuario                                                              ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Nome AS NomeUsuario                                                    ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Cpf                                                                    ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.IdDepartamento                                                        ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.IdDepartamentoPai AS IdDepartamentoPai                                ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.Nome AS NomeDepartamento                                              ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.UsuarioDepartamento.Ativar                                                     ");
                objSbSelect.AppendLine(@"     FROM  DBAthon.dbo.UsuarioDepartamento                                                            ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Usuario                                                                      ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Usuario.IdUsuario = DBAthon.dbo.UsuarioDepartamento.IdUsuario              ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Departamento                                                                     ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Departamento.IdDepartamento = DBAthon.dbo.UsuarioDepartamento.IdDepartamento   ");
                objSbSelect.AppendLine(@"  WHERE 1 = 1                                                                                             ");

                if (usuarioDepartamentoVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (usuarioDepartamentoVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioDepartamento.IdUsuarioDepartamento= @IdUsuarioDepartamento");
                        GetSqlCommand().Parameters.Add("IdUsuarioDepartamento", SqlDbType.Int).Value = usuarioDepartamentoVo.Id;
                    }
                    if (usuarioDepartamentoVo.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioDepartamentoVo.Usuario.Id;
                    }
                    if (usuarioDepartamentoVo.Departamento.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento = @IdDepartamento");
                        GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = usuarioDepartamentoVo.Departamento.Id;
                    }

                    if (usuarioDepartamentoVo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioDepartamento.Ativar = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = usuarioDepartamentoVo.Ativar;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY  DBAthon.dbo.Departamento.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioDepartamento = new UsuarioDepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioDepartamento"))))
                        usuarioDepartamento.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioDepartamento.Usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeUsuario"))))
                        usuarioDepartamento.Usuario.Nome = Convert.ToString(GetSqlDataReader()["NomeUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        usuarioDepartamento.Usuario.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        usuarioDepartamento.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamentoPai"))))
                        usuarioDepartamento.Departamento.IdDepartamentoPai = Convert.ToInt32(GetSqlDataReader()["IdDepartamentoPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeDepartamento"))))
                        usuarioDepartamento.Departamento.Nome = Convert.ToString(GetSqlDataReader()["NomeDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioDepartamento.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    lstUsuarioDepartamento.Add(usuarioDepartamento);
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

            return lstUsuarioDepartamento;
        }


        // Selecionar com Nivel de Alçada
        public List<UsuarioDepartamentoVO> SelecionarUsuarioDepartamento(UsuarioDepartamentoVO usuarioDepartamentoVo = null, int top = 0)
        {
            UsuarioDepartamentoVO usuarioDepartamento = null;
            List<UsuarioDepartamentoVO> lstUsuarioDepartamento = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioDepartamento = new List<UsuarioDepartamentoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"      SELECT                                                                                                             ").Append(varTop);
                objSbSelect.AppendLine(@"             UsuarioDepartamento.IdUsuarioDepartamento                                                                   ");
                objSbSelect.AppendLine(@"           , Usuario.IdUsuario                                                                                           ");
                objSbSelect.AppendLine(@"           , Usuario.Nome                         NomeUsuario                                                            ");
                objSbSelect.AppendLine(@"           , Usuario.Cpf                                                                                                 ");
                objSbSelect.AppendLine(@"           , Departamento.IdDepartamento                                                                                 ");
                objSbSelect.AppendLine(@"           , Departamento.IdDepartamentoPai       IdDepartamentoPai                                                      ");
                objSbSelect.AppendLine(@"           , Departamento.Nome                    NomeDepartamento                                                       ");
                objSbSelect.AppendLine(@"           , UsuarioDepartamento.Ativar                                                                                  ");
                objSbSelect.AppendLine(@"           , NivelAlcada.IdNivelAlcada                                                                                   ");
                objSbSelect.AppendLine(@"           , NivelAlcada.Descricao                NivelAlcadaDescricao                                                   ");
                objSbSelect.AppendLine(@"        FROM DBAthon.dbo.UsuarioDepartamento                                                                         ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Usuario  ON DBAthon.dbo.Usuario.IdUsuario = UsuarioDepartamento.IdUsuario               ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Departamento ON DBAthon.dbo.Departamento.IdDepartamento = UsuarioDepartamento.IdDepartamento    ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.NivelAlcada ON NivelAlcada.IdNivelAlcada = Departamento.IdNivelAlcada                          ");
                objSbSelect.AppendLine(@"       WHERE 1 = 1                                                                                                       ");

                if (usuarioDepartamentoVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (usuarioDepartamentoVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioDepartamento.IdUsuarioDepartamento= @IdUsuarioDepartamento");
                        GetSqlCommand().Parameters.Add("IdUsuarioDepartamento", SqlDbType.Int).Value = usuarioDepartamentoVo.Id;
                    }
                    if (usuarioDepartamentoVo.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioDepartamentoVo.Usuario.Id;
                    }
                    if (usuarioDepartamentoVo.Departamento.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Departamento.IdDepartamento = @IdDepartamento");
                        GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = usuarioDepartamentoVo.Departamento.Id;
                    }

                    if (usuarioDepartamentoVo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioDepartamento.Ativar = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = usuarioDepartamentoVo.Ativar;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY  DBAthon.dbo.Departamento.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioDepartamento = new UsuarioDepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioDepartamento"))))
                        usuarioDepartamento.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioDepartamento.Usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeUsuario"))))
                        usuarioDepartamento.Usuario.Nome = Convert.ToString(GetSqlDataReader()["NomeUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        usuarioDepartamento.Usuario.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        usuarioDepartamento.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamentoPai"))))
                        usuarioDepartamento.Departamento.IdDepartamentoPai = Convert.ToInt32(GetSqlDataReader()["IdDepartamentoPai"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeDepartamento"))))
                        usuarioDepartamento.Departamento.Nome = Convert.ToString(GetSqlDataReader()["NomeDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioDepartamento.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdNivelAlcada"))))
                        usuarioDepartamento.Departamento.IdNivelAlcada = Convert.ToInt64(GetSqlDataReader()["IdNivelAlcada"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NivelAlcadaDescricao"))))
                        usuarioDepartamento.Departamento.NivelAlcadaDescricao = Convert.ToString(GetSqlDataReader()["NivelAlcadaDescricao"]);

                    lstUsuarioDepartamento.Add(usuarioDepartamento);
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

            return lstUsuarioDepartamento;
        }

        public UsuarioDepartamentoVO Consultar(UsuarioDepartamentoVO usuarioDepartamentoVo)
        {
            try
            {
                List<UsuarioDepartamentoVO> lstUsuarioDepartamento = Selecionar(usuarioDepartamentoVo);

                return lstUsuarioDepartamento.Count() > 0 ? (UsuarioDepartamentoVO)lstUsuarioDepartamento.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioDepartamentoVO> Listar(UsuarioDepartamentoVO usuarioDepartamentoVo)
        {
            try
            {
                List<UsuarioDepartamentoVO> lstUsuarioDepartamento = Selecionar(usuarioDepartamentoVo);

                return lstUsuarioDepartamento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<UsuarioDepartamentoVO>> Paginar(UsuarioDepartamentoVO objVO)
        {
            Dictionary<int, List<UsuarioDepartamentoVO>> dictionary = null;
            try
            {
                List<UsuarioDepartamentoVO> lstUsuarioDepartamentoVO;
                dictionary = new Dictionary<int, List<UsuarioDepartamentoVO>>();
                var sbPaginar = new StringBuilder();
                lstUsuarioDepartamentoVO = Selecionar(objVO);
                dictionary.Add(lstUsuarioDepartamentoVO.Count, lstUsuarioDepartamentoVO);
                return dictionary;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}