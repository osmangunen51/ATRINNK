<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<VideoModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <link href="../../Content/video-js-7.4.1/video-js.min.css" rel="stylesheet" />
    <script src="../../Content/video-js-7.4.1/video.js"></script>


    <script src="/Content/Scripts/VideoDetails/VideoDetail.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <% int pageDimension = 12;%>
    <div class="row hidden-xs">
        <div class="col-xs-12">
            <%if (ViewData["ViewNavigation"] != null)
              { %>
            <%=ViewData["ViewNavigation"]%>
            <%}%>
        </div>
    </div>
    <div class="row">
        <%=Html.RenderHtmlPartial("VideoTop")%>
        <%=Html.RenderHtmlPartial("_OtherVideos",Model.SearchModel)%>
    </div>

    <input id="hdnCategoryId" type="hidden" name="categoryId" value="<%= ViewData["CategoryId"].ToInt32() %>" />
</asp:Content>
