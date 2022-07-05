<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<PacketFeatureType>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.PacketFeatureTypeId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.PacketFeatureTypeName %>
  </td>
  <td class="Cell">
    <%: item.PacketFeatureTypeDesc %>
  </td>
  <td class="Cell">
    <%: item.PacketFeatureTypeOrder %>
  </td>
  <td class="CellEnd">
    <a href="/PacketFeatureType/Edit/<%: item.PacketFeatureTypeId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
      <%--DeletePost(<%: item.PacketFeatureTypeId %>);--%>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.PacketFeatureTypeId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
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
