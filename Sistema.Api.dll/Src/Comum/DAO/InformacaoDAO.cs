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
    public class InformacaoDAO : AbstractDAO, IDAO<InformacaoVO>
    {
        public InformacaoDAO(SqlCommand sqlComm)
             : base(sqlComm)
        {
        }
        //                                                                                                 
        // MÉTODO RESPONSÁVEL POR INCLUIR [ Informacao ]                         
        //                                                                                                 
        public long Inserir(VO.InformacaoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdInformacao = GetCodigoSequece("DBAthon.dbo.SeqInformacao");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.Informacao     ");
                objSbInsert.AppendLine(@"(                              ");
                objSbInsert.AppendLine(@"             IdInformacao      ");
                objSbInsert.AppendLine(@"          ,  Descricao         ");
                objSbInsert.AppendLine(@"          ,  NomeCampo         ");
                objSbInsert.AppendLine(@"          ,  IdAlunoDocumentoTipo         ");
                objSbInsert.AppendLine(@"          ,  Ativo             ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdInformacao     ");
                objSbInsert.AppendLine(@"          ,  @Descricao        ");
                objSbInsert.AppendLine(@"          ,  @NomeCampo        ");
                objSbInsert.AppendLine(@"          ,  @IdAlunoDocumentoTipo         ");
                objSbInsert.AppendLine(@"          ,  @Ativo            ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdInformacao", SqlDbType.Int).Value = IdInformacao;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("NomeCampo", SqlDbType.VarChar).Value = objVO.NomeCampo;
                GetSqlCommand().Parameters.Add("IdAlunoDocumentoTipo", SqlDbType.Int).Value = 1;
                GetSqlCommand().Parameters.Add("Ativo", SqlDbType.VarChar).Value = objVO.Ativo;
                GetSqlCommand().ExecuteNonQuery();
                return IdInformacao;

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
        // MÉTODO RESPONSÁVEL POR ALTERAR UM(A) [ Informacao ] SELECIONADO            
        //                                                                                                 
        public long Alterar(VO.InformacaoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.Informacao ");
                objSbUpdate.AppendLine("   SET Descricao = @Descricao ");
                objSbUpdate.AppendLine("     , NomeCampo = @NomeCampo");
                objSbUpdate.AppendLine("     , IdAlunoDocumentoTipo = @IdAlunoDocumentoTipo");
                objSbUpdate.AppendLine("     , Ativo = @Ativo");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdInformacao = @IdInformacao");
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
                    GetSqlCommand().Parameters.Add("IdInformacao", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("NomeCampo", SqlDbType.VarChar).Value = objVO.NomeCampo;
                    GetSqlCommand().Parameters.Add("IdAlunoDocumentoTipo", SqlDbType.Int).Value = 1;
                    GetSqlCommand().Parameters.Add("Ativo", SqlDbType.VarChar).Value = objVO.Ativo;
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
        // MÉTODO RESPONSÁVEL POR DELETAR UM(A) [ Informacao ] SELECIONADO      
        //                                                                                              
        public void Deletar(VO.InformacaoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Informacao ");
                objSbDelete.AppendLine(" WHERE IdInformacao = @IdInformacao");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdInformacao", SqlDbType.Int).Value = objVO.Id;

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
        // MÉTODO RESPONSÁVEL POR LISTAR [ Informacao ]                                   
        //                                                                                                        
        public List<VO.InformacaoVO> Listar(VO.InformacaoVO objVO)
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
        // MÉTODO RESPONSÁVEL POR SELECIONAR [ Informacao ]                                
        //                                                                                                         
        public List<VO.InformacaoVO> Selecionar(string sql)
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
        public VO.InformacaoVO Consultar(VO.InformacaoVO objVO)
        {
            try
            {
                List<VO.InformacaoVO> lstInformacaoVO = Selecionar(objVO);
                return lstInformacaoVO.Count > 0 ? (VO.InformacaoVO)lstInformacaoVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR CAPTURAR A LISTA [ Informacao ]                          
        //                                                                                                         
        public List<VO.InformacaoVO> GetLista()
        {
            VO.InformacaoVO informacao = null;
            List<VO.InformacaoVO> lstInformacaoVO = null;
            try
            {
                lstInformacaoVO = new List<VO.InformacaoVO>();
                while (GetSqlDataReader().Read())
                {
                    informacao = new VO.InformacaoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdInformacao"))))
                        informacao.Id = Convert.ToInt64(GetSqlDataReader()["IdInformacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        informacao.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampo"))))
                        informacao.NomeCampo = Convert.ToString(GetSqlDataReader()["NomeCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativo"))))
                        informacao.Ativo = Convert.ToBoolean(GetSqlDataReader()["Ativo"]);

                    lstInformacaoVO.Add(informacao);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstInformacaoVO;
        }
        //                                                                                                         
        // MÉTODO RESPONSÁVEL POR SELECIONAR [ Informacao ]                                
        //                                                                                                         
        public List<VO.InformacaoVO> Selecionar(VO.InformacaoVO objVO, int top = 0)
        {
            VO.InformacaoVO informacao = null;
            List<VO.InformacaoVO> lstInformacao = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstInformacao = new List<VO.InformacaoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@" SELECT IdInformacao
                                                ,Descricao
                                                ,NomeCampo
                                                ,IdAlunoDocumentoTipo
                                                ,GrupoInformacao
                                                ,Ativo
                                            FROM DBAthon.dbo.Informacao
                                            WHERE 1 = 1 ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Informacao.IdInformacao = @IdInformacao");
                        GetSqlCommand().Parameters.Add("IdInformacao", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Informacao.Descricao = @IdDescricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }
                    
                    if (!string.IsNullOrEmpty(objVO.NomeCampo))
                    {
                        objSbSelect.AppendLine(@" AND Informacao.NomeCampo = @NomeCampo");
                        GetSqlCommand().Parameters.Add("NomeCampo", SqlDbType.VarChar).Value = objVO.NomeCampo;
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
    }
}
