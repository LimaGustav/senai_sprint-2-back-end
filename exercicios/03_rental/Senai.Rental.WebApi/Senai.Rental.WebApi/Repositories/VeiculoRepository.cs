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
    /// Repositório referente a entidade VEICULO
    /// </summary>
    public class VeiculoRepository : IVeiculoRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// </summary>
        public string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=rental; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idVeiculo, VeiculoDomain veiculoAtualizado)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE VEICULO SET idEmpresa = @idEmpresa, idModelo = @idModelo, placa = @placa WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    // Atribui valor ao parametro @idEmpresa
                    cmd.Parameters.AddWithValue("@idEmpresa", veiculoAtualizado.idEmpresa);

                    // Atribui valor ao parametro @idModelo
                    cmd.Parameters.AddWithValue("@idModelo", veiculoAtualizado.idModelo);

                    // Atribui valor ao parametro @placa
                    cmd.Parameters.AddWithValue("@placa", veiculoAtualizado.placa);

                    // Atribui valor ao parametro @idVeiculo
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    // Abre conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public VeiculoDomain BuscarPorId(int idVeiculo)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idVeiculo, ISNULL(V.idEmpresa, 0) idEmpresa, ISNULL(V.idModelo, 0) idModelo, placa, ISNULL(nomeEmpresa,'Não cadastradado') nomeEmpresa, ISNULL(nomeModelo,'Não cadastradado') nomeModelo, ISNULL(M.idMarca,0)idMarca, ISNULL(nomeMarca,'Não cadastradado') nomeMarca FROM VEICULO V LEFT JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa LEFT JOIN MODELO M ON V.idModelo = M.idModelo LEFT JOIN MARCA MAR ON M.idMarca = MAR.idMarca WHERE V.idVeiculo = @idVeiculo";

                //Declara uma SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara um SqlCommand passando a query e a conexão como parametros
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    //Abre conexão com o bando de dados
                    con.Open();

                    //Atribui valor ao parametro @idVeiculo
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    rdr = cmd.ExecuteReader();

                    // Caso for encontrado um veiculo com o id passado no argumento
                    if (rdr.Read())
                    {
                        VeiculoDomain novoVeiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                            idModelo = Convert.ToInt32(rdr["idModelo"]),
                            placa = rdr["placa"].ToString(),
                            empresa = new EmpresaDomain()
                            {
                                idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                nomeEmpresa = rdr["nomeEmpresa"].ToString(),
                            },
                            modelo = new ModeloDomain()
                            {
                                idModelo = Convert.ToInt32(rdr["idModelo"]),
                                idMarca = Convert.ToInt32(rdr["idMarca"]),
                                nomeModelo = rdr["nomeModelo"].ToString(),
                                marca = new MarcaDomain()
                                {
                                    idMarca = Convert.ToInt32(rdr["idMarca"]),
                                    nomeMarca = rdr["nomeMarca"].ToString(),
                                }

                            }
                        };
                        return novoVeiculo;
                    }
                    //Caso não for encontrado nenhum veiculo com o id passado no argumento
                    return null;
                }
            }
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string queryInsert = "INSERT INTO VEICULO (idEmpresa,idModelo,placa) VALUES (@idEmpresa, @idModelo, @placa)";

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Atribui valor ao parametro @idEmpresa
                    cmd.Parameters.AddWithValue("@idEmpresa", novoVeiculo.idEmpresa);

                    // Atribui valor ao parametro @idModelo
                    cmd.Parameters.AddWithValue("@idModelo", novoVeiculo.idModelo);

                    // Atribui valor ao parametro @placa
                    cmd.Parameters.AddWithValue("@placa", novoVeiculo.placa);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idVeiculo)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string queryDelete = "DELETE FROM VEICULO  WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Atribui valor ao parametro @idVeiculo
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    // Abre conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> listaVeiculos = new List<VeiculoDomain>();

            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idVeiculo, ISNULL(V.idEmpresa, 0) idEmpresa, ISNULL(V.idModelo, 0) idModelo, placa, ISNULL(nomeEmpresa,'Não cadastradado') nomeEmpresa, ISNULL(nomeModelo,'Não cadastradado') nomeModelo, ISNULL(M.idMarca,0)idMarca, ISNULL(nomeMarca,'Não cadastradado') nomeMarca FROM VEICULO V LEFT JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa LEFT JOIN MODELO M ON V.idModelo = M.idModelo LEFT JOIN MARCA MAR ON M.idMarca = MAR.idMarca";

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
                        VeiculoDomain novoVeiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                            idModelo = Convert.ToInt32(rdr["idModelo"]),
                            placa = rdr["placa"].ToString(),
                            empresa = new EmpresaDomain()
                            {
                                idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                nomeEmpresa = rdr["nomeEmpresa"].ToString(),
                            },
                            modelo = new ModeloDomain()
                            {
                                idModelo = Convert.ToInt32(rdr["idModelo"]),
                                idMarca = Convert.ToInt32(rdr["idMarca"]),
                                nomeModelo = rdr["nomeModelo"].ToString(),
                                marca = new MarcaDomain()
                                {
                                    idMarca = Convert.ToInt32(rdr["idMarca"]),
                                    nomeMarca = rdr["nomeMarca"].ToString(),
                                }

                            }

                        };
                        listaVeiculos.Add(novoVeiculo);
                    }
                    return listaVeiculos;
                }
            }
        }
    }
}
