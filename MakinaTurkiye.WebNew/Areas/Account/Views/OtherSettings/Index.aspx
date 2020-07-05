﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.OtherSettings.OtherSettingsProductModel>" %>

<%@ Import Namespace="NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        int pagetype = Request.QueryString["pagetype"].ToInt32();
        switch (pagetype)
        {
            case 0: %>
    <%break;
        case 4: %>
    <%Model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.OtherSettings, (byte)LeftMenuConstants.OtherSettings.SortEdit);%>
    <%break;
        case 5: %>
    <%Model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.OtherSettings, (byte)LeftMenuConstants.OtherSettings.PriceEdit);%>
    <%break;
        default:%>
    <%  break;
        }%>
        <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenuModel)%>
        </div>

    </div>
    <div class="row">
        <div>
                    <div class="loading">Loading&#8230;</div>
            <%
                switch (pagetype)
                {
                    case 0: %>
            <%break;
                case 4: %>
            <%= Html.RenderHtmlPartial("ProductSort",Model.MTProductItems)%>
            <%break;
                case 5: %>
            <%= Html.RenderHtmlPartial("ProductPrice",Model.MTProductItems)%>
            <%break;
                default:%>
            <%  break;
                }%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
        <script type="text/javascript">



        function SortEdit() {
            var Id = document.getElementById("hdnSortId").value;
            var SortNumber = document.getElementById("SortValue").value;
            $.ajax({
                url: '/Account/ilan/ProductSortUpdate',
                type: 'post',
                data: { ProductId: Id, SortNumber: SortNumber },
                success: function (data) {
                    window.location = "/Account/OtherSettings/Index?pagetype=4";
                }
            });
        }

        function SortEditShow(Id) {
            document.getElementById("hdnSortId").value = Id;

            $.ajax({
                url: '/Account/ilan/ProductSortGet',
                type: 'post',
                data: { ProductId: Id },
                success: function (data) {
                    document.getElementById("SortValue").value = data;
                }
            });
        }
        function PriceEditClick(Id)
        {
            var PriceNumber = document.getElementById("editPriceValue" + Id).value;
   
            if (PriceNumber != "") {
                $.ajax({
                    url: '<%:Url.Action("PriceUpdate1","OtherSettings")%>',
                    type: 'post',
                    data: { ProductId: Id, PriceNumber: PriceNumber },
                    success: function (data) {
                        $("#priceDisplayValue" + Id).html(PriceNumber);
                        $("#priceDisplay" + Id).show();
                        $("#priceEditWrapper" + Id).hide();
                    }
                });
            }
            else
            {
                alert("Fiyat Giriniz");
            }
        }
        function PriceEditCancel(Id)
        {
        
            $("#priceDisplay" + Id).show();
            $("#priceEditWrapper" + Id).hide();
        }

        function PriceEditShow(Id) {

            $("#priceDisplay" + Id).hide();
            $("#priceEditWrapper" + Id).show();
            document.getElementById("hdnPriceId").value = Id;
            }
            function ExportExcel() {
                   var url = '/Account/OtherSettings/ExportProducts';
    
        window.open(url);
            }

    </script>
</asp:Content>
