namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System.Collections.Generic;
  using EnterpriseEntity.Extensions.Data;
  using System;

  public class MessageMainPartyModel
  {
    public int MessageId { get; set; }
    public int MessageMainPartyId { get; set; }
    public int MainPartyId { get; set; }
    public int InOutMainPartyId { get; set; }
    public byte MessageType { get; set; }
  }

}