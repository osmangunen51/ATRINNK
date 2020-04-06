﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OrderFirm>" %>
<% int row = 0; %>
<% foreach (var item in Model.StoreItems)
   { %>
<% row++; %>
<tr id="row<%: item.MainPartyId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.StoreName %>
  </td>
  <td class="Cell">
    <%: Model.PacketItems.SingleOrDefault(c=> c.PacketId == item.StorePacketId.Value).PacketName %>
  </td>
  <td class="Cell">
    <%: Model.PacketItems.SingleOrDefault(c=> c.PacketId == item.StorePacketId.Value).PacketPrice %>
  </td>
  <td class="CellEnd">
    <a href="/OrderFirm/Confirmation/<%: item.MainPartyId %>" style="padding-bottom: 5px;">
      Onayla </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="4" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>
