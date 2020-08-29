using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
   public class PhoneActivationModel
    {
        public string GsmCountryCode { get; set; }
        public string GsmAreaCode { get; set; }
        public string Gsm { get; set; }
        public string ActivationCode { get; set; }

    }
}
