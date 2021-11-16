using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    class RelatorioLogDAO : AbstractDAO, IDAO<RelatorioLogVO>
    {
        public RelatorioLogDAO(SqlCommand sqlCommand)
            : base(sqlCommand)
        {
        }

        public long Alterar(RelatorioLogVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(RelatorioLogVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(RelatorioLogVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idRelatorioLog = GetCodigoSequece("DBAthon.dbo.SeqRelatorioLog");
                //SetRelatorioLog(objVO);

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.RelatorioLog  ");
                objSbInsert.AppendLine(@"(                                         ");
                objSbInsert.AppendLine(@"             IdRelatorioLog		       ");
                objSbInsert.AppendLine(@"           , IdUsuario			           ");
                objSbInsert.AppendLine(@"           , Login				           ");
                objSbInsert.AppendLine(@"           , Senha				           ");
                objSbInsert.AppendLine(@"           , Spid				           ");
                objSbInsert.AppendLine(@"           , DataOperacao		           ");
                objSbInsert.AppendLine(@"           , IdModulo			           ");
                objSbInsert.AppendLine(@"           , IdSubModulo		           ");
                objSbInsert.AppendLine(@"           , IdFuncionalidade	           ");
                objSbInsert.AppendLine(@"           , EnderecoIp			       ");
                objSbInsert.AppendLine(@"           , BrowserNome		           ");
                objSbInsert.AppendLine(@"           , BrowserTipo		           ");
                objSbInsert.AppendLine(@"           , ServerName			       ");
                objSbInsert.AppendLine(@"           , MACAddress			       ");
                objSbInsert.AppendLine(@"           , UsuarioDominio		       ");
                objSbInsert.AppendLine(@")                                         ");
                objSbInsert.AppendLine(@"     VALUES                               ");
                objSbInsert.AppendLine(@"(                                         ");
                objSbInsert.AppendLine(@"             @IdRelatorioLog	           ");
                objSbInsert.AppendLine(@"           , @IdUsuario			       ");
                objSbInsert.AppendLine(@"           , @Login				       ");
                objSbInsert.AppendLine(@"           , @Senha				       ");
                objSbInsert.AppendLine(@"           , @Spid				           ");
                objSbInsert.AppendLine(@"           , @DataOperacao		           ");
                objSbInsert.AppendLine(@"           , @IdModulo			           ");
                objSbInsert.AppendLine(@"           , @IdSubModulo		           ");
                objSbInsert.AppendLine(@"           , @IdFuncionalidade	           ");
                objSbInsert.AppendLine(@"           , @EnderecoIp		           ");
                objSbInsert.AppendLine(@"           , @BrowserNome		           ");
                objSbInsert.AppendLine(@"           , @BrowserTipo		           ");
                objSbInsert.AppendLine(@"           , @ServerName		           ");
                objSbInsert.AppendLine(@"           , @MACAddress		           ");
                objSbInsert.AppendLine(@"           , @UsuarioDominio              ");
                objSbInsert.AppendLine(@")                                         ");
                
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdRelatorioLog", SqlDbType.Int).Value = idRelatorioLog;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("Login", SqlDbType.VarChar).Value = objVO.Login;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.VarChar).Value = objVO.Senha;
                GetSqlCommand().Parameters.Add("Spid", SqlDbType.Int).Value = objVO.Spid;
                GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.DateTime).Value = objVO.DataOperacao;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
                GetSqlCommand().Parameters.Add("EnderecoIp", SqlDbType.VarChar).Value = objVO.EnderecoIp;
                GetSqlCommand().Parameters.Add("BrowserNome", SqlDbType.VarChar).Value = objVO.BrowserNome;
                GetSqlCommand().Parameters.Add("BrowserTipo", SqlDbType.VarChar).Value = objVO.BrowserTipo;
                GetSqlCommand().Parameters.Add("ServerName", SqlDbType.VarChar).Value = objVO.ServerName;
                GetSqlCommand().Parameters.Add("MACAddress", SqlDbType.VarChar).Value = objVO.MACAddress;
                GetSqlCommand().Parameters.Add("UsuarioDominio", SqlDbType.VarChar).Value = objVO.UsuarioDominio;

                GetSqlCommand().ExecuteNonQuery();

                return idRelatorioLog;

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

        //private void SetRelatorioLog(RelatorioLogVO objVO)
        //{

        //    try
        //    {
        //        objSbSelect = new StringBuilder();

        //        objSbSelect.AppendLine(@"SELECT                                  ");
        //        objSbSelect.AppendLine(@"       SPID                             ");
        //        objSbSelect.AppendLine(@"     , GETDATE() AS DATAHORA            ");
        //        objSbSelect.AppendLine(@"     , HOSTPROCESS                      ");
        //        objSbSelect.AppendLine(@"     , HOSTNAME                          ");
        //        objSbSelect.AppendLine(@"  FROM MASTER..SYSPROCESSES             ");
        //        objSbSelect.AppendLine(@" WHERE SPID = @@SPID                    ");



        //        GetSqlCommand().CommandText = "";
        //        GetSqlCommand().CommandText = objSbSelect.ToString();

        //        while (GetSqlDataReader().Read())
        //        {

        //            if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SPID"))))
        //                objVO.Spid = Convert.ToString(GetSqlDataReader()["SPID"]);

        //            if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DATAHORA"))))
        //                objVO.DataWho = Convert.ToDateTime(GetSqlDataReader()["DATAHORA"]);

        //            if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HOSTPROCESS"))))
        //                objVO.HostProcesso = Convert.ToString(GetSqlDataReader()["HOSTPROCESS"]);

        //            if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("HOSTNAME"))))
        //                objVO.HostName = Convert.ToString(GetSqlDataReader()["HOSTNAME"]);


        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        Close();
        //    }
        //}

        public List<RelatorioLogVO> Selecionar(RelatorioLogVO objVO, int top = 0)
        {
            throw new NotImplementedException();
        }

        public RelatorioLogVO Consultar(RelatorioLogVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<RelatorioLogVO> Listar(RelatorioLogVO objVO)
        {
            throw new NotImplementedException();
        }
    }
}
