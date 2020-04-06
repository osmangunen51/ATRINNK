<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Catalog.MTCategoryProductModel>" %>
<%string productUrl = MakinaTurkiye.Utilities.HttpHelpers.UrlBuilder.GetProductUrl(Model.ProductId, Model.ProductName); %>

<% string s = Model.ModelYear.ToString();
    string Marka = (Model.BrandName != null || Model.BrandName != "") ? Model.BrandName : "";
    string Modeli = (Model.ModelName != null) ? ("" + Model.ModelName) : "";
%>

    <div class="product-list-mt__item js-hover-product-item">
        <div class="product-list-mt__inner">
            <a
                href="<%=productUrl %>"
                title="<%:Model.ProductName%>"
                class="product-list-mt__link clearfix">
                <div class="product-list-mt__image-area">
                    <% if (!string.IsNullOrEmpty(Model.PicturePath))
                        {%>
                    <img
                        class="img-thumbnail"
                        src="<%=Model.PicturePath.Replace("160x120","200x150") %>"
                  
                        alt="<%:Model.ProductName %>"
                        title="<%:Model.ProductName %>" />

                    <%} %>
                    <%else
                        { %>
                    <img
                        class="img-thumbnail broken-image"
                        src="/UserFiles/image-loading.png"
                        data-src="<%=Model.PicturePath %>"
                        title="<%:Model.ProductName%>"
                        alt="<%:Model.ProductName%>" />
                    <%} %>
                </div>
                <span class="product-list-mt__advert-no product-list-mt__advert-no--mobile"><%:Model.ProductNo %></span>
                <div class="product-list-mt__detail">

                    <div class="product-list-mt__title-info">
                        <div class="product-list-mt__title">
                            <%:Model.ProductName%>
                        </div>
                        <p class="product-list-mt__brand-Model">
                            <%: Marka%>, <%: Modeli%>
                        </p>
                    </div>



                    <p class="product-list-mt__price">

                        <%if (string.IsNullOrEmpty(Model.Price) || (Model.ProductPriceType != (byte)ProductPriceType.Price && Model.ProductPriceType != (byte)ProductPriceType.PriceRange))
                            {%>
                        <span class="interview"></span>
                        <%}
                            else
                            {
                                string priceCss = "";
                                if (Model.CurrencyCss == "")
                                    priceCss = "interview";
                        %>

                        <i itemprop="priceCurrency" class="<%=Model.CurrencyCss %>"></i>

                        <% if (Model.Price != "Fiyat Sorunuz")
                            {%>
                        <span><%:Model.Price%></span>
                        <% }
                            else
                            { %>
                        <span class="interview"></span>
                        <% } %>
                        <%if (!string.IsNullOrEmpty(Model.Price))
                            { %>

                        <small style="display: block; font-weight: 500; padding-left: 18px; font-size: 10px;"><%:Model.KdvOrFobText %></small>
                        <%} %>
                        <%} %>
                    </p>


                    <p class="product-list-mt__advert-no"><%:Model.ProductNo %></p>

                    <div class="product-list-mt__features">
                        <% if (Model.HasVideo)
                            {%>
                        <span class="product-list-mt__has-video js-has-video-link" data-url="<%=productUrl + "?tab=video" %>">
                            <span class="glyphicon glyphicon-facetime-video"></span>
                            Video
                        </span>
                        <%}%>

                        <%if (Model.Doping)
                            {%>
                        <span class="product-list-mt__has-doping">D</span>
                        <%} %>
                    </div>



                </div>

            </a>
            <div class="product-list-mt__footer">
                <a
                    href="<%:Model.StoreUrl %>"
                    title="<%:Model.StoreName %>"
                    class="product-list-mt__store"
                    data-storename="<%:Model.StoreName %>"></a>
            </div>
        </div>
    </div>

