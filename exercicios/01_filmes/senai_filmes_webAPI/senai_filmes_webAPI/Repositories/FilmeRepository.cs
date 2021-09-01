using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        /// <summary>
        /// String com as informações para conectar no servidor
        /// </summary>
        private string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=catalogo; user Id=sa; pwd=senai@132";
        public void AtualizarIdCorpo(FilmeDomain filmeAtualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int idFilme, FilmeDomain filmeAtualizado)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE FILME SET idGenero = @idGenero, nomeFilme = @nomeFilme WHERE idFilme = @idFilme";

                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {

                    cmd.Parameters.AddWithValue("@idGenero", filmeAtualizado.idGenero);

                    cmd.Parameters.AddWithValue("@nomeFilme", filmeAtualizado.nomeFilme);

                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FilmeDomain BuscarPorId(int idFilme)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idFilme, ISNULL(F.idGenero, 0) idGenero, nomeFilme, ISNULL(G.nomeGenero,'Não Cadastrado') 'nomeGenero' FROM FILME F LEFT JOIN GENERO G ON F.idGenero = G.idGenero WHERE F.idFilme = @idFilme";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filmeBuscado = new FilmeDomain()
                        {
                            idFilme = Convert.ToInt32(rdr["idFilme"]),
                            idGenero = Convert.ToInt32(rdr["idGenero"]),
                            nomeFilme = rdr["nomeFilme"].ToString(),
                            genero = new GeneroDomain()
                            {
                                idGenero = Convert.ToInt32(rdr["idGenero"]),
                                nomeGenero = rdr["nomeGenero"].ToString(),
                            }
                        };
                        return filmeBuscado;
                    }
                }
                return null;
            }
        }

        public void Cadastrar(FilmeDomain novoFilme)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert;
                if (novoFilme.idGenero > 0)
                {
                    queryInsert = "INSERT INTO FILME (idGenero, nomeFilme) VALUES (@idGenero, @nomeFilme)";
                }
                else 
                {
                    queryInsert = $"INSERT INTO FILME (nomeFilme) VALUES (@nomeFilme)";
                }
                

                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryInsert,con))
                {
                    // Atribui o nome do filme no parametro @nomeFilme
                    cmd.Parameters.AddWithValue("@nomeFilme", novoFilme.nomeFilme);

                    // Atribui o id do Gênero caso especificado
                    if (novoFilme.idGenero > 0)
                    {
                        cmd.Parameters.AddWithValue("@idGenero", novoFilme.idGenero);
                    }

                    //Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idFilme)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                //Estabelece o comando que vai ser rodado dentro do banco de dados
                string queryDelete = "DELETE FROM FILME WHERE idFilme = @idFilme";

                using(SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Adiciona valor ao @idFilmes
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }


            }
        }

        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string querySelectAll = "SELECT idFilme, isnull(FILME.idGenero,0) idGenero, nomeFilme, isnull(nomeGenero,'Não Cadastrado') 'nome do genero' FROM FILME LEFT JOIN GENERO ON FILME.idGenero = GENERO.idGenero";

                //abre conexão com o banco de dados
                con.Open();

                //Declara um SqlDataReader para percorrer o banco de dados
                SqlDataReader rdr;

                //Declara um SqlCommand passando a query e a conexão como parametros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registro para serem lidos no rdr o laço se repete
                    while (rdr.Read())
                    {
                        //Cria o objeto FilmeDomain com os atributos já alocados
                        FilmeDomain filme = new FilmeDomain()
                        {
                            idFilme = Convert.ToInt32(rdr[0]),
                            idGenero = Convert.ToInt32(rdr[1]),
                            nomeFilme = rdr[2].ToString(),
                            genero = new GeneroDomain()
                            {
                                idGenero = Convert.ToInt32(rdr[1]),
                                nomeGenero = (rdr[3]).ToString()
                            }

                    };

                        listaFilmes.Add(filme);
                    }
                }
            }
            return listaFilmes;
        }
    }
}
