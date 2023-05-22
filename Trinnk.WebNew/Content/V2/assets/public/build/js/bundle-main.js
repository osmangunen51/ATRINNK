/*
    Ana Menu Üst Menüye Göre Renk Değişimi
*/
function NewPhoneNumberWrapperShow() {
    $("#noActivationWrapper").show();
    $("#PhoneActivation-wrapper").hide();
    $("#noActivationWarm").hide();
    $("#redirectbtn").hide();
    $("#PhoneNumberAgain").val("");
}

function mainMenuColorChange() {
    var selecter = $('.subheader .nav-tabs-mt > li');
    var menuContainer = $('.left-menu');
    var navContainer = $('.subheader .nav-tabs-mt');

    if (selecter.eq(0).hasClass('active')) {
        menuContainer.addClass('left-menu__color1');
        navContainer.addClass('color1');
    }
    if (selecter.eq(1).hasClass('active')) {
        menuContainer.addClass('left-menu__color2');
        navContainer.addClass('color2');
    }
    if (selecter.eq(2).hasClass('active')) {
        menuContainer.addClass('left-menu__color3');
        navContainer.addClass('color3');
    }
}
/// <reference path="../../V2/assets/js/jquery.min.js" />
$(function () {
    //document.getElementById('vd_html5_api').addEventListener('ended', VideoFinished, false);
});
function VideoFinished() {
    // What you want to do after the event
    // alert($('ul.media-list li:eq(0) a:eq(0)').attr('href'));
    location.href = "" + $('ul.media-list li:eq(0) a:eq(0)').attr('href');
}
function PopulerVideosRemoveItemVideo(videoId) {
    $(window).load(function () {
        var nextVideoIdItem = "V_" + videoId;
        $("li#" + nextVideoIdItem).remove();
    });
}



function FacebookAjaxResponse(response) {
    $.ajax({
        type: "post",
        url: '/Membership/SocialMembership',
        data: JSON.stringify({
            "model": {
                "MemberName": response.first_name,
                "MemberSurname": response.last_name,
                "MemberEmail": response.email,
                "MemberEmailAgain": response.email,
                "LocalityId": "0",
                "TownId": "0",
                "CityId": "0",
                "CountryId": "0",
                "AddressTypeId": "0"
            },
            "MembershipType": "5",
            "profileId": response.id
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg)
                window.location.href = window.location.origin;
        },
        error: function () {

        }
    });
}
function HariciLinkler() {
    if (!document.getElementsByTagName) return;
    var linkler = document.getElementsByTagName("a");
    var linklerAdet = linkler.length;
    for (var i = 0; i < linklerAdet; i++) {
        var tekLink = linkler[i];
        if (tekLink.getAttribute("href") && tekLink.getAttribute("rel") == "external") {
            tekLink.target = "_blank";
        }
    }
}
function facebookLoginClickEvent() {
    $(".js-facebook-login").click(function () {
        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                FB.api('/me?fields=id,email,name,first_name,last_name,age_range,gender,locale,picture,verified', FacebookAjaxResponse, { scope: 'email,public_profile' });
            }
            else {
                FB.login(function (response) {
                    if (response.authResponse) {
                        var faceMemberToken = response.authResponse.accessToken;
                        var faceProfilId = response.authResponse.userID;
                        FB.api('/me?fields=id,email,name,first_name,last_name,age_range,gender,locale,picture,verified', FacebookAjaxResponse,
                        { scope: 'email,public_profile,id,name,first_name,last_name,age_range,gender,locale,picture,verified' });
                    }
                }, { scope: 'email,public_profile' });
            }
        });
    });
}

/*Listeden Firma Ara Seç*/
function selectStoreSearch() {
    $(".navbar-form").attr("action", "/Sirketler?SearchType=3&amp;");

};

/*Listeden Ürün Ara Seç*/
function selectProductSearch() {
    $(".navbar-form").attr("action", "/Urunler/AramaKelimesi?CategoryId=0");
};
function selectVideoSearch() {
    $(".navbar-form").attr("action", "/Videolar/AramaSonuclari?CategoryId=0");
}



/*
    Sayfalama Git Inputu
*/
function pagerGoto() {
    if ($('#paget').length < 1) return;
    $('#paget').bind('keypress', function (e) {
        if (e.keyCode == 13) {
            return false;
        }
    });
    $('#paget').click(function () {
        $(this).val('');
    });
}
/*
    Listeleme de ürün görsellerini mobil görünüm de 400x300 yapar
*/
//function productImageResize() {
//    if ($("body").width() < 775 && $(".urun-resim img").length > 0) {
//        $(".urun-resim img").each(function () {
//            var lastW = $(this).attr("src");
//            var newW = lastW.replace("160x120", "400x300");
//            $(this).attr("src", newW);
//        });
//    }
//}

function ChangeVideo(e) {
    //console.log($(this));
    //document.getElementById('vd');
    //var src = $(this).attr('data-videopath');//document.getElementById("videoSlide").getAttribute('data-videoPath');
    // v.children().remove(); //.innerHTML += '<source src=' + src + ' type=video/mp4>';
    //v.load();
}


function siteChangeVideo() {
    $('[data-target="#videocontent"]').on('click', function () {
        var videoPath = $(this).attr('data-videopath');
        videoViewOnModalShow(videoPath);
    });

    $(document).on('click', '.js-video-slide-item', function () {
        var videoPath = $(this).attr('data-videopath');
        videoViewOnModalShow(videoPath);
    });
}

function videoViewOnModalShow(videoPath) {
    var v = document.getElementById('vd');
    v.innerHTML = "";
    v.innerHTML += '<source src=' + videoPath + ' type=video/mp4>';
    v.load();
}

function mailActivationValidate() {
    if ($('#activation').length < 1) return;
    $('#activation').validationEngine();
}

function videoPause() {
    document.getElementById('vd').pause();
}

/*Url Redirect*/
function Redirect() {
    window.location.replace(window.location.href);
}

function checkitem() {
    if ($('#megafoto-slider').length < 1) return;
    var $this = $('#megafoto-slider');
    if ($('.carousel-inner .item:first').hasClass('active')) {
        $this.children('.left.carousel-control').hide();
        $this.children('.right.carousel-control').show();
    } else if ($('.carousel-inner .item:last').hasClass('active')) {
        $this.children('.left.carousel-control').show();
        $this.children('.right.carousel-control').hide();
    } else {
        $this.children('.carousel-control').show();

    }
}

function AddFavoriteStore(storeId, storeName) {
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('\'' + storeName + '\' Favori listenize ekleniyor.. &nbsp;&nbsp;' + image);
    $.ajax({
        url: '/Product/AddFavoriteStore',
        dataType: "json",
        type: 'post',
        data:
     {
         MainPartyId: storeId
         //                productId: productId
     },
        success: function (data) {
            if (data == true) {
                //                        $.facebox('\'' + storeName + '\' başarıyla favori satıcılarınıza eklenmiştir.');
                $('#aRemoveFavoriteStore').show();
                $('#aAddFavoriteStore').hide();
                //                        $('#divFavroriteStoreImage').css('background-image', 'url(/Content/Images/removeFavorite.png)');
            }
            else {
                window.location.href = '/Uyelik/kullanicigirisi';
                //    $.facebox('\'' + storeName + '\' firmasını favori satıcılıarınıza eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.');
            }
        },
        error: function (x, l, e) {
            $.facebox('\'' + storeName + '\' favori satıcılarınıza eklerken bir sorun oluştu.');
        }
    });
}

