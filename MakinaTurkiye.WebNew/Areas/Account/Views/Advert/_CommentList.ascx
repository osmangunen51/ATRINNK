﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductCommentStoreItem>>" %>

<%foreach (var item in Model.Source)
    {%>
<tr id="row<%:item.ProductCommentId %>">
    <td><a href="<%:item.ProductUrl %>"><%:item.ProductName %></a></td>
    <td><%:item.MemberNameSurname %></td>
    <td><%:item.CommentText %></td>
    <td><%:item.Rate %></td>
    <td><%if (item.Status)
            {%>
        <i class="fa fa-check" title="Onaylandı"></i>
        <%}
            else
            { %>
        <i class="fa fa-ban" title="Onaylanmadı"></i>
        <%} %></td>
    <td><%:item.RecordDate.ToString("dd/MM/yyyy, hh:mm") %></td>
    <td>
        <%if (item.Reported == true)
            {%>
            Şikayet Edildi(İnceleniyor)
        <% }
            else
            {%>
        <a style="cursor: pointer" onclick="ReportComment(<%:item.ProductCommentId%>) ">Şikayet Et</a>
        <% } %>
    </td>
</tr>
<%} %>
<%if (Model.TotalRecord > 0)
    {%>

<tr>
    <td colspan="7">
        <ul class="pagination pull-left" style="margin: 0px;">
            <%foreach (var item in Model.TotalLinkPages)
                {

                    if (item == Model.CurrentPage)
                    {%>
            <li class="active"><span><%:item %></span> </li>
            <%}
                else
                { %>
            <li><a onclick="ProductCommentPaging(<%:item %>)" style="cursor: pointer;"><%:item %></a></li>
            <%}

            %>

            <%   } %>
        </ul>
        <div class="pull-right">
            <b>Toplam Yorum:</b><%:Model.TotalRecord %>
        </div>
        <div style="clear: both"></div>
    </td>
</tr>
<% } %>