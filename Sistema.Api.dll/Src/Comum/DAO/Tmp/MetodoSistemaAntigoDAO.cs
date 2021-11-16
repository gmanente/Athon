using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO.Tmp
{
    /// <summary>
    /// Autor: Marcelo Campaner
    /// Descrição: Classe que Contém metodos de inteligação com o sistema antigo
    /// </summary>
    public class MetodoSistemaAntigoDAO : AbstractDAO
    {
        public MetodoSistemaAntigoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

    }
}
