using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class EmpresaDAO : AbstractDAO, IDAO<EmpresaVO>
    {


        public EmpresaDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(EmpresaVO EmpresaVo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(EmpresaVO EmpresaVo)
        {
            throw new NotImplementedException();
        }

        public long Inserir(EmpresaVO EmpresaVo)
        {
            throw new NotImplementedException();
        }


        public List<EmpresaVO> Selecionar(EmpresaVO EmpresaVo = null, int top = 0)
        {
            EmpresaVO empresa = null;
            List<EmpresaVO> lstEmpresa = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstEmpresa = new List<EmpresaVO>();
                string varTop = "";

                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                     ").Append(varTop);
                objSbSelect.AppendLine(@"        IdEmpresa          ");
                objSbSelect.AppendLine(@"       ,Nome               ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Empresa ");
                objSbSelect.AppendLine(@" WHERE 1 = 1               ");

                if (EmpresaVo != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (EmpresaVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdEmpresa = @IdEmpresa");
                        GetSqlCommand().Parameters.Add("IdEmpresa", SqlDbType.Int).Value = EmpresaVo.Id;
                    }

                    if (!string.IsNullOrEmpty(EmpresaVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.Int).Value = EmpresaVo.Nome;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    empresa = new EmpresaVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEmpresa"))))
                        empresa.Id = Convert.ToInt32(GetSqlDataReader()["IdEmpresa"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        empresa.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstEmpresa.Add(empresa);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }

            return lstEmpresa;
        }


        public EmpresaVO Consultar(EmpresaVO EmpresaVo)
        {
            try
            {
                List<EmpresaVO> lstEmpresa = Selecionar(EmpresaVo);
                return lstEmpresa.Count() > 0 ? (EmpresaVO)lstEmpresa.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EmpresaVO> Listar(EmpresaVO EmpresaVo)
        {
            try
            {

                return Selecionar(EmpresaVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public long Alterar(EmpresaVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }



        public List<EmpresaVO> Paginar(EmpresaVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}
