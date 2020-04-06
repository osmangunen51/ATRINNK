<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTHomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="https://www.makinaturkiye.com" />
    <script type="text/javascript">
        $(document).ready(function () {

            GetProductRecomandation();

            GetProductSelected(0, 1);
        });
        function GetProductRecomandation() {
            $.ajax({
                type: 'get',
                url: '/Home/GetProductRecomandation',
                data: {},
                success: function (data) {
                    $('.productMayLike').html(data);

                    var owlTemplate = $('.owlTemplate');
                    $.each(owlTemplate, function () {
                        var item = $(this).find(".bottom-slider");
                        if (item != undefined) {
                            item.owlCarousel({
                                margin: 0,
                                loop: true,
                                nav: false,
                                autoplay: true,
                                autoplayTimeout: 5000,
                                autoplayHoverPause: true,
                                autoWidth: true,
                                responsive: {
                                    0: {
                                        items: 1,
                                        slideBy: 1
                                    },
                                    768: {
                                        items: 2,
                                        slideBy: 2
                                    },
                                    992: {
                                        items: 3,
                                        slideBy: 3

                                    },
                                    1200: {
                                        items: 5,
                                        slideBy: 5
                                    }
                                }
                            })
                            var nextItem = $(this).find(".overflow-next");
                            var prevItem = $(this).find(".overflow-prev");
                            $(nextItem).click(function () {
                                item.trigger('next.owl.carousel');

                            });
                            $(prevItem).click(function () {
                                item.trigger('prev.owl.carousel', [300]);

                            });
               

                        }
                    });
                    function setOwlStageHeight(event) {
                        var maxHeight = 0;
                        $('.owl-item.active').each(function () { // LOOP THROUGH ACTIVE ITEMS
                            var thisHeight = parseInt($(this).height());
                            maxHeight = (maxHeight >= thisHeight ? maxHeight : thisHeight);
                        });
                        $('.owl-carousel').css('height', maxHeight);
                        $('.owl-stage-outer').css('height', maxHeight); // CORRECT DRAG-AREA SO BUTTONS ARE CLICKABLE
                    }


                },
                error: function (x, y, z) {
                    //alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
                }
            });
        }


        function GetProductSelected(skip, counter) {
            if (counter == 9) {
                return 0;
            }

            $.ajax({

                url: '/Home/ProductSeletected',
                type: 'GET',
                data: {
                    'skip': skip
                },
                success: function (data) {
                    $("#category-selected-home").append(data);
                    $("#imgLoading").hide();
                    var owlOwerflowedp = $('.overflowdouble-p');
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
                        $('.overflow-carousel-selected:not(.owlTemplate) .overflow-next1').click(function () {
                            owlOwerflowedp.trigger('next.owl.carousel');
                        });
                        $('.overflow-carousel-selected:not(.owlTemplate) .overflow-prev1').click(function () {
                            owlOwerflowedp.trigger('prev.owl.carousel', [300]);
                        });

                    }
                    counter++;
                     GetProductSelected(skip + 1, counter);

                },
                error: function (request, error) {
                    console.log(request);
                }
            });
        }


    </script>
    <link rel="canonical" href="https://www.makinaturkiye.com" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HomePageSlider" runat="server">
    <div class="home-banner-area">
        <div class="row">
            <%--       <%=Html.Partial("_HomeLeftCategories",Model.HomeLeftCategoriesModel) %>--%>
            <div class="col-xs-12 col-lg-12">
                <%= Html.Partial("_SliderBanner", Model.SliderBannerMoldes)%>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.RenderHtmlPartial("_SelectedProductCategory", Model.MTAllSelectedProduct) %>
    <%--   <%= Html.Partial("_ProductRelatedCategories", Model.HomeProductsRelatedCategoryModel)  %>--%>


    <%--        <%= Html.Partial("_PopularAds", Model.PopularAdModels) %>--%>
    <div id="category-selected-home" style="position: relative;">
        <img src="../../Content/V2/images/loading.gif" style="text-align: center; width: 32px; position: absolute; left: 50%;" id="imgLoading" />
    </div>
    <div class="productMayLike">
        <%--            <%=Html.Partial("_ProductMayLike",Model.MTMayLikeProductModel) %>--%>
    </div>

    <%--        <%=Html.Partial("_PopularAds",Model.PopularAdModels) %>--%>

    <%=Html.Partial("_ShowCaseStores",Model.StoreModels) %>


    <%--<div class="row">
        <div class="col-md-12">
               <%=Html.RenderHtmlPartial("_HomeNews",Model.StoreNewItems) %>
        </div>
        <div class="col-md-6">
               <%=Html.RenderHtmlPartial("_HomeSuccessStories",Model.SuccessStories) %>
        </div>
  
    </div>--%>

    <div class="row">
        <div class="col-xs-12">
            <div class="home-seo-content">
                <h1>
                    <%= Model.ConstantTitle%> 
                </h1>
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
</asp:Content>
