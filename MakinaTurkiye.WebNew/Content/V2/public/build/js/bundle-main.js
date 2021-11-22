/*
    Ana Menu Üst Menüye Göre Renk Değişimi
*/
/**Kategori image lazyloading */

/**kategori image lazy loading finish */





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


function FacebookAjaxResponse(response) {

    $.ajax({
        async: true,
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
        success: function (msg) {
            if (msg)
                window.location.href = window.location.origin;
        },
        error: function (jqXHR, exception) {
            console.error('error tanımlandı');
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

                FB.api('/me?fields=id,email,name,first_name,last_name,age_range,gender,locale,picture,verified&access_token=' + response.authResponse.accessToken, FacebookAjaxResponse, { scope: 'email,public_profile' });
            }
            else {
                FB.login(function (response) {

                    if (response.authResponse) {

                        var faceMemberToken = response.authResponse.accessToken;
                        var faceProfilId = response.authResponse.userID;
                        FB.api('/me?fields=id,email,name,first_name,last_name,age_range,gender,locale,picture,verified&access_token' + response.authResponse.accessToken, FacebookAjaxResponse,
                            { scope: 'email,public_profile,id,name,first_name,last_name,age_range,gender,locale,picture,verified' });
                    }
                }, { scope: 'email,public_profile' });
            }
        });
    });
}

/*Listeden Firma Ara Seç*/
function selectStoreSearch() {
    $(".navbar-form").attr("action", "https://www.makinaturkiye.com/Sirketler?SearchType=3&amp;");
}

/*Listeden Ürün Ara Seç*/
function selectProductSearch() {
    $(".navbar-form").attr("action", "https://www.makinaturkiye.com/kelime-arama?CategoryId=0");
}

function selectVideoSearch() {
    $(".navbar-form").attr("action", "https://www.makinaturkiye.com/Videolar/AramaSonuclari?CategoryId=0");
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
    $("#FavoriteProductLoading").show();
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('Görüntülemiş olduğunuz ürün favori listenize ekleniyor.. &nbsp;&nbsp;' + image);
    $.ajax({
        url: '/ajax/AddFavoriteProduct',
        type: 'get',
        data:
            {
                ProductId: id
            },
        success: function (data) {
            if (data == true) {
                //                        $.facebox('Görüntülemiş olduğunuz ürün başarıyla favori ürünlerinize eklenmiştir.');
                $('#aRemoveFavoriteProduct').show();
                $('#aAddFavoriteProduct').hide();
                $("#FavoriteProductLoading").hide();
                //                        $('#divFavroriteProductImage').css('background-image', 'url(/Content/Images/removeFavorite.png)');
            }
            else {
                $("#FavoriteProductLoading").hide();
                //  $.facebox('Görüntülemiş olduğunuz ürünü favori ürünlerinize eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.');
                window.location.href = 'https://www.makinaturkiye.com/Uyelik/kullanicigirisi';
            }
        },
        error: function (x, l, e) {
            $("#FavoriteProductLoading").hide();
            $.facebox('Görüntülemiş olduğunuz ürünü favori ürünlerinize eklerken bir sorun oluştu.');
        }
    });
}

