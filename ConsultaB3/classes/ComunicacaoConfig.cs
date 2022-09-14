using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaB3.models
{
    public class ComunicacaoConfig //configuracao necessaria pra comunicacao
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }   
        public string ToEmail { get; set; }   
        public string ToName { get; set; }   
    }
}
