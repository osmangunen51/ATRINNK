<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<ConstantModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.ConstantId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.ConstantName %>
  </td>
     <td class="Cell">
    <%: item.Order %>
  </td>
  <%if (item.ConstantType == 13 || item.ConstantType == 14 || item.ConstantType == 15 || item.ConstantType == 18 || item.ConstantType == 19 || item.ConstantType == 20 || item.ConstantType == 22 || item.ConstantType == 23  || item.ConstantType==29) 
    { %>
    <td class="Cell">
  <a style="padding-bottom: 5px; cursor: pointer; float:left; margin-right:7px;" href="/Constant/EditConstants/<%=item.ConstantId %>" id="lightbox_click" rel="superbox[iframe]">içerik</a>
  </td>
   <%} %>
  <td class="CellEnd" style="width:100px;">

    <a style="padding-bottom: 5px; cursor: pointer" onclick="EditConstant(<%: item.ConstantId %>);">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.ConstantId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>
