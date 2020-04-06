<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeCategoryModel>>" %>

<ul role="menubar">
    <% foreach (var item in Model)
       { %>
    <li role="menuitem" class="<%= item.SubCategoryModels.Count() > 0 ? "has-sub-item": "" %>">
        <a href="<%: item.CategoryUrl %>">
            <%= item.CategoryName %>
            <span class="text-muted text-sm" style="font-size: 10px;">(<%= item.ProductCount %>)</span></a>
        <div class="sub-category">
            <div class="sub-category__list">
                <ul>
                    <% foreach (var subItem in item.SubCategoryModels)
                       {%>
                    <li><a href="<%:subItem.CategoryUrl%>">
                        <%= subItem.CategoryName %> </a>
                    </li>
                    <% }%>
                </ul>
            </div>
        </div>
    </li>
    <% } %>
</ul>
