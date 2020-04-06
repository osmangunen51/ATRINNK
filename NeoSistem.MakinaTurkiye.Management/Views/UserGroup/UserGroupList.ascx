﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<UserGroupModel>>" %>
<table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
  <thead>
    <tr>
      <td class="HeaderBegin">
        Kullanıcı Grubu
      </td>
      <td class="Header" style="width: 100px; height: 19px">
      </td>
    </tr>
  </thead>
  <tbody>
    <% int row = 0; %>
    <% foreach (var item in Model)
       { %>
    <% row++; %>
    <tr id="row<%: item.UserGroupId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
      <td class="CellBegin">
        <%: item.GroupName %>
      </td>
      <td class="CellEnd">
        <a href="/UserGroup/Edit/<%: item.UserGroupId %>" style="padding-bottom: 5px;">
          <img src="/Content/images/edit.png" /></a> &nbsp;&nbsp; <a style="cursor: pointer;"
            onclick="if(confirm('İzin grubunu silmek istediğinizden eminmisiniz ?')){Delete(<%: item.UserGroupId %>);} ">
            <img src="/Content/images/delete.png" /></a>
      </td>
    </tr>
    <% } %>
    <% if (Model.Count() <= 0)
       { %>
    <tr class="Row">
      <td colspan="5" class="CellBegin Cell">
        Henüz izin eklenmedi.
      </td>
    </tr>
    <% } %>
  </tbody>
</table>
