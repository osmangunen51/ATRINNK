<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTPopularProductsModel>>" %>
<%if (Model.ToList().Count > 0)
    { %>
<div id="StoreProfileNewPopularProducts" class="owl-carousel owl-theme">
    <% 
        int index = 1;
        foreach (var item in Model.ToList())
        {
            if (index < 7)
            {
                index++; %>
    <div class="PopularProductsItem">
        <div class="thumbnail thumbnail-mt">
            <a href="<%= item.ProductUrl %>">
                <img src="<%:item.ProductImagePath %>" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=item.ProductName%>" />
<%--                <%if (item.FavoriteProductId > 0)
                    {%>
                <div class="product-list-favorite-icon product-list-favorite-icon-c" onclick="RemoveFavoriteProductItem(<%:item.ProductId %>)" data-pid="<%:item.ProductId %>" data-productid="product-favorite-item-<%:item.ProductId %>" title="Favorilerden Kaldır"><i class="fa fa-heart"></i></div>

                <% }
                    else
                    {%>
                <div data-pid="<%:item.ProductId %>" onclick="AddFavoriteProductItem(<%:item.ProductId %>)" class="product-list-favorite-icon product-list-favorite-icon-c" data-productid="product-favorite-item-<%:item.ProductId %>" title="Favorilere Ekle"><i class="fa fa-heart-o"></i></div>

                <%}%>--%>
                <%if (item.HasVideo)
                    {
                %>
                <div class="product-list-favorite-icon" style="left:0px;" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

                <%
                    } %>
            </a>
            <!-- /.thumnail.thumbnail-mt -->
        </div>
        <div class="caption text-left">
            <a href="<%=item.ProductUrl %>"><span>
                <%:item.ProductName%></span> </a>
            <span style="color: #396; font-size: 12px;">
                <%if (!string.IsNullOrEmpty(item.BrandName))
                    {%>
                <%=item.BrandName %>
                <% } %>
                <%if (!string.IsNullOrEmpty(item.ModelName))
                    {%>
                <%=item.ModelName %>
                <% } %>
            </span>

            <div class="interview" style="color: #333">
                <%if (!string.IsNullOrEmpty(item.CurrencyCodeName))
                    { %>
                <span class="<%:!string.IsNullOrEmpty(item.ProductPriceWithDiscount) ? "old-price" : "" %>">
                <i class="<%:item.CurrencyCodeName %>"></i>
                <%} %>
                <%=item.PriceText %>
                    </span>
                <%if (!string.IsNullOrEmpty(item.ProductPriceWithDiscount)) {%>
                    <span>
                           <i class="<%:item.CurrencyCodeName %>"></i>
                        <%:item.ProductPriceWithDiscount %>
                    </span>
                <% } %>
            </div>
            <%--
                                  <div class="small-gray">
                                        <span style="display: block"><%:item.BrandName %></span>
                                        <span><%:item.ModelName %></span>
                                    </div>
												    <b><i class="fa fa-turkish-lira"></i><% model.ProductPrice.ToString(); %>
                                </b>--%>

            <!-- /.caption -->
        </div>
        <!-- /.PopularProductsItem -->
    </div>

    <% }
            else break;
        } %>
    <!-- /#StoreProfileNewPopularProducts -->
</div>
<% } %>
