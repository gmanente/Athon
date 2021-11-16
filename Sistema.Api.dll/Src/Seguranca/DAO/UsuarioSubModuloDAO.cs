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
    public class UsuarioSubmoduloDAO : AbstractDAO, IDAO<UsuarioSubModuloVO>
    {
        public UsuarioSubmoduloDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(UsuarioSubModuloVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long idUsuarioSubModulo = GetCodigoSequece("DBAthon.dbo.SeqUsuarioSubModulo   ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.UsuarioSubModulo             ");
                objSbInsert.AppendLine(@"(                                                        ");
                objSbInsert.AppendLine(@"             idUsuarioSubModulo                          ");
                objSbInsert.AppendLine(@"           , IdUsuarioModulo                             ");
                objSbInsert.AppendLine(@"           , IdSubModulo                                 ");
                objSbInsert.AppendLine(@"           , Ativar                                      ");
                objSbInsert.AppendLine(@" )                                                       ");
                objSbInsert.AppendLine(@"     VALUES                                              ");
                objSbInsert.AppendLine(@"(                                                        ");
                objSbInsert.AppendLine(@"             @idUsuarioSubModulo                         ");
                objSbInsert.AppendLine(@"           , @IdUsuarioModulo                            ");
                objSbInsert.AppendLine(@"           , @IdSubModulo                                ");
                objSbInsert.AppendLine(@"           , @Ativar                                     ");
                objSbInsert.AppendLine(@" )                                                       ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("idUsuarioSubModulo", SqlDbType.Int).Value = idUsuarioSubModulo;
                GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = objVO.UsuarioModulo.Id;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                GetSqlCommand().ExecuteNonQuery();

                return idUsuarioSubModulo;
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


        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Alterar(UsuarioSubModuloVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.UsuarioSubModulo     ");
                objSbUpdate.AppendLine(@"  SET                                       ");

                if (objVO.UsuarioModulo.Id > 0)
                {
                    objSbUpdate.AppendLine(@"     IdUsuarioModulo = @IdUsuarioModulo  ");
                }

                if (objVO.SubModulo.Id > 0)
                {
                    objSbUpdate.AppendLine(@"    ,IdSubModulo  = @IdSubModulo         ");
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
                        objSbUpdate.AppendLine(@"AND IdUsuarioSubModulo = @IdUsuarioSubModulo     ");
                    }
                    if (objVO.UsuarioModulo.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdUsuarioModulo = @IdUsuarioModulo     ");
                    }
                    if (objVO.SubModulo.Id > 0)
                    {
                        objSbUpdate.AppendLine(@"AND IdSubModulo = @IdSubModulo                   ");
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
                        GetSqlCommand().Parameters.Add("IdUsuarioSubModulo", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.UsuarioModulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = objVO.UsuarioModulo.Id;

                    }
                    if (objVO.SubModulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
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


        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="usuarioSubModuloVo"></param>
        public void Deletar(UsuarioSubModuloVO usuarioSubModuloVo)
        {
            throw new NotImplementedException();
        }


        // DesativarUsuarioSubmodulo
        public void DesativarUsuarioSubmodulo(List<UsuarioSubModuloVO> lst)
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


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="lstUsuarioSubModuloVo"></param>
        /// <param name="idUsuarioCampus"></param>
        /// <param name="idUsuarioModulo"></param>
        public void Inserir(List<UsuarioSubModuloVO> lstUsuarioSubModuloVo, long idUsuarioCampus = 0, long idUsuarioModulo = 0)
        {
            try
            {
                //lista atual do banco de dados
                var lstUsuarioSubmoduloVODB = Listar(new UsuarioSubModuloVO()
                {
                    UsuarioModulo =
                    {
                        Id = idUsuarioModulo,
                        UsuarioCampus =
                        {
                            Id = idUsuarioCampus
                        }
                    }
                });


                //DesativarUsuarioSubmodulo
                DesativarUsuarioSubmodulo(lstUsuarioSubmoduloVODB);


                //Se a lista do form de dados possui pelo menos 1 objeto
                if (lstUsuarioSubModuloVo.Count > 0)
                {
                    foreach (var diff in lstUsuarioSubModuloVo)
                    {
                        var lst = from p in lstUsuarioSubmoduloVODB
                                  where p.SubModulo.Id == diff.SubModulo.Id
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


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="usuarioSubModuloVo"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<UsuarioSubModuloVO> Selecionar(UsuarioSubModuloVO usuarioSubModuloVo = null, int top = 0)
        {
            UsuarioSubModuloVO usuarioSubModulo = null;
            List<UsuarioSubModuloVO> lstUsuarioSubModulo = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioSubModulo = new List<UsuarioSubModuloVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                                                                         ").Append(varTop);
                objSbSelect.AppendLine(@"       DBAthon.dbo.UsuarioSubModulo.IdUsuarioSubModulo                                                     ");
                objSbSelect.AppendLine(@"     , DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo                                                        ");
                objSbSelect.AppendLine(@"     , DBAthon.dbo.UsuarioSubModulo.IdSubModulo                                                            ");
                objSbSelect.AppendLine(@"     , DBAthon.dbo.UsuarioSubModulo.Ativar                                                                 ");
                objSbSelect.AppendLine(@"     , DBAthon.dbo.UsuarioModulo.IdUsuarioCampus                                                           ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.SubModulo.DataCadastro  AS dataCadastroSubModulo                                        ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.SubModulo.Icone AS iconeSubModulo                                                       ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.SubModulo.IdModulo                                                                      ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.SubModulo.Link AS linkSubModulo                                                         ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.SubModulo.Nome AS nomeSubModulo                                                         ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.Cor                                                                              ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.DataCadastro  AS dataCadastroModulo                                              ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.Icone AS iconeModulo                                                             ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.IdDepartamento                                                                   ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.IdSistema                                                                        ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.Link AS linkModulo                                                               ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Modulo.Nome AS nomeModulo                                                               ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.UsuarioSubModulo                                                                        ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.UsuarioModulo                                                                      ");
                objSbSelect.AppendLine(@"		 ON DBAthon.dbo.UsuarioModulo.IdUsuarioModulo = DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo    ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.Modulo                                                                             ");
                objSbSelect.AppendLine(@"		 ON DBAthon.dbo.Modulo.IdModulo = DBAthon.dbo.UsuarioModulo.IdModulo                            ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.SubModulo                                                                          ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.SubModulo.IdSubModulo = DBAthon.dbo.UsuarioSubModulo.IdSubModulo               ");
                objSbSelect.AppendLine(@" INNER JOIN DBAthon.dbo.UsuarioCampus                                                                      ");
                objSbSelect.AppendLine(@"         ON DBAthon.dbo.UsuarioCampus.IdUsuarioCampus = DBAthon.dbo.UsuarioModulo.IdUsuarioCampus      ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                                                    ");

                if (usuarioSubModuloVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (usuarioSubModuloVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.IdUsuarioSubModulo = @IdUsuarioSubModulo");
                        GetSqlCommand().Parameters.Add("IdUsuarioSubModulo", SqlDbType.Int).Value = usuarioSubModuloVo.Id;
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo = @IdUsuarioModulo");
                        GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = usuarioSubModuloVo.UsuarioModulo.Id;
                    }
                    if (usuarioSubModuloVo.SubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.IdSubModulo = @IdSubModulo");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = usuarioSubModuloVo.SubModulo.Id;
                    }
                    if (usuarioSubModuloVo.SubModulo.Modulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.SubModulo.IdModulo = @IdModulo");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = usuarioSubModuloVo.SubModulo.Modulo.Id;
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdUsuarioCampus = @IdUsuarioCampus");
                        GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Id;
                    }
                    if (usuarioSubModuloVo.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.Ativar  = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = usuarioSubModuloVo.Ativar;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                while (GetSqlDataReader().Read())
                {
                    usuarioSubModulo = new UsuarioSubModuloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSubModulo"))))
                        usuarioSubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioModulo"))))
                        usuarioSubModulo.UsuarioModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        usuarioSubModulo.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioSubModulo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("dataCadastroSubModulo"))))
                        usuarioSubModulo.SubModulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["dataCadastroSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("nomeSubModulo"))))
                        usuarioSubModulo.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["nomeSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("iconeSubModulo"))))
                        usuarioSubModulo.SubModulo.Icone = Convert.ToString(GetSqlDataReader()["iconeSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkSubModulo"))))
                        usuarioSubModulo.SubModulo.Link = Convert.ToString(GetSqlDataReader()["linkSubModulo"]);

                    ////
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        usuarioSubModulo.SubModulo.Modulo.Sistema.Id = Convert.ToInt32(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("dataCadastroModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["dataCadastroModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("nomeModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Nome = Convert.ToString(GetSqlDataReader()["nomeModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("iconeModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Icone = Convert.ToString(GetSqlDataReader()["iconeModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cor"))))
                        usuarioSubModulo.SubModulo.Modulo.Cor = Convert.ToString(GetSqlDataReader()["Cor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Link = Convert.ToString(GetSqlDataReader()["linkModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCampus"))))
                        usuarioSubModulo.UsuarioModulo.UsuarioCampus.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuarioCampus"]);

                    lstUsuarioSubModulo.Add(usuarioSubModulo);
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

            return lstUsuarioSubModulo;
        }


        public List<UsuarioSubModuloVO> Autenticar(UsuarioSubModuloVO usuarioSubModuloVo)
        {
            UsuarioSubModuloVO usuarioSubModulo = null;
            List<UsuarioSubModuloVO> lstUsuarioSubModulo = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstUsuarioSubModulo = new List<UsuarioSubModuloVO>();

                objSbSelect.AppendLine(@"SELECT                                                                                              ");
                objSbSelect.AppendLine(@"        DBAthon.dbo.UsuarioSubModulo.IdUsuarioSubModulo                                         ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo                                            ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.UsuarioSubModulo.IdSubModulo                                                ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.UsuarioSubModulo.Ativar                                                     ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.SubModulo.DataCadastro                                                      ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.SubModulo.Icone                                                             ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.SubModulo.IdModulo                                                          ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.SubModulo.Link                                                              ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.SubModulo.Nome                                                              ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.UsuarioModulo.Ativar                                                        ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.UsuarioSubModulo                                                             ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.SubModulo ON                                                                ");
                objSbSelect.AppendLine(@"      DBAthon.dbo.UsuarioSubModulo.IdSubModulo = DBAthon.dbo.SubModulo.IdSubModulo             ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.UsuarioModulo ON                                                            ");
                objSbSelect.AppendLine(@"      DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo = DBAthon.dbo.UsuarioModulo.IdUsuarioModulo ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.UsuarioCampus ON                                                            ");
                objSbSelect.AppendLine(@"      DBAthon.dbo.UsuarioModulo.IdUsuarioCampus =  DBAthon.dbo.UsuarioCampus.IdUsuarioCampus   ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                                            ");

                if (usuarioSubModuloVo != null)
                {

                    //IdCampus
                    if (usuarioSubModuloVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.IdUsuarioSubModulo = @IdUsuarioSubModulo");
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.IdUsuarioModulo = @IdUsuarioModulo");
                    }
                    if (usuarioSubModuloVo.SubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.IdSubModulo = @IdSubModulo");
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.Modulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.SubModulo.IdModulo = @IdModulo");
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdCampus = @IdCampus");
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdUsuario = @IdUsuario");
                    }

                    objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.Ativar = @SAtivar");
                    objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioSubModulo.Ativar = @MAtivar");
                    GetSqlCommand().Parameters.Clear();


                    if (usuarioSubModuloVo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioSubModulo", SqlDbType.Int).Value = usuarioSubModuloVo.Id;
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuarioModulo", SqlDbType.Int).Value = usuarioSubModuloVo.UsuarioModulo.Id;
                    }
                    if (usuarioSubModuloVo.SubModulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = usuarioSubModuloVo.SubModulo.Id;
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.Modulo.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = usuarioSubModuloVo.UsuarioModulo.Modulo.Id;
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Campus.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Campus.Id;
                    }
                    if (usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Usuario.Id > 0)
                    {
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = usuarioSubModuloVo.UsuarioModulo.UsuarioCampus.Usuario.Id;
                    }
                    GetSqlCommand().Parameters.Add("SAtivar", SqlDbType.Bit).Value = usuarioSubModuloVo.Ativar;
                    GetSqlCommand().Parameters.Add("MAtivar", SqlDbType.Bit).Value = usuarioSubModuloVo.UsuarioModulo.Ativar;
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    usuarioSubModulo = new UsuarioSubModuloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioSubModulo"))))
                        usuarioSubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioModulo"))))
                        usuarioSubModulo.UsuarioModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        usuarioSubModulo.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        usuarioSubModulo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    //SubModulo
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        usuarioSubModulo.SubModulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        usuarioSubModulo.SubModulo.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        usuarioSubModulo.SubModulo.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        usuarioSubModulo.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstUsuarioSubModulo.Add(usuarioSubModulo);
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

            return lstUsuarioSubModulo;
        }

        


        /// <summary>
        /// AutenticarSubModulos
        /// </summary>
        /// <param name="idCampus"></param>
        /// <param name="idUsuario"></param>
        /// <param name="urlModulo"></param>
        /// <param name="acessoExteno"></param>
        /// <returns></returns>
        public List<UsuarioSubModuloVO> AutenticarSubModulos(long idCampus, long idUsuario, string urlModulo, bool acessoExteno)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                        SELECT   DISTINCT  SubModulo.IdSubModulo
                                         , SubModulo.DataCadastro            AS dataCadastroSubModulo
                                         , SubModulo.Nome                    AS nomeSubModulo
                                         , SubModulo.Icone                   AS iconeSubModulo
                                         , SubModulo.IdModulo
                                         , SubModulo.Link                    AS linkSubModulo
                                         , SubModulo.Ordem
                                         , SubModulo.IdSubModuloPai

                                         , Modulo.IdSistema
                                         , Modulo.IdDepartamento
                                         , Modulo.DataCadastro               AS dataCadastroModulo
                                         , Modulo.Nome                       AS nomeModulo
                                         , Modulo.Icone                      AS iconeModulo 
                                         , Modulo.Cor
                                         , Modulo.Link                       AS linkModulo
                                         , Modulo.LinkTeste                  AS linkTesteModulo
                                         , Modulo.LinkDebug                  AS linkDebugModulo
                                         , Modulo.LinkHomologacao            AS linkHomologacaoModulo
                                         
                                      FROM DBAthon.dbo.PerfilSubModulo

                                INNER JOIN DBAthon.dbo.SubModulo 
                                        ON SubModulo.IdSubModulo = PerfilSubModulo.IdSubModulo

                                INNER JOIN DBAthon.dbo.PerfilModulo
                                        ON PerfilModulo.IdPerfilModulo = PerfilSubModulo.IdPerfilModulo

                                INNER JOIN DBAthon.dbo.Modulo		
                                        ON Modulo.IdModulo = PerfilModulo.IdModulo  

                                INNER JOIN DBAthon.dbo.Perfil
                                        ON Perfil.IdPerfil = PerfilModulo.IdPerfil

                                INNER JOIN DBAthon.dbo.UsuarioPerfil
                                        ON UsuarioPerfil.IdPerfil = Perfil.IdPerfil

                                INNER JOIN DBAthon.dbo.UsuarioCampus
                                        ON UsuarioCampus.IdUsuarioCampus = UsuarioPerfil.IdUsuarioCampus

                                     WHERE UsuarioCampus.IdUsuario = @IdUsuario
                                       AND UsuarioCampus.Ativar = 1
                                       --AND UsuarioCampus.AcessoExterno = IIF(@AcessoExterno = 0, UsuarioCampus.AcessoExterno,1) 
                                       AND (Modulo.Link = @UrlModulo OR 
                                            Modulo.LinkTeste = @UrlModulo OR 
                                            Modulo.LinkDebug = @UrlModulo OR
                                            Modulo.LinkHomologacao = @UrlModulo)

                                       AND PerfilSubModulo.Ativar = 1
                                       --AND PerfilSubModulo.AcessoExterno = IIF(@AcessoExterno = 0, PerfilSubModulo.AcessoExterno,1) 
                                       AND PerfilModulo.Ativar = 1
                                       --AND PerfilModulo.AcessoExterno = IIF(@AcessoExterno = 0, PerfilModulo.AcessoExterno,1)
                                       AND Perfil.Ativar = 1
                                       AND UsuarioPerfil.Ativar = 1	 	 		 		 	   		               
                                       AND GETDATE() BETWEEN UsuarioPerfil.DataInicio AND UsuarioPerfil.DataTermino ");

                GetSqlCommand().Parameters.Clear();

                // Altera a urlModulo funcionais para atender multiplos protocolos
                GetSqlCommand().Parameters.Add("UrlModulo", SqlDbType.VarChar).Value = urlModulo.Replace("https:", "").Replace("http:", "");
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = acessoExteno;

                if (idCampus > 0)
                {
                    objSbSelect.AppendLine(@" AND UsuarioCampus.IdCampus = @IdCampus");
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = idCampus;
                }

                objSbSelect.AppendLine(@"ORDER BY Modulo.Nome, SubModulo.Nome");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lstUsuarioSubModuloVO = new List<UsuarioSubModuloVO>();

                while (GetSqlDataReader().Read())
                {
                    var usuarioSubModulo = new UsuarioSubModuloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        usuarioSubModulo.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("dataCadastroSubModulo"))))
                        usuarioSubModulo.SubModulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["dataCadastroSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("nomeSubModulo"))))
                        usuarioSubModulo.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["nomeSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("iconeSubModulo"))))
                        usuarioSubModulo.SubModulo.Icone = Convert.ToString(GetSqlDataReader()["iconeSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkSubModulo"))))
                        usuarioSubModulo.SubModulo.Link = Convert.ToString(GetSqlDataReader()["linkSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ordem"))))
                        usuarioSubModulo.SubModulo.Ordem = Convert.ToInt32(GetSqlDataReader()["Ordem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModuloPai"))))
                        usuarioSubModulo.SubModulo.IdSubModuloPai = Convert.ToInt32(GetSqlDataReader()["IdSubModuloPai"]);


                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        usuarioSubModulo.SubModulo.Modulo.Sistema.Id = Convert.ToInt32(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        usuarioSubModulo.SubModulo.Modulo.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("dataCadastroModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["dataCadastroModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("nomeModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Nome = Convert.ToString(GetSqlDataReader()["nomeModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("iconeModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Icone = Convert.ToString(GetSqlDataReader()["iconeModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cor"))))
                        usuarioSubModulo.SubModulo.Modulo.Cor = Convert.ToString(GetSqlDataReader()["Cor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.Link = Convert.ToString(GetSqlDataReader()["linkModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkTesteModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.LinkTeste = Convert.ToString(GetSqlDataReader()["linkTesteModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkDebugModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.LinkDebug = Convert.ToString(GetSqlDataReader()["linkDebugModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("linkHomologacaoModulo"))))
                        usuarioSubModulo.SubModulo.Modulo.LinkHomologacao = Convert.ToString(GetSqlDataReader()["linkHomologacaoModulo"]);

                    lstUsuarioSubModuloVO.Add(usuarioSubModulo);
                }

                return lstUsuarioSubModuloVO;
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


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="usuarioSubModuloVo"></param>
        /// <returns></returns>
        public UsuarioSubModuloVO Consultar(UsuarioSubModuloVO objVO)
        {
            try
            {
                var lstVO = Selecionar(objVO);

                return lstVO.Count() > 0 ? (UsuarioSubModuloVO)lstVO.ToArray().GetValue(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="usuarioSubModuloVo"></param>
        /// <returns></returns>
        public List<UsuarioSubModuloVO> Listar(UsuarioSubModuloVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// paginar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="inicio"></param>
        /// <param name="fim"></param>
        /// <returns></returns>
        public List<UsuarioSubModuloVO> Paginar(UsuarioSubModuloVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}