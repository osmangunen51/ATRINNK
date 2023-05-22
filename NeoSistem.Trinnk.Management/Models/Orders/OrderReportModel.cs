namespace NeoSistem.Trinnk.Management.Models.Orders
{
    public class OrderReportModel
    {
        public OrderReportModel()
        {
            this.OrderReportItems = new FilterModel<OrderReportItemModel>();

        }
        public decimal TotalPrice { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRestPrice { get; set; }
        public FilterModel<OrderReportItemModel> OrderReportItems { get; set; }
    }
}