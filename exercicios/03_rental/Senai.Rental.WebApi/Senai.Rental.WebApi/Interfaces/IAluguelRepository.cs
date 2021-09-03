using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório AluguelRespository
    /// </summary>
    interface IAluguelRepository
    {
        /// <summary>
        /// Lista todos os alugueis
        /// </summary>
        /// <returns>Retorna uma lista de alugueis</returns>
        List<AluguelDomain> ListarTodos();

        /// <summary>
        /// Busca um alguel através do id
        /// </summary>
        /// <param name="idAlguel">Id do alguel a ser buscado</param>
        /// <returns></returns>
        AluguelDomain BuscarPorId(int idAlguel);

        /// <summary>
        /// Cadastra uma novo aluguel
        /// </summary>
        /// <param name="novoAluguel">Objeto alguel com os dados a serem cadastrados</param>
        void Cadastrar(AluguelDomain novoAluguel);

        /// <summary>
        /// Atualiza um registro de aluguel através do id
        /// </summary>
        /// <param name="idAlguel">Id do aluguel a ser atualizado</param>
        /// <param name="aluguelAtualizado">Objeto aluguel com os dados atualizados</param>
        void AtualizarIdUrl(int idAlguel, AluguelDomain aluguelAtualizado);

        /// <summary>
        /// Deleta um registro de aluguel através do id
        /// </summary>
        /// <param name="idAlguel">Id do registro de aluguel a ser deletado</param>
        void Deletar(int idAlguel);
    }
}
