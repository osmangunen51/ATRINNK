<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>
<%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities(); %>

<%--        <% 
                  
                       if (Model.ParentCategoryItems.First() == i)
                       { %>
        <%= Html.RenderHtmlPartial("_CategorySectorBannerItems", Model.BannerModels)%>
        <%}%>--%>


<div class="col-xs-12">
<%--    <div class="sector-category-nav-select-container hidden-lg">
              <select class="sector-category-nav-select form-control">
            <% int selectIndex = 1; %>
            <% foreach (var i in Model.ParentCategoryItems.ToList())
               {
                   var parname = (from c in entities.Categories
                                  where c.CategoryParentId == i.CategoryId
                                  select c).FirstOrDefault();
                   if (i.ProductCount > 0)
                   {
                       if (i.ProductCount != 0)
                       {
            %>


            <option value="anchor<%=selectIndex %>"><%=i.CategoryName%></option>
            <%}%>
            <%selectIndex = selectIndex + 1; %>
            <%}%>

            <%}%>
        </select>
    </div>--%>
    <div class="sector-category-nav hidden-xs hidden-sm hidden-md">

        <% int buttonIndex = 1; %>
        <% foreach (var i in Model.ParentCategoryItems.ToList())
           {
               var parname = (from c in entities.Categories
                              where c.CategoryParentId == i.CategoryId
                              select c).FirstOrDefault();
               if (i.ProductCount > 0)
               {
                   if (i.ProductCount != 0)
                   {
        %>

        <div class="sector-category-nav__item <%--anchor<%=buttonIndex %>--%> <%=Helpers.ToUrl(i.CategoryName)%>" data-role="anchor-<%=Helpers.ToUrl(i.CategoryName)%>"><a href="javascript:;"><%=i.CategoryName%></a></div>

     <%}%>
           <%buttonIndex = buttonIndex + 1; %>
        <%}%>

        <%}%>
    </div>

        <div class="sector-category-nav-scroll-fix"></div>


</div>

<% %>