function AddFavoriteStoreSuccess(result) {
    if (result) {
        $("#aAddFavoriteStore").hide();
        $("#aRemoveFavoriteStore").show();
    }
    else {
        window.location.href = '/Uyelik/kullanicigirisi';
    }

}
function RemoveFavoriteStoreSuccess(result) {
    if (result) {
        $("#aRemoveFavoriteStore").hide();
        $("#aAddFavoriteStore").show();
    }
    else {
        window.location.href = '/Uyelik/kullanicigirisi';
    }
}


function RemoveFavoriteStore(storeId, storeName) {
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('\'' + storeName + '\' Favori listenizden çıkarılıyor.. &nbsp;&nbsp;' + image);
    $.ajax({
        url: '/Product/RemoveFavoriteStore',
        type: 'post',
        dataType: "json",
        data:
     {
         MainPartyId: storeId
     },
        success: function (data) {
            //                    $.facebox('\'' + storeName + '\' favori satıcılarınızdan çıkarılmıştır.');
            $('#aRemoveFavoriteStore').hide();
            $('#aAddFavoriteStore').show();

            //                    $('#divFavroriteStoreImage').css('background-image', 'url(/Content/Images/addFavorite.png)');
        },
        error: function (x, l, e) {
            $.facebox('\'' + storeName + '\' favori satıcılarınızdan çıkarılırken bir sorun oluştu.');
        }
    });
}

function AddFavoriteProduct(id) {
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('Görüntülemiş olduğunuz ürün favori listenize ekleniyor.. &nbsp;&nbsp;' + image);
    $.ajax({
        url: '/Product/AddFavoriteProduct',
        type: 'post',
        dataType: 'json',
        data:
     {
         ProductId: id
     },
        success: function (data) {
            if (data == true) {
                //                        $.facebox('Görüntülemiş olduğunuz ürün başarıyla favori ürünlerinize eklenmiştir.');
                $('#aRemoveFavoriteProduct').show();
                $('#aAddFavoriteProduct').hide();
                //                        $('#divFavroriteProductImage').css('background-image', 'url(/Content/Images/removeFavorite.png)');
            }
            else {

                //  $.facebox('Görüntülemiş olduğunuz ürünü favori ürünlerinize eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.');
                window.location.href = '/Uyelik/kullanicigirisi';
            }
        },
        error: function (x, l, e) {
            $.facebox('Görüntülemiş olduğunuz ürünü favori ürünlerinize eklerken bir sorun oluştu.');
        }
    });
}

function RemoveFavoriteProduct(id) {
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('Görüntülemiş olduğunuz ürün favori listenizden çıkarılıyor.. &nbsp;&nbsp;' + image);

    $.ajax({
        url: '/Product/RemoveFavoriteProduct',
        type: 'post',
        data:
     {
         ProductId: id
     },
        success: function (data) {
            //                    $.facebox('Görüntülemiş olduğunuz ürün favori ürünlerinizden çıkarılmıştır.');
            $('#aRemoveFavoriteProduct').hide();
            $('#aAddFavoriteProduct').show();
            //                    $('#divFavroriteProductImage').css('background-image', 'url(/Content/Images/addFavorite.png)');
        },
        error: function (x, l, e) {
            $.facebox('Görüntülemiş olduğunuz ürün favori ürünlerinizden çıkarılırken bir sorun oluştu.');
        }
    });
}
$("#productNumberForInf").keyup(function () {
    if (this.value.match(/[^0-9]/g)) {
        this.value = this.value.replace(/[^0-9]/g, '');
    }
});
function AddInfoForDemand() {
    var productNum = $.trim($("#productNumberForInf").val());
    if (productNum != "") {
        $.ajax({
            url: '/MemberShipSales/AddInfoForDemand',
            data: { productNumber: productNum },
            type: 'post',
            success: function (data) {
                $("#productNumberForInf").val("");
                $("#InfSuccess").show();

            }, error: function (x, l, e) {
                alert(x.status);
                alert(e.responseText);
            }
        });
    }
    else {
        alert("Lütfen Değer Giriniz.");
    }


}

function SendMessageforpro(messagetype) {
    var memberNo = $('#hiddenMemberNo').val();
    memberNo = memberNo.replace('#', '');
    memberNo = memberNo.replace('#', '');
    var productNo = $('#hiddenProductNo').val()
    productNo = productNo.replace('#', '');
    var obj = new Object();
    obj.redirect = '/Account/Message/Index?MessagePageType=1&UyeNo=' + memberNo + '&UrunNo=' + productNo;
    $.ajax({
        url: '/Product/IsAuthenticate',
        data: obj,
        success: function (data) {
            if (data == true) {
                var memberNo = $('#hiddenMemberNo').val();
                memberNo = memberNo.replace('#', '');
                memberNo = memberNo.replace('#', '');
                var productNo = $('#hiddenProductNo').val()
                productNo = productNo.replace('#', '');

                //                                                window.location.href = '/Mesaj/UyeNo=' + memberNo + '/UrunNo=' + productNo;
                window.location.href = '/Account/Message/Index?MessagePageType=1&UyeNo=' + memberNo + '&UrunNo=' + productNo;

            }
            else {
                if (messagetype == 'message') {//üye girişi yapmamış ve mesajdan gelmiş popup acılacak
                    var country = $("#country").val();
                    if (country == "Turkey")
                        $("#PostCommentMessage").trigger("click");
                    else {
                        window.location.href = '/Uyelik/HizliUyelik/UyelikTipi-0?gelenSayfa=mesaj';

                    }
                    //                        <a href="#" data-toggle="modal" data-target="#PostCommentsModal">Post Comments</a>
                    //                             <a href="#" data-toggle="modal" data-target="#LoginModal">Logintt</a>

                } else {

                    var memberNo = $('#hiddenMemberNo').val();
                    memberNo = memberNo.replace('#', '');
                    memberNo = memberNo.replace('#', '');

                    var productNo = $('#hiddenProductNo').val()
                    productNo = productNo.replace('#', '');

                    //                                                window.location.href = '/mesajgonder/' + memberNo + '/' + productNo;
                    window.location.href = '/Uyelik/kullanicigirisi';
                }
            }
        },
        error: function (x, l, e) {
            $.facebox('Bilinmeyen bir hata oluştu. Kısa bir süre sonra tekrar deneyiniz.');
        }
    });

}

function moreLessLink() {
    if ($(".more-link").length < 1) return;
    $(".more-link").click(function () {
        $(this).hide().parent().find("ul").slideDown().end().find(".less-link").show();
        return false;
    });
    $(".less-link").click(function () {
        $(this).hide().parent().find("ul").slideUp().end().find(".more-link").show();
        return false;
    });
}

function SendActivationSms() {
    $("#mailSuccess").html("Gönderiliyor..");
    $.ajax({
        url: '/Product/fastSignupPhone',
        type: 'post',
        data: {
            Email: $("#MemberEmail").val()
        },
        success: function (data) {
            if (data) {
                //basarili
                $("#activation-wrapper").show();
                $("#mailSuccess").hide();
            }
            else {
                //basarili degil
                $("#mailSuccess").hide();
                $("#errorMessage").show();
            }

        }
    });
}

