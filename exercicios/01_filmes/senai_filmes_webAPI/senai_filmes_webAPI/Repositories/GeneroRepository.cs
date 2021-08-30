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
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int idGenero, GeneroDomain generoAtualizado)
        {
            throw new NotImplementedException();
        }

        public GeneroDomain BuscarPorId(int idGenero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cadastra um novo gênero
        /// </summary>
        /// <param name="novoGenero">Objeto novoGenero com as informações do genero a ser cadastrado</param>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = $"INSERT INTO GENERO (nomeGenero) VALUES ('{novoGenero.nomeGenero}')";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Deletar(int idGenero)
        {
            throw new NotImplementedException();
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
