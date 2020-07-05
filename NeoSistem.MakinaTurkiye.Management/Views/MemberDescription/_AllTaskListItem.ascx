<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionTaskItem>>" %>
<%:Html.HiddenFor(x=>x.OrderName,new {id="OrderName" }) %>
<%:Html.HiddenFor(x=>x.Order,new {id="OrderType" }) %>
<%:Html.HiddenFor(x=>x.CurrentPage,new {id="CurrentPage" }) %>
<%int row = 0; %>
<%foreach (var itemMemberDesc in Model.Source)
    {
        row++;
        string backColor = "";
        if (itemMemberDesc.Title == "Ödeme")
        {
            backColor = "#dabd9b";
        }
%>
<tr id="row<%:itemMemberDesc.ID%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate")%>" style="background-color: <%:backColor%>">
    <td class="Cell CellBegin"><%:itemMemberDesc.ID%></td>
    <td class="Cell">
        <%if (!string.IsNullOrEmpty(itemMemberDesc.MemberName))
            {%>
        <%: itemMemberDesc.MemberName+" "+itemMemberDesc.MemberSurname%>

        <% }
            else
            {%>
        <%:itemMemberDesc.PreMemberName+" "+itemMemberDesc.PreMemberSurname %>
        <% } %>


    </td>

    <td class="Cell">
        <%if (!string.IsNullOrEmpty(itemMemberDesc.PreStoreName))
            {%>
        <%:itemMemberDesc.PreStoreName %>
        <% }
            else
            {%>
        <a href="/Store/EditStore/<%:itemMemberDesc.StoreMainPartyId %>"><%:itemMemberDesc.StoreName%></a>

        <% } %>
    </td>
    <td class="Cell"><%:itemMemberDesc.Title%></td>
    <td class="Cell" style="font-size: 15px;"><%Response.Write(itemMemberDesc.Description); %></td>
    <td class="Cell" style="font-size: 10px;">
        <font style="font-weight: 600; font-size: 13px;"><%Response.Write(itemMemberDesc.Date.ToString().Split(' ')[0]); %></font>
        <br />
        <%:itemMemberDesc.Date.ToString().Split(' ')[1]%>
    </td>
    <td class="Cell">
        <font style="font-weight: 600; font-size: 13px;"><%Response.Write(itemMemberDesc.UpdateDate.ToString().Split(' ')[0]); %></font>
        <br />
        <font style="color: #a02323; font-size: 13px; font-weight: 600;"><%:itemMemberDesc.UpdateDate.ToString().Split(' ')[1]%></font>
    </td>
    <td class="Cell" style="background-color: #2776e5; color: #fff"><%:itemMemberDesc.FromUserName %></td>
    <td class="Cell" style="background-color: #31c854; color: #fff"><%:itemMemberDesc.UserName %></td>
    <td class="Cell">
        <%string type = "Normal";
            if (!string.IsNullOrEmpty(itemMemberDesc.PreStoreName))
            {
                type = "Ön Kayıt";
            }
        %>
        <%:type %>
    </td>
    <td class="Cell CellEnd">
        <a title="Açıklama" href="/Member/EditDesc1/<%:itemMemberDesc.ID%>">
            <img src="/Content/images/ac.png" style="width: 16px;" />
        </a>
        <%if (itemMemberDesc.FromUserName == itemMemberDesc.UserName && itemMemberDesc.UserName == ViewData["CurrentUserName"].ToString())
            {%>
        <a href="/MemberDescription/UpdateDescriptionDate/<%=itemMemberDesc.ID %>" id="lightbox_click" rel="superbox[iframe]">
            <img src="/Content/Images/newtime.png" style="width: 16px;" /></a>
        <% } %>
    </td>
</tr>

<%}
%>
<tr>
    <td class="ui-state ui-state-hover" colspan="11" align="right" style="border-color: #DDD; border-top: none; padding-right: 10px">
        <div style="float: right;" class="pagination">
            <ul>
                <%
                    foreach (int i in Model.TotalLinkPages)
                    {
                        if (i == Model.CurrentPage)
                        {%>
                <li><span class="currentpage"><%:i %></span></li>
                <%}
                    else
                    {
                %>
                <li><a onclick="PagePost(<%:i %>);"><%:i %></a></li>
                <%}
                    }
                %>
            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-hover" colspan="11" align="right" style="border-color: #DDD; border-top: none; padding-right: 10px">Toplam Kayıt : &nbsp;&nbsp;<strong>
        <%:Model.TotalRecord%></strong>
    </td>
</tr>
