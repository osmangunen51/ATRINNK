<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<style type="text/css">
    .satici-iletisim { font-size: 15px; display: inline-block; }

    @media(min-width: 768px) {
        .satici-iletisim { font-size: 13px; margin-top: 5px; }
    }


    @media(max-width:767px) {
        .product-list-v2__name { height: auto !important; font-size: 18px; }

        .product-list-v2__item .item-price { text-align: left !important; }
    }

    @media( min-width: 991px) {
        .product-list-v2__item .item-price { margin-top: 18px; margin-bottom: 20px; }
    }
</style>
<div class="product-list-mt row clearfix">
    <%
        int i = 0;
        int otherIndexNumber = 0;
    %>
    <% foreach (var model in Model.CategoryProductModels)
        { %>


    <%
        ++i;
        if (otherIndexNumber != 0 && otherIndexNumber % 3 == 0 && ((otherIndexNumber - 1) / 3) < Model.BannerModels.Count)
        {%>

    <%}

    %>
    <%string productUrl = MakinaTurkiye.Utilities.HttpHelpers.UrlBuilder.GetProductUrl(model.ProductId, model.ProductName); %>

    <% string s = model.ModelYear.ToString();
        string Marka = (model.BrandName != null || model.BrandName != "") ? model.BrandName : "";
        string Modeli = (model.ModelName != null) ? ("" + model.ModelName) : "";
    %>
    <div class="col-md-4 col-lg-3">
        <div class="product-list-mt__item js-hover-product-item">
            <div class="product-list-mt__inner">
                <a
                    href="<%=productUrl %>"
                    title="<%:model.ProductName%>"
                    class="product-list-mt__link clearfix">
                    <div class="product-list-mt__image-area">
                        <% if (!string.IsNullOrEmpty(model.PicturePath))
                            {
                                if (i < 5)
                                {%>
                        <img
                            class="img-thumbnail"
                            src="<%=model.PicturePath.Replace("160x120","200x150") %>"
                            alt="<%:model.ProductName %>"
                            title="<%:model.ProductName %>" />

                        <% }
                            else
                            {%>

                        <img
                            class="img-thumbnail img-lazy"
                            src="/UserFiles/image-loading.png"
                            data-src="<%=model.PicturePath.Replace("160x120","200x150") %>"
                            alt="<%:model.ProductName %>"
                            title="<%:model.ProductName %>" />
                        <% } %>


                        <%} %>
                        <%else
                            { %>
                        <img
                            class="img-thumbnail broken-image"
                            src="/UserFiles/image-loading.png"
                            data-src="<%=model.PicturePath %>"
                            title="<%:model.ProductName%>"
                            alt="<%:model.ProductName%>" />
                        <%} %>
                        <%if (model.FavoriteProductId > 0)
                            {%>
                        <div class="product-list-favorite-icon product-list-favorite-icon-c" onclick="RemoveFavoriteProductItem(<%:model.ProductId %>)" data-pid="<%:model.ProductId %>" data-productid="product-favorite-item-<%:model.ProductId %>" title="Favorilerden Kaldır"><i class="fa fa-heart"></i></div>

                        <% }
                            else
                            {%>
                        <div data-pid="<%:model.ProductId %>" onclick="AddFavoriteProductItem(<%:model.ProductId %>)" class="product-list-favorite-icon product-list-favorite-icon-c" data-productid="product-favorite-item-<%:model.ProductId %>" title="Favorilere Ekle"><i class="fa fa-heart-o"></i></div>

                        <%}%>
                        <%if (model.HasVideo)
                            {
                        %>
                        <div class="product-list-video-icon" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

                        <%
                            } %>
                    </div>

                    <span class="product-list-mt__advert-no product-list-mt__advert-no--mobile"><%:model.ProductNo %></span>

                    <div class="product-list-mt__detail">

                        <div class="product-list-mt__title-info">
                            <div class="product-list-mt__title">
                                <%:model.ProductName%>
                            </div>
                            <p class="product-list-mt__brand-model">
                                <%: Marka%>, <%: Modeli%>
                            </p>
                        </div>



                        <p class="product-list-mt__price">

                            <%if (string.IsNullOrEmpty(model.Price) || (model.ProductPriceType != (byte)ProductPriceType.Price && model.ProductPriceType != (byte)ProductPriceType.PriceRange))
                                {%>
                            <span class="interview"></span>
                            <%}
                                else
                                {
                                    string priceCss = "";
                                    if (model.CurrencyCss == "")
                                        priceCss = "interview";
                            %>

                            <i itemprop="priceCurrency" class="<%=model.CurrencyCss %> <%:!string.IsNullOrEmpty(model.ProductPriceWithDiscount) ? "old-price" : "" %>"></i>

                            <% if (model.Price != "Fiyat Sorunuz")
                                {%>
                            <span class="<%:!string.IsNullOrEmpty(model.ProductPriceWithDiscount) ? "old-price" : "" %>"><%:model.Price%></span>
                            <%if (!string.IsNullOrEmpty(model.ProductPriceWithDiscount))
                                {%>
                            <i itemprop="priceCurrency" class="<%=model.CurrencyCss %>"></i>
                            <span><%:model.ProductPriceWithDiscount %></span>
                            <% } %>
                            <% }
                                else
                                { %>
                            <span class="interview"></span>
                            <% } %>
                            <%if (!string.IsNullOrEmpty(model.Price))
                                { %>

                            <small style="display: block; font-weight: 500; padding-left: 18px; font-size: 10px;"><%:model.KdvOrFobText %></small>
                            <%} %>
                            <%} %>
                        </p>


                        <p class="product-list-mt__advert-no"><%:model.ProductNo %></p>

                        <div class="product-list-mt__features">

                            <%if (model.Doping)
                                {%>
                            <span class="product-list-mt__has-doping">D</span>
                            <%} %>
                        </div>
                    </div>

                </a>
                <div class="product-list-mt__footer">
                    <a
                        href="<%:model.StoreUrl %>"
                        title="<%:model.StoreName %>"
                        class="product-list-mt__store"
                        data-storename="<%:model.StoreName %>"></a>
                </div>
            </div>
        </div>
    </div>

    <%otherIndexNumber++; %>

    <%  } %>
</div>
<%= Html.RenderHtmlPartial("_ProductPaging", Model.PagingModel)%>



