<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<MessageModel>>" %>
<%
    var entities = new MakinaTurkiyeEntities();
    short index = 0;
    foreach (var item in Model)
    {
        index++;
%>
<%
        var checkmessage = entities.messagechecks.Where(c => c.MessageId == item.MessageId).Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).FirstOrDefault();
%>
<%if (checkmessage == null)
  {  %>
  <%--okunmamış mesajları--%>
<tr class="info">
    <td>
        <%:Html.CheckBox("click")%>
    </td>
    <td>
        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.Inbox %>">
            <%= Html.Truncate(item.MainPartyFullName, 22)%>
        </a>
    </td>
    <td>
        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.Inbox %>">
            <%
      var subject = entities.Messages.Where(c => c.MessageId == item.MessageId).SingleOrDefault().MessageSubject;
            %>
            <%if (subject != null)
              {  %>
                <%=Html.Truncate(subject, 70)%>
            <%} %>
        </a>
    </td>
    <td class="hidden-xs">
        <%=item.MessageDate.ToString("dd.MM.yyyy HH:mm")%>
    </td>
</tr>
<%}
  else
  {  %>
<tr>
    <td>
        <%:Html.CheckBox("click")%>
    </td>
    <td>
        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.Inbox %>">
            <%= Html.Truncate(item.MainPartyFullName, 22)%>
        </a>
    </td>
    <td>
        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.Inbox %>">
            <%
      var subject = entities.Messages.Where(c => c.MessageId == item.MessageId).SingleOrDefault().MessageSubject;
            %>
            <%if (subject != null)
              {  %>
                <%=Html.Truncate(subject, 70)%>
            <%} %>
        </a>
    </td>
    <td class="hidden-xs">
        <%=item.MessageDate.ToString("dd.MM.yyyy HH:mm")%>
    </td>
</tr>
<% }
    } %>