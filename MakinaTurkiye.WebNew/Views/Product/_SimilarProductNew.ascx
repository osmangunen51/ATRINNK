<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTSimilarProductModel>" %>
<%if (Model.ProductItemModels.Count > 0)
  {%>
<div class="diger-urunler">
    <div class="urun-bilgisi">
        <div class="panel panel-default urun-alan">
            <div class="panel-heading" style="padding-bottom: 5px;">
                <div class="urunler-link">
                    <div class="row">
                        <div class="col-md-8 col-xs-7">
                            <span>Benzer İlanlar</span>
                        </div>
                        <div class="col-md-4 col-xs-5">
                            <div class="text-right">
                                <a href="<%:Model.AllSimilarProductUrl %>">Tümü</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-body" style="padding-top: 0px;">

                <div class="overflow-carousel owlTemplate maxw stagereset" style="margin-top: -3px;">
                    <div class="owl-carousel bottom-slider">
                        <% foreach (var item in Model.ProductItemModels)
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
                                    <a href="<%:item.ProductUrl %>"><%:item.ProductName %> </a>
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
                   <%--             <div class="product-action text-center" style="display: block">
                                    <a class="label label-danger btnClik btn-k" rel="nofollow" href="<%:item.ProductContactUrl %>"><b>Satıcıyla İletişim Kur </b></a>
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

                <%--    <div class="carousel slide media-carousel" id="media">
                   <div class="carousel-inner">
                               <%decimal pageCount =Math.Ceiling((decimal)Model.ProductItemModels.Count/5);
                                 for (int i = 1; i <= pageCount; i++)
			                            {
                                            int pageSize = 5;

                                            int skip = i * pageSize - pageSize;
                                   
                                            if (i == 1)
                                            {%>
                                                <div class="item active">  
                                            <%}
                                            else { %> <div class="item">  
                                                    <%}
                                            %>
                                                 <div class="urun-item-s">
                                                  <div class="row">
                                        <%
                                            foreach (var item in Model.ProductItemModels.Skip(skip).Take(pageSize))
                                            {%>
                                                          <div class="col-md-2 col-sm-2 col-xs-6">
                              <div class="urun-tablo">
                                <div class="urun-res">
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
                                <div class="urun-icerik">
                                  <a href="<%:item.ProductUrl %>"><%:item.ProductName %> </a>
                                  <div class="ozet-alan">
                                    <div class="ozet"><%:item.BrandName %>,<%:item.BrandName %></div>
                                    <div class="fiyat">
                                                    <%if (!string.IsNullOrEmpty(item.Price))
                                      {%>
                                        <i class="<%=item.CurrencyCss %>"></i>
                                         <%:item.Price%>
                                <%}
                                else {%>
                                   Fiyat Sorunuz
                                 <% } %>
                                  
                                    </div>
                                  </div>
                                </div>
                                <div class="text-center">
                                  <a class="label label-danger btnClik btn-k" href="javaScript:void(0);" onclick="return PopupCenter('Category/ProductContact?productId=<%:item.ProductId %>','Satıcı ile iletişime geç',900,540);"> <b>Satıcıyla İletişim Kur </b> </a>
                                </div>
                              </div>
                            </div> 
                                            <%}   %>
                              </div></div></div>
                                                    <%} %>
                   
                        </div>  
                     <div class="butonlar">
                       <a data-slide="prev" href="#media" class="left carousel-control">‹</a>
                       <a data-slide="next" href="#media" class="right carousel-control">›</a>
                     </div>
                 </div>
              </div>--%>
            </div>
        </div>
    </div>
</div>
<%} %>
