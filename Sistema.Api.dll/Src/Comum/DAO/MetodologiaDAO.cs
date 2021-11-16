using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class MetodologiaDAO : AbstractDAO, IDAO<VO.MetodologiaVO>
    {
        public MetodologiaDAO(SqlCommand sqlComm)
             : base(sqlComm)
        {
        }
        //                                                                                                 
        // MÉTODO RESPONSÁVEL POR INCLUIR [ Metodologia ]                         
        //                                                                                                 
        public long Inserir(VO.MetodologiaVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdMetodologia = GetCodigoSequece("DBAthon.dbo.SeqMetodologia");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.Metodologia          ");
                objSbInsert.AppendLine(@"(                                                                                             ");
                objSbInsert.AppendLine(@"             IdMetodologia      ");
                objSbInsert.AppendLine(@"          ,  Nome               ");
                objSbInsert.AppendLine(@"          ,  Descricao          ");
                objSbInsert.AppendLine(@"          ,  DataCadastro       ");
                objSbInsert.AppendLine(@"          ,  IdUsuario         ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdMetodologia     ");
                objSbInsert.AppendLine(@"          ,  @Nome              ");
                objSbInsert.AppendLine(@"          ,  @Descricao         ");
                objSbInsert.AppendLine(@"          ,  @DataCadastro      ");
                objSbInsert.AppendLine(@"          ,  @IdUsuario        ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMetodologia", SqlDbType.Int).Value = IdMetodologia;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().ExecuteNonQuery();
                return IdMetodologia;

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

        //                                                                                                 
        // MÉTODO RESPONSÁVEL POR ALTERAR UM(A) [ Metodologia ] SELECIONADO            
        //                                                                                                 
        public long Alterar(VO.MetodologiaVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.Metodologia ");
                objSbUpdate.AppendLine("   SET Nome = @Nome ");
                objSbUpdate.AppendLine("     , Descricao = @Descricao");
                objSbUpdate.AppendLine("     , DataCadastro = @DataCadastro");
                objSbUpdate.AppendLine("     , IdUsuario = @IdUsuario");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdMetodologia = @IdMetodologia");
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
                    GetSqlCommand().Parameters.Add("IdMetodologia", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
                    GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
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


        //                                                                                              
        // MÉTODO RESPONSÁVEL POR DELETAR UM(A) [ Metodologia ] SELECIONADO      
        //                                                                                              
        public void Deletar(VO.MetodologiaVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Metodologia ");
                objSbDelete.AppendLine(" WHERE IdMetodologia = @IdMetodologia");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMetodologia", SqlDbType.Int).Value = objVO.Id;

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

        //                                                                                                        
        // MÉTODO RESPONSÁVEL POR LISTAR [ Metodologia ]                                   
        //                                                                                                        
        public List<VO.MetodologiaVO> Listar(VO.MetodologiaVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR SELECIONAR [ Metodologia ]                                
        //                                                                                                         
        public List<VO.MetodologiaVO> Selecionar(string sql)
        {
            try
            {
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                return GetLista();
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


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public MetodologiaVO Consultar(MetodologiaVO objVO)
        {
            try
            {
                var lstMetodologiaVO = Selecionar(objVO);

                return lstMetodologiaVO.Count > 0 ? (MetodologiaVO)lstMetodologiaVO.ToArray().GetValue(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR CAPTURAR A LISTA [ Metodologia ]                          
        //                                                                                                         
        public List<VO.MetodologiaVO> GetLista()
        {
            VO.MetodologiaVO metodologia = null;
            List<VO.MetodologiaVO> lstMetodologiaVO = null;
            try
            {
                lstMetodologiaVO = new List<VO.MetodologiaVO>();
                while (GetSqlDataReader().Read())
                {
                    metodologia = new VO.MetodologiaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMetodologia"))))
                        metodologia.Id = Convert.ToInt64(GetSqlDataReader()["IdMetodologia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        metodologia.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        metodologia.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        metodologia.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        metodologia.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    lstMetodologiaVO.Add(metodologia);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstMetodologiaVO;
        }


        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR SELECIONAR [ Metodologia ]                                
        //                                                                                                         
        public List<VO.MetodologiaVO> Selecionar(VO.MetodologiaVO objVO, int top = 0)
        {
            VO.MetodologiaVO metodologia = null;
            List<VO.MetodologiaVO> lstMetodologia = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstMetodologia = new List<VO.MetodologiaVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@" SELECT 
	                                           IdMetodologia
                                              ,Nome
                                              ,Descricao
                                              ,DataCadastro
                                              ,IdUsuario
                                          FROM DBAthon.dbo.Metodologia
                                          WHERE 1 = 1 ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Metodologia.IdMetodologia = @IdMetodologia");
                        GetSqlCommand().Parameters.Add("IdMetodologia", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Metodologia.Nome = @IdNome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                return GetLista();
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
        }


        /// <summary>
        /// ConsultarWeb
        /// </summary>
        /// <param name="idMetodologia"></param>
        /// <returns></returns>        
        public MetodologiaVO ConsultarWeb(long idMetodologia)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT IdMetodologia
                                                ,Nome
                                                ,Descricao
                                            FROM DBAthon.dbo.Metodologia
                                           WHERE IdMetodologia = @IdMetodologia ");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdMetodologia", SqlDbType.Int).Value = idMetodologia;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lstVO = new List<MetodologiaVO>();

                while (GetSqlDataReader().Read())
                {
                    var item = new MetodologiaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdMetodologia"))))
                        item.Id = Convert.ToInt64(GetSqlDataReader()["IdMetodologia"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        item.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        item.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstVO.Add(item);
                }

                return lstVO.Any() ? lstVO[0] : null;
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
        }

        // Fim dos métodos
    }
}
