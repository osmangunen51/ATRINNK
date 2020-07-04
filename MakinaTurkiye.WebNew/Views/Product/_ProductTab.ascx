﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<MTProductTabModel>" %>

<%if (!string.IsNullOrEmpty(Model.ProductName))
    {%>
<div class="row clearfix">
    <div class="col-xs-12">
        <div class="panel panel-mt2">
            <div class="panel-heading p0">
                <div class="btn-group">
                    <a href="#aciklama" data-toggle="tab" class="active btn btn-md btn-mt"><span style="margin:0; font-size:14px;">İlan Detayları</span></a>
                    <%if (!string.IsNullOrEmpty(Model.MapCode))
                        {%>
                    <a href="#konum" data-toggle="tab" class="btn btn-md btn-mt" id="map"><span style="margin:0; font-size:14px;">Konum</span></a>
                    <%}%>
                    <%if (Model.MTProductCatologs.Count > 0) {%>
                          <a href="#catolog" data-toggle="tab" class="btn btn-md btn-mt" id="catolog1"><span style="margin:0; font-size:14px;">Dosyalar</span></a>
                    <% } %>
                </div>
                <div class="btn btn-md btn-mt2 disabled pull-right">
                    <span class="glyphicon glyphicon-eye-open"></span>
                    <%:Model.ProductViewCount%>
                </div>
            </div>
            <div class="panel-body" style="overflow: hidden;">
                <div class="tab-content">
                    <div class="tab-pane active" id="aciklama">
                        <h2>
                            <%:Model.ProductName%></h2>
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
                   
                    </div>
                     <%if (!string.IsNullOrEmpty(Model.MapCode))
                            { %>
                        <div class="tab-pane" id="konum">
                            <div style="width: 100%; overflow: hidden; height: 562px">
                                <input id="hiddenMapSrc" type="hidden" value="<%=Model.MapCode %> &amp;num=1&amp;ie=UTF8&amp;t=m&amp;z=14&amp;iwloc=A&amp;output=embed" />
                                <iframe id="mainMap" height="723" width="100%" frameborder="0" scrolling="no" marginheight="0"
                                    marginwidth="0" style="border: 0; margin-top: -150px;"></iframe>
                            </div>
                        </div>
                    <%} %>
                    <%if (Model.MTProductCatologs.Count > 0) {%>
                                     <div class="tab-pane" id="catolog">
                            <div style="width: 100%; overflow: hidden; height: 562px">
                                <%foreach (var item in Model.MTProductCatologs)
                                    {%>
                                         <a href="<%:item.FilePath %>" target="_blank"><%:Model.ProductName %> Katoloğu</a>
                                    <%} %>
                            </div>
                        </div>
                            
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</div>
<%} %>
