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
    public class FuncionalidadeDAO : AbstractDAO, IDAO<FuncionalidadeVO>
    {
        public FuncionalidadeDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(FuncionalidadeVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idFuncionalidade = GetCodigoSequece("DBAthon.dbo.SeqFuncionalidade");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Funcionalidade");
                objSbInsert.AppendLine(@"(                          ");
                objSbInsert.AppendLine(@"   IdFuncionalidade        ");
                objSbInsert.AppendLine(@"  ,IdSubModulo             ");
                objSbInsert.AppendLine(@"  ,DataCadastro            ");
                objSbInsert.AppendLine(@"  ,Nome                    ");
                objSbInsert.AppendLine(@"  ,RequisitoFuncional      ");
                objSbInsert.AppendLine(@"  ,DescricaoFuncional      ");
                objSbInsert.AppendLine(@")                          ");
                objSbInsert.AppendLine(@"VALUES                     ");
                objSbInsert.AppendLine(@"(     @IdFuncionalidade    ");
                objSbInsert.AppendLine(@"     ,@IdSubModulo         ");
                objSbInsert.AppendLine(@"     ,@DataCadastro        ");
                objSbInsert.AppendLine(@"     ,@Nome                ");
                objSbInsert.AppendLine(@"     ,@RequisitoFuncional  ");
                objSbInsert.AppendLine(@"     ,@DescricaoFuncional  ");
                objSbInsert.AppendLine(@")                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = idFuncionalidade;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("RequisitoFuncional", SqlDbType.VarChar).Value = objVO.RequisitoFuncional;
                GetSqlCommand().Parameters.Add("DescricaoFuncional", SqlDbType.VarChar).Value = objVO.DescricaoFuncional;

                GetSqlCommand().ExecuteNonQuery();

                return idFuncionalidade;
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

        public long Alterar(FuncionalidadeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Funcionalidade  ");
                objSbUpdate.AppendLine(@"  SET                                  ");
                objSbUpdate.AppendLine(@"      Nome  = @Nome                    ");
                objSbUpdate.AppendLine(@"     ,RequisitoFuncional = @RequisitoFuncional      ");
                objSbUpdate.AppendLine(@"     ,DescricaoFuncional  = @DescricaoFuncional     ");
                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdFuncionalidade = @IdFuncionalidade                ");
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
                    GetSqlCommand().Parameters.Add("RequisitoFuncional", SqlDbType.VarChar).Value = objVO.RequisitoFuncional;
                    GetSqlCommand().Parameters.Add("DescricaoFuncional", SqlDbType.VarChar).Value = objVO.DescricaoFuncional;

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(FuncionalidadeVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM                                 ");
                objSbDelete.AppendLine(@" DBAthon.dbo.Funcionalidade              ");
                objSbDelete.AppendLine(@" WHERE IdFuncionalidade =  @IdFuncionalidade ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Id;
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

        public List<FuncionalidadeVO> Selecionar(FuncionalidadeVO funcionalidadeVo = null, int top = 0)
        {
            FuncionalidadeVO funcionalidade = null;
            List<FuncionalidadeVO> lstFuncionalidade = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstFuncionalidade = new List<FuncionalidadeVO>();
                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                            ").Append(varTop);
                objSbSelect.AppendLine(@"        IdFuncionalidade                          ");
                objSbSelect.AppendLine(@"      , IdSubModulo                               ");
                objSbSelect.AppendLine(@"      , DataCadastro                              ");
                objSbSelect.AppendLine(@"      , Nome                                      ");
                objSbSelect.AppendLine(@"      , RequisitoFuncional                        ");
                objSbSelect.AppendLine(@"      , DescricaoFuncional                        ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Funcionalidade WITH(NOLOCK)");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                       ");

                if (funcionalidadeVo != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (funcionalidadeVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdFuncionalidade = @IdFuncionalidade");
                        GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = funcionalidadeVo.Id;
                    }
                    if (funcionalidadeVo.SubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdSubModulo = @IdSubModulo");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = funcionalidadeVo.SubModulo.Id;
                    }
                    if (funcionalidadeVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = funcionalidadeVo.DataCadastro;
                    }
                    if (!string.IsNullOrEmpty(funcionalidadeVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = funcionalidadeVo.Nome;
                    }
                    if (!string.IsNullOrEmpty(funcionalidadeVo.RequisitoFuncional))
                    {
                        objSbSelect.AppendLine(@" AND RequisitoFuncional = @RequisitoFuncional");
                        GetSqlCommand().Parameters.Add("RequisitoFuncional", SqlDbType.VarChar).Value = funcionalidadeVo.RequisitoFuncional;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                while (GetSqlDataReader().Read())
                {
                    funcionalidade = new FuncionalidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        funcionalidade.Id = Convert.ToInt32(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        funcionalidade.SubModulo.Id = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        funcionalidade.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        funcionalidade.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("RequisitoFuncional"))))
                        funcionalidade.RequisitoFuncional = Convert.ToString(GetSqlDataReader()["RequisitoFuncional"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DescricaoFuncional"))))
                        funcionalidade.DescricaoFuncional = Convert.ToString(GetSqlDataReader()["DescricaoFuncional"]);

                    lstFuncionalidade.Add(funcionalidade);

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
            return lstFuncionalidade;
        }

        public FuncionalidadeVO Consultar(FuncionalidadeVO funcionalidadeVo)
        {
            try
            {
                List<FuncionalidadeVO> lstFuncionalidade = Selecionar(funcionalidadeVo);
                return lstFuncionalidade.Count() > 0 ? (FuncionalidadeVO)lstFuncionalidade.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionalidadeVO> Listar(FuncionalidadeVO funcionalidadeVo)
        {
            try
            {
                List<FuncionalidadeVO> lstFuncionalidade = Selecionar(funcionalidadeVo);
                return lstFuncionalidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionalidadeVO> Paginar(FuncionalidadeVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, List<FuncionalidadeVO>> Paginar(FuncionalidadeVO objVO)
        {
            Dictionary<int, List<FuncionalidadeVO>> dictionany = null;
            try
            {
                List<FuncionalidadeVO> lstFuncionalidade;
                dictionany = new Dictionary<int, List<FuncionalidadeVO>>();
                var sbPaginar = new StringBuilder();
                lstFuncionalidade = Selecionar(objVO);
                dictionany.Add(lstFuncionalidade.Count(), lstFuncionalidade);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}