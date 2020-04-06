﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Account>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.AccountId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.BankName %>
  </td>
  <td class="Cell">
    <%: item.IbanNo %>
  </td>
  <td class="Cell">
    <%: item.AccountName %>
  </td>
  <td class="Cell">
    <%: item.AccountNo %>
  </td>
  <td class="Cell">
    <%: item.BranchName%>
  </td>
  <td class="Cell">
    <%: item.BranchCode %>
  </td>
  <td class="CellEnd">
    <a href="/BankAccount/Edit/<%: item.AccountId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.AccountId %>);">
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