function signUpWithActivationCode(activationCode1) {
    $.ajax({
        url: '/Product/fastSignupPhone1',
        type: 'post',
        data: {
            activationCode: activationCode1
        },
        success: function (data) {
            if (data) {
                //basarili
                $("#loginSuccess").show();
                window.location.replace(window.location.href);
            }
            else {
                //basarili degil
                $("#loginError").show();
            }
        }
    });
}
function Signup(email, password) {
    $.ajax({
        url: '/Product/fastSignup',
        type: 'post',
        data: {
            Password: password,
            Email: email
        },
        success: function (data) {
            if (data) {
                //basarili
                $("#loginSuccess").show();
                window.location.replace(window.location.href);

            }
            else {
                //basarili degil
                $("#loginError").show();
            }

        }
    });
}

function SendMessagePopup(senderMainPartyID1, error1, code) {
    $.ajax({
        url: '/Product/ProductMessageSender',
        contenType: "text/html",
        type: 'post',
        data: {
            content: $('#mailDescription').val(),
            subject: $('#mailTitle').val(),
            productId: $('#hdnProductId').val(),
            senderMainPartyId: senderMainPartyID1,
            memberEmail: $('#hdnMemberEmail').val(),
            mailTitle: $('#mailTitle').val(),
            mailDescription: $('#mailDescription').val(),
            PhoneNumber: $('#PhoneNumber').val(),
            error: error1,
            phoneCode: code


            //                mailpass
            //                mailname
            //                mailsurname
            //                mailtelephone
            //                mailtitle
            //                maildescription
            //                  MemberName
            //                  MemberSurname
            //                  MemberPassword
            //                  MemberEmail

            //                CategoryId: $('#Edit_CategoryId').val(),
            //                  CategoryName: $('#Edit_Name').val(),
            //                  CategoryOrder: $('#Edit_Order').val()

            //email: $(this).val(), productNumber: productNo, memberNumber: memberNo 

        },
        success: function (data) {
            if (data) {

                //                    var _ID = '#' + $('#Edit_CategoryId').val();
                //                    $(_ID + ' span:first label:first').html(data.CategoryName);
                //                    $(_ID + ' span:first .sort').html(data.CategoryOrder);
                //                    $('#EditCategory').dialog('close');
                if (data == "basarili") {


                    $(this).hide();
                    $("#PhoneActivation-wrapper").slideUp();
                    $("#noActivationWarm").slideUp();
                    $("#successMessage").slideDown();
                    $("#errorMessage").slideUp();

                }
                else {
                    $("#redirectbtn").show();
                    $("#errorMessage").slideDown();
                    $("#successMessage").slideUp();
                    $("#activationcode").val("");


                }
                //                $('[data-rel="memberEmail').validationEngine('showPrompt', 'E-Posta Adresiniz Sistemde Kayıtlıdır.', 'red');
                //                $('[data-rel="form-submit"]').addClass('disabled');
            }
            else {
                $('#mailinfo').css('display', 'inline-table');
                //setTimeout("window.location.href = '/Uyelik/HizliUyelik/UyelikTipi-0'", 6000);

                //                  $('#mailname').css('display', 'table');
                //                  $('#mailsurname').css('display', 'table');
                //                  $('#mailtelephone').css('display', 'table');
                //                  $('#mailtitle').css('display', 'table');
                //                  $('#maildescription').css('display', 'table');

                //$('[data-rel="form-submit"]').removeClass('disabled');
            }
        }, error: function (request, status, error) {

        }
    });

}
function PageChange(p, pd) {
    $(this).addClass('active')
    $('#spanLoading').show();
    $.ajax({
        type: 'post',
        url: '/StoreProfile/ProductPaging',
        data: { page: p, displayType: $('#hdnDisplayType').val(), storeId: $('#hiddenStoreId').val(), pageDimension: pd, CategoryId: $('#hdnCategoryId').val() },
        success: function (data) {
            $('#spanLoading').hide();
            var id = ($('#hdnDisplayType').val() == 2 ? "#liste" : "#pencere")
            $('.tab-content ' + id).html(data);
            $('html, body').animate({ scrollTop: 150 }, 500);
        },
        error: function (x, y, z) {
            alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
        }
    });

}
function PageChangeNew(p, pd) {
    $(this).addClass('active')
    $('#spanLoading').show();
    $.ajax({
        type: 'post',
        url: '/StoreProfileNew/ProductPaging',
        data: { page: p, displayType: $('#hdnDisplayType').val(), storeId: $('#hiddenStoreId').val(), pageDimension: pd, CategoryId: $('#hdnCategoryId').val() },
        success: function (data) {
            $('#spanLoading').hide();
            var id = ($('#hdnDisplayType').val() == 2 ? " #liste" : " #pencere")
            $('.tab-content' + id).html(data);
            $('html, body').animate({ scrollTop: 150 }, 500);
        },
        error: function (x, y, z) {
            alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
        }
    });

}

//function check() {
//    if ($('#Member_MemberPassword').val() != $('#OldMemberPassword').val()) {
//        alert("Girmiş olduğunuz şifre, sistemimizde kayıtlı şifrenizle uyuşmamaktadır.");
//        return false;
//    }
//    else if ($('#Member_MemberEmailAgain').val() != $('#Member_NewEmail').val()) {
//        alert("Girmiş olduğunuz E-Posta adresleri uyuşmamaktadır.");
//        return false;
//    }
//    else {
//        return true;
//    }
//}

var login = function (response) {
    $.ajax({
        type: "post",
        url: '/SocialMembership/Membership',
        data: JSON.stringify({
            "model": {
                "MemberName": response.first_name,
                "MemberSurname": response.last_name,
                "MemberEmail": response.email,
                "MemberEmailAgain": response.email,
                "LocalityId": "0",
                "TownId": "0",
                "CityId": "0",
                "CountryId": "0",
                "AddressTypeId": "0"
            },
            "MembershipType": "5",
            "profileId": response.id
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg)
                window.location.replace(window.location.href);
        },
        error: function () {

        }

    });
}



function scrollGoto(element) {
    var _offset = $(window).width() > 1200 ? 250 : 85;
    $('html,body').stop().animate({ scrollTop: $(element).offset().top - _offset }, 500);
}



var toggleResponsiveMenu = (function () {

    var $body = $('body'),
        $responsiveMenu = $('.responsive-menu'),
        $responsiveOverlay = $('.responsive-overlay'),
        pushSize = 290;


    function init() {
        clickEvent();
    }


    function clickEvent() {
        $('.js-hamburger').on('click', function () {


            if (!$body.hasClass('is-pushed')) {
                open($(this));
            } else {
                hide($(this));
            }
        });
    }

    function open(button) {
        $body.addClass('is-pushed').css({ 'width': $(window).width() });
        $responsiveMenu.addClass('is-active');
        $responsiveOverlay.addClass('is-active');

        $(button).children('.header__mobile-menu-icon').addClass('icon-close').removeClass('icon-menu');

        $responsiveMenu.animate({
            left: '0px'
        }, 200),
        $body.animate({
            left: '290px'
        }, 200);
    }

    function hide(button) {
        $body.removeClass('is-pushed');
        $responsiveMenu.removeClass('is-active');
        $responsiveOverlay.removeClass('is-active');

        $(button).children('.header__mobile-menu-icon').addClass('icon-menu').removeClass('icon-close');

        $responsiveMenu.animate({
            left: '-290px'
        }, 200),
        $body.animate({
            left: '0px'
        }, 200, function () {
            $body.css({ 'width': '' });
        })
    }

    return {
        init: init,
        hide: hide
    }
})();

