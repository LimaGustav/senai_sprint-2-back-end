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

        public void AtualizarIdUrl(int idAlguel, AluguelDomain aluguelAtualizado)
        {
            throw new NotImplementedException();
        }

        public AluguelDomain BuscarPorId(int idAlguel)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(AluguelDomain novoAluguel)
        {
            throw new NotImplementedException();
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

        public List<AluguelDomain> ListarTodos()
        {
            //Declara uma lista de alugueis que será retornada no fim
            List<AluguelDomain> listaAluguel = new List<AluguelDomain>();

            // Declara uma SqlConnection com a string de conexão como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando a ser rodado no banco de dados
                string querySelectAll = "SELECT	idAluguel, ISNULL(A.idVeiculo,0) idVeiculo, ISNULL(A.idCliente,0) idCliente, ISNULL(A.dataRetirada,0) dataRetirada, ISNULL(A.dataDevolucao,0)dataDevolucao, ISNULL(V.idEmpresa,0) idEmpresa, ISNULL(V.idModelo,0) idModelo, ISNULL(V.placa,'Não cadastrado!') placa, ISNULL(E.nomeEmpresa,'Não cadastrado!') nomeEmpresa, ISNULL(M.idMarca,0) idMarca, ISNULL(M.nomeModelo,'Não cadastrado!') nomeModelo, ISNULL(MAR.nomeMarca,'Não cadastrado!') nomeMarca, ISNULL (C.idCliente,0) idCliente, ISNULL (C.nome,'Não cadastrado!') nome, ISNULL (C.sobreNome, 'Não cadastrado!') sobreNome, ISNULL (cnh, 'Não cadastrado!') cnh FROM ALUGUEL A LEFT JOIN VEICULO V ON A.idVeiculo = V.idVeiculo LEFT JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa LEFT JOIN MODELO M ON V.idModelo = M.idModelo LEFT JOIN MARCA MAR ON M.idMarca = MAR.idMarca LEFT JOIN CLIENTE C ON A.idCliente = C.idCliente";

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
