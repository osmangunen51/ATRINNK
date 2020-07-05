﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>
<%@ Import Namespace="MakinaTurkiye.Utilities.HttpHelpers" %>

<div class="col-sm-5 col-md-4 col-lg-3">
    <div class="panel panel-mt2 left-menu">
      
        <nav>
            <ul class="list-group list-group-mt" role="menubar">

                <%foreach (var parentCategoryItem in Model.ParentCategoryItems.OrderBy(c => c.CategoryOrder).ThenBy(c => c.CategoryName))
                  {
                      string paretntUrl = "/" + Html.ToUrl(parentCategoryItem.CategoryName) + "-c-" + parentCategoryItem.CategoryId;
                      
                      if (parentCategoryItem.ProductCount != 0)
                      { %>
                <li class="list-group-item" role="menuitem"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;&nbsp;
                    <a href="<%:paretntUrl %>" class="text-bold">
                        <%:parentCategoryItem.CategoryName%>
                        <span class="text-muted text-sm">(<%:parentCategoryItem.ProductCount%>)</span>
                    </a></li>
      
                <% }
                  } 
                %>
            </ul>
            
        </nav>
    </div>
</div>
