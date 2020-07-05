﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel>" %>

<nav id='cssmenu'>
    <p class="hidden-lg hidden-md store-menu-text">MENÜ</p>
    <div id="head-mobile"></div>
    <div class="button"></div>

    <ul>
        <%
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                var makinaTurkiyeEntities = new MakinaTurkiyeEntities();
                IList<Product> productItems = makinaTurkiyeEntities.Products.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId && c.ProductActiveType != (byte)ProductActiveType.CopKutusuYeni).ToList();

                foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems leftMenuItem in Model.Items)
                {
                    if (leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAccount ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyProfile ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.StoreSettings ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAds ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyFavorites ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyMessage ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.Statistics ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.OtherSettings ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.Order)
                    { %>
        <li class='<%:leftMenuItem.IsActive ? "active" : "" %>'>
            <a <%if (leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAccount)
                {
                        %>
                <%=Html.Raw("href=/Account/Home") %>
                <% }
                else if (leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.Order)
                {
                        %>
                <%=Html.Raw("href=/Account/Order") %>
                <%} %>>
                <span class="glyphicon glyphicon-<%=leftMenuItem.IconName %>"></span>
                <%:leftMenuItem.Name %>
            </a>
            <%if (leftMenuItem.GroupItems.Count > 0)
                { %>
            <ul>
                <%foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuGroup groupItem in leftMenuItem.GroupItems)
                    {
                %>
                <%if (!string.IsNullOrEmpty(groupItem.Name))
                    { %>
                <li>
                    <a href="javascript:void(0)">
                        <strong>
                            <%:groupItem.Name %>
                        </strong>
                    </a>
                    <ul>


                        <%foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems item in groupItem.Items)
                            { %>
                        <li>
                            <a href="<%=item.Url %>">
                                <%:item.Name%></a>
                        </li>

                        <% } %>
                    </ul>
                </li>
                <% }
                    else
                    {
                        foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems item in groupItem.Items)
                        { %>
                <li>
                    <a href="<%=item.Url %>" class="<%:item.IsActive ? "text-bold" : string.Empty %>">
                        <%:item.Name%></a>

                    <% if (item.Name == "Tüm İlanlar" && productItems.ToList().Count > 0)
                        { %>
                    <ul>
                        <li>
                            <a href="/Account/Advert/Index?ProductActive=<%=(byte)ProductActive.Aktif%>&DisplayType=2">Aktif İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActive == true && c.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count()%>)
                            </span></a>
                        </li>
                        <li><a href="/Account/Advert/Index?ProductActive=<%=(byte)ProductActive.Pasif%>&DisplayType=2">Pasif İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActive == false).Count()%>)
                        </span></a></li>
                        <li><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Inceleniyor %>&DisplayType=2">Onay Bekleyen İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Inceleniyor).Count()%>)
                        </span></a></li>
                        <li><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Onaylandi %>&DisplayType=2">Onaylanan İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count()%>)
                        </span></a></li>
                        <li><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Onaylanmadi %>&DisplayType=2">Onaylanmamış İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylanmadi).Count()%>)
                        </span></a></li>
                        <li><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Silindi %>&DisplayType=2">Silinen İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Silindi).Count()%>)
                        </span></a></li>
                        <li>
                            <a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.CopKutusunda %>&DisplayType=2">Çöp İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.CopKutusunda).Count()%>)
                            </span></a>
                        </li>
                    </ul>

                    <%} %>
                </li>

                <% } %>
                <% } %>
                <% } %>
            </ul>
            <%} %>
        </li>
        <%}
            } %>
        <%}
            else if (AuthenticationUser.Membership.MemberType == (byte)MemberType.FastIndividual)
            {
                foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems leftMenuItem in Model.Items)
                {
                    if (leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAccount ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyProfile)
                    { %>
        <li class='<%:leftMenuItem.IsActive ? "active" : "" %>'>
            <a <%= leftMenuItem.ControlNubmer != (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAccount  ? "data-toggle=\"collapse\" data-parent=\"#accordion\" href=\"#Control" + leftMenuItem.ControlNubmer + "\"" :"href=\"/Account/Home\"" %>>
                <span class="glyphicon glyphicon-<%=leftMenuItem.IconName %>"></span>
                <%:leftMenuItem.Name %>
            </a>
            <%if (leftMenuItem.GroupItems.Count > 0)
                {%>



            <ul>
                <%foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuGroup groupItem in leftMenuItem.GroupItems)
                    {
                %>
                <%if (!string.IsNullOrEmpty(groupItem.Name))
                    { %>
                <li>
                    <strong>
                        <%:groupItem.Name %>
                    </strong>
                    <br />
                    <%foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems item in groupItem.Items)
                        { %>
                    <i class="fa fa-angle-right"></i>&nbsp; <a href="<%=item.Url %>" class="">
                        <%:item.Name%></a>
                    <br />
                    <% } %>
                </li>
                <% }
                    else
                    {
                        foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems item in groupItem.Items)
                        {
                            if (item.ControlNubmer != (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.MyProfile.MyPersonalAdressUpdate)
                            {
                %>
                <li>
                    <a href="<%=item.Url %>" class="<%:item.IsActive ? "text-bold" : string.Empty %>">
                        <%:item.Name%></a>
                </li>
                <% }
                    }
                %>
                <% } %>
                <% } %>
            </ul>
            <% } %>
        </li>


        <% }
                }
            }
            else if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
            {
                foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems leftMenuItem in Model.Items)
                {
                    if (leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAccount ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyFavorites ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyMessage ||
                        leftMenuItem.ControlNubmer == (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyProfile)
                    { %>

        <li class='<%:leftMenuItem.IsActive ? "active" : "" %>'>
            <a <%= leftMenuItem.ControlNubmer != (byte)NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants.LeftMenuConstants.GroupName.MyAccount  ? "data-toggle=\"collapse\" data-parent=\"#accordion\" href=\"#Control" + leftMenuItem.ControlNubmer + "\"" : "href=\"/Account/Home\"" %>>
                <span class="glyphicon glyphicon-<%=leftMenuItem.IconName %>"></span>
                <%:leftMenuItem.Name %>
            </a>
            <%if (leftMenuItem.GroupItems.Count > 0)
                {%>
            <ul>
                <%foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuGroup groupItem in leftMenuItem.GroupItems)
                    {
                %>
                <%if (!string.IsNullOrEmpty(groupItem.Name))
                    { %>
                <li>
                    <strong>
                        <%:groupItem.Name %>
                    </strong>
                    <br />
                    <%foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems item in groupItem.Items)
                        { %>
                    <a href="<%=item.Url %>" class="">
                        <%:item.Name%></a>
                    <br />
                    <% } %>
                </li>
                <% }
                    else
                    {
                        foreach (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuItems item in groupItem.Items)
                        { %>
                <li>
                    <a href="<%=item.Url %>" class="<%:item.IsActive ? "text-bold" : string.Empty %>">
                        <%:item.Name%></a>
                </li>
                <% } %>
                <% } %>
                <% } %>
            </ul>
            <%} %>
        </li>

        <% }
                }
            }
        %>
    </ul>
</nav>
