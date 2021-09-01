using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String com as informações para conectar no servidor
        /// </summary>
        private string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=catalogo; user Id=sa; pwd=senai@132";

        public void AtualizarIdCorpo(GeneroDomain generoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateBody = "UPDATE GENERO SET nomeGenero = @nomeGenero WHERE idGenero = @idGenero";

                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    cmd.Parameters.AddWithValue("@nomeGenero", generoAtualizado.nomeGenero);

                    cmd.Parameters.AddWithValue("idGenero", generoAtualizado.idGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int idGenero, GeneroDomain generoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE GENERO SET nomeGenero = @nomeGenero WHERE idGenero = @idGenero";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeGenero", generoAtualizado.nomeGenero);

                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public GeneroDomain BuscarPorId(int idGenero)
        {

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idGenero, nomeGenero FROM GENERO WHERE idGenero = @idGenero";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        GeneroDomain generoBuscado = new GeneroDomain()
                        {
                            idGenero = Convert.ToInt32(rdr[0]),
                            nomeGenero = rdr[1].ToString()
                        };

                        return generoBuscado;
                    }
                }
            }
            return null;
        }
        
        public void Cadastrar(GeneroDomain novoGenero)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO GENERO (nomeGenero) VALUES (@nomeGenero)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                    // Atribui valor ao parametro @nomeGenero
                    cmd.Parameters.AddWithValue("@nomeGenero", novoGenero.nomeGenero);

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Deletar(int idGenero)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM GENERO WHERE idGenero = @idGenero";

                using(SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Atribiu valor ao parametro @idGenero
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

       
        public List<GeneroDomain> ListarTodos()
        {
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string querySelectAll = "SELECT idGenero, nomeGenero FROM GENERO";

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
                        //Cria o objeto GeneroDomain com os atributos já alocados
                        GeneroDomain genero = new GeneroDomain()
                        {
                            idGenero = Convert.ToInt32(rdr[0]),
                            nomeGenero = (rdr[1]).ToString()
                        };

                        listaGeneros.Add(genero);
                    }
                };
            };
            return listaGeneros;
        }
    }
}
