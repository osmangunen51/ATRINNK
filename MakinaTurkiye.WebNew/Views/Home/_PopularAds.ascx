<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeAdModel>>" %>
<div class="row popular-ilanlar">
    <div class="col-xs-12">
        <h2 class="section-title">
            <span>Popüler İlanlar</span>
        </h2>
    </div>
    <div class="col-xs-12">
        <div class="overflow-carousel" id="PopulerProductContainer">
            <div class="owl-carousel overflowdouble" id="popularitemscarousel">
                <% 
                    int StepCounter = 0;
                    foreach (var populerAd in Model)
                    {
                        StepCounter += 1;
                        if (StepCounter == 1)
                        {
                %>
                <div class="item double-item">
                    <%
                        }

                    %>

                    <div class="product-card">
                        <a href="<%=populerAd.ProductUrl%>" class="product-image">
                            <img alt="<%:populerAd.ProductName %>" src="/UserFiles/image-loading.png" class="img-lazy-l" data-src="<%:populerAd.PicturePath.Replace("160x120","200x150")  %>" title="<%:populerAd.ProductName %>" />
                            <span class="product-details">
                                <%= populerAd.ProductName %>
                            </span>
                        </a>
                        <%--                  <a href="<%=populerAd.SimilarUrl %>"  title="<%= populerAd.CategoryName %>" class="product-category"><%= populerAd.TruncatedCategoryName %></a> --%>
                    </div>
                    <%
                        if (StepCounter == 2)
                        {
                    %>
                </div>
                <%
                            StepCounter = 0;
                        }
                    }
                    if (StepCounter == 1)
                    { %>
            </div>
            <% }%>
        </div>
        <a class="left overflow-prev" data-slide="prev" href="#popularitemscarousel">
            <div><i class="fa fa-angle-left fa-3x"></i></div>
        </a>
        <a class="left overflow-next" data-slide="next" href="#popularitemscarousel">
            <div><i class="fa fa-angle-right fa-3x"></i></div>
        </a>
        <%--      <span class="overflow-prev"><div><i class="fa fa-angle-left fa-3x"></i></div></span>
          <span class="overflow-next"><div><i class="fa fa-angle-right fa-3x"></i></div></span>--%>
    </div>
</div>


<%--    <%  foreach (var populerAd in Model) {%>
    <div class="col-xs-6 col-sm-4 col-md-3 col-lg-2 mb20">
        <div class="popular-ilanlar__item">
            <a href="<%=populerAd.SimilarUrl %>" class="popular-ilanlar__category-link" title="<%= populerAd.CategoryName %>"><%= populerAd.TruncatedCategoryName %></a>
            <a href='<%=populerAd.ProductUrl %>' class='popular-ilanlar__item-link'>
               <!-- <span><%= populerAd.TruncatedProductName %></span>-->

                <img alt="<%:populerAd.ProductName %>" class="popular-ilanlar__img-thumbnail" height="135" src="<%:populerAd.PicturePath %>" title="<%:populerAd.ProductName %>" width="180">
                </a>
        </div>
    </div>
    <% } %>--%>
</div>



 