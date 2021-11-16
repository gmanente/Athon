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
    public class UsuarioSenhaDAO : AbstractDAO, IDAO<UsuarioSenhaVO>
    {
        public UsuarioSenhaDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        //Alterar
        public long Alterar(UsuarioSenhaVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.UsuarioSenha            ");
                objSbUpdate.AppendLine(@"  SET                                          ");
                objSbUpdate.AppendLine(@"    Senha        = @Senha                      ");
                objSbUpdate.AppendLine(@"  , DataTermino  = @DataTermino                ");
                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdUsuarioSenha = @IdUsuarioSenha     ");
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
                    GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = objVO.Senha;
                    GetSqlCommand().Parameters.Add("DataTermino", SqlDbType.DateTime).Value = objVO.DataTermino;

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioSenha", SqlDbType.Int).Value = objVO.Id;
                    }

                    GetSqlCommand().ExecuteNonQuery();
                }
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

        public void Deletar(UsuarioSenhaVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM                   ");
                objSbDelete.AppendLine(@"  DBAthon.dbo.UsuarioSenha ");
                objSbDelete.AppendLine(@" WHERE IdUsuario =  @IdUsuario ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
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

        //Inserir
        public long Inserir(UsuarioSenhaVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idUsuarioSenha = GetCodigoSequece("DBAthon.dbo.SeqUsuarioSenha  ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.UsuarioSenha   ");
                objSbInsert.AppendLine(@"(                                          ");
                objSbInsert.AppendLine(@"   IdUsuarioSenha                          ");
                objSbInsert.AppendLine(@"  ,IdUsuario                               ");
                objSbInsert.AppendLine(@"  ,Senha                                   ");
                objSbInsert.AppendLine(@"  ,DataCadastro                            ");
                objSbInsert.AppendLine(@"  ,DataTermino                             ");
                objSbInsert.AppendLine(@")                                          ");
                objSbInsert.AppendLine(@"VALUES                                     ");
                objSbInsert.AppendLine(@"(                                          ");
                objSbInsert.AppendLine(@"      @IdUsuarioSenha                      ");
                objSbInsert.AppendLine(@"     ,@IdUsuario                           ");
                objSbInsert.AppendLine(@"     ,@Senha                               ");
                objSbInsert.AppendLine(@"     ,@DataCadastro                        ");
                objSbInsert.AppendLine(@"     ,@DataTermino                         ");
                objSbInsert.AppendLine(@")                                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioSenha", SqlDbType.Int).Value = idUsuarioSenha;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.IdUsuario;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = objVO.Senha;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
                GetSqlCommand().Parameters.Add("DataTermino", SqlDbType.DateTime).Value = objVO.DataTermino;

                GetSqlCommand().ExecuteNonQuery();

                return idUsuarioSenha;
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



        public List<UsuarioSenhaVO> Selecionar(UsuarioSenhaVO usuarioSenhaVo = null, int top = 0)
        {
            UsuarioSenhaVO usuarioSenha = null;
            List<UsuarioSenhaVO> lstUsuarioSenha = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioSenha = new List<UsuarioSenhaVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                              ").Append(varTop);
                objSbSelect.AppendLine(@"        IdUsuarioSenha              ");
                objSbSelect.AppendLine(@"      , IdUsuario                   ");
                objSbSelect.AppendLine(@"      , Senha                       ");
                objSbSelect.AppendLine(@"      , DataCadastro                ");
                objSbSelect.AppendLine(@"      , DataTermino                 ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.UsuarioSenha     ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                         ");

                if (usuarioSenhaVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (usuarioSenhaVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdUsuarioSenha = @IdUsuarioSenha");
                        GetSqlCommand().Parameters.Add("IdUsuarioSenha", SqlDbType.Int).Value = usuarioSenhaVo.Id;
                    }
                    if (usuarioSenhaVo.IdUsuario > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioSenhaVo.IdUsuario;
                    }
                    if (!string.IsNullOrEmpty(usuarioSenhaVo.Senha))
                    {
                        objSbSelect.AppendLine(@" AND Senha = @Senha");
                        GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = usuarioSenhaVo.Senha;
                    }
                    if (usuarioSenhaVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = usuarioSenhaVo.DataCadastro;
                    }
                    if (usuarioSenhaVo.DataTermino != null)
                    {
                        objSbSelect.AppendLine(@" AND DataTermino = @DataTermino");
                        GetSqlCommand().Parameters.Add("DataTermino", SqlDbType.DateTime).Value = usuarioSenhaVo.DataTermino;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY IdUsuarioSenha DESC ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioSenha = new UsuarioSenhaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSenha"))))
                        usuarioSenha.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSenha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioSenha.IdUsuario = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuarioSenha.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataTermino"))))
                        usuarioSenha.DataTermino = Convert.ToDateTime(GetSqlDataReader()["DataTermino"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        usuarioSenha.Senha = Convert.ToString(GetSqlDataReader()["Senha"]);

                    lstUsuarioSenha.Add(usuarioSenha);
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

            return lstUsuarioSenha;
        }

        public List<UsuarioSenhaVO> VerificarSenha(UsuarioSenhaVO usuarioSenhaVo,  string top = " ")
        {
            UsuarioSenhaVO usuarioSenha = null;
            List<UsuarioSenhaVO> lstUsuarioSenha = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioSenha = new List<UsuarioSenhaVO>();


                    objSbSelect.Append(@"SELECT ").Append(top).AppendLine("IdUsuarioSenha");
                objSbSelect.AppendLine(@"      , IdUsuario                                                               ");
                objSbSelect.AppendLine(@"      , Senha                                                                   ");
                objSbSelect.AppendLine(@"      , DataCadastro                                                            ");
                objSbSelect.AppendLine(@"      , DataTermino                                                             ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.UsuarioSenha                                                 ");
                objSbSelect.AppendLine(@"WHERE UsuarioSenha.IdUsuarioSenha IN (SELECT TOP 20 US.IdUsuarioSenha           ");
                objSbSelect.AppendLine(@"									    FROM DBAthon.dbo.UsuarioSenha US         ");
                objSbSelect.AppendLine(@"									    WHERE US.IdUsuario = @IdUsuario          ");
                objSbSelect.AppendLine(@"									    ORDER BY US.IdUsuarioSenha DESC)         ");

                GetSqlCommand().Parameters.Clear();
                if (usuarioSenhaVo.Senha != null)
                {
                    objSbSelect.AppendLine(@" AND UsuarioSenha.Senha =  @Senha                                               ");
                    GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = usuarioSenhaVo.Senha;
                }
               
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.VarChar).Value = usuarioSenhaVo.IdUsuario;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioSenha = new UsuarioSenhaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSenha"))))
                        usuarioSenha.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSenha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSenha"))))
                        usuarioSenha.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSenha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuarioSenha.IdUsuario = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuarioSenha.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataTermino"))))
                        usuarioSenha.DataTermino = Convert.ToDateTime(GetSqlDataReader()["DataTermino"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        usuarioSenha.Senha = Convert.ToString(GetSqlDataReader()["Senha"]);

                    lstUsuarioSenha.Add(usuarioSenha);
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

            return lstUsuarioSenha;
        }

        public UsuarioSenhaVO Consultar(UsuarioSenhaVO usuarioSenhaVo)
        {
            try
            {
                List<UsuarioSenhaVO> lstUsuarioSenha = Selecionar(usuarioSenhaVo);
                return lstUsuarioSenha.Count() > 0 ? (UsuarioSenhaVO)lstUsuarioSenha.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioSenhaVO> Listar(UsuarioSenhaVO usuarioSenhaVo)
        {
            try
            {
                List<UsuarioSenhaVO> lstUsuarioSenha = Selecionar(usuarioSenhaVo);
                return lstUsuarioSenha;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<UsuarioSenhaVO> Paginar(UsuarioSenhaVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}
