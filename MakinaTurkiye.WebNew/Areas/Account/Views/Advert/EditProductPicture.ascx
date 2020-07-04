﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<PictureModel>>" %>
<% int i = 0; foreach (var item in Model) { %>
<div class="pull-left m5" id="<%=item.PictureId %>">
    <img class="img-thumbnail" width="100px" height="100px" style="width: 100px; height: 100px;"
        src="//s.makinaturkiye.com/Product/<%=item.ProductId+"/"+ item.PicturePath %>"
        alt="..">
    <br>
    <a class="mt10" style="cursor:pointer;" onclick="DeletePicture('<%=item.PictureId %>','<%=item.PicturePath %>');">Sil </a>
    <% if (i != 0) { %>
    <a class="mt10" style="cursor:pointer;" onclick="mainImage('<%=item.ProductId %>','<%=item.PicturePath %>');">Ana Resim Seç</a>
    <% } %>
</div>
<% i++;
    } %>
