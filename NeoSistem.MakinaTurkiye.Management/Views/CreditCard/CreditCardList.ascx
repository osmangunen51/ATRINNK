<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<CreditCard>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.CreditCardId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.CreditCardName %>
  </td>
    <td class="Cell">
        <%if (item.Active == true) {%>Aktif<% }else{%>Pasif<%} %>
    </td>
  <td class="CellEnd">
    <a href="/CreditCard/Edit/<%: item.CreditCardId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.CreditCardId %>);">
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
     
    </div>
  </td>
</tr>
