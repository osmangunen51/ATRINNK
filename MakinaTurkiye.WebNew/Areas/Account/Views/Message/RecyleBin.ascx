<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MessageViewModel>" %>
<div>
    <h4 class="mt0 text-info">
        <span class="text-primary glyphicon glyphicon-cog"></span> Silinen Mesajlar
    </h4>
    <div class="well well-mt2">
        <table class="table table-hover table-condensed">
            <thead>
                <tr>
                    <th style="width: 20px;">
                    </th>
                    <th style="width: 100px;">
                        Kimden
                    </th>
                    <th>
                        Konu
                    </th>
                    <th class="hidden-xs" style="width: 150px;">
                        Tarih
                    </th>
                </tr>
            </thead>
            <tbody>
                <% 
                    short index = 0;
                    foreach (var item in Model.MessageItems)
                    {
                        index++;
                %>
                <tr>
                    <td>
                        <%:Html.CheckBox("click")%>
                    </td>
                    <td>
                        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.RecyleBin %>">
                            <%= Html.Truncate(item.MainPartyFullName, 22)%>
                        </a>
                    </td>
                    <td>
                        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.RecyleBin %>">
                            <%=Html.Truncate(item.Subject, 70)%>
                        </a>
                    </td>
                    <td class="hidden-xs">
                        <%=item.MessageDate.ToString("dd.MM.yyyy HH:mm")%>
                    </td>
                </tr>
                <% } %>
                 <%  if (Model.MessageItems.Count == 0)  { %>
                <tr>
                    <td colspan="4" align="center">
                       Mesaj Bulunamamıştır. 
                    </td>
                </tr>
                <% }  %>
            </tbody>
        </table>
    </div>
</div>
