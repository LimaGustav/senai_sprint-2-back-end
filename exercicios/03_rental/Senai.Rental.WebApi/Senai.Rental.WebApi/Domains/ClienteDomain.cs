using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Domains
{
    /// <summary>
    /// Representa e entidade CLIENTE
    /// </summary>
    public class ClienteDomain
    {
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string  sobreNome { get; set; }
    }
}
