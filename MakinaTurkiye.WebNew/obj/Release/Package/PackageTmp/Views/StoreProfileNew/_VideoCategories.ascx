﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTVideoCategoryModel>" %>
<nav>
    <ul role="menubar" class="list-group" id="VideoCategoriesNav">
       <%if (Model.SelectedCategoryId == 0)
            {  %>
        <% foreach (var item in Model.VideoCategoryItemModels)
            {%>
        <li role="menuitem">
            <span class="fa fa-angle-right"></span> 
                        <a href="<%:item.CategoryUrl %>">
                            <%=item.CategoryName%> Videoları
                        </a>
        </li>
        <% } %>
        <%}
            else
            {  %>
  

        <%
            string leftCategoryPrefix = string.Empty;
            for (int i = 0; i < Model.VideoTopCategoryItemModels.Count; i++)
            {
        %>
        <li class="list-group-item"><%:MvcHtmlString.Create(leftCategoryPrefix)%>
            <i class="fa fa-angle-right"></i>
            <a href='<%:Model.VideoTopCategoryItemModels[i].CategoryUrl %>'><%=Model.VideoTopCategoryItemModels[i].CategoryName%> Videoları</a>
        </li>
        <%  leftCategoryPrefix += "&nbsp;&nbsp;";
            } %>

        <% for (int i = 0; i < Model.VideoTopSubCategoryItemModels.Count; i++)
            {
        %>
        <li class="list-group-item"><%:MvcHtmlString.Create(leftCategoryPrefix)%>
            <i class="fa fa-angle-right"></i>
            <a href='<%:Model.VideoTopSubCategoryItemModels[i].CategoryUrl %>'><%=Model.VideoTopSubCategoryItemModels[i].CategoryName%> Videoları</a>
        </li>
        <%  leftCategoryPrefix += "&nbsp;&nbsp;";
            } %>


        <%
            leftCategoryPrefix += "&nbsp;&nbsp;";
            foreach (var item in Model.VideoCategoryItemModels)
            {%>
        <li class="list-group-item"><%:MvcHtmlString.Create(leftCategoryPrefix)%>
            <i class="fa fa-angle-double-right"></i>
            <a href="<%:item.CategoryUrl %>">
                <%: item.CategoryName%> Videoları 
            </a>
        </li>
        <%} %>
            
        <%} %>
    </ul>
</nav>
