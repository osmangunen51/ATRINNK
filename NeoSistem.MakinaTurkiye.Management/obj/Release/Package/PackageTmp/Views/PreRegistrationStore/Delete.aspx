﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrationStoreDeleteModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>

<div style="margin: auto">
    <p>Not:Firmayı kayıt ettiyseniz lütfen firma numarasını alana girip öyle siliniz.</p>
    <%using (Html.BeginForm())
        {%>
    <%:Html.HiddenFor(x=>x.Id) %>
    <table>
        <tr>
            <td>Firma No:</td>
            <td><%:Html.TextBoxFor(x=>x.StoreNo, new {@style="height:30px;", @autocomplete="off" }) %></td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="Sil" /></td>
        </tr>
        <tr>
            <td colspan="2"><%:Model.Message %></td>
        </tr>
    </table>
    <% } %>
</div>


