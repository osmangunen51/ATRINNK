<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.BaseMemberDescriptionModelItem>>" %>

<%int row = 0; %>
<%foreach (var itemMemberDesc in Model.Source.ToList())
    {
%>
<tr id="row<%:itemMemberDesc.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="Cell"><a href="/Store/EditStore/<%:itemMemberDesc.StoreID %>"><%:itemMemberDesc.StoreName %></a></td>
    <td class="Cell"><%:itemMemberDesc.Title %></td>
    <td class="Cell" style="font-size: 15px;"><% if (itemMemberDesc.Description != null)
                                                  { %>
        <%=Html.Raw(itemMemberDesc.Description)%>
        <% }
        %>
    </td>
    <td class="Cell"><% if (itemMemberDesc.InputDate != null)
                         {
                             string[] Inputdate = itemMemberDesc.InputDate.ToString().Split(' ');
                             Response.Write(Inputdate[0] + " ");
                             Response.Write("<font style='color:#C5D5DD'>" + Inputdate[1] + "</font>");
                         }%></td>
    <td class="Cell"><%if (itemMemberDesc.LastDate!=null)
                         {
                             string[] lastDate = itemMemberDesc.LastDate.ToString().Split(' ');
                             Response.Write(lastDate[0] + " ");
                             Response.Write("<font style='color:#9d0825'>" + lastDate[1] + "</font>");
                         } %></td>
    <td class="Cell" style="background-color: #2776e5; color: #fff"><%:itemMemberDesc.FromUserName %></td>
    <td class="Cell" style="background-color: #31c854; color: #fff">
        <%:itemMemberDesc.ToUserName %>
    </td>
    <td class="Cell">
        <a href="/StoreSeoNotification/Create/<%:Request.QueryString["storeMainPartyId"] %>?storeNotId=<%:itemMemberDesc.ID %>">
            <img src="/Content/images/ac.png" />
        </a>



    </td>
</tr>

<%} %>


<tr>
    <td class="ui-state ui-state-default" colspan="8" align="right" style="border-color: #DDD; border-top: none;">
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
                    <a onclick="PagePost(<%: page %>)">
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


