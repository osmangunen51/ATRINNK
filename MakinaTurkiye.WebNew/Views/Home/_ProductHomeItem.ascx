<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTHomeAdModel>" %>

<div class="product-card-home-selected">
        <a href="<%=Model.ProductUrl%>" class="product-card-home-selected-image" title="<%:Model.ProductName %>">
        <img  alt="<%:Model.ProductName %>"
            
            
            data-src="<%:Model.PicturePath.Replace("160x120","200x150")  %>"
  src="/UserFiles/image-loading.png"
                class="img-lazy"
           title="<%:Model.ProductName %>" />
    </a>


    <%if (Model.HasVideo)
        {
    %>
    <div class="product-list-favorite-icon" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

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

        <div class="product-card-home-selected-title">
            <%:Model.BrandName %>
        </div>
    </div>

</div>