function RemoveFavoriteProduct(id) {
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('Görüntülemiş olduğunuz ürün favori listenizden çıkarılıyor.. &nbsp;&nbsp;' + image);

    $.ajax({
        url: '/ajax/RemoveFavoriteProduct',
        type: 'get',
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
    obj.redirect = 'https://www.makinaturkiye.com/Account/Message/Index?MessagePageType=1&UyeNo=' + memberNo + '&UrunNo=' + productNo;
    $.ajax({
        url: 'https://www.makinaturkiye.com/ajax/IsAuthenticate',
        data: obj,
        type:'get',
        success: function (data) {
            if (data == true) {
                var memberNo = $('#hiddenMemberNo').val();
                memberNo = memberNo.replace('#', '');
                memberNo = memberNo.replace('#', '');
                var productNo = $('#hiddenProductNo').val()
                productNo = productNo.replace('#', '');

                //                                                window.location.href = '/Mesaj/UyeNo=' + memberNo + '/UrunNo=' + productNo;
                window.location.href = 'https://www.makinaturkiye.com/Account/Message/Index?MessagePageType=1&UyeNo=' + memberNo + '&UrunNo=' + productNo;

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
            MailActivationValue: $("#mailActivationValue").val(),
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
                    $("#redirectbtn").hide();
                }

                else if (data == "hatali") {
                    $("#redirectbtn").show();
                    $("#errorMessage").slideDown();
                    $("#successMessage").slideUp();
                    $("#activationcode").val("");


                }
                else if (data = "mailactivation") {
                    $("#redirectbtn").hide();
                    $("#activationcode").val("");
                    $("#mailActivationSuccess").show();
                    $("#noActivationWarm").hide();

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
            //console.log(data);


            $('#spanLoading').hide();
            //var id = ($('#hdnDisplayType').val() == 2 ? " #liste" : " #pencere")
            $('.store-product-container').html(data);
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


function hasVideoLinkClick() {
    $('.js-has-video-link').on('click', function (e) {
        e.preventDefault();
        window.location.href = $(this).attr('data-url');
    })
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
    $('.subCategory .showAllSub').unbind().click(function () {

        var that = $(this);
        var text = that.children('b');
        var icon = that.children('span');

       // that.children('b').html(that.children('b').html() == "Tümünü Gör" ? "Tümünü Gizle" : "Tümünü Gör");
        //icon.toggleClass("icon-fill-up-arrow icon-fill-down-arrow");

        if (text.html() == "Tümünü Gör") {
            that.closest('.result-category__item').addClass('expanded');
            text.html("Tümünü Gizle");
            icon.attr("class", "icon-fill-up-arrow");

        }
        else {
            that.closest('.result-category__item').removeClass('expanded');
            text.html("Tümünü Gör");
            icon.attr("class", "icon-fill-down-arrow");
        }

    });
}


function bottomSlider() {
    if ($('.js-bottom-slider').length > 0) {
        $('.js-bottom-slider').owlCarousel({
            loop: true,
            margin: 10,
            nav: false,
            dots: false,
            autoHeight: true,
            autoHeightClass: 'owl-height',
            autoplay: false,
            autoplayTimeout: 5000,
            autoplayHoverPause: true,
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

/*
Firma sayfasında sayfanın belli bir miktar aşağı kayması işini yapar
*/
function storePageScroll() {
    if ($('.StoreProfileAvatar').length > 0) {
        var body = $("html, body");
        setTimeout(function () {
            body.stop().animate({ scrollTop: $('.StoreProfileAvatar').offset().top - 75 }, 500, 'swing');
        }, 1000);

    }
}
//function CategorySectorWithAjax(CategoryId,SelectedBrandId,SelectedCountryId,SelectedCityId,SelectedLocalityId,) {

//    $.ajax({
//        type: 'post',
//        url: '/Category/GetStoresByCategoryId',
//        data: { selectedCategoryId: CategoryId, selectedBrandId: SelectedBrandId, selectedCountryId: SelectedCountryId, selectedCityId: SelectedCityId, selectedLocalityId: SelectedLocalityId },
//        success: function (data) {
//            $('.storesContent').html(data);
//            if ($('.js-sector-firm-slider').length > 0) {
//                $('.js-sector-firm-slider').owlCarousel({
//                    loop: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
//                    margin: 10,
//                    nav: true,
//                    autoplay: true,
//                    items: 9,
//                    autoplayHoverPause: true,
//                    autoplaySpeed: 1000,
//                    autoHeight: true,
//                    navText: ["", ""],
//                    center: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
//                    responsive: {
//                        0: {
//                            items: 2
//                        },
//                        768: {
//                            items: 4
//                        },
//                        991: {
//                            items: 9
//                        }
//                    }
//                })
//            }
//        }
//    });
//}

function categorybannerSlider() {

    var $homebannerBig = $('.category-banner-carousel');

    var syncedSecondary = true;
    $homebannerBig.owlCarousel({
        items: 1,
        loop: true,
        autoplay: 1,
        autoplayTimeout: 9000,
        autoplayHoverPause: true,
        //animateOut: 'fadeOut',
        //animateIn: 'fadeIn',
        lazyLoad: true,
        margin: 0,
        dots: true,
        touchDrag: true,
        responsiveRefreshRate: 200,
        mouseDrag: false,

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
            '<i class="fa fa-chevron-left" aria-hidden="true"></i>',
            '<i class="fa fa-chevron-right" aria-hidden="true"></i>'
        ]
    }).on('changed.owl.carousel', syncPosition);




    function syncPosition(el) {
        //if you set loop to false, you have to restore this next line
        //var current = el.item.index;

        //if you disable loop you have to comment this block
        var count = el.item.count - 1;
        var current = Math.round(el.item.index - (el.item.count / 2) - .5);

        if (current < 0) {
            current = count;
        }
        if (current > count) {
            current = 0;
        }
    }
    //end block



    function syncPosition2(el) {
        if (syncedSecondary) {
            var number = el.item.index;
            $homebannerBig.data('owl.carousel').to(number, 100, true);
        }
    }


}
function homebannerSlider() {

    var $homebannerBig = $('.home-banner-carousel');

    var syncedSecondary = true;
    $homebannerBig.owlCarousel({
        items: 1,
        loop: true,
        autoplay: 1,
        smartSpeed:1000,
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
        //navText: [
        //'<i class="fa fa-arrow-left" aria-hidden="true"></i>',
        //'<i class="fa fa-arrow-right" aria-hidden="true"></i>'
        //]
    });





}


function productHoverItem() {


    $(".js-hover-product-item").on("mouseenter", function (e) {
        $(this).addClass("active");
        if ($(window).width() >= 992) {
            $(this).css({ "height": $(this).closest(".col-lg-3").outerHeight() - 30 });
        }
    }).on("mouseleave", function () {
        $(this).removeClass("active");
        if ($(window).width() >= 992) {
            $(this).css({ "height": "auto" });
        }
    });



    //$(".js-hover-product-item").hover(function () {
    //    console.log($(this).outerHeight()); return;
    //    var e = $(this);
    //    var outerHeight = e.outerHeight() - 2;
    //    e.css({
    //        height: outerHeight
    //    }).addClass("active");
    //}, function(){
    //    var e = $(this);
    //    e.css({
    //        height: "auto"
    //    }).removeClass("active")
    //});
}


function categoryFirmSliderInit() {
    var categoryFirmSlider = $('.category-firm-slider');
    if (categoryFirmSlider != undefined) {
        categoryFirmSlider.owlCarousel({
            items: 1,
            loop: true,
            autoplay: 1,
            dots: true,
            nav: true,
            navText: ["", ""],
            autoplayTimeout: 9000,
            autoplayHoverPause: true,
            responsive: {
                0: {
                    nav: false
                },
                992: {
                    nav: true
                }
            }
        });
    }
}

function cookiePolicyInit() {
    $('body').cookieDisclaimer({
        style: " dark",
        settings: {
            style: ' col-md-4'
        },
        text: "<i class='fa fa-info-circle'></i> Hizmetlerimizden en iyi şekilde faydalanabilmeniz için çerezler kullanıyoruz. makinaturkiye.com'u kullanarak çerezlere izin vermiş olursunuz. <a href='/cerez-politikasi-y-183318'>Çerez politikamız için tıklayın.</a>",
        acceptBtn: {
            text: "<i class='fa fa-close' style='font-size: 20px; color:#636363; margin-left:20px; cursor:pointer;'></i>", // accept btn text
            cssClass: "cdbtn cookie", // accept btn class
            cssId: "cookieAcceptBtn", // univocal accept btn ID
            onAfter: "" // callback after accept button click
        },
        policyBtn: {
            active: false,
        }

    });
}

function GetHeaderSectorCategory() {
    $.ajax({
        type: 'get',
        url: '/Home/_HeaderMainMenu',
        data: {},
        success: function (data) {

            $('#SectorsCategories').html(data);
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
        },
        error: function (x, y, z) {
            //alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
        }
    });
}
function GetHeaderSectorStoreCategory() {
    $.ajax({
        type: 'get',
        url: '/Store/_HeaderStoreCategoryGeneral',
        data: {},
        success: function (data) {

            $('#StoreCategories').html(data);

            var ClonedCategories2 = $("#StoreCategories [role=menubar]").clone();
            //$("#MobileCustomersContainer").children("a").append($(DownIcon).clone());
            $("#MobileCustomersContainer").append(ClonedCategories2);


        },
        error: function (x, y, z) {
            //alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
        }
    });
}
function GetSubMenu(id, thisobj) {

    var data = $('.mega-dropdown-menu', thisobj).html();


    if (data.indexOf("loading") > 0) {
        $('.mega-dropdown-menu', thisobj).find("#loading").html('<img src="../../Content/V2/images/loading.gif" />');
        $.ajax({
            type: "get", //rest Type
            dataType: 'json', //mispelled
            url: "/ajax/GetSubMenu/"+id,
            //contentType: "application/json; charset=utf-8",
            success: function (msg) {

                if (msg) {

                    $('.mega-dropdown-menu', thisobj).html(msg);
                }
            },
            error: function (xhr) {

                if (xhr.status == 200) {

                    $('.mega-dropdown-menu', thisobj).html(xhr.responseText);
                }
            }
        });

    }
    else {

    }
}
function topFunction() {

    $("html, body").animate({
        scrollTop: 0
    }, "slow");
}
function scrollFunction() {
    var mybutton = document.getElementById("myBtn");
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}


$(document).ready(function () {
    $('.product-category-tab-container .nav-item a').on('shown.bs.tab', function (e) {
        //e.target // newly activated tab
        //e.relatedTarget // previous active tab

        var crousalClass = $(this).attr("data-crousal");

        $("." + crousalClass).trigger('refresh.owl.carousel');
    });

    $('#btnGroupVerticalDrop1').unbind().on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();

        $(this).parent().find(".dropdown-menu").slideToggle();


    });


    //GetHeaderSectorCategory();
   // GetHeaderSectorStoreCategory();
    var senderMainPartyId;
    var activationCode = "";
    cookiePolicyInit();
    mainMenuColorChange();
    facebookLoginClickEvent();
    hasVideoLinkClick();
  //  productHoverItem();
    pagerGoto();


    function toggleNavbarMethod() {
        if ($(window).width() > 991) {
            $('.main-navigation .dropdown').hover(function () {
                var dataId = $(this).attr("data-menu-id");
                if (dataId) {
                    GetSubMenu(dataId, this);
                }

                $(this).find('.dropdown-menu').stop(true, true).delay(350).fadeIn(0);



            }, function () {

                $(this).find('.dropdown-menu').stop(true, true).delay(350).fadeOut(0);
            });
        }
        else {
            $(".main-navigation .dropdown .dropdown-toggle").unbind().on('click', function (event) {

                var categoryId = $(this).attr("data-cat-id");
                console.log("clicked", categoryId);
                event.preventDefault();
                event.stopPropagation();
                $(this).parent().siblings().removeClass('open');

                var parentAttrClass = $(this).parent().parent().parent().attr("class");
                console.log(parentAttrClass);
                $(this).parent().parent().parent().addClass('open');
                if (categoryId) {

                    GetSubMenu(categoryId, $(this).parent().parent().parent());
                }
                if (parentAttrClass.indexOf("open") > 0) {
                    $(this).parent().parent().parent().removeClass("open");
                }


            });
        }
    }
    toggleNavbarMethod();
    $(window).resize(toggleNavbarMethod);

    $(".ac-dropdown").hover(
        function () {

            $('.dropdown-menu', this).not('.in .dropdown-menu').show();
            $(this).toggleClass('open');
        },
        function () {
            $('.dropdown-menu', this).not('.in .dropdown-menu').hide();
            $(this).toggleClass('open');
        }
    );


    // When the user scrolls down 20px from the top of the document, show the button
    var isMobile1 = window.matchMedia("only screen and (max-width: 760px)").matches;
    if (isMobile1) {
        window.onscroll = function () { scrollFunction() };
    }




    // When the user clicks on the button, scroll to the top of the document

    // productImageResize();
    mailActivationValidate();
    checkitem();
    homebannerSlider();
    categorybannerSlider();
    siteChangeVideo();
    //moreLessLink();
    HariciLinkler();
    toggleResponsiveMenu.init();
    showSubCategory();
    bottomSlider();
    categoryFirmSliderInit();

    $(".urun-aciklama #aciklama table").wrap("<div class='table-responsive'></div>")

    //storePageScroll();
    $("#mailActivation").change(function () {
        if (this.checked) {
            //Do stuff
            $("#PhoneActivation-wrapper").hide();
            $("#noActivationWarm #activationWarm-sub").hide();
            $("#mailActivationValue").val("1");
        }
        else {
            $("#PhoneActivation-wrapper").show();
            $("#noActivationWarm #activationWarm-sub").show();
            $("#mailActivationValue").val("0");
        }
    });


    if ($('.js-sector-firm-slider').length > 0) {
        $('.js-sector-firm-slider').owlCarousel({
            loop: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
            margin: 10,
            nav: true,
            autoplay: true,
            items: 6,
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
                    items: 3
                },
                991: {
                    items: 5
                },
                1200: {
                    items: 6
                }
            }
        })
    }
    //kategori filtre alanı
    if ($('.leftSideBar').length > 0) {
        $('.leftSideBar').theiaStickySidebar({
            additionalMarginTop: 50,
        });
    }

    // Ürün detay firma bilgileri alanı
    if ($('.rightSidebar').length > 0) {
        $('.rightSidebar').theiaStickySidebar({
            additionalMarginTop: 50,
        });
    }


    //Sektör sayfası
    if ($('.sidebarBanner').length > 0) {
        $('.sidebarBanner').theiaStickySidebar({
            additionalMarginTop: 50,
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

    //if ($('.js-sector-firm-slider').length > 0) {
    //    $('.js-sector-firm-slider').owlCarousel({
    //        loop: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
    //        margin: 10,
    //        nav: true,
    //        autoplay: true,
    //        items: 6,
    //        autoplayHoverPause: true,
    //        autoplaySpeed: 1000,
    //        autoHeight: true,
    //        navText: ["", ""],
    //        center: $('.js-sector-firm-slider').find('.owl-item').length > 1 ? true : false,
    //        responsive: {
    //            0: {
    //                items: 2
    //            },
    //            768: {
    //                items: 4
    //            },
    //            991: {
    //                items: 6
    //            }
    //        }
    //    })
    //}


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
        $('.js-hamburger').eq(0).trigger('click');
    });

    $('.upBtn').on('click', function () {

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

        //console.log(_top);

        if (_top >= 250) {
            $('.sector-category-nav').addClass('is-fixed');//.css({ 'position': 'fixed', 'top': '70px' });
            $('.sector-category-nav-scroll-fix').show();
        } else {
            $('.sector-category-nav').removeClass('is-fixed');//.css({ 'position': 'relative', 'top': '0px' });
            $('.sector-category-nav-scroll-fix').hide();
        }


        /*Store Profile Header Fixed*/
        if ($('.store-profile-header').length > 0) {
            var pixs = _top / 50;
            var s = 250 - Math.min(250, _top - 240);
            var scrollOffset = $(this).width() < 992 ? 515 : 400;


            if ($(this).width() > 767) {

                $('.store-profile-header__cover').css({ "-webkit-filter": "blur(" + pixs + "px)", "filter": "blur(" + pixs + "px)" });


                if (_top >= 250) {
                    $('.store-profile-header__brand-logo').width(s).height(s);
                } else {
                    $('.store-profile-header__brand-logo').width(250).height(250);
                }

                if (_top >= scrollOffset) {
                    $('.store-profile-header__info').addClass('is-fixed');
                } else {
                    $('.store-profile-header__info').removeClass('is-fixed');
                }

            }

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


        /*Breadcrumb Fixed*/
        if (_top >= 100 && $(".fast-access-bar").length > 0) {
            $(".fast-access-bar").addClass('is-fixed');
        } else {
            $(".fast-access-bar").removeClass('is-fixed');
        }


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

        if (top > 125) {
            $(".js-sticky-header").show();
            //$('.new-header').addClass('is-fixed');
        } else {
            $(".js-sticky-header").hide();
            //$('.new-header').removeClass('is-fixed');
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
        appendTo: $('.categort-filter'),
        source: function (request, response) {
            $.ajax({
                url: "/StoreSearch/SearchCategory",
                type: "POST",
                dataType: "json",
                data: { categoryName: request.term },
                success: function (data) {

                    if (data.length > 0) {
                        response($.map(data, function (item) {
                            return {
                                label: item.Text + " Firmaları",
                                id: item.Value,
                                value: item.Text + " Firmaları"
                            };
                        }));
                    } else {
                        $('#newCategoryId').val("");
                    }

                },
                error: function (x, l, e) {
                    alert(x.responseText);
                }

            });
        }, select: function (event, ui) {
            $('#newCategoryId').val(ui.item.id);
        }
    });
    //$('#categoryName').Autocomplete({
    //    serviceUrl: '/StoreSearch/SearchCategory',
    //    minChars: 1,
    //    showNoSuggestionNotice: true,
    //    noSuggestionNotice: '',
    //    appendTo: $('#categoryName').parentNode,
    //    formatResult: function (data) {
    //        return "<span class='suggestion-wrapper'><span class='suggestion-  value'>" + data.CategoryName + "</span></span>";
    //    },
    //    onSelect: function (data) {
    //        $('#SearchText').val(data.id);
    //        this.form.submit();
    //    }
    //});


    $('.js-firm-category-search').click(function () {

        $.ajax({
            url: "/StoreSearch/CategoryGetUrlName",
            type: "POST",
            dataType: "json",
            data: { categoryName: JSON.stringify($('#categoryName').val()) },
            success: function (data) {
                $('#categoryUrlName').val(data);
                var newCategoryId = $('#newCategoryId').val();

                if (newCategoryId) {
                    window.location = $('#categoryUrlName').val() + '-sc-' + newCategoryId;
                }
                else {
                    $("#error-categoryname").html("*Böyle Bir Kategori Bulunamadı.");
                    $("#error-categoryname").show();
                }

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
                url: '/ajax/CheckEmail',
                type: "get",
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
        else {
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
                if ($("#mailActivationValue").val() == "0" || $("#mailActivationValue").val() == "") {
                    if (activationCodeGet.trim() != "") {

                        if (activationCode == activationCodeGet) {
                            SendMessagePopup(senderMainPartyId, 1, activationCodeGet);
                            $(this).hide();
                        }
                        else {
                            SendMessagePopup(senderMainPartyId, 0, activationCodeGet);
                            $(this).hide();
                        }
                    }
                    else {
                        alert("Lütfen Aktivasyon Kodunu Giriniz");
                    }
                }
                else {

                    SendMessagePopup(senderMainPartyId, 1, activationCodeGet);
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
        $(".popovers").popover("hide");
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

    var width = window.innerWidth ? window.innerWidth : documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
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
    });
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
        autoWidth: false,
        autoplayHoverPause: true,
        responsive: {
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
        }
    });

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


var DownIcon = $("<i class='icon-down-arrow'></i>");
$(DownIcon).attr("data-toggle", "setactive");
$(DownIcon).addClass("menutoggleicon");


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
        autoHeight :false,
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


var __slice = [].slice;

(function ($, window) {
    var Starrr;

    Starrr = (function () {
        Starrr.prototype.defaults = {
            rating: void 0,
            numStars: 5,
            change: function (e, value) { }
        };

        function Starrr($el, options) {
            var i, _, _ref,
                _this = this;

            this.options = $.extend({}, this.defaults, options);
            this.$el = $el;
            _ref = this.defaults;
            for (i in _ref) {
                _ = _ref[i];
                if (this.$el.data(i) != null) {
                    this.options[i] = this.$el.data(i);
                }
            }
            this.createStars();
            this.syncRating();
            this.$el.on('mouseover.starrr', 'span', function (e) {
                return _this.syncRating(_this.$el.find('span').index(e.currentTarget) + 1);
            });
            this.$el.on('mouseout.starrr', function () {
                return _this.syncRating();
            });
            this.$el.on('click.starrr', 'span', function (e) {
                return _this.setRating(_this.$el.find('span').index(e.currentTarget) + 1);
            });
            this.$el.on('starrr:change', this.options.change);
        }

        Starrr.prototype.createStars = function () {
            var _i, _ref, _results;

            _results = [];
            for (_i = 1, _ref = this.options.numStars; 1 <= _ref ? _i <= _ref : _i >= _ref; 1 <= _ref ? _i++ : _i--) {
                _results.push(this.$el.append("<span class='glyphicon .glyphicon-star-empty' style='font-size:18px; color:#F28B00;'></span>"));
            }
            return _results;
        };

        Starrr.prototype.setRating = function (rating) {
            if (this.options.rating === rating) {
                rating = void 0;
            }
            this.options.rating = rating;
            this.syncRating();
            return this.$el.trigger('starrr:change', rating);
        };

        Starrr.prototype.syncRating = function (rating) {
            var i, _i, _j, _ref;

            rating || (rating = this.options.rating);
            if (rating) {
                for (i = _i = 0, _ref = rating - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; i = 0 <= _ref ? ++_i : --_i) {
                    this.$el.find('span').eq(i).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
                }
            }
            if (rating && rating < 5) {
                for (i = _j = rating; rating <= 4 ? _j <= 4 : _j >= 4; i = rating <= 4 ? ++_j : --_j) {
                    this.$el.find('span').eq(i).removeClass('glyphicon-star').addClass('glyphicon-star-empty');
                }
            }
            if (!rating) {
                return this.$el.find('span').removeClass('glyphicon-star').addClass('glyphicon-star-empty');
            }
        };

        return Starrr;

    })();
    return $.fn.extend({
        starrr: function () {
            var args, option;

            option = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            return this.each(function () {
                var data;

                data = $(this).data('star-rating');
                if (!data) {
                    $(this).data('star-rating', (data = new Starrr($(this), option)));
                }
                if (typeof option === 'string') {
                    return data[option].apply(data, args);
                }
            });
        }
    });
})(window.jQuery, window);

$(function () {
    return $(".starrr").starrr();
});



//lazy loading image
document.addEventListener("DOMContentLoaded", function () {
    var lazyloadImages;

    if ("IntersectionObserver" in window) {
        lazyloadImages = document.querySelectorAll(".img-lazy-l");
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
        lazyloadImages = document.querySelectorAll(".img-lazy-l");

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
        if (lazyloadImages.length > 0) {
            document.addEventListener("scroll", lazyload);
            window.addEventListener("resize", lazyload);
            window.addEventListener("orientationChange", lazyload);
        }
    }
});

    (function ($) {
        $.fn.menumaker = function (options) {
            var cssmenu = $(this), settings = $.extend({
                format: "dropdown",
                sticky: false
            }, options);
            return this.each(function () {
               $(".button").on('click', function () {
                    $(this).toggleClass('menu-opened');
                    var mainmenu = $(this).next('ul');
                    if (mainmenu.hasClass('open')) {
                        mainmenu.slideToggle().removeClass('open');
                    }
                    else {
                        mainmenu.slideToggle().addClass('open');
                        if (settings.format === "dropdown") {
                            mainmenu.find('ul').show();
                        }
                    }
                });
                cssmenu.find('li ul').parent().addClass('has-sub');
                multiTg = function () {
                    cssmenu.find(".has-sub").prepend('<span class="submenu-button"></span>');
                    cssmenu.find('.submenu-button').on('click', function () {
                        $(this).toggleClass('submenu-opened');
                        if ($(this).siblings('ul').hasClass('open')) {
                            $(this).siblings('ul').removeClass('open').slideToggle();
                        }
                        else {
                            $(this).siblings('ul').addClass('open').slideToggle();
                        }
                    });
                };
                if (settings.format === 'multitoggle') multiTg();
                else cssmenu.addClass('dropdown');
                if (settings.sticky === true) cssmenu.css('position', 'fixed');
                resizeFix = function () {
                    var mediasize = 1200;
                    if ($(window).width() > mediasize) {
                        cssmenu.find('ul').show();
                    }
                    if ($(window).width() <= mediasize) {
                        cssmenu.find('ul').hide().removeClass('open');
                    }
                };
                resizeFix();
                return $(window).on('resize', resizeFix);
            });
        };
    })(jQuery);

(function ($) {
    $(document).ready(function () {
        $("#cssmenu").menumaker({
            format: "multitoggle"
        });
    });
})(jQuery);




﻿$(document).ready(function () {
    $('#add-user').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            memberemail: {
                selector: '#MemberEmail',
                message: 'Email Adresi Zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Email adresi alanı zorunludur'
                    },
                    emailAddress: {
                        message: 'E-mail adresini doğru giriniz.'
                    },
                    remote: {
                        message: 'Bu Email adresi kullanılmaktadır',
                        url: '/Membership/CheckMemberEmail/'
                    }

                }
            },
            password: {
                selector: '#MemberPassword',
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Şifre en az 6 karakterden oluşmalıdır'
                    }
                }
            },
            name: {
                selector: '#MemberName',
                validators: {
                    notEmpty: {
                        message: 'İsim  boş geçilemez'
                    }
                }
            },
            surname: {
                selector: '#MemberSurname',
                validators: {
                    notEmpty: {
                        message: 'Soyisim  boş geçilemez'
                    }
                }
            },
            gender: {
                selector: '.Gender',
                validators: {
                    notEmpty: {
                        message: 'Cinsiyet Boş Geçilemez'
                    }
                }
            },

        }
    });
    $('#login-form').bootstrapValidator({
        message: 'Bu değer valid değildir',
        live: 'enabled',
        trigger: null,
        submitButton: '$user_fact_form button[type="submit"]',
        feedbackIcons: {
            valid: 'fa fa-sync',
            invalid: 'fa fa-exclamation',
            validating: 'fa fa-check'
        },

        fields: {
            Email: {
                message: 'Email Adresi Zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Email adresi alanı zorunludur'
                    },
                    emailAddress: {
                        message: 'E-mail adresini doğru giriniz.'
                    }

                }
            },
            Password: {
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Şifre en az 6 karakterden oluşmalıdır'
                    }
                }
            }
        }
    }).on('success.form.bv', function (e) {
   e.preventDefault();

        $(".loading-membership").show();
        var email = $("#Email").val();
        var password = $("#Password").val();
        var returnUrl = $("#ReturnUrl").val();
        $.ajax({
            url: '/Membership/Logon',
            type: 'post',
            data: {
                Email: email,
                Password: password,
                ReturnUrl: returnUrl
            },
            dataType: 'json',
            success: function (data) {
                $(".loading-membership").hide();
                if (data.IsSuccess) {

                    window.location = data.Result.ReturnUrl;
                }
                else {
                    console.log(data.Message);
                    $("#MembershipError").fadeIn();
                    $("#MembershipError").html('<i class="fa fa-exclamation-circle" aria-hidden="true"></i>' + data.Message);

                }
            },
            error: function (request, error) {
                $(".loading-membership").hide();
                alert("Request: " + JSON.stringify(request));
            }
        });
     });

    $('#register-form').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'fa fa-sync',
            invalid: 'fa fa-exclamation',
            validating: 'fa fa-check'
        },
        fields: {
            MemberEmail: {
                message: 'Email Adresi Zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Email adresi alanı zorunludur'
                    },
                    emailAddress: {
                        message: 'E-mail adresini doğru giriniz.'
                    },
                    remote: {
                        message: 'Bu Email adresi kullanılmaktadır',
                        url: '/Membership/CheckMemberEmail/'
                    }

                }
            },
            MemberPassword: {
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Şifre en az 6 karakterden oluşmalıdır'
                    }
                }
            },
            Name: {
                validators: {
                    notEmpty: {
                        message: 'İsim  boş geçilemez'
                    }
                }
            },
            Surname: {
                validators: {
                    notEmpty: {
                        message: 'Soyisim  boş geçilemez'
                    }
                }
            }
        }
    });


    $('#add-video-form').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {

            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            VideoTitle: {
                validators: {
                    notEmpty: {
                        message: 'Başlık Giriniz'
                    }

                },


            },

            video: {
                validators: {
                    file: {
                        extension: 'mp4,mpeg mpg,m4v',
                        type: 'video/mp4,video/mpeg, video/x-m4v',
                        message: 'Dosya formatı uygun değil'
                    },
                    notEmpty: {
                        message: 'Video Seçiniz'
                    }
                }
            }
        }
    });


});


