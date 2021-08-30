using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_pessoas_WebAPI.Domains
{
    public class TelefoneDomain
    {
        public int idTelefone { get; set; }
        public int idPessoa { get; set; }
        public string numeroTelefone { get; set; }
        public PessoaDomain pessoa { get; set; }
    }
}