function setOwlStageHeight(event) {
    var maxHeight = 0;
    $('.owl-item.active').each(function () { // LOOP THROUGH ACTIVE ITEMS
        var thisHeight = parseInt($(this).height());
        maxHeight = (maxHeight >= thisHeight ? maxHeight : thisHeight);
    });
    $('.owl-carousel').css('height', maxHeight);
    $('.owl-stage-outer').css('height', maxHeight); // CORRECT DRAG-AREA SO BUTTONS ARE CLICKABLE
}


function showSubCategory() {
    $('.subCategory .showAllSub').on('click', function () {
        var that = $(this);
        var text = that.children('b');
        var icon = that.children('span');
        text.html(text.html() == "Tümünü Gör" ? "Tümünü Gizle" : "Tümünü Gör");
        icon.toggleClass("icon-fill-up-arrow icon-fill-down-arrow");
        that.closest('.result-category__item').toggleClass('expanded');
    });
}


function bottomSlider() {
    if ($('.js-bottom-slider').length > 0) {
        $('.js-bottom-slider').owlCarousel({
            loop: true,
            margin: 10,
            nav: false,
            dots:false,
            autoHeight: true,
            autoHeightClass: 'owl-height',
            autoplay:false,
            autoplayTimeout:5000,
            autoplayHoverPause:true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 4
                }
            }
        });
    }
}


