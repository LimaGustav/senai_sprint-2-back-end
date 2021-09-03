using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    /// <summary>
    /// Repositório referente a entidade EMPRESA
    /// </summary>
    public class EmpresaRepository : IEmpresaRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// </summary>
        public string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=rental; user Id=sa; pwd=senai@132";
        public void AtualizarIdUrl(int idEmpresa, EmpresaDomain empresaAtualizada)
        {
            throw new NotImplementedException();
        }

        public EmpresaDomain BuscarPorId(int idEmpresa)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(EmpresaDomain novaEmpresa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idEmpresa)
        {
            throw new NotImplementedException();
        }

        public List<EmpresaDomain> ListarTodos()
        {
            //Declara uma lista de empresas que será retornada no fim
            List<EmpresaDomain> listaEmpresas = new List<EmpresaDomain>();

            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string querySelectAll = "SELECT idEmpresa, nomeEmpresa FROM EMPRESA";

                //Abre conexão como banco de dados.
                con.Open();

                // Declara uma SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara um SqlCommand passando a query e a conexão como parametros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registro para serem lidos no rdr o laço se repete
                    while (rdr.Read())
                    {
                        // Cria objeto EmpresaDomain com os atributos já alocados
                        EmpresaDomain empresa = new EmpresaDomain()
                        {
                            idEmpresa = Convert.ToInt32(rdr[0]),
                            nomeEmpresa = rdr[1].ToString()
                        };

                        listaEmpresas.Add(empresa);
                    }
                    return listaEmpresas;
                }
            }


        }
    }
}
