using System;
using System.Collections.Generic;
using Sistema.Api.dll.Src.Financeiro.BE;
using Sistema.Api.dll.Src.Financeiro.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Src.Seguranca.BE;

namespace Sistema.Web.UI.Relatorio.View.Page
{
    public partial class ChequeTerceiro : CommonPage
    {
        private List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade { get; set; }
        public List<ChequeSituacaoVO> LstChequeSituacao { get; set; }

        protected new void Page_Load(object sender, EventArgs e)
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            if (!IsPostBack)
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                base.Page_Load(sender, e);
                LstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);
                ListarChequeSituacao();
            }
        }

        //Listar Cheque Situação
        public void ListarChequeSituacao()
        {
            ChequeSituacaoBE chequeSituacaoBE = null;
            try
            {
                chequeSituacaoBE = new ChequeSituacaoBE();
                LstChequeSituacao = chequeSituacaoBE.Listar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (chequeSituacaoBE != null)
                {
                    chequeSituacaoBE.FecharConexao();
                }
            }
        }

        /// <summary>
        /// Autor: Gustavo Martins
        /// Data: 06.05.2015
        /// Descrição: Resonsavel pela construção da pagina relatorio
        /// </summary>
        /// <param name="input">Objeto com todos os campos do formulário de abertura do Movimento Cheque</param>
        /// <returns>Retorna objeto JSON da classe Ajax</returns> 
        //public static RelatorioGridTemplate GetGridTemplate()
        //{
        //    RenovarChecarSessao();
        //    var gridTemplate = new RelatorioGridTemplate(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus);
        //    return gridTemplate;
        //}

        public bool Autenticar(string rf)
        {
            foreach (var usuFuncionalidade in LstUsuarioFuncionalidade)
            {
                if (usuFuncionalidade.Funcionalidade.RequisitoFuncional != null &&
                    usuFuncionalidade.Funcionalidade.RequisitoFuncional.ToLower() == rf.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

    }
}