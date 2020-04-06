<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTBrandModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_HeaderContent") %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="StoreProfileHeaderContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%} %>
    <%=Html.RenderHtmlPartial("_HeaderTop",Model.MTStoreProfileHeaderModel) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StoprofileMenu" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%=Html.RenderHtmlPartial("_LeftMenu", Model.MTStoreProfileHeaderModel)%>

    <%} %>
</asp:Content>

<asp:Content ContentPlaceHolderID="StoreProfileContent" runat="server" ID="Content9">
    <%if(Model.StoreActiveType==2) {%>
    <div class="col-sm-7 col-md-8 col-lg-9">
        <div class="StoreProfileContent" id="StoreProfileBrandNew">
            <% foreach (var item in Model.StoreBrands)
                { %>
            <div class="col-xs-12 col-md-12">
                <div class="thumbnail thumbnail-mt">
                    <div class="col-md-3">
                        <img src="<%=AppSettings.SiteUrl+MakinaTurkiye.Utilities.FileHelpers.FileHelper.ImageThumbnailName(AppSettings.StoreBrandImageFolder + item.BrandPicture) %>"
                            alt="  <%= item.BrandDescription %>" />
                    </div>
                    <div class="caption col-md-9">
                        <span><%= item.BrandName %></span>
                        <%= item.BrandDescription %>
                    </div>
                </div>
            </div>
            <% } %>
            <div class="clearfix"></div>
            <!-- ./StoreProfileContent -->
        </div>
        <!-- /.col-md-5 main content -->
    </div>
    <%}else{ %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>
