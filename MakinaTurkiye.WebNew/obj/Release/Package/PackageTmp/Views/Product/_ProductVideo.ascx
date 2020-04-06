﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductTabModel>" %>

<%if (Model.VideoModels.Count > 0)
    { %>
        <div class="row clearfix">
        <div class="col-xs-12">
            <div class="panel panel-mt2">
                <div class="panel-heading p0">
                    <div class="btn-group">
                        <a href="#video" data-toggle="tab" class="active btn btn-md btn-mt"><span style="margin:0; font-size:14px;">Video</span>
                        </a>
                    </div>
                </div>
                <div class="panel-body" style="overflow: hidden;">
                    <div class="tab-content">
                        <div class="tab-pane active" id="video">
                           
                         <div class="row">
                         <div class="col-sm-2">
                         </div>
                        <div class="col-sm-8 m0 p0 text-center">
                             <div id="video-slider" data-interval="false" class="carousel slide" data-ride="carousel">
                                 <div class="carousel-inner"">
                                    <% for (int i = 0; i < Model.VideoModels.Keys.Count; i++)
                                        { %> 
                                            <div class="item <%:i==0? "active": "" %>">
                                             <%foreach (var item in Model.VideoModels[i])
                                                 {%>
                                                 <div class="col-xs-3 m0">
                                                    <div class="thumbnail thumbnail-mt">
                                                        <a class="js-video-slide-item" data-videoId="<%=item.VideoId %>" data-videoPath="<%=item.VideoPath %>">
                                             <img  src="<%=item.VideoPicturePath %>" alt="<%=item.VideoTitle %>"  class="img-thumbnail" />
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
                                   <a class="carousel-right"  href="#video-slider" data-slide="next"><span class="btn btn-sm glyphicon glyphicon-chevron-right"> </span></a>
                              <%} %>
                                </div>
                        </div>
                     </div>
                            <div class="tab-content pr">   
                                <div class="videocontent col-md-8 col-md-push-2">
                                        <video id="vd" class="video-js vjs-default-skin" controls preload="auto" width="100%" height="100%" style="padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;" poster="" data-setup='{"techOrder": ["html5", "flash"]}'>
                                            <source src="<%=Model.VideoModels[0][0] .VideoPath %>" type='video/mp4' />
		                                </video>
                                    </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  <%} %>