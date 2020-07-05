﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels.BaseMenuModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ana Menüler
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; margin: 0 auto;">
        <button style="margin-top: 10px;" onclick="window.location='/category/CreateBaseMenu'">Yeni Ekle</button>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header">Başlık
                    </td>
                    <td class="Header">Sektörler
                    </td>
                    <td class="Header">Fotoğraflar</td>
                    <td class="Header">Tarih
                    </td>
                    <td class="Header">Durum
                    </td>
                    <td class="Header">Menü Sıra</td>
                    <td class="Header">Anasayfa Sıra</td>
                    <td class="Header"></td>

                </tr>
            </thead>
            <tbody>
                <%foreach (var item in Model.BaseMenuItems)
                    {%>
                <tr>
                    <td class="Cell CellBegin"><%:item.BaseMenuName %></td>
                    <td class="Cell">

                        <%foreach (var baseCategory in item.BaseMenuCategories)
                            {%>
                        <%:baseCategory.Category.CategoryContentTitle %>,
                                    <%} %></td>
                    <td class="Cell">
                        <%:item.CreatedTime.ToString("dd/MM/yyyy") %>
                    </td>
                    <td class="Cell">
                        <%foreach (var imageItem in item.ImagePaths)
                            {%>
                        <img alt="fotoğraf" src="<%:imageItem %>" style="width: 50px;" />
                        <%} %>
                    </td>
                    <td class="Cell">
                        <%if (item.Active)
                            {%>
                        <img src="/Content/Images/Goodshield.png" title="Aktif" />
                        <% }
                            else
                            {%>
                        <img src="/Content/Images/Errorshield.png" title="Pasif" />
                        <% } %>
                    </td>
                    <td class="Cell">
                        <%:item.Order %>
                    </td>
                    <td class="Cell">
                        <%:item.HomePageOrder.HasValue ? item.HomePageOrder.Value:0 %>
                    </td>
                    <td class="Cell CellEnd">
                        <a href="/Category/EditBaseMenu/<%:item.BaseMenuId %>">Düzenle</a>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

