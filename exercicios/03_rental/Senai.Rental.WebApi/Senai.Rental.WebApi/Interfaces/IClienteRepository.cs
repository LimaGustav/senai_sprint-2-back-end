using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório ClienteRepository
    /// </summary>
    interface IClienteRepository
    {
        /// <summary>
        /// Lista todas os cliente
        /// </summary>
        /// <returns>Retorna uma lista de clientes</returns>
        List<ClienteDomain> ListarTodos();

        /// <summary>
        /// Busca um cliente através do id
        /// </summary>
        /// <param name="idCliente">Id do cliente a ser buscado</param>
        /// <returns>Retorna o cliente buscado</returns>
        ClienteDomain BuscarPorId(int idCliente);

        /// <summary>
        /// Busca um cliente através do nome
        /// </summary>
        /// <param name="nomeCliente">Nome do cliente a ser buscado</param>
        /// <returns></returns>
        ClienteDomain BuscarPorNome(string nomeCliente);

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        /// <param name="novoCliente">Objeto cliente a ser cadastrado</param>
        /// 
        void Cadastrar(ClienteDomain novoCliente);

        /// <summary>
        /// Atualiza um cliente através do id
        /// </summary>
        /// <param name="idCliente">Id do cliente a ser atualizado</param>
        /// <param name="clienteAtualizado">Objeto cliente com os dados atualizados</param>
        void AtualizarIdUrl(int idCliente, ClienteDomain clienteAtualizado);

        /// <summary>
        /// Deleta um cliente através do id
        /// </summary>
        /// <param name="idCliente">Id do cliente a ser deletado</param>
        void Deletar(int idCliente);
    }
}
