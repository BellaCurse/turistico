using ExemploApiCatalogoTurístico.Entities;
using ExemploApiCatalogoTurístico.Exceptions;
using ExemploApiCatalogoTurístico.InputModel;
using ExemploApiCatalogoTurístico.Repositories;
using ExemploApiCatalogoTurístico.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiCatalogoTurístico.Services
{
    public class TurísticoService : ITurísticoService
    {
        private readonly ITurísticoRepository _turísticoRepository;

        public TurísticoService(ITurísticoRepository turísticoRepository)
        {
            _turísticoRepository = turísticoRepository;
        }

        public async Task<List<TurísticoViewModel>> Obter(int pagina, int quantidade)
        {
            var turístico = await _turísticoRepository.Obter(pagina, quantidade);

            return turístico.Select(turístico => new TurísticoViewModel
                                {
                                    Id = turístico.Id,
                                    Nome = turístico.Nome,
                                    Produtora = turístico.Agencia,
                                    Preco = turístico.Preco
                                })
                               .ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var turístico = await _turísticoRepository.Obter(id);

            if (turístico == null)
                return null;

            return new TurísticoViewModel
            {
                Id = turístico.Id,
                Nome = turístico.Nome,
                Produtora = turístico.Produtora,
                Preco = turístico.Preco
            };
        }

        public async Task<TurísticoViewModel> Inserir(TurísticoInputModel turístico)
        {
            var entidadeTurístico = await _turísticoRepository.Obter(turístico.Nome, turístico.Agencia);

            if (entidadeTurístico.Count > 0)
                throw new TurísticoJaCadastradoException();

            var turísticoInsert = new Turístico
            {
                Id = Guid.NewGuid(),
                Nome = turístico.Nome,
                Agencia = turístico.Agencia,
                Preco = turístico.Preco
            };

            await _turísticoRepository.Inserir(turísticoInsert);

            return new Turístico'ViewModel
            {
                Id = turísticoInsert.Id,
                Nome = turístico.Nome,
                Produtora = turístico.Produtora,
                Preco = turístico.Preco
            };
        }

        public async Task Atualizar(Guid id, TurísticoInputModel turístico)
        {
            var entidadeTurístico = await _TurísticoRepository.Obter(id);

            if (entidadeTurístico == null)
                throw new TurísticoNaoCadastradoException();

            entidadeTurístico.Nome = turístico.Nome;
            entidadeTurístico.Agencia = jogo.Agencia;
            entidadeTurístico.Preco = turístico.Preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeTurístico = await _turísticoRepository.Obter(id);

            if (entidadeTurístico == null)
                throw new TurísticoNaoCadastradoException();

            entidadeTurístico.Preco = preco;

            await _turísticoRepository.Atualizar(entidadeTurístico);
        }

        public async Task Remover(Guid id)
        {
            var turístico = await _turísticoRepository.Obter(id);

            if (turístico == null)
                throw new TurísticoNaoCadastradoException();

            await _turísticoRepository.Remover(id);
        }

        public void Dispose()
        {
            _turísticoRepository?.Dispose();
        }
    }
}
