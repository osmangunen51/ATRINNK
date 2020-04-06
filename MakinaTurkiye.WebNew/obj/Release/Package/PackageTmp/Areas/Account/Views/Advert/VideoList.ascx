﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<VideoModel>>" %>
<% byte well = 0; %>
<% foreach (var item in Model)
   { %>
<%well++; %>

<div class="pull-left m5">
    <img class="img-thumbnail" width="110" src="<%= AppSettings.VideoThumbnailFolder + item.VideoPicturePath %>" alt="<%=item.VideoTitle %>" />
    <br>
    <a onclick="DeleteVideo(<%=well %>);" class="mt10">Sil </a>
</div>
<% } %>
<%if (Model.Count < 5)
  {%>
<%for (int i = Model.Count + 1; i <= 5; i++)
  {%>
  <div class="pull-left m5">
    <img class="img-thumbnail" src="https://dummyimage.com/100x66/efefef/000000.jpg&text=Video%20Eklenmedi." alt="Video Eklenmedi."/>
</div>
<% } %>
<% } %>
