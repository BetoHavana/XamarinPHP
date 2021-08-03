using System;

namespace CarsAppAPI.Models
{
    public class StaticConstants
    {
       
        public static string Token { get; set; }
        public static string ChargeEndpoint = "/api/v1/openpay/charges";
        public static string GetCardsEndpoint = "/api/v1/openpay/cards";
        public static string GetCarsEndpoint = "/api/v1/cars?page=1";
        public static string AddCardEndpoint = "/api/v1/openpay/cards";
        public static string UrlBase = "https://ruffsstudios.com/api_carros/public";
        public static string LoginUrl = "/api/v1/auth/login";
        public static string RegisterEndpoint = "/api/v1/auth/create";
        public static bool isLogged = false;
        public static bool showBackLabel = true;
        public static bool showCarResponse = false;
        public static bool isRegisteredFromGuest = false;

        public StaticConstants(){ }
    }
}
