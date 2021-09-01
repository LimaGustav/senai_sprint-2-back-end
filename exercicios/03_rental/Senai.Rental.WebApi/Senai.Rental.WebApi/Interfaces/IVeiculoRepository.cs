using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório VeiculoRepository
    /// </summary>
    interface IVeiculoRepository
    {
        /// <summary>
        /// Lista todos os veículos
        /// </summary>
        /// <returns>Retorna uma lista de veículos</returns>
        List<VeiculoDomain> ListarTodos();

        /// <summary>
        /// Busca um veiculo através do id
        /// </summary>
        /// <param name="idVeiculo">Id do veiculo a ser buscado</param>
        /// <returns>Retorna o veiculo buscado</returns>
        VeiculoDomain BuscarPorId(int idVeiculo);

        /// <summary>
        /// Cadastra um novo veiculo
        /// </summary>
        /// <param name="novoVeiculo">Objeto veiculo a ser cadastrado</param>
        void Cadastrar(VeiculoDomain novoVeiculo);
    }
}
