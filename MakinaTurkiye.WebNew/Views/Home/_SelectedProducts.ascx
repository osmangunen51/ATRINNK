<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTHomeProductsRelatedCategoryModel>" %>
<div class="row popular-ilanlar">
    <div class="col-xs-12">
        <h2 class="section-title">
            <span>Seçilmiş Kategoriler</span>
        </h2>
    </div>
    <div class="col-xs-12">
        <div class="selected-products-container flex-middler">
            <div class="flex-row">
                <div class="flex-xs-12 flex-sm-12 flex-md-4 flex-lg-2">
                    <div class="tabs-container-wrapper">
                        <ul class="tabs-container nav flex-row" role="tablist">

                            <%
                                int ActCounter = 0;
                                string activeClass = "";
                                foreach (var item in Model.Categories)
                                {
                                    if (ActCounter == 0)
                                    {
                                        ActCounter = 1;
                                        activeClass = "active";
                                    }
                                    else
                                    {
                                        activeClass = "";
                                    } 
                            %>
                            <li class="<%=activeClass %> flex-xs-6 flex-sm-4 flex-md-12 flex-lg-12">
                                <a
                                    href="javascript:;"
                                    data-target="#rpc_<%=item.CategoryId %>"
                                    role="presentation"
                                    aria-controls="home"
                                    role="tab"
                                    data-toggle="tab">
                                    <p><%=item.CategoryName %></p>
                                    <span class="border"></span>
                                </a>
                                <%-- <div class="selected-products-tab-item"   data-target="#rpc_<%=item.CategoryId %>" role="presentation"  aria-controls="home" role="tab" data-toggle="tab">
                            <div class="selected-products-tab-item-img  hidden-xs hidden-sm" >
                                <img src="https://www.makinaturkiye.com<%=item.CategoryIcon %>" />
                            </div>
                            <div class="selected-products-tab-item-text">
                               <%=item.CategoryName %>
                            </div>
                        </div> --%>
                            </li>
                            <%  } %>


                            <li class="flex-xs-6 flex-sm-12 flex-md-12 flex-lg-12">
                                <a href="/urun-kategori-c-0">
                                    <p>Daha Fazla</p>
                                    <span class="border"></span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="flex-xs-12 flex-sm-12  flex-md-8 flex-lg-10">

                    <div class="carousel-inner">


                        <% 
                            int ActPanelCounter = 0;
                            foreach (var item in Model.Categories)
                            {

                                if (ActPanelCounter == 0)
                                {
                                    ActPanelCounter = 1;
                                    activeClass = "active in";
                                }
                                else
                                {
                                    activeClass = "";
                                }
                                ViewData["PVCategoryName"] = item.CategoryName;
                        %>
                        <div class="tab-content-container    tab-pane     <%=activeClass %>" id="rpc_<%=item.CategoryId %>" role="tabpanel">
                            <div class="tab-content-slider">
                                <div class="overflow-carousel-selected-categories">
                                    <%=Html.Partial("_SelectedProductItems",Model.Products.Where(m=> m.CategoryId == item.CategoryId).ToList()) %>
                                    <span class="overflow-prev">
                                        <div><i class="fa fa-angle-left fa-3x"></i></div>
                                    </span>
                                    <span class="overflow-next">
                                        <div><i class="fa fa-angle-right fa-3x"></i></div>
                                    </span>
                                </div>
                            </div>
                            <div class="tab-content-footer">
                                <ul class="anchor-list-inline">
                                    <% foreach (var sc in item.SubCategoryModels.ToList())
                                       {%>
                                    <li><a href="<%=sc.CategoryUrl %>"><%=sc.CategoryName %></a></li>
                                    <% } %>
                                </ul>
                            </div>
                        </div>
                        <% } %>
                    </div>


                </div>
            </div>

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



