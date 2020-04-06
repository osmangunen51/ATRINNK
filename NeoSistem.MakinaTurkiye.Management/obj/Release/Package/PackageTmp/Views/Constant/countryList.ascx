﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<Country>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.CountryId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin"><%:item.CountryId %></td>
  <td class="Cell">
    <%: item.CountryName %>
  </td>
     <td class="Cell">
    <%: item.CultureCode %>
  </td>
        <td class="Cell">
  <%if (item.Active==true)
    { %>

    Onaylanmış
     
   <%}else{ %>
    Onaylanmamış
    <%} %>
    </td>
  <td class="CellEnd" style="width:100px;">

    <a style="padding-bottom: 5px; cursor: pointer" href="/Constant/Country?id=<%:item.CountryId %>">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.CountryId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
              
   <%--     <% foreach (int page in Model.TotalLinkPages)
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
            <option value="20" <%: Session["member_PAGEDIMENSION"].ToString() == "20" ? "selected=selected" : "" %>>
              20</option>
            <option value="50" <%: Session["member_PAGEDIMENSION"].ToString() == "50" ? "selected=selected" : "" %>>
              50</option>
            <option value="100" <%: Session["member_PAGEDIMENSION"].ToString() == "100" ? "selected=selected" : "" %>>
              100</option>
          </select>
        </li>--%>
      </ul>
    </div>
  </td>
</tr>
