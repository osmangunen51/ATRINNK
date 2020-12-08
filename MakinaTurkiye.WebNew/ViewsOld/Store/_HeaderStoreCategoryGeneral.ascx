<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreCategoryModel>" %>

<%if (Model.SelectedCategoryId == 0)
    {  %>

<ul class="search-store__list" role="menu">
    <% foreach (var item in Model.StoreCategoryItemModels)
        { %>
    <li role="menuitem" >
        <a href="<%: item.CategoryUrl %>" rel="nofollow">
            <%= item.CategoryName %>
        </a>
    </li>
    <% } %>
</ul>
<% } %>