﻿$(document).ready(function() {
    ChooseMembershipForm();
});

function ChooseMembershipForm() {
    $('input:radio[name=membership]').change(function () {
        if (this.value == 'login') {
            $("#LogInContainer").show();
            $("#RegisterContainer").hide();
            $("#MemberShipHeader").html("Giriş Yap");
        }
        else if (this.value == 'register') {
            $("#LogInContainer").hide();
            $("#RegisterContainer").show();
            $("#MemberShipHeader").html("Üye Ol");
        }
    });
}
﻿
$("#ActivityCategory").change(function () {
    var sectorId = this.value;
    $("#SubCategory").html("");
    if (sectorId != "") {
        $.ajax({

            url: '/Account/StoreActivity/GetSubCategory',
            type: 'GET',
            data: {
                'sectorId': sectorId
            },
            dataType: 'json',
            success: function (data) {
                console.log(data);
                $.each(data.Result, function (i, item) {
                    $("#subCategoryContainer").show();
                    $("#SubCategory").append(
                        "<option value='" + item.CategoryId + "' >" + item.CategoryName + "</option>"
                    );

                });

            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    }
    else {
        $("#subCategoryContainer").hide();
        $("#SubCategory").html("");
    }

});

function DeleteStoreActivityCategory(id) {
    if (confirm('Kaldırmak istediğinize emin misiniz?')) {
        $.ajax({

            url: '/Account/StoreActivity/Delete',
            type: 'GET',
            data: {
                'storeActivityCategoryId':id
            },
            dataType: 'json',
            success: function (data) {
                if (data) {
                    $("#storeActivity"+id).hide();
                }
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });

    }

}

function DeleteStoreSector(id) {
    if (confirm('Kaldırmak istediğinize emin misiniz?')) {
        $.ajax({

            url: '/Account/Store/DeleteSector',
            type: 'GET',
            data: {
                'storeSectorId': id
            },
            dataType: 'json',
            success: function (data) {
                if (data) {
                    $("#storeSector" + id).hide();
                }
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });

    }

}
function AddFavoriteProductItem(id) {

    $("[data-productid=product-favorite-item-" + id + "]").attr("title", "Favorilerimden Kaldır");
    $.ajax({
        url: '/ajax/AddFavoriteProduct',
        type: 'get',
        data:
            {
                ProductId: id
            },
        success: function (data) {
            if (data == true) {

                var item = $("[data-productid=product-favorite-item-" + id + "]");

                $("[data-productid=product-favorite-item-" + id + "]").attr("onclick", "RemoveFavoriteProductItem("+id+")");
                $("[data-productid=product-favorite-item-" + id + "]").attr("title", "Favorilerimden Kaldır");

                item.html("<i class='fa fa-heart'></i>");

                //                        $.facebox('Görüntülemiş olduğunuz ürün başarıyla favori ürünlerinize eklenmiştir.');
                //                        $('#divFavroriteProductImage').css('background-image', 'url(/Content/Images/removeFavorite.png)');
            }
            else {
                $("#FavoriteProductLoading").hide();
                //  $.facebox('Görüntülemiş olduğunuz ürünü favori ürünlerinize eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.');
                window.location.href = '/Uyelik/kullanicigirisi';
            }
        }
    });

}


function RemoveFavoriteProductItem(id) {


    $.ajax({
        url: '/ajax/RemoveFavoriteProduct',
        type: 'get',
        data:
            {
                ProductId: id
            },
        success: function (data) {
            var item = $("[data-productid=product-favorite-item-" + id + "]");
            $("[data-productid=product-favorite-item-" + id + "]").attr("onclick", "AddFavoriteProductItem(" + id + ")");
            $("[data-productid=product-favorite-item-" + id + "]").attr("title", "Favorilerime Ekle");
            item.html("<i class='fa fa-heart-o' ></i>");
        },
        error: function (x, l, e) {
            $.facebox('Görüntülemiş olduğunuz ürün favori ürünlerinizden çıkarılırken bir sorun oluştu.');
        }
    });

}



$(document).ready(function () {

    $(".product-list-favorite-icon-c").click(function (event) {
        event.preventDefault();
    });

});
﻿var isEmail = true;
$(document).ready(function () {
    $("#formFastMembership").validationEngine();
    $("#formFastMembershipIndividual").validationEngine();
    $('#-xcaptcha-refresh').attr("title", "Güvenlik Kodunu Yenile");
    var isEmail = true;
    var cityWrapper = $('[data-rel="city-wrapper"]');
    var addressWrapper = $('[data-rel="address-wrapper"]');
    var localityWrapper = $('[data-rel="locality-wrapper"]');
    var districtWrapper = $('[data-rel="district-wrapper"]');
    var otherElementsWrapper = $('[data-rel="other-wrapper"]');
    var phoneWrapper = $('[data-rel="phone-wrapper"]');
    var gsmWrapper = $('[data-rel="gsm-wrapper"]');

    $('[data-rel="countryId"]').change(function () {
        var countryId = $(this).val();

        getCultureCode(countryId, function (cultureCode) {

            $('#MembershipModel_InstitutionalPhoneCulture,#MembershipModel_InstitutionalGSMCulture,#MembershipModel_InstitutionalPhoneCulture2,#MembershipModel_InstitutionalFaxCulture').val(cultureCode);
        });
        phoneWrapper.slideDown();

        if (countryId == 246) {//türkiye ise
            cityWrapper.slideDown();
            addressWrapper.slideUp();
            gsmWrapper.slideDown();
            $.ajax({
                url: '/Membership/Cities',
                data: { id: countryId },
                success: function (msg) {
                    $('[data-rel="cityId"]' + " > option").remove();
                    $.each(msg, function (i) {
                        $('[data-rel="cityId"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                    });
                },
                error: function (e) {
                    alert(e.responseText);
                }
            });
        }
        else {
            cityWrapper.slideUp();
            localityWrapper.slideUp();
            districtWrapper.slideUp();
            otherElementsWrapper.slideUp();
            addressWrapper.slideDown();
            gsmWrapper.slideUp();
            $('[data-rel="cityId"]' + " > option").remove();
            $('#MembershipModel_InstitutionalPhoneCulture,#MembershipModel_InstitutionalPhoneCulture2,#MembershipModel_InstitutionalFaxCulture').val("");
            $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val("").show();
            $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
        }
    });

    //il
    $('[data-rel="cityId"]').change(function () {
        var cityId = $(this).val();
        localityWrapper.slideDown();
        districtWrapper.slideUp();
        otherElementsWrapper.slideUp();
        $.ajax({
            url: '/Membership/Localities',
            data: { id: cityId },
            success: function (msg) {
                $('[data-rel="localityId"]' + " > option").remove();
                $.each(msg, function (i) {
                    $('[data-rel="localityId"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                });

                getAreaCode(cityId, function (areaCode) {
                    if (areaCode == 0) {//istanbul
                        $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').show();
                        $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val('').hide();
                    } else {

                        $('#TextInstitutionalPhoneAreaCode,#InstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val(areaCode).show();
                        $('#MembershipModel_InstitutionalPhoneAreaCode,#MembershipModel_InstitutionalPhoneAreaCode2,#MembershipModel_InstitutionalFaxAreaCode').val(areaCode);
                        $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
                    }
                });
            },
            error: function (e) {
                alert(e.responseText);
            }
        });


        $('#TextInstitutionalPhoneAreaCode').keyup(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode').val($(this).val());

        });
        $('#TextInstitutionalPhoneAreaCode2').keyup(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode2').val($(this).val());
        });
        $('#TextInstitutionalFaxAreaCode').keyup(function () {
            $('#MembershipModel_InstitutionalFaxAreaCode').val($(this).val());
        });
        $('#DropDownInstitutionalPhoneAreaCode').change(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode').val($(this).val());

        });
        $('#DropDownInstitutionalPhoneAreaCode2').change(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode2').val($(this).val());
        });
        $('#DropDownInstitutionalFaxAreaCode').change(function () {
            $('#MembershipModel_InstitutionalFaxAreaCode').val($(this).val());
        });
    });

    function getAreaCode(cityId, callback) {
        $.ajax({
            url: '/Membership/AreaCode',
            data: { CityId: cityId },
            success: function (msg) {
                callback(msg);
            },
            error: function (e) {
                callback('');
            }
        });
    }

    function getCultureCode(countryid, callback) {
        $.ajax({
            url: '/Membership/CultureCode',
            data: { CountryId: countryid },
            success: function (msg) {
                callback(msg);
            },
            error: function (e) {
                callback('');
            }
        });
    }


    //ilçe
    $('[data-rel="localityId"]').change(function () {
        districtWrapper.slideDown();
        $.ajax({
            url: '/Membership/Towns',
            data: { id: $(this).val() },
            success: function (msg) {
                $('[data-rel="mahalle"]' + " > option").remove();
                $.each(msg, function (i) {
                    $('[data-rel="mahalle"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                });
            },
            error: function (e) {
                alert(e.responseText);
            }
        });
    });

    //mahalle - köy
    $('[data-rel="mahalle"]').change(function () {
        otherElementsWrapper.slideDown();
        var townId = $(this).val();
        $('[name="MembershipModel.TownId"]').val(townId);
        if ($(this).val() != "0") {
            $.ajax({
                url: '/Membership/ZipCode',
                data: { TownId: townId },
                success: function (data) {
                    $('[name="MembershipModel.ZipCode"]').val(data);
                }
            });
        }

    });

    //bireysel, kurumsal
    $('[data-toggle="tab"]').click(function () {
        var selectedType = ($(this).attr('href') == "#bireysel" ? 10 : 20);
        $('[data-rel="MembershipType"]').val(selectedType);
        $('[data-rel="title"]').html(selectedType == 10 ? "Ücretsiz Kayıt Olun (Bireysel)" : "Ücretsiz Kayıt Olun (Kurumsal)");
    });

    //günvelik kodu refresh
    $('[data-rel="security-code-refresh"]').click(function () {
        randomSecuirtyCode();
        $('[data-rel="security-code"]').val('');
    });
    //    $('#btnSend').click(function () {
    //        var isValidate = $(this).validationEngine('validate');
    //        if (!isValidate) {
    //            var comparePassword = $(this).val();
    //            var newPassword = $('#newPassword').val();

    //            if (comparePassword != newPassword) {
    //                setTimeout(function () {
    //                    $(this).validationEngine('showPrompt', 'Girmiş olduğunuz şifre ile doğrulama şifresi aynı değil. Tekrar deneyiniz.', 'red')
    //                }, 1000);
    //                return false;
    //            }
    //
    //        }
    //    });

    $('[data-rel="DestinationEmail"]').keyup(function () {
        var isValidate = $(this).validationEngine('validate');
        if (!isValidate) {
            $.ajax({
                url: '/Membership/CheckMail',
                data: { email: $(this).val() },
                success: function (data) {
                    isEmail = (data == "true" ? true : false);
                    //  alert(isEmail.toString());

                    if (isEmail) {
                        setTimeout(function () {
                            $('[data-rel="DestinationEmail').validationEngine('showPrompt', 'Bu E-Posta adresi kullanılmamaktadır. Lütfen Tekrar Deneyiniz.', 'red')
                        }, 1000);

                        $('[data-rel="form-submit"]').attr('disabled', 'disabled');

                    }
                    else {
                        $('[data-rel="form-submit"]').removeAttr('disabled');
                    }

                }
            });
        }
    });
    $('[data-rel="memberEmail"]').keyup(function () {
        var isValidate = $(this).validationEngine('validate');
        if (!isValidate) {
            $.ajax({
                url: '/Membership/CheckEmailForNewMember',
                data: { email: $(this).val() },
                success: function (data) {
                    if (data) {
                        $('[data-rel="memberEmail').validationEngine('showPrompt', 'Bu E-Posta adresi kullanılmaktadır. Lütfen Tekrar Deneyiniz.', 'red');
                        $('[data-rel="form-submit"]').addClass('disabled');
                    }
                    else {
                        $('[data-rel="form-submit"]').removeClass('disabled');
                    }
                }
            });
        }
    });

    $("[data-rel='form-submit']").click(function () {
        var sehirId = $("#MembershipModel_CityId").val();
        var telNo = $("#MembershipModel_InstitutionalPhoneNumber").val();
        var gsm = $("#MembershipModel_InstitutionalGSMAreaCode").val();
        var gsmAna = $("#MembershipModel_InstitutionalGSMNumber").val();

        if (isEmail) {
          //  $('#formFastMembership').submit();
            $('[data-rel="email-wrapper').hide();

        }
        else {
            $('[data-rel="memberEmail').focus();
            $('[data-rel="email-wrapper').show();
        }
    });

});

//dropdown selected value control
function ifSelectNotEmpty(field, rules, i, options) {
    if (field.val() == "0") {
        return "* Bu alan zorunludur";
    }
}

$(document).on('invalid-form.validate', 'form', function () {
    $('[data-rel="form-submit"]').removeClass('disabled');
});
$(document).on('submit', 'form', function () {
    $('[data-rel="form-submit"]').addClass('disabled');
});