<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreProfileVideoModel>" %>
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

        <div class="StoreProfileMenu mb20">
            <%=Html.RenderHtmlPartial("_VideoCategories",Model.VideoCategoryModel)%>
        </div>

        <div class="StoreProfileContent" id="StoreProfileVideosNew">

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <!--test asdada!-->
                <div class="row hidden-xs">
                    <div class="col-xs-12 sektorBack">
                    </div>
                </div>
                <div class="alert alert-info tumSektorBack">

                    <%:Model.StoreName+" " %><%:string.IsNullOrEmpty(Model.VideoCategoryModel.SelectedCategoryName) ? "Tüm Ürün" : Model.VideoCategoryModel.SelectedCategoryName+" Ürün" %> Videoları
                </div>
                <%=Html.RenderHtmlPartial("_VideoViewWindow",Model.VideosPage)%>
            </div>

            <div class="clearfix"></div>
            <!-- ./StoreProfileContent -->
        </div>
        <!-- /.col-md-5 main content -->
    </div>


    <%}
    else
    { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>
