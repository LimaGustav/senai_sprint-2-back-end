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
    // ex: http://localhost:5000/api/clientes
    [Route("api/[controller]")]

    [ApiController]
    public class ClientesController : ControllerBase
    {
        /// <summary>
        /// Instancia do IClienteRepository que armazena os métodos
        /// </summary>
        private IClienteRepository _clienteRepository { get; set; }

        /// <summary>
        /// Construtor ClientesController
        /// </summary>
        public ClientesController()
        {
            _clienteRepository = new ClienteRepository();
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<ClienteDomain> listaClientes = _clienteRepository.ListarTodos();

            return Ok(listaClientes);
        }

        /// <summary>
        /// Busca um cliente através do id
        /// </summary>
        /// <param name="id">Id do cliente a ser buscado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "id inválido",
                            error = true
                        }
                    );
            }

            try
            {
                ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(id);

                if (clienteBuscado == null)
                {
                    return NotFound(
                            new
                            {
                                mensagem = "Cliente não encontrado",
                                error = true
                            }
                        );
                }

                return Ok(clienteBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }

        /// <summary>
        /// Cadastra um novo Cliente
        /// </summary>
        /// <param name="novoCliente">Objeto cliente a ser cadastrado (JSON)</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ClienteDomain novoCliente)
        {
            if (novoCliente.nome == null || novoCliente.sobreNome == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Nome ou sobrenome do cliente vazio!",
                            error = true
                        }
                    );
            }

            // Chama o método cadastrar
            try
            {
                _clienteRepository.Cadastrar(novoCliente);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
            
        }

        /// <summary>
        /// Deleta um cliente
        /// </summary>
        /// <param name="id">Id do cliente a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "id inválido",
                            error = true
                        }
                    );
            }

            try
            {
                //Chama o método Deletar
                _clienteRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
            
        }

        /// <summary>
        /// Atualiza um cliente através do id
        /// </summary>
        /// <param name="id">Id do cliente a ser atualizado</param>
        /// <param name="clienteAtualizado">Objeto clientes com os dados atualizados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateIdUrl(int id, ClienteDomain clienteAtualizado)
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

            // Busca um filme pelo id
            ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(id);

            if (clienteBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Cliente não encontrado!",
                            error = true
                        }
                    );
            }

            try
            {
                if (clienteAtualizado.nome == null) clienteAtualizado.nome = clienteBuscado.nome;

                if (clienteAtualizado.sobreNome == null) clienteAtualizado.sobreNome = clienteBuscado.sobreNome;

                if (clienteAtualizado.cnh == null) clienteAtualizado.cnh = clienteBuscado.cnh;

                _clienteRepository.AtualizarIdUrl(id, clienteAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

    }
}
