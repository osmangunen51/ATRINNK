﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTSimilarProductModel>" %>
<%if (Model.ProductItemModels.Count > 0)
  {%>
<div class="diger-urunler">
    <div class="urun-bilgisi">
        <div class="panel panel-default urun-alan">
            <div class="panel-heading" style="padding-bottom: 5px;">
                <div class="urunler-link">
                    <div class="row">
                        <div class="col-md-8 col-xs-7">
                            <span>Benzer Ürünler</span>
                        </div>
                        <div class="col-md-4 col-xs-5">
                            <a href="<%=Model.AllSimilarProductUrl %>" class="label text-sm label-success pull-right">Tümünü Göster </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-body" style="padding-top: 0px;">

                <div class="overflow-carousel owlTemplate maxw stagereset" style="margin-top: -3px;">
                    <div class="owl-carousel bottom-slider">
                        <% foreach (var item in Model.ProductItemModels.OrderByDescending(p=>p.ViewCount))
                          {   %>
                        <div class="item">
                            <div class="product-card-02" style="margin: 10px;">
                                <div class="product-image">
                                    <a href="<%:item.ProductUrl %>">
                                        <%  
                                          if (!string.IsNullOrEmpty(item.SmallPicturePah))
                                          {
                                        %>
                                        <img alt="<%=item.SmallPictureName %>" src="<%=item.SmallPicturePah %>" title="<%=item.SmallPictureName %>">
                                        <% 
                                          }
                                          else
                                          { %>
                                        <img src="https://dummyimage.com/150x100/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=Html.Truncate(item.ProductName, 80)%>" />
                                        <% }
                                        %> 
                                    </a>
                                </div>
                                <div class="product-content" style="min-width: 180px;">
                                    <a href="<%:item.ProductUrl %>"><%:item.ProductName %></a>
                                </div>
                                <div class="product-details clearfix">
                                    <div class="small-black">
                                        <span style="display: block"><%:item.BrandName %></span>
                                        <span><%:item.ModelName %></span>
                                    </div>

                                    <div class="product-price">
                                        <%if (!string.IsNullOrEmpty(item.Price))
                                          {%>
                                        <i class="<%=item.CurrencyCss %>"></i>
                                        <%if (item.Price == "Fiyat Sorunuz")
                                            {%>
                                            <span style="font-size:13px">
                                                <%:item.Price %>
                                            </span>
                                        <% }
                                             else {%>
                                                   <%:item.Price%> 
                                        <% } %>
                                        
                                
                                        <%}%>
                              <%--            <%else
                                          {%>
                                   Fiyat Sorunuz
                                 <% } %>--%>
                                    </div>
                                </div>
                         <%--       <div class="product-action text-center" style="display: block">
                                    <a class="label label-danger btnClik btn-k" href="<%:item.ProductContactUrl %>"><b style="font-size: 13px">Satıcıyla İletişim Kur </b></a>
                                </div>--%>
                            </div>
                        </div>
                        <%} %>
                    </div>
                    <a class="left  overflow-prev">
                        <div><i class="fa fa-angle-left fa-3x"></i></div>
                    </a>
                    <a class="left  overflow-next">
                        <div><i class="fa fa-angle-right fa-3x"></i></div>
                    </a>

                </div>
            </div>
        </div>
    </div>
</div>
 
<%} %>
