<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<PermissionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Edit
</asp:Content>
<asp:content id="Content3" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
  <% using (Html.BeginPanel())
     { %>
  <% using (Html.BeginForm())
     { %>
  <table border="0" cellpadding="5" cellspacing="0" style="margin: 10px">
     <tr>
      <td>
        <%: Html.LabelFor(m => m.PermissionName) %>
      </td>
      <td>
        :
      </td>
      <td>
        <%: Html.TextBoxFor(m => m.PermissionName) %>
      </td>
      <td>
        <%: Html.ValidationMessageFor(m => m.PermissionName) %>
      </td>
    </tr>
    <tr>
      <td colspan="3" align="right">
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
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
  <% } %>
</asp:content>
