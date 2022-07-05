<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<IEnumerable<PermissionModel>>" %>
<table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
  <thead>
    <tr>
      <td class="Header">
        İzin
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
    <tr id="row<%: item.PermissionId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
      <td class="Cell">
        <%: item.PermissionName %>
      </td>
      <td class="CellEnd">
        <a onclick="alert('Düzenleme modu pasif edilmiştir.\nSabit alanı düzenlenemez !')"
          style="padding-bottom: 5px; cursor: pointer;">
          <img src="/Content/images/edit.png" /></a> &nbsp;&nbsp; <a style="cursor: pointer;"
            onclick="alert('İzinler sabit olduğundan silme işlemi başarısız !')">
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
