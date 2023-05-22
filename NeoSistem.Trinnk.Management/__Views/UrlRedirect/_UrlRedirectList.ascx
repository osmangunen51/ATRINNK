<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<Trinnk.Entities.Tables.Common.UrlRedirect>>" %>

<%int row = 0; %>
<%foreach (var urlRedirect in Model.Source.ToList())
    {
%>
<tr id="row<%:urlRedirect.UrlRedirectId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="Cell"><%:urlRedirect.UrlRedirectId %></td>
    <td class="Cell">
        <a target="_blank" href="<%:ConfigurationManager.AppSettings["TrinnkWebUrl"]+urlRedirect.OldUrl%>">
            <%:urlRedirect.OldUrl%>
        </a>
        </td>
    <td class="Cell" style="font-size: 15px;">
        <%:urlRedirect.NewUrl %>
    </td>
    <td class="Cell"><%:urlRedirect.CreatedDate.ToString("dd.MM.yyyy")%></td>
    <td class="Cell">
        <a onclick="DeletePost(<%:urlRedirect.UrlRedirectId %>)" title="Sil"  style="cursor:pointer;"><img src="/Content/images/delete.png" style="width: 16px;"></a>
    </td>
</tr>
<%
        row++;
    } %>


<tr>
    <td class="ui-state ui-state-default" colspan="5" align="right" style="border-color: #DDD; border-top: none;">
        <div style="float: right;" class="pagination">
            <ul>
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
                    <a onclick="PagingPost(<%: page %>)">
                        <%: page %></a>&nbsp;
          <% } %>
                    <% } %>
                </li>

            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-hover" colspan="17" align="right" style="border-color: #DDD; border-top: none;">
        <%--    <input type="button" value="Exele Aktar" id="ExcelButon" onclick="ExportExcel();" />--%>
        Toplam Kayıt : &nbsp;&nbsp;<strong>
            <%:Model.TotalRecord%></strong>
    </td>
</tr>


