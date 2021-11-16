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
    public class UsuarioModuloDAO : AbstractDAO, IDAO<UsuarioModuloVO>
    {
        public UsuarioModuloDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(UsuarioModuloVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long idUsuarioModulo = GetCodigoSequece("DBAthon.dbo.SeqUsuarioModulo   ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.UsuarioModulo                   ");
                objSbInsert.AppendLine(@"(                                                           ");
                objSbInsert.AppendLine(@"             IdUsuarioModulo                                ");
                objSbInsert.AppendLine(@"           , IdUsuarioCampus                                ");
                objSbInsert.AppendLine(@"           , IdModulo                                       ");
                objSbInsert.AppendLine(@"           , Ativar                                         ");
                objSbInsert.AppendLine(@" )                                                          ");
                objSbInsert.AppendLine(@"     VALUES                                                 ");
                objSbInsert.AppendLine(@"(                                                           ");
                objSbInsert.AppendLine(@"             @IdUsuarioModulo                               ");
                objSbInsert.AppendLine(@"           , @IdUsuarioCampus                                ");
                objSbInsert.AppendLine(@"           , @IdModulo                                      ");
                objSbInsert.AppendLine(@"           , @Ativar                                        ");
                objSbInsert.AppendLine(@" )                                                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = idUsuarioModulo;
                GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.UsuarioCampus.Id;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                GetSqlCommand().ExecuteNonQuery();

                return idUsuarioModulo;
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

        public long Alterar(UsuarioModuloVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.UsuarioModulo     ");
                objSbUpdate.AppendLine(@"  SET                                    ");
                if (objVO.UsuarioCampus.Id > 0)
                {
                    objSbUpdate.AppendLine(@"     IdUsuarioCampus = @IdUsuarioCampus  ");
                }
                if (objVO.Modulo.Id > 0)
                {
                    objSbUpdate.AppendLine(@"    ,IdModulo  = @IdModulo               ");
                }
                if (objVO.Ativar != null)
                {
                    objSbUpdate.AppendLine(@"    ,Ativar    = @Ativar                 ");
                }


                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE 1 = 1                                    ");
                    if (objVO.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdUsuarioModulo = @IdUsuarioModulo     ");
                    }
                    if (objVO.UsuarioCampus.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdUsuarioCampus = @IdUsuarioCampus     ");
                    }
                    if (objVO.Modulo.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdModulo = @IdModulo                   ");
                    }
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

                    if (objVO.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.UsuarioCampus.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.UsuarioCampus.Id;

                    }
                    if (objVO.Modulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                    }
                    if (objVO.Ativar != null)
                    {
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
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

        public void Deletar(UsuarioModuloVO usuarioModuloVo)
        {
            throw new NotImplementedException();
        }


        // DesativarUsuarioModulo
        public void DesativarUsuarioModulo(List<UsuarioModuloVO> lst)
        {
            if (lst.Count() > 0)
            {
                foreach (var l in lst)
                {
                    l.Ativar = false;
                    Alterar(l);
                }
            }
        }

        public void Inserir(List<UsuarioModuloVO> lstUsuarioModuloVo, long idUsuarioCampus = 0)
        {
            try
            {
                //lista atual do banco de dados
                var lstUsuarioModuloVODB = Listar(new UsuarioModuloVO()
                {
                    UsuarioCampus = { Id = idUsuarioCampus }
                });

                //DesativarUsuarioModulo
                DesativarUsuarioModulo(lstUsuarioModuloVODB);

                //Se a lista do form de dados possui pelo menos 1 objeto
                if (lstUsuarioModuloVo.Count > 0)
                {
                    foreach (var diff in lstUsuarioModuloVo)
                    {
                        var lst = from p in lstUsuarioModuloVODB
                                  where p.UsuarioCampus.Id == diff.UsuarioCampus.Id && p.Modulo.Id == diff.Modulo.Id
                                  select p;

                        if (lst.Count() > 0)
                        {
                            Alterar(diff);
                        }
                        else
                        {
                            Inserir(diff);
                        }

                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<UsuarioModuloVO> Selecionar(UsuarioModuloVO usuarioModuloVo = null, int top = 0)
        {
            UsuarioModuloVO usuarioModulo = null;
            List<UsuarioModuloVO> lstUsuarioModulo = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioModulo = new List<UsuarioModuloVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                                                                       ").Append(varTop);
                objSbSelect.AppendLine(@"         IdUsuarioModulo                                                                                     ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.UsuarioCampus.IdUsuarioCampus                                                       ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.IdModulo                                                                     ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.Cor                                                                          ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.DataCadastro                                                                 ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.Icone                                                                        ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.IdDepartamento                                                               ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.IdModulo                                                                     ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.IdSistema                                                                    ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.Link                                                                         ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.Modulo.Nome                                                                         ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.UsuarioModulo.IdUsuarioModulo                                                       ");
                objSbSelect.AppendLine(@"       , DBAthon.dbo.UsuarioCampus.Ativar                                                                ");
                objSbSelect.AppendLine(@"   FROM  DBAthon.dbo.UsuarioModulo                                                                       ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.Modulo                                                                            ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Modulo.IdModulo = DBAthon.dbo.UsuarioModulo.IdModulo                              ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.UsuarioCampus                                                                     ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.UsuarioCampus.IdUsuarioCampus = DBAthon.dbo.UsuarioModulo.IdUsuarioCampus         ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                                              ");

                if (usuarioModuloVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (usuarioModuloVo.UsuarioCampus.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioModuloVo.UsuarioCampus.Usuario.Id;
                    }
                    if (usuarioModuloVo.UsuarioCampus.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = usuarioModuloVo.UsuarioCampus.Campus.Id;
                    }
                    if (usuarioModuloVo.UsuarioCampus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioModulo.IdUsuarioCampus = @IdUsuarioCampus");
                        GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = usuarioModuloVo.UsuarioCampus.Id;
                    }
                    if (usuarioModuloVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioModulo.Id = @IdUsuarioModulo");
                        GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = usuarioModuloVo.Id;
                    }
                    if (usuarioModuloVo.Modulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioModulo.IdModulo = @IdModulo");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = usuarioModuloVo.Modulo.Id;
                    }
                    if (usuarioModuloVo.UsuarioCampus.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.Ativar = @AtivarCampus");
                        GetSqlCommand().Parameters.Add("AtivarCampus", SqlDbType.Bit).Value = usuarioModuloVo.UsuarioCampus.Ativar;
                    }
                    if (usuarioModuloVo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioModulo.Ativar = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = usuarioModuloVo.Ativar;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioModulo = new UsuarioModuloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioModulo"))))
                        usuarioModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCampus"))))
                        usuarioModulo.UsuarioCampus.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioModulo"))))
                        usuarioModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        usuarioModulo.Modulo.Sistema.Id = Convert.ToInt32(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuarioModulo.Modulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuarioModulo.Modulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        usuarioModulo.Modulo.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        usuarioModulo.Modulo.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cor"))))
                        usuarioModulo.Modulo.Cor = Convert.ToString(GetSqlDataReader()["Cor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        usuarioModulo.Modulo.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioModulo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    lstUsuarioModulo.Add(usuarioModulo);
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

            return lstUsuarioModulo;
        }


        /// <summary>
        /// AutenticarModulos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idCampus"></param>
        /// <param name="acessoExterno"></param>
        /// <param name="portal"></param>
        /// <param name="idModulo"></param>
        /// <returns></returns>
        public List<UsuarioModuloVO> AutenticarModulos(long idUsuario, long idCampus, bool acessoExterno, bool? portal = null, long idModulo = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();  

                objSbSelect.AppendLine(@"
                                    
                           SELECT DISTINCT Modulo.IdModulo
                                         , Modulo.IdSistema
                                         , Modulo.IdDepartamento
                                         , Modulo.DataCadastro   
                                         , Modulo.Nome
                                         , Modulo.Icone  
                                         , Modulo.Cor
                                         , Modulo.Link   
                                         , Modulo.LinkTeste
                                         , Modulo.LinkDebug
                                         , Modulo.LinkHomologacao 
                                         , Modulo.Portal

                                      FROM DBAthon.dbo.PerfilModulo

                                INNER JOIN DBAthon.dbo.Modulo
                                        ON Modulo.IdModulo = PerfilModulo.IdModulo

                                INNER JOIN DBAthon.dbo.Perfil
                                        ON Perfil.IdPerfil = PerfilModulo.IdPerfil

                                INNER JOIN DBAthon.dbo.UsuarioPerfil
                                        ON UsuarioPerfil.IdPerfil = Perfil.IdPerfil

                                INNER JOIN DBAthon.dbo.UsuarioCampus
                                        ON UsuarioCampus.IdUsuarioCampus = UsuarioPerfil.IdUsuarioCampus

                                     WHERE UsuarioCampus.IdUsuario = @IdUsuario
                                       AND PerfilModulo.Ativar = 1 
                                       /*AND PerfilModulo.AcessoExterno = IIF(@AcessoExterno = 0, PerfilModulo.AcessoExterno, 1)*/
                                       AND Perfil.Ativar = 1
                                       AND UsuarioPerfil.Ativar = 1
                                       AND GETDATE() BETWEEN UsuarioPerfil.DataInicio AND UsuarioPerfil.DataTermino
                                       AND UsuarioCampus.Ativar = 1 	
                                       /*AND UsuarioCampus.AcessoExterno = IIF(@AcessoExterno = 0, UsuarioCampus.AcessoExterno, 1) */");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                //GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = idCampus;                
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = acessoExterno; 

                if (idModulo > 0)
                {
                    objSbSelect.AppendLine(@" AND Modulo.IdModulo = @IdModulo");
                    GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = idModulo;
                }

                if (idCampus > 0)
                {
                   objSbSelect.AppendLine(@" AND UsuarioCampus.IdCampus = @IdCampus");
                   GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = idCampus;
                }

                if (portal != null)
                {
                    objSbSelect.AppendLine(@" AND Modulo.Portal = @Portal");
                    GetSqlCommand().Parameters.Add("Portal", SqlDbType.Bit).Value = portal;
                }

                objSbSelect.AppendLine(@" ORDER BY Modulo.Nome ");               

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lstUsuarioModuloVO = new List<UsuarioModuloVO>();

                while (GetSqlDataReader().Read())
                {
                    var item = new UsuarioModuloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        item.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        item.Modulo.Sistema.Id = Convert.ToInt32(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        item.Modulo.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        item.Modulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        item.Modulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        item.Modulo.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cor"))))
                        item.Modulo.Cor = Convert.ToString(GetSqlDataReader()["Cor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        item.Modulo.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("LinkTeste"))))
                        item.Modulo.LinkTeste = Convert.ToString(GetSqlDataReader()["LinkTeste"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("LinkDebug"))))
                        item.Modulo.LinkDebug = Convert.ToString(GetSqlDataReader()["LinkDebug"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("LinkHomologacao"))))
                        item.Modulo.LinkHomologacao = Convert.ToString(GetSqlDataReader()["LinkHomologacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Portal"))))
                        item.Modulo.Portal = Convert.ToBoolean(GetSqlDataReader()["Portal"]);

                    lstUsuarioModuloVO.Add(item);
                }

                return lstUsuarioModuloVO;
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
        }


        public UsuarioModuloVO Consultar(UsuarioModuloVO usuarioModuloVo)
        {
            try
            {
                List<UsuarioModuloVO> lstUsuarioModulo = Selecionar(usuarioModuloVo);

                return lstUsuarioModulo.Count() > 0 ? (UsuarioModuloVO)lstUsuarioModulo.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioModuloVO> Listar(UsuarioModuloVO usuarioModuloVo)
        {
            try
            {
                List<UsuarioModuloVO> lstUsuarioModulo = Selecionar(usuarioModuloVo);

                return lstUsuarioModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioModuloVO> Paginar(UsuarioModuloVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}