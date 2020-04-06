<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTVideoModel>" %>

    <%if (Page.RouteData.Values["VideoId"] != null)
        { %>
    <style>
        /*.videocontent {
            width: 100%;
            max-width: 755px;
            margin: 0 auto;
            min-width: 230px;
            height: 400px;
            max-height: 400px;
        }*/
    </style>
    <div class="videocontent">
        <video id="vd" class="video-js vjs-default-skin" controls preload="auto" autoplay="autoplay" width="100%"
            height="100%" style="padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;"
            poster="" data-setup='{"techOrder": ["html5", "flash"]}'>
            <source src="https://s.makinaturkiye.com/NewVideos/<%= Model.VideoPath %>.mp4" type='video/mp4' />
        </video>
    </div>
    <%}
        else
        {  %>
    <img class="img-thumbnail" src="https://dummyimage.com/800x400/efefef/000000.jpg&text=video" alt=".." />
    <%} %>
  