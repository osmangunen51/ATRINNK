<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryStoreModel>" %>
<% string gorunumt = Request.QueryString["Gorunum"]; %>

<%if (gorunumt != "3")
  {  %>
<div class="row clearfix">

    <div class="col-xs-12">
        <h2 class="section-title section-title--left">
            <span>
                <a href="<%:Model.StoreCategoryUrl %>" title="<%:Model.SelectedCategoryName %> Firmaları">

                    <% if (Model.SelectedCategoryType >= (byte)CategoryType.Brand)
                       { %>
                    <% if (Model.SelectedCategoryType == (byte)CategoryType.Brand)
                       { %>
                    <%:Model.SelectedCategoryName%>
                    <%--<%:Model.TopCategoryItems.FirstOrDefault(c=> c.CategoryId == Model.ActiveCategory.CategoryParentId.Value).CategoryName %>--%>
                    <% }
                       else
                       { %>
                    <%-- <%:Model.TopCategoryItems.FirstOrDefault(c=> c.CategoryId == Model.ActiveCategory.CategoryParentId.Value).CategoryName %>--%>
                    <%:Model.SelectedCategoryName%>
                    <% } %>
                    <% }
                       else
                       { %>
                    <%if (Model.SelectedCategoryType == (byte)CategoryType.Brand)
                      {  %>
                    <%:Model.SelectedCategoryName%>
                    <%} %>
                    <%:Model.SelectedCategoryName%>
                    <% } %>Firmaları 
                </a>
            </span>

        </h2>
    </div>


    <div class="col-xs-12">
        <div class="category-firms">
            <div class="sector-firms-slider owl-carousel clearfix js-sector-firm-slider">

                <%
                       var asastores = Model.StoreItemModes.ToList();
                       if (asastores.Count > 0)
                       {
                           foreach (var item in asastores)
                           {
                               string logoPath = "";
                               if (item != null)
                               {
                                   logoPath = item.PictureLogoPath;
                %>

                <div class="sector-firms-slider__item item">
                    <div class="sector-firms-slider__img">
                        <a href="<%:item.StoreUrl %>">
                            <img src="<%=item.PictureLogoPath %>" alt="<%=item.StoreName %>" />
                        </a>
                    </div>

                    <%--                    <span style="display: block; margin-bottom:15px;"><%:Html.Truncatet(Model.SelectedCategoryName, 38) %></span>--%>

                    <%--<div class="sector-firms-slider__name">
            <a href="<%=item.StoreUrl %>" target="_blank">
                <span><%:Html.Truncate(item.StoreName, 24)%></span>
            </a>
        </div>
                    --%>

                    <a href="<%=item.StoreUrl %>" target="_blank">
                        <p style="color: #333; display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden; min-height: 32px;"><%=item.StoreName%></p>
                    </a>
                    <div class="sector-firms-slider__category">
                        <a href="<%:item.StoreProductCategoryUrl %>" class="btn-xs btn btn-primary">
                            <%--<span><%:Html.Truncatet(Model.SelectedCategoryName, 38) %></span>--%>
                            <span><%:Html.Truncatet(Model.SelectedCategoryName, 38) %></span>
                        </a>
                    </div>
                </div>


                <%
                           }
                       }
                   } %>
            </div>
        </div>
    </div>
</div>
<% } %>