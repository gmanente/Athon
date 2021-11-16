using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;

namespace Sistema.Api.dll.Src.Seguranca
{
    public class Controle
    {       
        //Get Modulo
        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 03/12/2015
        /// Descrição: Resonsavel por retornar o Modulo
        /// </summary>                
        public static ModuloVO GetModulo(string modulo)
        {
            ModuloBE ModuloBE = null;
            ModuloVO ModuloVO = null;
            try
            {

                ModuloBE = new ModuloBE();
                ModuloVO = ModuloBE.Consultar(new ModuloVO()
                {
                    Nome = modulo,
                });

                //Verifica se o Modulo foi encontrado
                if (ModuloVO != null)
                {
                    return ModuloVO;
                }
                else
                {
                    throw new Exception(string.Format("O Modulo {0} não foi encontrado. Por favor entre em contato com a equipe de Tecnologia da Informação.", modulo));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuloBE != null)
                {
                    ModuloBE.FecharConexao();
                }
            }
        }

        //Get IdModulo
        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 03/12/2015
        /// Descrição: Resonsavel por retornar o IdModulo
        /// </summary>        
        public static long GetIdModulo(string modulo)
        {
            var Modulo = GetModulo(modulo);
            return (Modulo != null) ? Modulo.Id : 0;
        }

        //Get LstSubmodulos
        /// <summary>
        /// Autor: Lucas Holanda
        /// Data: 03/12/2015
        /// Descrição: Resonsavel por retornar uma lista de Submodulos
        /// </summary>        
        public static List<SubmoduloVO> GetLstSubmodulos(string modulo)
        {
            SubmoduloBE SubmoduloBE = null;
            try
            {

                SubmoduloBE = new SubmoduloBE();
                var LstSubmodulos = SubmoduloBE.Selecionar(new SubmoduloVO()
                {
                    Modulo = { Id = GetIdModulo(modulo) }
                });

                //Verifica se o Submodulo foi encontrado
                if (LstSubmodulos != null)
                {
                    return LstSubmodulos;
                }
                else
                {
                    throw new Exception(string.Format("Não foram encontrados submodulos para o modulo {0}. Por favor entre em contato com a equipe de Tecnologia da Informação.", modulo));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SubmoduloBE != null)
                {
                    SubmoduloBE.FecharConexao();
                }
            }
        }
    }

    public class Modulo
    {
        public const string Vestibular = "Vestibular";
        public const string Biblioteca = "Biblioteca";
        public const string Protocolo = "Protocolo";
        public const string Seguranca = "Segurança";
        public const string SecretariaAcademica = "Secretaria Academica";
        public const string CAE = "CAE";
        public const string FrenteCaixa = "Frente de Caixa";
        public const string Coordenação = "Coordenação";
        public const string GerenciaFinanceira = "Gerência Financeira";
        public const string Mensageria = "Mensageria";
        public const string Carteirinha = "Carteirinha";
        public const string AvaliacaoInstitucional = "Avaliação Institucional";
        public const string AssessoriaAcadêmica = "Assessoria Acadêmica";
        public const string Marketing = "Marketing";
        public const string TesourariaMaster = "Tesouraria Master";
        public const string Aluno = "Aluno";
        public const string PortalAluno = "Portal do Aluno";
        public const string ChequeTerceiros = "Cheque de Terceiros";
        public const string PortalProfessor = "Portal do Professor";
        public const string ControleBancario = "Controle Bancário ";
        public const string BolsasConvenios = "Bolsas e Convênios";
        public const string Relatorios = "Relatórios";
    }
}