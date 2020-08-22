<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTHomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="https://www.makinaturkiye.com" />
    <script type="text/javascript">

        //function GetProductRecomandation() {
        //    $.ajax({
        //        type: 'get',
        //        url: '/Home/GetProductRecomandation',
        //        data: {},
        //        success: function (data) {
        //            $('.productMayLike').html(data);
        //            $(".advertiseHome").show();
        //            var owlTemplate = $('.owlTemplate');
        //            $.each(owlTemplate, function () {
        //                var item = $(this).find(".bottom-slider");
        //                if (item != undefined) {
        //                    item.owlCarousel({
        //                        margin: 0,
        //                        loop: true,
        //                        nav: false,
        //                        autoplay: true,
        //                        autoplayTimeout: 5000,
        //                        autoplayHoverPause: true,
        //                        autoWidth: true,
        //                        responsive: {
        //                            0: {
        //                                items: 1,
        //                                slideBy: 1
        //                            },
        //                            768: {
        //                                items: 2,
        //                                slideBy: 2
        //                            },
        //                            992: {
        //                                items: 3,
        //                                slideBy: 3

        //                            },
        //                            1200: {
        //                                items: 5,
        //                                slideBy: 5
        //                            }
        //                        }
        //                    })
        //                    var nextItem = $(this).find(".overflow-next");
        //                    var prevItem = $(this).find(".overflow-prev");
        //                    $(nextItem).click(function () {
        //                        item.trigger('next.owl.carousel');

        //                    });
        //                    $(prevItem).click(function () {
        //                        item.trigger('prev.owl.carousel', [300]);

        //                    });


        //                }
        //            });
        //            function setOwlStageHeight(event) {
        //                var maxHeight = 0;
        //                $('.owl-item.active').each(function () { // LOOP THROUGH ACTIVE ITEMS
        //                    var thisHeight = parseInt($(this).height());
        //                    maxHeight = (maxHeight >= thisHeight ? maxHeight : thisHeight);
        //                });
        //                $('.owl-carousel').css('height', maxHeight);
        //                $('.owl-stage-outer').css('height', maxHeight); // CORRECT DRAG-AREA SO BUTTONS ARE CLICKABLE
        //            }


        //        },
        //        error: function (x, y, z) {
        //            //alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
        //            console.log("hata");
        //        }
        //    });
        //}
        ////function GetProductSelected(skip, counter) {

        //    $.ajax({

        //        url: '/Home/ProductSeletected',
        //        type: 'GET',
        //        data: {
        //            'skip': skip
        //        },
        //        success: function (data) {

        //            if (data.IsSuccess) {
        //                var page = Number($("#Page").val()) + 1;

        //                $("#Page").val(page);
        //                $("#category-selected-home").append(data.Result);
        //                $("#imgLoading").hide();
        //                var owlOwerflowedp = $('.overflowdouble-p');
        //                if (owlOwerflowedp != undefined) {
        //                    owlOwerflowedp.owlCarousel({
        //                        margin: 0,
        //                        loop: true,
        //                        nav: false,
        //                        autoplay: true,

        //                        autoplayTimeout: 4000,
        //                        autoplayHoverPause: true,
        //                        responsive: {
        //                            0: {
        //                                items: 2
        //                            },
        //                            600: {
        //                                items: 4
        //                            },
        //                            1000: {
        //                                items: 6
        //                            }
        //                        }
        //                    });
        //                    $('.overflow-carousel-selected:not(.owlTemplate) .overflow-next1').click(function () {
        //                        owlOwerflowedp.trigger('next.owl.carousel');
        //                    });
        //                    $('.overflow-carousel-selected:not(.owlTemplate) .overflow-prev1').click(function () {
        //                        owlOwerflowedp.trigger('prev.owl.carousel', [300]);
        //                    });

        //                }
        //                counter++;

        //                $("#RequestScrool").val("1");
        //            }
        //            else {
        //                $("#RequestScrool").val("0");
        //                $("#imgLoading").hide();
        //            }


        //        },
        //        error: function (request, error) {
        //            console.log(request);
        //        }
        //    });
        //}
        document.addEventListener("DOMContentLoaded", function () {
            var lazyloadImages;

            if ("IntersectionObserver" in window) {
                lazyloadImages = document.querySelectorAll(".img-lazy");
                var imageObserver = new IntersectionObserver(function (entries, observer) {
                    entries.forEach(function (entry) {
                        if (entry.isIntersecting) {
                            var image = entry.target;
                            image.src = image.dataset.src;

                            imageObserver.unobserve(image);
                        }
                    });
                });

                lazyloadImages.forEach(function (image) {
                    imageObserver.observe(image);
                });
            } else {
                var lazyloadThrottleTimeout;
                lazyloadImages = document.querySelectorAll(".img-lazy");

                function lazyload() {
                    if (lazyloadThrottleTimeout) {
                        clearTimeout(lazyloadThrottleTimeout);
                    }

                    lazyloadThrottleTimeout = setTimeout(function () {
                        var scrollTop = window.pageYOffset;
                        lazyloadImages.forEach(function (img) {
                            if (img.offsetTop < (window.innerHeight + scrollTop)) {
                                img.src = img.dataset.src;
                                //img.classList.remove('img-thumbnail');
                            }
                        });
                        if (lazyloadImages.length == 0) {
                            document.removeEventListener("scroll", lazyload);
                            window.removeEventListener("resize", lazyload);
                            window.removeEventListener("orientationChange", lazyload);
                        }
                    }, 20);
                }

                document.addEventListener("scroll", lazyload);
                window.addEventListener("resize", lazyload);
                window.addEventListener("orientationChange", lazyload);
            }
        })
        var c = 1;
        var sectorLoaded = 0;
        function GetProductsShowCase() {
            $.ajax({

                url: '<%:AppSettings.SiteUrlWithoutLastSlash%>/ajax/GetProductShowCase',
                type: 'GET',
                data: {
                },

                success: function (data) {
                    $("#home-showcase").html(data);

                    //$("#category-selected-home").append($("#advertiseHomeContent").html());
                    //GetProductRecomandation();


                },
                error: function (request, error) {

                    console.log("Request: " + JSON.stringify(request));
                }
            });

        }

        function SectorLoading() {
            $.ajax({

                url: '<%:AppSettings.SiteUrlWithoutLastSlash%>/ajax/GetHomeSector',
                type: 'GET',
                data: {
                },
                success: function (data) {
                    $("#home-sector").html(data);
                    $("#RequestScroolSector").val("0");
                    GetProductsShowCase();

                },
                error: function (request, error) {
                    console.log("Request: " + JSON.stringify(request));
                }
            });
        }
        SectorLoading();
       <%-- $(window).scroll(function () {
            var requestScroll = $("#RequestScrool").val();
            var requestScroolSector = $("#RequestScroolSector").val();
            var sectorHeight = $(".new-header").height() + $(".main-navigation").height() - 200;
            var b = $(".new-header").height() + $(".main-navigation").height() + $(".home-banner-area ").height() + $("#home-sector").height() - 200;
            if ($(window).scrollTop() > sectorHeight) {
                if (requestScroolSector == 1) {
                    $.ajax({

                        url: '<%:AppSettings.SiteUrlWithoutLastSlash%>/ajax/GetHomeSector',
                        type: 'GET',
                        data: {
                        },
                        success: function (data) {
                            $("#home-sector").html(data);
                            $("#RequestScroolSector").val("0");
                            GetProductsShowCase();

                        },
                        error: function (request, error) {
                            console.log("Request: " + JSON.stringify(request));
                        }
                    });

                }
            }
            //if ($(window).scrollTop() > b ) {
            //    if (requestScroll == 1 && c < 10 && sectorLoaded==0) {
            //        if (c < 8) {
            //            $("#imgLoading").show();
            //            $("#RequestScrool").val("0");
            //            GetProductSelected($("#Page").val(), c);
            //            if (c == 4) {
            //                $("#category-selected-home").append($("#advertiseHomeContent").html());

            //            }
            //        }
            //        else {
            //            GetProductRecomandation();
            //        }
            //        c++;
            //    }
            //}

        });--%>

    </script>
    <%--<link rel="canonical" href="https://www.makinaturkiye.com" />--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HomePageSlider" runat="server">
    <%:Html.Hidden("RequestScrool",1) %>
    <%:Html.Hidden("RequestScroolSector",1) %>
    <%:Html.Hidden("Page",2) %>
    <%if (!Request.Browser.IsMobileDevice)
        {%>
    <div class="home-banner-area hidden-xs">
        <div class="row">
            <%--       <%=Html.Partial("_HomeLeftCategories",Model.HomeLeftCategoriesModel) %>--%>
            <div class="col-xs-12 col-lg-12">
                <%= Html.Partial("_SliderBanner", Model.SliderBannerMoldes)%>
            </div>
        </div>
    </div>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12" style="margin-top:10px;">
            <div>
                 <%--<h1 style="color: #fff !important;margin: 0px;font-family: Arial; line-height: 24px !important;font-size:18px">Dünya'nın Makinası Bir Arada</h1>--%>
                <h1 style="color: #0350a5;
              font-family: Arial;
              font-size: 24px;
              font-weight: 700;
              line-height: 27px;">Dünya'nın Makinası Bir Arada</h1>
            </div>
            <div class="home-showcase__body" style="margin:0px;padding:0px;">
                    <div id="home-sector">
                    </div>
            </div>
        </div>
    </div>
    <%=Html.Partial("_NewsAds",Model.NewsAdModels)%>
    <%--    <%if (!Request.Browser.IsMobileDevice) {%>
            <%=Html.RenderHtmlPartial("_HomeSector",Model.HomeSectorItems) %>
    <% } %>--%>

    <%--   <%= Html.Partial("_ProductRelatedCategories", Model.HomeProductsRelatedCategoryModel)  %>--%>
    <%--        <%= Html.Partial("_PopularAds", Model.PopularAdModels) %>--%>

    <div  id="home-showcase" style="background-color:none !important;;margin:0px;padding:0px;border-radius:2px;">

    </div>
   <%-- <div class="advertiseHome" style="display: none;">
        <div class="container">
            <div class="row">
                <div class="col-md-6 col-sm-6 col-md-offset-1 aos-init aos-animate" data-aos="fade-right" data-aos-offset="200" data-aos-delay="100" data-aos-duration="1000">
                    <span>İlan Verin</span>
                    <p>İlan vermek ve diğer tüm avantajlardan yararlanmak için üye olun!</p>
                </div>
                <div class="col-md-4 col-sm-6 clearfix aos-init aos-animate" data-aos="fade-up">
                    <div class="clearfix buttons">
                        <a href="<%:AppSettings.SiteUrl %>uyelik/hizliuyelik/uyeliktipi-0">ÜYE OL</a>
                        <a href="<%:AppSettings.SiteUrl %>uyelik/kullanicigirisi">GİRİŞ YAP</a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="productMayLike">
        <%--            <%=Html.Partial("_ProductMayLike",Model.MTMayLikeProductModel) %>--%>
    </div>

    <%=Html.Partial("_ShowCaseStores",Model.StoreModels) %>
    <div class="row">
        <div class="col-md-12">
               <%--<%=Html.RenderHtmlPartial("_HomeNews",Model.StoreNewItems) %>--%>
        </div>
       <%-- <div class="col-md-6">
               <%=Html.RenderHtmlPartial("_HomeSuccessStories",Model.SuccessStories) %>
        </div>--%>

    </div>

    <div class="row">
        <div class="col-xs-12">
            <div class="home-seo-content">
                <h2>
                    <%= Model.ConstantTitle%>
                </h2>
                <%= Model.ConstantProperty%>
            </div>
        </div>
    </div>


    <%--<div class="row">




        <div class="col-xs-12 text-dark text-center">
            <h1 style="font-size: 12px; font-weight: bold; color: #2d6ab4">
                <%= SeoModel.GetConstantsTitle(235)%>
            </h1>
            <%= SeoModel.GetConstantsProperty(235)%>
        </div>

    </div>--%>



    <%--    <div class="row">
        <div class="col-sm-5 col-md-4 col-lg-3 zindex-md">
            <nav>
                 <%:Html.RenderHtmlPartial("_MainMenu",Model.CategoryModels) %>
            </nav>
            <%= Html.RenderHtmlPartial("_Videos", Model.PopularVideoModels)%>
        </div>




        <div class="home-content-right col-sm-7 col-md-8 col-lg-9">
            <div class="row">

            </div>
            <div class="row" style="margin-bottom: -1%;">
                <%= Html.Partial("_PopularAds", Model.PopularAdModels) %>
            </div>
            <div class="row">
                <%=Html.Partial("_ShowCaseStores",Model.StoreModels) %>
            </div>
            <div class="col-xs-12 text-dark text-center">
                <h1 style="font-size: 12px; font-weight: bold; color: #2d6ab4">
                    <%= SeoModel.GetConstantsTitle(235)%>
                </h1>
                    <%= SeoModel.GetConstantsProperty(235)%>
            </div>
        </div>
    </div>--%>
    <%--    <%= Html.RenderHtmlPartial("CallYou",Model.companyMembershipDemand) %>--%>


<%--    <%=Html.RenderHtmlPartial("_Bulletin") %>--%>
</asp:Content>
