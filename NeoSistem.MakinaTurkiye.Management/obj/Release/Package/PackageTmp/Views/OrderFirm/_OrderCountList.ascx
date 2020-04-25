﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Management.Models.Orders.OrderCountItemModel>>" %>

<%foreach (var item in Model.ToList())
    {%>
<tr>
    <td class="Cell CellBegin"><%:item.Username %></td>
    <td class="Cell"><%:item.Count %></td>
    <td class="Cell"><%:item.TotalAmount %></td>
    <td class="Cell CellEnd"><%:item.StoreNames %></td>
</tr>
<%} %>