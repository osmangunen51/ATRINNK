<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.StoreItem>>" %>

<%if (Model.Count > 0) {%>
<h5>Bu isimle bulunan kayıtlar</h5>
<table class="TableList">
    <thead>
        <tr>
            
            <th class="HeaderBegin Header">Firma No</th>
            <th class="Header">Firma adı</th>
            <th class="Header">Üye Adı Soyadı</th>
            <th class="Header">Kayıt Tipi</th>
            <th class="HeaderEnd Header">Üye No</th>
            
        </tr>
    </thead>
<tbody>
    <%foreach (var item in Model.ToList())
        {%>
        <tr>
            <td class="CellBegin Cell">
                <%:item.StoreNo %>
            </td>
            <td class="Cell"><a target="_blank" href="/Store/EditStore/<%:item.StoreMainPartId %>"><%:item.StoreName %></a></td>
            <td class="Cell"><%:item.MemberNameSurname %></td>
            <td class="Cell"><%:item.MemberNo %></td>
            <td class="CellEnd Cell"><%:item.Type %></td>
        </tr>
        <%} %>

</tbody>
</table>

<% } %>