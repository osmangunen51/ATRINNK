﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<SurveyOptionModel>>" %>
<table border="0" cellpadding="3" cellspacing="0" class="TableList" width="100%"
  style="border: solid 1px #FFF">
  <tr>
    <td class="HeaderBegin">
      Seçenekler
    </td>
  </tr>
  <% foreach(var item in Model) { %>
  <tr id="opRow<%: item.OptionId %>" class="<%: item.OptionId % 2 == 0 ? "Row" : "RowAlternate" %>"
    style="height: 23px">
    <td class="CellBegin">
      <%: item.OptionContent %>
    </td>
  </tr>
  <% } %>
</table>
