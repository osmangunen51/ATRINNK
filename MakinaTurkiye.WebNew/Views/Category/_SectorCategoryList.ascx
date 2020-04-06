﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>

<%--        <% 
                  
                       if (Model.ParentCategoryItems.First() == i)
                       { %>
        <%= Html.RenderHtmlPartial("_CategorySectorBannerItems", Model.BannerModels)%>
        <%}%>--%>


 <h2 style="font-size:20px;margin-bottom:-20px;" class="hidden-xs hidden-sm hidden-md"><b>Ürün Kategorileri</b></h2>
<div class="sector-category-list" role="tablist"  id="sectorAccordian" aria-multiselectable="true">

    <% int categoryListIndex = 1; %>
    <% foreach (var i in Model.ParentCategoryItems.ToList())
        {
            if (i.ProductCount > 0)
            {
                if (i.ProductCount != 0)
                {
                    var categoryUrlName = !string.IsNullOrEmpty(i.CategoryContentTitle) ? i.CategoryContentTitle : i.CategoryName;
    %>

   
    <div class="panel sector-category-list__item <%--anchor<%=categoryListIndex %>--%> <%=Helpers.ToUrl(i.CategoryName)%> clearfix">
        <div class="sector-category-list__big-title" role="tab" data-role="anchor-<%=Helpers.ToUrl(i.CategoryName)%>-scroll" id="heading<%=categoryListIndex %>">
            <% if (Model.CategoryModel.SelectedCategoryId == 0)
               {
            %> <a href="<%:AppSettings.SiteUrl %><%= Helpers.ToUrl(categoryUrlName) + "-c-" + i.CategoryId.ToString() %>"><%= i.CategoryName %></a><%

                   } %>
            <% else
               {%>
            <%=i.CategoryName%> <%
                   } %>
            <span class="text-muted text-ln">(<%=i.ProductCount%>)</span>
            <a 
                class="hidden-lg" 
                role="button" 
                data-toggle="collapse" 
                data-parent="#sectorAccordian" 
                href="#subitems_<%=categoryListIndex %>" 
                aria-expanded="false" 
                aria-controls="subitems_<%=categoryListIndex %>">
           <span class="icon-down-arrow"></span>
        </a>

        </div>
        <ul role="tabpanel" class="sector-category-list__sub-items visible-lg collapse" aria-labelledby="heading<%=categoryListIndex %>" id="subitems_<%=categoryListIndex %>">

            <% foreach (var item in Model.ProductGroupParentItems.Where(c => c.CategoryParentId == i.CategoryId).OrderBy(c => c.CategoryOrder).ThenBy(c => c.CategoryName))
                {
                    string categoryUrlName1 = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                    string urlHead = Helpers.ToUrl(Helpers.ToPlural(categoryUrlName1)) + "-c-" + item.CategoryId.ToString();

                    if (item.ProductCount.HasValue)
                    {
                        if (item.ProductCount.Value != 0)
                        {
            %>
            <li class="s_category__sub-item">
                <a href="<%:AppSettings.SiteUrl %><%:urlHead %>"><%: item.CategoryName%></a>
                <span class="text-muted text-sm">(<%:item.ProductCount%>)</span>
            </li>

            <%       
                           }
                       }
                   }
            %>
        </ul>
    </div>

    <%}%>
    <%categoryListIndex = categoryListIndex + 1; %>
    <%}%>

    <%}%>
</div>


