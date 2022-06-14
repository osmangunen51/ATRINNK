﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.PreRegistrationItem>>" %>

<%int count = Model.CurrentPage * 20 - 19; %>
<%foreach (var item in Model.Source.ToList())
    {%>
<tr id="row<%:item.Id%>">
    <td class="Cell CellBegin"><%:count %></td>
    <td class="Cell"><%:item.StoreName %></td>
    <td class="Cell"><%:item.MemberName %> <%:item.MemberSurname %></td>
    <td class="Cell"><%:item.Email %></td>
    <td class="Cell"><%:item.PhoneNumber %></td>
    <td class="Cell"><%:item.PhoneNumber2 %><br /><%:item.PhoneNumber3 %></td>
    <td class="Cell">
        <%if (!string.IsNullOrEmpty(item.WebUrl)) {%>
        <a href="<%:item.WebUrl %>" target="_blank"><%:item.WebUrl %></a>
        <% } %>
    </td>
    <td class="Cell"><%:item.City %></td>
    <td class="Cell"><%:item.RecordDate.ToString("dd/MM/yyyy HH:mm") %></td>
    <td class="Cell CellEnd">
        <a href="/PreRegistrationStore/Edit/<%:item.Id %>">
            <img src="/Content/images/edit.png" /></a>
        <a  href="/PreRegistrationStore/Delete?Id=<%=item.Id  %>"  id="lightbox_click" rel="superbox[iframe]"><img src="/Content/images/delete.png" hspace="2"></a>
        <a href="/Member/BrowseDesc1/<%:item.Id %>">
            <%if (item.HasDescriptions)
                {%>
            <img src="/Content/images/productonay.png">
            <% }
                else
                {%>
            <img src="/Content/images/product.png">
            <% } %>  
        </a>
        <%if (item.IsInserted)
            {%>
        <span title="Firma Olarak Eklenmiş.">
        F.Eklendi
            </span>
        <% }
                                  else {%>
                      <a  href="/PreRegistrationStore/NewStore?preRegistrationId=<%=item.Id  %>" title="Firma Kaydı Olarak Ekle" >Firma O.</a>
        <% } %>
  

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
                    <a href="javascript:void(0)" onclick="PagingPost(<%:i %>)">
                        <%: i%></a>&nbsp;
			 <% } %>
                </li>
                <% } %>
            </ul>
        </div>
    </td>

</tr>
