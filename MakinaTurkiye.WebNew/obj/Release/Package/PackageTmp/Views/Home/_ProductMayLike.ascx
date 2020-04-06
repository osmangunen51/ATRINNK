﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTMayLikeProductModel>" %>

<%if (Model.Products.Count > 0)
  {%>
<div class="diger-urunler">
        <div class="col-xs-12">
        <h2 class="section-title">
            <span>İlginizi Çekebilecek İlanlar</span>
        </h2>
    </div>
    <div class="urun-bilgisi">
        <div class="panel panel-default urun-alan category-popular-product">
   
            <div class="panel-body">

                <div class="overflow-carousel owlTemplate maxw stagereset" style="margin-top: -3px;">
                    <div class="owl-carousel bottom-slider">
                        <% foreach (var item in Model.Products)
                           {   %>
                        <div class="item">
                            <div class="product-card-02" style="margin: 10px; min-height:280px;">
                                <div class="product-image">
                                    <a href="<%:item.ProductUrl %>?ref=suggestionhome">
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
                                    <a href="<%:item.ProductUrl %>?ref=suggestionhome"><%:item.ProductName %> </a>
                                    <span><%:item.BrandName %> <%:item.ModelName %></span>
                                </div>
                                <div class="product-details clearfix">
                                    <div class="product-price">
                                        <%if (!string.IsNullOrEmpty(item.Price))
                                          {%>
                                        <i class="<%=item.CurrencyCss %>"></i>
                                        <%if (item.Price == "Fiyat Sorunuz")
                                            {%>
                                            <span class="interview"></span>
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

                                   <p class="product-id">İlan No: <%: item.ProductNo %></p>
                                </div>
                        
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
