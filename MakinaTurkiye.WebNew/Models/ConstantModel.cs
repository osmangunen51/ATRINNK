namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.ComponentModel;
  using System.Web.Mvc;
  using EnterpriseEntity.Extensions.Data;
  using System.Data;

  public class ConstantModel
  {
    public short ConstantId { get; set; }
    public byte ConstantType { get; set; }
    public string ConstantName { get; set; }
    public int Order { get; set; }
  }

}