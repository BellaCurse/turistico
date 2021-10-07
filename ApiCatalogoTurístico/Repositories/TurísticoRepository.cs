using ExemploApiCatalogoTurístico.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiCatalogoTurístico.Repositories
{
    public class TurísticoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Turístico> turístico = new Dictionary<Guid, Turístico>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Turístico{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Fifa 21", Produtora = "EA", Preco = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Turísstico{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Fifa 20", Produtora = "EA", Preco = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Turístico{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Fifa 19", Produtora = "EA", Preco = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Turístico{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Fifa 18", Produtora = "EA", Preco = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Turístico{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Turístico{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task<List<Turístico>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(turístico.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Turístico> Obter(Guid id)
        {
            if (!turístico.ContainsKey(id))
                return Task.FromResult<Turístico>(null);

            return Task.FromResult(turístico[id]);
        }

        public Task<List<Turístico>> Obter(string nome, string agencia)
        {
            return Task.FromResult(turístico.Values.Where(turístico => turístico.Nome.Equals(nome) && turístico.Agencia.Equals(agencia)).ToList());
        }

        public Task<List<Turístico>> ObterSemLambda(string nome, string agencia)
        {
            var retorno = new List<Vitrine>();

            foreach(var vitrine in vitrine.Values)
            {
                if (vitrine.Nome.Equals(nome) && turístico.Agencia.Equals(agencia))
                    retorno.Add(turístico);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Turístico turístico)
        {
            turístico.Add(turístico.Id, turístico);
            return Task.CompletedTask;
        }

        public Task Atualizar(Turístico turístico)
        {
            turístico[turístico.Id] = turístico;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            turístico.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
