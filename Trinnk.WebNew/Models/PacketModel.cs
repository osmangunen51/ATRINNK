using Trinnk.Core;
using Trinnk.Core.Infrastructure;
using Trinnk.Entities.Tables.Payments;
using Trinnk.Services.Payments;
using NeoSistem.Trinnk.Web.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models
{

    public class PacketModel
    {

        public PacketModel()
        {
            this.OrderType = (byte)Ordertype.KrediKarti;
            this.TaxAndAddressViewModel = new TaxAndAddressViewModel();

        }
        public string paymecpost
        {
            get
            {
                return "https://www.paymec.com/paymecodeme/paymec.asp";
            }
        }
        public string siteno
        {
            get
            {
                return "76144";
            }
        }

        public string banka
        {
            get
            {
                if (CreditCardId == 15 || CreditCardId == 16)
                {
                    //Finansbank( CardFinans , FixCard )
                    return "1";
                }
                else if (CreditCardId == 8)
                {
                    //Garanti Bankası, DenizBank, TEB ( BonusCard )
                    return "2";
                }
                else if (CreditCardId == 13)
                {
                    //İş Bankası ( MaximumCard )
                    return "3";
                }
                else if (CreditCardId == 12)
                {
                    //HalkBank ( ParafCard )
                    return "4";
                }
                else
                {
                    return "";
                }

            }
        }



        public int OrderId { get; set; }
        public int MainPartyId { get; set; }
        public int PacketId { get; set; }
        public byte AccountId { get; set; }
        public string OrderCode { get; set; }
        public string OrderNo { get; set; }
        //public string Address { get; set; }
        //public string TaxOffice { get; set; }
        //public string TaxNo { get; set; }
        public decimal OrderPrice { get; set; }
        public byte PacketStatu { get; set; }
        public DateTime RecordDate { get; set; }
        public byte OrderType { get; set; }
        public string StoreName { get; set; }
        public string AvenueOtherCountries { get; set; }
        public string PacketName { get; set; }
        public bool NewAddress { get; set; }
        public byte CreditCardId { get; set; }
        public short CreditCardInstallmentId { get; set; }
        public bool Registered { get; set; }
        public bool UnRegistered { get; set; }
        public string Gsm { get; set; }
        public decimal? PayPriceAmount { get; set; }
        public int PacketDay { get; set; }




        public IList<BankAccount> AccountList { get; set; }

        public IList<CreditCard> CreditCardItems { get; set; }
        public IList<CreditCardInstallment> CreditCardInstallmentItems { get; set; }

        public CreditCardInstallment GetCreditCardInstallment
        {
            get
            {

                ICreditCardService creditCardService = EngineContext.Current.Resolve<ICreditCardService>();
                var creditCardInstallment = creditCardService.GetCreditCardInstallmentByCreditCardInstallmentId(CreditCardInstallmentId);
                return creditCardInstallment;
            }
        }

        public CreditCard GetCreditCard
        {
            get
            {
                //CreditCard CCI = null;
                //using (var entities = new TrinnkEntities())
                //{
                //    CCI = entities.CreditCards.Include("VirtualPos").SingleOrDefault(c => c.CreditCardId == CreditCardId);
                //}
                //return CCI;

                ICreditCardService creditCardService = EngineContext.Current.Resolve<ICreditCardService>();
                var creditCard = creditCardService.GetCreditCardByCreditCardId(CreditCardId);
                return creditCard;
            }
        }
        public TaxAndAddressViewModel TaxAndAddressViewModel { get; set; }

        public VirtualPos VirtualPos
        {
            get
            {
                IVirtualPosService virtualPosService = EngineContext.Current.Resolve<IVirtualPosService>();
                var virtualPos = virtualPosService.GetVirtualPosByVirtualPosId(GetCreditCard.VirtualPosId);
                return virtualPos;
            }
        }

        public string VirtualPosReturnURL
        {
            get
            {
                var url = AppSettings.SiteUrl;
                //var url = AppSettings.SiteUrl;
                url = url + "UyelikSatis/CStep";
                return url;
            }
        }
        public string GetHashStrASNew(string amount, string taksit, string rnd)
        {

            String islemtipi = "Auth";
            String hashstr = this.VirtualPos.VirtualPosClientId + rnd.ToString().Replace(" ", "").Replace(".", "").Replace(":", "") + amount + VirtualPosReturnURL + VirtualPosReturnURL + islemtipi + taksit + rnd + VirtualPos.VirtualPosStoreKey;
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashstr);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            var hash = Convert.ToBase64String(inputbytes);
            return hash;
        }

        public string GetHashStrASPaymec(string tutar, string taksit)
        {

            string anahtar = "69c1365cd932bffaf715009b50ef2e1d";
            string siteno = "76144";
            String hashstr = anahtar + siteno + this.banka + tutar + taksit;
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashstr);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            var hash = Convert.ToBase64String(inputbytes);
            return hash;
        }
        public string GetHashStr(string amount, string rnd)
        {

            //String hashstr = this.VirtualPos.VirtualPosClientId + amount + VirtualPosReturnURL + VirtualPosReturnURL + rnd.ToString().Replace(" ", "").Replace(".", "").Replace(":", "") + "KEYMTTR8535";
            //hashstr = hashstr.Replace("Auth3", "").Replace("Auth", "").Replace("CStep3", "CStep").Replace("CStep 3", "CStep");
            string hashstr = this.VirtualPos.VirtualPosClientId + amount + VirtualPosReturnURL + VirtualPosReturnURL + rnd + VirtualPos.VirtualPosStoreKey;
            // string hashstr = "10101197787.51https://www.trinnk.com/UyelikSatis/CStephttps://www.trinnk.com/UyelikSatis/CStep30062014234140KEYMTTR8535";
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashstr);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            var hash = Convert.ToBase64String(inputbytes);
            return hash;
        }

        public decimal MaturityCalculation(decimal price, decimal maturity)
        {
            return (price + (price * (maturity) / 100));
        }


    }
}