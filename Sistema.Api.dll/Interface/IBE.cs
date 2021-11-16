using System.Collections.Generic;

namespace Sistema.Api.dll.Interface
{
    public interface IBE<T> where T : new()
    {
        List<T> Selecionar(T objVO, int top = 0, bool detalhar = false);
        List<T> Listar(T objVO, bool detalhar = false);
        T Consultar(T objVO);
        long Inserir(T objVO);
        long Alterar(T objVO, string where = null);
        void Deletar(T objVO);
    }
}