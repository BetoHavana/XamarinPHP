using System;
using System.Collections.Generic;
using System.Text;
namespace CarsAppAPI.Models
{
    public class PaymentModel
    {

        public List<Card> cards { get; set; }

        public class Card
        {
            public string id { get; set; }
            public string card_number { get; set; }
            public string holder_name { get; set; }
            public string expiration_year { get; set; }
            public string expiration_month { get; set; }
            public bool allows_charges { get; set; }
            public bool allows_payouts { get; set; }
            public DateTime creation_date { get; set; }
            public string bank_name { get; set; }
            public string bank_code { get; set; }
            public string type { get; set; }
            public string brand { get; set; }
        }
    }

}
