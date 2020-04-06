<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreCategoryModel>" %>

<%if (Model.SelectedCategoryId == 0)
    {  %>

<ul class="dropdown-menu supporter-nav" role="menu">
    <% foreach (var item in Model.StoreCategoryItemModels)
        { %>
    <li role="menuitem" class="supporer-menu-item">
        <a href="<%: item.CategoryUrl %>" rel="nofollow">
            <%= item.CategoryName %>
        </a>
    </li>
    <% } %>
</ul>
<% } %>
