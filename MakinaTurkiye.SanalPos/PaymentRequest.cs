using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.SanalPos
{
   public class PaymentRequest
    {
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public decimal Amount { get; set; } = 0;
        public int Installment { get; set; } = 0;
        public User User { get; set; } = new User();
        public Card Card { get; set; } = new Card();
    }

    public class Card
    {
        public string HolderName { get; set; } = ""; 
        public string No { get; set; } = "";
        public string Mount { get; set; } = "";
        public string Year { get; set; } = "";
        public string Cvc { get; set; } = "";
    }

    public class User
    {
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public class PaymentGatewayResult
    {
        public Uri GatewayUrl { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public string HtmlFormContent { get; set; }
        public bool HtmlContent => !string.IsNullOrEmpty(HtmlFormContent);

        public static PaymentGatewayResult Successed(string htmlFormContent,
            string message = null)
        {
            return new PaymentGatewayResult
            {
                Success = true,
                HtmlFormContent = htmlFormContent,
                Message = message
            };
        }

        public static PaymentGatewayResult Successed(IDictionary<string, object> parameters,
            string gatewayUrl,
            string message = null)
        {
            return new PaymentGatewayResult
            {
                Success = true,
                Parameters = parameters,
                GatewayUrl = new Uri(gatewayUrl),
                Message = message
            };
        }

        public static PaymentGatewayResult Failed(string errorMessage, string errorCode = null)
        {
            return new PaymentGatewayResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode
            };
        }
    }

}
