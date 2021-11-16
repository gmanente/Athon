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
    public class SubModuloDAO : AbstractDAO, IDAO<SubmoduloVO>
    {
        public SubModuloDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Alterar(SubmoduloVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.SubModulo ");
                objSbUpdate.AppendLine(@"  SET                ");
                objSbUpdate.AppendLine(@"      Nome  = @Nome  ");
                objSbUpdate.AppendLine(@"     ,Icone = @Icone ");
                objSbUpdate.AppendLine(@"     ,Link  = @Link  ");
                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdSubModulo = @IdSubModulo                ");
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
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("Icone", SqlDbType.VarChar).Value = objVO.Icone;
                    GetSqlCommand().Parameters.Add("Link", SqlDbType.VarChar).Value = objVO.Link;

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.Id;
                    }

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

        public void Deletar(SubmoduloVO objVO)
        {
           // throw new NotImplementedException();
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                                        ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.SubModulo WHERE                 ");
                objSbDelete.AppendLine(@"      IdSubModulo =  @IdSubModulo                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
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

        public long Inserir(SubmoduloVO objVO)
        {
            //throw new NotImplementedException();
            try
            {
                objSbInsert = new StringBuilder();
                long idSubModulo = GetCodigoSequece("DBAthon.dbo.SeqSubModulo");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.SubModulo");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"   IdSubModulo     ");
                objSbInsert.AppendLine(@"  ,IdModulo        ");
                objSbInsert.AppendLine(@"  ,DataCadastro    ");
                objSbInsert.AppendLine(@"  ,Nome            ");
                objSbInsert.AppendLine(@"  ,Icone           ");
                objSbInsert.AppendLine(@"  ,Link            ");
                objSbInsert.AppendLine(@")                          ");
                objSbInsert.AppendLine(@"     VALUES                ");
                objSbInsert.AppendLine(@"(            @IdSubModulo  ");
                objSbInsert.AppendLine(@"           , @IdModulo     ");
                objSbInsert.AppendLine(@"           , @DataCadastro ");
                objSbInsert.AppendLine(@"           , @Nome         ");
                objSbInsert.AppendLine(@"           , @Icone        ");
                objSbInsert.AppendLine(@"           , @Link         ");
                objSbInsert.AppendLine(@")                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = idSubModulo;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Icone", SqlDbType.VarChar).Value = objVO.Icone;
                GetSqlCommand().Parameters.Add("Link", SqlDbType.VarChar).Value = objVO.Link;

                GetSqlCommand().ExecuteNonQuery();

                return idSubModulo;
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

        public List<SubmoduloVO> Selecionar(SubmoduloVO subModuloVo = null, int top = 0)
        {
            SubmoduloVO subModulo = null;
            List<SubmoduloVO> lstSubModulo = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstSubModulo = new List<SubmoduloVO>();
                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"        IdSubModulo              ");
                objSbSelect.AppendLine(@"      , IdModulo                 ");
                objSbSelect.AppendLine(@"      , DataCadastro             ");
                objSbSelect.AppendLine(@"      , Nome                     ");
                objSbSelect.AppendLine(@"      , Icone                    ");
                objSbSelect.AppendLine(@"      , Link                     ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.SubModulo ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                      ");

                if (subModuloVo != null)
                {

                    GetSqlCommand().Parameters.Clear();
                    if (subModuloVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdSubModulo = @IdSubModulo");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = subModuloVo.Id;
                    }
                    if (subModuloVo.Modulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdModulo = @IdModulo");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = subModuloVo.Modulo.Id;
                    }
                    if (subModuloVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = subModuloVo.DataCadastro;
                    }
                    if (!string.IsNullOrEmpty(subModuloVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = subModuloVo.Nome;
                    }

                }

                objSbSelect.AppendLine(" ORDER BY Nome ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                while (GetSqlDataReader().Read())
                {
                    subModulo = new SubmoduloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        subModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        subModulo.Modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        subModulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        subModulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        subModulo.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        subModulo.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    lstSubModulo.Add(subModulo);

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
            return lstSubModulo;
        }

        public SubmoduloVO Consultar(SubmoduloVO SubModuloVO)
        {
            try
            {
                List<SubmoduloVO> lstSubModulo = Selecionar(SubModuloVO);
                return lstSubModulo.Count() > 0 ? (SubmoduloVO)lstSubModulo.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<SubmoduloVO> Listar(SubmoduloVO SubModuloVO)
        {
            try
            {
                List<SubmoduloVO> lstSubModulo = Selecionar(SubModuloVO);
                return lstSubModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<SubmoduloVO> Paginar(SubmoduloVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, List<SubmoduloVO>> Paginar(SubmoduloVO objVO)
        {
            Dictionary<int, List<SubmoduloVO>> dictionany = null;
            try
            {
                List<SubmoduloVO> lstSubModulo;
                dictionany = new Dictionary<int, List<SubmoduloVO>>();
                var sbPaginar = new StringBuilder();
                lstSubModulo = Selecionar(objVO);
                dictionany.Add(lstSubModulo.Count(), lstSubModulo);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
