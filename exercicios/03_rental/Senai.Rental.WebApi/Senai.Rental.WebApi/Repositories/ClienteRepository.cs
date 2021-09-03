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
    /// Repositório referente a entidade CLIENTE
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// </summary>
        public string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=rental; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idCliente, ClienteDomain clienteAtualizado)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE CLIENTE SET nome = @nome, sobreNome = @sobreNome, cnh = @cnh WHERE idCliente = @idCliente";

                using(SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    // Atribui valor ao parametro @nome
                    cmd.Parameters.AddWithValue("@nome", clienteAtualizado.nome);

                    // Atribui valor ao parametro @sobreNome
                    cmd.Parameters.AddWithValue("sobreNome", clienteAtualizado.sobreNome);

                    // Atribui valor ao parametro @cnh
                    cmd.Parameters.AddWithValue("@cnh", clienteAtualizado.cnh);

                    // Atribui valor ao parametro @idCliente
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ClienteDomain BuscarPorId(int idCliente)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idCliente, nome, sobreNome, cnh FROM CLIENTE C WHERE C.idCliente = @idCliente";

                //Abre a conexão com o banco de dados
                con.Open();

                SqlDataReader rdr;

                using(SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Atribui valor ao parametro @idCliente
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    // rdr recebe o resultado do comando cmd no banco de dados
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        ClienteDomain clienteBuscado = new ClienteDomain()
                        {
                            idCliente = Convert.ToInt32(rdr["idCliente"]),
                            nome = rdr["nome"].ToString(),
                            sobreNome = rdr["sobreNome"].ToString(),
                            cnh = rdr["cnh"].ToString()
                        };
                        return clienteBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(ClienteDomain novoCliente)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert;
                if (novoCliente.cnh != null)
                {
                    //Comando a ser rodado no banco de dados
                    queryInsert = "INSERT INTO CLIENTE (nome,sobreNome,cnh) VALUES(@nome,@sobreNome,@cnh)";
                }
                else
                {
                    queryInsert = "INSERT INTO CLIENTE (nome,sobreNome) VALUES(@nome,@sobreNome)";
                }
                
                //Abre a conexão com o banco de dados
                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    // Atribui valor ao parametro @nome
                    cmd.Parameters.AddWithValue("@nome", novoCliente.nome);

                    // Atribui valor ao parametro @sobreNome
                    cmd.Parameters.AddWithValue("@sobreNome", novoCliente.sobreNome);
                    
                    if (novoCliente.cnh != null)
                    {
                        // Atribui valor ao parametro @cnh
                        cmd.Parameters.AddWithValue("@cnh", novoCliente.cnh);
                        //Execute a query
                    }

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idCliente)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                // Comando a ser rodado no banco de dados
                string queryDelete = "DELETE FROM CLIENTE WHERE idCliente = @idCliente";

                // Abre conexão com o banco de dados
                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Atribui valor ao parametro idCliente
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ClienteDomain> ListarTodos()
        {
            //Declara uma lista de clientes que será retornada no fim
            List<ClienteDomain> listaClientes = new List<ClienteDomain>();

            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string querySelectAll = "SELECT idCliente, nome, sobreNome, ISNULL(cnh,'Não cadastrado') cnh FROM CLIENTE";

                //Abre conexão com o banco de dados
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
                        ClienteDomain cliente = new ClienteDomain()
                        {
                            idCliente = Convert.ToInt32(rdr["idCliente"]),
                            nome = rdr["nome"].ToString(),
                            sobreNome = rdr["sobreNome"].ToString(),
                            cnh = rdr["cnh"].ToString(),
                        };

                        listaClientes.Add(cliente);
                    }
                    return listaClientes;
                }
            }
        }
    }
}
