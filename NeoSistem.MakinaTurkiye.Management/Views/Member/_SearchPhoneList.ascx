﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Management.Models.MemberModels.SearchPhoneModel>>" %>

<%if (Model.Count > 0)
    {%>
<h3>Arama Sonuçları(<%:Model.Count %>)</h3>
<table cellpadding="8" cellspacing="0" class="TableList">
    <tr>
        <td class="HeaderBegin Header">İsim</td>
        <td class="Header">Üyelik Tipi</td>
        <td class="Header HeaderEnd">Numara</td>

    </tr>
    <%foreach (var item in Model.ToList())
        {%>
    <tr>
        <td class="CellBegin Cell">
            <%if (!string.IsNullOrEmpty(item.Url))
                { %>
            <a href="<%:item.Url %>">
                <%:item.NameSurname %>
            </a>
            <% }
                else
                {%>
            <%:item.NameSurname %>
            <% } %>
        </td>
        <td class="Cell"><%:item.MemberTypeText %></td>
        <td class="CellEnd"><%:item.PhoneNumber %></td>
    </tr>
    <%} %>
</table>


<% }
    else {%>
<p>Aradığınız telefon numarası bulunamamıştır. Eğer kaydın var olduğunu düşünüyorsanız lütfen numarayı inceleyiniz.</p>

<% } %>
