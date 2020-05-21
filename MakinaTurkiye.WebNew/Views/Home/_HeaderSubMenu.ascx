﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTBaseSubMenuModel>" %>

<%string menuCss = "col-md-12";
    string subMenuCss = "col-md-2";
    if (Model.ImageModels.Count > 0)
    {
        menuCss = "col-md-9";
    }
%>
<li class="col-sm-12 <%=menuCss %> " style="padding: 0px;">
    <%foreach (var category in Model.CategoryModels.ToList())
        {%>
    <section class="col-sm-12  <%:subMenuCss %>" style="padding: 0px;">
        <ul>
            <li class="dropdown-header col-md-12">
                <a href="<%:category.CategoryUrl %>">
                <%if (category.CategoryName.ToLower().Contains("makin"))
                    {
                        category.CategoryName = category.CategoryName.Split(' ')[0];
                    } %>
                <%:category.CategoryName %>
                    </a>
            </li>
            <%int counter = 1;

            %>
            <%foreach (var subCategory in category.SubCategoryModels.Skip(0).Take(8))
                {%>
            <li class="col-md-12 col-sm-12" style="padding-left: 5px;">
                <a href="<%:subCategory.CategoryUrl %>">
                    <%string seperator = category.SubCategoryModels.Count != counter ? "," : "";  %>
                    <%:Html.Raw(subCategory.CategoryName) %>

                    <% counter++;
                    %>
                </a>
            </li>
            <%} %>
            <%if (category.SubCategoryModels.Count > 8)
                {%>
            <li class="col-md-12 col-sm-12" style="padding-left: 5px;">
                <a href="<%:category.CategoryUrl %>"> <i class="fa fa-plus" style="font-size:16px; font-weight:500; color:#ff6a00;"></i> Tümü
                </a>
            </li>
            <% } %>
        </ul>
    </section>
    <%} %>
</li>
<%if (Model.ImageModels.Count > 0)
    {%>
<li class="col-sm-3 hidden-xs hidden-sm menu-image-container">
    <%foreach (var item in Model.ImageModels)
        {
            if (!string.IsNullOrEmpty(item.Key))
            {%>
    <a href="<%:item.Key %>">
        <img src="<%:item.Value %>" />
    </a>
    <% }
        else
        {%>
    <img src="<%:item.Value %>" />
    <%
            }
        } %>
    <a href="/"></a>
</li>
<% } %>




