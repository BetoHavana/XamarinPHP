using System;
using System.Collections;
using System.Collections.Generic;

namespace CarsAppAPI.Models
{
    public class MyPaidCarsModel
    {
        public List<Payment> payments { get; set; }

    }
    public class CorralonInfo
    {
        public string name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CarPaymentInfo
    {
        public string license_plate { get; set; }
        public string state { get; set; }
        public DateTime created_at { get; set; }
    }

    public class PaymentInfo
    {
        public string id { get; set; }
        public int amount { get; set; }
        public DateTime created_at { get; set; }
    }

    public class Payment
    {
        public CorralonInfo corralon_info { get; set; }
        public CarPaymentInfo car_payment_info { get; set; }
        public PaymentInfo payment_info { get; set; }
    }



}
