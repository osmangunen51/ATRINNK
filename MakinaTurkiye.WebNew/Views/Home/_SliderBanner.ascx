<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeBannerModel>>" %>
<div class="home-banner-carousel owl-carousel owl-theme">
    <%
        int counter = 1;
        foreach (var item in Model)
        {
    %>
    <div class="item">
        <a href="<%:item.Url %>">
            <picture>
  <source srcset="<%:item.PicturePathPc %>" media="(min-width: 1200px)" />

  <source srcset="<%:item.PicturePathTablet %>" media="(min-width: 800px)" />
    
  <img src="<%:item.PicturePathMobile %>" alt="slider <%:counter %>" />
</picture>
            <%--         <img alt="slider" 
                srcset=" 720w, https://www.makinaturkiye.com/<%:item.PicturePathTablet %> 768w,https://www.makinaturkiye.com/<%:item.PicturePathPc %>" 
                src="https://www.makinaturkiye.com/<%:item.PicturePathPc %>" width="630" height="228">--%>
        </a>
    </div>
    <%counter++;
        } %>
</div>
<div class="home-banner-carousel__controller owl-carousel owl-theme">
    <%  foreach (var item in Model)
        {

    %>

    <div class="item">
        <img src="<%: item.PicturePathMobile %>">
    </div>

    <%} %>
</div>

