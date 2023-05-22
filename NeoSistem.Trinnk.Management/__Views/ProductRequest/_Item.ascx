﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.Trinnk.Management.Models.ProductRequests.ProductRequestItem>>" %>

<%int count = Model.CurrentPage * 20 - 19; %>
<%foreach (var item in Model.Source.ToList())
    {%>
<tr id="row<%:item.ProductRequestId%>">
    <td class="Cell CellBegin"><%:count %></td>
    <td class="Cell"><%:item.MemberNameSurname %></td>
    <td class="Cell"><%:item.Email %></td>
    <td class="Cell"><%:item.PhoneNumber %></td>
    <td class="Cell"><%:item.CategoryName %></td>
    <td class="Cell"><%:item.BrandName %>
        <%if (!string.IsNullOrEmpty(item.ModelName))
            {%>
        /<%:item.ModelName %>
        <% } %>
        <%if (!string.IsNullOrEmpty(item.SeriesName))
            {%>
            /<%:item.SeriesName %>
        <% } %>
    </td>
    <td class="Cell"><%:item.Message %></td>
    <td class="Cell"><%:item.RecordDate.ToString("dd/MM/yyyy HH:mm") %></td>
    <td class="Cell CellEnd">
                <input type="checkbox" class="checkboxNew" value="<%:item.ProductRequestId %>" />
        <a onclick="DeletePost(<%:item.ProductRequestId%>)">
            <img src="/Content/images/delete.png" hspace="2"></a>
        <a href="/Member/BrowseDesc1/<%:item.ProductRequestId %>"></a>
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
