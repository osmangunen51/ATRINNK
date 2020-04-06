﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<VideoModel>>" %>
<% int pageDimension = 12;%>
<div class="row hidden-xs">
    <div class="col-xs-12">
        <ol class="breadcrumb breadcrumb-mt">
            <li><a href="/">Anasayfa </a></li>
            <li><a href="/Videolar">Videolar </a></li>
            <li class="active">
                <%:ViewData["CategoryName"]%>
                Sektörüne Ait Videolar </li>
        </ol>
    </div>
</div>
<div class="row">
    <%=Html.RenderHtmlPartial("VideoTop")%>
    <div class="col-sm-12 col-md-4">
        <div class="panel panel-mt2">
            <div class="panel-heading">
                <i class="fa fa-play"></i>&nbsp;&nbsp;Popüler Videolar
            </div>
            <ul class="media-list panel-body">
                <% if (Model.Source.Count == 0)
                   {%>
                Herhangi bir video bulunamadı.
                <%}
                   else
                   {
                       foreach (var model in Model.Source)
                       {
                           var imagePath = AppSettings.VideoThumbnailFolder + model.VideoPicturePath;
                           string name = Helpers.ToUrl(model.ProductName);
                %>
                <li class="media"><a class="pull-left" href="#">
                    <% if (FileHelpers.HasFile(imagePath))
                       { %>
                    <img class="media-object" src="<%:imagePath %>" width="120" alt="<%:model.CategoryName %>" />
                    <%}
                       else
                       {%>
                    <img class="media-object" src="https://dummyimage.com/120x80/efefef/000000.jpg&text=Video Resmi Bulunamadı" alt="...">
                    <%} %>
                </a>
                    <div class="media-body">
                        <div class="media-heading">
                            <a href="/video/<%: model.VideoId%>/<%:name %>" title="<%=model.VideoTitle %>" class="text-info">
                                <%:Helpers.Truncatet( model.ProductName,30) %> </a>
                            <br />
                            <a href="#" class="text-muted text-xs"><%:Helpers.Truncatet( model.StoreName,30) %> </a>
                            <br />
                            <span class="text-muted text-xs"><%:model.SingularViewCount %> görüntüleme </span>
                        </div>
                    </div>
                </li>
                <%}} %>
            </ul>
        </div>
    </div>
</div>
