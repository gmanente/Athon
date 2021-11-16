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
    public class UsuarioDAO : AbstractDAO, IDAO<UsuarioVO>
    {
        public UsuarioDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {

        }

        public long Inserir(UsuarioVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long idUsuario = GetCodigoSequece("DBAthon.dbo.SeqUsuario");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Usuario");
                objSbInsert.AppendLine(@"(                          ");
                objSbInsert.AppendLine(@"   idUsuario               ");
                objSbInsert.AppendLine(@"  ,DataCadastro            ");
                objSbInsert.AppendLine(@"  ,NomeLogin               ");
                objSbInsert.AppendLine(@"  ,Email                   ");
                objSbInsert.AppendLine(@"  ,Nome                    ");
                objSbInsert.AppendLine(@"  ,Cpf                     ");
                objSbInsert.AppendLine(@"  ,DataNascimento          ");
                objSbInsert.AppendLine(@"  ,Telefone                ");
                objSbInsert.AppendLine(@"  ,Celular                 ");
                objSbInsert.AppendLine(@"  ,Ativo                   ");
                objSbInsert.AppendLine(@")                          ");
                objSbInsert.AppendLine(@"VALUES                     ");
                objSbInsert.AppendLine(@"(     @idUsuario           ");
                objSbInsert.AppendLine(@"     ,GETDATE()            ");
                objSbInsert.AppendLine(@"     ,@NomeLogin           ");
                objSbInsert.AppendLine(@"     ,@Email               ");
                objSbInsert.AppendLine(@"     ,@Nome                ");
                objSbInsert.AppendLine(@"     ,@Cpf                 ");
                objSbInsert.AppendLine(@"     ,@DataNascimento      ");
                objSbInsert.AppendLine(@"     ,@Telefone            ");
                objSbInsert.AppendLine(@"     ,@Celular             ");
                objSbInsert.AppendLine(@"     ,@Ativo               ");
                objSbInsert.AppendLine(@")                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("NomeLogin", SqlDbType.VarChar).Value = objVO.NomeLogin;
                GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                GetSqlCommand().Parameters.Add("DataNascimento", SqlDbType.DateTime).Value = objVO.DataNascimento;
                GetSqlCommand().Parameters.Add("Telefone", SqlDbType.VarChar).Value = objVO.Telefone;
                GetSqlCommand().Parameters.Add("Celular", SqlDbType.VarChar).Value = objVO.Celular;
                GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;

                GetSqlCommand().ExecuteNonQuery();

                return idUsuario;
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


        public long Alterar(UsuarioVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Usuario                ");
                objSbUpdate.AppendLine(@"  SET                                         ");
                objSbUpdate.AppendLine(@"       NomeLogin         = @NomeLogin         ");
                if (string.IsNullOrEmpty(objVO.Email))
                    objSbUpdate.AppendLine(@"     , Email            = @Email              ");
                objSbUpdate.AppendLine(@"     , Nome             = @Nome               ");
                objSbUpdate.AppendLine(@"     , Cpf              = @Cpf                ");
                objSbUpdate.AppendLine(@"     , DataNascimento   = @DataNascimento     ");
                objSbUpdate.AppendLine(@"     , Telefone         = @Telefone           ");
                objSbUpdate.AppendLine(@"     , Celular          = @Celular            ");
                objSbUpdate.AppendLine(@"     , Ativo            = @Ativo              ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@" WHERE IdUsuario = @IdUsuario             ");
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
                    GetSqlCommand().Parameters.Add("NomeLogin", SqlDbType.VarChar).Value = objVO.NomeLogin;
                    if (string.IsNullOrEmpty(objVO.Email))
                        GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                    GetSqlCommand().Parameters.Add("DataNascimento", SqlDbType.DateTime).Value = objVO.DataNascimento;
                    GetSqlCommand().Parameters.Add("Telefone", SqlDbType.VarChar).Value = objVO.Telefone;
                    GetSqlCommand().Parameters.Add("Celular", SqlDbType.VarChar).Value = objVO.Celular;
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;
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


        public void Deletar(UsuarioVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM                   ");
                objSbDelete.AppendLine(@"  DBAthon.dbo.Usuario      ");
                objSbDelete.AppendLine(@" WHERE IdUsuario =  @IdUsuario ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;

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


        // Método AlterarEmail
        public long AlterarEmail(UsuarioVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Usuario         ");
                objSbUpdate.AppendLine(@"  SET                                  ");
                objSbUpdate.AppendLine(@"    Email     = @Email                 ");
                objSbUpdate.AppendLine(@"    WHERE IdUsuario = @IdUsuario       ");

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                    GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;

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
                    objSbUpdate = null;
            }
        }


        // Método AlterarDadosBasicos
        public long AlterarDadosBasicos(UsuarioVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE  DBAthon.dbo.Usuario                ");
                objSbUpdate.AppendLine(@"  SET                                          ");
                if (objVO.DataNascimento != null)
                    objSbUpdate.AppendLine(@"        DataNascimento   = @DataNascimento     ");
                else
                    objSbUpdate.AppendLine(@"        DataNascimento = NULL              ");

                if (objVO.Celular != null)
                    objSbUpdate.AppendLine(@"      , Telefone         = @Telefone           ");
                else
                    objSbUpdate.AppendLine(@"      , Telefone         = NULL            ");

                if (objVO.Celular != null)
                    objSbUpdate.AppendLine(@"      , Celular          = @Celular            ");
                else
                    objSbUpdate.AppendLine(@"      , Celular          = NULL            ");

                if (objVO.Email != null)
                    objSbUpdate.AppendLine(@"      , Email          = @Email            ");
                else
                    objSbUpdate.AppendLine(@"      , Email          = NULL            ");

                if (where == null)
                    objSbUpdate.AppendLine(@" WHERE IdUsuario = @IdUsuario              ");
                else
                    objSbUpdate.AppendLine(where);

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();

                    if (objVO.DataNascimento != null)
                        GetSqlCommand().Parameters.Add("DataNascimento", SqlDbType.DateTime).Value = objVO.DataNascimento;
                    if (objVO.Telefone != null)
                        GetSqlCommand().Parameters.Add("Telefone", SqlDbType.VarChar).Value = objVO.Telefone;
                    if (objVO.Celular != null)
                        GetSqlCommand().Parameters.Add("Celular", SqlDbType.VarChar).Value = objVO.Celular;
                    if (objVO.Email != null)
                        GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                    if (where == null)
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;

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
                    objSbUpdate = null;
            }
        }


        // Método Alterar
        public long AlterarUsuarioProfessor(UsuarioVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Usuario                ");
                objSbUpdate.AppendLine(@"  SET                                         ");
                objSbUpdate.AppendLine(@"       Nome             = @Nome               ");
                //objSbUpdate.AppendLine(@"     , Email            = @Email              ");
                //objSbUpdate.AppendLine(@"     , Cpf              = @Cpf                ");
                objSbUpdate.AppendLine(@"     , DataNascimento   = @DataNascimento     ");
                objSbUpdate.AppendLine(@"     , Telefone         = @Telefone           ");
                objSbUpdate.AppendLine(@"     , Celular          = @Celular            ");
                objSbUpdate.AppendLine(@"     , Ativo            = @Ativo              ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@" WHERE IdUsuario = @IdUsuario             ");
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
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    //GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                    //GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                    GetSqlCommand().Parameters.Add("DataNascimento", SqlDbType.DateTime).Value = objVO.DataNascimento;
                    GetSqlCommand().Parameters.Add("Telefone", SqlDbType.VarChar).Value = objVO.Telefone;
                    GetSqlCommand().Parameters.Add("Celular", SqlDbType.VarChar).Value = objVO.Celular;
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = objVO.Ativo;

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;
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


        // Método AutenticarUsuario
        public UsuarioVO AutenticarUsuario(string nomeLogin)
        {
            UsuarioVO usuario = null;

            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT TOP 1                                                                                        ");
                objSbSelect.AppendLine(@"       Usuario.DataCadastro                                                                         ");
                objSbSelect.AppendLine(@"     , NomeLogin                                                                                    ");
                objSbSelect.AppendLine(@"     , Email                                                                                        ");
                objSbSelect.AppendLine(@"     , Nome                                                                                         ");
                objSbSelect.AppendLine(@"     , UsuarioSenha.IdUsuarioSenha                                                                  ");
                objSbSelect.AppendLine(@"     , UsuarioSenha.DataTermino                                                                     ");
                objSbSelect.AppendLine(@"     , UsuarioSenha.IdUsuarioSenha                                                                  ");
                objSbSelect.AppendLine(@"     , UsuarioSenha.Senha                                                                           ");
                objSbSelect.AppendLine(@"     , Usuario.IdUsuario                                                                            ");
                objSbSelect.AppendLine(@"     , Usuario.Cpf                                                                                  ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Usuario                                                                      ");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.UsuarioSenha                                                           ");
                objSbSelect.AppendLine(@"         ON UsuarioSenha.IdUsuario = DBAthon.dbo.Usuario.IdUsuario                              ");
                objSbSelect.AppendLine(@"        AND UsuarioSenha.IdUsuarioSenha = (SELECT TOP 1 US.IdUsuarioSenha                           ");
                objSbSelect.AppendLine(@"                                             FROM DBAthon.dbo.UsuarioSenha US                   ");
                objSbSelect.AppendLine(@"                                            WHERE US.IdUsuario = DBAthon.dbo.Usuario.IdUsuario  ");
                objSbSelect.AppendLine(@"                                            ORDER BY US.IdUsuarioSenha DESC)                        ");
                objSbSelect.AppendLine(@"WHERE 1 = 1 ");




                GetSqlCommand().Parameters.Clear();
                objSbSelect.AppendLine(@"AND NomeLogin = @NomeLogin");
                GetSqlCommand().Parameters.Add("NomeLogin", SqlDbType.VarChar).Value = nomeLogin;
                objSbSelect.AppendLine(@"ORDER BY UsuarioSenha.IdUsuarioSenha DESC                                                                   ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuario = new UsuarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        usuario.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuario.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        usuario.NomeLogin = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        usuario.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuario.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSenha"))))
                        usuario.UsuarioSenha.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSenha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuario.UsuarioSenha.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataTermino"))))
                        usuario.UsuarioSenha.DataTermino = Convert.ToDateTime(GetSqlDataReader()["DataTermino"]);

                    //if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        usuario.UsuarioSenha.Senha = Convert.ToString(GetSqlDataReader()["Senha"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }

            return usuario;
        }


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<UsuarioVO> Selecionar(UsuarioVO usuarioVo = null, int top = 0)
        {
            List<UsuarioVO> lstUsuario = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstUsuario = new List<UsuarioVO>();

                string varTop = "";

                if (top > 0)
                {
                    varTop = " TOP " + top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                             ").Append(varTop);
                objSbSelect.AppendLine(@"        IdUsuario                  ");
                objSbSelect.AppendLine(@"      , DataCadastro               ");
                objSbSelect.AppendLine(@"      , NomeLogin                  ");
                objSbSelect.AppendLine(@"      , Email                      ");
                objSbSelect.AppendLine(@"      , Nome                       ");
                objSbSelect.AppendLine(@"      , Cpf                        ");
                objSbSelect.AppendLine(@"      , DataNascimento             ");
                objSbSelect.AppendLine(@"      , Telefone                   ");
                objSbSelect.AppendLine(@"      , Celular                    ");
                objSbSelect.AppendLine(@"      , Ativo                      ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Usuario     ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                       ");

                if (usuarioVo != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (usuarioVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioVo.Id;
                    }

                    if (usuarioVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = usuarioVo.DataCadastro;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.NomeLogin))
                    {
                        objSbSelect.AppendLine(@" AND NomeLogin = @NomeLogin");
                        GetSqlCommand().Parameters.Add("NomeLogin", SqlDbType.VarChar).Value = usuarioVo.NomeLogin;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Email))
                    {
                        objSbSelect.AppendLine(@" AND Email = @Email");
                        GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = usuarioVo.Email;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = usuarioVo.Nome;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Cpf))
                    {
                        objSbSelect.AppendLine(@" AND Cpf = @Cpf");
                        GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = usuarioVo.Cpf;
                    }

                    if (usuarioVo.Ativo != null)
                    {
                        objSbSelect.AppendLine(@" AND Ativo = @Ativo");
                        GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Int).Value = usuarioVo.Ativo;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.StrConsulta))
                    {
                        objSbSelect.AppendLine(@" AND (Usuario.Nome LIKE '%" + usuarioVo.StrConsulta + "%' OR Usuario.Cpf LIKE '%" + usuarioVo.StrConsulta + "%') ");
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    UsuarioVO usuario = new UsuarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuario.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        usuario.NomeLogin = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        usuario.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuario.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        usuario.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataNascimento"))))
                        usuario.DataNascimento = Convert.ToDateTime(GetSqlDataReader()["DataNascimento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Telefone"))))
                        usuario.Telefone = Convert.ToString(GetSqlDataReader()["Telefone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Celular"))))
                        usuario.Celular = Convert.ToString(GetSqlDataReader()["Celular"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        usuario.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    lstUsuario.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }

            return lstUsuario;
        }


        public List<UsuarioVO> SelecionarPorPerfil(UsuarioVO usuarioVo = null, int top = 0)
        {
            UsuarioVO usuario = null;
            List<UsuarioVO> lstUsuario = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstUsuario = new List<UsuarioVO>();

                objSbSelect.AppendLine(@" SELECT
                                                        DBAthon.dbo.Usuario.IdUsuario
                                                      , DataCadastro
                                                      , NomeLogin
                                                      , Email
                                                      , Nome
                                                      , Cpf
                                                      , DataNascimento
                                                      , Telefone
                                                      , Celular
                                                      --, Ativo
                                                  FROM DBAthon.dbo.Usuario
                                                  INNER JOIN DBAthon.dbo.UsuarioCampus
                                                          ON DBAthon.dbo.UsuarioCampus.IdUsuario = DBAthon.dbo.Usuario.IdUsuario
                                                  INNER JOIN DBAthon.dbo.UsuarioPerfil
                                                          ON DBAthon.dbo.UsuarioPerfil.IdUsuarioCampus = DBAthon.dbo.UsuarioCampus.IdUsuarioCampus
                                                 WHERE 1 = 1

                                        ");


                if (usuarioVo != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (usuarioVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioVo.Id;
                    }

                    if (usuarioVo.UsuarioCampus.UsuarioPerfil.Perfil.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioPerfil.IdPerfil = @IdPerfil");
                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = usuarioVo.UsuarioCampus.UsuarioPerfil.Perfil.Id;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.UsuarioCampus.UsuarioPerfil.Perfil.ListaId))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioPerfil.IdPerfil IN ("+ usuarioVo.UsuarioCampus.UsuarioPerfil.Perfil.ListaId + ")");
                    }

                    if (usuarioVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = usuarioVo.DataCadastro;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.NomeLogin))
                    {
                        objSbSelect.AppendLine(@" AND NomeLogin = @NomeLogin");
                        GetSqlCommand().Parameters.Add("NomeLogin", SqlDbType.VarChar).Value = usuarioVo.NomeLogin;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Email))
                    {
                        objSbSelect.AppendLine(@" AND Email = @Email");
                        GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = usuarioVo.Email;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = usuarioVo.Nome;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Cpf))
                    {
                        objSbSelect.AppendLine(@" AND Cpf = @Cpf");
                        GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = usuarioVo.Cpf;
                    }

                    if (usuarioVo.Ativo != null)
                    {
                        objSbSelect.AppendLine(@" AND Usuario.Ativo = Ativo");
                        GetSqlCommand().Parameters.Add("Ativo", SqlDbType.Bit).Value = usuarioVo.Ativo;
                    }

                    if (usuarioVo.UsuarioPerfil.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND UsuarioPerfil.Ativar = Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = usuarioVo.UsuarioPerfil.Ativar;
                    }
                }

                objSbSelect.AppendLine(@" GROUP BY
                                                        DBAthon.dbo.Usuario.IdUsuario
                                                      , DataCadastro
                                                      , NomeLogin
                                                      , Email
                                                      , Nome
                                                      , Cpf
                                                      , DataNascimento
                                                      , Telefone
                                                      , Celular
                                                      --, Ativo
                                        ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuario = new UsuarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuario.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        usuario.NomeLogin = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        usuario.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuario.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        usuario.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataNascimento"))))
                        usuario.DataNascimento = Convert.ToDateTime(GetSqlDataReader()["DataNascimento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Telefone"))))
                        usuario.Telefone = Convert.ToString(GetSqlDataReader()["Telefone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Celular"))))
                        usuario.Celular = Convert.ToString(GetSqlDataReader()["Celular"]);

                    //if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                    //    usuario.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    lstUsuario.Add(usuario);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }

            return lstUsuario;
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <returns></returns>
        public UsuarioVO Consultar(UsuarioVO usuarioVo)
        {
            try
            {
                List<UsuarioVO> lstUsuario = Selecionar(usuarioVo);

                return lstUsuario.Count() > 0 ? (UsuarioVO)lstUsuario.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <returns></returns>
        public List<UsuarioVO> Listar(UsuarioVO usuarioVo = null)
        {
            try
            {
                return Selecionar(usuarioVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public List<UsuarioVO> ListarPorPerfil(UsuarioVO usuarioVo = null)
        {
            try
            {
                List<UsuarioVO> lstUsuario = SelecionarPorPerfil(usuarioVo);

                return lstUsuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<UsuarioVO> GetListCadastroBasico()
        {
            UsuarioVO usuario = null;
            List<UsuarioVO> lstUsuarioVO = null;

            try
            {
                lstUsuarioVO = new List<UsuarioVO>();

                while (GetSqlDataReader().Read())
                {
                    usuario = new UsuarioVO();
                    usuario.UsuarioDepartamento = new UsuarioDepartamentoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuario.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        usuario.NomeLogin = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        usuario.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuario.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        usuario.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataNascimento"))))
                        usuario.DataNascimento = Convert.ToDateTime(GetSqlDataReader()["DataNascimento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Telefone"))))
                        usuario.Telefone = Convert.ToString(GetSqlDataReader()["Telefone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Celular"))))
                        usuario.Celular = Convert.ToString(GetSqlDataReader()["Celular"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        usuario.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioDepartamento"))))
                        usuario.UsuarioDepartamento.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        usuario.UsuarioDepartamento.Departamento.Id = Convert.ToInt64(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DepartamentoNome"))))
                        usuario.UsuarioDepartamento.Departamento.Nome = Convert.ToString(GetSqlDataReader()["DepartamentoNome"]);

                    lstUsuarioVO.Add(usuario);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return lstUsuarioVO;
        }


        public List<UsuarioVO> SelecionarCadastroBasico(UsuarioVO usuarioVo)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"    SELECT                                                                                                    ");
                objSbSelect.AppendLine(@"           DBAthon.dbo.Usuario.IdUsuario                                                                  ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.DataCadastro                                                               ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.NomeLogin                                                                  ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Email                                                                      ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Nome                                                                       ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Cpf                                                                        ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.DataNascimento                                                             ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Telefone                                                                   ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Celular                                                                    ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Usuario.Ativo                                                                      ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.UsuarioDepartamento.IdUsuarioDepartamento                                          ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.IdDepartamento                                                            ");
                objSbSelect.AppendLine(@"         , DBAthon.dbo.Departamento.Nome  DepartamentoNome                                                    ");
                objSbSelect.AppendLine(@"      FROM DBAthon.dbo.Usuario                                                                            ");
                objSbSelect.AppendLine(@"LEFT  JOIN DBAthon.dbo.UsuarioDepartamento                                                                ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.UsuarioDepartamento.IdUsuario = DBAthon.dbo.Usuario.IdUsuario                  ");
                objSbSelect.AppendLine(@"LEFT  JOIN DBAthon.dbo.Departamento                                                                           ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Departamento.IdDepartamento = DBAthon.dbo.UsuarioDepartamento.IdDepartamento       ");
                objSbSelect.AppendLine(@"     WHERE 1 = 1                                                                                              ");

                if (usuarioVo != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (usuarioVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioVo.Id;
                    }

                    if (usuarioVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = usuarioVo.DataCadastro;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.NomeLogin))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.NomeLogin = @NomeLogin");
                        GetSqlCommand().Parameters.Add("NomeLogin", SqlDbType.VarChar).Value = usuarioVo.NomeLogin;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Email))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.Email = @Email");
                        GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = usuarioVo.Email;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = usuarioVo.Nome;
                    }

                    if (!string.IsNullOrEmpty(usuarioVo.Cpf))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Usuario.Cpf = @Cpf");
                        GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = usuarioVo.Cpf;
                    }

                    if (usuarioVo.ListaDepartamentoOperar != null)
                    {
                        string sentencaSQL = "AND DBAthon.dbo.Departamento.IdDepartamento in (0";
                        string[] arrValor = usuarioVo.ListaDepartamentoOperar.Split(',');
                        for (int i = 0; i < arrValor.Length; i++)
                        {
                            sentencaSQL = sentencaSQL + "," + arrValor[i];
                        }
                        sentencaSQL = sentencaSQL + ")";
                        objSbSelect.AppendLine(@"" + sentencaSQL + "");
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                return GetListCadastroBasico();

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
        }



        public List<UsuarioVO> SelecionarFuncionariosAtivos(UsuarioVO objVO = null)
        {
            List<UsuarioVO> lstUsuarioVO = null;

            try
            {
                lstUsuarioVO = new List<UsuarioVO>();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"

        SELECT
              Usuario.IdUsuario
            , Usuario.DataCadastro
            , Usuario.NomeLogin
            , Usuario.Email
            , Usuario.DataNascimento
            , Usuario.Telefone
            , Usuario.Celular
            , Usuario.Nome                                  AS UsuarioNome
            , Usuario.Cpf                                   AS UsuarioCpf
            , Usuario.Ativo                                 AS UsuarioAtivo

            

        FROM DBAthon.dbo.Usuario
        
        WHERE 1 = 1

        AND Usuario.Ativo = 1

                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();


                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND Usuario.IdUsuario = @IdUsuario ");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                var reader = GetSqlDataReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var UsuarioVO = new UsuarioVO();

                        UsuarioVO.Id = reader.GetValue<long>("IdUsuario");
                        UsuarioVO.Nome = reader.GetValue<string>("UsuarioNome");
                        UsuarioVO.Cpf = reader.GetValue<string>("UsuarioCpf");
                        UsuarioVO.Ativo = reader.GetValue<bool>("UsuarioAtivo");

                        lstUsuarioVO.Add(UsuarioVO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }

            return lstUsuarioVO;
        }


        // ApiAuthenticate
        public List<UsuarioVO> ApiAuthenticate(UsuarioVO objVO)
        {
            List<UsuarioVO> lstUsuario = null;

            try
            {
                lstUsuario = new List<UsuarioVO>();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"

        SELECT DISTINCT

              US.IdUsuario               AS IdUsuario
            , US.Cpf                     AS UsuarioCpf
            , US.Email                   AS UsuarioEmail
            , US.Nome                    AS UsuarioNome
            , US.NomeLogin               AS UsuarioLogin
            , SS.Senha                   AS UsuarioSenha
            , SS.DataTermino             AS DataTermino
            , PF.Descricao               AS UsuarioPerfil
            , FC.Nome                    AS UsuarioRegra
            , UP.Ativar                  AS AtivoPerfil
            , US.Ativo                   AS Ativo

        FROM DBAthon.dbo.Usuario AS US                WITH (NOLOCK)
        JOIN DBAthon.dbo.UsuarioSenha AS SS           WITH (NOLOCK) ON SS.IdUsuarioSenha = (SELECT MAX(X.IdUsuarioSenha) FROM DBAthon.dbo.UsuarioSenha X WHERE X.IdUsuario = US.IdUsuario)
        JOIN DBAthon.dbo.UsuarioCampus AS UC          WITH (NOLOCK) ON UC.IdUsuario = US.IdUsuario
        JOIN DBAthon.dbo.UsuarioPerfil AS UP          WITH (NOLOCK) ON UC.IdUsuarioCampus = UP.IdUsuarioCampus
   LEFT JOIN DBAthon.dbo.Perfil AS PF                 WITH (NOLOCK) ON PF.IdPerfil = UP.IdPerfil
   LEFT JOIN DBAthon.dbo.PerfilModulo AS PM           WITH (NOLOCK) ON PM.IdPerfil = PF.IdPerfil
   LEFT JOIN DBAthon.dbo.PerfilSubModulo AS PSM       WITH (NOLOCK) ON PSM.IdPerfilModulo = PM.IdPerfilModulo
   LEFT JOIN DBAthon.dbo.PerfilFuncionalidade AS PFM  WITH (NOLOCK) ON PFM.IdPerfilSubModulo = PSM.IdPerfilSubModulo
   LEFT JOIN DBAthon.dbo.Funcionalidade AS FC         WITH (NOLOCK) ON FC.IdFuncionalidade = PFM.IdFuncionalidade


        WHERE 1 = 1

            --AND FC.Nome LIKE 'AcessoApi -%'
            AND FC.Nome IS NOT NULL


                ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND US.IdUsuario = @IdUsuario ");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.NomeLogin))
                    {
                        objSbSelect.AppendLine(" AND US.NomeLogin = @UsuarioLogin ");
                        GetSqlCommand().Parameters.Add("UsuarioLogin", SqlDbType.VarChar).Value = objVO.NomeLogin;
                    }

                    if (!string.IsNullOrEmpty(objVO.UsuarioSenha.Senha))
                    {
                        objSbSelect.AppendLine(" AND SS.Senha = @UsuarioSenha ");
                        GetSqlCommand().Parameters.Add("UsuarioSenha", SqlDbType.VarChar).Value = objVO.UsuarioSenha.Senha;
                    }
                }

                //objSbSelect.AppendLine(@"  ORDER BY UsuarioNome  ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                var reader = GetSqlDataReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var Usuario = new UsuarioVO();

                        Usuario.Id = reader.GetValue<Int64>("IdUsuario");
                        Usuario.Nome = reader.GetValue<String>("UsuarioNome");
                        Usuario.NomeLogin = reader.GetValue<String>("UsuarioLogin");
                        Usuario.Cpf = reader.GetValue<String>("UsuarioCpf");
                        Usuario.Email = reader.GetValue<String>("UsuarioEmail");
                        Usuario.UsuarioSenha.Senha = reader.GetValue<String>("UsuarioSenha");
                        Usuario.UsuarioSenha.DataTermino = reader.GetValue<DateTime?>("DataTermino");
                        Usuario.Ativo = reader.GetValue<Boolean?>("Ativo");

                        Usuario.UsuarioPerfil.Perfil.Descricao = reader.GetValue<String>("UsuarioPerfil");
                        Usuario.UsuarioPerfil.Perfil.Regra = reader.GetValue<String>("UsuarioRegra");
                        Usuario.UsuarioPerfil.Ativar = reader.GetValue<Boolean?>("AtivoPerfil");

                        lstUsuario.Add(Usuario);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Close();
            }

            return lstUsuario;
        }


        public List<UsuarioVO> Selecionar(string sql)
        {
            List<UsuarioVO> lstUsuario = null;

            try
            {
                lstUsuario = new List<UsuarioVO>();

                objSbSelect = new StringBuilder();

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                GetSqlCommand().Parameters.Clear();


                while (GetSqlDataReader().Read())
                {
                    var usuario = new UsuarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuario.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        usuario.NomeLogin = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        usuario.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuario.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstUsuario.Add(usuario);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }

            return lstUsuario;
        }


        public Dictionary<int, List<UsuarioVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<UsuarioVO>> dictionany = null;

            try
            {
                List<UsuarioVO> lstUsuario;

                dictionany = new Dictionary<int, List<UsuarioVO>>();

                var sbPaginar = new StringBuilder();

                int total = GetTotalResgistro(structs);

                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY Nome");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");

                lstUsuario = Selecionar(sbPaginar.ToString());

                dictionany.Add(total, lstUsuario);

                return dictionany;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Dictionary<int, List<UsuarioVO>> PaginarDadosBasicos(string structs, int inicio, int fim)
        {
            Dictionary<int, List<UsuarioVO>> dictionany = null;

            try
            {
                List<UsuarioVO> lstUsuario;

                dictionany = new Dictionary<int, List<UsuarioVO>>();

                var sbPaginar = new StringBuilder();

                int total = GetTotalResgistro(structs);

                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY Nome");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");

                lstUsuario = SelecionarDadosBasico(sbPaginar.ToString()).GroupBy(x => x.Id).Select(x => x.First()).ToList();

                dictionany.Add(total, lstUsuario);

                return dictionany;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<UsuarioVO> SelecionarDadosBasico(string sql)
        {
            List<UsuarioVO> lstUsuario = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstUsuario = new List<UsuarioVO>();

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                GetSqlCommand().Parameters.Clear();

                return GetListCadastroBasico();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }
        }

    }
}