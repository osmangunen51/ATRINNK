<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTAllSelectedProductModel>>" %>

<%foreach (var m in Model.ToList())
    {%>
<%if (m.Products.Count > 0)
    {%>
<div class="home-selected-sector-products">
    <div class="home-seleted-product-c">
            <div class="">
        <ul class="product-category-tab-container nav nav-tabs" id="myTab<%:m.Index %>" role="tablist">
            <%int counterContainer = 0; %>
            <li class="nav-item active all-selected">
                <a class="nav-item nav-link " id="<%:m.Index %>-tab" data-toggle="tab" data-crousal="overflowdouble-p-<%:m.Index %>" href="#<%:m.Index %>" role="tab" aria-controls="<%:m.Index %>" aria-selected="true">Tümü</a>
            </li>

            <%foreach (var item in m.CategoryModel)
                {%>
            <li class="nav-item">
                <a class="nav-item nav-link" id="<%:item.CategoryId %>-tab" data-toggle="tab" data-crousal="overflowdouble-p-<%:item.CategoryId %>" href="#<%:item.CategoryId %>" role="tab" aria-controls="<%:item.CategoryId %>" aria-selected="true"><%:item.CategoryName %></a>
            </li>
            <%} %>
        </ul>
    </div>
    <div class="">
 
            <div class="tab-content" id="nav-tabContent<%:m.Index %>">

                <div class="tab-pane fade active in" id="<%:m.Index %>">
                    <div class="overflow-carousel-selected">
                        <div class="owl-carousel overflowdouble-p-<%:m.Index %>">
                            <% 
                                foreach (var product in m.Products.Where(x => x.CategoryName == ""))
                                {
                            %>
                            <div class="product-card-home-selected">
                                <a href="<%=product.ProductUrl%>" class="product-card-home-selected-image" title="<%:product.ProductName %>">
                                    <img alt="<%:product.ProductName %>"
                                        src="<%:product.PicturePath.Replace("160x120","200x150")  %>"
                                        title="<%:product.ProductName %>" />
                                    
                                <%if (product.HasVideo)
                                    {
                                %>
                                <div class="product-list-favorite-icon" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

                                <%
                                } %>
                                </a>


                                <div class="">
                                                   <div class="product-card-home-selected-title">
                                        <%:product.TruncatedProductName %>
                                    </div>

                                    <%if (!string.IsNullOrEmpty(product.ProductPrice))
                                        {%>
                                    <div class="product-list-price">
                                        <%:product.ProductPrice %>
                                        <%if (!string.IsNullOrEmpty(product.CurrencyCssName))
                                            {%>
                                        <i class="<%:product.CurrencyCssName %>"></i>
                                        <% } %>
                                    </div>
                                    <% } %>

                     
                                </div>

                            </div>

                            <%
                                }%>
                        </div>
                        <a class="left overflow-prev overflow-prev1-<%:m.Index %>" data-slide="prev">
                        </a>
                        <a class="left overflow-next overflow-next1-<%:m.Index %>" data-slide="next">
                        </a>
                    </div>

                </div>
                <script type="text/javascript">

                    $(document).ready(function () {


                        var owlOwerflowedp = $('.overflowdouble-p-<%:m.Index %>');
                        if (owlOwerflowedp != undefined) {
                            owlOwerflowedp.owlCarousel({
                                margin: 10,
                                loop: true,
                                nav: false,
                                autoplay: true,
                                autoplayTimeout: 4000,
                                autoplayHoverPause: true,
                                responsive: {
                                    0: {
                                        items: 2
                                    },
                                    600: {
                                        items: 4
                                    },
                                    1000: {
                                        items: 6
                                    }
                                }
                            });
                            $('.overflow-carousel-selected:not(.owlTemplate) .overflow-next1-<%:m.Index%>').click(function () {
                                owlOwerflowedp.trigger('next.owl.carousel');
                            });
                            $('.overflow-carousel-selected:not(.owlTemplate) .overflow-prev1-<%:m.Index%>').click(function () {
                                owlOwerflowedp.trigger('prev.owl.carousel', [300]);
                            });

                        }
                    });


                </script>

                <%foreach (var item in m.CategoryModel)
                    {
                        counterContainer++;
                %>
                <div class="tab-pane fade" id="<%:item.CategoryId %>" role="tabpanel<%:m.Index %>" aria-labelledby="<%:item.CategoryId %>-tab">

                    <div class="overflow-carousel-selected">
                        <div class="owl-carousel asd overflowdouble-p-<%:item.CategoryId %>">
                            <% 
                                foreach (var product in m.Products.Where(x => x.CategoryName == item.CategoryName).OrderBy(x => x.Index))
                                {
                            %>

                            <div class="product-card-home-selected">
                                <a href="<%=product.ProductUrl%>" class="product-card-home-selected-image" title="<%:product.ProductName %>">
                                    <img alt="<%:product.ProductName %>"
                                        data-src="<%:product.PicturePath.Replace("160x120","200x150")  %>"
                                        src="/UserFiles/image-loading.png"
                                        class="img-lazy"
                                        title="<%:product.ProductName %>" />
                                                    <%if (product.HasVideo)
                                    {
                                %>
                                <div class="product-list-favorite-icon" title="Videolu Ürün"><i class="fa fa-video-camera"></i></div>

                                <%
                                    } %>
                                </a>


                
                                <div class="">
                                    
                                    <div class="product-card-home-selected-title">
                                  <%:product.TruncatedProductName %>
                                    </div>
                                    <%if (!string.IsNullOrEmpty(product.ProductPrice))
                                        {%>
                                    <div class="product-list-price">
                                        <%:product.ProductPrice %>
                                        <%if (!string.IsNullOrEmpty(product.CurrencyCssName))
                                            {%>
                                        <i class="<%:product.CurrencyCssName %>"></i>
                                        <% } %>
                                    </div>
                                    <% } %>

                                </div>

                            </div>

                            <%
                                }%>
                        </div>
             
                                             <a class="left overflow-prev overflow-prev1-<%:item.CategoryId %>" data-slide="prev">
                        </a>
                        <a class="left overflow-next overflow-next1-<%:item.CategoryId %>" data-slide="next">
                        </a>
                    </div>
         

                </div>
                <script type="text/javascript">
                    $(document).ready(function () {

                        var owlOwerflowedp = $('.overflowdouble-p-<%:item.CategoryId %>');
                        if (owlOwerflowedp != undefined) {
                            owlOwerflowedp.owlCarousel({
                                margin: 10,
                                loop: true,
                                nav: false,
                                autoplay: true,
                                autoplayTimeout: 4000,
                                autoplayHoverPause: true,
                                responsive: {
                                    0: {
                                        items: 2
                                    },
                                    600: {
                                        items: 4
                                    },
                                    1000: {
                                        items: 6
                                    }
                                }
                            });
                            $('.overflow-carousel-selected:not(.owlTemplate) .overflow-next1-<%:item.CategoryId%>').click(function () {
                                owlOwerflowedp.trigger('next.owl.carousel');
                            });
                            $('.overflow-carousel-selected:not(.owlTemplate) .overflow-prev1-<%:item.CategoryId%>').click(function () {
                                owlOwerflowedp.trigger('prev.owl.carousel', [300]);
                            });

                        }
                    <%if (m.Index > 0)
                    {
                    %>
                        $('.product-category-tab-container .nav-item a').on('shown.bs.tab', function (e) {
                            //e.target // newly activated tab
                            //e.relatedTarget // previous active tab

                            var crousalClass = $(this).attr("data-crousal");
                            console.log(crousalClass);
                            $("." + crousalClass).trigger('refresh.owl.carousel');
                        });
                    <%
                    }%>

                    });


                </script>
                <% } %>
            </div>
    
    </div>
    </div>
</div>

<% } %>
<% } %>

