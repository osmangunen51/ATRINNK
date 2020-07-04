﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeVideoModel>>" %>

<div class="col-xs-12 panel panel-mt hidden-xs popular-videos-home-page">
    <div class="panel-heading text-sm m0">
        <span class="glyphicon glyphicon-play"></span>&nbsp;&nbsp; Popüler Videolar
    </div>
    <div class="list-group list-group-mt2">
        <% foreach (var video in Model)
           {%>
        <a href="<%:video.VideoUrl%>" class="list-group-item popular-videos-home-page__item">
            <div class="popular-videos-home-page__images">
                <img width="236" height="160" src="<%:video.PicturePath%>" alt="<%:video.ProductName %>" />
            </div>
            <p class="popular-videos-home-page__title">
                <%:video.ProductName%>
                <span class="popular-videos-home-page__brand-name"><%:video.BrandName + "  " + video.ModelName%></span>
            </p>

        </a>
        <% } %>
        <div class="list-group-item popular-videos-home-page__bottom">
            <a href="/videolar">Tüm Videolar
            </a>
            <a class="pull-right" href="#">
                <span class="glyphicon glyphicon-stats"></span>
                Top 10
            </a>
        </div>
    </div>
</div>