$(document).ready(function () {
    var senderMainPartyId;
    var activationCode = "";
    mainMenuColorChange();
    facebookLoginClickEvent();
    pagerGoto();
    // productImageResize();
    mailActivationValidate();
    checkitem();
    siteChangeVideo();
    //moreLessLink();
    HariciLinkler();
    toggleResponsiveMenu.init();
    showSubCategory();
    bottomSlider();



    //kategori filtre alanı
    if ($('.leftSideBar').length > 0) {
        $('.leftSideBar').theiaStickySidebar({
            additionalMarginTop: 60
        });
    }

    // Ürün detay firma bilgileri alanı
    if ($('.rightSidebar').length > 0) {
        $('.rightSidebar').theiaStickySidebar({
            additionalMarginTop: 60
        });
    }


    //Sektör sayfası 
    if ($('.sidebarBanner').length > 0) {
        $('.sidebarBanner').theiaStickySidebar({
            additionalMarginTop: 60
        });
    }


    if (location.search.split("?tab=")[1] == "video") {
        $('.urun-aciklama [href="#video"]').trigger('click');
        var body = $("html, body");
        body.stop().animate({ scrollTop: $('.urun-aciklama [href="#video"]').offset().top - 100 }, 500, 'swing');

    }


    $('.js-toggle-filter, .js-close-filters').on('click', function () {
        $('.leftSideBar .filters').toggle();
        $('body').toggleClass('is-fixed');
    });


    $('select.mobile-sort').change(function () {
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();

        location.href = valueSelected;
    });

    if ($('.js-sector-firm-slider').length > 0) {
        $('.js-sector-firm-slider').owlCarousel({
            loop: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
            margin: 10,
            nav: true,
            autoplay: true,
            items: 9,
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
                    items: 9
                }
            }
        })
    }


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



    $('.showAllSubCat').on('click', function () {
        $(this).nextAll('.subCatNone').show();
        $(this).hide();
        $('.hideAllSubCat').show();
    });

    $('.hideAllSubCat').on('click', function () {
        $(this).prevAll('.subCatNone').hide();
        $(this).hide();
        $('.showAllSubCat').show();
    });


    $('.js-topmenu-item').on('mouseenter', function () {
        $(this).children('div').show();
    }).on('mouseleave', function () {
        $(this).children('div').hide();
    })

    $('.responsive-overlay').on('click', function () {
        $('.js-hamburger').trigger('click');
    });

    $('.js-scroll-top').on('click', function () {

        $("html, body").animate({
            scrollTop: 0
        }, "slow");
    });

    $(".sector-category-nav__item").on('click', function () {
        var _ind = $(this).attr('data-role').split('anchor-')[1];
        scrollGoto($('[data-role="anchor-' + _ind + '-scroll"]'));

        $(".sector-category-nav__item").removeClass('is-active');
        $(this).addClass('is-active');

        //$('.sector-category-nav-select option[value="' + $(this).attr('data-role') + '"]').prop('selected', true);

    });

    //$(".sector-category-nav__item").eq(0).is(':visible').trigger('click');


    $('.sector-category-nav-select').on('change', function () {
        var _ind = $(this).val().split('anchor-')[1];
        scrollGoto($('[data-role="anchor-' + _ind + '-scroll"]'));
        $('.sector-category-nav__item.' + $(this).val()).trigger('click');
    });


    $(window).scroll(function () {
        var _top = $(this).scrollTop();

        if (_top >= 125) {
            $('.sector-category-nav').addClass('is-fixed');//.css({ 'position': 'fixed', 'top': '70px' });
            $('.sector-category-nav-scroll-fix').show();
        } else {
            $('.sector-category-nav').removeClass('is-fixed');//.css({ 'position': 'relative', 'top': '0px' });
            $('.sector-category-nav-scroll-fix').hide();
        }


        $(".sector-category-nav__item").each(function () {

            var _offset = $(window).width() > 1200 ? 250 : 85;

            var currLink = $(this);
            var _ind = $(this).attr('data-role').split('anchor-')[1];
            var refElement = $('[data-role="anchor-' + _ind + '-scroll"]');
            if (refElement.parent().offset().top <= (_top + _offset) &&
                refElement.parent().offset().top + refElement.parent().height() > _top) {
                $(".sector-category-nav__item").removeClass("is-active");
                currLink.addClass("is-active");

                //$('.sector-category-nav-select option[value="' + $(this).attr('data-role') + '"]').prop('selected', true);

            }
            else {
                currLink.removeClass("is-active");
            }
        })



    });

    $(window).resize(function () {
        var _width = $(this).width();
        $(".sector-category-nav__item.is-active").trigger('click');
        //console.log(_width);
    });


    function toggleIcon(e) {
        $(e.target)
            .prev('.sector-category-list__big-title')
            .find("[role='button']").children('span')
            .toggleClass('icon-down-arrow icon-up-arrow');
    }
    $('.sector-category-list').on('hidden.bs.collapse', toggleIcon);
    $('.sector-category-list').on('shown.bs.collapse', toggleIcon);

    $('.sector-category-list').on('shown.bs.collapse', function (e, i) {
        var _offset = $(window).width() > 768 ? 55 : 0;
        var $panel = $(e.target).closest('.panel');
        $('html,body').animate({
            scrollTop: $panel.offset().top - _offset
        }, 250);
    });


    function toggleFilterIcon(e) {
        $(e.target)
            .prev('.panel-heading')
            .find("[role='button']").children('span')
            .toggleClass('icon-down-arrow icon-up-arrow');
    }
    $('.pos-absolute__inner').on('hidden.bs.collapse', toggleFilterIcon);
    $('.pos-absolute__inner').on('shown.bs.collapse', toggleFilterIcon);



    $(window).scroll(function () {
        var top = $(this).scrollTop();

        if (top >= 125) {
            $('.js-sticky-header').show();
        } else {
            $('.js-sticky-header').hide();
        }


        if (top >= 500) {
            $('.js-scroll-top').show();
        } else {
            $('.js-scroll-top').hide();
        }


    })

    $("#facebookLogin").click(function () {
        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                FB.api('/me?fields=id,email,name,first_name,last_name,age_range,gender,locale,picture,verified', login, { scope: 'email,public_profile' });
            }
            else {
                FB.login(function (response) {
                    if (response.authResponse) {
                        var faceMemberToken = response.authResponse.accessToken;
                        var faceProfilId = response.authResponse.userID;
                        FB.api('/me?fields=id,email,name,first_name,last_name,age_range,gender,locale,picture,verified', login,
                        { scope: 'email,public_profile,id,name,first_name,last_name,age_range,gender,locale,picture,verified' });
                    }
                }, { scope: 'email,public_profile' });
            }
        });
    });

    if ($("#country").length > 0) {
        $.getJSON("https://freegeoip.net/json/", function (data) {
            var country = data.country_name;
            var ip = data.ip;
            $("#country").val(country);

        });
    }
    if ($("#productDetail").length > 0) {
        $("#productDetail").validationEngine();
    }

    $("#signUpButton").click(function () {
        var password = $("#MemberPassword").val();
        var email = $("#MemberEmail").val();
        var activeCode = $("#ActivationCodeLogin").val();
        if (activeCode != "") {
            signUpWithActivationCode(activeCode);
        }
        else {
            Signup(email, password);
        }

    });

    $("#categoryName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/StoreSearch/SearchCategory",
                type: "POST",
                dataType: "json",
                data: { categoryName: request.term },
                success: function (data) {

                    console.log(data)

                    response($.map(data, function (item) {
                        return { label: item.Text + " Firmaları", value: item.Text + " Firmaları", id: item.Value }
                    }))
                },
                error: function (x, l, e) {
                    alert(x.responseText);
                }

            })
        }, select: function (event, ui) {
            $('#CategoryId').val(ui.item.id);
        }
    });

    $('.js-firm-category-search').click(function () {

        $.ajax({
            url: "/StoreSearch/CategoryGetUrlName",
            type: "POST",
            data: { categoryName: $('#categoryName').val() },
            success: function (data) {
                $('#categoryUrlName').val(data);
                window.location = '/Sirketler/' + $('#CategoryId').val() + '/' + $('#categoryUrlName').val();
            },
            error: function (x, l, e) {
                alert(x.responseText);
            }
        });
    });
    function isValidEmailAddress(emailAddress) {
        var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
        return pattern.test(emailAddress);
    };

    if ($('#DestinationEmail').length > 0) {
        $('#DestinationEmail').focusout(function () {

            $.ajax({
                url: '/MemberShip/CheckEmailForgetPassword',
                type: "post",
                data: { email: $(this).val() },
                success: function (data) {
                    if (!data) {
                        $("#EmailError").show();
                        $("#SendLinkForgetPassword").attr("disabled", "true");
                    }
                    else {
                        $("#EmailError").hide();
                        $("#SendLinkForgetPassword").removeAttr("disabled");
                    }


                }
            });

        });
    }
    $('[data-rel="Email"]').keyup(function () {


        var isValidate = $(this).validationEngine('validate');
        var memberNo = $('#hiddenMemberNo').val();
        memberNo = memberNo.replace('#', '');
        memberNo = memberNo.replace('#', '');

        var productNo = $('#hiddenProductNo').val()
        productNo = productNo.replace('#', '');


        if (!isValidate) {
            $.ajax({
                url: '/Product/CheckEmail',
                type: "post",
                data: { email: $(this).val(), productNumber: productNo, memberNumber: memberNo },
                success: function (data) {
                    if (data) {
                        $('#mailname').css('display', 'none');
                        $('#mailsurname').css('display', 'none');
                        $('#mailtelephone').css('display', 'none');
                        $('#mailtitle').css('display', 'none');
                        $('#maildescription').css('display', 'none');
                        $('#mailpass').css('display', 'none');
                        $("#mailPhoneOther").css('display', 'none');

                        if (data.FastMembershipType1 == "20") {

                            $("#redirectbtn").hide();
                            $("#mailSuccess").show();
                            $("#signUpButton").show();
                            $("#messageSignWarm").show();



                        }
                        else if (data.FastMembershipType1 == "5" || data.Password) {
                            $("#password-wrapper").show();
                            $("#redirectbtn").hide();
                            $("#signUpButton").show();
                            $("#messageSignWarm").show();

                        }
                        else if (data.FastMembershipType1 == "10") {
                            $("#redirectbtn").hide();
                            $("#facebookLogin").show();
                        }


                        //                $('[data-rel="memberEmail').validationEngine('showPrompt', 'E-Posta Adresiniz Sistemde Kayıtlıdır.', 'red');
                        //                $('[data-rel="form-submit"]').addClass('disabled');
                    }
                    else {
                        $('#mailinfo').show();
                        $("#redirectbtn").html("Devam");
                        //setTimeout("window.location.href = '/Uyelik/HizliUyelik/UyelikTipi-0'", 6000);

                        //                  $('#mailname').css('display', 'table');
                        //                  $('#mailsurname').css('display', 'table');
                        //                  $('#mailtelephone').css('display', 'table');
                        //                  $('#mailtitle').css('display', 'table');
                        //                  $('#maildescription').css('display', 'table');

                        //$('[data-rel="form-submit"]').removeClass('disabled');
                    }
                }
            });
        } else {
            $('#mailname').css('display', 'none');
            $('#mailsurname').css('display', 'none');
            $('#mailtelephone').css('display', 'none');
            $('#mailtitle').css('display', 'none');
            $('#maildescription').css('display', 'none');
            $('#mailpass').css('display', 'none');
            $("#mailPhoneOther").css('display', 'none');
        }
    });


    $("#btnNewActivationCode").click(function () {
        var phoneNumber = $("#PhoneNumberAgain").val().trim();
        var messageId = $("#messageIdHidden").val();
        if (phoneNumber.length > 13) {
            $.ajax({
                url: '/Product/SendMessageErrorAgain',
                dataType: "json",
                type: 'post',
                data:
             {
                 MessageId: messageId,
                 PhoneNumber: phoneNumber

             },
                success: function (data) {
                    if (data.phoneNumber != "") {
                        $("#noActivationWrapper").hide();
                        activationCode = data.ActivationCode;

                        $("#redirectbtn").show();
                        senderMainPartyId = data.MainPartyId;
                        $("#phoneActivationLabel").html("+90" + data.phoneNumber + " numaraya gelen aktivasyon kodunu giriniz:");
                        $("#noActivationWarm").show();
                        $("#PhoneActivation-wrapper").show();
                    }
                    else {
                        alert("Yeniden Kod Gönderilirken Hata Oluştu");
                    }
                },
                error: function (x, l, e) {
                    alert("Yeniden Kod Gönderilirken Hata Oluştu");

                }
            });
        }
        else
        {
            alert("Telefon Numaranızı 10 Karakter Olmalı")
        }
    });
    $("#redirectbtn").click(function () {
        //window.location.href = '/Uyelik/HizliUyelik/UyelikTipi-0';
        var isValidate = $('[data-rel="Email"]').validationEngine('validate');
        if (!isValidate) {
            if ($(this).html() == "Tamamla") {
                var memberEmail = $('#MemberEmail').val();
                memberEmail = $.trim(memberEmail);
                var memberName = $("#MemberName").val();
                memberName = $.trim(memberName);
                var memberSurname = $('#MemberSurname').val();
                memberSurname = $.trim(memberSurname);
                var phoneNumber = $('#PhoneNumber').val();
                phoneNumber = $.trim(phoneNumber);
                var MailTitle = $("#mailTitle").val();
                MailTitle = $.trim(mailTitle);
                var MailDescription = $('#mailDescription').val();
                MailDescription = $.trim(MailDescription);
                var phoneAreaCode = $("#phoneAreaCode").val();
                phoneAreaCode = $.trim(phoneAreaCode);
                var phoneCulture = $("#phoneCulture").val();
                phoneCulture = $.trim(phoneCulture);
                var phoneNumberOther = $("#phoneNumberOther").val();
                phoneNumberOther = $.trim(phoneNumberOther);
                if (memberEmail != "" && memberName != "" && memberSurname != "" && phoneNumber != "" && MailDescription != "") {
                    $(this).html("İşlem Yapılıyor");
                    $.ajax({
                        url: '/Product/FastMember',
                        type: 'post',
                        data: {
                            MemberEmail: $('#MemberEmail').val(),
                            MemberName: $("#MemberName").val(),
                            MemberSurname: $('#MemberSurname').val(),
                            PhoneNumber: $('#PhoneNumber').val(),
                            subject: $("#mailTitle").val(),
                            content: $('#mailDescription').val(),
                            productId: $("#hdnProductId").val(),
                            memberEmail1: $('#hdnMemberEmail').val()
                            //                mailpass
                            //                mailname
                            //                mailsurname
                            //                mailtelephone
                            //                mailtitle
                            //                maildescription
                            //                  MemberName
                            //                  MemberSurname
                            //                  MemberPassword
                            //                  MemberEmail

                            //                CategoryId: $('#Edit_CategoryId').val(),
                            //                  CategoryName: $('#Edit_Name').val(),
                            //                  CategoryOrder: $('#Edit_Order').val()

                            //email: $(this).val(), productNumber: productNo, memberNumber: memberNo 
                        },
                        success: function (data) {
                            if (data) {

                                //                    var _ID = '#' + $('#Edit_CategoryId').val();
                                //                    $(_ID + ' span:first label:first').html(data.CategoryName);
                                //                    $(_ID + ' span:first .sort').html(data.CategoryOrder);
                                //                    $('#EditCategory').dialog('close');
                                $("#messageIdHidden").val(data.MessageId);
                                if (data.MainPartyId == "Exist") {
                                    $("#errorMessage").show();
                                }
                                else {
                                    $('#mailname').css('display', 'none');
                                    $('#mailsurname').css('display', 'none');
                                    $('#mailtelephone').css('display', 'none');
                                    $('#mailtitle').css('display', 'none');
                                    $('#maildescription').css('display', 'none');
                                    $('#mailpass').css('display', 'none');
                                    $("#mailPhoneOther").css('display', 'none');
                                    senderMainPartyId = data.MainPartyId;
                                    $("#phoneActivationLabel").html(data.PhoneNumber + " numaraya gelen aktivasyon kodunu giriniz:");
                                    $("#noActivationWarm").show();
                                    $("#PhoneActivation-wrapper").show();
                                    activationCode = data.ActivationCodePhone;
                                    $("#mail-wrapper").css("display", "none");
                                    $(this).removeAttr("disabled");
                                    $("#redirectbtn").html("Gönder");
                                }
                                //                $('[data-rel="memberEmail').validationEngine('showPrompt', 'E-Posta Adresiniz Sistemde Kayıtlıdır.', 'red');
                                //                $('[data-rel="form-submit"]').addClass('disabled');
                            }
                            else {
                                $('#mailinfo').show();
                                //setTimeout("window.location.href = '/Uyelik/HizliUyelik/UyelikTipi-0'", 6000);

                                //                  $('#mailname').css('display', 'table');
                                //                  $('#mailsurname').css('display', 'table');
                                //                  $('#mailtelephone').css('display', 'table');
                                //                  $('#mailtitle').css('display', 'table');
                                //                  $('#maildescription').css('display', 'table');

                                //$('[data-rel="form-submit"]').removeClass('disabled');
                            }
                        }
                    });
                }
            }
            else if ($(this).html() == "Gönder") {
                var activationCodeGet = $("#activationcode").val();
                activationCodeGet = $.trim(activationCodeGet);
                if (activationCodeGet != "") {
             
                    if (activationCode == activationCodeGet) {
                        SendMessagePopup(senderMainPartyId, 1, activationCodeGet);
                        $(this).hide();
                    }
                    else {
                        SendMessagePopup(senderMainPartyId, 0, activationCodeGet);
                        $(this).hide();
                    }
                }
            }
            else {
                var memberEmail = $('#MemberEmail').val();
                memberEmail = $.trim(memberEmail);
                if (!isValidEmailAddress(memberEmail)) {
                    alert("Email adresinizi doğru giriniz.");
                }
                else {
                    $(this).html("Tamamla");
                    $('#mailname').show();
                    $('#mailsurname').show();
                    $("#mailtelephone").show();
                    $('#mailtitle').show();
                    $('#maildescription').show();
                    $('#mailpass').show();

                }

            }
        }
        else {

            $(this).html("Devam");
            $('#mailname').css('display', 'none');
            $('#mailsurname').css('display', 'none');
            $('#mailtelephone').css('display', 'none');
            $('#mailtitle').css('display', 'none');
            $('#maildescription').css('display', 'none');
            $('#mailpass').css('display', 'none');
            $("#mailPhoneOther").css('display', 'none');

        }
    });


    if ($('.account-change-email').length > 0) {
        //$('#formChangeEMail').validate({
        //    errorClass: 'membershipValid',
        //    errorElement: 'div',
        //    messages: {
        //        "NewEmail": ''
        //    }
        //});
        $.metadata.setType('attr', 'validate');
    }


    if ($('#productDetail').length > 0) {
        $('#mailname').css('display', 'none');
        $('#mailsurname').css('display', 'none');
        $('#mailtelephone').css('display', 'none');
        $('#mailtitle').css('display', 'none');
        $('#maildescription').css('display', 'none');
        $('#mailinfo').css('display', 'none');
        $('#mailpass').css('display', 'none');
        $('#mailPhoneOther').css("display", "none");
        $('#searchPurchaseAdvert').attr('class', 'searchMenuActive');
        $('#hdnTopSearchType').val('2');
        $('#searchSpan').html('Ürün Arama :');
    }

    $("#SendLoginActivate").click(function () {
        SendActivationSms();
    });
    $('#map').on('click', function () {
        var mapFrameSrc = $('#hiddenMapSrc').val();
        $('#mainMap').attr('src', mapFrameSrc);
        $('#mainMap').hide().show();
    });
    $('[data-rel="print"]').click(function () {
        window.print();
    });
    $('[data-rel="ReadyForSaleSubmit"]').click(function () {
        $('[data-rel="ReadyForSale"]').submit();
    });


    if ($('.store-detail__product').length > 0) {
        $('[data-toggle="tab"]').click(function () {
            var type = $(this).attr('href').toString().replace('#', '');
            switch (type) {
                case 'liste':
                    $('#hdnDisplayType').val('2')
                    break;
                case 'pencere':
                    $('#hdnDisplayType').val('1')
                    break;
                default:
            }
        });
    }

    $("a[data-toggle=tooltip]").tooltip();

    $('[data-toggle="tab"]').on("click", function () {
        "LI" == $(this).parent().prop("tagName") ? ($(this).parent().siblings().find("*").removeClass("active"),
        $(this).parent().siblings().removeClass("active"),
        "LI" == $(this).parent().parent().parent().prop("tagName") && ($(this).parent().parent().parent().siblings().removeClass("active"),
        $(this).parent().parent().parent().addClass("a"))) : ($(this).siblings().removeClass("active"),
        $(this).addClass("active"))
    }),
    $('.panel-title a[data-toggle="collapse"], .navbar-toggle[data-toggle="collapse"]').on("click", function () {
        $($(this).attr("href")).hasClass("in") ? ($(this).closest(".panel-group").find(".glyphicon-chevron-down").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down"),
        $(this).children(".glyphicon-chevron-up").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down")) : ($(this).closest(".panel-group").find(".glyphicon-chevron-up").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down"),
        $(this).children(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up"))
    }),
    $('[data-toggle="tooltip"]').tooltip(),

    $(".list-group-item").hover(function () {
        if ($(this).children(".submenu").length) {
            $(this).children(".submenu").css("top", 0),
            $(this).children(".submenu").css("left", $(this).parent().width() + 17),
            $(this).children(".submenu").show();
            var a = parseInt($(document).scrollTop()) + parseInt($(window).height())
              , b = $(this).children(".submenu").height() + $(this).children(".submenu").offset().top;
            b > a && $(this).children(".submenu").css("top", -1 * (b - a))
        }
    }, function () {
        $(this).children(".submenu").hide()
    }),
    $(".hidden-item-container").on({
        mouseenter: function () {
            $(this).find(".hidden-item").show()
        },
        mouseleave: function () {
            $(this).find(".hidden-item").hide()
        }
    });

    if ($(".tooltips").length > 0) {
        $(".tooltips").tooltip();
    }

    if ($(".popovers").length > 0) {
        $(".popovers").popover({
            html: !0,
            trigger: "manual"
        }).click(function (a) {
            $(".popovers").not(this).popover("hide"),
            $(this).popover("show"),
            a.stopPropagation()
        });
    }


    $("html").click(function () {
        $(".popovers").popover("hide")
    });

    $(".dropdown-dynamic li a").click(function () {
        var a = $(this).text()
          , b = " <span class='caret'></span>";
        $(this).html($(this).parent().parent().prev(".btn:first-child").text().replace(b, "")),
        $(this).parent().parent().prev(".btn:first-child").html(a + b)
    });

    if ($(".summernote").length > 0) {
        $(".summernote").summernote({
            height: 150
        });
    }

    $('#videocontent').on('hidden.bs.modal', function () {
        videoPause();
    })

    $("img.img-zoom").each(function () {
        $(this).wrap('<span style="display:inline-block; width: ' + $(this).width() + "px; height: " + $(this).height() + 'px;"></span>').css("display", "block").parent().zoom()
    });

    $(".modal").on("shown.bs.modal", function () {
        $(this).find("img.lazy").filter(':not([data-src=""])').each(function () {
            $(this).attr("src", $(this).data("src")),
            $(this).attr("data-src", "")
        })
    });

    $('a[data-toggle="tab"]').on("shown.bs.tab", function () {
        $($(this).attr("href")).find("img.lazy").filter(':not([data-src=""])').each(function () {
            $(this).attr("src", $(this).data("src")),
            $(this).attr("data-src", "")
        })
    });

    $(".prev-tab").on("click", function () {
        var a = $(this).parent().parent().find(".active").prev().attr("id");
        $('[href="#' + a + '"]').tab("show")
    });

    $(".next-tab").on("click", function () {
        var a = $(this).parent().parent().find(".active").next().attr("id");
        $('[href="#' + a + '"]').tab("show")
    });

    if ($(window).width() < 775 && (".urun-resim img").length > 0) {

        //$(".urun-resim a img").each(function () {
        //    var lastW = $(this).attr("src");
        //    var newW = lastW.replace("160x120", "400x300");
        //    $(this).attr("src", newW);
        //});
        //$(".urun-resim").css("margin-bottom", "10px");
        //if ($(".top-seller-list__image img").length > 0) {
        //    $(".top-seller-list__image img").each(function () {
        //        var lastW1 = $(this).attr("src");
        //        var newW1 = lastW1.replace("100x75", "400x300");
        //        $(this).attr("src", newW1);
        //    });
        //}
    }


});

