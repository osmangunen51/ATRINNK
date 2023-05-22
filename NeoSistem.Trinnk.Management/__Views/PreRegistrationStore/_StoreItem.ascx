<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.Trinnk.Management.Models.PreRegistrations.StoreItem>>" %>

<%if (Model.Count > 0)
    {%>
<h5>Bu isimle bulunan kayıtlar</h5>
<table class="TableList">
    <thead>
        <tr>

            <th class="HeaderBegin Header">Firma No</th>
            <th class="Header">Firma adı</th>
            <th class="Header">Üye Adı Soyadı</th>
            <th class="Header">Üye Email</th>
            <th class="Header">Telefon</th>
            <th class="Header">Web Adres</th>
            <th class="Header">Kayıt Tipi</th>
            <th class="HeaderEnd Header">İşlemler</th>
        </tr>
    </thead>
    <tbody>
        <%foreach (var item in Model.ToList())
            {%>
        <tr>
            <td class="CellBegin Cell">
                <%:item.StoreNo %>
            </td>
            <td class="Cell">
                <%if (item.StoreMainPartId != 0)
                    {
                    %>
                                <a target="_blank" href="/Store/EditStore/<%:item.StoreMainPartId %>"><%:item.StoreName %></a>

                <%}
                                                    else {%>
                <%=item.StoreName %>
                <% } %>

            </td>
            
            <td class="Cell"><%:item.MemberNameSurname %></td>
            <td class="Cell"><%:item.MemberEmail %></td>
            <td class="Cell"><%:item.PhoneNumbers %></td>
            <td class="Cell"><%:item.WebUrl %></td>
            <td class="Cell"><%:item.Type %></td>
            <td class="CellEnd Cell">
                <a title="Açıklamalar" href="/Member/BrowseDesc1/<%: item.MemberMainPartyId %>" target="_blank">
            <img src="/Content/images/productonay.png">
        </a>
            </td>
        </tr>
        <%} %>
    </tbody>
</table>

<% } %>