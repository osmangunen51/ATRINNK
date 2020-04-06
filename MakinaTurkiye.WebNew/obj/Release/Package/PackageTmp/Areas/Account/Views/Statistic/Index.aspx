<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Statistics.ProductStatisticModel>" %>

<%@ Import Namespace="NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    İstatistik
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function PageChangeStatic(p, d, a) {
            $.ajax({
                url: '/Account/ilan/AdvertPagingfor',
                type: 'post',
                data: { page: p, displayType: d, advertListType: 4, ProductActiveType: a },
                success: function (data) {
                    $('#AdvertData').html(data);
                }
            });
        }
    </script>
    <%
        int pagetype = Request.QueryString["pagetype"].ToInt32();
        switch (pagetype)
        {
            case 0: %>
    <%break;
        case 1: %>
    <%Model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.Statistics, (byte)LeftMenuConstants.Statistic.StoreStatistics);%>
    <%break;
        case 2: %>
    <%break;

        case 3: %>
    <%Model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.Statistics, (byte)LeftMenuConstants.Statistic.AdStatistics);%>
    <%break;
        default:%>
    <%  break;
        }%>
    <%--    <%= Html.RenderHtmlPartial("LeftControl")%>--%>

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
    </div>
    <div class="row">
        <div id="Advert">
            <%
                //int pagetype = Request.QueryString["pagetype"].ToInt32();
                switch (pagetype)
                {
                    case 0: %>
            <%--    <%= Html.RenderHtmlPartial("ForAll")%>--%>
            <%break;
                case 1: %>
            <%= Html.RenderHtmlPartial("StoreStatistic",Model.MTStatisticModel)%>
            <%break;
                case 2: %>
            <%--  <%= Html.RenderHtmlPartial("ProductCount")%>--%>
            <%break;

                case 3: %>
            <%= Html.RenderHtmlPartial("ProductOnebyOne",Model)%>
            <%break;
                default:%>
            <%-- <%= Html.RenderHtmlPartial("ForAll")%>--%>
            <%  break;
                }%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
