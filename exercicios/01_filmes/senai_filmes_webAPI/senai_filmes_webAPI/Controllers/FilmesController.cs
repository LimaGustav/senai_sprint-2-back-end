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
    // ex: http://localhost:500/api/filmes
    [Route("api/[controller]")]

    [ApiController]
    public class FilmesController : ControllerBase
    {
        /// <summary>
        /// Instancia do IFilmeRepository que armazena os métodos
        /// </summary>
        private IFilmeRepository _filmeRepository { get; set; }

        /// <summary>
        /// Construtor do filme Controller
        /// </summary>
        public FilmesController() 
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// Lista todos os filmes cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<FilmeDomain> listaFilmes = _filmeRepository.ListarTodos();

            return Ok(listaFilmes);
        }

        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto filme a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            /*            if (novoFilme.idGenero <= 0)
                        {
                            return NotFound(
                                    new
                                    {
                                        mensagem = "Id do gênero inválido!",
                                        error = true
                                    }
                                );

                        }*/

            try
            {
                _filmeRepository.Cadastrar(novoFilme);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        /// <summary>
        /// Atualiza um filme através do id
        /// </summary>
        /// <param name="id">Id do filme a ser atualizado</param>
        /// <param name="filmeAtualizado">Objeto filme com os dados atualizados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateByUrl(int id, FilmeDomain filmeAtualizado)
        {
            // Caso o id for menor que zero, será retornado NotFound
            if (id < 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "id inválido",
                            error = true
                        }

                    );
            }

            // Caso o id for zero e o nome for nulo, será retornado NotFound
            if (id == 0 && filmeAtualizado.nomeFilme == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id e nome vazios",
                            error = true
                        }

                    );
            }

            //Busca um filme pelo id
            FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

            if (filmeBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Filme não encontrado!",
                            error = true
                        }
                    );
            }

            try
            {
                if (filmeAtualizado.idGenero == 0) filmeAtualizado.idGenero = filmeBuscado.idGenero;

                if (filmeAtualizado.nomeFilme == null) filmeAtualizado.nomeFilme = filmeBuscado.nomeFilme;

                _filmeRepository.AtualizarIdUrl(id, filmeAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Delete um Filme atraves do id
        /// </summary>
        /// <param name="id">Id do filme a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _filmeRepository.Deletar(id);

            return StatusCode(204);
        }

        /// <summary>
        /// Busca um filme através do id
        /// </summary>
        /// <param name="id">Id do filme a ser buscado</param>
        /// <returns></returns>
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

            FilmeDomain  filmeBuscado = _filmeRepository.BuscarPorId(id);

            if (filmeBuscado == null)
            {
                return NotFound("Filme não encontrado");
            }

            try
            {
                return Ok(filmeBuscado);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }
    }
}
