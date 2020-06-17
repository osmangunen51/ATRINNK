<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTBaseMenuModel>>" %>

<nav class="navbar navbar-inverse main-navigation navbar-expand-md">
    <div class="collapse navbar-collapse js-navbar-collapse container-fluid">
        <div class="mobile-main-menu">
            <ul class="nav navbar-nav ">

                <%foreach (var item in Model.ToList())
                    {%>
                <li class="dropdown mega-dropdown" data-menu-id="<%:item.BaseMenuId %>">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">

                        <span class="hidden-sm hidden-xs">
                            <%=item.BaseMenuName %>
                        </span>

                        <span class="hidden-md hidden-lg">
                            <%=item.BaseMenuName.Replace("<br>","") %>

                            <i class="icon-down-arrow" style="float: right;" ></i>
                        </span>
                    </a>
                  <ul class="dropdown-menu mega-dropdown-menu container-fluid">
                      <div style="text-align:center;" id="loading">
                       
                      </div>
                      </ul>
                </li>
                <%} %>
            </ul>
        </div>
        <%--        <ul class="nav navbar-nav navbar-right">
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">My account <span class="caret"></span></a>
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                </ul>
            </li>

                       <li><a href="<%:AppSettings.SiteUrl %>Account/Favorite/Product" id="favoriteProductCount" class="hidden-xs plr5 tooltips tooltip-mt badge-link" data-toggle="tooltip" data-placement="bottom"
                        title="<%=favoriteProductCount %> favorilere eklenmiş ürününüz var">
                        <span class="badge-link__count"><%=favoriteProductCount %></span>
                        <span class="text-md  glyphicon glyphicon-heart"></span>
                    </a>
                    </li>
        </ul>--%>
    </div>
    <!-- /.nav-collapse -->
</nav>
