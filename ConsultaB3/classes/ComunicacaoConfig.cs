using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaB3.models
{
    public class ComunicacaoConfig
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }   
    }
}
