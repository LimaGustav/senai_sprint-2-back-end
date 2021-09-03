using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório MarcaRepository
    /// </summary>
    interface IMarcaRepository
    {
        /// <summary>
        /// Lista todas as marcas
        /// </summary>
        /// <returns>Retorna uma lista de marcas</returns>
        List<MarcaDomain> ListarTodos();

        /// <summary>
        /// Busca uma marca através do id
        /// </summary>
        /// <param name="idMarca">Id da marca a ser buscada</param>
        /// <returns>Retorna a marca buscada</returns>
        MarcaDomain BuscarPorId(int idMarca);

        /// <summary>
        /// Cadastra uma nova marca
        /// </summary>
        /// <param name="novaMarca">Objeto marca a ser cadastrado</param>
        void Cadastrar(MarcaDomain novaMarca);

        /// <summary>
        /// Atualiza uma marca através do id
        /// </summary>
        /// <param name="idMarca">Id da marca a ser atualizada</param>
        /// <param name="marcaAtualizada">Objeto marca com os dados atualizados</param>
        void AtualizarIdUrl(int idMarca, MarcaDomain marcaAtualizada);

        /// <summary>
        /// Deleta uma marca através do id
        /// </summary>
        /// <param name="idMarca">Id da marca a ser deletada</param>
        void Deletar(int idMarca);
    }
}
