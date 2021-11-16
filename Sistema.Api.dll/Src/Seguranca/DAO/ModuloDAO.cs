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
    public class ModuloDAO : AbstractDAO, IDAO<ModuloVO>
    {
        public ModuloDAO(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(ModuloVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idModulo = GetCodigoSequece("DBAthon.dbo.SeqModulo");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Modulo");
                objSbInsert.AppendLine(@"(                          ");
                objSbInsert.AppendLine(@"   IdModulo                ");
                objSbInsert.AppendLine(@"  ,IdSistema               ");
                objSbInsert.AppendLine(@"  ,IdDepartamento          ");
                objSbInsert.AppendLine(@"  ,DataCadastro            ");
                objSbInsert.AppendLine(@"  ,Nome                    ");
                objSbInsert.AppendLine(@"  ,Cor                     ");
                objSbInsert.AppendLine(@"  ,Link                    ");
                objSbInsert.AppendLine(@"  ,LinkDebug               ");
                objSbInsert.AppendLine(@"  ,Icone                   ");
                objSbInsert.AppendLine(@")                          ");
                objSbInsert.AppendLine(@"VALUES                     ");
                objSbInsert.AppendLine(@"(     @IdModulo            ");
                objSbInsert.AppendLine(@"     ,@IdSistema           ");
                objSbInsert.AppendLine(@"     ,@IdDepartamento      ");
                objSbInsert.AppendLine(@"     ,@DataCadastro        ");
                objSbInsert.AppendLine(@"     ,@Nome                ");
                objSbInsert.AppendLine(@"     ,@Cor                 ");
                objSbInsert.AppendLine(@"     ,@Link                ");
                objSbInsert.AppendLine(@"     ,@LinkDebug           ");
                objSbInsert.AppendLine(@"     ,@Icone               ");
                objSbInsert.AppendLine(@")                          ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = idModulo;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = objVO.DataCadastro;
                GetSqlCommand().Parameters.Add("IdSistema", SqlDbType.Int).Value = objVO.Sistema.Id;
                GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Departamento.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Cor", SqlDbType.VarChar).Value = objVO.Cor;
                GetSqlCommand().Parameters.Add("Link", SqlDbType.VarChar).Value = objVO.Link;
                GetSqlCommand().Parameters.Add("LinkDebug", SqlDbType.VarChar).Value = objVO.LinkDebug;

                GetSqlCommand().Parameters.Add("Icone", SqlDbType.VarChar).Value = objVO.Icone;

                GetSqlCommand().ExecuteNonQuery();

                return idModulo;
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

        public long Alterar(ModuloVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Modulo                  ");
                objSbUpdate.AppendLine(@"  SET                                          ");
                objSbUpdate.AppendLine(@"     IdSistema      = @IdSistema               ");
                objSbUpdate.AppendLine(@"    ,IdDepartamento = @IdDepartamento          ");
                objSbUpdate.AppendLine(@"    ,Nome           = @Nome                    ");
                objSbUpdate.AppendLine(@"    ,Cor            = @Cor                     ");
                objSbUpdate.AppendLine(@"    ,Link           = @Link                    ");
                objSbUpdate.AppendLine(@"    ,LinkDebug      = @LinkDebug               ");
             
                if (objVO.Icone != null){
                    objSbUpdate.AppendLine(@"    ,Icone          = @Icone               ");
                }

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdModulo = @IdModulo                ");
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
                    GetSqlCommand().Parameters.Add("IdSistema", SqlDbType.Int).Value = objVO.Sistema.Id;
                    GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = objVO.Departamento.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;

                    GetSqlCommand().Parameters.Add("Cor", SqlDbType.VarChar).Value = objVO.Cor;
                    GetSqlCommand().Parameters.Add("Link", SqlDbType.VarChar).Value = objVO.Link;
                    GetSqlCommand().Parameters.Add("LinkDebug", SqlDbType.VarChar).Value = objVO.LinkDebug;

                    if (objVO.Icone != null){
                        GetSqlCommand().Parameters.Add("Icone", SqlDbType.VarChar).Value = objVO.Icone;
                    }

                    if (where == null)
                    {
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(ModuloVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@" DELETE FROM                   ");
                objSbDelete.AppendLine(@"  DBAthon.dbo.Modulo       ");
                objSbDelete.AppendLine(@" WHERE IdModulo =  @IdModulo   ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Id;
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

        public List<ModuloVO> Selecionar(string sql)
        {
            List<ModuloVO> lstModulo = null;
            try
            {
                objSbSelect = new StringBuilder();
                lstModulo = new List<ModuloVO>();
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                GetSqlCommand().Parameters.Clear();
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

        public List<ModuloVO> Selecionar(ModuloVO moduloVo = null, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();
                
                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }


                objSbSelect.AppendLine(@"SELECT                        ").Append(varTop);
                objSbSelect.AppendLine(@"        IdModulo              ");
                objSbSelect.AppendLine(@"      , IdSistema             ");
                objSbSelect.AppendLine(@"      , IdDepartamento        ");
                objSbSelect.AppendLine(@"      , DataCadastro          ");
                objSbSelect.AppendLine(@"      , Nome                  ");
                objSbSelect.AppendLine(@"      , Icone                 ");
                objSbSelect.AppendLine(@"      , Cor                   ");
                objSbSelect.AppendLine(@"      , Link                  ");
                objSbSelect.AppendLine(@"      , LinkDebug             ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Modulo ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                   ");

                if (moduloVo != null)
                {

                    GetSqlCommand().Parameters.Clear();
                    if (moduloVo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdModulo = @IdModulo");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = moduloVo.Id;
                    }
                    if (moduloVo.Sistema.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdSistema = @IdSistema");
                        GetSqlCommand().Parameters.Add("IdSistema", SqlDbType.Int).Value = moduloVo.Sistema.Id;
                    }
                    if (moduloVo.Departamento.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdDepartamento = @IdDepartamento");
                        GetSqlCommand().Parameters.Add("IdDepartamento", SqlDbType.Int).Value = moduloVo.Departamento.Id;
                    }
                    if (moduloVo.DataCadastro != null)
                    {
                        objSbSelect.AppendLine(@" AND DataCadastro = @DataCadastro");
                        GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = moduloVo.DataCadastro;
                    }
                    if (!string.IsNullOrEmpty(moduloVo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = moduloVo.Nome;
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
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

        private List<ModuloVO> GetLista()
        {
           
            List<ModuloVO> lstModulo = null;
            ModuloVO modulo = null;

            lstModulo = new List<ModuloVO>();
            try
            {
                while (GetSqlDataReader().Read())
                {
                    modulo = new ModuloVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        modulo.Id = Convert.ToInt32(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSistema"))))
                        modulo.Sistema.Id = Convert.ToInt32(GetSqlDataReader()["IdSistema"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdDepartamento"))))
                        modulo.Departamento.Id = Convert.ToInt32(GetSqlDataReader()["IdDepartamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        modulo.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        modulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Icone"))))
                        modulo.Icone = Convert.ToString(GetSqlDataReader()["Icone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cor"))))
                        modulo.Cor = Convert.ToString(GetSqlDataReader()["Cor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Link"))))
                        modulo.Link = Convert.ToString(GetSqlDataReader()["Link"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("LinkDebug"))))
                        modulo.LinkDebug = Convert.ToString(GetSqlDataReader()["LinkDebug"]);

                    lstModulo.Add(modulo);

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstModulo;
        }

        public ModuloVO Consultar(ModuloVO moduloVo)
        {
            try
            {
                List<ModuloVO> lstModulo = Selecionar(moduloVo);
                return lstModulo.Count() > 0 ? (ModuloVO)lstModulo.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ModuloVO> Listar(ModuloVO moduloVo)
        {
            try
            {

                return Selecionar(moduloVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<ModuloVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<ModuloVO>> dictionany = null;
            try
            {
                List<ModuloVO> lstModulo;
                dictionany = new Dictionary<int, List<ModuloVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY Nome");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstModulo = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstModulo);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}