/*
    Ana Menu Üst Menüye Göre Renk Deðiþimi
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



$(document).ready(function () {

    mainMenuColorChange();
    facebookLoginClickEvent();

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
