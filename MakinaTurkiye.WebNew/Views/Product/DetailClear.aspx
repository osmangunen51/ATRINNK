<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTProductDetailViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function ProductPopupGallery() {
            $('.product-popup-gallery').magnificPopup({
                delegate: 'div a',
                type: 'image',
                tLoading: 'Resim Yükeniyor #%curr%...',
                mainClass: 'mfp-img-mobile',
                gallery: {
                    enabled: true,
                    navigateByImgClick: true,
                    preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
                },
                image: {
                    tError: '<a href="%url%"> #%curr%</a> resim yüklenemedi',
                    titleSrc: function (item) {
                        return item.el.attr('title') + '<small></small>';
                    }
                }
            });
        }
        function ProductCommentPage(p) {
            $.ajax({
                url: 'https://wwww.makinaturkiye.com/ajax/ProductCommentPagination',
                data: { ProductId: <%:Model.ProductDetailModel.ProductId%>, page: p },
                type: 'get',
                success: function (data) {
                    if (data) {
                        $("#productCommentContainer").html(data);
                        $('html, body').animate({
                            scrollTop: $("#productCommentContainer").offset().top
                        }, 2000);
                    }
                    else {

                    }

                }
            }
            );

        }
                        function CertificatePopUpGallery() {
            $('.certificate-popup-gallery').magnificPopup({
                delegate: 'div a',
                type: 'image',
                tLoading: 'Resim Yükeniyor #%curr%...',
                mainClass: 'mfp-img-mobile',
                gallery: {
                    enabled: true,
                    navigateByImgClick: true,
                    preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
                },
                image: {
                    tError: '<a href="%url%"> #%curr%</a> resim yüklenemedi',
                    titleSrc: function (item) {
                        return item.el.attr('title') + '<small></small>';
                    }
                }
            });
        }
        $(document).ready(function () {
            CertificatePopUpGallery();
            ProductPopupGallery();
             let isMobile = window.matchMedia("only screen and (max-width: 760px)").matches;

            if (isMobile) {

                var i = 0;
                var count = $(".breadcrumb-mt li").length;
                $(".fast-access-bar").css("margin-top", "-20px");
                $(".fast-access-bar").removeAttr("class");
               
                $(".breadcrumb-mt li").each(function (index) {
                    if (index == 0) {
                              $(this).show();
                    }
                    else if (index == count - 1) {
                          $(this).css("display", "none");
                    }
                    else if (index < count - 4)
                    {
                        $(this).css("display", "none");
                    }
                 });
            }

            $("#btnProductCommentInValid").click(function () {

                $.ajax({
                    url: '<%:AppSettings.SiteUrlWithoutLastSlash%>/ajax/AddProductComment',

                    data: { CommentText: "", Rate: 0, ProductId: '<%:Model.ProductDetailModel.ProductId%>' },
                    type: 'get',
                    success: function (data) {
                        if (data) {
                            $("#CommentForm").slideUp();
                            $("#CommentedAlert").show();
                        }
                        else {
                            window.location = "/uyelik/kullanicigirisi?returnUrl=<%:Request.Url.AbsolutePath%>";
                        }

                    }

                }
                );
            });
            $("#btnProductComment").click(function () {
                var rate = $("#rateProduct").val();
                var commentText = $("#CommentText").val();
                if (rate == "0" || commentText == "") {
                    if (rate == "0")
                        $("#displayRateError").show();
                    else if (commentText == "")
                        $("#CommentText").focus();
                }
                else {
                    //ajax post et
                    $.ajax({
                        url: '<%:AppSettings.SiteUrlWithoutLastSlash%>/ajax/AddProductComment',
                        data: { CommentText: commentText, Rate: rate, ProductId: '<%:Model.ProductDetailModel.ProductId%>' },
                        type: 'get',
                        success: function (data) {
                            if (data) {
                                $("#CommentForm").slideUp();
                                $("#CommentedAlert").show();
                            }
                            else {
                                window.location = "/uyelik/kullanicigirisi";
                            }

                        }

                    }
                    );

                }
            });

            $('#stars').on('starrr:change', function (e, value) {
                $('#count').html(value);
            });

            $('#stars-existing').on('starrr:change', function (e, value) {
                $('#rateProduct').val(value);
                $("#displayRateError").hide();
            });
        });


    </script>
    <%if (Model.ProductPictureModels.Count > 0)
        {
    %>
    <meta name="og:image" content="<%:Model.ProductPictureModels.FirstOrDefault().LargePath %>" />
    <%} %>
    <%=Html.RenderHtmlPartial("_ProductHeader") %>
    <%if (!Model.ProductDetailModel.IsActive)
        { %>

    <%}
        else
        {
    %>
    <script type="text/javascript" src="/Content/v2/assets/js/phonemask.js"></script>

    <script type="text/javascript">

        function AddWhatsappLog(id) {
            $.ajax({
                url:'<%:AppSettings.SiteUrlWithoutLastSlash%>/ajax/AddWhatsappLog',
                data: { storeId: id },
                type:'get',
                success: function (data) {

                }
            }
            );
        }

        $(document).ready(function () {

            SetProductStatistic();
            $("#PhoneNumber").mask("(999) 999-9999");
            $("#PhoneNumberAgain").mask("(999) 999-9999");


            $("#phone").on("blur", function () {
                var last = $(this).val().substr($(this).val().indexOf("-") + 1);

                if (last.length == 5) {
                    var move = $(this).val().substr($(this).val().indexOf("-") + 1, 1);

                    var lastfour = last.substr(1, 4);

                    var first = $(this).val().substr(0, 9);

                    $(this).val(first + move + '-' + lastfour);
                }
            });
        });
        function SetProductStatistic() {
            $.ajax({
                url: 'https://www.makinaturkiye.com/ajax/ProductStatisticCreate',
                header: {
                    'X-Robots-Tag': 'googlebot: nofollow'
                },
                data: { productId: <%:Model.ProductDetailModel.ProductId%> },
                type: 'get',

                success: function (data) {
                }
            }
            );
        }
        function GetSimilarProductAjax() {
            $.ajax({
                type: 'post',
                contentType: "application/json",
                dataType: "json",
                url: '/Product/GetSimilarProducts',
                header: {
                    'X-Robots-Tag': 'googlebot: nofollow'
                },
                data: JSON.stringify({ categoryId: '<%:Model.ProductDetailModel.CategoryId%>', ProductId: '<%=Model.ProductDetailModel.ProductId%>' }),
                success: function (data) {
                    $(".SimilarProductWrapper").html(data);

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
                                        items: 1
                                    },
                                    600: {
                                        items: 2
                                    },
                                    768: {
                                        items: 2
                                    },
                                    960: {
                                        items: 3
                                    },
                                    1100: {
                                        items: 6
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
                    alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
                }
            });
        }


    </script>

    <meta name="robots" content="INDEX,FOLLOW" />
    <% }%>
    <meta name="google-site-verification" content="jpeiLIXc-vAKBB2vjRZg3PluGG3hsty0n6vSXUr_C-A" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <%=Html.RenderHtmlPartial("_ProductPageHaderNew",Model.ProductPageHeaderModel) %>
    <style>
        .addthis_inline_share_toolbox { padding-top: 10px; padding-bottom: 0; margin-bottom: 0px; }
    </style>
    <div class="row urun-detay">
        <div class="col-md-12">
        <h1 class="product-detail__title">
           
            <%:Model.ProductDetailModel.ProductName %>
        </h1>
            </div>
        <%if (Model.OnlyStoreSee)
            { %>
        <div class="flex-row flex-lg-nowrap flex-md-nowrap" style="padding-left: 15px; padding-right: 15px; margin-bottom: 20px;">

            <div class="flex-md-12 flex-sm-12 flex-xs-12 text-center product-activetype-display p-r-lg-15 p-r-md-15  p-r-sm-15">
                <b style="font-size: 16px;">İlanınız inceleniyor</b>
            </div>
        </div>
        <%} %>
        <div class="flex-row flex-lg-nowrap flex-md-nowrap" style="padding-left: 15px; padding-right: 15px;">
            <div class="flex-xs-12 flex-sm-12 flex-md-9 flex-lg-6">
                <div class="flex-row">
                    <div class="flex-xs-12 flex-md-12 flex-sm-12 flex-lg-12 p-r-lg-15 p-r-md-15  p-r-sm-15">
                        <div class="thumbnail">
                            <%=Html.RenderHtmlPartial("_ProductPictureNew",Model.ProductPictureModels)%>
                        </div>
                    </div>
    
                </div>
            </div>
                     <div class="flex-xs-12 flex-md-7 flex-sm-7 flex-lg-3 p-r-lg-15 p-r-md-15 ">
                        <%=Html.RenderHtmlPartial("_ProductDetailNew", Model.ProductDetailModel)%>
                    </div>
            <div class="flex-xs-12 flex-sm-12 flex-md-3 flex-lg-3 rightSidebar">
                <div>
                    <div class="affixRight" data-bottom-fix="#fixthis">
                        <div style="display: block;">
                            <div class="row">
                                <div class="col-sm-6 col-xs-12 col-md-12 col-lg-12">
                                    
                                 
                                            <%=Html.RenderHtmlPartial("_ProductStoreNew", Model.ProductStoreModel)%>
                                            <%=Html.RenderHtmlPartial("_StoreOtherProductNew", Model.StoreOtherProductModel)%>
                                   
                                    
                                </div>
                                <%--         <div class="col-sm-6 col-xs-12 col-md-12 col-lg-12 hidden-xs">
                                    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                                    <!-- Ürün Detay Sağ Üst -->
                                    <ins class="adsbygoogle"
                                        style="display: inline-block; width: 250px; height: 300px;"
                                        data-ad-client="ca-pub-5337199739337318"
                                        data-ad-slot="4218995628"></ins>
                                    <script>
                                        (adsbygoogle = window.adsbygoogle || []).push({});
                                    </script>
                                </div>--%>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
            
                    <div class="flex-xs-12  p-r-lg-15 p-r-md-15">
                        <%=Html.RenderHtmlPartial("_ProductTabNew",Model.ProductTabModel) %>
                    </div>
        <div class="col-xs-12" id="fixthis">
            <%=Html.RenderHtmlPartial("_SimilarProductNew",Model.SimilarProductModel) %>
        </div>

        <%--        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="panel-title">
                        Sponsorlu
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-4 text-center">
                            <!-- Ürün Detay Alt Sol -->
                            <ins class="adsbygoogle"
                                style="display: inline-block; width: 300px; height: 250px"
                                data-ad-client="ca-pub-5337199739337318"
                                data-ad-slot="5648766405"></ins>
                            <script>
                                (adsbygoogle = window.adsbygoogle || []).push({});
                            </script>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-4 text-center">
                            <!-- Ürün Detay Alt Orta -->
                            <ins class="adsbygoogle"
                                style="display: inline-block; width: 300px; height: 250px"
                                data-ad-client="ca-pub-5337199739337318"
                                data-ad-slot="6376235150"></ins>
                            <script>
                                (adsbygoogle = window.adsbygoogle || []).push({});
                            </script>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-4 text-center">
                            <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                            <!-- Ürün Detay Alt sağ -->
                            <ins class="adsbygoogle"
                                style="display: inline-block; width: 300px; height: 250px"
                                data-ad-client="ca-pub-5337199739337318"
                                data-ad-slot="3765673249"></ins>
                            <script>
                                (adsbygoogle = window.adsbygoogle || []).push({});
                            </script>
                        </div>
                    </div>
                </div>
            </div>

        </div>--%>
    </div>
    <%=Html.RenderHtmlPartial("_ProductStoreContact",Model.ProductContanctModel) %>
    <%if (!string.IsNullOrEmpty(Model.MtJsonLdModel.JsonLdString))
        {%>
    <script type="application/ld+json">
       
         <%=Model.MtJsonLdModel.JsonLdString %>
    </script>

    <% } %>
</asp:Content>
