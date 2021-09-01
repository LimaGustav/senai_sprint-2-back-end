using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using senai_filmes_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// Controller responsável pelos endpoints
/// </summary>
namespace senai_filmes_webAPI.Controllers
{
    //Define que tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Define a rota de uma requisição será no formato dominio/api/controller
    // ex: http://localhost:500/api/generos
    [Route("api/[controller]")]


    [ApiController]
    public class GenerosController : ControllerBase
    {
        /// <summary>
        /// Armazena todos os métodos do GeneroRepository
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        public GenerosController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Lista todos os Gêneros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

            return Ok(listaGeneros);
        }

        /// <summary>
        /// Cadastra um novo Gênero
        /// </summary>
        /// <param name="novoGenero">Objeto novoGenero a ser cadastrado</param>
        /// <returns>Retorna um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            //Chama o método de Cadastrar
            _generoRepository.Cadastrar(novoGenero);

            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza o valor de um gênero através do id
        /// </summary>
        /// <param name="id">Id passado pela url</param>
        /// <param name="generoAtualizado">Objeto gênero com os dados atualizados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateByUrl(int id, GeneroDomain generoAtualizado)
        {
            // Caso o id for menor ou igual a zero, será retornado NotFound
            if (id <= 0 || generoAtualizado.nomeGenero == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "id ou nome inválido",
                            error = true
                        }

                    );
            }

            // Busca o Genero através do id
            GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

            if (generoBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Gênero não encontrado!",
                            error = true
                        }
                    );
            }

            // Atualiza o Gênero
            try
            {
                _generoRepository.AtualizarIdUrl(id, generoAtualizado);
                return NoContent();
            }

            // Mostra o erro caso ocorra
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Atualiza o valor de um gênero através do id passado no Objeto generoAtualizado
        /// </summary>
        /// <param name="generoAtualizado">Objeto com os valores atualizados</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateByBody(GeneroDomain generoAtualizado)
        {
            // Caso o id for menor ou igual a zero, será retornado NotFound
            if (generoAtualizado.idGenero <= 0 || generoAtualizado.nomeGenero == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "id ou nome inválido",
                            error = true
                        }

                    );
            }

            GeneroDomain generoBuscado = _generoRepository.BuscarPorId(generoAtualizado.idGenero);

            if (generoBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Gênero não encontrado!",
                            error = true
                        }
                    );
            }

            try
            {
                _generoRepository.AtualizarIdCorpo(generoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um Gênero
        /// </summary>
        /// <param name="id">Id do gênero a ser deletado, passado pelo usuário</param>
        /// <returns></returns>

        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            //Chama o método Deletar
            _generoRepository.Deletar(id);

            return StatusCode(204);
        }

        /// <summary>
        /// Busca um gênero atravês de seu id
        /// </summary>
        /// <param name="id">id do gênero a ser buscado</param>
        /// <returns>NotFound caso o id não for encntrado e o Gênero buscado caso for encontrado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Caso o id sejá invalido, retorna erro
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "id inválido!",
                            error = true
                        }
                    );
            }

            GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

            if (generoBuscado == null)
            {
                return NotFound("Gênero não encontrado");
            }

            return Ok(generoBuscado);
        }
    }
}
