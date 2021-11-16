using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Threading;
using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class AuditoriaBE : AbstractBE, IBE<AuditoriaVO>
    {
        public AuditoriaBE() : base()
        {
        }


        public AuditoriaBE(SqlCommand sqlConn) : base(sqlConn)
        {
        }

        public AuditoriaBE(bool semConexao)
        {
        }


        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Alterar(AuditoriaVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="objVO"></param>
        public void Deletar(AuditoriaVO objVO)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(AuditoriaVO objVO)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());


                BeginTransaction();


                // ---------- Insere o AuditoriaVO
                objVO.Id = dao.Inserir(objVO);


                Commit();

                return objVO.Id;
            }
            catch (Exception ex)
            {
                Rollback();

                throw ex;
            }
        }


        /// <summary>
        /// SetAuditoria
        /// </summary>
        /// <param name="AuditoriaVo"></param>
        public void SetAuditoria(AuditoriaVO AuditoriaVo)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());

                dao.SetAuditoria(AuditoriaVo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// AuditarAcao
        /// </summary>
        /// <param name="AuditoriaVo"></param>
        public void AuditarAcao(AuditoriaVO AuditoriaVo)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());


                BeginTransaction();


                dao.AuditarAcao(AuditoriaVo);


                Commit();
            }
            catch (Exception ex)
            {
                Rollback();

                throw ex;
            }
        }


        /// <summary>
        /// Auditar
        /// </summary>
        /// <param name="AuditoriaVo"></param>
        public void Auditar(AuditoriaVO AuditoriaVo)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());


                dao.AuditarAcesso(AuditoriaVo);


                dao.Auditar(AuditoriaVo);


                // ---------- Ativa a sesssão
                dao.AtivarDesativarSessao(AuditoriaVo.IdUsuario, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// AtivarDesativarSessao
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="sessaoAtiva"></param>
        /// <param name="openTransaction"></param>
        public void AtivarDesativarSessao(long idUsuario, bool sessaoAtiva, bool openTransaction = true)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());


                if (openTransaction)
                    BeginTransaction();


                dao.AtivarDesativarSessao(idUsuario, sessaoAtiva);


               if (openTransaction)
                    Commit();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    Rollback();

                throw ex;
            }
        }


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="top"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<AuditoriaVO> Selecionar(AuditoriaVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());

                return dao.Selecionar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public AuditoriaVO Consultar(AuditoriaVO objVO)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// SessaoAtiva
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="enderecoIp"></param>
        /// <param name="verificarIP"></param>
        /// <returns></returns>
        public bool SessaoAtiva(long idUsuario, string enderecoIp = null, bool verificarIP = true)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());


                var auditoriaVO = dao.Consultar(new AuditoriaVO() { IdUsuario = idUsuario });


                bool MesmoIP = auditoriaVO.EnderecoIp == enderecoIp;

                if (verificarIP)
                {
                    return auditoriaVO == null ? false : (auditoriaVO.SessaoAtiva && MesmoIP);
                }
                else
                {
                    return auditoriaVO == null ? false : (auditoriaVO.SessaoAtiva && SessaoValida(auditoriaVO.DataWho) && !MesmoIP);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// SessaoAtiva
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="enderecoIp"></param>
        /// <param name="verificarIP"></param>
        /// <returns></returns>
        public bool SessaoAtivaChaveUnica(Guid? chaveUnica, long idUsuario)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());
                
                var auditoriaVO = dao.Consultar(new AuditoriaVO() { IdUsuario = idUsuario });

                // --------------- Se não encontrar o AuditoriaVO
                if (auditoriaVO == null)
                {
                    throw new Exception("Não foi possível recuperar a auditoria.");
                }                             

                // --------------- Verifica se a sessão é válida
                bool sessaoValida = SessaoValida(auditoriaVO.DataWho);

                // --------------- Se a sessão Não for válida
                if (sessaoValida == false)
                {
                    return false;
                }
                
                // --------------- Se a auditoria possui ChaveUnica
                if (auditoriaVO.ChaveUnica != null)
                {
                    // ---------- Se o chaveUnica for nulo
                    if (chaveUnica == null)
                    {
                        return false;
                    }

                    // ---------- Se a chave única não confere
                    if (chaveUnica != auditoriaVO.ChaveUnica)
                    {
                        return false;
                    }

                    // ---------- Se a chave única não confere
                    if (Dominio.UnicaSessao && chaveUnica != auditoriaVO.ChaveUnica)
                    {
                        return false;
                    }      

                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// SessaoValida
        /// </summary>
        /// <param name="DataWho"></param>
        /// <returns></returns>
        public bool SessaoValida(DateTime DataWho)
        {
            try
            {
                // Verifica tempo da ultima sessão do usuario
                int Dias = DateTime.Now.Subtract(DataWho).Days,
                    Horas = DateTime.Now.Subtract(DataWho).Hours,
                    Minutos = DateTime.Now.Subtract(DataWho).Minutes;

                return Dias > 0 ? false : ((Horas > 0 ? false : (Minutos > 60 ? false : true)));
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<AuditoriaVO> Listar(AuditoriaVO objVO = null, bool detalhar = false)
        {
            bool abriuTransacao = false;

            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());


                var LstAuditoriaVo = dao.Listar(objVO);


                BeginTransaction();
                abriuTransacao = true;


                foreach (var auditoria in LstAuditoriaVo)
                {
                    if (auditoria.SessaoAtiva && !SessaoValida(auditoria.DataWho))
                    {
                        AtivarDesativarSessao(auditoria.IdUsuario, false, false);

                        auditoria.SessaoAtiva = false;
                    }
                }


                Commit();


                return LstAuditoriaVo;
            }
            catch (Exception ex)
            {
                if (abriuTransacao)
                    Rollback();

                throw ex;
            }
        }


        /// <summary>
        /// ListarAuditoriaAcesso
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<AuditoriaAcessoVO> ListarAuditoriaAcesso(AuditoriaAcessoVO objVO)
        {
            try
            {
                var dao = new AuditoriaDAO(GetSqlCommand());

                return dao.ListarAuditoriaAcesso(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GerarMonitoracaoAbrirEmail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="referencia"></param>
        public string GerarMonitoracaoAbrirEmail(long id, string referencia)
        {
            try
            {
                // ---------- Validações
                if (id == 0)
                    throw new ArgumentException("O Identificador não foi informado corretamente.");

                if (string.IsNullOrEmpty(referencia))
                    throw new ArgumentException("A Referência da entidade não foi informada corretamente.");

                if (Enum.IsDefined(typeof(EntidadeVO), referencia) == false)
                    throw new ArgumentException("A Referência da entidade informada não é valida.");


                // ---------- Gera o Token e a Query da imagem
                string debug = Dominio.AppState.ToString() != "Producao" ? "&debug=1" : "";
                string token = Criptografia.HashSHA1("univag-mail-" + id);
                string query = "?id=" + id.ToString() +"&ref=" + referencia + "&token=" + token + debug;

                string imagem = "<img src='https://sistema.univag.edu.br/monitor_email.png" + query + "' alt='Monitor de Acesso ao E-mail' />";

                return imagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ChecarAbrirEmail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="referencia"></param>
        /// <param name="token"></param>
        public void ChecarAbrirEmail(long id, string referencia, string token)
        {
            try
            {
                // ---------- Validações
                if (Enum.IsDefined(typeof(EntidadeVO), referencia) == false)
                    throw new ArgumentException("A entidade informada não é valida.");

                if (token != Criptografia.HashSHA1("univag-mail-" + id))
                    throw new ArgumentException("O Token informado não é valido.");


                // ---------- Atualizações
                string tabela = "";
                string coluna = "";

                try
                {
                    EntidadeVO entidade = (EntidadeVO)Enum.Parse(typeof(EntidadeVO), referencia);

                    tabela = GetEnumMemberAttrValue(entidade);

                    string[] arrayTb = tabela.Split('.');

                    coluna = "Id" + arrayTb[2];
                }
                catch
                {
                    throw new Exception("A Entidade informada não foi encontrada.");
                }             
                   

                // ---------- Consulta o Registro
                var dao = new AuditoriaDAO(GetSqlCommand());

                var obj = dao.ConsultarChecarAbrirEmail(tabela, coluna, id);

                if (obj == null)
                    throw new Exception("O Registro da entidade informado não foi encontrado.");


                // ---------- Atualiza o Registro, Informando a data e horario de abertura do E-mail
                if (obj.AbriuEmail == false)
                {
                    dao.DefinirAbrirEmail(tabela, coluna, id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetEnumMemberAttrValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public string GetEnumMemberAttrValue<T>(T enumVal)
        {
            var enumType = typeof(T);
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();

            if (attr != null)
                return attr.Value;

            return null;
        }

    }
}
