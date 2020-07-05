﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTHomeProductsRelatedCategoryModel>" %>

<div class="selected-products">
    <div class="container">
        <div class="row">
         <div class="col-xs-12">
        <h2 class="section-title">
            <span>Seçilen Ürünler</span>
        </h2>
    </div>
        </div>
    </div>

    <div class="container">
        <div class="selected-products-block">
            <div class="row">
                <!-- Products Menu List -->
                <div class="col-md-3 col-xs-12 haha">
                    <div class="selected-products-menu-list">
                        <ul id="myTabs">
                            <%int i = 0; %>
                            <%foreach (var item in Model.Categories)
                              { if(i==0)
                              {%>
                              <li class="active">
                                <a href="#<%:item.CategoryId %>" data-toggle="tab">
                                    <div class="image hidden-xs">
                                        <img src="https://www.makinaturkiye.com/<%:item.CategoryIcon %>" height="40" width="40">
                                    </div>
                                    <div class="text">
                                        <span><%:item.CategoryName %></span>
                                    </div>
                                </a>
                            </li>
                              <%}
                              else
                              {%>
                               <li>
                                <a href="#<%:item.CategoryId %>" data-toggle="tab">
                                    <div class="image hidden-xs">
                                        <img src="https://www.makinaturkiye.com/<%:item.CategoryIcon%>" height="40" width="40">
                                    </div>
                                    <div class="text">
                                        <span><%:item.CategoryName %></span>
                                    </div>
                                </a>
                            </li>
                              <%} i++;
                                  %>
                              <%} %>
                            <li class="view-more hidden-xs">
                                <a href="">... Tümünü Gör</a>
                            </li>
                        </ul>
                    </div>
                </div>
                <!-- Products Menu List -->

                <!-- Products -->
                <div class="col-md-9 col-xs-12" >

                    <div class="selected-products-list-right">

                        <!-- Consumer Electronics -->
                        <% int a = 0;
                           string tabBlockCss = "active";
                           string style = "display:block;";
                           foreach (var item in Model.Categories)
                          { if(a>0)
                          {
                              tabBlockCss ="";
                              style = "";
                          }
                              %>
                                <div id="<%:item.CategoryId %>" class="tab-block <%:tabBlockCss %>" style="<%:style%>">
                            <div class="products-list-item-block">
                                <div class="owl-list-btn-list">
                                    <div class="geri"><i class="fa fa-angle-left fa-3x"></i></div>
                                    <div class="ileri"><i class="fa fa-angle-right fa-3x"></i></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="owl-carousel owl-theme ahmet-1">
                                            <%foreach (var productItem in Model.Products.Where(x=>x.CategoryId==item.CategoryId).Skip(0).Take(10))
                                              {%>
                                               <div class="item">
                                                <div class="urun-box">
                                                    <div class="image pull-left">
                                                        <img src="<%:productItem.PicturePath %>" width="180" height="135">
                                                    </div>

                                                    <div class="text pull-left">
                                                        <div class="products-name">
                                                            <span><%:productItem.ProductName %></span>
                                                        </div>
                                                        <div class="products-price">
                                                            <span><%:productItem.ProductPrice %></span>
                                                        </div>
                                                        <div class="products-benzer-urunler">
                                                            <a href="#"><i class="fa fa-list-ol"></i> benzer ürünler</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                              <%} %>
                                        </div>
                                    </div>


                                    <div class="col-md-12" style="margin-top: 30px;">
                                        <div class="owl-carousel owl-theme ahmet-2">

                                            <%foreach (var productItem in Model.Products.Where(x=>x.CategoryId==item.CategoryId).Skip(10).Take(20))
                                              {%>
                                                                   <div class="item">
                                                <div class="urun-box">
                                                    <div class="image pull-left">
                                                        <img src="images/p1.jpg" width="180" height="135">
                                                    </div>

                                                    <div class="text pull-left">
                                                        <div class="products-name">
                                                            <span><%:productItem.ProductName %></span>
                                                        </div>
                                                        <div class="products-price">
                                                            <span><%:productItem.ProductPrice %><%if (!string.IsNullOrEmpty(productItem.CurrencyCss)) {
                                                                                                  %>
                                                                <i class="<%:productItem.CurrencyCss %>"></i><%} %></span>
                                                        </div>
                                                        <div class="products-benzer-urunler">
                                                            <a href="#"><i class="fa fa-list-ol"></i> benzer ürünler</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                
                                              <%} a++; %>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="div-footer hidden-xs">
                                <!-- Products Links -->
                                <div class="products-footer-block">
                                    <ul class="links">
                        <%--                <li><a href="#">xcookie cuuter</a></li>
                                        <li><a href="#">pizaa cutter</a></li>
                                        <li><a href="#">heat resistant glover</a></li>
                                        <li><a href="#">baby bottle</a></li>
                                        <li><a href="#">ice bucket</a></li>
                                        <li><a href="#">cutting board</a></li>
                                        <li><a href="#">ceramic vase</a></li>--%>
                                    </ul>
                                </div>
                                <!-- Products Links -->
                            </div>
                        </div>
                        <!-- Consumer Electronics -->  
                          <%} %>
                    </div>
                </div>
                <!-- Products -->
            </div>
        </div>
    </div>

</div>
