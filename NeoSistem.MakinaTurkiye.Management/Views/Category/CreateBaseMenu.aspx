﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels.BaseMenuCreateModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ana Menü Oluştur 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="margin-left: 20px; margin-top: 25px;">Ana Menü Oluştur </h2>
    <%if (TempData["success"] != null)
        {
    %>
    <div style="font-size: 20px"><%:TempData["sucess"].ToString() %></div>
    <% } %>
    <% using (Html.BeginForm(new { style = " margin-left:20px; margin-top:25px;" }))
        {%>
    <%: Html.ValidationSummary(true)%>

    <table>
        <tr>
            <td>Menü İsmi</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.BaseMenuName) %></td>
        </tr>
        <tr>
            <td>Sektörler</td>
            <td>:</td>
            <td>
                <div style="width: 400px">
                    <%foreach (var item in Model.SectorCategories)
                        {%>
                    <input type="checkbox" name="SectorCategoriesForm[]" value="<%:item.Value %>" />
                    <%:item.Text %><br />
                    <%  } %>
                </div>

            </td>

        </tr>
        <tr>
            <td>Aktif</td>
            <td>:</td>
            <td><%:Html.CheckBoxFor(x=>x.Active) %></td>
        </tr>
        <tr>
            <td>Sıra</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.Order) %></td>
        </tr>
        <tr>
            <td>Anasayfa Sıra</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.HomePageOrder) %></td>
        </tr>
        <tr>
            <td>Arka plan css</td>
            <td>:</td>
            <td><%:Html.TextAreaFor(x=>x.BackgroundCss) %></td>
        </tr>
        <tr>
            <tr>
                <td>Tab Arka Plan Css</td>
                <td>:</td>
                <td><%:Html.TextAreaFor(x=>x.TabBackgroundCss) %></td>
            </tr>
            <td></td>
            <td></td>
            <td>
                <input type="submit" value="Ekle" /></td>
        </tr>
    </table>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

