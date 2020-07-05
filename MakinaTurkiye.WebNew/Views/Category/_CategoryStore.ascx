<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryStoreModel>" %>

<div class="panel panel-mt" id="sidebar">
    <div class="panel-heading">
        <span class="glyphicon glyphicon-briefcase"></span><a class="targethoverf" href="<%=Model.StoreCategoryUrl%>">
            <%=Model.SelectedCategoryName %> Firmaları </a>
    </div>
    <div class="list-group list-group-mt2">

        <%foreach (var item in Model.StoreItemModes)
            {%>
                 <div class="list-group-item">
                     <a href="<%=item.StoreUrl %>">
                         <img class="border0 w100" src="<%=item.PictureLogoPath %>" alt="<%=item.StoreName %>" />
                     </a><a href="<%=item.StoreUrl %>" target="_blank"><b>
                         <%:Html.Truncate(item.StoreName, 24)%></b> </a><br />
                       <a href="<%=item.StoreUrl %>"
                             class="text-muted">
                             <%:Html.Truncatet(Model.SelectedCategoryName, 38) %>
                         </a>
                 </div>
        <% } %>
        <div class="list-group-item">
            <a href="<%=Model.StoreCategoryUrl%>"><span class="glyphicon glyphicon-briefcase"></span>
                <%=Model.SelectedCategoryName %>Firmaları 
            </a>
        </div>
    </div>
</div>

