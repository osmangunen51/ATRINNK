namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTStorePhoneModel
    {
        public PhoneType PhoneType { get; set; }
        public string PhoneCulture { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool ShowPhone { get; set; }


        public string GetFullText(bool withSpace = true)
        {
            //TODO AdilD diger sayfalarda manuel yapilmis birlestirme islemi daha sonra temizlenebilir, extensions clasi kullanilir ise ilerde bu metod oraya tasinabilir
            var result = this.PhoneCulture + " " + this.PhoneAreaCode + " " + this.PhoneNumber;
            if (!withSpace)
            {
                result = result.Replace(" ", "");
            }
            return result;
        }
    }
}