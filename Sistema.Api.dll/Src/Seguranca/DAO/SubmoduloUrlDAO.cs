using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class SubmoduloUrlDAO : AbstractDAO, IDAO<SubmoduloUrlVO>
    {
        public SubmoduloUrlDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }
        //                                                                                                 
        // MÉTODO RESPONSÁVEL POR INCLUIR [ SubmoduloUrl ]                         
        //                                                                                                 
        public long Inserir(SubmoduloUrlVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdSubmoduloUrl = GetCodigoSequece("DBAthon.dbo.SeqSubmoduloUrl");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.SubmoduloUrl      
                                         (                                              
                                                      IdSubmoduloUrl     
                                                   ,  IdSubmodulo        
                                                   ,  Url                
                                         )
                                         VALUES 
                                         (
                                                      @IdSubmoduloUrl    
                                                   ,  @IdSubmodulo       
                                                   ,  @Url               
                                         )");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubmoduloUrl", SqlDbType.Int).Value = IdSubmoduloUrl;
                GetSqlCommand().Parameters.Add("IdSubmodulo", SqlDbType.Int).Value = objVO.Submodulo.Id;
                GetSqlCommand().Parameters.Add("Url", SqlDbType.VarChar).Value = objVO.Url;
                GetSqlCommand().ExecuteNonQuery();
                return IdSubmoduloUrl;

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
        // MÉTODO RESPONSÁVEL POR ALTERAR UM(A) [ SubmoduloUrl ] SELECIONADO            
        //                                                                                                 
        public long Alterar(SubmoduloUrlVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.SubmoduloUrl 
                                        SET IdSubmodulo = @IdSubmodulo   
                                          , Url = @Url                       ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdSubmoduloUrl = @IdSubmoduloUrl");
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
                    GetSqlCommand().Parameters.Add("IdSubmoduloUrl", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdSubmodulo", SqlDbType.Int).Value = objVO.Submodulo.Id;
                    GetSqlCommand().Parameters.Add("Url", SqlDbType.VarChar).Value = objVO.Url;

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


        //                                                                                              
        // MÉTODO RESPONSÁVEL POR DELETAR UM(A) [ SubmoduloUrl ] SELECIONADO      
        //                                                                                              
        public void Deletar(SubmoduloUrlVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.SubmoduloUrl", "IdSubmoduloUrl", objVO.Id);
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.SubmoduloUrl WHERE IdSubmoduloUrl = @IdSubmoduloUrl");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubmoduloUrl", SqlDbType.Int).Value = objVO.Id;

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
        // MÉTODO RESPONSÁVEL POR LISTAR [ SubmoduloUrl ]                                   
        //                                                                                                        
        public List<SubmoduloUrlVO> Listar(SubmoduloUrlVO objVO)
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

        public SubmoduloUrlVO Consultar(SubmoduloUrlVO objVO)
        {
            try
            {
                List<SubmoduloUrlVO> lstSubmoduloUrlVO = Selecionar(objVO);
                return lstSubmoduloUrlVO.Count > 0 ? (SubmoduloUrlVO)lstSubmoduloUrlVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR CAPTURAR A LISTA [ SubmoduloUrl ]                          
        //                                                                                                         
        public List<SubmoduloUrlVO> GetLista()
        {
            SubmoduloUrlVO SubmoduloUrl = null;
            List<SubmoduloUrlVO> lstSubmoduloUrlVO = null;
            try
            {
                lstSubmoduloUrlVO = new List<SubmoduloUrlVO>();
                while (GetSqlDataReader().Read())
                {
                    SubmoduloUrl = new SubmoduloUrlVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubmoduloUrl"))))
                        SubmoduloUrl.Id = Convert.ToInt64(GetSqlDataReader()["IdSubmoduloUrl"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubmodulo"))))
                        SubmoduloUrl.Submodulo.Id = Convert.ToInt64(GetSqlDataReader()["IdSubmodulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Url"))))
                        SubmoduloUrl.Url = Convert.ToString(GetSqlDataReader()["Url"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Submodulo"))))
                        SubmoduloUrl.NomeSubModulo = Convert.ToString(GetSqlDataReader()["Submodulo"]);

                    lstSubmoduloUrlVO.Add(SubmoduloUrl);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstSubmoduloUrlVO;
        }
        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR SELECIONAR [ SubmoduloUrl ]                                
        //                                                                                                         
        public List<SubmoduloUrlVO> Selecionar(SubmoduloUrlVO objVO, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT 
                                                SubmoduloUrl.IdSubmoduloUrl     
                                             ,  SubmoduloUrl.IdSubmodulo        
                                             ,  SubmoduloUrl.Url   
                                             ,  Submodulo.Nome Submodulo   

                                           FROM DBAthon.dbo.SubmoduloUrl
                                     INNER JOIN DBAthon.dbo.Submodulo ON                                      
                                                DBAthon.dbo.Submodulo.IdSubmodulo = SubmoduloUrl.IdSubmodulo 

                                          WHERE 1 = 1                                                               ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.SubmoduloUrl.IdSubmoduloUrl = @IdSubmoduloUrl");
                        GetSqlCommand().Parameters.Add("IdSubmoduloUrl", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Submodulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.SubmoduloUrl.IdSubmodulo = @IdSubmodulo");
                        GetSqlCommand().Parameters.Add("IdSubmodulo", SqlDbType.Int).Value = objVO.Submodulo.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Url))
                    {
                        if (objVO.FiltroUrl == 1)
                            objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.SubmoduloUrl.Url LIKE '%{0}%' ", objVO.Url));
                        else if (objVO.FiltroUrl == 2)
                            objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.SubmoduloUrl.Url LIKE '{0}%' ", objVO.Url));
                        else if (objVO.FiltroUrl == 3)
                            objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.SubmoduloUrl.Url LIKE '%{0}' ", objVO.Url));
                        else
                            objSbSelect.AppendLine(string.Format(" AND DBAthon.dbo.SubmoduloUrl.Url = '{0}' ", objVO.Url));
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

        public Dictionary<int, List<SubmoduloUrlVO>> Paginar(SubmoduloUrlVO objVO)
        {
            Dictionary<int, List<SubmoduloUrlVO>> dictionary = null;
            try
            {
                List<SubmoduloUrlVO> lstSubmoduloUrlVO;
                dictionary = new Dictionary<int, List<SubmoduloUrlVO>>();
                var sbPaginar = new StringBuilder();
                lstSubmoduloUrlVO = Selecionar(objVO);
                dictionary.Add(lstSubmoduloUrlVO.Count, lstSubmoduloUrlVO);
                return dictionary;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
