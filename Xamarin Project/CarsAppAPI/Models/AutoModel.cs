using System;
using System.Collections.Generic;
using System.Text;

namespace CarsAppAPI.Models
{
    public class AutoModel
    {
        public Cars_Pagination cars_pagination { get; set; }
        public Car car { get; set; }
        public String response {get;set;}
    }

    public class Cars_Pagination
    {
        public int current_page { get; set; }
        public Datum[] data { get; set; }
        public string first_page_url { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public string last_page_url { get; set; }
        public string next_page_url { get; set; }
        public string path { get; set; }
        public int per_page { get; set; }
        public object prev_page_url { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string license_plate { get; set; }
        public string model { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class Car
    {
        public int id { get; set; }
        public string license_plate { get; set; }
        public string model { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

}
