var POLICY_LINK = "https://www.makinaturkiye.com/cerez-politikasi-y-183318";
var COOKIE_NAME = "hideCookiePolicy";
var COOKIE_CSS_PATH ="/Content/V2/assets/css/cookiePolicy.css";
var createCookiePolicy = {
    

    text: "<span class='text-wrapper'><i class='fa fa-info-circle'></i> Hizmetlerimizden en iyi şekilde faydalanabilmeniz için çerezler kullanıyoruz.<br> makinaturkiye.com'u kullanarak çerezlere izin vermiş olursunuz. <a href='"+POLICY_LINK+"'>Çerez politikamız için tıklayın.</a></span>",
    createDiv : function() {
        return '<div id="cookiePolicy" class="cookiePolicy"><div class="container"><div class="row"><div class="col-xs-12 col-sm-6 col-sm-offset-3">'+this.text+'<p><button class="btn btn-primary closeCookie">KAPAT</button></p></div></div></div></div>' 
    }
};
function setCookie(cname, cvalue) {
    var d = new Date();
    d.setTime(d.getTime() + (365 * 24 * 60 * 60 * 1000));
    var expires = d.toGMTString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";domain=.makinaturkiye.com;path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');



    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

window.addEventListener("load", function () {
    if (getCookie(COOKIE_NAME)) return;
    var bodySelector = document.getElementsByTagName("body")[0];
  document.getElementsByTagName("head")[0].insertAdjacentHTML("beforeend",
    '<link rel="stylesheet" href="' + COOKIE_CSS_PATH  + '" />');
    bodySelector.insertAdjacentHTML("beforeend", createCookiePolicy.createDiv());
    var b = document.getElementsByClassName("closeCookie");
    b[0].addEventListener("click", function() {
        document.getElementsByClassName("cookiePolicy")[0].style.display = "none";
        setCookie(COOKIE_NAME,"true");
    });

});
