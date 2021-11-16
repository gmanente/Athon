using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class SexoBE : AbstractBE, IBE<SexoVO>
    {
        public SexoBE()
           : base()
        {
        }

        public SexoBE(SqlCommand sqlConn)
           : base(sqlConn)
        {
        }

        public long Inserir(SexoVO objVO)
        {
            SexoDAO sexoDAO = null;
            try
            {
                BeginTransaction();
                sexoDAO = new SexoDAO(GetSqlCommand());
                long id = sexoDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(SexoVO objVO, string where = null)
        {
            SexoDAO sexoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                sexoDAO = new SexoDAO(GetSqlCommand());
                id = sexoDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(SexoVO objVO)
        {
            SexoDAO sexoDAO = null;
            try
            {
                BeginTransaction();
                sexoDAO = new SexoDAO(GetSqlCommand());
                sexoDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        /// <summary>
        /// Autor: Michael Lopes.
        /// Data: 29/09/2014
        /// Descrição: Responsavel por validar os dados de inserção de sexo
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public string ValidarInsercao(SexoVO objVO)
        {
            string mensagem = null;
            if (Consultar(new SexoVO() { Descricao = objVO.Descricao }) != null)
            {
                mensagem = "Sexo no pode ser inserido, pois já outro Sexo cadastrado com essa descrição.";
            }
            return mensagem;
        }

        /// <summary>
        /// Autor: Marcelo Campaner.
        /// Data: 29/09/2014
        /// Descrição: Responsavel por validar os dados de alterção de sexo
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public string ValidarAlteracao(SexoVO objVO)
        {
            string mensagem = null;
            var tur = Consultar(new SexoVO() { Descricao = objVO.Descricao });
            if (tur != null && tur.Id != objVO.Id)
            {
                mensagem = "Sexo no pode ser alterado, ois já outro Sexo cadastrado com essa descrição.";
            }
            return mensagem;
        }

        public List<SexoVO> Selecionar(SexoVO objVO, int top = 0, bool detalhar = false)
        {
            SexoDAO tunroDao = null;
            try
            {
                tunroDao = new SexoDAO(GetSqlCommand());
                return tunroDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SexoVO Consultar(SexoVO objVO)
        {
            SexoDAO tunroDao = null;
            try
            {
                tunroDao = new SexoDAO(GetSqlCommand());
                return tunroDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Dictionary<int, List<SexoVO>> Paginar(string sql, int inicio, int fim)
        {
            SexoDAO sexoDao = null;

            try
            {
                sexoDao = new SexoDAO(GetSqlCommand());
                return sexoDao.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<SexoVO> Listar(SexoVO objVO = null, bool detalhar = false)
        {
            SexoDAO tunroDao = null;
            try
            {
                tunroDao = new SexoDAO(GetSqlCommand());
                return tunroDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}