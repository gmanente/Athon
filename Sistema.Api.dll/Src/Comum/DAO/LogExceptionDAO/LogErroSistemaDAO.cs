using Sistema.Api.dll.Src.Comum.VO.LogErroSistemaVO;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO.LogExceptionDAO
{
    public class LogErroSistemaDAO : AbstractDAO
    {

        public LogErroSistemaDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public void InserirLogErro(LogErroSistemaVO logErroSistema)
        {
            StringBuilder objSb = null;
            try
            {
                objSb = new StringBuilder();

                objSb.AppendLine(@"INSERT INTO LogErroSistema              ");
                objSb.AppendLine(@"(                                       ");
                objSb.AppendLine(@"    Gravidade                           ");
                objSb.AppendLine(@"   ,IdLotacao                           ");
                objSb.AppendLine(@"   ,IpMaquina                           ");
                objSb.AppendLine(@"   ,NomeClasse                          ");
                objSb.AppendLine(@"   ,NomeMetodo                          ");
                objSb.AppendLine(@"   ,Linha                               ");
                objSb.AppendLine(@"   ,CaminhoArquivo                      ");
                objSb.AppendLine(@"   ,Status                              ");
                objSb.AppendLine(@"   ,Mensagem                            ");
                objSb.AppendLine(@"   ,DataHoraCadastro                    ");
                objSb.AppendLine(@")                                       ");
                objSb.AppendLine(@"VALUES                                  ");
                objSb.AppendLine(@"(                                       ");
                objSb.AppendLine(@"    @Gravidade                          ");
                objSb.AppendLine(@"   ,@IdLotacao                          ");
                objSb.AppendLine(@"   ,@IpMaquina                          ");
                objSb.AppendLine(@"   ,@NomeClasse                         ");
                objSb.AppendLine(@"   ,@NomeMetodo                         ");
                objSb.AppendLine(@"   ,@Linha                              ");
                objSb.AppendLine(@"   ,@CaminhoArquivo                     ");
                objSb.AppendLine(@"   ,@Status                             ");
                objSb.AppendLine(@"   ,@Mensagem                           ");
                objSb.AppendLine(@"   ,@DataHoraCadastro                   ");
                objSb.AppendLine(@")                                       ");



                GetSqlCommand().CommandText = objSb.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Gravidade", SqlDbType.Int).Value = logErroSistema.Gravidade;
                GetSqlCommand().Parameters.Add("IdLotacao", SqlDbType.Int).Value = logErroSistema.IdLotacao;
                GetSqlCommand().Parameters.Add("IpMaquina", SqlDbType.VarChar).Value = logErroSistema.IpMaquina;
                GetSqlCommand().Parameters.Add("NomeClasse", SqlDbType.VarChar).Value = logErroSistema.NomeClasse;
                GetSqlCommand().Parameters.Add("NomeMetodo", SqlDbType.VarChar).Value = logErroSistema.NomeMetodo;
                GetSqlCommand().Parameters.Add("Linha", SqlDbType.VarChar).Value = logErroSistema.Linha;
                GetSqlCommand().Parameters.Add("CaminhoArquivo", SqlDbType.VarChar).Value = logErroSistema.CaminhoArquivo;
                GetSqlCommand().Parameters.Add("Status", SqlDbType.Int).Value = logErroSistema.Status;
                GetSqlCommand().Parameters.Add("Mensagem", SqlDbType.VarChar).Value = logErroSistema.Menssagem;
                GetSqlCommand().Parameters.Add("DataHoraCadastro", SqlDbType.DateTime).Value = logErroSistema.DataHoraCadastro;


            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (objSb != null)
                {
                    objSb = null;
                }
            }

        }
    }
}