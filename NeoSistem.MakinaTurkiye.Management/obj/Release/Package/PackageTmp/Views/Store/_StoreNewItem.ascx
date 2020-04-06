<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreNewItem>>" %>

<%int count = Model.CurrentPage * 20 - 19; %>
<%foreach (var item in Model.Source.ToList())
    {%>
<tr>
    <td class="Cell CellBegin"><%:count %></td>
    <td class="Cell"><img src="<%:item.ImagePath %>" style="width:70px;" /></td>
    <td class="Cell"><%:item.StoreName %></td>
    <td class="Cell"><%:item.Title %></td>
    <td class="Cell"><%:item.RecordDate.ToString("dd/MM/yyyy HH:mm") %></td>
    <td class="Cell"><%:item.UpdateDate.ToString("dd/MM/yyyy HH:mm") %></td>
    <td class="Cell"><%:item.ViewCount %></td>
    <td class="Cell">
        <%if (item.Active)
            {%>
            Onaylandı
        <% }
                              else {%>
            Onay Bekliyor
        <% } %>
    </td>
    <td class="Cell CellEnd">
        <input type="checkbox" class="checkboxNew" value="<%:item.StoreNewId %>" />
        <a href="/Store/EditNew/<%:item.StoreNewId %>"><img src="/Content/images/edit.png" /></a>
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
                <li><button value="Onayla" type="button" onclick="ConfirmNew(1)">Onayla</button></li>
                <li><button value="Onayla" type="button" onclick="ConfirmNew(0)">Onay Kaldır</button></li>
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
                    <a href="javascript:void(0)" onclick="PagingNew(<%:i %>)">
                        <%: i%></a>&nbsp;
			 <% } %>
                </li>
                <% } %>
            </ul>
        </div>
    </td>

</tr>
