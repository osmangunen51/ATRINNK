namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System.ComponentModel;

    public class ActivityTypeModel
  {
    public byte ActivityTypeId { get; set; }

    [DisplayName("Faaliyet Tipi")]
    public string ActivityName { get; set; }
    [DisplayName("Sıra")]
    public byte Order { get; set; }
    
  }
}