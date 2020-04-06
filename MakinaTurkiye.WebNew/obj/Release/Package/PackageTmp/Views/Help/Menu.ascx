﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHelpMenuModel>>" %>
    

<% foreach (var item in Model)
   {
       if (item.CategoryParentId == 0)
       {%>
<div class="panel panel-mt help-menu">
    <div class="panel-heading">
        <h5 class="panel-title">
            <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree<%:item.CategoryId %>">
                <span class="glyphicon glyphicon-chevron-down"></span>
                <%:item.CategoryName %>
            </a>
        </h5>
    </div>
    <div id="collapseThree<%:item.CategoryId %>" class="panel-collapse collapse">
        <div class="panel-body pl0">
            <ol>
                <%foreach (var i in Model)
                  {%>
                <%if (i.CategoryParentId == item.CategoryId)
                  {%><li>
                    
                      <a href="<%:i.HelpUrl %>"><%:i.CategoryName %></a>
                  
                  </li>
                <% } %>
                <% } %>
            </ol>
        </div>
    </div>
</div>
<%} %>
<% } %>
