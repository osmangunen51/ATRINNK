<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>
<%
    string leftPX = "";
%>
<div class="panel panel-mt panel-mtv2">
  <div class="panel-heading left-menu-header">
        <span class="icon-menu"></span>
        <span class="title" style="display: inline-block; margin: 0; font-size: 13px; color:#000;font-weight: 700;">Kategoriler</span>
        <a href="javascript:;" role="button"  data-toggle="collapse" data-parent="#filters" data-target="#menu-body">
            <span class="more-less icon-up-arrow"></span>
        </a>
    </div>
    <div class="panel-body collapse in CategoryLeftCategory" id="menu-body">
        <ul class="list-group list-group-mt3" role="menubar">
            <% foreach (var item in Model.CategoryModel.TopCategoryItemModels.Where(c => c.CategoryType != (byte)CategoryType.Model && c.CategoryType != (byte)CategoryType.Brand && c.CategoryType != (byte)CategoryType.Series))
               { %>

            <li title="<%:item.CategoryName %>" class="list-group-item <%: Model.CategoryModel.SelectedCategoryId== item.CategoryId ? "active" : string.Empty %>">
                <%:MvcHtmlString.Create(leftPX) %>
                <a href="<%:item.CategoryUrl %>">
                    <%:item.TruncatedCategoryName%>
                   
                    <% if (item.CategoryType != (byte)CategoryType.Sector)
                     { %>
                        <span class="text-muted text-sm">(<%: item.ProductCount %>) </span>
                     <% } %>      
                </a>
            </li>
            <% leftPX += " &nbsp; ";  %>
            <% } %>
            <% int itemIndex = 0; %>
            <% foreach (var item in Model.CategoryModel.CategoryItemModels.Where(c => c.CategoryType != (byte)CategoryType.Model && c.CategoryType != (byte)CategoryType.Brand && c.CategoryType != (byte)CategoryType.Series))
               { %>
            <li title="<%:item.CategoryName %>" class="list-group-item <%: Model.CategoryModel.SelectedCategoryId== item.CategoryId ? "active" : string.Empty %>">
                <%:MvcHtmlString.Create(leftPX) %>
                <a href="<%:item.CategoryUrl %>">
                    <%:item.TruncatedCategoryName%>
                    <span class="text-muted text-sm">(<%:item.ProductCount %>) </span>
                </a>
            </li>
           <%-- <% if (itemIndex <= 11)
               { %>
            <li title="<%:item.CategoryName %>" class="list-group-item <%: Model.CategoryModel.SelectedCategoryId== item.CategoryId ? "active" : string.Empty %>">
                <%:MvcHtmlString.Create(leftPX) %>
                <a href="<%:item.CategoryUrl %>">
                    <%:item.TruncatedCategoryName%>
                    <span class="text-muted text-sm">(<%:item.ProductCount %>) </span>
                </a>
            </li>
            <%if (itemIndex == 11)
              {%>
            <li class="list-group-item showAllSubCat">
                <%:MvcHtmlString.Create(leftPX) %> <a href="javascript:;">Tüm alt kategorileri göster <span class="icon-fill-down-arrow"></span></a>
            </li>
            <%} %>
            <%}
               else
               { %>
            <li title="<%:item.CategoryName %>" class="list-group-item subCatNone <%: Model.CategoryModel.SelectedCategoryId== item.CategoryId ? "active" : string.Empty %>" style="display: none;">
                <%:MvcHtmlString.Create(leftPX) %>
                <a href="<%:item.CategoryUrl %>">
                    <%:item.TruncatedCategoryName%>
                    <span class="text-muted text-sm">(<%:item.ProductCount %>) </span>
                </a>
            </li>
            <%} %>--%>
            <% itemIndex++; %>
            <% } %>

            <%--<li class="list-group-item hideAllSubCat" style="display: none;">
                <%:MvcHtmlString.Create(leftPX) %> <a href="javascript:;">Gizle <span class="icon-fill-up-arrow"></span></a>
            </li>--%>

        </ul>
    </div>
</div>

