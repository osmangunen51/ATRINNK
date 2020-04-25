﻿<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Stores.StoreChangeInfoResult>>" %>
<% int row = 0; %>
<%foreach (var item in Model.Source)
    {
%>
<tr>
    <td class="CellBegin Cell"><%:item.MainPartyId %></td>
    <td class="Cell"><%:item.StoreName %></td>
    <td class="Cell">
                <%string typeName = "";
            switch (item.ChangeType)
            {
                case "Store":
                    typeName = "Firma Bilgileri Güncelleme";
                    break;
                case "Adress":
                    typeName = "Adres Bilgisi Güncelleme";
                    break;
                case "Phone":
                         typeName ="Telefon Bilgisi Güncelleme";
                    break;
                default:
                    break;
            } %>
        <%:typeName %>


    </td>
    <td class="Cell"><%:item.UpdatedDated.ToString("dd.MM.yyyy HH:mm") %></td>

    <td class="CellEnd Cell">
        <%string link = "";
            switch (item.ChangeType)
            {
                case "Store":
                    link = "/ChangeLogs/Store?mainpartyId="+item.MainPartyId;
                    break;
                case "Adress":
                    link = "/ChangeLogs/address?mainPartyId=" + item.MainPartyId;
                    break;
                case "Phone":
                         link = "/ChangeLogs/Phone?mainPartyId=" + item.MainPartyId;
                    break;
                default:
                    break;
            } %>
        <a href="<%=link %>" target="_blank">Detaylar</a>

    </td>
</tr>
<%
                } %>
<tr>
    <td class="ui-state ui-state-default" colspan="6" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <ul>
                &nbsp;Sayfa&nbsp;&nbsp;
        <% foreach (int i in Model.TotalLinkPages)
            { %>
                <li>
                    <% if (i == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: i %></span>&nbsp;
          <% } %>
                    <% else
                        { %>
                    <a onclick="PagingPost(<%: i %>)">
                        <%: i %></a>&nbsp;
          <% } %>
                </li>
                <% } %>
            </ul>
            <b>Toplam Kayıt:<%:Model.TotalRecord %></b>
        </div>
    </td>
</tr>
