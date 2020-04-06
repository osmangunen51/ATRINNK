<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTHomeAdModel>" %>

<div class="product-card-home-selected">
    <%--<a href="<%=Model.ProductUrl%>" class="product-card-home-selected-image" title="<%:Model.ProductName %>">
        <img alt="<%:Model.ProductName %>"
            
           
           src="<%:Model.PicturePath.Replace("160x120","200x150")  %>"

           title="<%:Model.ProductName %>" />
    </a>--%>
    <%if (Model.IsFavoriteProduct)
        {%>
    <div onclick="RemoveFavoriteProductItem(<%:Model.ProductId %>)" class="product-list-favorite-icon" data-productid="product-favorite-item-<%:Model.ProductId %>" title="Favorilerden Kaldır"><i class="fa fa-heart"></i></div>

    <% }
        else
        {%>
    <div onclick="AddFavoriteProductItem(<%:Model.ProductId %>)" class="product-list-favorite-icon" data-productid="product-favorite-item-<%:Model.ProductId %>" title="Favorilere Ekle"><i class="fa fa-heart-o"></i></div>

    <%}%>

    <%if (Model.HasVideo)
        {
    %>
    <div class="product-list-video-icon" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

    <%
        } %>
    <div class="">        
        <%if (!string.IsNullOrEmpty(Model.ProductPrice))
        {%>
    <div class="product-list-price">
        <%:Model.ProductPrice %>
        <%if (!string.IsNullOrEmpty(Model.CurrencyCssName))
            {%>
        <i class="<%:Model.CurrencyCssName %>"></i>
        <% } %>
    </div>
    <% } %>

        <%--        <%if (Model.HasVideo)
            { %>
        <div class="product-card-home-icon-container">
            <i class="fa fa-video-camera" title="Bu ürüne ait video bulunmaktadır"></i>
        </div>
        <%} %>--%>
        <div class="product-card-home-selected-title">
            <%:Model.BrandName %>
        </div>
    </div>


    <%--                  <a href="<%=populerAd.SimilarUrl %>"  title="<%= populerAd.CategoryName %>" class="product-category"><%= populerAd.TruncatedCategoryName %></a> --%>
</div>
