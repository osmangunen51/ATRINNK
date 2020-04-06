<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.MemberDescriptionCountModel>>" %>
<style type="text/css">
    table tr td .subtable tr td { padding: 5px; }
</style>
<%int count = Model.CurrentPage * 50 - 50; %>
<%foreach (var item in Model.Source.ToList())
    { %>
<%      count++;
%>
<tr>
    <td class="Cell CellBegin"><%=count %></td>
    <td class="Cell"><%:item.UpdateDateNew.ToString("dd-MM-yyyy") %></td>
    <td class="Cell CellEnd"><%:item.TotalCount %></td>
    <%foreach (var itemNew in item.Usercounts.OrderBy(x => x.UserName))
        {%>
    <td class="Cell"><span style="color: #b00606; font-size: 14px; font-weight: 600"><%:itemNew.Count %></span></td>

    <%} %>
</tr>

<%} %>
<%int colspan = 4 + Model.Source.FirstOrDefault().Usercounts.Count; %>
<td class="ui-state ui-state-default" colspan="<%:colspan %>" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
        <div style="float: left; margin-right: 10px;">
            <b><%:Model.CurrentPage %> . sayfadasınız
        </div>
        <ul style="float: right;">

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
                <a href="javascript:void(0)" onclick="PagingNotCount(<%:i %>)">
                    <%: i%></a>&nbsp;
			 <% } %>
            </li>
            <% } %>
        </ul>
    </div>
</td>

</tr>
<tr>

    <td class="ui-state ui-state-default" colspan="<%:colspan %>" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <b>Toplam Adet:</b><%:Model.TotalRecord %>
    </td>
</tr>
