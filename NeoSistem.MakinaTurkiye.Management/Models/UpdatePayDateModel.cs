using System;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class UpdatePayDateModel
    {
        public UpdatePayDateModel()
        {
            this.UpdatePayDateModels = new List<UpdatePayDateModel>();
            this.OrderInstallmentItems = new List<OrderInstallmentItemModel>();
        }
        public int UpdatePayDateId { get; set; }
        public int OrderId { get; set; }
        public DateTime WillPayDate { get; set; }
        public string Description { get; set; }
        public DateTime? RecordDate { get; set; }

        public List<UpdatePayDateModel> UpdatePayDateModels { get; set; }
        public List<OrderInstallmentItemModel> OrderInstallmentItems { get; set; }
    }
}