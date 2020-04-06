﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<MemberModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.Source)
    { %>
<% row++; %>
<tr id="row<%: item.MainPartyId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin" style="height: 30px;">
        <input type="checkbox" name="cb_<%: item.MainPartyId %>" id="cb_<%: item.MainPartyId %>" />
    </td>
    <td class="Cell" style="height: 30px;">
        <%: item.MemberNo %>
    </td>
    <td class="Cell">
        <%: item.MainPartyFullName %>
    </td>
    <td class="Cell">
        <%: item.StoreName %>
    </td>
    <td class="Cell">
        <%: item.MemberTypeText %>
    </td>
    <td class="Cell">
        <%: item.MemberEmail %>
    </td>

   <%-- <td>
        <%  var phone = item.PhoneItems.Where(x => x.PhoneType == (byte)PhoneType.Gsm).FirstOrDefault();
            if (phone != null)
            {
                if (phone.active == 1)
                {
        %>
        <span style="color: #04890f"><%:phone.PhoneCulture+" "+phone.PhoneAreaCode+" "+phone.PhoneNumber %></span>

        <%}
            else
            {%>
        <span style=""><a onclick="PhoneActiveMailSend(<%:item.MainPartyId %>)" style="cursor: pointer; color: #ad0808;"><%:phone.PhoneCulture+" "+phone.PhoneAreaCode+" "+phone.PhoneNumber %></a></span>

        <%}
            } %>  
    </td>--%>
    <td class="Cell">
        <%if (item.Active)
            {%>
        <div style="float: left;">
            Aktif
        </div>
<%--        <div style="float: left; background-image: url('/Content/Images/Goodshield.png'); height: 16px; width: auto;">
            <img src="/Content/Images/Goodshield.png" />
        </div>--%>
        <%} %>
        <%else
            {%>
        <div style="float: left;">
            Pasif
        </div>
        <%--<div style="float: left; background-image: url('/Content/Images/Errorshield.png'); height: 16px;">
            <img src="/Content/Images/Errorshield.png" />
        </div>
        <div style="float: left; margin-left: 20px; width: 40px; text-align: center; cursor: pointer;">
            <a onclick="Activation(<%: item.MainPartyId %>);">
                <img src="/Content/Images/activationicon.png" /></a>
        </div>--%>

        <%} %>
      
    </td>
   <%-- <td class="Cell">
        <%if (item.FastMemberShipType == (byte)FastMembershipType.Facebook)
            {
                Response.Write("F");
            }
            else if (item.FastMemberShipType == (byte)FastMembershipType.Normal)
            {
                Response.Write("B");
            }
            else if (item.FastMemberShipType == (byte)FastMembershipType.Phone)
            {
                Response.Write("T");
            }
            else
            {
                Response.Write("");
            }


        %>
    </td>--%>
    <%--<td class="CellEnd" style="text-align: center; width: 140px;">

        <a href="/Member/Edit/<%: item.MainPartyId %>">
            <img src="/Content/images/edit.png" hspace="5" />
        </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.MainPartyId %>);">
            <img src="/Content/images/delete.png" hspace="5" />
        </a><a href="/Member/BrowseDesc/<%: item.MainPartyId %>">
            <img src="/Content/images/product.png" />
        </a>
        <div style="float: right; width: 30px; text-align: right; cursor: pointer;">
            <a href="/Member/memberactivation/<%=item.MainPartyId %>" id="lightbox_click" rel="superbox[iframe]">
                <img src="/Content/Images/ikon_ozel_mesaj_gonder.png" /></a>
        </div>
    </td>--%>
    <td class="Cell" title="<%: item.MainPartyRecordDate.ToString("dd MMMM yyyy dddd") %>">
        <%: item.MainPartyRecordDate.ToString("dd.MM.yyyy HH:mm:ss")%>
    </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
    { %>
<tr class="Row">
    <td colspan="10" class="CellBegin Cell" style="color: #FF0000; font-weight: 700; font-size: 14px;">Üye bulunamadı.
    </td>
</tr>
<% } %>
<tr>
    <td class="ui-state ui-state-default" colspan="10" align="right" style="border-color: #DDD; border-top: none;">
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
                <li>Gösterim: </li>
                <li>
                    <select id="PageDimension" name="PageDimension" onchange="SearchPost();">
                        <option value="20" <%: Session["member_PAGEDIMENSION"].ToString() == "20" ? "selected=selected" : "" %>>20</option>
                        <option value="50" <%: Session["member_PAGEDIMENSION"].ToString() == "50" ? "selected=selected" : "" %>>50</option>
                        <option value="100" <%: Session["member_PAGEDIMENSION"].ToString() == "100" ? "selected=selected" : "" %>>100</option>
                    </select>
                </li>
            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-hover" colspan="10" align="right" style="border-color: #DDD; border-top: none;">Toplam Kayıt : &nbsp;&nbsp;<strong>
        <%: Model.TotalRecord %></strong>
    </td>
</tr>
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
<script type="text/javascript">
    $(function () {
        $.superbox.settings = {
            closeTxt: "Kapat",
            loadTxt: "Yükleniyor...",
            nextTxt: "Sonraki",
            prevTxt: "Önceki"
        };
        $.superbox();
    });
</script>
