<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductCommentStoreItem>>" %>

<%foreach (var item in Model.Source)
    {%>
<tr id="row<%:item.ProductCommentId %>">
    <td><a href="<%:item.ProductUrl %>" title="<%:item.ProductName %>">
        <%if (item.ProductName.Length > 25)
            {%>
        <%:item.ProductName.Substring(0, 25) %> ..
        <% }
                                               else {%>
                <%:item.ProductName %>
        <% } %>
    


        </a></td>
    <td>
         <p title="<%:item.CommentText%>">   <%if (item.CommentText.Length > 25)
            {%>
        <%:item.CommentText.Substring(0, 25) %> ..
        <% }
                                               else {%>
                <%:item.CommentText %>
        <% } %>
             </p>
    </td>
    <td><%:item.Rate %></td>
        <td><%:item.RecordDate.ToString("dd/MM/yyyy") %></td>
    <td><%if (item.Status)
            {%>
        <i class="fa fa-check" title="Onaylandı"></i>
        <%}
            else
            { %>
        <i class="fa fa-ban" title="Onaylanmadı"></i>
        <%} %></td>

    <td><a style="cursor:pointer;" onclick="CommentDelete(<%:item.ProductCommentId %>)" >Kaldır</a></td>
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