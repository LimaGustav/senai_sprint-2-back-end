using senai_pessoas_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_pessoas_WebAPI.Interfaces
{
    /// <summary>
    /// Interface do PessoaRepository (Contrato)
    /// </summary>
    interface IPessoaRepository
    {
        /// <summary>
        /// Lista todas as pessoas
        /// </summary>
        /// <returns>Retorna uma lista de Pessoas (PessoaDomain)</returns>
        List<PessoaDomain> ListarTodos();

        /// <summary>
        /// Busca uma pessoa através do id
        /// </summary>
        /// <param name="idPessoa">Id da pessoa a ser buscada</param>
        /// <returns>Retorna uma pessoa (PessoaDomain)</returns>
        PessoaDomain BuscarPorId(int idPessoa);

        /// <summary>
        /// Cadastra uma nova pessoa
        /// </summary>
        /// <param name="novaPessoa">Objeto PessoaDomain a ser cadastrado</param>
        void Cadastrar(PessoaDomain novaPessoa);

        /// <summary>
        /// Atualiza os dados de uma pessoa. Id passado no Objeto PessoaAtualizada
        /// </summary>
        /// <param name="pessoaAtualizada">Objeto PessoaDomain com os dados atualizados</param>
        void AtualizarIdCorpo(PessoaDomain pessoaAtualizada);

        /// <summary>
        /// Atualiza os dados de uma pessoa. Id passado no URL
        /// </summary>
        /// <param name="id">Id da pessoa a ser atualizada</param>
        /// <param name="PessoaAtualizada">Objeto PessoaDomain com os dados atualizados</param>
        void AtualizarIdUrl(int id, PessoaDomain PessoaAtualizada);

        /// <summary>
        /// Deleta uma pessoa
        /// </summary>
        /// <param name="idPessoa">Id da pessoa a ser deletada</param>
        void Deletar(int idPessoa);
    }
}