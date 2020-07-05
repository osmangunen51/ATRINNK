﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreUserItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Yetkili Kullanıcılar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <%=Html.RenderHtmlPartial("Style") %>
    <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%=Html.RenderHtmlPartial("TabMenu") %>
    <div style="width: 100%; margin: 0 auto;">
        <h3>Yetkili Kullanıcılar</h3>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 50%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header">Adı</td>
                    <td class="Header">Soyadı</td>
                    <td class="Header">Email</td>
                    <td class="Header">Durum</td>
                    <td class="Header HeaderEnd"></td>
                </tr>

            </thead>
            <tbody>
                <%foreach (var item in Model.Source)
                    {%>
                <tr>
                    <td class="Cell CellBegin"><%:item.MemberNo %></td>
                    <td class="Cell"><%:item.MemberName %></td>
                    <td class="Cell"><%:item.MemberSurname %></td>
                    <td class="Cell"><%:item.MemberEmail %></td>
                    <td class="Cell">
                        <%if (item.Active == true)
                            {%>
                        <img title="Aktif" src="/Content/Images/Goodshield.png">
                        <% }
                            else
                            {%>
                        <img title="Pasif" src="/Content/Images/Errorshield.png">
                        <% } %>
                    </td>
                    <td class="Cell CellEnd">
                        <a href="/Member/Edit/<%:item.MemberMainPartyId %>"><img title="Düzenle" src="/Content/images/edit.png" hspace="5"></a>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
</asp:Content>
