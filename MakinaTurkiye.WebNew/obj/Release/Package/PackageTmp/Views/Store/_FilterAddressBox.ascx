<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreAddressFilterModel>" %>
<%if (Model.CityFilterItemModels.Count > 0)
  { %>

<div class="panel panel-mt panel-mtv2">
    <div class="panel-heading">
        <span class="icon-map-pin"></span>
        <span class="title">Adres</span>


        <a href="javascript:;" role="button" data-toggle="collapse" data-parent="#filters" data-target="#adres-body">
            <span class="more-less icon-down-arrow"></span>
        </a>
    </div>


    <div class="panel-body collapse" id="adres-body">
        <div class="list-group list-group-mt3" style="overflow: auto; max-height: 200px;">
            <%foreach (var item in Model.CityFilterItemModels)
              { %>
            <%if (item.Filtered)
              { %>
            <div class="list-group-item selected">
                <a href="<%:item.FilterUrl %>"><i class="text-md fa fa-fw fa-check-square-o" aria-hidden="true"></i>
                    <%:item.FilterItemName%>
                    <span class="text-muted text-sm">(<%:item.FilterItemStoreCount%>)</span>
                </a>
            </div>
                <div class="list-group list-group-mt3" style="max-height: 277px; margin-left: 40px;margin-bottom: 10px">
                    <%foreach (var item2 in Model.LocalityFilterItemModels)
                      { %>
                    <%if (item2.Filtered)
                      { %>
                    <div class="list-group-item p5">
                        <a href="<%=item2.FilterUrl%>" class="text-bold" ><i class="text-md fa fa-fw fa-check-square-o"></i><%=item2.FilterItemName %> (<%=item2.FilterItemStoreCount%>) </a>
                    </div>
                    <%}
                      else
                      {%>
                    <div class="list-group-item p5">
                        <a href="<%=item2.FilterUrl%>"><i class="text-md fa fa-fw fa-square-o"></i><%=item2.FilterItemName %> (<%=item2.FilterItemStoreCount%>)</a>
                    </div>
                    <%}%>

                    <%} %>
                </div>
        
            <%}

              else
              {%>
            <div class="list-group-item unselected">
                <a href="<%:item.FilterUrl %>"><i class="text-md fa fa-fw fa-square-o"></i>
                    <%:item.FilterItemName %>
                    <span class="text-muted text-sm">(<%:item.FilterItemStoreCount %>)</span>
                </a>
            </div>
            <%} %>
            <%} %>
        </div>
    </div>
</div>
<% } %>

<%--<%if (Model.LocalityFilterItemModels.Count > 0)
  {%>
<div class="panel panel-mt">
    <div class="panel-heading">
        <span class="glyphicon glyphicon-th-list"></span>İlçeler
    </div>
    <div class="list-group list-group-mt3" style="overflow: auto; max-height: 277px;">
        <%foreach (var item in Model.LocalityFilterItemModels)
          { %>
        <%if (item.Filtered)
          { %>
        <div class="list-group-item p5">
            <a href="<%=item.FilterUrl%>" class="text-bold"><i class="text-md fa fa-fw fa-check-square-o"></i><%=item.FilterItemName %> (<%=item.FilterItemStoreCount%>) </a>
        </div>
        <%}
          else
          {%>
        <div class="list-group-item p5">
            <a href="<%=item.FilterUrl%>"><i class="text-md fa fa-fw fa-square-o"></i><%=item.FilterItemName %> (<%=item.FilterItemStoreCount%>)</a>
        </div>
        <%}%>

        <%} %>
    </div>
</div>
<%} %>--%>
