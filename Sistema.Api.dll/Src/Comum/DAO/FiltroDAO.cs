using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class FiltroDAO : AbstractDAO//, IDAO<ConsultaVO>
    {
        public FiltroDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(FiltroVO objVO)
        {
            try
            {
                objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqFiltro");

                objSbInsert = new StringBuilder();
                objSbInsert.AppendLine(@" INSERT INTO DBAthon.dbo.Filtro
                                        (
                                              IdFiltro
                                            , IdSubModulo
                                            , Nome
                                            , InstrucaoSQL
                                        ) VALUES (
                                            @IdFiltro
                                            , @IdSubModulo
                                            , @Nome
                                            , @InstrucaoSQL
                                        )
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.IdSubModulo;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("InstrucaoSQL", SqlDbType.VarChar).Value = objVO.InstrucaoSQL;

                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }

        public long Alterar(FiltroVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@" UPDATE DBAthon.dbo.Filtro
                                            SET
                                                  IdSubModulo      = @IdSubModulo
                                                , Nome             = @Nome
                ");

                if (!string.IsNullOrEmpty(objVO.InstrucaoSQL))
                    objSbUpdate.AppendLine(", InstrucaoSQL     = @InstrucaoSQL ");

                objSbUpdate.AppendLine(" WHERE IdFiltro = @IdFiltro ");


                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();

                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.IdSubModulo;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;

                    if (!string.IsNullOrEmpty(objVO.InstrucaoSQL))
                        GetSqlCommand().Parameters.Add("InstrucaoSQL", SqlDbType.VarChar).Value = objVO.InstrucaoSQL;

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
                    objSbUpdate = null;
            }
        }

        public void Deletar(FiltroVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Filtro", "IdFiltro", objVO.Id);

                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(" DELETE FROM DBAthon.dbo.Filtro WHERE IdFiltro = @IdFiltro ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }

        public long InserirFiltroCampo(FiltroCampoVO objVO)
        {
            try
            {
                objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqFiltroCampo");

                objSbInsert = new StringBuilder();
                objSbInsert.AppendLine(@" INSERT INTO  DBAthon.dbo.FiltroCampo
                                        (
                                                     IdFiltroCampo
                                                  ,  IdFiltro
                                                  ,  NomeCampo
                                                  ,  DescricaoCampo
                                                  ,  TipoCampo
                                                  ,  TamanhoCampo
                                                  ,  Ativar
                                                  ,  Ordem
                                        ) VALUES (
                                                     @IdFiltroCampo
                                                  ,  @IdFiltro
                                                  ,  @NomeCampo
                                                  ,  @DescricaoCampo
                                                  ,  @TipoCampo
                                                  ,  @TamanhoCampo
                                                  ,  @Ativar
                                                  ,  @Ordem
                                        )
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFiltroCampo", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.IdFiltro;
                GetSqlCommand().Parameters.Add("NomeCampo", SqlDbType.VarChar).Value = objVO.NomeCampo;
                GetSqlCommand().Parameters.Add("DescricaoCampo", SqlDbType.VarChar).Value = objVO.DescricaoCampo;
                GetSqlCommand().Parameters.Add("TipoCampo", SqlDbType.VarChar).Value = objVO.TipoCampo;
                GetSqlCommand().Parameters.Add("TamanhoCampo", SqlDbType.Int).Value = objVO.TamanhoCampo;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("Ordem", SqlDbType.Int).Value = objVO.Ordem;
                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }

        public long AlterarFiltroCampo(FiltroCampoVO objVO)
        {
            try
            {

                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"

                        UPDATE DBAthon.dbo.FiltroCampo SET

                              IdFiltro               = @IdFiltro
                            , NomeCampo              = @NomeCampo
                            , DescricaoCampo         = @DescricaoCampo
                            , TipoCampo              = @TipoCampo
                            , TamanhoCampo           = @TamanhoCampo
                            , Ativar                 = @Ativar
                            , Ordem                  = @Ordem

                        WHERE IdFiltroCampo = @IdFiltroCampo

                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFiltroCampo", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.IdFiltro;
                GetSqlCommand().Parameters.Add("NomeCampo", SqlDbType.VarChar).Value = objVO.NomeCampo;
                GetSqlCommand().Parameters.Add("DescricaoCampo", SqlDbType.VarChar).Value = objVO.DescricaoCampo;
                GetSqlCommand().Parameters.Add("TipoCampo", SqlDbType.VarChar).Value = objVO.TipoCampo;
                GetSqlCommand().Parameters.Add("TamanhoCampo", SqlDbType.Int).Value = objVO.TamanhoCampo;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("Ordem", SqlDbType.Int).Value = objVO.Ordem;

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
                    objSbUpdate = null;
            }
        }

        public void DeletarFiltroCampo(FiltroCampoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.FiltroCampo", "IdFiltroCampo", objVO.Id);

                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(" DELETE FROM DBAthon.dbo.FiltroCampo WHERE IdFiltroCampo = @IdFiltroCampo ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFiltroCampo", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }


        public void AlterarInstrucaoSQL(long idFiltro, string InstrucaoSQL)
        {
            try
            {

                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"

                        UPDATE DBAthon.dbo.Filtro SET

                            InstrucaoSQL     = @InstrucaoSQL

                        WHERE IdFiltro = @IdFiltro

                ");


                if (!string.IsNullOrEmpty(InstrucaoSQL))
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();

                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = idFiltro;
                    GetSqlCommand().Parameters.Add("InstrucaoSQL", SqlDbType.VarChar).Value = InstrucaoSQL;

                    GetSqlCommand().ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                    objSbUpdate = null;
            }
        }

        public long AtualizarSequence()
        {
            long idAtual = 0;

            try
            {
                idAtual = ProximoIdFiltroCampo();
                GetSqlCommand().CommandText = "USE DBAthon ALTER SEQUENCE dbo.SeqFiltroCampo RESTART WITH " + idAtual + " INCREMENT BY 1 ";
                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

            return idAtual;
        }

        private long ProximoIdFiltroCampo()
        {
            long proximoIdFiltroCampo = 0;
            try
            {
                GetSqlCommand().CommandText = "SELECT MAX(IdFiltroCampo) + 1 AS ProximoIdFiltroCampo FROM DBAthon.dbo.FiltroCampo ";
                GetSqlCommand().Parameters.Clear();

                if (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ProximoIdFiltroCampo"))))
                    {
                        proximoIdFiltroCampo = Convert.ToInt64(GetSqlDataReader()["ProximoIdFiltroCampo"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

            return proximoIdFiltroCampo;
        }


        public List<ConsultaVO> Selecionar(ConsultaVO objVO, int top = 0)
        {
            List<ConsultaVO> lstFiltro = null;
            try
            {
                lstFiltro = new List<ConsultaVO>();

                string varTop = "";
                if (top > 0)
                    varTop = top.ToString();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"  SELECT  ").Append(varTop);
                objSbSelect.AppendLine(@"

                          Filtro.IdFiltro
                        , Filtro.IdSubModulo
                        , Filtro.Nome
                        , Filtro.InstrucaoSQL
                        , FiltroCampo.IdFiltroCampo
                        , FiltroCampo.NomeCampo
                        , FiltroCampo.DescricaoCampo
                        , FiltroCampo.TamanhoCampo
                        , FiltroCampo.TipoCampo
                        , FiltroCampo.Ativar

                    FROM DBAthon.dbo.Filtro       WITH (NOLOCK)
                    JOIN DBAthon.dbo.FiltroCampo  WITH (NOLOCK) ON Filtro.IdFiltro = FiltroCampo.IdFiltro

                    WHERE 1 = 1

                ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND Filtro.IdFiltro = @IdFiltro ");
                        GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.IdSubModulo > 0)
                    {
                        objSbSelect.AppendLine(" AND Filtro.IdSubModulo = @IdSubModulo ");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.IdSubModulo;
                    }

                    if (!string.IsNullOrEmpty(objVO.UrlSubModulo))
                    {
                        var appState = Dominio.AppState;
                        objSbSelect.AppendLine(@"

                            AND Filtro.IdSubModulo = (
                                SELECT MAX(SUB_URL.IdSubModulo)
                                    FROM DBAthon.dbo.SubModuloUrl SUB_URL  WITH (NOLOCK)
                                    JOIN DBAthon.dbo.SubModulo  WITH (NOLOCK) ON SubModulo.IdSubModulo = SUB_URL.IdSubModulo
                                    JOIN DBAthon.dbo.Modulo     WITH (NOLOCK) ON Modulo.IdModulo = SubModulo.IdModulo

                        ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Debug, "       WHERE CONCAT(Modulo.LinkDebug,'/',SUB_URL.Url) = @UrlSubModulo ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Teste, "       WHERE CONCAT(Modulo.LinkTeste,'/',SUB_URL.Url) = @UrlSubModulo ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Homologacao, " WHERE CONCAT(Modulo.LinkHomologacao,'/',SUB_URL.Url) = @UrlSubModulo ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Producao, "    WHERE CONCAT(Modulo.Link,'/',SUB_URL.Url)  =  @UrlSubModulo ");
                        objSbSelect.AppendLine(" ) ");

                        GetSqlCommand().Parameters.Add("UrlSubModulo", SqlDbType.VarChar).Value = objVO.UrlSubModulo;
                    }

                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(" AND Nome = @Nome ");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    }
                }

                objSbSelect.AppendLine(" ORDER BY Ordem ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                long ultId = 0;
                ConsultaVO filtro = null;
                while (GetSqlDataReader().Read())
                {
                    ConsultaCampoVO filtroCampo = new ConsultaCampoVO();
                    if (ultId != Convert.ToInt32(GetSqlDataReader()["IdFiltro"]))
                    {
                        filtro = new ConsultaVO();

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltro"))))
                            filtro.Id = Convert.ToInt32(GetSqlDataReader()["IdFiltro"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                            filtro.IdSubModulo = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                            filtro.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("InstrucaoSQL"))))
                            filtro.InstrucaoSQL = Convert.ToString(GetSqlDataReader()["InstrucaoSQL"]);
                    }

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltroCampo"))))
                        filtroCampo.Id = Convert.ToInt32(GetSqlDataReader()["IdFiltroCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampo"))))
                        filtroCampo.NomeCampo = Convert.ToString(GetSqlDataReader()["NomeCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DescricaoCampo"))))
                        filtroCampo.DescricaoCampo = Convert.ToString(GetSqlDataReader()["DescricaoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TamanhoCampo"))))
                        filtroCampo.TamanhoCampo = Convert.ToInt32(GetSqlDataReader()["TamanhoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TipoCampo"))))
                        filtroCampo.TipoCampo = Convert.ToString(GetSqlDataReader()["TipoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        filtroCampo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    filtro.LstFiltroCampos.Add(filtroCampo);
                    if (ultId != Convert.ToInt32(GetSqlDataReader()["IdFiltro"]))
                    {
                        lstFiltro.Add(filtro);
                        ultId = Convert.ToInt32(GetSqlDataReader()["IdFiltro"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }

            return lstFiltro;
        }

        public List<ConsultaVO> Listar(ConsultaVO objVO)
        {
            try
            {
                List<ConsultaVO> lstFiltro = Selecionar(objVO);
                return lstFiltro;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ConsultaVO Consultar(ConsultaVO objVO)
        {
            try
            {
                List<ConsultaVO> lstFiltro = Selecionar(objVO);
                return lstFiltro.Count() > 0 ? (ConsultaVO)lstFiltro.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<FiltroVO> SelecionarFiltro(FiltroVO objVO)
        {
            FiltroVO filtro = null;
            List<FiltroVO> lstFiltro = null;
            try
            {
                lstFiltro = new List<FiltroVO>();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"

                    SELECT
                          Filtro.IdFiltro
                        , Filtro.IdSubModulo
                        , Filtro.Nome
                        , Filtro.InstrucaoSQL
                        , FiltroCampo.IdFiltroCampo
                        , FiltroCampo.NomeCampo
                        , FiltroCampo.DescricaoCampo
                        , FiltroCampo.TamanhoCampo
                        , FiltroCampo.TipoCampo
                        , FiltroCampo.Ativar
                        , FiltroCampo.Ordem
                        , Submodulo.Nome Submodulo

                    FROM DBAthon.dbo.Filtro         WITH (NOLOCK)
               LEFT JOIN DBAthon.dbo.FiltroCampo    WITH (NOLOCK) ON Filtro.IdFiltro = FiltroCampo.IdFiltro
               LEFT JOIN DBAthon.dbo.Submodulo  WITH (NOLOCK) ON Filtro.IdSubmodulo = Submodulo.IdSubmodulo
                    WHERE 1 = 1

                ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND Filtro.IdFiltro = @IdFiltro ");
                        GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.IdSubModulo > 0)
                    {
                        objSbSelect.AppendLine(" AND Filtro.IdSubModulo = @IdSubModulo ");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.IdSubModulo;
                    }

                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        var filter = String.Empty;
                        switch (objVO.FiltroNome)
                        {
                            case 1:  filter = " AND Filtro.Nome LIKE '%{0}%' "; break;
                            case 2:  filter = " AND Filtro.Nome LIKE '{0}%' "; break;
                            case 3:  filter = " AND Filtro.Nome LIKE '%{0}' "; break;
                            default: filter = " AND Filtro.Nome = '{0}' "; break;
                        }

                        objSbSelect.AppendLine(string.Format(filter, objVO.Nome));
                    }

                    if (objVO.NomeSubModulo == "Não Identificado")
                    {
                        objSbSelect.AppendLine(" AND Submodulo.IdSubmodulo IS NULL ");
                    }
                }

                objSbSelect.AppendLine(" ORDER BY Filtro.IdFiltro ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                long ultId = 0;
                while (GetSqlDataReader().Read())
                {
                    FiltroCampoVO filtroCampo = new FiltroCampoVO();
                    if (ultId != Convert.ToInt32(GetSqlDataReader()["IdFiltro"]))
                    {
                        filtro = new FiltroVO();

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltro"))))
                            filtro.Id = Convert.ToInt32(GetSqlDataReader()["IdFiltro"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                            filtro.IdSubModulo = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                            filtro.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Submodulo"))))
                            filtro.NomeSubModulo = Convert.ToString(GetSqlDataReader()["Submodulo"]);
                        else
                            filtro.NomeSubModulo = "Não Identificado";

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("InstrucaoSQL"))))
                            filtro.InstrucaoSQL = Convert.ToString(GetSqlDataReader()["InstrucaoSQL"]);
                    }

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltroCampo"))))
                        filtroCampo.Id = Convert.ToInt32(GetSqlDataReader()["IdFiltroCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampo"))))
                        filtroCampo.NomeCampo = Convert.ToString(GetSqlDataReader()["NomeCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DescricaoCampo"))))
                        filtroCampo.DescricaoCampo = Convert.ToString(GetSqlDataReader()["DescricaoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TamanhoCampo"))))
                        filtroCampo.TamanhoCampo = Convert.ToInt32(GetSqlDataReader()["TamanhoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TipoCampo"))))
                        filtroCampo.TipoCampo = Convert.ToString(GetSqlDataReader()["TipoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ordem"))))
                        filtroCampo.Ordem = Convert.ToInt32(GetSqlDataReader()["Ordem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        filtroCampo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    // SÓ ADICIONAR SE POSSUIR VALOR POIS OS CAMPOS SÃO LEFT JOIN
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltroCampo"))))
                        filtro.LstFiltroCampos.Add(filtroCampo);

                    if (ultId != Convert.ToInt32(GetSqlDataReader()["IdFiltro"]))
                    {
                        lstFiltro.Add(filtro);
                        ultId = Convert.ToInt32(GetSqlDataReader()["IdFiltro"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
            return lstFiltro;
        }

        public FiltroVO ConsultarFiltro(FiltroVO objVO)
        {
            try
            {
                List<FiltroVO> lstFiltro = SelecionarFiltro(objVO);
                return lstFiltro.Count() > 0 ? (FiltroVO)lstFiltro.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<ConsultaVO> SelecionarSubmoduloUrl(ConsultaVO objVO, int top = 0)
        {
            List<ConsultaVO> lstFiltro = null;
            try
            {
                lstFiltro = new List<ConsultaVO>();

                string varTop = "";
                if (top > 0)
                    varTop = top.ToString();

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"  SELECT  ").Append(varTop);
                objSbSelect.AppendLine(@"

                          Filtro.IdFiltro
                        , Filtro.IdSubModulo
                        , Filtro.Nome
                        , Filtro.InstrucaoSQL
                        , FiltroCampo.IdFiltroCampo
                        , FiltroCampo.NomeCampo
                        , FiltroCampo.DescricaoCampo
                        , FiltroCampo.TamanhoCampo
                        , FiltroCampo.TipoCampo
                        , FiltroCampo.Ativar

                    FROM DBAthon.dbo.Filtro       WITH (NOLOCK)
                    JOIN DBAthon.dbo.FiltroCampo  WITH (NOLOCK) ON Filtro.IdFiltro = FiltroCampo.IdFiltro

                    WHERE 1 = 1

                ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND Filtro.IdFiltro = @IdFiltro ");
                        GetSqlCommand().Parameters.Add("IdFiltro", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.IdSubModulo > 0)
                    {
                        objSbSelect.AppendLine(" AND Filtro.IdSubModulo = @IdSubModulo ");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.IdSubModulo;
                    }

                    if (!string.IsNullOrEmpty(objVO.UrlSubModulo))
                    {
                        var appState = Dominio.AppState;
                        objSbSelect.AppendLine(@"

                            AND Filtro.IdSubModuloUrl = (
                                SELECT MAX(SUB_URL.IdSubModuloUrl)
                                    FROM DBAthon.dbo.SubModuloUrl SUB_URL  WITH (NOLOCK)
                                    JOIN DBAthon.dbo.SubModulo  WITH (NOLOCK) ON SubModulo.IdSubModulo = SUB_URL.IdSubModulo
                                    JOIN DBAthon.dbo.Modulo     WITH (NOLOCK) ON Modulo.IdModulo = SubModulo.IdModulo

                        ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Debug, "       WHERE CONCAT(Modulo.LinkDebug,'/',SUB_URL.Url) = @UrlSubModulo ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Teste, "       WHERE CONCAT(Modulo.LinkTeste,'/',SUB_URL.Url) = @UrlSubModulo ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Homologacao, " WHERE CONCAT(Modulo.LinkHomologacao,'/',SUB_URL.Url) = @UrlSubModulo ");
                        objSbSelect.AppendLineIf(appState == Dominio.ApplicationState.Producao, "    WHERE CONCAT(Modulo.Link,'/',SUB_URL.Url)  =  @UrlSubModulo ");
                        objSbSelect.AppendLine(" ) ");

                        GetSqlCommand().Parameters.Add("UrlSubModulo", SqlDbType.VarChar).Value = objVO.UrlSubModulo;
                    }

                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(" AND Nome = @Nome ");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    }
                }

                objSbSelect.AppendLine(" ORDER BY Ordem ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                long ultId = 0;
                ConsultaVO filtro = null;
                while (GetSqlDataReader().Read())
                {
                    ConsultaCampoVO filtroCampo = new ConsultaCampoVO();
                    if (ultId != Convert.ToInt32(GetSqlDataReader()["IdFiltro"]))
                    {
                        filtro = new ConsultaVO();

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltro"))))
                            filtro.Id = Convert.ToInt32(GetSqlDataReader()["IdFiltro"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                            filtro.IdSubModulo = Convert.ToInt32(GetSqlDataReader()["IdSubModulo"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                            filtro.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("InstrucaoSQL"))))
                            filtro.InstrucaoSQL = Convert.ToString(GetSqlDataReader()["InstrucaoSQL"]);
                    }

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFiltroCampo"))))
                        filtroCampo.Id = Convert.ToInt32(GetSqlDataReader()["IdFiltroCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampo"))))
                        filtroCampo.NomeCampo = Convert.ToString(GetSqlDataReader()["NomeCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DescricaoCampo"))))
                        filtroCampo.DescricaoCampo = Convert.ToString(GetSqlDataReader()["DescricaoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TamanhoCampo"))))
                        filtroCampo.TamanhoCampo = Convert.ToInt32(GetSqlDataReader()["TamanhoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TipoCampo"))))
                        filtroCampo.TipoCampo = Convert.ToString(GetSqlDataReader()["TipoCampo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        filtroCampo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    filtro.LstFiltroCampos.Add(filtroCampo);
                    if (ultId != Convert.ToInt32(GetSqlDataReader()["IdFiltro"]))
                    {
                        lstFiltro.Add(filtro);
                        ultId = Convert.ToInt32(GetSqlDataReader()["IdFiltro"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
            return lstFiltro;
        }

        public ConsultaVO ConsultarSubmoduloUrl(ConsultaVO objVO)
        {
            try
            {
                List<ConsultaVO> lstFiltro = SelecionarSubmoduloUrl(objVO);
                return lstFiltro.Count() > 0 ? (ConsultaVO)lstFiltro.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Dictionary<int, List<FiltroVO>> Paginar(FiltroVO objVO)
        {
            Dictionary<int, List<FiltroVO>> dictionary = null;
            try
            {
                List<FiltroVO> lstFiltroVO;
                dictionary = new Dictionary<int, List<FiltroVO>>();
                var sbPaginar = new StringBuilder();
                lstFiltroVO = SelecionarFiltro(objVO);
                dictionary.Add(lstFiltroVO.Count, lstFiltroVO);
                return dictionary;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}