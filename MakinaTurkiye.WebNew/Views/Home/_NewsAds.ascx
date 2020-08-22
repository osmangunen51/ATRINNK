﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeAdModel>>" %>
<div class="row popular-ilanlar">
    <div class="col-xs-12">
        <h2 style="color: #4F4F4F;
              font-family: Arial;
              font-size: 24px;
              font-weight: 700;
              line-height: 27px;">
            <span>En Yeni İlanlar</span>
        </h2>
    </div>
    <div class="col-xs-12">
        <div class="row">
            <div class="col-md-2 col-sm-12 hidden-xs" style="margin: 0px;
    padding-top: 10px;
    padding-right: 0px;
    padding-bottom: 0px;
    padding-left: 10px;">
                <center><img height="100%" class="img-responsive " src="../../Content/V2/images/newadsbanner.png" /></center>
             </div>
            <div class="col-md-10 col-sm-12 category-popular-product" style="padding:10px;padding-right: 30px;">
                 <div class="overflow-carousel" id="PopulerProductContainer" style="padding-left:25px;padding-right:25px;">
                    <div class="owl-carousel" id="newtemscarousel">
                        <%foreach (var item in Model){%>
                        <div class="item col-sm-12">
                                    <div class="product-card-02" style="margin: 10px; min-height: 230px;">
                                            <h3 class="product-content" style="min-width: 180px;word-wrap:break-word;width:200px;margin-bottom: 20px;">
                                                <a style="height:24px"  href="<%:item.ProductUrl %>"><%:item.ProductName %> </a>
                                            </h3>
                                            <div class="product-image">
                                                <a href="<%:item.ProductUrl %>">
                                                    <img width="160" height="120" alt="<%=item.ProductName %>" src="<%=item.PicturePath%>" title="<%=item.ProductName %>">
                                                </a>
                                            </div>
                                                    <div class="product-price">
                                                <%if (!string.IsNullOrEmpty(item.ProductPrice))
                                                    {%>
                                                <i class="<%=item.CurrencyCssName %>"></i>
                                                <%if (item.ProductPrice == "Fiyat Sorunuz")
                                                    {%>
                                                <span class="interview"></span>
                                                <% }
                                                    else
                                                    {%>
                                                <%:item.ProductPrice%>
                                                <% } %>
                                                <%}%>
                                            </div>
                                        </div>
                                    </div>
                        <%}%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        var $homebannerBig = $('#newtemscarousel');
        var syncedSecondary = true;
        $homebannerBig.owlCarousel({
            autoWidth: true,
            autoHeight: true,
            items: 4,
            loop: true,
            autoplay: 1,
            smartSpeed: 1000,
            autoplayTimeout: 4000,
            autoplayHoverPause: true,
            //animateOut: 'fadeOut',
            //animateIn: 'fadeIn',
            lazyLoad: true,
            margin: 0,
            dots: true,
            touchDrag: true,
            responsiveRefreshRate: 200,
            mouseDrag: false,
            navText: ["", ""],
            //slideTransition: 'ease',
            responsive: {
                0: {
                    nav: false,
                    touchDrag: true,
                    mouseDrag: true,
                },

                992: {
                    nav: true,
                    touchDrag: false,
                    mouseDrag: false,
                }
            },
            navText: [
            '<a style="margin-right:5px;" class="left overflow-prev" data-slide="prev" href="#newtemscarousel"><div><i class="fa fa-angle-left fa-3x"></i></div></a>',
            '<a class="left overflow-next" data-slide="next" href="#newtemscarousel"><div><i class="fa fa-angle-right fa-3x"></i></div></a>'
            ]
        });

    });
</script>

