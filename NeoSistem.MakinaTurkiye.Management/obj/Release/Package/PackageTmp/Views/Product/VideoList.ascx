﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<Video>>" %>
<% if (Model.Count > 0)
   { %>
<% foreach (var item in Model)
   { %>
<div style="width: 100px; height: 140px; margin-left: 10px; margin-top: 10px; float: left;
  text-align: center;">
  <img src="<%=AppSettings.VideoThumbnailFolder + item.VideoPicturePath %>" width="100" height="100"
    style="border: solid 1px #bababa;" />
  <a style="cursor: pointer; text-decoration: none;" onclick="DeleteVideo('<%=item.VideoId %>');">
    Videoyu Sil</a>
 <%if (item.ShowOnShowcase.HasValue && !item.ShowOnShowcase.Value)
   {%>
      <br /> 
      <hr />
      <a style="cursor: pointer; text-decoration: none;" onclick="DoVideoShowcase('<%=item.VideoId %>');">Vitrin Yap</a>    
  <%}
   else
   { %>
     <br /> 
     <hr />
     Vitrin videosu   
  <%}%>
</div>
<% } %>
<% }
   else
   { %>
<div style="float: left; margin-top: 20px; margin-left: 20px;">
  Herhangi bir video bulunamadı.
</div>
<% } %>
