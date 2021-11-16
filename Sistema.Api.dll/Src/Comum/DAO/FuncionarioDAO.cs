using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class FuncionarioDAO : AbstractDAO, IDAO<FuncionarioVO>
    {
        public FuncionarioDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Alterar(FuncionarioVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(FuncionarioVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(FuncionarioVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioVO> Listar(FuncionarioVO objVO)
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

        public List<FuncionarioVO> ListarSimples(FuncionarioVO objVO)
        {
            try
            {
                return SelecionarSimples(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionarioVO> ListarGestorContrato(FuncionarioVO objVO)
        {
            try
            {
                return SelecionarGestorContrato(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionarioVO> SelecionarSimples(FuncionarioVO objVO, int top = 0)
        {
            FuncionarioVO FuncionarioVO = null;

            List<FuncionarioVO> lstFuncionarioVO = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstFuncionarioVO = new List<FuncionarioVO>();
                objSbSelect.AppendLine(@"SELECT TOP 20 
                                                    Matricula
                                                  , DBAthon.dbo.UfcFormatarNome(Nome) AS Nome
                                                  , DBAthon.dbo.UfcFormatarNome(Cargo) AS Cargo
                                                  , Departamento
                                                  , CPF 
                                            FROM     DBAthon.DBO.UvwConsultarFuncionarioSimples  
                                            WHERE 1 = 1");
                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (!string.IsNullOrEmpty(objVO.Matricula))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarFuncionarioSimples.Matricula = @Matricula ");
                        GetSqlCommand().Parameters.AddWithNullable("Matricula", objVO.Matricula);
                    }
                    if (!string.IsNullOrEmpty(objVO.Cpf))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarFuncionarioSimples.CPF = @Cpf ");
                        GetSqlCommand().Parameters.AddWithNullable("Cpf", objVO.Cpf);
                    }
                    if (!string.IsNullOrEmpty(objVO.NomeColaborador))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarFuncionarioSimples.Nome = @Nome ");
                        GetSqlCommand().Parameters.AddWithNullable("Nome", objVO.NomeColaborador);
                    }
                    if (!string.IsNullOrEmpty(objVO.LikeNomeMatricula))
                    {
                        string likeNomeMatricula = Regex.Replace(objVO.LikeNomeMatricula, @"\s+", "%");
                        objSbSelect.AppendLine(string.Format(" AND (DBAthon.dbo.UvwConsultarFuncionarioSimples.Nome {0} LIKE '%{1}%' {0} OR DBAthon.dbo.UvwConsultarFuncionarioSimples.Matricula LIKE '%{1}%') ", "COLLATE Latin1_general_CI_AI", likeNomeMatricula));
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    FuncionarioVO = new FuncionarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        FuncionarioVO.NomeColaborador = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Matricula"))))
                        FuncionarioVO.Matricula = Convert.ToString(GetSqlDataReader()["Matricula"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cargo"))))
                        FuncionarioVO.CargoNome = Convert.ToString(GetSqlDataReader()["Cargo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Departamento"))))
                        FuncionarioVO.Departamento = Convert.ToString(GetSqlDataReader()["Departamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CPF"))))
                        FuncionarioVO.Cpf = Convert.ToString(GetSqlDataReader()["CPF"]);

                    lstFuncionarioVO.Add(FuncionarioVO);
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

            return lstFuncionarioVO;
        }


        public List<FuncionarioVO> Selecionar(FuncionarioVO objVO, int top = 0)
        {
            FuncionarioVO FuncionarioVO = null;

            List<FuncionarioVO> lstFuncionarioVO = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstFuncionarioVO = new List<FuncionarioVO>();
                objSbSelect.AppendLine(@"
                                        SELECT TOP 50                                                            
                                               DBAthon.dbo.UvwConsultarFuncionario.[CODCOLIGADA]          AS CodColigada
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[COLIGADA]             AS NomeColigada
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Matrícula]            AS Matricula
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Nome do Colaborador]  AS NomeColaborador
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[NomeCracha]           AS NomeCracha      
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Departamento]         AS Departamento
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Recebimento]          AS Recebimento
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Tipo]                 AS Tipo
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Instrução]            AS Instrucao
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Situação]             AS Situacao
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Data de Admissão]     AS DataAdmissao
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Data de Demissão]     AS DataDemissao
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Estado Civil]         AS EstadoCivil
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Cargo]                AS CargoNome
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[CargoCodigo]          AS CargoCodigo 
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Identidade]           AS Rg
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[CPF]                  AS Cpf
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Titulo de Eleitor]    AS TituloEleitor
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[Carteira de Trabalho] AS CarteiraTrabalho
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[PisPasep]             AS PisPasep
                                              ,DBAthon.dbo.UvwConsultarFuncionario.[E-Mail]               AS Email
                                          FROM UvwConsultarFuncionario
                                           WHERE 1 = 1");
                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (!string.IsNullOrEmpty(objVO.Matricula))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarFuncionario.[Matrícula] = @Matricula ");
                        GetSqlCommand().Parameters.AddWithNullable("Matricula", objVO.Matricula);
                    }
                    if (!string.IsNullOrEmpty(objVO.Cpf))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarFuncionario.[Cpf] = @Cpf ");
                        GetSqlCommand().Parameters.AddWithNullable("Cpf", objVO.Cpf);
                    }
                    if (!string.IsNullOrEmpty(objVO.NomeColaborador))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwConsultarFuncionario.[Nome do Colaborador] = @Nome ");
                        GetSqlCommand().Parameters.AddWithNullable("Nome", objVO.NomeColaborador);
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    FuncionarioVO = new FuncionarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodColigada"))))
                        FuncionarioVO.CodColigada = Convert.ToInt32(GetSqlDataReader()["CodColigada"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeColigada"))))
                        FuncionarioVO.NomeColigada = Convert.ToString(GetSqlDataReader()["NomeColigada"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeColaborador"))))
                        FuncionarioVO.NomeColaborador = Convert.ToString(GetSqlDataReader()["NomeColaborador"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Matricula"))))
                        FuncionarioVO.Matricula = Convert.ToString(GetSqlDataReader()["Matricula"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Departamento"))))
                        FuncionarioVO.Departamento = Convert.ToString(GetSqlDataReader()["Departamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Recebimento"))))
                        FuncionarioVO.Recebimento = Convert.ToString(GetSqlDataReader()["Recebimento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Tipo"))))
                        FuncionarioVO.Tipo = Convert.ToString(GetSqlDataReader()["Tipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Instrucao"))))
                        FuncionarioVO.Instrucao = Convert.ToString(GetSqlDataReader()["Instrucao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Situacao"))))
                        FuncionarioVO.Situacao = Convert.ToString(GetSqlDataReader()["Situacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EstadoCivil"))))
                        FuncionarioVO.EstadoCivil = Convert.ToString(GetSqlDataReader()["EstadoCivil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CargoNome"))))
                        FuncionarioVO.CargoNome = Convert.ToString(GetSqlDataReader()["CargoNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CargoCodigo"))))
                        FuncionarioVO.CargoCodigo = Convert.ToInt32(GetSqlDataReader()["CargoCodigo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CargoNome"))))
                        FuncionarioVO.CargoNome = Convert.ToString(GetSqlDataReader()["CargoNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Rg"))))
                        FuncionarioVO.Rg = Convert.ToString(GetSqlDataReader()["Rg"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        FuncionarioVO.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TituloEleitor"))))
                        FuncionarioVO.TituloEleitor = Convert.ToString(GetSqlDataReader()["TituloEleitor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CarteiraTrabalho"))))
                        FuncionarioVO.CarteiraTrabalho = Convert.ToString(GetSqlDataReader()["CarteiraTrabalho"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("PisPasep"))))
                        FuncionarioVO.PisPasep = Convert.ToString(GetSqlDataReader()["PisPasep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        FuncionarioVO.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    /* if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataInicio"))))
                         FuncionarioVO.DataInicio = Convert.ToDateTime(GetSqlDataReader()["DataInicio"]);

                     if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataTermino"))))
                         FuncionarioVO.DataTermino = Convert.ToDateTime(GetSqlDataReader()["DataTermino"]);*/

                    lstFuncionarioVO.Add(FuncionarioVO);
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

            return lstFuncionarioVO;
        }

        public List<FuncionarioVO> SelecionarGestorContrato(FuncionarioVO objVO, int top = 0)
        {
            FuncionarioVO FuncionarioVO = null;

            List<FuncionarioVO> lstFuncionarioVO = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstFuncionarioVO = new List<FuncionarioVO>();
                objSbSelect.AppendLine(@"
                                        SELECT   
                                              CorporeRM.dbo.PFUNC.CODCOLIGADA        AS CodColigada  
                                             ,CorporeRM.dbo.PFUNC.CHAPA              AS Matricula  
                                             ,DBAthon.dbo.UfcFormatarNome(CorporeRM.dbo.PPESSOA.NOME) AS Nome  
                                             ,CorporeRM.dbo.PSECAO.DESCRICAO         AS Departamento  
                                             ,CorporeRM.dbo.PCODSITUACAO.DESCRICAO   AS Situacao  
                                             ,CorporeRM.dbo.PFUNC.DATAADMISSAO       AS DataAdmissao  
                                             ,CorporeRM.dbo.PFUNC.DATADEMISSAO       AS DataDemissao   
                                             ,CorporeRM.dbo.PFUNCAO.NOME             AS CargoNome  
                                             ,CorporeRM.dbo.PFUNCAO.CODIGO           AS CargoCodigo   
                                             ,CorporeRM.dbo.PPESSOA.CARTIDENTIDADE   AS Identidade  
                                             ,CorporeRM.dbo.PPESSOA.CPF              AS CPF  
                                             ,CorporeRM.dbo.PPESSOA.EMAIL            AS Email  
   
                                        FROM CorporeRM.dbo.PFUNC WITH (NOLOCK)  
  
                                        JOIN CorporeRM.dbo.PPESSOA  WITH (NOLOCK)  
                                          ON CorporeRM.dbo.PFUNC.CODPESSOA = CorporeRM.dbo.PPESSOA.CODIGO  
  
                                        JOIN CorporeRM.dbo.PSECAO  WITH (NOLOCK)  
                                          ON CorporeRM.dbo.PFUNC.CODSECAO    = CorporeRM.dbo.PSECAO.CODIGO   
                                         AND CorporeRM.dbo.PFUNC.CODCOLIGADA = CorporeRM.dbo.PSECAO.CODCOLIGADA  
  
                                        JOIN CorporeRM.dbo.PFUNCAO  WITH (NOLOCK)  
                                          ON CorporeRM.dbo.PFUNCAO.CODIGO      = CorporeRM.dbo.PFUNC.CODFUNCAO   
                                         AND CorporeRM.dbo.PFUNCAO.CODCOLIGADA = CorporeRM.dbo.PFUNC.CODCOLIGADA  
  
                                        JOIN CorporeRM.dbo.PCODSITUACAO  WITH (NOLOCK)  
                                          ON CorporeRM.dbo.PCODSITUACAO.CODCLIENTE = CorporeRM.dbo.PFUNC.CODSITUACAO  

                                        WHERE CorporeRM.dbo.PFUNC.CHAPA COLLATE Latin1_General_CI_AS IN (SELECT DBCompra.dbo.ContratoGestor.Matricula COLLATE Latin1_General_CI_AS 
                                                                                                           FROM DBCompra.dbo.ContratoGestor) ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (!string.IsNullOrEmpty(objVO.Matricula))
                    {
                        string nome = System.Text.RegularExpressions.Regex.Replace(objVO.Matricula, @"\s+", "%");
                        objSbSelect.AppendLine(@" AND (CorporeRM.dbo.PPESSOA.NOME LIKE ").Append(" '%" + nome + "%' ");
                        objSbSelect.AppendLine(@" OR CorporeRM.dbo.PFUNC.CHAPA LIKE ").Append(" '%" + nome + "%' ");
                        objSbSelect.AppendLine(@" OR CorporeRM.dbo.PPESSOA.CPF LIKE ").Append(" '%" + nome + "%' )");
                    }
                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    FuncionarioVO = new FuncionarioVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodColigada"))))
                        FuncionarioVO.CodColigada = Convert.ToInt32(GetSqlDataReader()["CodColigada"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Matricula"))))
                        FuncionarioVO.Matricula = Convert.ToString(GetSqlDataReader()["Matricula"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        FuncionarioVO.NomeColaborador = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Departamento"))))
                        FuncionarioVO.Departamento = Convert.ToString(GetSqlDataReader()["Departamento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Situacao"))))
                        FuncionarioVO.Situacao = Convert.ToString(GetSqlDataReader()["Situacao"]);

                    //if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EstadoCivil"))))
                    //    FuncionarioVO.EstadoCivil = Convert.ToString(GetSqlDataReader()["EstadoCivil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CargoNome"))))
                        FuncionarioVO.CargoNome = Convert.ToString(GetSqlDataReader()["CargoNome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CargoCodigo"))))
                        FuncionarioVO.CargoCodigo = Convert.ToInt32(GetSqlDataReader()["CargoCodigo"]);
                    
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Identidade"))))
                        FuncionarioVO.Rg = Convert.ToString(GetSqlDataReader()["Identidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        FuncionarioVO.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    //if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("TituloEleitor"))))
                    //    FuncionarioVO.TituloEleitor = Convert.ToString(GetSqlDataReader()["TituloEleitor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        FuncionarioVO.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    lstFuncionarioVO.Add(FuncionarioVO);
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

            return lstFuncionarioVO;
        }
        public FuncionarioVO Consultar(FuncionarioVO objVO)
        {
            try
            {
                List<FuncionarioVO> lstFuncionarioVO = Selecionar(objVO);

                return lstFuncionarioVO.Count > 0 ? (FuncionarioVO)lstFuncionarioVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public FuncionarioVO ConsultarGestorContrato(FuncionarioVO objVO)
        {
            try
            {
                List<FuncionarioVO> lstFuncionarioVO = SelecionarGestorContrato(objVO);

                return lstFuncionarioVO.Count > 0 ? (FuncionarioVO)lstFuncionarioVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
