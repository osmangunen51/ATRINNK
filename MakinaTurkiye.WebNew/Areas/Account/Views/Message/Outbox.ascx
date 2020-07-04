<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MessageViewModel>" %>
<div>
    <h4 class="mt0 text-info">
        <span class="text-primary glyphicon glyphicon-cog"></span> Giden Mesajlar
    </h4>
    <div class="well well-mt2">
        <%if( ViewData["message"]=="true"){
          %>
                 <div class="col-md-12" style="">
                                <div class="alert alert-danger alert-dismissable" data-rel="email-wrapper"  id="telefonHata"   style="display:none;text-align:center;">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        &times;
                                    </button>
                                          Kayıtlı olup gitmeyen tüm mesajlarınız gönderilmiştir.
                                        </div>
                                </div>
        <% }%>
        <table class="table table-hover table-condensed">
            <thead>
                <tr>
                    <th style="width: 20px;">
                    </th>
                    <th style="width: 100px;">
                        Kime
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
                    MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
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
                        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.Outbox %>">
                            <%= Html.Truncate(item.MainPartyFullName, 22)%>
                        </a>
                    </td>
                    <td>
                        <a href="/Account/Message/Detail/<%:item.MessageId%>?RedirectMessageType=<%:(byte)MessageType.Outbox %>">
                            <%
        var subject = entities.Messages.Where(c => c.MessageId == item.MessageId).SingleOrDefault().MessageSubject; %>
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
                <% } %>
            </tbody>
        </table>
    </div>
</div>
