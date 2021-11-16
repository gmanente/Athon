using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class AgendaEmailDAO : AbstractDAO, IDAO<AgendaEmailVO>
    {
        public AgendaEmailDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }
        public long Alterar(AgendaEmailVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(AgendaEmailVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(AgendaEmailVO objVO)
        {
            try
            {

                objSbInsert = new StringBuilder();
                long IdAgendaEmail = GetCodigoSequece("DBAthon.dbo.SeqAgendaEmail");
                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.AgendaEmail                    ");
                objSbInsert.AppendLine(@"(                                                      ");
                objSbInsert.AppendLine(@"             IdAgendaEmail                             ");
                objSbInsert.AppendLine(@"           , Parametros                                ");
                objSbInsert.AppendLine(@"           , DataHoraAgendamento                       ");
                objSbInsert.AppendLine(@"           , Email                                     ");
                objSbInsert.AppendLine(@")                                                      ");
                objSbInsert.AppendLine(@"     VALUES                                            ");
                objSbInsert.AppendLine(@"(                                                      ");
                objSbInsert.AppendLine(@"             @IdAgendaEmail                            ");
                objSbInsert.AppendLine(@"           , @Parametros                               ");
                objSbInsert.AppendLine(@"           , @DataHoraAgendamento                      ");
                objSbInsert.AppendLine(@"           , @Email                                     ");
                objSbInsert.AppendLine(@")                                                      ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAgendaEmail", SqlDbType.Int).Value = IdAgendaEmail;
                GetSqlCommand().Parameters.Add("Parametros", SqlDbType.VarChar).Value = objVO.Parametros;
                GetSqlCommand().Parameters.Add("DataHoraAgendamento", SqlDbType.DateTime).Value = objVO.DataHoraAgendamento;
                GetSqlCommand().Parameters.Add("Email", SqlDbType.VarChar).Value = objVO.Email;
                GetSqlCommand().ExecuteNonQuery();
                return IdAgendaEmail;

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

        public List<AgendaEmailVO> Selecionar(AgendaEmailVO objVO, int top = 0)
        {
            throw new NotImplementedException();
        }

        public AgendaEmailVO Consultar(AgendaEmailVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<AgendaEmailVO> Listar(AgendaEmailVO objVO)
        {
            throw new NotImplementedException();
        }
    }
}
