<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Packet>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.PacketId%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.PacketName %>
  </td>
  <td class="Cell">
    <%: item.PacketDescription %>
  </td>
  <td class="Cell">
    <%:item.PacketPrice.HasValue ? item.PacketPrice.Value.ToString("N") : "" %>
  </td>
  <td class="Cell">
    <%: item.PacketDay %>
  </td>
  <td class="Cell">
    <%: item.IsOnset.HasValue && item.IsOnset.Value ? "Aktif" : "Pasif"%>
  </td>
  <td class="Cell">
    <%: item.IsStandart.HasValue && item.IsStandart.Value ? "Aktif" : "Pasif"%>
  </td>
    <td class="Cell">
        <%:item.SendReminderMail==true ?"Aktif":"Pasif" %>
    </td>
    <td>
        <%:String.Format("{0:0.0}", item.ProductFactor) %>
    </td>
  <td class="CellEnd">
    <a href="/Packet/Edit/<%: item.PacketId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.PacketId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>
