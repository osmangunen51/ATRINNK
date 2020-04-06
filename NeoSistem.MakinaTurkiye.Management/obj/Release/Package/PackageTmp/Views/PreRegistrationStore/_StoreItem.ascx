<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.StoreItem>>" %>

<%if (Model.Count > 0) {%>
<h5>Bu isimle bulunan kayıtlar</h5>
<table>
    <thead>
        <tr>
            
            <th class="HeaderBegin">Firma No</th>
            <th class="HeaderBegin">Firma adı</th>
            <th class="HeaderEnd">Üye Adı Soyadı</th>
                        <th class="HeaderEnd">Üye No</th>
        </tr>
    </thead>
<tbody>
    <%foreach (var item in Model.ToList())
        {%>
        <tr>
            <td class="CellBegin">
                <%:item.StoreNo %>
            </td>
            <td class="CellBegin"><a target="_blank" href="/Store/EditStore/<%:item.StoreMainPartId %>"><%:item.StoreName %></a></td>
            <td class="CellEnd"><%:item.MemberNameSurname %></td>
            <td class="CellEnd"><%:item.MemberNo %></td>
        </tr>
        <%} %>

</tbody>
</table>

<% } %>