﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTNewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_HeaderContent") %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="StoreProfileHeaderContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%} %>
    <%=Html.RenderHtmlPartial("_HeaderTop",Model.MTStoreProfileHeaderModel) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StoprofileMenu" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%=Html.RenderHtmlPartial("_LeftMenu", Model.MTStoreProfileHeaderModel)%>

    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>
    <%:Html.Hidden("hdnStoreMainPartyId",Model.MainPartyId) %>
    <div class="col-sm-7 col-md-8 col-lg-9">
        <div class="StoreProfileContent news-container-p" style="background-color: transparent;">
            <%foreach (var item in Model.StoreNewItems)
                {%>

            <div class="new-item-mt-c">
                <div class="new-item-mt">
                    <%if (!string.IsNullOrEmpty(item.ImagePath))
                        {%>
                    <div class="new-item-image">
                        <div class="new-item2">
                            <div class="item-image" style="background-color: #fff; max-height: 230px; text-align: center;">
                                <img src="<%:item.ImagePath %>" alt="<%:item.Title %>" title="<%:item.Title %>" />
                            </div>
                            <a class="item-link" href="<%:item.NewUrl %>"></a>
                        </div>
                    </div>
                    <% } %>

                    <div class="new-date2">
                        <%:item.DateString %>
                    </div>

                    <div class="new-blog-title">
                        <a href="<%:item.NewUrl %>"><%:item.Title %> </a>
                    </div>

                    <div class="pull-right">
                        <a class="btn background-mt-btn" href="<%:item.NewUrl %>" style="font-size: 12px; font-family: 'Roboto'">Yazıyı Keşfet</a>
                    </div>
                    <div style="clear: both"></div>
                </div>
            </div>

            <% } %>
        </div>
    </div>
    <%}
        else
        { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>
