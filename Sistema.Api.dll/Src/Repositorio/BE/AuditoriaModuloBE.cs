using Sistema.Api.dll.Src.Repositorio.DAO;

namespace Sistema.Api.dll.Src.Repositorio.BE
{
    public class AuditoriaModuloBE : AbstractBE
    {
        /// <summary>
        /// Autor: Michael Lopes
        /// Data: 09.10.2014
        /// Descrição: Resonsavel por atualizar a tabela de auditoria sempre com o modulo atual do usuario
        /// </summary>
        /// <param name="idAuditoria"></param>
        public void Atualizar(long idAuditoria, long idModulo)
        {
            AuditoriaModuloDAO auditoriaModuloDAO = null;
            try
            {
                auditoriaModuloDAO = new AuditoriaModuloDAO(GetSqlCommand());
                auditoriaModuloDAO.Atualizar(idAuditoria, idModulo);

            }
            catch
            {
                throw;
            }
        }
    }
}