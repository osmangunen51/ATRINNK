﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<PermissionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  İzin Oluştur
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <% using (Html.BeginForm())
     { %>
  <div style="float: left; width: 347px;">
    <% using (Html.BeginPanel())
       { %>
    <table border="0" cellpadding="5" cellspacing="0" style="margin: 10px">
      <tr>
        <td>
          İzin Adı :
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.PermissionName) %>
        </td>
        <td>
          <% Html.ValidateFor(m => m.PermissionName); %>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="right">
          <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 10px;
            margin-bottom: 10px;">
          </div>
          <button type="submit" style="height: 27px;">
            Kaydet</button>
          <button type="button" style="height: 27px;" onclick="window.location.href='/Permission'">
            İptal</button>
        </td>
        <td>
        </td>
      </tr>
    </table>
    <% } %>
  </div>
  <div style="float: left; width: 477px; margin-top: 7px">
    <%= Html.ValidationSummary("", new { style = "width: 375px; " })%>
  </div>
  <% } %>
</asp:Content>
