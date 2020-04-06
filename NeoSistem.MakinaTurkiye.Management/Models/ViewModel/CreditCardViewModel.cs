namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class CreditCardViewModel
  {
    public CreditCard CreditCard { get; set; }
    public IList<CreditCardInstallment> CreditCardInstallmentItems { get; set; } 

    public SelectList VirtualPosItems
    {
      get
      {
        IList<VirtualPos> VirtualPosItems = null;
        using (var entities = new MakinaTurkiyeEntities())
        {
          VirtualPosItems = entities.VirtualPos.ToList();
        }
        VirtualPosItems.Insert(0, new VirtualPos { VirtualPosId = 0, VirtualPostName = "< Lütfen Seçiniz >" });
        return new SelectList(VirtualPosItems, "VirtualPosId", "VirtualPostName", 0);
      }
    }


  }
}