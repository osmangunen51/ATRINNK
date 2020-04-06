<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<SurveyModel>>" %>
<% int row = 0; %>
<% foreach(var item in Model.Source) { %>
<% row++; %>
<tr id="row<%: item.SurveyId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin" align="center">
    <span class="ui-icon ui-icon-plusthick" style="cursor: pointer;" id="sp<%: item.SurveyId %>"
      onclick="OpenOption(this.id, '#trOption<%: item.SurveyId %>', '#row<%: item.SurveyId %>');">
    </span>
  </td>
  <td class="Cell">
    <%: item.SurveyQuestion %>
  </td>
  <td class="CellEnd">
    <a href="/Survey/Edit/<%: item.SurveyId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="Delete(<%: item.SurveyId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<tr class="ui-helper-hidden" id="trOption<%: item.SurveyId %>" style="background-color: #DDD; ">
  <td>
  </td>
  <td>
    <%= Html.RenderHtmlPartial("SurveyOptionList", item.GetOptions(item.SurveyId)) %>
  </td>
  <td>
  </td>
</tr>
<% } %>
<% if(Model.TotalRecord <= 0) { %>
<tr class="Row">
  <td colspan="3" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700;
    font-size: 14px;">
    Anket bulunamadı.
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="3" align="right" style="border-color: #DDD;
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
          <a onclick="Page(<%: page %>)">
            <%: page %></a>&nbsp;
          <% } %>
        </li>
        <% } %>
      </ul>
    </div>
  </td>
</tr>
