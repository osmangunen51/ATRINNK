<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTCategoryModel>" %>
<%string display = "";

    if (Model.ActiveCategory != null) { display = "display:block; list-style-type:none;"; }%>
<ul class="sub-menu" style="<%: display%>">
    <% if (Model.MTTopCategoryItems.Count > 0)
        { %>
    <%--Üst Kategoriler--%>
    <%int LeftMargin = 0;  %>
    <% if (Model.ActiveCategory != null)
        { %>
    <%LeftMargin += 10; %>
    <%foreach (var item in Model.MTTopCategoryItems.Where(x=>x.).ToList())
        {
            LeftMargin += 10;%>
                    <li style="margin-left:<%:LeftMargin+"px"%> "><a href="<%:item.CategoryUrl %>">
        <%: item.CategoryName%></a></li>
        <%} %>
    <%--Aktif Kategori--%>
    <li class="sub-menu-item"><a href="<%: Request.FilePath %>?CategoryId=<%: Model.ActiveCategory.CategoryId %>">
        <%: Model.ActiveCategory.CategoryName%></a></li>
    <%LeftMargin += 1; %>
    <%--Alt Kategoriler--%>

    <% foreach (var item in Model.MTTopCategoryItems.Where(c => c.CategoryParentId == Model.ActiveCategory.CategoryId && c.CategoryType != (byte)CategoryType.Model).OrderBy(c => c.CategoryName))
        { %>
    <li class="sub-menu-item">&nbsp;&nbsp;&nbsp;<a href="<%:item.CategoryUrl %>">
        <%: item.CategoryName%></a>      </li>
    <% } %>

    <% }
        else
        { %>
    <%--Kategori Yoksa Ürün Grupları Listeleniyor--%>
    <% foreach (var item in Model.MTCategoryItems.Where(c => c.CategoryType == (byte)CategoryType.ProductGroup).OrderBy(c => c.CategoryName))
        { %>
    <li class="sub-menu-item">
        <a href="<%:item.CategoryUrl %>"><%: item.CategoryName%></a>

        <%if (Model.MTCategoryItems.Where(x => x.CategoryParentId == item.CategoryId).ToList().Count > 0)
            {%>
        <i class="icon-down-arrow menuarrow"></i>
        <ul class="sub-menu">
            <% foreach (var itemParent in Model.MTCategoryItems.Where(c => c.CategoryParentId == item.CategoryId))
                { %>
            <li class="sub-menu-item">
                <a href="<%:itemParent.CategoryUrl %>"><%:itemParent.CategoryName %></a>
            </li>

            <% }%>
        </ul>
    </li>
    <% }
                }
            }
        } %>
</ul>


