<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreImageModel>" %>
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
<asp:Content ID="Content3" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>
    <%:Html.Hidden("hdnStoreMainPartyId",Model.MainPartyId) %>

    <div class="col-sm-7 col-md-8 col-lg-9">

        <div class="StoreProfileContent clearfix">

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                <%foreach (var item in Model.ImagePath.ToList())
                    {%>
                <div class="col-md-4 col-lg-3">
                    <img class="img-responsive firm-picture" src="https://www.makinaturkiye.com/<%:item %>" />
                </div>
                <%	} %>

                <div class="clearfix"></div>
                <!-- ./StoreProfileContent -->
            </div>
            <!-- /.col-md-5 main content -->
        </div>
        <!-- container -->
    </div>

    <%}
    else
    { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>
