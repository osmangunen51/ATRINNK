namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;

  public class MainPartyModel
  {
    public int MainPartyId { get; set; }

    public string MainPartyFullName { get; set; }

    public bool Active { get; set; }

    public DateTime MainPartyRecordDate { get; set; }

  }
}