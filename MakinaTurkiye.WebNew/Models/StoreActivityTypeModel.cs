namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using EnterpriseEntity.Extensions.Data;

  public class StoreActivityTypeModel
  {
    public byte ActivityTypeId { get; set; }
    public int StoreId { get; set; }
    public string ActivityName { get; set; }
  }
}