﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SurveyOptionModel>>" %>
<% foreach(var item in Model) { %>
<tr id="opRow<%: item.OptionId %>" class="<%: item.OptionId % 2 == 0 ? "Row" : "RowAlternate" %>"
  style="height: 23px">
  <td class="CellBegin">
    <%: item.OptionContent %>
  </td>
  <td class="Cell">
    <span class="ui-icon ui-icon-circle-minus" style="cursor: pointer;" onclick="RemoveOption(<%: item.OptionId %>);">
    </span>
  </td>
</tr>
<% } %>