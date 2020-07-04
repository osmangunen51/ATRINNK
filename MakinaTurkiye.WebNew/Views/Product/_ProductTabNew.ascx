﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<MTProductTabModel>" %>

<%if (!string.IsNullOrEmpty(Model.ProductName))
    {%>

<div class="">
    <!-- URUN BILGI TAB ALAN -->

    <div class="urun-aciklama">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#aciklama">Açıklama</a></li>
            <%if (Model.VideoModels.Count > 0)
                { %>
            <li><a data-toggle="tab" href="#video">Video</a></li>
            <%} %>
            <%if (!string.IsNullOrEmpty(Model.MapCode))
                {%>
            <li id="map"><a data-toggle="tab" href="#konum">Konum</a></li>

            <%} %>
            <%if (Model.MTProductTechnicalInfoItems.Count > 0)
                {%>

            <li><a data-toggle="tab" href="#teknik">Teknik Bilgiler</a></li>
            <% } %>
            <%if (Model.MTProductCatologs.Count > 0)
                {%>
            <li><a data-toggle="tab" href="#catolog">Dosyalar</a></li>
            <% } %>
            <li><a data-toggle="tab" href="#comments">Yorumlar <span class="badge badge-info" style="background-color: #fc8120;"><%:Model.MTProductComment.TotalProductComment %></span></a></li>
            <%if (Model.Certificates.Count > 0)
                {%>
            <li><a data-toggle="tab" href="#certificates">Sertifikalar <span class="badge badge-info" style="background-color: #fc8120;"><%:Model.Certificates.Count%></span></a></li>

            <% } %>
        </ul>
        <div class="tab-content">
            <div id="aciklama" class="clearfix tab-pane fade in active">
                <p>
                    <% if (!string.IsNullOrWhiteSpace(Model.ProductDescription))
                        { %>
                    <%=Model.ProductDescription%>
                    <% }
                        else
                        { %>
                                     Bu ilana açıklama eklenmemiştir.
                            <% } %>
                </p>
                <%=Model.ProductSeoDescription %>

<%--                <%if (Model.ProductKeywords.Count > 0)
                    {%>
                <div class="product-keywords">
                    <%foreach (var keyword in Model.ProductKeywords)
                        {%>
                    <span class="product-keywords-item"><%:keyword.Keyword %></span>
                    <% } %>
                </div>
                <% } %>--%>

                <div style="font-size: 12px; margin-top: 20px;">Bu ilan <b style="font-size: 14px;"><%=Model.ProductViewCount %></b> kez incelenmiştir.</div>

            </div>
            <%if (Model.VideoModels.Count > 0)
                { %>
            <div id="video" style="min-height: 500px;" class="tab-pane fade">


                <div class="col-sm-8 m0 p0 text-center">
                    <div id="video-slider" data-interval="false" class="carousel slide" data-ride="carousel">
                        <div class="carousel-inner">
                            <% for (int i = 0; i < Model.VideoModels.Keys.Count; i++)
                                { %>
                            <div class="item <%:i==0? "active": "" %>">
                                <%foreach (var item in Model.VideoModels[i])
                                    {%>
                                <div class="col-xs-3 m0">
                                    <div class="thumbnail thumbnail-mt">
                                        <a class="js-video-slide-item" data-videoid="<%=item.VideoId %>" data-videopath="<%=item.VideoPath %>">
                                            <img src="<%=item.VideoPicturePath %>" alt="<%=item.VideoTitle %>" class="img-thumbnail" />
                                        </a>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                            <%}%>
                        </div>
                        <span class="clearfix"></span>
                        <%if (Model.VideoModels.Keys.Count > 1)
                            { %>
                        <a class="carousel-left" href="#video-slider" data-slide="prev"><span class="btn btn-sm glyphicon glyphicon-chevron-left"></span></a>
                        <a class="carousel-right" href="#video-slider" data-slide="next"><span class="btn btn-sm glyphicon glyphicon-chevron-right"></span></a>
                        <%} %>
                    </div>
                </div>


                <div class="videocontent col-md-8">
                    <video id="vd" class="video-js vjs-default-skin" controls preload="auto" width="100%" height="100%" style="padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;" poster="" data-setup='{"techOrder": ["html5", "flash"]}'>
                        <source src="<%=Model.VideoModels[0][0] .VideoPath %>" type='video/mp4' />
                    </video>
                </div>

            </div>
            <%} %>
            <%if (!string.IsNullOrEmpty(Model.MapCode))
                {%>
            <div id="konum" class="clearfix tab-pane fade">
                <div style="width: 100%; overflow: hidden; height: 562px">
                    <input id="hiddenMapSrc" type="hidden" value="<%=Model.MapCode %> &amp;num=1&amp;ie=UTF8&amp;t=m&amp;z=14&amp;iwloc=A&amp;output=embed" />
                    <iframe id="mainMap" height="723" width="100%" frameborder="0" scrolling="no" marginheight="0"
                        marginwidth="0" style="border: 0; margin-top: -150px;"></iframe>
                </div>
            </div>
            <%}%>
            <%if (Model.MTProductTechnicalInfoItems.Count > 0)
                {%>
            <div id="teknik" class="clearfix tab-pane fade" style="font-size: 15px;">
                <%foreach (var item in Model.MTProductTechnicalInfoItems.ToList())
                    {%>
                <div class="col-md-4">
                    <span style="color: #ccc;" class="glyphicon glyphicon-check"></span><b><%:item.DisplayName %></b>:<%:item.Value %>
                </div>
                <%} %>
            </div>
            <% } %>
            <%if (Model.MTProductCatologs.Count > 0)
                {%>
            <div id="catolog" class="clearfix tab-pane fade" style="font-size: 15px;">
                <div style="width: 100%; overflow: hidden; height: 562px">

                    <div class="row">
                        <%foreach (var item in Model.MTProductCatologs)
                            {%>
                        <div class="col-xs-4 col-md-3">
                            <div class="thumbnail thumbnail-mt">
                                <a target="_blank" href="<%:item.FilePath%>" class="pdf-icon-link">
                                    <img class="pdf-icon" src="/Content/V2/images/pdf-icon.png" /></a>
                                <div class="caption-pdf">
                                    <%= Model.ProductName %>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>

            <% } %>
            <div id="comments" class="clearfix tab-pane fade" style="font-size: 15px;">
                <%=Html.RenderHtmlPartial("_ProductComment",Model) %>
            </div>
            <%if (Model.Certificates.Count > 0)
                { %>
            <div id="certificates" class="clearfix tab-pane fade" style="font-size: 15px;">
                <div class="row">
                    <div class="col-md-12 certificate-popup-gallery">

                        <%foreach (KeyValuePair<string, string> entry in Model.Certificates)
                            {%>
                        <div class="col-md-3">
                            <div style="height: 280px;">
                                <a class="img-cerficate-link" href="<%:entry.Value.Replace("-500x800","_certificate")%>" title="<%:entry.Key %>">
                                    <img class="img-responsive" style="border: 1px solid #ccc; height: 100%; -webkit-box-shadow: -2px 4px 7px -3px #000000; box-shadow: -2px 4px 7px -3px #000000;" src="<%:entry.Value.Replace("png","jpg") %>" alt="<%:entry.Key %>" />
                                </a>
                            </div>
                            <p style="font-size: 14px; margin-top: 5px;"><%:entry.Key %></p>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
</div>


<% } %>