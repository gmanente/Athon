﻿using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Api.dll.Template.Seguranca.Grid
{
    public class SubmoduloUrlGridTemplate : SubmoduloWireFrameTemplate
    {
        public List<UsuarioFuncionalidadeVO> LstUsuarioFuncionalidade { get; set; }
        private long IdUsuario { get; set; }
        private long IdSubModulo { get; set; }

        public SubmoduloUrlGridTemplate(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
            : base()
        {
            IdSubModulo = 0;
            IdUsuario = idUsuario;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                LstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(urlSubModulo, idUsuario, idCampus, acessoExterno);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }

        }

        private bool Autenticar(string rf)
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

        public void SetSubmoduloUrlTemplate()
        {

            SetTemplate();


            //Breadcrumb template
            var l = new List<Li>();
            l.Add(new Li()
            {
                Link = { Href = "../Page/SubModuloUrl.aspx", Title = "Voltar para o SubModuloUrl", Target = Target.Self, Text = "SubModuloUrl" }

            });
            l.Add(new Li()
            {
                Text = "Segurança"
            });
            l.Add(new Li()
            {
                Text = "SubModuloUrl",
                Class = "active"
            });

            BreadcrumbTemplate.LiList = l;
            GridContainer.AddComponentContent(Grid);

            //Add content               
            Content.Add(BreadcrumbTemplate);
            Content.Add(ImageLoading);
            Content.Add(GridContainer);
        }

        //Set funções grid
        public BtnDropDown GetFuncoesGrid()
        {

            //Botão dropdown de funcionalidades
            var btnDrop = new BtnDropDown()
            {
                Tag = Tag.Button,
                Text = "Ações",
                Layout = Layout.Padrao,
                Icon = "share",
                Size = Size.ExtraPequeno
            };

            if (Autenticar("RF003"))
            {
                var itemMenu = new ItemMenu()
                {
                    Text = "Alterar",
                    Icone = "edit",
                    Titulo = "Alterar",
                    Class = "item-acao-alterar",
                };
                btnDrop.AddItem(itemMenu);
            }
            if (Autenticar("RF004"))
            {
                var itemMenu = new ItemMenu()
                {
                    Text = "Excluir",
                    Icone = "trash-o",
                    Titulo = "Excluir",
                    Class = "item-acao-excluir",
                };
                btnDrop.AddItem(itemMenu);
            }

            return btnDrop;
        }

        //Set funções grid
        private void SetFuncoesGrid()
        {
            Grid.SetBtnFuncoes(GetFuncoesGrid());
        }

        //GetGrid
        public string GetGrid()
        {
            return Grid.ToString();
        }

        //SetGrid
        public void SetGrid<T>(List<T> lista, string[] binding)
        {
            SetFuncoesGrid();

            var AlterarAjax = new JsInjector() {                 
                ContentCode = "AlterarSubmoduloUrl();"                
            };

            var ExcluirAjax = new JsInjector()
            {
                ContentCode = "ExcluirSubmoduloUrl();"
            };

            //Chamada ajax grid paginação
            var chamadaAjaxGridPaginacao = new AjaxCall()
            {
                ContentCode = "var page = $(this).attr('data-pag');", 
                Arr = "{page:page}",
                ElementSelector = "'.pagination-pag'",
                EventFunction = "click",
                CleanForm = "false",
                FormId = "'#form'",
                Button = "false",
                ValidationRules = "{}",
                RequestUrl = "'../Page/SubmoduloUrl.aspx'",
                WebMethod = "'PaginacaoAjax'",
                RequestMethod = "'POST'",
                RequestAsynchronous = "true",
                Callback = "paginacaoCallback(objJson);"
            };

            Grid.AjaxCall = chamadaAjaxGridPaginacao.Create() + AlterarAjax.Create() + ExcluirAjax.Create();
            Grid.MontarGrid(lista, binding, null, false);
        }

        //ToString
        public override string ToString()
        {
            SetSubmoduloUrlTemplate();
            return Content.ToString();
        }

        //Render
        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }

    }
}