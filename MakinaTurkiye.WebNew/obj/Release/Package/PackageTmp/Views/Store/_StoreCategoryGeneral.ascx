<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreCategoryModel>" %>
<div class="panel panel-mt2 left-menu">
<%--    <div class="panel-heading">
        <h5 class="panel-title">
            <span class="glyphicon glyphicon-globe"></span><a href="#">&nbsp;Sektörler</a>
        </h5>
    </div>--%>
    <nav>
        <ul role="menubar" class="list-group list-group-mt" >
       <%if (Model.SelectedCategoryId == 0)
         {  %>
            <% foreach (var item in Model.StoreCategoryItemModels)
               {%>
                      <li class="list-group-item" role="menuitem">
                        <i class="fa fa-angle-right"></i>
                        &nbsp;&nbsp;&nbsp;
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
                <i class="fa fa-angle-right"></i>
                <a href='<%:Model.StoreTopCategoryItemModels[i].CategoryUrl %>' >
                    <%if (Model.SelectedCategoryId == Model.StoreTopCategoryItemModels[i].CategoryId)
                      { %>
                    <b><%=Model.StoreTopCategoryItemModels[i].CategoryName%></b>
                    <%}else{ %>
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
                               <i class="fa fa-angle-double-right"></i>
                                   <a href="<%:item.CategoryUrl %>">
                                       <%: item.CategoryName%>  
                                  </a>
                            </li> 
                      <%} %>

                     <%if (item.SubStoreCategoryItemModes.Count > 0)
                       { %>
                           <li class="list-group-item" role="menuitem" ><%:MvcHtmlString.Create(leftCategoryPrefix)%>
                               <i class="fa fa-angle-right"></i>
                                   <a href="<%:item.CategoryUrl %>" class="text-bold">
                                       <%: item.CategoryName%> 
                                  </a>
                           </li>   
                            <%  leftCategoryPrefix += "&nbsp;&nbsp;&nbsp;";  
                              foreach (var subItem in item.SubStoreCategoryItemModes)
                              {%>
                                 <li class="list-group-item" role="menuitem" ><%:MvcHtmlString.Create(leftCategoryPrefix)%><i class="fa fa-angle-double-right"></i>
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
    </nav>
 </div>

