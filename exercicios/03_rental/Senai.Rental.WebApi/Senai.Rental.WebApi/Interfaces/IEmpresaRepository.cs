using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório EmpresaRepository
    /// </summary>
    interface IEmpresaRepository
    {
        /// <summary>
        /// Lista todas as empresas
        /// </summary>
        /// <returns>Retorna uma lisa de empresas</returns>
        List<EmpresaDomain> ListarTodos();

        /// <summary>
        /// Busca uma empresa através do id
        /// </summary>
        /// <param name="idEmpresa">Id da empresa a ser buscada</param>
        /// <returns>Retorna a empresa buscada</returns>
        EmpresaDomain BuscarPorId(int idEmpresa);

        /// <summary>
        /// Cadastra uma nova empresa
        /// </summary>
        /// <param name="novaEmpresa">Objeto empresa a ser cadastrado</param>
        void Cadastrar(EmpresaDomain novaEmpresa);

        /// <summary>
        /// Atualiza um registro de empresa através do id
        /// </summary>
        /// <param name="idEmpresa">Id da empresa a ser atualizada</param>
        /// <param name="empresaAtualizada">Objeto empresa com os dados atualizados</param>
        void AtualizarIdUrl(int idEmpresa, EmpresaDomain empresaAtualizada);

        /// <summary>
        /// Deleta uma empresa através do id
        /// </summary>
        /// <param name="idEmpresa">Id da empresa a ser deletada</param>
        void Deletar(int idEmpresa);
    }
}
