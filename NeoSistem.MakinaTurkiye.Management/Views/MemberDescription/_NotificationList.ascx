﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.NotificationModel>>" %>
<%:Html.HiddenFor(x=>x.CurrentPage,new {id="CurrentPage" }) %>
<%if (Model.Source.ToList().Count > 0)
    {%>
<%int row = 0; %>



<%foreach (var itemMemberDesc in Model.Source.ToList())
    {
        string backgroundColor = "";
        var compateFisrst = DateTime.Compare(itemMemberDesc.LastDate.Value.AddMinutes(45), DateTime.Now);
        var compareSecond = DateTime.Compare(DateTime.Now, itemMemberDesc.LastDate.Value);
        if (compateFisrst > 0 && itemMemberDesc.IsImmediate)
        {
            backgroundColor = "background-color:#f97b7b";
            if (itemMemberDesc.LastDate.Value.AddMinutes(30) > DateTime.Now)
            {
                backgroundColor = "background-color:#f1dc65";
            }
        }
        else if (compareSecond > 0 && itemMemberDesc.IsImmediate)
        {
            backgroundColor = "background-color:#f97b7b";
        }
        row++;
%>
<tr id="row<%:itemMemberDesc.ID%>" style="<%: backgroundColor%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate")%>">
    <td class="Cell CellBegin"><%:row%></td>
    <td class="Cell"><%: itemMemberDesc.MemberName%></td>

    <td class="Cell">
        <%if (itemMemberDesc.StoreMainPartyId == 0)
            {%>
        <%:itemMemberDesc.StoreName %>
        <% }
            else
            {%>
        <a target="_blank" href="/Store/EditStore/<%:itemMemberDesc.StoreMainPartyId %>"><%:itemMemberDesc.StoreName%></a>
        <% } %>
    </td>
    <td class="Cell"><%:itemMemberDesc.Title%></td>
    <td class="Cell" style="font-size: 15px; width: 680px;"><%Response.Write(itemMemberDesc.Description); %></td>
    <td class="Cell" style="font-size: 10px;">
        <span style="font-weight: 600; font-size: 13px;"><%Response.Write(itemMemberDesc.InputDate.ToString().Split(' ')[0]); %></span>
        <br />
        <%:itemMemberDesc.InputDate.ToString().Split(' ')[1]%>
                      
    </td>
    <td class="Cell">
        <span style="color: #808080; font-size: 10px;"><%Response.Write(itemMemberDesc.LastDate.ToString().Split(' ')[0]); %></span>
        <br />
        <span style="color: #a02323; font-size: 13px; font-weight: 600;"><%:itemMemberDesc.LastDate.ToString().Split(' ')[1]%></span>
    </td>
    <td class="Cell" style="background-color: #2776e5; color: #fff"><%:itemMemberDesc.FromUserName %></td>
    <td class="Cell" style="background-color: #31c854; color: #fff"><%:itemMemberDesc.SalesPersonName %></td>
    <td class="Cell">
        <%string type = "Normal";
            if (itemMemberDesc.StoreMainPartyId == 0)
            { type = "Ön Kayıt"; }%>
        <%:type %>

    </td>
    <td class="Cell CellEnd">
        <a title="Açıklama" href="/Member/EditDesc1/<%:itemMemberDesc.ID%>">
            <img src="/Content/images/ac.png" style="width: 24px;" />
        </a>
        <%if (Request.QueryString["UserId"] != null)
            {
                if (itemMemberDesc.IsImmediate)
                { %>
                   Çok Acil
        <%}
            else
            {
                if (itemMemberDesc.IsFirst == true)
                {%>
        <a style="color: #339966;" href="/MemberDescription/FirstProcess/<%:itemMemberDesc.ID %>?userId=<%:Request.QueryString["UserId"].ToString() %>&isFirst=0"><b>Öncelikli</b> Kaldır</a>
        <%}
            else
            {%>
        <%if (itemMemberDesc.FromUserName == itemMemberDesc.SalesPersonName)
            {%>
        <a href="/MemberDescription/UpdateDescriptionDate/<%=itemMemberDesc.ID %>" id="lightbox_click" rel="superbox[iframe]">
            <img src="/Content/Images/newtime.png" style="width: 16px;" /></a>
        <% } %>
        <a href="/MemberDescription/FirstProcess/<%:itemMemberDesc.ID %>?userId=<%:Request.QueryString["UserId"].ToString() %>&isFirst=1">Öncelik Ver</a>
        <%}
                }
            }%>
       
    </td>
</tr>
<%}
    }
    else
    {%>
<%:Html.Raw("<tr><td></td><td colspan='8'><span style='color:red'>Kayıtlı Bildirim Bulunamadı</td></tr>")%>
<%  } %>
<tr>
    <td class="ui-state ui-state-hover" colspan="11" align="right" style="border-color: #DDD; border-top: none; padding-right: 10px">
        <div style="float: right;" class="pagination">
            <b>Toplam Kayıt:<%:Model.TotalRecord %></b>
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
                <li><a onclick="PagingPost(<%:i %>)"><%:i %></a></li>
                <%
                        }
                    }
                %>
            </ul>
        </div>
    </td>
</tr>
