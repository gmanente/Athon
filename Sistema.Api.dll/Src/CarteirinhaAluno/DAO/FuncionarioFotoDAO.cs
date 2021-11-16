using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.DAO
{
    public class FuncionarioFotoDAO : AbstractDAO
    {
        public FuncionarioFotoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public long Inserir(FuncionarioFotoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                objSbInsert.AppendLine(@"INSERT INTO  DBAlunoFoto.dbo.FuncionarioFoto       ");
                objSbInsert.AppendLine(@"(                                                  ");
                objSbInsert.AppendLine(@"               IdFuncionarioFotoTipo               ");
                objSbInsert.AppendLine(@"             , Nome                                ");
                objSbInsert.AppendLine(@"             , NomeCracha                          ");
                objSbInsert.AppendLine(@"             , Departamento                        ");
                objSbInsert.AppendLine(@"             , Cargo                               ");
                objSbInsert.AppendLine(@"             , Cpf                                 ");
                objSbInsert.AppendLine(@"             , Rg                                  ");
                objSbInsert.AppendLine(@"             , Matricula                           ");
                objSbInsert.AppendLine(@"             , DataAdmissao                        ");
                objSbInsert.AppendLine(@"             , TituloEleitor                       ");
                objSbInsert.AppendLine(@"             , CarteiraTrabalho                    ");
                objSbInsert.AppendLine(@"             , PisPasep                            ");
                objSbInsert.AppendLine(@"             , Imagem                              ");
                objSbInsert.AppendLine(@"             , NrVia                               ");
                objSbInsert.AppendLine(@"              ,DataCadastro                        ");
                objSbInsert.AppendLine(@")                                                  ");
                objSbInsert.AppendLine(@"VALUES                                             ");
                objSbInsert.AppendLine(@"(                                                  ");
                objSbInsert.AppendLine(@"                @IdFuncionarioFotoTipo             ");
                objSbInsert.AppendLine(@"              , @Nome                              ");
                objSbInsert.AppendLine(@"              , @NomeCracha                        ");
                objSbInsert.AppendLine(@"              , @Departamento                      ");
                objSbInsert.AppendLine(@"              , @Cargo                             ");
                objSbInsert.AppendLine(@"              , @Cpf                               ");
                objSbInsert.AppendLine(@"              , @Rg                                ");
                objSbInsert.AppendLine(@"              , @Matricula                         ");
                objSbInsert.AppendLine(@"              , @DataAdmissao                      ");
                objSbInsert.AppendLine(@"              , @TituloEleitor                     ");
                objSbInsert.AppendLine(@"              , @CarteiraTrabalho                  ");
                objSbInsert.AppendLine(@"              , @PisPasep                          ");
                objSbInsert.AppendLine(@"              , @Imagem                            ");
                objSbInsert.AppendLine(@"              , @NrVia                             ");
                objSbInsert.AppendLine(@"              , @DataCadastro                      ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFuncionarioFotoTipo", SqlDbType.Int).Value = objVO.FuncionarioFotoTipo.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("NomeCracha", SqlDbType.VarChar).Value = objVO.NomeCracha;
                GetSqlCommand().Parameters.Add("PisPasep", SqlDbType.VarChar).Value = objVO.PisPasep;
                GetSqlCommand().Parameters.Add("Departamento", SqlDbType.VarChar).Value = objVO.Departamento;
                GetSqlCommand().Parameters.Add("Cargo", SqlDbType.VarChar).Value = objVO.Cargo;
                GetSqlCommand().Parameters.Add("CarteiraTrabalho", SqlDbType.VarChar).Value = objVO.CarteiraTrabalho;
                GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                GetSqlCommand().Parameters.Add("Rg", SqlDbType.VarChar).Value = objVO.Rg;
                GetSqlCommand().Parameters.Add("Matricula", SqlDbType.VarChar).Value = objVO.Matricula;
                GetSqlCommand().Parameters.Add("DataAdmissao", SqlDbType.DateTime).Value = objVO.DataAdmissao;
                GetSqlCommand().Parameters.Add("TituloEleitor", SqlDbType.VarChar).Value = objVO.TituloEleitor;
                GetSqlCommand().Parameters.Add("Imagem", SqlDbType.VarBinary).Value = objVO.Imagem;
                GetSqlCommand().Parameters.Add("NrVia", SqlDbType.Int).Value = objVO.NrVia;
                GetSqlCommand().Parameters.Add("DataCadastro", SqlDbType.DateTime).Value = DateTime.Now;

                GetSqlCommand().ExecuteNonQuery();

                return UltimoIdInseridoIdentity("DBAlunoFoto.dbo.FuncionarioFoto");

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
                Close();
            }
        }

        public long Alterar(FuncionarioFotoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAlunoFoto.dbo.FuncionarioFoto      ");
                objSbUpdate.AppendLine("   SET                                      ");
                objSbUpdate.AppendLine("          Imagem    =  @Imagem              ");


                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdFuncionarioFoto = @IdFuncionarioFoto");
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
                    GetSqlCommand().Parameters.Add("Imagem", SqlDbType.VarBinary).Value = objVO.Imagem;
                    GetSqlCommand().Parameters.Add("IdFuncionarioFoto", SqlDbType.Int).Value = objVO.Id;
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
                Close();
            }
        }

        public void Deletar(FuncionarioFotoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                // Foi adicionado as duas linhas abaixo para excluir os registros de log do crachá
                // o Colaborador pode ter sido demitido e retornado a IES
                // o cracha é vinculado ao CPF do colaborador
                objSbDelete.AppendLine("DELETE FROM DBAlunoFoto.dbo.FuncionarioFotoLog ");
                objSbDelete.AppendLine(" WHERE IdFuncionarioFoto = @IdFuncionarioFoto");

                objSbDelete.AppendLine("DELETE FROM DBAlunoFoto.dbo.FuncionarioFoto ");
                objSbDelete.AppendLine(" WHERE IdFuncionarioFoto = @IdFuncionarioFoto");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFuncionarioFoto", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                {
                    objSbDelete = null;
                }
                Close();
            }
        }


        public long AlterarDados(FuncionarioFotoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAlunoFoto.dbo.FuncionarioFoto                    ");
                objSbUpdate.AppendLine("   SET                                                    ");
                objSbUpdate.AppendLine("          Nome              =  @Nome,                     ");
                objSbUpdate.AppendLine("          NomeCracha        =  @NomeCracha,               ");
                objSbUpdate.AppendLine("          Cpf               =  @Cpf,                      ");
                objSbUpdate.AppendLine("          Departamento      =  @Departamento,             ");
                objSbUpdate.AppendLine("          Cargo             =  @Cargo,                    ");
                objSbUpdate.AppendLine("          Matricula         =  @Matricula,                ");
                objSbUpdate.AppendLine("          DataAdmissao      =  @DataAdmissao              ");



                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdFuncionarioFoto = @IdFuncionarioFoto");
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
                    GetSqlCommand().Parameters.Add("NomeCracha", SqlDbType.VarChar).Value = objVO.NomeCracha;
                    GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                    GetSqlCommand().Parameters.Add("Departamento", SqlDbType.VarChar).Value = objVO.Departamento;
                    GetSqlCommand().Parameters.Add("Cargo", SqlDbType.VarChar).Value = objVO.Cargo;
                    GetSqlCommand().Parameters.Add("Matricula", SqlDbType.VarChar).Value = objVO.Matricula;
                    GetSqlCommand().Parameters.Add("DataAdmissao", SqlDbType.DateTime).Value = objVO.DataAdmissao;
                    GetSqlCommand().Parameters.Add("IdFuncionarioFoto", SqlDbType.Int).Value = objVO.Id;
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
                Close();
            }
        }


        public long AlterarNrVia(FuncionarioFotoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAlunoFoto.dbo.FuncionarioFoto                                                           ");
                objSbUpdate.AppendLine("SET NrVia =  (SELECT                                                          ");
                objSbUpdate.AppendLine("    DBAlunoFoto.dbo.FuncionarioFoto.NrVia  + 1                                ");
                objSbUpdate.AppendLine("    FROM DBAlunoFoto.dbo.FuncionarioFoto                                      ");
                objSbUpdate.AppendLine("WHERE DBAlunoFoto.dbo.FuncionarioFoto.IdFuncionarioFoto = @IdFuncionarioFoto) ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdFuncionarioFoto = @IdFuncionarioFoto");
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
                    GetSqlCommand().Parameters.Add("IdFuncionarioFoto", SqlDbType.Int).Value = objVO.Id;
                }
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
                {
                    objSbUpdate = null;
                }
            }
        }


        public List<FuncionarioFotoVO> GetLista()
        {
            FuncionarioFotoVO funcionarioFotoVO = null;
            List<FuncionarioFotoVO> lstFuncionarioFotoVO = null;

            try
            {
                lstFuncionarioFotoVO = new List<FuncionarioFotoVO>();
                while (GetSqlDataReader().Read())
                {
                    funcionarioFotoVO = new FuncionarioFotoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionarioFoto"))))
                        funcionarioFotoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionarioFoto"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionarioFotoTipo"))))
                        funcionarioFotoVO.FuncionarioFotoTipo.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionarioFotoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        funcionarioFotoVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCracha"))))
                        funcionarioFotoVO.NomeCracha = Convert.ToString(GetSqlDataReader()["NomeCracha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Rg"))))
                        funcionarioFotoVO.Rg = Convert.ToString(GetSqlDataReader()["Rg"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Departamento"))))
                        funcionarioFotoVO.Departamento = Convert.ToString(GetSqlDataReader()["Departamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cargo"))))
                        funcionarioFotoVO.Cargo = Convert.ToString(GetSqlDataReader()["Cargo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        funcionarioFotoVO.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataAdmissao"))))
                        funcionarioFotoVO.DataAdmissao = Convert.ToDateTime(GetSqlDataReader()["DataAdmissao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TituloEleitor"))))
                        funcionarioFotoVO.TituloEleitor = Convert.ToString(GetSqlDataReader()["TituloEleitor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Matricula"))))
                        funcionarioFotoVO.Matricula = Convert.ToString(GetSqlDataReader()["Matricula"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CarteiraTrabalho"))))
                        funcionarioFotoVO.CarteiraTrabalho = Convert.ToString(GetSqlDataReader()["CarteiraTrabalho"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("PisPasep"))))
                        funcionarioFotoVO.PisPasep = Convert.ToString(GetSqlDataReader()["PisPasep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Imagem"))))
                        funcionarioFotoVO.ImagemBase64 = Convert.ToBase64String((byte[])GetSqlDataReader()["Imagem"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NrVia"))))
                        funcionarioFotoVO.NrVia = Convert.ToInt32(GetSqlDataReader()["NrVia"]);

                    lstFuncionarioFotoVO.Add(funcionarioFotoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstFuncionarioFotoVO;
        }

        public List<FuncionarioFotoVO> Selecionar(string sql)
        {
            try
            {
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;

                return GetLista();
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

        }

        public List<FuncionarioFotoVO> Selecionar(FuncionarioFotoVO objVO, int top = 0)
        {
            try
            {

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"

                        SELECT

                               IdFuncionarioFoto
                             , IdFuncionarioFotoTipo
                             , Nome
                             , NomeCracha
                             , Departamento
                             , Cargo
                             , Cpf
                             , Rg
                             , Matricula
                             , DataAdmissao
                             , TituloEleitor
                             , CarteiraTrabalho
                             , PisPasep
                             , Imagem
                             , NrVia

                        FROM DBAlunoFoto.dbo.FuncionarioFoto  WITH (NOLOCK)

                        WHERE 1 = 1

                ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND FuncionarioFoto.IdFuncionarioFoto = @IdFuncionarioFoto");
                        GetSqlCommand().Parameters.Add("IdFuncionarioFoto", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.Matricula != null)
                    {
                        objSbSelect.AppendLine(@" AND FuncionarioFoto.Matricula = @Matricula");
                        GetSqlCommand().Parameters.Add("Matricula", SqlDbType.VarChar).Value = objVO.Matricula;
                    }

                    if (objVO.Cpf != null)
                    {
                        objSbSelect.AppendLine(@" AND FuncionarioFoto.Cpf = @Cpf");
                        GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = Funcoes.limparCpf(objVO.Cpf);
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                return GetLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }
        }

        public List<FuncionarioFotoVO> Listar(FuncionarioFotoVO objVO)
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

        public FuncionarioFotoVO Consultar(FuncionarioFotoVO objVO)
        {
            try
            {
                List<FuncionarioFotoVO> lstFuncionarioFotoVO = Selecionar(objVO);
                return lstFuncionarioFotoVO.Count > 0 ? (FuncionarioFotoVO)lstFuncionarioFotoVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<FuncionarioFotoVO> ConsultarPorCpfCarteirinha(string cpf, string situacaoFuncionario)
        {
            FuncionarioFotoVO funcionarioFotoVO = null;
            List<FuncionarioFotoVO> lstFuncionarioFotoVO = null;

            try
            {
                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"SELECT
                                            COLIGADA
                                          , [Matrícula] Matricula
                                          , [Nome do Colaborador] AS Nome
                                          , Departamento
                                          , Recebimento
                                          , NomeCracha
                                          , Tipo
                                          , [Salário] AS Salariao
                                          , Instrução
                                          , Situação
                                          , [Data de Admissão] AS DataAdimissao
                                          , [Data de Demissão] AS DataDemissao
                                          , [Estado Civil] AS EstadoCivil
                                          , Cargo
                                          , Identidade
                                          , CPF
                                          , [Titulo de Eleitor] AS Titulo
                                          , [Carteira de Trabalho] AS CarteiraTrabalho
                                          , PisPasep
                                      FROM DBComum.dbo.UvwConsultarFuncionario");

                GetSqlCommand().Parameters.Clear();

                //objSbSelect.AppendLine("WHERE UvwConsultarFuncionario.[Situação] IN ('Ativo','Admissão prox.mês')");
                objSbSelect.AppendLine(@" WHERE 1 = 1");
                objSbSelect.AppendLine(@" AND UvwConsultarFuncionario.CPF = @Cpf");
                objSbSelect.AppendLine(@" AND UvwConsultarFuncionario.CodSituacao COLLATE SQL_Latin1_General_CP1_CI_AI IN (SELECT Value FROM DBComum.dbo.F_Split('" + situacaoFuncionario + "', ','))");

                GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = Funcoes.limparCpf(cpf);


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                lstFuncionarioFotoVO = new List<FuncionarioFotoVO>();

                while (GetSqlDataReader().Read())
                {
                    funcionarioFotoVO = new FuncionarioFotoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        funcionarioFotoVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCracha"))))
                        funcionarioFotoVO.NomeCracha = Convert.ToString(GetSqlDataReader()["NomeCracha"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Departamento"))))
                        funcionarioFotoVO.Departamento = Convert.ToString(GetSqlDataReader()["Departamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cargo"))))
                        funcionarioFotoVO.Cargo = Convert.ToString(GetSqlDataReader()["Cargo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CPF"))))
                        funcionarioFotoVO.Cpf = Convert.ToString(GetSqlDataReader()["CPF"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataAdimissao"))))
                        funcionarioFotoVO.DataAdmissao = Convert.ToDateTime(GetSqlDataReader()["DataAdimissao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Titulo"))))
                        funcionarioFotoVO.TituloEleitor = Convert.ToString(GetSqlDataReader()["Titulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Matricula"))))
                        funcionarioFotoVO.Matricula = Convert.ToString(GetSqlDataReader()["Matricula"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CarteiraTrabalho"))))
                        funcionarioFotoVO.CarteiraTrabalho = Convert.ToString(GetSqlDataReader()["CarteiraTrabalho"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("PisPasep"))))
                        funcionarioFotoVO.PisPasep = Convert.ToString(GetSqlDataReader()["PisPasep"]);

                    lstFuncionarioFotoVO.Add(funcionarioFotoVO);
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

            return lstFuncionarioFotoVO;
        }

        public List<FuncionarioFotoVO> ListarCargos(FuncionarioFotoVO objVO)
        {
            List<FuncionarioFotoVO> lstFuncionarioFotoVO = null;

            try
            {
                objSbSelect = new StringBuilder();
                objSbSelect.AppendLine(@"       SELECT CargoCodigo                                  ");
                objSbSelect.AppendLine(@"       	   ,Cargo                                       ");
                objSbSelect.AppendLine(@"         FROM DBComum.dbo.UvwConsultarFuncionario          ");
                objSbSelect.AppendLine(@"        WHERE 1 = 1                                        ");


                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (!string.IsNullOrEmpty(objVO.CargoCodigo))
                    {
                        objSbSelect.AppendLine(@" AND DBComum.dbo.UvwConsultarFuncionario.CargoCodigo = @CargoCodigo");
                        GetSqlCommand().Parameters.Add("CargoCodigo", SqlDbType.VarChar).Value = objVO.CargoCodigo;
                    }

                    if (!string.IsNullOrEmpty(objVO.Cargo))
                    {
                        objSbSelect.AppendLine(@" AND DBComum.dbo.UvwConsultarFuncionario.Cargo = @Cargo");
                        GetSqlCommand().Parameters.Add("Cargo", SqlDbType.VarChar).Value = objVO.Cargo;
                    }
                }

                objSbSelect.AppendLine(@"         GROUP BY CargoCodigo           ");
                objSbSelect.AppendLine(@"                 ,Cargo                 ");
                objSbSelect.AppendLine(@"         ORDER BY Cargo                 ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                lstFuncionarioFotoVO = new List<FuncionarioFotoVO>();
                var reader = GetSqlDataReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var funcionarioFotoVO = new FuncionarioFotoVO();

                        funcionarioFotoVO.CargoCodigo = reader.GetValue<string>("CargoCodigo");
                        funcionarioFotoVO.Cargo = reader.GetValue<string>("Cargo");

                        lstFuncionarioFotoVO.Add(funcionarioFotoVO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;
                Close();
            }

            return lstFuncionarioFotoVO;
        }



    }
}
