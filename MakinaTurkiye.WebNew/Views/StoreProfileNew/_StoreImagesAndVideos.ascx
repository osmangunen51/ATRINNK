﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreImageAndVideosModel>" %>
   <div class="row mb20">

					 <div class="col-sm-12">
						  <h4>
							<span class="glyphicon glyphicon-picture"></span>&nbsp;Firma Görselleri
                                     <span class="btn-group pull-right"><a href="#resimler" class="btn btn-md btn-default"
										  data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a><a href="#resimler"
												class="btn btn-md btn-default" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
												</span></a></span>
						  </h4>
						  <hr class="mb0">
						  <div id="resimler" data-interval="false" class="carousel slide" data-ride="carousel">
								<div class="carousel-inner"> 
                                     <%if (Model.StoreImages.ToList().Count > 0) { 
                                         int i = 0;
                                  foreach (var item in Model.StoreImages)
                                {
                                   
                                      if(i==0)
                                      {%>
                                 
                                                 <div class="item active">
                                  <img src="<%:item %>" class="img-thumbnail" style="height:200px; width:auto" alt="<%:Model.StoreName %>">
                                </div>
                                      <%}
                                        else
                                      {%>
                                               <div class="item">
                                    <img src="<%:item %>" style="height:200px; width:auto" alt="<%:Model.StoreName %>">
                                </div>
                                     <%  }
                                

                                }
                                        }else
                                        {%>
                                  Firmaya ait görsel bulunamadı. 
                                    <%} %>
								</div>
						  </div>
					 </div>

					 <div class="col-sm-12 pt20">
						  <h4>
								<span class="glyphicon glyphicon-play"></span>&nbsp;Videolar
                                <%if (Model.TotalVideoPage>1)
                                  { %>
                                    <span class="btn-group pull-right"><a href="#videolar" class="btn btn-md btn-default"
									      data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a><a href="#videolar"
										  class="btn btn-md btn-default" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
										  </span></a></span>
                                  <%}%>
                               
						  </h4>
						  <hr class="mb0">
						  <div id="videolar" data-interval="false" class="carousel slide" data-ride="carousel">
								<div class="carousel-inner">
                                    <%if (Model.TotalVideoPage > 0)
                                      {
                                          for (int i = 0; i < Model.TotalVideoPage; i++)
                                          { 
                                          %>
                                     <div class="item p10 <%:i == 0 ? "active" : ""%>" > 
                                            <%foreach (var item in Model.MTCompanyProfileVideosModels[i].MTCompanyProfileVideosModelsSub.ToList())
                                              {%>
                                                <div class="col-xs-6 col-md-3 p0">
												                <a href="#" data-toggle="modal" data-target="#videocontent" data-videoPath="<%=item.VideoPath%>.mp4">
                                                                     <img src="<%=item.VideoImagePath%>" alt="<%=Model.StoreName%>"  class="img-thumbnail" />
												                </a>
										                    </div>
                                                
                                            <% 
                                              }%> 
                                           </div>
                                      <%}
                                      }
                                      else
                                      {%>
                                        Bu firmaya ait video bulunamadı.
                                      <%} %>
								</div>
						  </div>
					 </div>
				</div>
      <%if (Model.TotalVideoPage> 0)
        {
           %> 
          <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-hidden="true" id="videocontent">
             <div class="modal-dialog modal-lg">
                <div class="modal-content pr">
                     <div class="p0">
                     <span onclick="videoPause();" class="close m10" data-dismiss="modal" aria-hidden="true">
                             &times;</span>
                     </div>
                    <div class="row">
                         <div class="col-sm-2">
                         </div>
                        <div class="col-sm-8 m0 p0 text-center">
                             <div id="video-slider" data-interval="false" class="carousel slide" data-ride="carousel">
                                <div class="carousel-inner"">
                                    <% for (int i = 0; i < Model.TotalVideoPage; i++)
                                       {%>
                                                <div class="item <%:i == 0 ? "active" : ""%>">
                                           <%foreach (var item in Model.MTCompanyProfileVideosModels[i].MTCompanyProfileVideosModelsSub.ToList())
                                           { 
                                           
                                           %> 
                                                 <div class="col-xs-2 m0">
                                                    <div class="thumbnail thumbnail-mt">
                                                        <a class="js-video-slide-item" data-videoId="<%=item.VideoId%>" data-videoPath="<%=item.VideoPath%>.mp4">
                                                            <img src="<%=item.VideoImagePath%>" alt="<%=Model.StoreName%>"  class="img-thumbnail" style="cursor:pointer;" />
                                                        </a>
                                                    </div>
                                                </div>    
                                          
                                           
                                  
                                         <%  }%>
                                         </div> 
                                          <%
                                       }%>
                                </div>
                                 <span class="clearfix"></span>
                                <%if (Model.TotalVideoPage > 1)
                                  { %>
                                   <a class="carousel-left" href="#video-slider" data-slide="prev"><span class="btn btn-sm glyphicon glyphicon-chevron-left"></span></a>
                                   <a class="carousel-right"  href="#video-slider" data-slide="next"><span class="btn btn-sm glyphicon glyphicon-chevron-right"> </span></a>
                              <%} %>
                                </div>
                        </div>
                     </div>
                    <div class="tab-content pr">
                            <div class="videocontent">
                                <video id="vd" class="video-js vjs-default-skin" controls preload="auto" width="100%" 
                                    height="100%" style="padding: 4px; border: 1px solid #ddd; border-radius: 4px;
                                    transition: all .2s ease-in-out; display: inline-block;" poster="" data-setup='{"techOrder": ["html5", "flash"]}'>
		                            <source  id="vds" src="<%= Model.MTCompanyProfileVideosModels[0].MTCompanyProfileVideosModelsSub[0].VideoPath %>.mp4" type='video/mp4' />
		                        </video>
                            </div>
                    </div>
                </div>
             </div>
         </div>
      <%}%>