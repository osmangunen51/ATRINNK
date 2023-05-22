﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.Trinnk.Management.Models.Logs.LogItemModel>>" %>

<%int count = Model.CurrentPage * Model.PageDimension - (Model.PageDimension - 1); %>
<%foreach (var item in Model.Source.ToList())
    {%>
<tr id="row<%:item.ID%>">
    <td class="Cell CellBegin"><%:count%></td>
    <td class="Cell" style="color: <%:item.LogSituatinColor%>"><%:item.LogSituationText %></td>
    <td class="Cell"><%:item.Logger %></td>
    <td class="Cell"><%:item.Message %></td>

    <td class="Cell"><%:item.Date.ToString("dd/MM/yyyy HH:mm") %></td>
    <td class="Cell CellEnd">
        <a onclick="DeletePost(<%:item.ID%>)">
            <img src="/Content/images/delete.png" hspace="2"></a>
    </td>
</tr>
<%count = count + 1; %>
<%} %>
<tr>
    <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <div style="float: right; margin-right: 10px;">
                <b>Toplam Kayıt:<%:Model.TotalRecord %></b>
            </div>
            <ul style="float: left;">

                <% foreach (int i in Model.TotalLinkPages)
                    { %>
                <li>
                    <% if (i == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: i%></span>&nbsp;
			 <% } %>
                    <% else
                        { %>
                    <a href="javascript:void(0)" onclick="PagingPost(<%:i %>)">
                        <%: i%></a>&nbsp;
			 <% } %>
                </li>
                <% } %>
            </ul>
        </div>
    </td>

</tr>
