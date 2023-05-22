/*
    Ana Menu Üst Menüye Göre Renk Değişimi
*/
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
    $(".navbar-form").attr("action", "https://www.makinaturkiye.com/Sirketler?SearchType=3&amp;");
};

/*Listeden Ürün Ara Seç*/
function selectProductSearch() {
    $(".navbar-form").attr("action", "https://www.makinaturkiye.com/Urunler/AramaSonuclari?CategoryId=0");
};

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
function productImageResize() {
    if ($("body").width() < 775 && $(".urun-resim img").length > 0) {
        $(".urun-resim img").each(function () {
            var lastW = $(this).attr("src");
            var newW = lastW.replace("160x120", "400x300");
            $(this).attr("src", newW);
        });
    }
}

function ChangeVideo(e) {
    var v = document.getElementById('vd');
    var src = document.getElementById("videoSlide").getAttribute('data-videoPath');
    v.innerHTML += '<source src=' + src + ' type=video/mp4>';
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


function RemoveFavoriteStore(storeId, storeName) {
    //            var image = '<img src=\'/Content/Images/load.gif\' />'
    //            $.facebox('\'' + storeName + '\' Favori listenizden çıkarılıyor.. &nbsp;&nbsp;' + image);
    $.ajax({
        url: '/Product/RemoveFavoriteStore',
        type: 'post',
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
                    $("#successMessage").slideDown();
                    $("#PhoneActivation-wrapper").slideUp();


                }
                else {
                    $("#redirectbtn").show();
                    $("#errorMessage").slideDown();
                    $("#successMessage").slideUp();

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

function check() {
    if ($('#Member_MemberPassword').val() != $('#OldMemberPassword').val()) {
        alert("Girmiş olduğunuz şifre, sistemimizde kayıtlı şifrenizle uyuşmamaktadır.");
        return false;
    }
    else if ($('#Member_MemberEmailAgain').val() != $('#Member_NewEmail').val()) {
        alert("Girmiş olduğunuz E-Posta adresleri uyuşmamaktadır.");
        return false;
    }
    else {
        return true;
    }
}

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

$(document).ready(function () {
    var senderMainPartyId;
    var activationCode = "";
    mainMenuColorChange();
    facebookLoginClickEvent();
    pagerGoto();
    productImageResize();
    mailActivationValidate();
    checkitem();
    //moreLessLink();

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

    $('#btnFirmCategorySearch').click(function () {

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



                        }
                        else if (data.FastMembershipType1 == "5" || data.Password) {
                            $("#password-wrapper").show();
                            $("#redirectbtn").hide();
                            $("#signUpButton").show();

                        }
                        else if (data.FastMembershipType1 == "10") {
                            $("#redirectbtn").hide();
                            $("#facebookLogin").show();
                        }


                        //                $('[data-rel="memberEmail').validationEngine('showPrompt', 'E-Posta Adresiniz Sistemde Kayıtlıdır.', 'red');
                        //                $('[data-rel="form-submit"]').addClass('disabled');
                    }
                    else {
                        $('#mailinfo').css('display', 'inline-table');
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
                                    $("#phoneActivationLabel").html(data.PhoneNumber + " numaraya gelen aktivasyon kodunu giriniz:")
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
                                $('#mailinfo').css('display', 'inline-table');
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
                $(this).html("Tamamla");
                $('#mailname').css('display', 'inline-table');
                $('#mailsurname').css('display', 'inline-table');
                $("#mailtelephone").css("display", "inline-table");
                $('#mailtitle').css('display', 'inline-table');
                $('#maildescription').css('display', 'inline-table');
                $('#mailpass').css('display', 'inline-table');
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
    $('[data-toggle="collapse"]').on("click", function () {
        $($(this).attr("href")).hasClass("in") ? ($(this).closest(".panel-group").find(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right"),
        $(this).children(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right")) : ($(this).closest(".panel-group").find(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right"),
        $(this).children(".glyphicon-chevron-right").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down"))
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
    }),
    $(".tooltips").tooltip(),
    $(".popovers").popover({
        html: !0,
        trigger: "manual"
    }).click(function (a) {
        $(".popovers").not(this).popover("hide"),
        $(this).popover("show"),
        a.stopPropagation()
    }),
    $("html").click(function () {
        $(".popovers").popover("hide")
    }),
    $(".dropdown-dynamic li a").click(function () {
        var a = $(this).text()
          , b = " <span class='caret'></span>";
        $(this).html($(this).parent().parent().prev(".btn:first-child").text().replace(b, "")),
        $(this).parent().parent().prev(".btn:first-child").html(a + b)
    }),
    $(".summernote").summernote({
        height: 150
    }),
    $("img.img-zoom").each(function () {
        $(this).wrap('<span style="display:inline-block; width: ' + $(this).width() + "px; height: " + $(this).height() + 'px;"></span>').css("display", "block").parent().zoom()
    }),
    $(".modal").on("shown.bs.modal", function () {
        $(this).find("img.lazy").filter(':not([data-src=""])').each(function () {
            $(this).attr("src", $(this).data("src")),
            $(this).attr("data-src", "")
        })
    }),
    $('a[data-toggle="tab"]').on("shown.bs.tab", function () {
        $($(this).attr("href")).find("img.lazy").filter(':not([data-src=""])').each(function () {
            $(this).attr("src", $(this).data("src")),
            $(this).attr("data-src", "")
        })
    }),
    $(".prev-tab").on("click", function () {
        var a = $(this).parent().parent().find(".active").prev().attr("id");
        $('[href="#' + a + '"]').tab("show")
    }),
    $(".next-tab").on("click", function () {
        var a = $(this).parent().parent().find(".active").next().attr("id");
        $('[href="#' + a + '"]').tab("show")
    })

});