using ExemploApiCatalogoTurístico.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemploApiCatalogoTurístico.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<Turístico>> Obter(int pagina, int quantidade);
        Task<Turístico> Obter(Guid id);
        Task<List<Turístico>> Obter(string nome, string agencia);
        Task Inserir(Turístico turístico);
        Task Atualizar(Turístico turístico);
        Task Remover(Guid id);
    }
}
