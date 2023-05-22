<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<ActivityTypeModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.Source)
   { %>
<% row++; %>
<tr id="row<%: item.ActivityTypeId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.ActivityName %>
  </td>
    <td class="Cell">
        <%:item.Order %>
    </td>
  <td class="CellEnd">
    <a href="/ActivityType/Edit/<%: item.ActivityTypeId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.ActivityTypeId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="8" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>
