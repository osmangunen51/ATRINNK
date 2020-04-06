﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductCategoryModel>" %>
<%int activeCategoryId = Request.QueryString["CategoryId"].ToInt32(); %>
<div class="col-sm-5 col-md-4 col-lg-3">
<nav>
<ul class="hidden-xs list-group list-group-mt"  role="menubar">
    <li class="list-group-item first" role="menuitem">
        <h3 class="text-md m0">
            <span class="glyphicon glyphicon-globe text-info"></span> Aramanızı Daraltın
        </h3>
    </li>

   <%if (Request.QueryString["CategoryId"].ToInt32() !=0)
    { %>
   <%int LeftMargin = 4;  %>
  <% var activeCategory = Model.TopCategoryItemModels.SingleOrDefault(c => c.CategoryId == Request.QueryString["CategoryId"].ToInt32()); %>
  <%--Aktif Kategori Ürün Grubu Değilse--%>
  <% if (activeCategory.CategoryType != (byte)CategoryType.ProductGroup)
     { %>
     <%if (activeCategory.CategoryParentId != null)
       {  %>
  <% int lasTopCategoryId = activeCategory.CategoryParentId.Value;%>
  <% bool hasTopFinish = true; %>
  <%ICollection<MTCategoryItemModel> topCategories = new List<MTCategoryItemModel>();
    byte orderNo = 0; 
  %>
  <% while (hasTopFinish)
     { %>
  <% var topCategory = Model.TopCategoryItemModels.SingleOrDefault(c => c.CategoryId == lasTopCategoryId);%>
  <% if (topCategory != null)
     { %>
    <% if (topCategory.CategoryType == (byte)CategoryType.Sector)
     { %>
  <%
       orderNo += 1;
       topCategories.Add(topCategory);
       topCategory.OrderNo = orderNo;
       hasTopFinish = false; %>
  <% }%>
  <% else if (topCategory.CategoryType == (byte)CategoryType.ProductGroup)
     { %>
  <%   hasTopFinish = true;
       lasTopCategoryId = topCategory.CategoryParentId.Value;  %>
  <% }
     else
     { %>
  <%
       orderNo += 1;
       topCategory.OrderNo = orderNo;
       topCategories.Add(topCategory);
       lasTopCategoryId = topCategory.CategoryParentId.Value;  %>
  <% } %>
  <% } %>
  <% } %>
  <%topCategories = topCategories.OrderByDescending(c => c.OrderNo).ToList(); %>
  <% foreach (var item in topCategories)
     { %>
  <% if (item.CategoryType != (byte)CategoryType.ProductGroup)
     { %>
  <%LeftMargin += 8; %>
  <li style="padding-left:<%:LeftMargin %>px;" class="list-group-item"><i class="fa fa-angle-right"></i><a  href="<%:item.CategoryUrl %>">
        <%: item.CategoryName%> </a> </li>
  <% } %>
  <% } %>
  <% } %>
  <%} %>
  <%LeftMargin += 8; %>
  <%--Aktif Kategori--%>
   <li style="padding-left:<%:LeftMargin %>px;" class="list-group-item"><i class="fa fa-angle-right"></i><a  href="<%: activeCategory.CategoryUrl%>">
        <%: activeCategory.CategoryName%></a> </li>
  <%LeftMargin += 8; %>
    <%foreach (var item in Model.CategoryItemModels)
    {  %>
     <% if (item.CategoryType != (byte) CategoryType.Sector)
       { %>
  <li style="padding-left:<%: LeftMargin %>px;" class="list-group-item"><i class="fa fa-angle-double-right"></i><a href="<%: item.CategoryUrl %>">
        <%: item.CategoryName %> <span class="text-muted text-sm"> (<%: item.ProductCount %>)</span></a> </li>
        <% } %>
  <%} %>
  <%} else{ %>
  <%foreach (var item in Model.CategoryItemModels)
    {  %>
      <li class="list-group-item"><i class="fa fa-angle-right"></i><a href="<%:item.CategoryUrl %>">
        <%: item.CategoryName%> <span class="text-muted text-sm"> (<%:item.ProductCount%>)</span> </a> </li>

  <%} %>

  <%} %>

</ul>
</nav>
</div>