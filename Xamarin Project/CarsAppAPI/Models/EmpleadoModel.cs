using System;
using System.Collections.Generic;
using System.Text;

namespace CarsAppAPI.Models
{
    public class EmpleadoModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string placa { get; set; }
        public string modelo { get; set; }
        public string token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }

    }
}
