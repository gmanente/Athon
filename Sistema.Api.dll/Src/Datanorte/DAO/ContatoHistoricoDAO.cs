using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.DAO
{
    public class ContatoHistoricoDAO : AbstractDAO, IDAO<ContatoHistoricoVO>
    {
        public ContatoHistoricoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(ContatoHistoricoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.ContatoHistorico                              ");
                objSbUpdate.AppendLine("   SET DBAthon.dbo.ContatoHistorico.Contatos_Id  = @Contatos_Id  ");
                objSbUpdate.AppendLine("      ,DBAthon.dbo.ContatoHistorico.Idusuario    = @Idusuario    ");
                objSbUpdate.AppendLine("      ,DBAthon.dbo.ContatoHistorico.DataOperacao = @DataOperacao ");
                objSbUpdate.AppendLine("      ,DBAthon.dbo.ContatoHistorico.Observacao   = @Observacao   ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE DBAthon.dbo.ContatoHistorico.IdContatoHistorico = @IdContatoHistorico");
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
                    GetSqlCommand().Parameters.Add("IdContatoHistorico", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Contatos_Id", SqlDbType.Int).Value = objVO.Contatos_id;
                    GetSqlCommand().Parameters.Add("Idusuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                    GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.DateTime).Value = objVO.Dataoperacao;
                    GetSqlCommand().Parameters.Add("Observacao", SqlDbType.VarChar).Value = objVO.Observacao;
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

        public ContatoHistoricoVO Consultar(ContatoHistoricoVO objVO)
        {
            try
            {
                List<ContatoHistoricoVO> lst = Selecionar(objVO);

                return lst.Count > 0 ? (ContatoHistoricoVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(ContatoHistoricoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.ContatoHistorico WHERE IdContatoHistorico = @IdContatoHistorico ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdContatoHistorico", SqlDbType.BigInt).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }

        public long Inserir(ContatoHistoricoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqContatoHistorico");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.ContatoHistorico  ");
                objSbInsert.AppendLine(@"(            IdContatoHistorico           ");
                objSbInsert.AppendLine(@"            ,Contatos_Id                  ");
                objSbInsert.AppendLine(@"            ,DataOperacao                 ");
                objSbInsert.AppendLine(@"            ,IdUsuario                    ");
                objSbInsert.AppendLine(@"            ,Observacao                   ");
                objSbInsert.AppendLine(@")                                         ");
                objSbInsert.AppendLine(@"     VALUES                               ");
                objSbInsert.AppendLine(@"(                                         ");
                objSbInsert.AppendLine(@"             @IdContatoHistorico          ");
                objSbInsert.AppendLine(@"           , @Contatos_Id                 ");
                objSbInsert.AppendLine(@"           , @DataOperacao                ");
                objSbInsert.AppendLine(@"           , @IdUsuario                   ");
                objSbInsert.AppendLine(@"           , @Observacao                  ");
                objSbInsert.AppendLine(@")                                         ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();

                GetSqlCommand().Parameters.Add("IdContatoHistorico", SqlDbType.BigInt).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Contatos_Id", SqlDbType.Int).Value = objVO.Contatos_id;
                GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.DateTime).Value = DateTime.Now;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.BigInt).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("Observacao", SqlDbType.VarChar).Value = objVO.Observacao;

                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }

        public List<ContatoHistoricoVO> Listar(ContatoHistoricoVO objVO)
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

        public List<ContatoHistoricoVO> Selecionar(ContatoHistoricoVO objVO, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                            SELECT   DBAthon.dbo.ContatoHistorico.IdContatoHistorico
		                            ,DBAthon.dbo.ContatoHistorico.Contatos_Id
		                            ,DBAthon.dbo.ContatoHistorico.IdUsuario
                                    ,DBAthon.dbo.ContatoHistorico.DataOperacao
		                            ,DBAthon.dbo.ContatoHistorico.Observacao
		                            ,DBAthon.dbo.Usuario.Nome AS UsuarioNome
		                            ,DBAthon.dbo.UvwConsultarContato.Cpf
		                            ,DBAthon.dbo.UvwConsultarContato.Nome AS NomeContato


	                            FROM DBAthon.dbo.ContatoHistorico

                        INNER JOIN DBAthon.dbo.UvwConsultarContato
                                ON DBAthon.dbo.UvwConsultarContato.Contatos_Id = DBAthon.dbo.ContatoHistorico.Contatos_Id

                        INNER JOIN DBAthon.dbo.Usuario 
                                ON DBAthon.dbo.Usuario.IdUsuario = DBAthon.dbo.ContatoHistorico.IdUsuario

                             WHERE 1 = 1 ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.ContatoHistorico.IdContatoHistorico = @IdContatoHistorico");
                        GetSqlCommand().Parameters.Add("IdContatoHistorico", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.ContatoHistorico.IdUsuario = @IdUsuario");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                    }

                    if (objVO.Contatos_id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.ContatoHistorico.Contatos_Id = @Contatos_Id");
                        GetSqlCommand().Parameters.Add("Contatos_Id", SqlDbType.Int).Value = objVO.Contatos_id;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY DBAthon.dbo.ContatoHistorico.DataOperacao DESC "); 

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                List<ContatoHistoricoVO> lst = new List<ContatoHistoricoVO>();

                while (GetSqlDataReader().Read())
                {
                    ContatoHistoricoVO contatoHistoricoVO = new ContatoHistoricoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdContatoHistorico"))))
                            contatoHistoricoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdContatoHistorico"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Contatos_Id"))))
                            contatoHistoricoVO.Contatos_id = Convert.ToInt32(GetSqlDataReader()["Contatos_Id"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataOperacao"))))
                            contatoHistoricoVO.Dataoperacao = Convert.ToDateTime(GetSqlDataReader()["DataOperacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Observacao"))))
                            contatoHistoricoVO.Observacao = Convert.ToString(GetSqlDataReader()["Observacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                            contatoHistoricoVO.Contato.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeContato"))))
                            contatoHistoricoVO.Contato.Nome = Convert.ToString(GetSqlDataReader()["NomeContato"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                            contatoHistoricoVO.Usuario.Id = Convert.ToInt64(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UsuarioNome"))))
                            contatoHistoricoVO.Usuario.Nome = Convert.ToString(GetSqlDataReader()["UsuarioNome"]);

                        lst.Add(contatoHistoricoVO);
                }

                return lst;
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
    }
}