$(window).resize(function () {

    if ($(window).width() < 775 && (".urun-resim img").length > 0) {

        $(".urun-resim a img").each(function () {
            var lastW = $(this).attr("src");
            var newW = lastW.replace("160x120", "400x300");
            $(this).attr("src", newW);

        });
        $(".urun-resim").css("margin-bottom", "10px");
        if ($(".top-seller-list__image img").length > 0) {
            $(".top-seller-list__image img").each(function () {
                var lastW1 = $(this).attr("src");
                var newW1 = lastW1.replace("100x75", "400x300");
                $(this).attr("src", newW1);
            });

        }

    }
    else {
        $(".urun-resim a img").each(function () {
            var lastW = $(this).attr("src");
            var newW = lastW.replace("400x300", "160x120");
            $(this).attr("src", newW);

        });
        $(".urun-resim").css("margin-bottom", "0px");
        if ($(".top-seller-list__image img").length > 0) {
            $(".top-seller-list__image img").each(function () {
                var lastW1 = $(this).attr("src");
                var newW1 = lastW1.replace("400x300", "100x75");
                $(this).attr("src", newW1);
            });

        }
    }
});
//video
//Ahmet.js değişikler 

function PopupCenter(url, title, w, h) {
    //AdilD
    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;
    var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    // Puts focus on the newWindow
    if (window.focus) {
        newWindow.focus();
    }
}



