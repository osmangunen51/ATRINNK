﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NewsModel>>" %>
<% int row = 0; %>
<% foreach(var item in Model.Source) { %>
<% row++; %>
<tr id="row<%: item.NewsId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <div style="overflow: hidden; width: 100%; float: left; height: 15px;">
      <%: item.NewsTitle%></div>
  </td> 
  <td class="Cell" title="<%: item.NewsDate.ToString("dd MMMM yyyy dddd") %>">
    <%: item.NewsDate.ToString("dd.MM.yyyy")  %>
  </td>
  <td class="Cell">
    <%: item.Active %>
  </td>
  <td class="CellEnd">
    <a href="/News/Edit/<%: item.NewsId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
        <%--<div style="float: right; margin-top: 2px; margin-left: 3px;">
          Düzenle</div>--%>
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.NewsId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
        <%--<div style="float: right; margin-top: 2px; margin-left: 3px;">
          Sil</div>--%>
      </div>
    </a>
  </td>
</tr>
<% } %>
<% if(Model.TotalRecord <= 0) { %>
<tr class="Row">
  <td colspan="7" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700;
    font-size: 14px;">
    Haber bulunamadı.
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="4" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
        <% foreach(int page in Model.TotalLinkPages) { %>
        <li>
          <% if(page == Model.CurrentPage) { %>
          <span class="currentpage">
            <%: page %></span>&nbsp;
          <% } %>
          <% else { %>
          <a onclick="PagePost(<%: page %>)">
            <%: page %></a>&nbsp;
          <% } %>
        </li>
        <% } %>
      </ul>
    </div>
  </td>
</tr>
