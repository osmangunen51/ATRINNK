<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Statistics.MTProductStatisticViewModel>" %>

<%@ Import Namespace="NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ürün Görüntülenme İstatistikleri

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>Ürün İstatistikleri
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well" style="background-color: #fff!important;">
                    <div class="alert alert-info">
                        <span class="glyphicon glyphicon-info-sign"></span>Makina Türkiye özel reklam seçenekleri
                ile firmanızın görüntülenme sayısını arttırabilirsiniz. Detaylı bilgi için <a href="https://www.makinaturkiye.com/reklam-y-141615">buraya </a>tıklayın.
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="float:left">

                                <%foreach (var item in Model.FilterItemModels)
                                    {%>

                                <span style="padding: 5px;" class="badge badge-info"><%:item.FilterName %> <a style="cursor: pointer;" href="<%:item.FilterBackUrl %>"><i style="color: #fff;" class="fa fa-window-close"></i></a></span>
                                <% } %>
                            </div>

                            <div style="float: right">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Başlangıç Tarihi</label>
                                            <%:Html.TextBoxFor(x=>x.StartDate, new {@class="mt-form-control date", @autocomplete="off" }) %>
                        
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Bitiş Tarihi</label>
                                                     <%:Html.TextBoxFor(x=>x.EndDate, new {@class="mt-form-control date", @autocomplete="off" }) %>
                           
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>

                    <div id="data" class="row">

                        <%=Html.RenderHtmlPartial("_ProductStatisticCenter",Model) %>
                    </div>

                    <%string productId = "0";
                        if (Request.QueryString["ProductId"] != null)
                        {
                            productId = Request.QueryString["ProductId"].ToString();
                        }%>
                    <%:Html.Hidden("ProductId",productId,new {id="selectedProductId" }) %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="../../../../Content/V2/assets/js/bootstrap-datepicker.min.js"></script>
       <script type="text/javascript" src="../../../../Content/V2/assets/js/bootstrap-datepicker.tr.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {



            $('.date').datepicker({
                format: 'dd/mm/yyyy',
                language: 'tr',
                  todayHighlight:true
            });

            $('#EndDate').change(function () {
                // set the window's location property to the value of the option the user has selected
                var val = $(this).val();
                var url = "/Account/Statistic/ProductStatistics";
                var data = $("#selectedProductId").val();

                if (data != 0) {
                    url = url + "?ProductId=" + data + "&startDate=" +$("#StartDate").val()+"&endDate="+val;
                }
                else {
                    url = url + "?startDate=" +$("#StartDate").val()+"&endDate="+val;
                }
                window.location = url;
            });
        });


    </script>
</asp:Content>