/*ERS TEKNOLOJİ*/



var owlThumb = $('#kresim2');
$.each(owlThumb, function () {
    var item = $(this)

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
    }
});


var owlOwerflowed = $('.overflowdouble');
if (owlOwerflowed != undefined) {
    owlOwerflowed.owlCarousel({
        margin: 0,
        loop: true,
        nav: false,
        autoplay: true,
        autoplayTimeout: 5000,
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
    })

    $('.overflow-carousel:not(.owlTemplate) .overflow-next').click(function () {
        owlOwerflowed.trigger('next.owl.carousel');
    });
    $('.overflow-carousel:not(.owlTemplate) .overflow-prev').click(function () {
        owlOwerflowed.trigger('prev.owl.carousel', [300]);
    });

}

var owlOwerflowedSelectedCategories = $('.overflowdouble2');
if (owlOwerflowedSelectedCategories != undefined) {
    owlOwerflowedSelectedCategories.owlCarousel({
        margin: 0,
        loop: true,
        nav: false,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            1000: {
                items: 2
            }
        }
    })

    $('.overflow-carousel-selected-categories:not(.owlTemplate) .overflow-next').click(function () {
        owlOwerflowedSelectedCategories.trigger('next.owl.carousel');
    });
    $('.overflow-carousel-selected-categories:not(.owlTemplate) .overflow-prev').click(function () {
        owlOwerflowedSelectedCategories.trigger('prev.owl.carousel', [300]);
    });


}


