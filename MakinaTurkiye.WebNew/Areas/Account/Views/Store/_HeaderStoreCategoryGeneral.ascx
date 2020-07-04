<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreCategoryModel>" %>

<%if (Model.SelectedCategoryId == 0)
  {  %>
<ul role="menubar">
    <% foreach (var item in Model.StoreCategoryItemModels)
       { %>
    <li role="menuitem">
        <a href="<%: item.CategoryUrl %>">
            <%= item.CategoryName %>
        </a>
    </li>
    <% } %>
</ul>
<% } %>
