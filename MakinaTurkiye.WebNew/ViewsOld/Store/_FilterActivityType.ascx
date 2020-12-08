<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreActivityTypeFilterModel>" %>
<%if (Model.ActivityTypeFilterItemModels.Count > 0)
  {
      var listActivityTypeFilters = Model.ActivityTypeFilterItemModels.OrderByDescending(p => p.Filtered).ThenBy(p => p.FilterItemName);
%>
<div class="panel panel-mt panel-mtv2">
    <div class="panel-heading">

        <span class="glyphicon glyphicon-filter"></span>
        <span class="title">Filtre</span>

         <a href="javascript:;" role="button" data-toggle="collapse" data-parent="#filters" data-target="#filter-activity-body">
            <span class="more-less icon-down-arrow"></span>
        </a>
    </div>
    <div class="panel-body collapse" id="filter-activity-body">
        <div class="list-group list-group-mt3" style="overflow: auto; max-height: 200px;">
            <%
      foreach (var item in listActivityTypeFilters)
      {
          if (item.Filtered)
          {
            %>
            <div class="list-group-item">
                <a href="<%: item.FilterUrl %>" style="color: #eb3b25"><i class="text-md fa fa-fw fa-check-square-o" aria-hidden="true"></i>
                    <%: item.FilterItemName %>
                </a>
            </div>
            <%
                  }
                  else
                  {
            %>
            <div class="list-group-item unselected">
                <a href="<%:item.FilterUrl %>"><i class="text-md fa fa-fw fa-square-o"></i>
                    <%:item.FilterItemName %>
                </a>
            </div>
            <%
              }

              }
            %>
        </div>

    </div>
</div>
<% } %>
<style>
    /*Activity Type Filter */
    .span-activity-filter-toggle:hover {
        cursor: pointer;
    }
    /*Activity Type Filter */
</style>
