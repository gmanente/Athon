using Sistema.Api.dll.Src.CarteirinhaAluno.DAO;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using System;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.BE
{
    public class FuncionarioFotoLogBE : AbstractBE
    {
        public FuncionarioFotoLogBE(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public FuncionarioFotoLogBE()
            : base()
        { }

        public void InserirLista(long[] arrIdFotoAluno, long IdUsuario, DateTime dataImpressao)
        {
            try
            {
                BeginTransaction();

                var funcionarioFotoLogDAO = new FuncionarioFotoLogDAO(GetSqlCommand());
                var alunoFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());

                foreach (var item in arrIdFotoAluno)
                {
                    var itemFuncionarioFotoLogVO = new FuncionarioFotoLogVO();
                    itemFuncionarioFotoLogVO.FuncionarioFoto.Id = item;
                    itemFuncionarioFotoLogVO.Usuario.Id = IdUsuario;
                    itemFuncionarioFotoLogVO.DataImpressao = dataImpressao;

                    funcionarioFotoLogDAO.Inserir(itemFuncionarioFotoLogVO);

                    var itemFuncionarioFotoVO = new FuncionarioFotoVO();
                    itemFuncionarioFotoVO.Id = item;

                    alunoFotoDAO.AlterarNrVia(itemFuncionarioFotoVO);
                }

                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

    }
}
