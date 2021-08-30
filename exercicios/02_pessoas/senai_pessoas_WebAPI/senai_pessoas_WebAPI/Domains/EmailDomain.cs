using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_pessoas_WebAPI.Domains
{
    public class EmailDomain
    {
        public int idEmail { get; set; }
        public int idPessoa { get; set; }
        public string endEmail { get; set; }
        public PessoaDomain pessoa { get; set; }
    }
}