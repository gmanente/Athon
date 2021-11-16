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
    public class SubModuloTutorialDAO : AbstractDAO, IDAO<SubmoduloTutorialVO>
    {
        public SubModuloTutorialDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }


        public long Alterar(SubmoduloTutorialVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.SubModuloTutorial      ");
                objSbUpdate.AppendLine(@"  SET                                         ");
                objSbUpdate.AppendLine(@"      IdSubModulo  = @IdSubModulo             ");
                objSbUpdate.AppendLine(@"     ,Titulo = @Titulo                        ");
                objSbUpdate.AppendLine(@"     ,Descricao  = @Descricao                 ");
                objSbUpdate.AppendLine(@"     ,IdArquivoExtensao  = @IdArquivoExtensao ");
                objSbUpdate.AppendLine(@"     ,Arquivo  = @Arquivo                     ");
                objSbUpdate.AppendLine(@"     ,Versao  = @Versao                       ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdSubModuloTutorial = @IdSubModuloTutorial");
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

                    GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.Submodulo.Id;
                    GetSqlCommand().Parameters.Add("Titulo", SqlDbType.VarChar).Value = objVO.Titulo;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("IdArquivoExtensao", SqlDbType.Int).Value = objVO.ArquivoExtensao.Id;
                    GetSqlCommand().Parameters.Add("Arquivo", SqlDbType.VarChar).Value = objVO.Arquivo;
                    GetSqlCommand().Parameters.Add("Versao", SqlDbType.VarChar).Value = objVO.Versao;

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdSubModuloTutorial", SqlDbType.Int).Value = objVO.Submodulo.Id;
                    }

                    GetSqlCommand().ExecuteNonQuery();
                }

                return objVO.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }


        public void Deletar(SubmoduloTutorialVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.SubModuloTutorial WHERE IdSubModuloTutorial = @IdSubModuloTutorial");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubModuloTutorial", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbDelete != null)
                {
                    objSbDelete = null;
                }
            }
        }


        public long Inserir(SubmoduloTutorialVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long idSubModuloTutorial = GetCodigoSequece("DBAthon.dbo.SubModuloTutorial");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.SubModuloTutorial  ");
                objSbInsert.AppendLine(@"(                                              ");
                objSbInsert.AppendLine(@"   IdSubModuloTutorial                         ");
                objSbInsert.AppendLine(@"  ,IdSubModulo                                 ");
                objSbInsert.AppendLine(@"  ,Titulo                                      ");
                objSbInsert.AppendLine(@"  ,Descricao                                   ");
                objSbInsert.AppendLine(@"  ,IdArquivoExtensao                           ");
                objSbInsert.AppendLine(@"  ,Arquivo                                     ");
                objSbInsert.AppendLine(@"  ,Versao                                      ");
                objSbInsert.AppendLine(@"  ,IdUsuario                                   ");
                objSbInsert.AppendLine(@"  ,DataCadastro                                ");
                objSbInsert.AppendLine(@")                                              ");
                objSbInsert.AppendLine(@"     VALUES                                    ");
                objSbInsert.AppendLine(@"(            @IdSubModuloTutorial              ");
                objSbInsert.AppendLine(@"           , @IdSubModulo                      ");
                objSbInsert.AppendLine(@"           , @Titulo                           ");
                objSbInsert.AppendLine(@"           , @Descricao                        ");
                objSbInsert.AppendLine(@"           , @IdArquivoExtensao                ");
                objSbInsert.AppendLine(@"           , @Arquivo                          ");
                objSbInsert.AppendLine(@"           , @Versao                           ");
                objSbInsert.AppendLine(@"           , @IdUsuario                        ");
                objSbInsert.AppendLine(@"           , @DataCadastro                     ");
                objSbInsert.AppendLine(@")                                              ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubModuloTutorial", SqlDbType.Int).Value = idSubModuloTutorial;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.Submodulo.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.DateTime).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("IdArquivoExtensao", SqlDbType.Int).Value = objVO.ArquivoExtensao.Id;
                GetSqlCommand().Parameters.Add("Arquivo", SqlDbType.VarChar).Value = objVO.Arquivo;
                GetSqlCommand().Parameters.Add("Versao", SqlDbType.VarChar).Value = objVO.Versao;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.VarChar).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;

                GetSqlCommand().ExecuteNonQuery();

                return idSubModuloTutorial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }


        public List<SubmoduloTutorialVO> Selecionar(SubmoduloTutorialVO objVO = null, int top = 0)
        {
            SubmoduloTutorialVO submoduloTutorial = null;
            List<SubmoduloTutorialVO> lstSubmoduloTutorial = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstSubmoduloTutorial = new List<SubmoduloTutorialVO>();

                string varTop = "";

                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                                          ").Append(varTop);
                objSbSelect.AppendLine(@"        SubModuloTutorial.*
		                                        ,SubModulo.Nome AS NomeSubModulo
		                                        ,ArquivoTipo.Descricao AS DescricaoArquivoTipo
		                                        ,ArquivoTipo.Icone AS IconeArquivoTipo
                                            FROM DBAthon.dbo.SubModuloTutorial
                                      INNER JOIN DBAthon.dbo.SubModulo
                                              ON SubModulo.IdSubModulo = SubModuloTutorial.IdSubModulo
                                      INNER JOIN DBAthon.dbo.ArquivoExtensao
                                              ON ArquivoExtensao.IdArquivoExtensao = SubModuloTutorial.IdArquivoExtensao
                                      INNER JOIN DBAthon.dbo.ArquivoTipo
                                              ON ArquivoTipo.IdArquivoTipo = ArquivoExtensao.IdArquivoTipo

                                          WHERE 1 = 1
");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdSubModuloTutorial = @IdSubModuloTutorial");
                        GetSqlCommand().Parameters.Add("IdSubModuloTutorial", SqlDbType.Int).Value = objVO.Id;
                    }
                }

                objSbSelect.AppendLine(" ORDER BY SubModulo.Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    submoduloTutorial = new SubmoduloTutorialVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModuloTutorial"))))
                        submoduloTutorial.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModuloTutorial"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        submoduloTutorial.Submodulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        submoduloTutorial.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Titulo"))))
                        submoduloTutorial.Titulo = Convert.ToString(GetSqlDataReader()["Titulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        submoduloTutorial.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdArquivoExtensao"))))
                        submoduloTutorial.ArquivoExtensao.Id = Convert.ToInt32(GetSqlDataReader()["IdArquivoExtensao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Arquivo"))))
                        submoduloTutorial.Arquivo = Convert.ToString(GetSqlDataReader()["Arquivo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Versao"))))
                        submoduloTutorial.Versao = Convert.ToString(GetSqlDataReader()["Versao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        submoduloTutorial.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        submoduloTutorial.Submodulo.Nome = Convert.ToString(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DescricaoArquivoTipo"))))
                        submoduloTutorial.ArquivoExtensao.ArquivoTipo.Descricao = Convert.ToString(GetSqlDataReader()["DescricaoArquivoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IconeArquivoTipo"))))
                        submoduloTutorial.ArquivoExtensao.ArquivoTipo.Icone = Convert.ToString(GetSqlDataReader()["IconeArquivoTipo"]);

                    lstSubmoduloTutorial.Add(submoduloTutorial);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }

            return lstSubmoduloTutorial;
        }


        public SubmoduloTutorialVO Consultar(SubmoduloTutorialVO objVO)
        {
            try
            {
                List<SubmoduloTutorialVO> lst = Selecionar(objVO);

                return lst.Count() > 0 ? (SubmoduloTutorialVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SubmoduloTutorialVO> Listar(SubmoduloTutorialVO objVO)
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


        // fim dos métodos
    }
}
