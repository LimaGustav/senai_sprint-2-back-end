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
    /// Repositório referente a entidade ALUGUEL
    /// </summary>
    public class AluguelRepository : IAluguelRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// </summary>
        public string stringConexao = "Data Source=DESKTOP-8VJGUSR\\SQLEXPRESS; initial catalog=rental; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idAluguel, AluguelDomain aluguelAtualizado)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl;
                if (aluguelAtualizado.dataDevolucao != null)
                {
                    // Comando a ser rodado no banco de dados
                    queryUpdateUrl = "UPDATE ALUGUEL SET idVeiculo = @idVeiculo, idCliente = @idCliente, dataRetirada = @dataRetirada, dataDevolucao = @dataDevolucao WHERE idAluguel = @idAluguel";
                }
                else
                {
                    // Comando a ser rodado no banco de dados
                    queryUpdateUrl = "UPDATE ALUGUEL SET idVeiculo = @idVeiculo, idCliente = @idCliente, dataRetirada = @dataRetirada WHERE idAluguel = @idAluguel";
                }

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    // Atribui valor ao parametro @idVeiculo
                    cmd.Parameters.AddWithValue("@idVeiculo", aluguelAtualizado.idVeiculo);

                    // Atribui valor ao parametro @idCliente
                    cmd.Parameters.AddWithValue("@idCliente", aluguelAtualizado.idCliente);

                    // Atribui valor ao parametro @dataRetirada
                    cmd.Parameters.AddWithValue("@dataRetirada", aluguelAtualizado.dataRetirada);

                    if (aluguelAtualizado.dataDevolucao != null)
                    {
                        // Atribui valor ao parametro @dataDevolucao
                        cmd.Parameters.AddWithValue("@dataDevolucao", aluguelAtualizado.dataDevolucao);
                    }

                    // Atribui valor ao parametro @idAluguel
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    // Abre conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public AluguelDomain BuscarPorId(int idAlguel)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Comando a ser rodado no banco de dados
                string querySelectById = "SELECT idAluguel, ISNULL(A.idVeiculo,0) idVeiculo, ISNULL(A.idCliente,0) idCliente, convert(varchar(30), dataRetirada, 103)dataRetirada, ISNULL(convert(varchar(30), dataDevolucao, 103), '01/01/01') dataDevolucao, ISNULL(V.idEmpresa,0) idEmpresa, ISNULL(V.idModelo,0) idModelo, ISNULL(V.placa,'Não cadastrado!') placa, ISNULL(E.nomeEmpresa,'Não cadastrado!') nomeEmpresa, ISNULL(M.idMarca,0) idMarca, ISNULL(M.nomeModelo,'Não cadastrado!') nomeModelo, ISNULL(MAR.nomeMarca,'Não cadastrado!') nomeMarca, ISNULL (C.idCliente,0) idCliente, ISNULL (C.nome,'Não cadastrado!') nome, ISNULL (C.sobreNome, 'Não cadastrado!') sobreNome, ISNULL (cnh, 'Não cadastrado!') cnh FROM ALUGUEL A LEFT JOIN VEICULO V ON A.idVeiculo = V.idVeiculo LEFT JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa LEFT JOIN MODELO M ON V.idModelo = M.idModelo LEFT JOIN MARCA MAR ON M.idMarca = MAR.idMarca LEFT JOIN CLIENTE C ON A.idCliente = C.idCliente WHERE A.idAluguel = @idAluguel";

                //Declara uma SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara um SqlCommand passando a query e a conexão como parametros
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    //Abre conexão com o bando de dados
                    con.Open();

                    //Atribui valor ao parametro @idAluguel
                    cmd.Parameters.AddWithValue("@idAluguel", idAlguel);

                    rdr = cmd.ExecuteReader();

                    // Caso for encontrado um veiculo com o id passado no argumento
                    if (rdr.Read())
                    {
                        AluguelDomain aluguelBuscado = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr["idAluguel"]),
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            idCliente = Convert.ToInt32(rdr["idCliente"]),
                            dataRetirada = Convert.ToDateTime(rdr["dataRetirada"]),
                            dataDevolucao = Convert.ToDateTime(rdr["dataDevolucao"]),
                            veiculo = new VeiculoDomain()
                            {
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
                            },
                            cliente = new ClienteDomain()
                            {
                                idCliente = Convert.ToInt32(rdr["idCliente"]),
                                nome = rdr["nome"].ToString(),
                                sobreNome = rdr["sobreNome"].ToString(),
                                cnh = rdr["cnh"].ToString(),
                            }
                        };
                        return aluguelBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(AluguelDomain novoAluguel)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert;
                if (novoAluguel.dataDevolucao == null)
                {
                    queryInsert = "INSERT INTO ALUGUEL (idVeiculo,idCliente,dataRetirada) VALUES (@idVeiculo, @idCliente, @dataRetirada)";
                }
                else
                {
                    queryInsert = "INSERT INTO ALUGUEL (idVeiculo,idCliente,dataRetirada,dataDevolucao) VALUES ( @idVeiculo, @idCliente, @dataRetirada,@dataDevolucao)";
                }

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    // Atribui valor ao parametro @idVeiculo
                    cmd.Parameters.AddWithValue("@idVeiculo", novoAluguel.idVeiculo);

                    // Atribui valor ao parametro @idCliente
                    cmd.Parameters.AddWithValue("@idCliente", novoAluguel.idCliente);

                    // Atribui valor ao parametro @dataRetirada
                    cmd.Parameters.AddWithValue("@dataRetirada", novoAluguel.dataRetirada);

                    if (novoAluguel.dataDevolucao != null)
                    {
                        // Atribui valor ao parametro @dataDevolucao
                        cmd.Parameters.AddWithValue("@dataDevolucao", novoAluguel.dataDevolucao);
                    }

                    // Abre conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Deletar(int idAlguel)
        {
            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string queryDelete = "DELETE FROM ALUGUEL  WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Atribui valor ao parametro @idAluguel
                    cmd.Parameters.AddWithValue("@idAluguel", idAlguel);

                    // Abre conexão com o banco de dados
                    con.Open();

                    // Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<AluguelDomain> ListarTodos()
        {
            //Declara uma lista de alugueis que será retornada no fim
            List<AluguelDomain> listaAluguel = new List<AluguelDomain>();

            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string querySelectAll = "SELECT	idAluguel, ISNULL(A.idVeiculo,0) idVeiculo, ISNULL(A.idCliente,0) idCliente, convert(varchar(30), dataRetirada, 103)dataRetirada, ISNULL(convert(varchar(30), dataDevolucao, 103),'01/01/01')dataDevolucao, ISNULL(V.idEmpresa,0) idEmpresa, ISNULL(V.idModelo,0) idModelo, ISNULL(V.placa,'Não cadastrado!') placa, ISNULL(E.nomeEmpresa,'Não cadastrado!') nomeEmpresa, ISNULL(M.idMarca,0) idMarca, ISNULL(M.nomeModelo,'Não cadastrado!') nomeModelo, ISNULL(MAR.nomeMarca,'Não cadastrado!') nomeMarca, ISNULL (C.idCliente,0) idCliente, ISNULL (C.nome,'Não cadastrado!') nome, ISNULL (C.sobreNome, 'Não cadastrado!') sobreNome, ISNULL (cnh, 'Não cadastrado!') cnh FROM ALUGUEL A LEFT JOIN VEICULO V ON A.idVeiculo = V.idVeiculo LEFT JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa LEFT JOIN MODELO M ON V.idModelo = M.idModelo LEFT JOIN MARCA MAR ON M.idMarca = MAR.idMarca LEFT JOIN CLIENTE C ON A.idCliente = C.idCliente";

                // Declara uma SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara um SqlCommand passando a query e a conexão como parametros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Abre conexão com o banco de dados
                    con.Open();

                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registro para serem lidos no rdr o laço se repete
                    while (rdr.Read())
                    {
                        AluguelDomain novoAlugel = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr["idAluguel"]),
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            idCliente = Convert.ToInt32(rdr["idCliente"]),
                            dataRetirada = Convert.ToDateTime(rdr["dataRetirada"]),
                            dataDevolucao = Convert.ToDateTime(rdr["dataDevolucao"]),
                            veiculo = new VeiculoDomain()
                            {
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
                            },
                            cliente = new ClienteDomain()
                            {
                                idCliente = Convert.ToInt32(rdr["idCliente"]),
                                nome = rdr["nome"].ToString(),
                                sobreNome = rdr["sobreNome"].ToString(),
                                cnh = rdr["cnh"].ToString(),
                            }
                        };
                        listaAluguel.Add(novoAlugel);
                    }
                    return listaAluguel;
                }
            }
        }

    }
}
