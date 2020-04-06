namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System.ComponentModel;

    public class SeoModel
  {
    public int SeoId { get; set; }

    [DisplayName("Sayfa")]
    public string PageName { get; set; }

    [DisplayName("Başlık (title)")]
    public string Title { get; set; }

    [DisplayName("Açıklama (description)")]
    public string Description { get; set; }

    [DisplayName("Açıklama (abstract)")]
    public string Abstract { get; set; }

    [DisplayName("Anahtar Kelimeler (keywords)")]
    public string Keywords { get; set; }

    [DisplayName("Kategori (classification)")]
    public string Classification { get; set; }

    [DisplayName("Takip (robots)")]
    public string Robots { get; set; }

    [DisplayName("Ziyaret (revisit-after)")]
    public string RevisitAfter { get; set; }

    [DisplayName("Parametre")]
    public string Parameter { get; set; }
  }
}