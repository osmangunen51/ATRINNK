<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<List<MTCategorySliderItem>>" %>

<%if (Model.Count > 0 && Request.QueryString["SearchText"]==null)
    {%>
<div class="category-banner-carousel owl-carousel owl-theme">
    <%
        int counter = 1;
        foreach (var item in Model)
        {
    %>
    <div class="item">
        <picture>
           
          <source srcset="<%:item.SliderImagePathPc %>" media="(min-width: 1200px)" />
          <source srcset="<%:item.SliderImagePathTablet %>" media="(min-width: 800px)" />
          <%if (!string.IsNullOrEmpty(item.SliderImagePathMobile))
              { %>
                 <img src="<%:item.SliderImagePathMobile %>" alt="slider <%:counter %>" />
            <%}
                                                                      else {%>
                  <img src="<%:item.SliderImagePathPc %>" alt="<%=!string.IsNullOrEmpty(item.AltTag) ? item.AltTag : "slider"+counter %>" />
            <% } %>
           
        </picture>
    </div>
    <%counter++;
        } %>
</div>
<div class="category-banner-carousel__controller owl-carousel owl-theme">
    <%  foreach (var item in Model)
        {

    %>

    <div class="item">
        <img src="<%: item.SliderImagePathPc %>">
    </div>

    <%} %>
</div>




<% } %>


