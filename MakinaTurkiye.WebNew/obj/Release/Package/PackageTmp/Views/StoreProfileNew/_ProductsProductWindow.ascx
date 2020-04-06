﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTProductsProductListModel>" %>
<%:Html.Hidden("hiddenStoreId",Model.StoreMainPartyId) %>
<%:Html.Hidden("hdnCategoryId",Model.CategoryId) %>

<input type="hidden" id="hdnDisplayType" value="<%:Model.ViewType == 0 ? 1 : Model.ViewType %>" />

<div class="tab-pane fade in active" id="pencere">
    <div class="product-list-mt row clearfix">

        <% foreach (var item in Model.MTProductsPageProductLists.Source.ToList())
            { %>


        <div class="col-md-4 col-lg-3">
            <div class="product-list-mt__item js-hover-product-item">
                <div class="product-list-mt__inner">
                    <div class="row">
                        <div class="col-md-12">


                            <a
                                href="<%=item.ProductUrl %>"
                                title="<%:item.ProductName%>"
                                class="product-list-mt__link">
                                <div class="product-list-mt__image-area">

                                    <img
                                        class="img-thumbnail"
                                        src="<%=item.ProductImagePath.Replace("500x375","200x150")%>"
                                        alt="<%:item.ProductName %>" />
                                    <%if (item.FavoriteProductId > 0)
                                        {%>
                                    <div class="product-list-favorite-icon product-list-favorite-icon-c" onclick="RemoveFavoriteProductItem(<%:item.ProductId %>)" data-pid="<%:item.ProductId %>" data-productid="product-favorite-item-<%:item.ProductId %>" title="Favorilerden Kaldır"><i class="fa fa-heart"></i></div>

                                    <% }
                                        else
                                        {%>
                                    <div data-pid="<%:item.ProductId %>" onclick="AddFavoriteProductItem(<%:item.ProductId %>)" class="product-list-favorite-icon product-list-favorite-icon-c" data-productid="product-favorite-item-<%:item.ProductId %>" title="Favorilere Ekle"><i class="fa fa-heart-o"></i></div>

                                    <%}%>
                                    <%if (item.HasVideo)
                                        {
                                    %>
                                    <div class="product-list-video-icon" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

                                    <%
                                        } %>
                                </div>

                                <div class="product-list-mt__detail">

                                    <div class="product-list-mt__title-info">
                                        <h4 class="product-list-mt__title">
                                            <%:item.ProductName%>
                                        </h4>
                                        <p class="product-list-mt__brand-model">
                                            <%: item.BrandName%>, <%: item.ModelName%>
                                        </p>
                                    </div>
                                    <p class="product-list-mt__price">
                                        <%if (!item.ProductPrice.Contains("Fiyat"))
                                            {
                                        %>

                                    
                     
                                        <span class="<%:!string.IsNullOrEmpty(item.ProductPriceDiscount) ? "old-price" :"" %>">
                                                <i itemprop="priceCurrency" class="<%:item.Currency %>"></i>
                                            <%:item.ProductPrice %></span>

                                                           <%if (!string.IsNullOrEmpty(item.ProductPriceDiscount)) {%>
                                            <i itemprop="priceCurrency" class="<%:item.Currency %>"></i>
                                            <span><%:item.ProductPriceDiscount %></span>
                                        <% } %>
                                        <%}
                                            else
                                            {%>
                                        <span class="interview"></span>
                                        <% } %>
                                    </p>
                                    <p class="product-list-mt__advert-no">
                                        <%:item.ProductNo%>
                                    </p>

                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <% } %>
    </div>
</div>
<div class="row clearfix">
    <div class="col-xs-12">
        <%= Html.RenderHtmlPartial("Paging", Model.MTProductsPageProductLists)%>
    </div>
</div>

