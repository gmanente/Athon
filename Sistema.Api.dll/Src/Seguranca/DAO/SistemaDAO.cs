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
    public class SistemaDAO : AbstractDAO, IDAO<SistemaVO>
    {
        public SistemaDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(SistemaVO sistemaVo)
        {
            throw new NotImplementedException();
        }

        public long Alterar(SistemaVO sistemaVo, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(SistemaVO sistemaVo)
        {
            throw new NotImplementedException();
        }

        public List<SistemaVO> Selecionar(SistemaVO sistemaVo = null, int top = 0)
        {
            SistemaVO sistema = null;
            List<SistemaVO> lstSistema = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstSistema = new List<SistemaVO>();
                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }


                objSbSelect.AppendLine(@"SELECT                        ").Append(varTop);
                objSbSelect.AppendLine(@"       IdSistema              ");
                objSbSelect.AppendLine(@"     , DataCadastro           ");
                objSbSelect.AppendLine(@"     , Nome                   ");
                objSbSelect.AppendLine(@"     , LogoEmpresa            ");
                objSbSelect.AppendLine(@" FROM DBAthon.dbo.Sistema ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                   ");

                if (sistemaVo != null)
                {

                    GetSqlCommand().Parameters.Clear();
                    if (sistemaVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdSistema = @IdSistema");
                        GetSqlCommand().Parameters.Add("IdSistema", SqlDbType.Int).Value = sistemaVo.Id;
                    }
                    if (sistemaVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = sistemaVo.DataCadastro;
                    }
                    if (!string.IsNullOrEmpty(sistemaVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = sistemaVo.Nome;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                while (GetSqlDataReader().Read())
                {
                    sistema = new SistemaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        sistema.Id = Convert.ToInt32(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        sistema.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        sistema.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("LogoEmpresa"))))
                        sistema.LogoEmpresa = (byte[])GetSqlDataReader()["LogoEmpresa"];

                    lstSistema.Add(sistema);

                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();

            }
            return lstSistema;
        }

        public SistemaVO Consultar(SistemaVO sistemaVo)
        {
            try
            {
                List<SistemaVO> lstSistema = Selecionar(sistemaVo);
                return lstSistema.Count() > 0 ? (SistemaVO)lstSistema.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SistemaVO> Listar(SistemaVO sistemaVo)
        {
            try
            {

                return Selecionar(sistemaVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public List<SistemaVO> Paginar(SistemaVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}