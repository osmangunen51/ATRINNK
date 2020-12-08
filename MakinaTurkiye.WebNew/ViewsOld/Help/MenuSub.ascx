<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTHelpTopModel>" %>
<% foreach (var item in Model.MenuItemModels)
   {
       if (item.CategoryParentId == 0)
       {%>
<div class="panel panel-mt">
    <div class="panel-heading">
        <h5 class="panel-title">
    
            <a data-toggle="collapse"  data-parent="#accordion" href="#collapseThree<%:item.CategoryId %>">
                <span class="glyphicon glyphicon-chevron-right"></span>
                <%:item.CategoryName %>
            </a>
        </h5>
    </div>

     <% string style="collapse";
       
             if (item.CategoryId ==Model.CurrentMenuModel.CategoryParentId)
             {
                 style = "collapse in";
               
             }
          %>

    <div id="collapseThree<%:item.CategoryId %>" class="panel-collapse <%=  style %>">
        <div class="panel-body pl0">
            <ol>
                <%foreach (var item2 in Model.MenuItemModels)
                  {%>
                <%if (item2.CategoryParentId == item.CategoryId)
                  {%><li>
                         <a  rel="nofollow"  href="<%:item2.HelpUrl %>"><%:item2.CategoryName %></a>
                    </li>
                <% } %>
                <% } %>
            </ol>
        </div>
    </div>
</div>
<% } %>
<%}%>