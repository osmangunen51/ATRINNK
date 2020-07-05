<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTBaseMenuModel>>" %>

<%
    bool isMobile = Request.Browser.IsMobileDevice;

%>

<nav class="navbar navbar-inverse main-navigation navbar-expand-md">
    <div class="collapse navbar-collapse js-navbar-collapse container-fluid">
        <div class="mobile-main-menu">
            <%if (isMobile)
                {%>
            <div class="mobile-hm-title">Tüm Kategoriler</div>
            <ul class="nav navbar-nav mobile-hm">
                <%foreach (var item in Model.ToList())
                    {%>
                    <%foreach (var category in item.CategoryModels)
                        {%>
                    <li class="dropdown mega-dropdown" data-menu-id="<%:item.BaseMenuId %>" >
                        <a href="#" class="dropdown-toggle hm-item hm-text" data-cat-id="<%:category.CategoryId %>" data-toggle="dropdown" >
                            <span class="hm-image-container">
                                <img class="hm-image" src="<%:category.CategoryIcon %>" alt="" style="width:24px;">
                            </span>
                            <span>

                                <%=category.CategoryName %>

                                <i class="hm-icon"></i>
                            </span>
                        </a>
                        <ul class="dropdown-menu mega-dropdown-menu container-fluid" style="margin-top: 5px;">
                            <div id="loading" style="text-align: center;"></div>
                        </ul>
                    </li>
                    <%} %>
                <%} %>
            </ul>
            <% }
                else
                { %>
            <ul class="nav navbar-nav">
                <%foreach (var item in Model.ToList())
                    {%>
                <li class="dropdown mega-dropdown" data-menu-id="<%:item.BaseMenuId %>">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">

                        <span class="hidden-sm hidden-xs">
                            <%=item.BaseMenuName %>
                        </span>

                        <span class="hidden-md hidden-lg">
                            <%=item.BaseMenuName.Replace("<br>","") %>

                            <i class="icon-down-arrow" style="float: right;"></i>
                        </span>
                    </a>
                    <ul class="dropdown-menu mega-dropdown-menu container-fluid">
                        <div style="text-align: center;" id="loading"></div>
                    </ul>
                </li>
                <%} %>
            </ul>
            <% } %>
        </div>
    </div>
</nav>

<%--<nav class="navbar navbar-inverse main-navigation navbar-expand-md">
    <div class="collapse navbar-collapse js-navbar-collapse container-fluid">
    </div>
</nav>--%>