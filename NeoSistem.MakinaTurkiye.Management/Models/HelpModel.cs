using NeoSistem.MakinaTurkiye.BaseModule.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class HelpModel
    {
        public int ID { get; set; }
        public int ConstantId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime RecordDate { get; set; }

        private SelectList myConstantsList;
        public SelectList ConstantList
        {
            get
            {
                var entities = new MakinaTurkiyeEntities();
                if (myConstantsList == null || myConstantsList.Count() <= 0)
                {
                    var curCountry = new Classes.Constant();
                    var Constants = entities.Constants.Where(x=>x.ConstantType==(byte)ConstantType.CrmYardimKategori).OrderBy(c => c.ConstantName).ToList();
                    Constants.Insert(0, new Constant { ConstantId = 0, ConstantName = "< Lütfen Seçiniz >" });
                    myConstantsList = new SelectList(Constants, "ConstantId", "ConstantName", 0);
                }
                return myConstantsList;
            }
            set { myConstantsList = value; }
        }

    }
}