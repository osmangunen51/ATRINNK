﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UserGroupModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Oluştur
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
    $(document).ready(function () {
      $(".hover").hover(function () { $(this).addClass("ui-state-hover"); }, function () { $(this).removeClass("ui-state-hover"); });
    });
  </script>
  <% using (Html.BeginForm())
     { %>
  <div style="float: left; width: 700px; margin-top: 7px">
    <% using (Html.BeginPanel())
       { %>
    <table border="0" cellpadding="5" cellspacing="0" style="margin: 10px; width: 550px;">
      <tr>
        <td style="width: 70px">
          <%: Html.LabelFor(m => m.GroupName) %>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.GroupName) %>
          <%: Html.ValidationMessageFor(m => m.GroupName) %>
        </td>
        <td>
        </td>
      </tr>
      <tr>
        <td colspan="2">
        </td>
        <td>
          <div style="max-height: 300px; overflow: auto; border: solid 1px #CCC">
            <ul style="list-style: none; padding: 0; margin: 0;">
              <% foreach (var item in PermissionModel.Permissions)
                 { %>
              <li class='hover' style="border: none; float: left; width: 200px" onclick="$get('per<%: item.PermissionId %>').click();">
                <%: Html.CheckBox("Permission", new { id = "per" + item.PermissionId, value = item.PermissionId })%><span
                  style="cursor: default; display: inline-block; height: 17px;"><%: item.PermissionName %></span></li>
              <% } %>
            </ul>
          </div>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="right">
          <button type="submit" style="height: 27px;">
            Kaydet</button>
          <button type="button" style="height: 27px;" onclick="window.location.href='/UserGroup'">
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
