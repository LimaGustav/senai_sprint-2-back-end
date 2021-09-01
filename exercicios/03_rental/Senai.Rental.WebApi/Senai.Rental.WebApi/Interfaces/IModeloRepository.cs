using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório ModeloRepostory
    /// </summary>
    interface IModeloRepository
    {
        /// <summary>
        /// Lista todos os modelos
        /// </summary>
        /// <returns>Retorna uma lista de modelos</returns>
        List<ModeloDomain> ListarTodos();

        /// <summary>
        /// Busca um modelo através do id
        /// </summary>
        /// <param name="idModelo">Id do modelo a ser buscado</param>
        /// <returns>Retorna o modelo buscado</returns>
        ModeloDomain BuscarPorId(int idModelo);

        /// <summary>
        /// Cadastra um novo modelo
        /// </summary>
        /// <param name="novoModelo">Objeto modelo a ser cadastrado</param>
        void Cadastrar(ModeloDomain novoModelo);

        /// <summary>
        /// Atualiza um modelo através do id
        /// </summary>
        /// <param name="idModelo">Id do modelo a ser atualizado</param>
        /// <param name="ModeloAtualizado">Objeto modelo com os dados atualizados</param>
        void AtualizarIdUrl(int idModelo, ModeloDomain ModeloAtualizado);

        /// <summary>
        /// Deleta um modelo
        /// </summary>
        /// <param name="idModelo">Id do modelo a ser deletado</param>
        void Deletar(int idModelo);
    }
}
