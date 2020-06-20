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
<div class="product-list-mtgrid row clearfix">
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
    <div class="col-md-12">
        <div class="product-list-mtgrid__item">
            <div class="product-list-mtgrid__media">
                <a
                    href="<%=productUrl %>"
                    title="<%:model.ProductName%>">
                    <% if (!string.IsNullOrEmpty(model.PicturePath))
                        {
                    %>
                    <img
                        class="img-thumbnail"
                        src="<%=model.PicturePath.Replace("160x120","200x150") %>"
                        alt="<%:model.ProductName %>"
                        title="<%:model.ProductName %>" />




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

                </a>
            </div>
            <div class="product-list-mtgrid__body">
                <div class="product-list-mtgrid__content">
                    <a
                        href="<%=productUrl %>"
                        title="<%:model.ProductName%>"
                        class="product-list-mt__link clearfix">
                        <div class="product-list-mt__title-info">
                            <h3 class="product-list-mtgrid__title">
                                <%:model.ProductName%>
                            </h3>

                            <p class="product-list-mtgrid__brandmodel">
                                <%: Marka%> | <%: Modeli%>
                            </p>
                        </div>
                    </a>

                    <span class="product-list-mt__advert-no product-list-mt__advert-no--mobile"><%:model.ProductNo %></span>

                    <div class="product-list-mtgrid__detail hidden-xs">
                        <div class="product-list-mtgrid__info">
                            <%if (!string.IsNullOrEmpty(model.ProductSalesTypeText) && model.ProductSalesTypeText.Contains(","))
                                {
                            %>
                            <div class="product-list-mtgrid__item-col">
                                <div class="product-list-mtgrid__infotitle">Satış Detayı:</div>
                                <ul class="product-list-mtgrid__info-list">

                                    <%var productSalesTypeTexts = model.ProductSalesTypeText.Split(','); %>
                                    <%foreach (var item in productSalesTypeTexts)
                                        {%>
                                             <li><%:item %></li>
                                        <%} %>
                           
                 
                                </ul>
                            </div>
                            <%
                                } %>

            <%--                <div class="product-list-mtgrid__item-coll">
                                <div class="product-list-mtgrid__infotitle">Kısa Detay:</div>
                                <ul class="product-list-mtgrid__info-list">
                                    <li>2 yıl Garantili</li>
                                </ul>
                            </div>--%>
                            <div class="product-list-mtgrid__item-col">
                                <div class="product-list-mtgrid__infotitle">Ürün Durumu:</div>
                                <ul class="product-list-mtgrid__info-list">
                                    <li><%:model.ProductStatuText %></li>
                                </ul>
                            </div>
                            <div class="product-list-mtgrid__item-col">
                                <div class="product-list-mtgrid__infotitle">Ürün Tipi:</div>
                                <ul class="product-list-mtgrid__info-list">
                                    <li><%:model.ProductTypeText %></li>
                                </ul>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="product-list-mtgrid__extra">
                    <p class="product-list-mtgrid__price">

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

                        <p><%:model.KdvOrFobText %></p>
                        <%} %>
                        <%} %>
                    </p>


                    <p class="product-list-mtgrid__advert-no"><%:model.ProductNo %></p>
                    <p class="product-list-mtgrid__store"><a href="<%:model.StoreUrl %>"><%:model.StoreName %></a></p>
                    <div class="product-list-mtgrid__extra-buttons hidden-xs">
                        <%if (model.HasVideo) {%>
                                     <a title="Bu Ürün İçin Video Eklenmiştir" href="javascript:;" rel="nofollow" class="btn btn-sm btn-primary">
                            <i class="fa fa-video-camera"></i>
                        </a>
                        <% } %>
                       <%if (model.Doping)
                            {%>
                          <a href="javascript:;" rel="nofollow" class="btn btn-sm btn-success">
                            D</a>
                        <%} %>
          
                    </div>
                    <div class="product-list-mt__features">

            
                    </div>
                </div>



            </div>

            <%--        <div class="product-list-mt__footer">
                    <a
                        href="<%:model.StoreUrl %>"
                        title="<%:model.StoreName %>"
                        class="product-list-mt__store"
                        data-storename="<%:model.StoreName %>"></a>
                </div>--%>
        </div>
    </div>

    <%otherIndexNumber++; %>

    <%  } %>
</div>
<%= Html.RenderHtmlPartial("_ProductPaging", Model.PagingModel)%>



