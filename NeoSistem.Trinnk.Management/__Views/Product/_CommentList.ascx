<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.Trinnk.Management.Models.ProductCommentItem>>" %>
<%foreach (var item in Model.Source)
    {%>
<tr id="row<%:item.ProductCommentId %>">
    <td class="Cell"><%:item.ProductName %></td>

    <td class="Cell"><a href="/Product/Edit/<%:item.ProductId %>"><%:item.ProductNo %></a></td>
    <td class="Cell"><%:item.MemberNameSurname %></td>
    <td class="Cell"><%:item.MemberEmail %></td>
    <td class="Cell">
        <%:item.CommenText %>                    
    </td>

    <td class="Cell">
        <%:item.Rate %>
    </td>
    <td class="Cell"><%:item.RecorDate %></td>
    <td class="Cell">
        <%if (item.Status == false)
            { %>
        <a href="/Product/CommentStatus?set=1&id=<%:item.ProductCommentId %>">Onayla</a>
        <%}
            else
            {%>
        <a href="/Product/CommentStatus?set=0&id=<%:item.ProductCommentId %>">Onay Kaldır</a>
        <% } %>
    </td>
    <td class="Cell"><%if (item.Reported) {%>
            Şikayetli
        <% } %></td>
    <td class="Cell">
        <input type="checkbox" class="checkBoxProductComment" value="<%:item.ProductCommentId %>" />
    </td>
</tr>
<%} %>

<tr>
    <td class="ui-state ui-state-default" colspan="19" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <ul>
                <li>
                    <button value="Onayla" type="button" onclick="confirmComment(1)">Onayla</button></li>
                <li>
                    <button value="Onayla" type="button" onclick="confirmComment(0)">Onay Kaldır</button></li>

                &nbsp;Sayfa&nbsp;&nbsp;
        <% foreach (int page in Model.TotalLinkPages)
            { %>
                <li>
                    <% if (page == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: page %></span>&nbsp;
          <% } %>
                    <% else
                        { %>
                    <a onclick="PagePost(<%: page %>)">
                        <%: page %></a>&nbsp;
          <% } %>
                </li>
                <% } %>
                <li>Toplam Yorum:<b><%:Model.TotalRecord %></b></li>
            </ul>
        </div>
    </td>
</tr>
