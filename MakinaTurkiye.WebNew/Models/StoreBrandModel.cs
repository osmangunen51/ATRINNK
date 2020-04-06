namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;
  using System.Runtime.CompilerServices;

  public class StoreBrandModel
  {
    public int StoreBrandId { get; set; }
    public int StoreId { get; set; }
    public string BrandName { get; set; }
    public string BrandDescription { get; set; }
    public string BrandPicture { get; set; }
  }
}