var innerSlider = $('.innerSlider');
if (innerSlider != undefined) {
    innerSlider.owlCarousel({
        margin: 0,
        loop: false,
        nav: false,
        autoplay: false,
        lazyLoad: true,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    })

    $('.overflow-carousel-selected-categories:not(.owlTemplate) .overflow-next').click(function () {
        innerSlider.trigger('next.owl.carousel');
    });
    $('.overflow-carousel-selected-categories:not(.owlTemplate) .overflow-prev').click(function () {
        innerSlider.trigger('prev.owl.carousel', [300]);
    });


}





$('[data-toggle="tab"]').on('show.bs.tab', function (e) {
    //e.target // newly activated tab
    //e.relatedTarget // previous active tab
    var t = $(e.target).attr("data-target");
    $(t).css({ "visibility": "hidden" })
    setTimeout(function () {
        $(t).css({ "visibility": "visible" })
    }, 400)
})

$(document).on("click", "[data-toggle=showsearch]", function () {

    $(".mobilesearch").show();

});
$(document).on("click", "[data-toggle=closesearch]", function () {

    $(".mobilesearch").hide();

});

$(window).on("resize", function () {
    var w = $(window).width();
    if (w < 768) {
        // $("#mobileswitcharea").addClass("collapse");

    } else {
        // $("#mobileswitcharea").removeClass("collapse");
    }
})



var ClonedCategories = $("#SectorsCategories [role=menubar]").clone();
var DownIcon = $("<i class='icon-down-arrow'></i>");
$(DownIcon).attr("data-toggle", "setactive");
$(DownIcon).addClass("menutoggleicon");

$(ClonedCategories).children("li").children("a").append(DownIcon)

$("#MobileCategoriesContainer").append(ClonedCategories);
$("#MobileCategoriesContainer>a").append($(DownIcon).clone())

$("#MobileCategoriesContainer>a>i").on("click", function () {
    if ($(this).parent().parent().hasClass("active")) {
        $(this).parent().parent().removeClass("active");
    } else {
        $(this).parent().parent().parent().find(".active").removeClass("active");
        $(this).parent().parent().addClass("active");
    };
    return false;
})

$(document).on("click", ".menutoggleicon", function () {
    // $(ClonedCategories).find(".active").removeClass("active"); 
    if ($(this).parent().parent().hasClass("active")) {
        $(this).parent().parent().removeClass("active");

    } else {
        $(this).parent().parent().parent().find(".active").removeClass("active");
        $(this).parent().parent().addClass("active");
    }
    return false;
})



var ClonedCategories2 = $("#CustomerCategories [role=menubar]").clone();
$("#MobileCustomersContainer").children("a").append($(DownIcon).clone());
//var DownIcon = $("<i class='fa fa-chevron-down'></i>");
//$(DownIcon).attr("data-toggle", "setactive");
//$(DownIcon).addClass("menutoggleicon");

//$(ClonedCategories).children("li").children("a").append(DownIcon)

$("#MobileCustomersContainer").append(ClonedCategories2);
//$("#CustomerCategories>a").append($(DownIcon).clone())

//$("#CustomerCategories>a>i").on("click", function () {
//    if ($(this).parent().parent().hasClass("active")) {
//        $(this).parent().parent().removeClass("active");
//    } else {
//        $(this).parent().parent().addClass("active");
//    };
//    return false;
//})

//$(document).on("click", ".menutoggleicon", function () {
//    // $(ClonedCategories).find(".active").removeClass("active");

//    if ($(this).parent().parent().hasClass("active")) {
//        $(this).parent().parent().removeClass("active");
//    } else {
//        $(this).parent().parent().addClass("active");
//    }
//    return false;
//})



var ClonedCustomers = $("#CustomerCategories [role=menubar]").clone();

//$(ClonedCustomers).children("li").children("a").attr("data-toggle", "setactive");
//$("#MobileCustomersContainer").append(ClonedCategories);

//$("#MobileCustomersContainer>a").on("click", function () {
//    if ($(this).parent().hasClass("active")) {
//        $(this).parent().removeClass("active");
//    } else {
//        $(this).parent().addClass("active");
//    };
//    return false;
//})

////$(document).on("click", "[data-toggle=setactive]", function () {
////    // $(ClonedCategories).find(".active").removeClass("active");

////    if ($(this).parent().hasClass("active")) {
////        $(this).parent().removeClass("active");
////    } else {
////        $(this).parent().addClass("active");
////    }
////    return false;
////})



//$("input[name='SearchText']").autocomplete({ 
//    minLength: 3,
//    source: function (request, response) {
//        $.ajax({
//            url: "/Product/AutoComplete",
//            type: "POST",
//            dataType: "json",
//            data: { term: request.term },
//            success: function (data) {
//                response($.map(data, function (item) {
//                    return {
//                        label: item.text,
//                        value: item.text,
//                        // id: item.id
//                    };
//                }))
//            }
//        })
//    },
//    select: function (event, ui) {
//        window.location.href = '/Urunler/AramaKelimesi?SearchText=' + ui.item.value;
//    }

//}).data("ui-autocomplete")._renderItem = function (ul, item) {
//    var inner_html = item.label;
//    ul.addClass("");
//    return $("<div class=''></div>")
//            .data("item.autocomplete", item)
//            .append(inner_html)
//            .appendTo(ul)
//};


$(document).ready(function () {
    $('#StoreProfileNewPopularProducts').owlCarousel({

        lazyLoad: true,

        loop: true,

        margin: 10,


        responsive: {
            0: {
                items: 2,
                nav: true,
                navText: ["", ""],
                rewindNav: true,
            },
            600: {
                items: 3,
                nav: true,
                navText: ["", ""],
                rewindNav: true,
            },
            1000: {
                items: 4,
                nav: true,
                navText: ["", ""],
                rewindNav: true,
            }
        }
    });

    $('#StoreProfileImageSlider').owlCarousel({

        lazyLoad: true,

        loop: true,

        margin: 10,

        nav: true,

        responsive: {
            0: {
                items: 1,
                nav: true,
                navText: ["", ""],
                rewindNav: true,
            },
            600: {
                items: 1,
                nav: true,
                navText: ["", ""],
                rewindNav: true,
            },
            1000: {
                items: 1,
                nav: true,
                navText: ["", ""],
                rewindNav: true,
            }
        }
    });

 

    $(".menuarrow").on("click", function () {
        $(".sub-menu-item .arrowActive").removeClass("arrowActive");
        $(".sub-menu-item .sub-menu").slideUp();
        $(this).toggleClass("arrowActive");
        
        $(this).siblings("ul").slideToggle();
        
    });
 
});
