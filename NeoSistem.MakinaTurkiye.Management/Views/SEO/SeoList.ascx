﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<SeoModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.Source)
   { %>
<% row++; %>
<tr id="row<%: item.SeoId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.PageName %>
  </td>
  <td class="Cell">
    <%: item.Title %>
  </td>
  <td class="CellEnd">
    <a href="/Seo/Edit/<%: item.SeoId %>" style="padding-bottom: 5px;">
    
        <img style="float: left; margin-right: 10px; display: block;" src="/Content/images/edit.png" />
      
    </a>
  </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
   { %>
<tr class="Row">
  <td colspan="6" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700; font-size: 14px;">
    
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="6" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
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
          <a onclick="Page(<%: page %>)">
            <%: page %></a>&nbsp;
          <% } %>
        </li>
        <% } %>
      </ul>
    </div>
  </td>
</tr>
