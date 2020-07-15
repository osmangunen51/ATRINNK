﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<MTCategoryProductViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <% if (!string.IsNullOrEmpty(ViewBag.Canonical))
        { %>
    <%if (Model.CategoryModel.SelectedCategoryId == 0) { ViewBag.Canonical = "https://urun.makinaturkiye.com"; } %>
    <link rel="canonical" href="<%= ViewBag.Canonical%>" />
    <% }  %>
    <%if (!string.IsNullOrEmpty(Model.PrevPageUrl))
        {%>
    <link rel="prev" href="<%:Model.PrevPageUrl %>" />
    <%} %>
    <%if (!string.IsNullOrEmpty(Model.NextPageUrl))
        { %>
    <link rel="next" href="<%:Model.NextPageUrl%>" />
    <%} %>

    <% if (!this.Context.IsDebuggingEnabled)
        { %>
    <meta name="robots" content="INDEX,FOLLOW" />
    <% }%>

    <script type="text/javascript">
        $(document).ready(function () {
            //GetMostViewedProductAjax();
            //GetStoreWithAjax();
            $('#carouselExampleControls').carousel();
             let isMobile = window.matchMedia("only screen and (max-width: 760px)").matches;
            if (isMobile) {
                                var i = 0;
                var count = $(".breadcrumb-mt li").length;
                $(".fast-access-bar").css("margin-top", "-20px");
                $(".fast-access-bar").removeAttr("class");
                var i = 0;
                var count = $(".breadcrumb-mt li").length;
                $(".breadcrumb-mt li").each(function (index) {
                    if (index == 0) {
                              $(this).show();
                    }
                    else if (index < count - 2)
                    {
                        $(this).css("display", "none");
                    }
                 });
            }
        });
        //lazyloading
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
        //lazyloading finish

        function GetMostViewedProductAjax() {
            $.ajax({
                type: 'post',
                url: '/Category/MViewedProductGet',
                data: { SelectedCategoryId:'<%:Model.CategoryModel.SelectedCategoryId%>', SelectedCategoryName:'<%=Model.CategoryModel.SelectedCategoryContentTitle%>' },
                success: function (data) {
                    if (data == "") {
                        $("#PopulerUrunlerTitle").hide();
                    }
                    $('.MostViewProduct').html(data);
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
        function GetStoreWithAjax() {
            $.ajax({
                type: 'post',
                url: '/Category/GetStoresByCategoryId',
                data: { selectedCategoryId:<%:Model.CategoryId%>, selectedBrandId:<%:Model.BrandId%>, selectedCountryId:<%:Model.CountryId%>, selectedCityId:<%:Model.CityId%>, selectedLocalityId:<%:Model.LocalityId%>},
                success: function (data) {
                    //$('#spanLoading').hide();
                    //var id = ($('#hdnDisplayType').val() == 2 ? " #liste" : " #pencere")
                    $('.storesContent').html(data);
                    if ($('.js-sector-firm-slider').length > 0) {
                        $('.js-sector-firm-slider').owlCarousel({
                            loop: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
                            margin: 10,
                            nav: true,
                            autoplay: true,
                            items: 8,
                            autoplayHoverPause: true,
                            autoplaySpeed: 1000,
                            autoHeight: true,
                            navText: ["", ""],
                            center: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
                            responsive: {
                                0: {
                                    items: 2
                                },
                                768: {
                                    items: 4
                                },
                                991: {
                                    items: 6
                                },
                                1200: {
                                    items: 8
                                }
                            }
                        })
                    }
                    //$('html, body').animate({ scrollTop: 150 }, 500);
                },
                error: function (x, y, z) {
                    //alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
                }
            });
        }

    </script>
    <%=Html.RenderHtmlPartial("CategoryHeaderContent")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        if (Model.CategoryModel.SelectedCategoryId == 0)
        { %>
    <%--<%=Html.RenderHtmlPartial("LeftMenuSectorCategories")%>--%>
    <div class="row clearfix">
        <div class="col-xs-12">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <%= Model.CategoryModel.Navigation%>
                </div>
            </div>
            <h1 style="font-size: 22px; padding-bottom: 5px; margin-top: 0px;"><b>Tüm Kategoriler</b></h1>
            <div class="row">
                <%=Html.RenderHtmlPartial("_SectorCategoryListMenu")%>
            </div>
        </div>
    </div>
    <div class="row clearfix cg-container">
        <div class="col-sm-12 col-md-12 col-lg-9">
            <%=Html.RenderHtmlPartial("_SectorCategoryList")%>
        </div>
    </div>
    <%--     <div class="row">
        <div class="col-xs-12">
            <div class="diger-urunler">
                <div class="urun-bilgisi">
                    <div class="panel panel-default urun-alan">
                        <div class="panel-heading" style="padding-bottom: 5px;">
                            <div class="urunler-link">
                                <div class="row">
                                    <div class="col-md-8 col-xs-7">
                                        <span>Sizin İçin Seçtiklerimiz</span>
                                    </div>
                                    <div class="col-md-4 col-xs-5">
                                    </div>
                                </div>
                            </div>
                        </div>
                   <div class="panel-body" style="padding-top: 0px;">
                            <div class="overflow-carousel owlTemplate maxw stagereset" style="margin-top: -3px;">
                                <div class="owl-carousel bottom-slider">
                                    <% foreach (var item in Model.RandomProducts)
                                       {   %>
                                    <div class="item">
                                        <div class="product-card-02" style="margin: 10px;">
                                            <div class="product-image">
                                                <a href="<%:item.ProductUrl %>">
                                                    <%
                                                   if (!string.IsNullOrEmpty(item.SmallPicturePah))
                                                   {
                                                    %>
                                                    <img alt="<%=item.SmallPictureName %>" src="<%=item.SmallPicturePah %>" title="<%=item.SmallPictureName %>">
                                                    <%
                               }
                               else
                               { %>
                                                    <img src="https://dummyimage.com/150x100/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=Html.Truncate(item.ProductName, 80)%>" />
                                                    <% }
                                                    %>
                                                </a>
                                            </div>
                                            <div class="product-content" style="min-width: 180px;">
                                                <a href="<%:item.ProductUrl %>"><%:item.ProductName %> </a>
                                            </div>
                                            <div class="product-details clearfix">
                                                <div class="small-gray">
                                                    <span style="display: block"><%:item.BrandName %></span>
                                                    <span><%:item.ModelName %></span>
                                                </div>

                                                <div class="product-price">
                                                    <%if (!string.IsNullOrEmpty(item.Price))
                                                      {%>
                                                    <i class="<%=item.CurrencyCss %>"></i>
                                                    <%:item.Price%>
                                                    <%}
                                                      else
                                                      {%>
                                   Fiyat Sorunuz
                                 <% } %>
                                                </div>
                                            </div>
                                            <div class="product-action text-center" style="display: block">
                                                <a class="label label-danger btnClik btn-k" href="<%:item.ProductContactUrl %>"><b>Satıcıyla İletişim Kur </b></a>
                                            </div>
                                        </div>
                                    </div>
                                    <%} %>
                                </div>
                                <a class="left  overflow-prev">
                                    <div><i class="fa fa-angle-left fa-3x"></i></div>
                                </a>
                                <a class="left  overflow-next">
                                    <div><i class="fa fa-angle-right fa-3x"></i></div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <% }
        else
        { %>

    <div class="fast-access-bar">
        <div class="fast-access-bar__inner">
            <div class="row clearfix">
                <div class="col-xs-12 col-md-12">
                    <%= Model.CategoryModel.Navigation%>
                </div>
            </div>
        </div>
    </div>

    <div class="row clearfix">
        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2 leftSideBar " style="padding-right: 0;">
            <div class="theiaStickySidebar">

                <div class="filters">

                    <div class="pos-relative">

                        <div class="filters__header row visible-xs">
                            <div class="col-xs-12">
                                <a href="javascript:;" class="js-close-filters"><span class="icon-close"></span></a>Filterele
                            </div>
                        </div>

                        <div class="filters__inner">
                            <div class="pos-absolute">

                                <div class="pos-absolute__inner panel-group" id="filters" role="tablist">
                                    <div class="panel panel-mt panel-mtv2 hidden-xs" style="border-bottom: 1px solid #E0E0E0;">
                                        <div class="panel-heading left-menu-header">
                                        </div>
                                        <div class="panel-body collapse in CategoryLeftCategoryTop" id="menu-body2">
                                            <h1>
                                                  <%
                                                      string brandName = "";
                                                      if (Model.FilteringContext.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.New)
                                                      {%>
                                      Sıfır
                                 <%}
                                     else if (Model.FilteringContext.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.Used)
                                     {%>
                                      İkinci El
                                 <%}%>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Marka") &&

                                                              Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Marka").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%brandName = Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Marka").ItemModels.FirstOrDefault(k => k.Selected).FilterName; %>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Marka").ItemModels.FirstOrDefault(k => k.Selected).FilterName%>
                                                <%}
                                                    else
                                                    {%>
                                                <%if (!string.IsNullOrEmpty(Model.SpesificBrandName))
                                                    { %>
                                                <%=Model.SpesificBrandName%>
                                                <%}
                                                    } %>

                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Model") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Model").ItemModels.Any(k => k.Selected))
                                                    {%>

                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Model").ItemModels.FirstOrDefault(k=>k.Selected).FilterName%>
                                                <%} %>
                                                <%if (!string.IsNullOrEmpty(Model.SpesificCategoryNameForModelH1) && brandName != Model.SpesificCategoryNameForModelH1)
                                                    { %>

                                                <%:Model.SpesificCategoryNameForModelH1 %>

                                                <%} %>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Seri") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Seri").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Seri").ItemModels.FirstOrDefault(k=>k.Selected).FilterName%>
                                                <%} %>

                                                <%if (Model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Brand && Model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Series && Model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Model)
                                                    {%>

                                                <%:Model.CategoryModel.SelectedCategoryContentTitle%>
                                                <%}
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(Model.SpesificCategoryNameForModelH1))
                                                        {
                                                            var topCategory = Model.CategoryModel.TopCategoryItemModels.LastOrDefault(c => c.CategoryType != (byte)CategoryType.Brand && c.CategoryType != (byte)CategoryType.Model && c.CategoryType != (byte)CategoryType.Series);
                                                            if (!string.IsNullOrWhiteSpace(topCategory.CategoryContentTitle))
                                                            {
                                                %>
                                                <%:topCategory.CategoryContentTitle %>
                                                <%
                                                    }
                                                    else
                                                    {
                                                %>
                                                <%:topCategory.CategoryName %>
                                                <% }
                                                %>


                                                <%}
                                                    }%>

                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "İlçe") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "İlçe").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "İlçe").ItemModels.FirstOrDefault(k=>k.Selected).FilterName%>
                                                <%} %>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Şehir") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Şehir").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Şehir").ItemModels.FirstOrDefault(k => k.Selected).FilterName%>
                                                <%}
                                                    else
                                                    { %>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Ülke") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Ülke").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Ülke").ItemModels.FirstOrDefault(k => k.Selected).FilterName%>
                                                <%}
                                                    } %>
                                                <%if (!string.IsNullOrEmpty(Model.SameCategoryH1))
                                                    { %>
                                        (<%:Model.SameCategoryH1 %>)
                                <%} %>

                                            </h1>

                                        </div>
                                    </div>

                                    <%=Html.RenderHtmlPartial("LeftMenuCategories")%>


                                    <%if (Model.FilteringContext.DataFilterMoldes.Where(x => x.FilterName == "Marka").Count() > 0)
                                        { %>
                                    <div class="panel panel-mt panel-mtv2">
                                        <div class="panel-heading">
                                            <span class="icon-buffer"></span>&nbsp;<span class="title">Marka</span>

                                            <a href="javascript:;" role="button" data-toggle="collapse" data-parent="#filters" data-target="#marka-body">
                                                <span class="more-less icon-down-arrow"></span>
                                            </a>


                                            <%var clearFilterUrl = Model.FilteringContext.DataFilterMoldes.ToList().Where(x => x.FilterName == "Marka" && x.FilterName != "Seri" && x.FilterName != "Model" && x.ClearFilterUrl != "").FirstOrDefault(); %> <%if (clearFilterUrl != null)
                                                                                                                                                                                                                                                                { %> <a href="<%:clearFilterUrl.ClearFilterUrl %>" class="pull-right">Tümü</a> <%} %>
                                        </div>

                                        <div class="panel-body collapse" id="marka-body">
                                            <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                                                <%foreach (var filterItem in Model.FilteringContext.DataFilterMoldes.ToList().Where(x => x.FilterName == "Marka" || x.FilterName == "Seri" || x.FilterName == "Model"))
                                                    {%>
                                                <%if (filterItem.SelectedFilter)
                                                    { %>
                                                <div class="list-group-item selected">
                                                    <a href="<%:filterItem.SelectedFilterUrl %>"><i class="text-md fa fa-fw fa-check-square-o" aria-hidden="true"></i>
                                                        <%:filterItem.SelectedFilterItemName%>
                                                        <span class="text-muted text-sm">(<%:filterItem.SelectedFilterItemCount%>)</span>
                                                    </a>
                                                </div>
                                                <%}
                                                    else
                                                    {%>

                                                <%foreach (var item in filterItem.ItemModels)
                                                    {  %>
                                                <div class="list-group-item unselected">

                                                    <a href="<%:item.FilterUrl %>"><i class="text-md fa fa-fw fa-square-o"></i>
                                                        <%:item.FilterName %>
                                                        <span class="text-muted text-sm">(<%:item.Count %>)</span> </a>
                                                </div>
                                                <%} %>


                                                <%}%>

                                                <%} %>
                                            </div>
                                        </div>
                                    </div>

                                    <%} %>
                                    <div class="panel panel-mt panel-mtv2">
                                        <div class="panel-heading">
                                            <span class="icon-map-pin"></span>
                                            <span class="title">Adres</span>
                                            <a href="javascript:;" role="button" data-toggle="collapse" data-parent="#filters" data-target="#adres-body">
                                                <span class="more-less icon-down-arrow"></span>
                                            </a>

                                            <%var clearFilterUrl1 = Model.FilteringContext.DataFilterMoldes.ToList().Where(x => x.FilterName != "Marka" && x.FilterName != "Seri" && x.FilterName != "Model" && x.ClearFilterUrl != "").FirstOrDefault(); %> <%if (clearFilterUrl1 != null)
                                                                                                                                                                                                                                                                 { %> <a href="<%:clearFilterUrl1.ClearFilterUrl %>" class="pull-right">Tümü</a> <%} %>
                                        </div>

                                        <div class="panel-body collapse" id="adres-body">
                                            <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">

                                                <%foreach (var filterItem in Model.FilteringContext.DataFilterMoldes.ToList().Where(x => x.FilterName != "Marka" && x.FilterName != "Seri" && x.FilterName != "Model"))
                                                    {%>

                                                <%if (filterItem.SelectedFilter)
                                                    { %>
                                                <div class="list-group-item selected">
                                                    <a href="<%:filterItem.SelectedFilterUrl %>" rel="nofollow"><i class="text-md fa fa-fw fa-check-square-o" aria-hidden="true"></i>
                                                        <%:filterItem.SelectedFilterItemName%>
                                                        <span class="text-muted text-sm">(<%:filterItem.SelectedFilterItemCount%>)</span>
                                                    </a>
                                                </div>
                                                <%}
                                                    else
                                                    {%>

                                                <%foreach (var item in filterItem.ItemModels)
                                                    {  %>
                                                <div class="list-group-item unselected">
                                                    <a href="<%:item.FilterUrl %>" rel="nofollow"><i class="text-md fa fa-fw fa-square-o"></i>
                                                        <%:item.FilterName %>
                                                        <span class="text-muted text-sm">(<%:item.Count %>)</span> </a>
                                                </div>
                                                <%} %>


                                                <%}%>


                                                <%} %>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel panel-mt panel-mtv2">
                                        <div class="panel-heading">
                                            <span class="icon-search"></span>
                                            <span class="title">Kelime ile filtrele</span>

                                            <a href="javascript:;" role="button" data-toggle="collapse" data-parent="#filters" data-target="#kelime-body">
                                                <span class="more-less icon-down-arrow"></span>
                                            </a>

                                        </div>
                                        <div class="panel-body collapse" id="kelime-body">
                                            <div class="input-group input-group-sm" style="overflow: auto; max-height: 195px;">
                                                <input type="text" class="form-control" name="filterByText" id="filterText" aria-describedby="basic-addon2" value="<%= Model.SearchText %>">
                                                <span class="input-group-btn">
                                                    <button class="btn  btn-default" type="submit" id="filterByText">
                                                        <span class="glyphicon glyphicon-search"></span>&nbsp; Filtrele
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>



                    </div>
                </div>
                <%--   <% if (Model.VideoModels.Count > 0)
                   {%>
                <div class="panel panel-mt">
                    <div class="panel-heading">
                        <i class="fa fa-play"></i>&nbsp;&nbsp;Popüler Videolar
                    </div>
                    <ul class="media-list panel-body">
                        <%foreach (var item in Model.VideoModels)
                          {  %>
                        <li class="media"><a class="pull-left" href="<%:item.VideoUrl %>" title="<%:item.CategoryName %>">
                            <img class="media-object" width="80" height="60" src="<%:item.PicturePath %>" alt="<%:item.ProductName %>" />
                        </a>
                            <div class="media-body">
                                <div class="media-heading">
                                    <a href="<%:item.VideoUrl %>" class="text-info">
                                        <%:item.ProductName%></a>
                                    <br />
                                    <a href="<%:item.VideoUrl %>" class="text-muted text-xs">
                                        <%:item.TruncatetStoreName%></a>
                                    <br />
                                    <!--asdad!-->
                                    <span class="text-muted text-xs">
                                        <%:item.SingularViewCount%> görüntüleme
                                    </span>
                                </div>
                            </div>
                        </li>
                        <%} %>
                    </ul>
                </div>
                <% }%>--%>


                <% var display = string.IsNullOrEmpty(Request.QueryString["Gorunum"]) ? "?Gorunum=Liste" : "?Gorunum=" + Request.QueryString["Gorunum"].ToString(); %>
                <% string page = ""; if (Request.QueryString["Sayfa"] != null) { page = "&Sayfa=" + Request.QueryString["Sayfa"]; }  %>
                <% string querySearchType = string.IsNullOrEmpty(Request.QueryString["SearchType"]) ? "" : "&SearchType=" + Request.QueryString["SearchType"].ToString(); %>

                <div class="mobile-filter-buttons mobile-filter-top-alignment visible-xs">
                    <a href="javascript:;" class="js-toggle-filter"><span class="icon-filter"></span>Filterele</a>
                    <a href="javascript:;" class="js-toggle-sort"><span class="icon-sort"></span>Sırala</a>
                    <select class="mobile-sort" style="position: absolute; right: 0; width: 50%; height: 100%; opacity: 0">
                        <option value="<%: Request.FilePath %><%= display %><%:page  %><%:querySearchType %>&amp;Order=a-z">a-Z</option>
                        <option value="<%: Request.FilePath %><%= display %><%:page  %><%:querySearchType %>&amp;Order=z-a">Z-a</option>
                        <option value="<%: Request.FilePath %><%= display %><%:page  %><%:querySearchType %>&amp;Order=soneklenen">Son Eklenen</option>
                        <option value="<%: Request.FilePath %><%= display %><%:page  %><%:querySearchType %>">En Çok Görüntülenen</option>
                        <option value="<%: Request.FilePath %><%= display %><%:page  %><%:querySearchType %>&amp;Order=fiyat-artan">Fiyat Artan</option>
                        <%--                           <option value="<%: Request.FilePath %><%= display %><%:page  %><%:querySearchType %>&amp;Order=fiyat-artan">Fiyata Göre Artan</option>--%>
                    </select>
                </div>

                <script type="text/javascript">

                    $('#filterByText').on('click',
                        function (e) {
                            if ((e.type === 'keypress' && e.which === 13) || e.type === 'click') {
                                searchOperation();
                            }
                        });

                    $('#filterText').on('keypress',
                        function (e) {
                            if (e.type === 'keypress' && e.which === 13) {
                                searchOperation();
                            }
                        });

                    function searchOperation() {
                        var searchText = getParameterByName('SearchText');

                        var newSearchText = decodeURIComponent($('#filterText').val());
                        var currUrl = decodeURIComponent(window.location.href);

                        if (newSearchText === null || newSearchText === '') {
                            var categoryUrl = currUrl.replace(currUrl.substring(currUrl.indexOf('?SearchText') === -1 ? currUrl.indexOf('&SearchText') : currUrl.indexOf('?SearchText'), currUrl.length), '');
                            window.location.replace(categoryUrl);
                        } else {
                            if (currUrl.indexOf('SearchText') !== -1) {
                                window.location.replace(currUrl.replace(searchText, newSearchText));
                            } else {
                                if (currUrl.indexOf('?') === -1)
                                    window.location.replace(currUrl + '?SearchText=' + newSearchText);
                                else
                                    window.location.replace(currUrl + '&SearchText=' + newSearchText);
                            }
                        }
                    };
                    function getParameterByName(name, url) {
                        if (!url) url = window.location.href;
                        name = name.replace(/[\[\]]/g, "\\$&");
                        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                            results = regex.exec(url);
                        if (!results) return null;
                        if (!results[2]) return '';
                        return decodeURIComponent(results[2].replace(/\+/g, " "));
                    }
                </script>
                <%--  Banner Menü --%>
                <% if (Model.BannerModels != null)
                    { %>
                <div style="float: left; width: 252px; height: auto;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                        <tr>
                            <%var banner1 = Model.BannerModels.FirstOrDefault(k => k.BannerType == (byte)BannerType.CategorySideLeft); %>

                            <td align="center" valign="middle">
                                <% if (banner1 != null)
                                    { %>
                                <% if (banner1.BannerResource.Contains(".gif"))
                                    { %>
                                <a href="http://<%:banner1.BannerLink %>" target="_blank">
                                    <img src="<%:AppSettings.BannerGifFolder  + banner1.BannerResource %>" alt="" /></a>
                                <% }
                                    else if (banner1.BannerResource.Contains("swf"))
                                    { %>
                                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0"
                                    width="252">
                                    <param name="wmode" value="transparent" />
                                    <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>" />
                                    <param name="quality" value="high" />
                                    <param name="wmode" value="transparent" />
                                    <embed src="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>" quality="high"
                                        pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
                                        type="application/x-shockwave-flash" width="252" wmode="transparent">
                                    </embed>
                                </object>
                                <% }
                                    else
                                    {%>
                                <a href="http://<%:banner1.BannerLink %>" target="_blank">
                                    <img width="252" src="<%:AppSettings.BannerImagesThumbFolder+ banner1.BannerResource %>" alt="" /></a>
                                <% } %>
                                <% } %>
                            </td>
                        </tr>
                    </table>
                </div>
                <% } %>
            </div>
        </div>



        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-10 category-list-wrapper">
            <%--<div class="row clearfix category-firm-slider-wrapper hidden">
                <div class="col-xs-12">
                    <div class="category-firm-slider owl-carousel owl-theme">

                        <div class="item">
                            <a href="javascript:;">
                                <img src="https://www.makinaturkiye.com//UserFiles/Banner/ImagesThumb/ea2f84cb2ea2481fa56b73f74b9d312b.jpg" alt="" title="" />
                            </a>
                        </div>
                        <div class="item">
                            <a href="javascript:;">
                                <img src="https://www.makinaturkiye.com//UserFiles/Banner/ImagesThumb/ea2f84cb2ea2481fa56b73f74b9d312b.jpg" alt="" title="" />
                            </a>
                        </div>
                        <div class="item">
                            <a href="javascript:;">
                                <img src="https://www.makinaturkiye.com//UserFiles/Banner/ImagesThumb/ea2f84cb2ea2481fa56b73f74b9d312b.jpg" alt="" title="" />
                            </a>
                        </div>

                    </div>
                </div>
            </div>--%>



            <%if (!string.IsNullOrEmpty(Model.ContentSide))
                {%>


            <%--<div class="row clearfix">
                <div class="col-xs-12">
                    <div class="category-desc">

                        <p class="category-desc__spot">
                            <%=Model.ContentSide %>
                        </p>
                    </div>
                </div>
            </div>--%>

            <%} %>
            <div class="row clearfix" >
                <div class="col-xs-12">
                    <%=Html.RenderHtmlPartial("_SliderBanner",Model.MTCategoSliderItems) %>
                    <div class="categort-filter">
                                <div class="categort-filter__bottom">
                                         <div class="row">
                                           <div class="col-md-12">
                                    <div class="categort-filter__result-text">
                                        <h2>
                                            <%if (string.IsNullOrEmpty(Model.SearchText))
                                                {%>
                                            <strong>
                                <% if (Model.FilteringContext.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.New)
                                    {%>
                                      Sıfır
                                 <%}
                                     else if (Model.FilteringContext.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.Used)
                                     {%>
                                      İkinci El
                                 <%}%>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Marka") &&

                                                              Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Marka").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%brandName = Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Marka").ItemModels.FirstOrDefault(k => k.Selected).FilterName; %>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Marka").ItemModels.FirstOrDefault(k => k.Selected).FilterName%>
                                                <%}
                                                    else
                                                    {%>
                                                <%if (!string.IsNullOrEmpty(Model.SpesificBrandName))
                                                    { %>
                                                <%=Model.SpesificBrandName%>
                                                <%}
                                                    } %>

                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Model") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Model").ItemModels.Any(k => k.Selected))
                                                    {%>

                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Model").ItemModels.FirstOrDefault(k=>k.Selected).FilterName%>
                                                <%} %>
                                                <%if (!string.IsNullOrEmpty(Model.SpesificCategoryNameForModelH1) && brandName != Model.SpesificCategoryNameForModelH1)
                                                    { %>

                                                <%:Model.SpesificCategoryNameForModelH1 %>

                                                <%} %>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Seri") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Seri").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Seri").ItemModels.FirstOrDefault(k=>k.Selected).FilterName%>
                                                <%} %>

                                                <%if (Model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Brand && Model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Series && Model.CategoryModel.SelectedCategoryType != (byte)CategoryType.Model)
                                                    {%>

                                                <%:Model.CategoryModel.SelectedCategoryContentTitle%>
                                                <%}
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(Model.SpesificCategoryNameForModelH1))
                                                        {
                                                            var topCategory = Model.CategoryModel.TopCategoryItemModels.LastOrDefault(c => c.CategoryType != (byte)CategoryType.Brand && c.CategoryType != (byte)CategoryType.Model && c.CategoryType != (byte)CategoryType.Series);
                                                            if (!string.IsNullOrWhiteSpace(topCategory.CategoryContentTitle))
                                                            {
                                                %>
                                                <%:topCategory.CategoryContentTitle %>
                                                <%
                                                    }
                                                    else
                                                    {
                                                %>
                                                <%:topCategory.CategoryName %>
                                                <% }
                                                %>


                                                <%}
                                                    }%>

                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "İlçe") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "İlçe").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "İlçe").ItemModels.FirstOrDefault(k=>k.Selected).FilterName%>
                                                <%} %>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Şehir") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Şehir").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Şehir").ItemModels.FirstOrDefault(k => k.Selected).FilterName%>
                                                <%}
                                                    else
                                                    { %>
                                                <%if (Model.FilteringContext.DataFilterMoldes.Any(k => k.FilterName == "Ülke") && Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Ülke").ItemModels.Any(k => k.Selected))
                                                    {%>
                                                <%:Model.FilteringContext.DataFilterMoldes.FirstOrDefault(k => k.FilterName == "Ülke").ItemModels.FirstOrDefault(k => k.Selected).FilterName%>
                                                <%}
                                                    } %>
                                                <%if (!string.IsNullOrEmpty(Model.SameCategoryH1))
                                                    { %>
                                        (<%:Model.SameCategoryH1 %>)
                                <%} %>
                                    kategorisi
                                            </strong>
                                                 </h2>
                                        <span>&nbsp; aramanızda <font class="text-danger"><%:Model.TotalItemCount%> </font>adet ilan bulundu     <%if (Request.QueryString["page"] != null)
                                            { %>
                                            <span class="small">&nbsp;<%:Model.PagingModel.CurrentPageIndex%>. Sayfa</span>
                                            <%} %></span>

                                        <% } %>
                                        <% if (!string.IsNullOrEmpty(Model.SearchText))
                                            { %>

                                        <%if (string.IsNullOrEmpty(Model.CategoryModel.SelectedCategoryName))
                                            { %>
                                "<strong><%:Model.SearchText%></strong>" kelimesinde <span class="text-danger"><%:Model.FilteringContext.TotalItemCount%></span> Adet ürün bulundu.
                        <%}
                            else
                            {
                        %>
                                "<%:Model.SearchText %>" araması için <strong>"<%:Model.CategoryModel.SelectedCategoryContentTitle%>"</strong> kategorisinde <span class="text-danger"><%:Model.FilteringContext.TotalItemCount%></span> Adet sonuç bulundu.
                        <%} %>


                                        <% }%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%= Html.RenderHtmlPartial("ProductHeader", Model.FilteringContext)%>


                    </div>
                </div>
            </div>


            <div class="row clearfix">
                <div class="col-md-12">


                    <% if (1 == 1)
                        { %>
                    <%if (Request.QueryString["Gorunum"] != null && Request.QueryString["Gorunum"].ToString() == "Liste")
                        {
                    %>
                    <%= Html.RenderHtmlPartial("ProductViewList", Model)%>

                    <%
                        }
                        else
                        {%>   <%= Html.RenderHtmlPartial("ProductViewGalery", Model)%>

                    <% } %>

                    <% }
                        else
                        { %>

                    <div class="productNotFound" style="height: 1000px; width: 468px;">
                        <span>Bu kategoriye ait herhangi bir ürün bulunamadı.</span>


                    </div>
                    <% } %>
                </div>
            </div>

        </div>
    </div>
    <% } %>



    <%if (!string.IsNullOrEmpty(Model.ContentBottomCenter))
        {%>
    <div class="row clearfix">
        <div class="alert alert-info alert-mt">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                ×
            </button>
            <h2 style="font-size: 12px; font-size: 12px; margin-top: 0px; margin-bottom: 0px;"><b><i><%:Model.CategoryModel.SelectedCategoryName %>  nerelerde kullanılır? Nasıl Kullanılır?</i></b></h2>
            <%=Model.ContentBottomCenter %>
        </div>
    </div>
    <%} %>


    <div class="clearfix" id="PopulerUrunlerTitle" style="margin-bottom: -30px">
        <div class="col-xs-12">
            <h2 class="section-title-category section-title--left">
                <span>
                    <%:Model.CategoryModel.SelectedCategoryContentTitle %> Popüler Ürünler
                </span>
            </h2>
        </div>
    </div>
    <div class="clearfix">
        <%=Html.RenderHtmlPartial("_MostViewProduct",Model.MostViewedProductModel) %>
    </div>

    <%
        var app = "1";
        if (Request.Url.Segments.Length > 2)
        {
            app = Request.Url.Segments.Length == 2 ? "" : Request.Url.Segments[2];
        }

        if (app != "Sektor" || app != "Sektor/" || app == "1")
        {
            if (Model.CategoryModel.SelectedCategoryId != 0)
            {
    %>

    <%=Html.RenderHtmlPartial("_CategoryStores", Model.StoreModel)%>


    <% }
        }%>


    <%if (!string.IsNullOrEmpty(Model.SeoModel.SeoContent) || !string.IsNullOrEmpty(Model.SeoModel.Description))
        { %>
    <div class="alert alert-info alert-mt-category" >
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
            ×
        </button>
        <%if (!string.IsNullOrEmpty(Model.SeoModel.SeoContent))
            { %>

        <%=Model.SeoModel.SeoContent %>
        <%=Model.ContentSide %>
        <%}%>
        <%=Model.SeoModel.Description %>
    </div>
    <%} %>


    <script type="application/ld+json">
    <%=Model.CategoryModel.MtJsonLdModel.JsonLdString %>
    </script>
</asp:Content>
