using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{
    //Define que tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Define a rota de uma requisição será no formato dominio/api/controller
    // ex: http://localhost:5000/api/alugueis
    [Route("api/[controller]")]

    [ApiController]
    public class AlugueisController : ControllerBase
    {
        /// <summary>
        /// Instancia do IAluguelRepository que armazena os métodos
        /// </summary>
        private IAluguelRepository _aluguelRepository { get; set; }

        /// <summary>
        /// Construtor AlugueisController
        /// </summary>
        public AlugueisController()
        {
            _aluguelRepository = new AluguelRepository();
        }

        /// <summary>
        /// Lista todos os alugueis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<AluguelDomain> listaAlugueis = _aluguelRepository.ListarTodos();

            return Ok(listaAlugueis);
        }

        /// <summary>
        /// Busca um registro de aluguel através do id
        /// </summary>
        /// <param name="id">Id do registro de aluguel a ser buscado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            error = true
                        }
                    );
            }

            try
            {
                AluguelDomain alguelBuscado = _aluguelRepository.BuscarPorId(id);

                if (alguelBuscado == null)
                {
                    return NotFound(
                            new
                            {
                                mensagem = "Aluguel não encontrado",
                                error = true
                            }
                        );
                }

                return Ok(alguelBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }


        }

        /// <summary>
        /// Cadastra um novo registro de aluguel
        /// </summary>
        /// <param name="novoAluguel">Objeto aluguel a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AluguelDomain novoAluguel)
        {
            if (novoAluguel.idVeiculo == 0 || novoAluguel.idCliente == 0 || novoAluguel.dataRetirada == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Dados incorretos ou incompletos!",
                            error = true
                        }
                    );
            }

            try
            {
                _aluguelRepository.Cadastrar(novoAluguel);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByUrl(int id, AluguelDomain aluguelAtualizado)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido!",
                            error = true
                        }
                    );
            }

            if (aluguelAtualizado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Dados insuficientes",
                            error = true
                        }
                    );
            }

            // Busca um registro de aluguel pelo id
            AluguelDomain aluguelBuscado = _aluguelRepository.BuscarPorId(id);

            if (aluguelBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Registro de aluguel não encontrado!",
                            error = true
                        }
                    );
            }

            try
            {
                if (aluguelBuscado.dataDevolucao == Convert.ToDateTime("2001-01-01 00:00:00")) aluguelBuscado.dataDevolucao = null;

                if (aluguelAtualizado.idVeiculo == 0) aluguelAtualizado.idVeiculo = aluguelBuscado.idVeiculo;

                if (aluguelAtualizado.idCliente == 0) aluguelAtualizado.idCliente = aluguelBuscado.idCliente;

                if (aluguelAtualizado.dataRetirada == null) aluguelAtualizado.dataRetirada = aluguelBuscado.dataRetirada;

                if (aluguelAtualizado.dataDevolucao == null) aluguelAtualizado.dataDevolucao = aluguelBuscado.dataDevolucao;

                _aluguelRepository.AtualizarIdUrl(id, aluguelAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um registro de aluguel através do id
        /// </summary>
        /// <param name="id">Id do registro de aluguel a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            error = true
                        }
                    );
            }

            try
            {
                _aluguelRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

    }
}
