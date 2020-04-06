namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System.ComponentModel;

    public class AddressTypeModel
  {
    public int AddressTypeId { get; set; }
    [DisplayName("Adres Tipi")]
    public string AddressTypeName { get; set; }
  }
}