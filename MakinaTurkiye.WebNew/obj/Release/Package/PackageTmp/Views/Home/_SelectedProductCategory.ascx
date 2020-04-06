<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTAllSelectedProductModel>" %>


<%if (Model.Products.Count > 0)
    {%>
<div class="row home-selected-sector-products">

    <div class="col-xs-12">
        <ul class="product-category-tab-container nav nav-tabs" style="<%: Model.TabBackgroundCss%>" id="myTab<%:Model.Index %>" role="tablist">
            <%int counterContainer = 0; %>
            <li class="nav-item active all-selected">
                <a class="nav-item nav-link " id="<%:Model.Index %>-tab" data-toggle="tab" data-crousal="overflowdouble-p-<%:Model.Index %>" href="#<%:Model.Index %>" role="tab" aria-controls="<%:Model.Index %>" aria-selected="true">Tümü</a>
            </li>

            <%foreach (var item in Model.CategoryModel)
                {%>
            <li class="nav-item">
                <a class="nav-item nav-link" id="<%:item.CategoryId %>-tab" data-toggle="tab" data-crousal="overflowdouble-p-<%:item.CategoryId %>" href="#<%:item.CategoryId %>" role="tab" aria-controls="<%:item.CategoryId %>" aria-selected="true"><%:item.CategoryName %></a>
            </li>
            <%} %>
        </ul>
    </div>
    <div class="col-xs-12">
        <div style="border: 1px solid #cacaca; border-top: none; <%: Model.TabBackgroundCss%>">
            <div class="tab-content" id="nav-tabContent<%:Model.Index %>">

                <div class="tab-pane fade active in" id="<%:Model.Index %>">
                    <div class="overflow-carousel-selected" style="<%: Model.BackgrounCss%>">
                        <div class="owl-carousel overflowdouble-p-<%:Model.Index %>">
                            <% 
                                foreach (var product in Model.Products.Where(x => x.CategoryName == ""))
                                {
                            %>
                            <%=Html.RenderHtmlPartial("_ProductHomeItem", product) %>
                            <%
                                }%>
                        </div>
                        <a class="left overflow-prev overflow-prev1-<%:Model.Index %>" data-slide="prev">
                            <div><i class="fa fa-angle-left fa-3x"></i></div>
                        </a>
                        <a class="left overflow-next overflow-next1-<%:Model.Index %>" data-slide="next">
                            <div><i class="fa fa-angle-right fa-3x"></i></div>
                        </a>
                    </div>

                </div>
                <script type="text/javascript">

                    $(document).ready(function () {


                        var owlOwerflowedp = $('.overflowdouble-p-<%:Model.Index %>');
                    if (owlOwerflowedp != undefined) {
                        owlOwerflowedp.owlCarousel({
                            margin: 0,
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
                        $('.overflow-carousel-selected:not(.owlTemplate) .overflow-next1-<%:Model.Index%>').click(function () {
                            owlOwerflowedp.trigger('next.owl.carousel');
                        });
                        $('.overflow-carousel-selected:not(.owlTemplate) .overflow-prev1-<%:Model.Index%>').click(function () {
                                owlOwerflowedp.trigger('prev.owl.carousel', [300]);
                            });

                        }
                    });


                </script>

                <%foreach (var item in Model.CategoryModel)
                    {
                        counterContainer++;
                %>
                <div class="tab-pane fade" id="<%:item.CategoryId %>" role="tabpanel<%:Model.Index %>" aria-labelledby="<%:item.CategoryId %>-tab">

                    <div class="overflow-carousel-selected" style="<%: Model.BackgrounCss%>">
                        <div class="owl-carousel asd overflowdouble-p-<%:item.CategoryId %>">
                            <% 
                                foreach (var product in Model.Products.Where(x => x.CategoryName == item.CategoryName).OrderBy(x=>x.Index))
                                {
                            %>
                            <%=Html.RenderHtmlPartial("_ProductHomeItem", product) %>
                            <%
                                }%>
                        </div>
                        <a class="left overflow-prev overflow-prev1-<%:item.CategoryId %>" data-slide="prev">
                            <div><i class="fa fa-angle-left fa-3x"></i></div>
                        </a>
                        <a class="left overflow-next overflow-next1-<%:item.CategoryId %>" data-slide="next">
                            <div><i class="fa fa-angle-right fa-3x"></i></div>
                        </a>
                    </div>


                </div>
                <script type="text/javascript">
                    $(document).ready(function () {

                        var owlOwerflowedp = $('.overflowdouble-p-<%:item.CategoryId %>');
                    if (owlOwerflowedp != undefined) {
                        owlOwerflowedp.owlCarousel({
                            margin: 0,
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
                    <%if (Model.Index > 0)
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