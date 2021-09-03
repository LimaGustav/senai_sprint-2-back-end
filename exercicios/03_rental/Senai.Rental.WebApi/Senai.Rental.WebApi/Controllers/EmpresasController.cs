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
    // ex: http://localhost:5000/api/empresas
    [Route("api/[controller]")]

    [ApiController]
    public class EmpresasController : ControllerBase
    {
        /// <summary>
        /// Instancia do IEmpresaRepository que armazena os métodos
        /// </summary>
        private IEmpresaRepository _empresaRepository { get; set; }

        /// <summary>
        /// Construtor EmpresaController
        /// </summary>
        public EmpresasController()
        {
            _empresaRepository = new EmpresaRepository();
        }

        /// <summary>
        /// Lista todas as empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<EmpresaDomain> listaEmpresas = _empresaRepository.ListarTodos();

            return Ok(listaEmpresas);
        }
    }
}
