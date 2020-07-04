<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTDealerShipModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>
    <%:Html.Hidden("hdnStoreMainPartyId",Model.MainPartyId) %>
    <div class="col-sm-7 col-md-8 col-lg-9">
        <div class="StoreProfileContent">

            <div class="col-xs-12">
                <% foreach (var item in Model.DealerBrands)
                    { %>
                <div class="col-xs-4 col-md-2">
                    <div class="thumbnail thumbnail-mt">
                        <img src="<%=MakinaTurkiye.Utilities.FileHelpers.FileHelper.ImageThumbnailName(AppSettings.DealerBrandImageFolder + item.DealerBrandPicture) %>" />
                        <div class="caption">
                            <%= item.DealerBrandName %>
                        </div>
                    </div>
                </div>
                <% } %>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <%}
    else
    { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>
