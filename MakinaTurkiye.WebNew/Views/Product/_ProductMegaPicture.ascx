﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTProductPictureModel>>" %>

<%if (Model.Count > 0)
    {%>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-hidden="true" id="megafotoContent">
         <div class="modal-dialog modal-lg">
        <div class="modal-content pr">
            <div class="p0">
                <button type="button" class="close m10" data-dismiss="modal" aria-hidden="true">
                    &times;</button>
            </div>
            <div class="row">
                <div class="col-sm-2">
                </div>
                <div class="col-sm-8 m0 p0 text-center">
                    <div id="megafoto-slider" data-interval="false" class="carousel slide" data-ride="carousel">
                        <div class="carousel-inner">
                            <%
                                var activePictures = Model.Take(6);%>
                            <div class="item active">
                                <%if (activePictures != null && activePictures.Count() > 0)
                                    {
                                        int activePoint = 0;
                                        foreach (var image in activePictures)
                                        {%>
                                   
                                <div class="col-xs-2 m0">
                                    <div class="thumbnail thumbnail-mt">
                                        <a href="#megaresim-<%:activePoint %>" data-toggle="tab">
                                            
                                            <img  src="<%=image.SmallPath %>" alt="<%=image.ProductName %>" title="<%=image.ProductName %>"/>
                                        </a>
                                    </div>
                                </div>
                           
                                    <%  activePoint++;
                                        }
                                    }%>
                            </div>
                            <%var inactivePictures = Model.Skip(6);%>
                            <div class="item">
                                <%if (inactivePictures != null && inactivePictures.Count() > 0)
                                    {
                                      
                                        int inactivePoint = 6;
                                        foreach (var image in inactivePictures)
                                        {%>   
                                <div class="col-xs-2 m0">
                                    <div class="thumbnail thumbnail-mt">
                                        <a href="#megaresim-<%:inactivePoint %>" data-toggle="tab">
                                            <img  src="<%=image.SmallPath %>" alt="<%=image.ProductName %>" title="<%=image.ProductName %>"/>
                                        </a>
                                    </div>
                                </div>
                             
                                <% inactivePoint++;
                                        }
                                    }%>
                            </div>
                        </div>
                        <% if (Model.Count > 6)
                            {%>
                        <span class="clearfix"></span><a class="carousel-left" href="#megafoto-slider" data-slide="prev">
                            <span class="btn btn-sm glyphicon glyphicon-chevron-left"></span></a><a class="carousel-right"
                                href="#megafoto-slider" data-slide="next"><span class="btn btn-sm glyphicon glyphicon-chevron-right"></span></a>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="tab-content pr">
                <div id="megaresim-0" class="tab-pane active">
                    
                    <img src="<%:Model.First().MegaPicturePath %>" alt="<%:Model.First().ProductName %>" title="<%:Model.First().ProductName %>" class="lazy w100"/>
                 
                </div>
                <%for (int i = 1; i < Model.Count; i++)
                    { %>
                     <div id="megaresim-<%:i %>" class="tab-pane ">
                      
                        <img src="<%:Model[i].MegaPicturePath %>" alt="<%:Model[i].ProductName%> <%:i+1 %>" title="<%:Model[i].ProductName %> <%:i+1 %>" class="lazy w100"/>
                       
                    </div>
                <%} %>
                <div class="carousel-left2">
                    <a href="#mtclick" class="prev-tab btn btn-lg btn-default"><span class="glyphicon glyphicon-chevron-left"></span></a>
                </div>
                <div class="carousel-right2">
                    <a href="#mtclick" class="next-tab btn btn-lg btn-default"><span class="glyphicon glyphicon-chevron-right"></span></a>
                </div>
            </div>
        </div>
    </div>
    </div>

<%} %>
