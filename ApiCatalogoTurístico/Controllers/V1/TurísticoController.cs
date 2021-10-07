using ExemploApiCatalogoTurístico.Exceptions;
using ExemploApiCatalogoTurístico.InputModel;
using ExemploApiCatalogoTurístico.Services;
using ExemploApiCatalogoTurístico.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiCatalogoTurístico.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly ITurísticoService _turísticoService;

        public JogosController(ITurísticoService turísticoService)
        {
            _turísticoService = turísticoService;
        }

        /// <summary>
        /// Buscar todos os lugares turísticos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os lugares turísticos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de lugares turísticos</response>
        /// <response code="204">Caso não haja lugares turíticos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurísticoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var turístico = await _turísticoService.Obter(pagina, quantidade);

            if (turístico.Count() == 0)
                return NoContent();

            return Ok(turístico);
        }

        /// <summary>
        /// Buscar um lugar turístico pelo seu Id
        /// </summary>
        /// <param name="idTurístico">Id do lugar turístico buscado</param>
        /// <response code="200">Retorna o lugar turístico filtrado</response>
        /// <response code="204">Caso não haja lugar turístico com este id</response>   
        [HttpGet("{idTurístico:guid}")]
        public async Task<ActionResult<TurísticoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var turístico = await _turísticoService.Obter(idTurístico);

            if (turístico == null)
                return NoContent();

            return Ok(turístico);
        }

        /// <summary>
        /// Inserir um lugar turístico no catálogo
        /// </summary>
        /// <param name="turísticoInputModel">Dados do lugar turístico a ser inserido</param>
        /// <response code="200">Caso o lugar turístico seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um lugar turístico com mesmo nome para a mesma agencia</response>   
        [HttpPost]
        public async Task<ActionResult<TurísticoViewModel>> InserirTurístico([FromBody] TurísticoInputModel jogoInputModel)
        {
            try
            {
                var turístico = await _turísticoService.Inserir(turíticoInputModel);

                return Ok(turístico);
            }
            catch (TurísticoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um lugar turístico com este nome para esta agencia");
            }
        }

        /// <summary>
        /// Atualizar um lugar turístico no catálogo
        /// </summary>
        /// /// <param name="idTurístico">Id do lugar turístico a ser atualizado</param>
        /// <param name="turísticoInputModel">Novos dados para atualizar o lugar turístico indicado</param>
        /// <response code="200">Caso o lugar turístico seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um lugar turístico com este Id</response>   
        [HttpPut("{idTurístico:guid}")]
        public async Task<ActionResult> AtualizarTurístico([FromRoute] Guid idTurístico, [FromBody] TurísticoInputModel turísticoInputModel)
        {
            try
            {
                await _turísticoService.Atualizar(idTurístico, turísticoInputModel);

                return Ok();
            }
            catch (Turístico'NaoCadastradoException ex)
            {
                return NotFound("Não existe este turístico");
            }
        }

        /// <summary>
        /// Atualizar o preço de um lugar Turístico
        /// </summary>
        /// /// <param name="idTurístico">Id do lugar turístico a ser atualizado</param>
        /// <param name="preco">Novo preço do lugar turístico</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um lugar turístico com este Id</response>   
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarTurístico([FromRoute] Guid idTurístico, [FromRoute] double preco)
        {
            try
            {
                await _turísticoService.Atualizar(idTurístico, preco);

                return Ok();
            }
            catch (TurísticoNaoCadastradoException ex)
            {
                return NotFound("Não existe este lugar turístico");
            }
        }

        /// <summary>
        /// Excluir um lugar turístico
        /// </summary>
        /// /// <param name="idTurístico">Id do lugar turístico a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um lugar turístico com este Id</response>   
        [HttpDelete("{idTurístico:guid}")]
        public async Task<ActionResult> ApagarTurístico([FromRoute] Guid idTurístico)
        {
            try
            {
                await _turísticoService.Remover(idTurístico);

                return Ok();
            }
            catch (TurísticoNaoCadastradoException ex)
            {
                return NotFound("Não existe este lugar turístico");
            }
        }

    }
}
