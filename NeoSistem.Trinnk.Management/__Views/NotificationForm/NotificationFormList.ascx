<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<NotificationFormModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.OrderByDescending(c => c.RecordDate).ThenByDescending(c => c.IsRead))
   { %>
<% row++; %>
<tr id="row<%: item.NotificationFormId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.NotificationFormSubject %>
  </td>
  <td class="Cell">
    <%: item.MemberName + " "+ item.MemberSurname %>
  </td>
  <td class="Cell">
    <%: item.RecordDate.ToString("dd.MM.yyyy HH:mm:ss")%>
  </td>
  <td class="Cell">
    <%: item.IsRead ? "Okundu" : "Okunmadı" %>
  </td>
  <td class="CellEnd">
    <a href="/NotificationForm/View/<%: item.NotificationFormId %>" style="padding-bottom: 5px;">
      Görüntüle </a>
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
