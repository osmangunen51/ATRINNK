﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MTPopularVideoModel>>" %>
<div class="panel panel-mt2 hidden-xs">
    <div class="panel-heading">
        <i class="fa fa-play"></i>&nbsp;&nbsp;Popüler Videolar
    </div>
   
    <ul class="media-list panel-body">
        <%foreach (var item in Model)
          {  %>
        <li class="media"><a class="pull-left" href="<%:item.VideoUrl %>" title="<%:item.CategoryName %>">
            <img class="media-object" width="80" height="60" src="<%:item.PicturePath %>" alt="<%:item.ProductName %>" />
        </a>
            <div class="media-body">
                <div class="media-heading">
                    <a href="<%:item.VideoUrl %>" class="text-info">
                        <%:item.ProductName%></a>
                    <br />
                    <a href="<%:item.VideoUrl %>" class="text-muted text-xs">
                        <%:item.TruncatetStoreName%></a>
                    <br />
                    <span class="text-muted text-xs">
                        <%:item.SingularViewCount%> görüntüleme
                    </span>
                </div>
            </div>
        </li>
        <%} %>
    </ul>
</div>
