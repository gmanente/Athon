using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class DiaSemanaBE : AbstractBE, IBE<DiaSemanaVO>
    {
        public DiaSemanaBE()
            : base()
        {
        }

        public DiaSemanaBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(DiaSemanaVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(DiaSemanaVO objVO, string @where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(DiaSemanaVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<DiaSemanaVO> Selecionar(DiaSemanaVO objVO, int top = 0, bool detalhar = false)
        {
            DiaSemanaDAO DiaSemanaDAO = null;
            try
            {
                DiaSemanaDAO = new DiaSemanaDAO(GetSqlCommand());
                return DiaSemanaDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DiaSemanaVO Consultar(DiaSemanaVO objVO)
        {
            DiaSemanaDAO DiaSemanaDAO = null;
            try
            {
                DiaSemanaDAO = new DiaSemanaDAO(GetSqlCommand());
                return DiaSemanaDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<DiaSemanaVO>> Paginar(string sql, int inicio, int fim)
        {
            DiaSemanaDAO DiaSemanaDAO = null;

            try
            {
                DiaSemanaDAO = new DiaSemanaDAO(GetSqlCommand());
                return DiaSemanaDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<DiaSemanaVO> Listar(DiaSemanaVO objVO = null, bool detalhar = false)
        {
            DiaSemanaDAO DiaSemanaDAO = null;
            try
            {
                DiaSemanaDAO = new DiaSemanaDAO(GetSqlCommand());
                return DiaSemanaDAO.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetDiaSemana(DayOfWeek dayOfWeek)
        {
            //Domingo
            if (dayOfWeek == DayOfWeek.Sunday)
                return 1;


            //Segunda
            if (dayOfWeek == DayOfWeek.Monday)
                return 2;

            //Terça
            if (dayOfWeek == DayOfWeek.Tuesday)
                return 3;

            //Quarta
            if (dayOfWeek == DayOfWeek.Wednesday)
                return 4;

            //Quinta
            if (dayOfWeek == DayOfWeek.Thursday)
                return 5;

            //Sexta
            if (dayOfWeek == DayOfWeek.Friday)
                return 6;

            //Sábado
            if (dayOfWeek == DayOfWeek.Saturday)
                return 7;

            return 0;
        }
    }
}