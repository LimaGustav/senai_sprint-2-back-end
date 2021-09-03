using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Domains
{
    /// <summary>
    /// Representa a entidade ALGUEL
    /// </summary>
    public class AluguelDomain
    {
        public int idAluguel { get; set; }
        public int idVeiculo { get; set; }
        public int idCliente { get; set; }
        public Nullable<DateTime> dataRetirada { get; set; }
        public Nullable<DateTime> dataDevolucao { get; set; }
        public VeiculoDomain veiculo { get; set; }
        public ClienteDomain cliente { get; set; }
    }
}
