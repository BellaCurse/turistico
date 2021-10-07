using ExemploApiCatalogoTurístico.InputModel;
using ExemploApiCatalogoTurístico.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemploApiCatalogoTurístico.Services
{
    public interface ITurísticoService : IDisposable
    {
        Task<List<TurísticoViewModel>> Obter(int pagina, int quantidade);
        Task<TurísticoViewModel> Obter(Guid id);
        Task<TurísticoViewModel> Inserir(TurísticoInputModel turístico);
        Task Atualizar(Guid id, TurísticoInputModel turístico);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
