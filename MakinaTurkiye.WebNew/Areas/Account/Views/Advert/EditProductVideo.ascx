﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MakinaTurkiye.Entities.Tables.Videos.Video>>" %>
<% int i = 0; foreach (var item in Model)
    { %>
<div class="pull-left 139155m5" id="<%=item.VideoId %>">
    <!--<img class="img-thumbnail" width="100px" height="100px" style="width: 100px; height: 100px;" src="<%=AppSettings.ProductImageFolder +item.ProductId+"/"+ item.VideoPicturePath %>" alt="..">-->
    <img class="img-thumbnail" width="100px" height="100px" style="width: 100px; height: 100px;"  src="<%="/UserFiles/VideoThumb/"+ item.VideoPicturePath %>" alt="..">
    <br>
    <a class="mt10" onclick="DeleteVideo('<%=item.ProductId %>','<%=item.VideoId %>');">Videoyu Sil </a>
</div>
<% i++;
    } %>
