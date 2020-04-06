<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreCategoryModel>" %>
<div class="panel panel-mt panel-mtv2">
      <div class="panel-heading left-menu-header">
        <span class="icon-menu"></span>
        <span class="title"><a style="display: inline-block; margin: 0; font-size: 13px; color:#000;font-weight: 700;" href="javascript:;">Kategoriler</a></span>
        <a href="javascript:;" role="button"  data-toggle="collapse" data-parent="#filters" data-target="#menu-body">
            <span class="more-less icon-up-arrow"></span>
        </a>
    </div>
    <div class="panel-body collapse in" id="menu-body">
        <ul class="list-group list-group-mt3" role="menubar">
            <%if (Model.SelectedCategoryId == 0)
              {  %>
            <% foreach (var item in Model.StoreCategoryItemModels)
               {%>
            <li class="list-group-item" role="menuitem">&nbsp;&nbsp;&nbsp;
                        <a href="<%:item.CategoryUrl %>">
                            <%=item.CategoryName%> 
                        </a>
            </li>
            <% } %>
            <%}
              else
              {  %>
            <%
                  string leftCategoryPrefix = string.Empty;
                  for (int i = 0; i < Model.StoreTopCategoryItemModels.Count; i++)
                  {
            %>
            <li class="list-group-item"><%:MvcHtmlString.Create(leftCategoryPrefix)%>

                <a href='<%:Model.StoreTopCategoryItemModels[i].CategoryUrl %>'>
                    <%if (Model.SelectedCategoryId == Model.StoreTopCategoryItemModels[i].CategoryId)
                      { %>
                    <b><%=Model.StoreTopCategoryItemModels[i].CategoryName%></b>
                    <%}
                      else
                      { %>
                    <%=Model.StoreTopCategoryItemModels[i].CategoryName%>
                    <%} %>
                </a>
            </li>
            <%  leftCategoryPrefix += "&nbsp;&nbsp;";
                      } %>

            <%
      
                  foreach (var item in Model.StoreCategoryItemModels)
                  {%>
            <%if (item.SubStoreCategoryItemModes.Count == 0)
              { %>
            <li class="list-group-item"><%:MvcHtmlString.Create(leftCategoryPrefix)%>

                <a href="<%:item.CategoryUrl %>">
                    <%: item.CategoryName%>  
                </a>
            </li>
            <%} %>

            <%if (item.SubStoreCategoryItemModes.Count > 0)
              { %>
            <li class="list-group-item" role="menuitem"><%:MvcHtmlString.Create(leftCategoryPrefix)%>

                <a href="<%:item.CategoryUrl %>" class="text-bold">
                    <%: item.CategoryName%> 
                </a>
            </li>
            <%  leftCategoryPrefix += "&nbsp;&nbsp;&nbsp;";
                foreach (var subItem in item.SubStoreCategoryItemModes)
                {%>
            <li class="list-group-item" role="menuitem"><%:MvcHtmlString.Create(leftCategoryPrefix)%><i class="fa fa-angle-double-right"></i>
                <a href="<%:subItem.CategoryUrl %>">
                    <%: subItem.CategoryName%> 
                </a>
            </li>
            <% 
                    if (item.SubStoreCategoryItemModes.LastOrDefault().CategoryId == subItem.CategoryId)
                    {
                        leftCategoryPrefix = leftCategoryPrefix.Substring(0, leftCategoryPrefix.Length - 18);
                    }
                } %>
            <%} %>
            <%} %>
            <%} %>
        </ul>
    </div>
</div>
