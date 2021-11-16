using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class TipoDocumentoFotoDAO : AbstractDAO, IDAO<TipoDocumentoFotoVO>
    {
        public TipoDocumentoFotoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(TipoDocumentoFotoVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(TipoDocumentoFotoVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(TipoDocumentoFotoVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<TipoDocumentoFotoVO> Selecionar(TipoDocumentoFotoVO objVO, int top = 0)
        {
            TipoDocumentoFotoVO tipoDocumentoFoto = null;
            List<TipoDocumentoFotoVO> lstTipoDocumentoFoto = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstTipoDocumentoFoto = new List<TipoDocumentoFotoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                     ").Append(varTop);
                objSbSelect.AppendLine(@"        IdTipoDocumentoFoto                ");
                objSbSelect.AppendLine(@"      , Descricao                          ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.TipoDocumentoFoto       ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdTipoDocumentoFoto = @IdTipoDocumentoFoto");
                        GetSqlCommand().Parameters.Add("IdTipoDocumentoFoto", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Descricao = @Descricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }


                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    tipoDocumentoFoto = new TipoDocumentoFotoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdTipoDocumentoFoto"))))
                        tipoDocumentoFoto.Id = Convert.ToInt32(GetSqlDataReader()["IdTipoDocumentoFoto"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        tipoDocumentoFoto.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstTipoDocumentoFoto.Add(tipoDocumentoFoto);

                }

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
            return lstTipoDocumentoFoto;
        }

        public TipoDocumentoFotoVO Consultar(TipoDocumentoFotoVO objVO)
        {
            try
            {
                return (TipoDocumentoFotoVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TipoDocumentoFotoVO> Listar(TipoDocumentoFotoVO objVO)
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

        public List<TipoDocumentoFotoVO> Paginar(TipoDocumentoFotoVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}