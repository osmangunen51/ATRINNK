<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<FavoriteStoreModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .thumbnail img { width: 186px !important; height: 142px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;Favori Firmalarım
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                    </ol>
                </div>
            </div>
            <div>
                <div class="well well-mt">
                    <div class="row">
                        <%foreach (var item in Model.GetStore.Source)
                            {%>
                        <div class="col-xs-6 col-md-4 col-lg-3">
                            <div class="thumbnail thumbnail-mt">
                                <%  string companyUrl = "/Sirket/" + item.MainPartyId + "/" + Helpers.ToUrl(item.StoreName); %>
                                <a href='<%=companyUrl %>/SirketProfili'>
                                    <% string logoPath = ImageHelpers.GetStoreImage(item.MainPartyId, item.StoreLogo, "300");%>
                                    <img src="<%=logoPath%>" alt="<%= item.StoreName %>" />
                                </a>
                                <div class="text-center">
                                    <a href="#">
                                        <%=item.StoreName %>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <% } %>
                    </div>
                </div>
            </div>
            <%--<div class="row">
            <div class="col-md-6 ">
                Toplam 8 sayfa içerisinde 1. sayfayı görmektesiniz.
            </div>
            <div class="col-md-6 text-right">
                <ul class="pagination m0">
                    <li><a href="#">&laquo; </a></li>
                    <li><a href="#">1 </a></li>
                    <li><a href="#">2 </a></li>
                    <li><a href="#">3 </a></li>
                    <li><a href="#">4 </a></li>
                    <li><a href="#">5 </a></li>
                    <li><a href="#">&raquo; </a></li>
                </ul>
            </div>
        </div>--%>
        </div>
    </div>
</asp:Content>
