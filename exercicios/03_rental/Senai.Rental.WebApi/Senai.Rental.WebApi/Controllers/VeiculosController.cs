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
    // ex: http://localhost:5000/api/veiculos
    [Route("api/[controller]")]

    [ApiController]
    public class VeiculosController : ControllerBase
    {
        /// <summary>
        /// Instancia do IVeiculoRepository que armazena os métodos
        /// </summary>
        private IVeiculoRepository _veiculoRepository { get; set; }

        /// <summary>
        /// Construtor VeiculosController
        /// </summary>
        public VeiculosController()
        {
            _veiculoRepository = new VeiculoRepository();
        }

        /// <summary>
        /// Lista todos os veiculos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<VeiculoDomain> listaVeiculos = _veiculoRepository.ListarTodos();

            return Ok(listaVeiculos);
        }

        /// <summary>
        /// Busca um veiculo a partir do se id
        /// </summary>
        /// <param name="id">Id do veiculo a ser buscado</param>
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
                VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(id);

                if (veiculoBuscado == null)
                {
                    return NotFound(
                            new
                            {
                                mensagem = "Veiculo não encontrado",
                                error = true
                            }
                        );
                }

                return Ok(veiculoBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Cadastra um novo veiculo
        /// </summary>
        /// <param name="novoVeiculo">Objeto veiculo a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(VeiculoDomain novoVeiculo)
        {
            if (novoVeiculo.idEmpresa == 0 || novoVeiculo.idModelo == 0 || novoVeiculo.placa == null)
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
                _veiculoRepository.Cadastrar(novoVeiculo);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um veiculo através do id
        /// </summary>
        /// <param name="id">Id do veiculo a ser deletado</param>
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
                _veiculoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateByUrl(int id, VeiculoDomain veiculoAtualizado)
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

            if (veiculoAtualizado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Dados insuficientes",
                            error = true
                        }
                    );
            }

            //Busca veiculo pelo id
            VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(id);

            if (veiculoBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Veiculo não encontrado!",
                            error = true
                        }
                    );
            }

            try
            {
                if (veiculoAtualizado.idVeiculo == 0) veiculoAtualizado.idVeiculo = veiculoBuscado.idVeiculo;

                if (veiculoAtualizado.idEmpresa == 0) veiculoAtualizado.idEmpresa = veiculoBuscado.idEmpresa;

                if (veiculoAtualizado.idModelo == 0) veiculoAtualizado.idModelo = veiculoBuscado.idModelo;

                if (veiculoBuscado.placa == null) veiculoAtualizado.placa = veiculoBuscado.placa;

                _veiculoRepository.AtualizarIdUrl(id, veiculoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

    }
}
