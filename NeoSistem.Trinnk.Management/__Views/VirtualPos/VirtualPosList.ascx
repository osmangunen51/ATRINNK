<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<VirtualPos>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.VirtualPosId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.VirtualPostName %>
  </td>
  <td class="Cell">
    <%: item.VirtualPosStoreType%>
  </td>
  <td class="Cell">
    <%: item.VirtualPosActive.HasValue && item.VirtualPosActive.Value ?  "Aktif" : "Pasif" %>
  </td>
  <td class="CellEnd">
    <a href="/VirtualPos/Edit/<%: item.VirtualPosId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.VirtualPosId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="5" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>
