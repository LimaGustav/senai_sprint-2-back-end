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
        /// Cadastra um novo registro de aluguel
        /// </summary>
        /// <param name="novoAluguel">Objeto aluguel a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AluguelDomain novoAluguel)
        {

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
