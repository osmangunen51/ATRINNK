<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTVideoItemViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="<%:ViewBag.Canonical %>" />

    <link href="../../Content/video-js-7.4.1/video-js.min.css" rel="stylesheet" />
    <script src="../../Content/video-js-7.4.1/video.js"></script>


    <script src="/Content/Scripts/VideoDetails/VideoDetail.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row hidden-xs">
        <div class="col-xs-12">
            <%if (ViewData["ViewNavigation"] != null)
                { %>
            <%=ViewData["ViewNavigation"]%>
            <%}%>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-8">
           <%if (Page.RouteData.Values["VideoId"] != null)
        { %>
    <div class="videocontent">
        <video id="vd" class="video-js vjs-default-skin" controls preload="auto" autoplay="autoplay" width="100%"
            height="100%" style="width:100%; height:100%; padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;"
            poster="" data-setup='{"techOrder": ["html5"]}'>
            <source src="https://s.makinaturkiye.com/NewVideos/<%= Model.VideoPath %>.mp4" type='video/mp4' />
        </video>
    </div>
    <%}
        else
        {  %>
    <img class="img-thumbnail" src="https://dummyimage.com/800x400/efefef/000000.jpg&text=video" alt=".." />
    <%} %>

        <%=Html.RenderHtmlPartial("StoreAndProductInf",Model.MTStoreAndProductDetailModel) %>
            </div>
            <div class="visible-xs mb20">
                &nbsp;
            </div>
            <div class="visible-sm mb20">
                &nbsp;
            </div>
           <%=Html.RenderHtmlPartial("_OtherVideos",Model.MTOtherVideosModel)%>
    </div>

    <input id="hdnCategoryId" type="hidden" name="categoryId" value="<%= ViewData["CategoryId"].ToInt32() %>" />
              <script type="text/javascript">
                  var videoItemId = <%=Model.VideoId %>;
                  PopulerVideosRemoveItemVideo(videoItemId);
            </script>
</asp:Content>
