﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LoginModel>" %>

<meta name="google-signin-client_id" content="638267544487-nqpu1s475ju88si2rpper76f0rs5e03o.apps.googleusercontent.com">
<form action="javascript:void(0)" id="login-form">

    <%:Html.HiddenFor(x=>x.ReturnUrl) %>
    <div class="form-group">
        <label>Email*</label>
            <input type="email" name="Email" id="Email" value="<%=ViewData["mail"]%>" class="mt-form-control"
        placeholder="E-mail*" autofocus />
    </div>
    <div class="form-group">
             <label>Şifre*</label>
            <input type="password" name="Password" id="Password" value="<%=ViewData["password"]%>"
        class="mt-form-control" placeholder="Şifre" />
    </div>

    <label class="checkbox">
        <%=Html.HiddenFor(x=>x.ReturnUrl) %>
        <%=Html.CheckBoxFor(m=>Model.Remember) %>
        <%--     <input type="checkbox" id="Remember" name="Remember"/>--%>
                    Beni hatırla <a class="pull-right forget-password"  href="/Uyelik/SifremiUnuttum">Şifremi Unuttum
                    </a>
    </label>
    <button class="btn btn-sign-up" id="btnLogin" type="submit">
        Giriş Yap
    </button>
    </form>
    <hr>
    <div class="form-group">
            <a class="btn btn-info btn-facebook-login loginBtn--google googlegogin" href="javascript:;"
            data-max-rows="1" data-scope="email,public_profile" data-size="medium" data-show-faces="false" data-auto-logout-link="true"
            style="color: #fff; border-color: #DD4B39; background: #DD4B39; width: 100%;"><i class="fa fa-google-plus"></i> Google ile bağlan
        </a>
    </div>
    <hr />
    <div class="form-group">
            <!--<div class="col-md-6"  style="padding-left:0px; padding-right:10px;">
                  <div class="g-signin2" data-onsuccess="onSignIn"></div>
            </div>
            <div class="col-md-6" style="padding-left:0px; padding-right:0px;">
            <a class="btn  btn-info btn-facebook-login js-facebook-login col-md-6" href="javascript:;"
                data-max-rows="1" data-scope="email,public_profile" data-size="medium" data-show-faces="false" data-auto-logout-link="true"
                style="color: #fff; border-color: #3B5998; background: #3B5998; width: 100%;"><i class="fa fa-facebook fa-fw"></i>Facebook ile bağlan
            </a>
            </div>!-->
    <a class="btn  btn-info btn-facebook-login js-facebook-login" href="javascript:;"
        data-max-rows="1" data-scope="email,public_profile" data-size="medium" data-show-faces="false" data-auto-logout-link="true"
        style="color: #fff; border-color: #3B5998; background: #3B5998; width: 100%;"><i class="fa fa-facebook fa-fw"></i>Facebook ile bağlan
    </a>
<%--<div id="gSignInWrapper">
    <span class="label">Sign in with:</span>
    <div id="customBtn" class="customGPlusSignIn">
      <span class="icon"></span>
      <span class="buttonText">Google</span>
    </div>
  </div>--%>
<%--  <div id="name"></div>
  <script>startApp();</script>--%>
        </div>
<%--<script type="text/javascript">
    function onSignIn(googleUser) {
        var profile = googleUser.getBasicProfile();
        console.log(profile);
  console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
  console.log('Name: ' + profile.getName());
  console.log('Image URL: ' + profile.getImageUrl());
  console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
}
</script>--%>

<script type="text/javascript">
    (function ($) {


        $(document).ready(function () {
            function include(file) {
                var script = document.createElement('script');
                script.src = file;
                script.type = 'text/javascript';
                script.defer = true;
                document.getElementsByTagName('head').item(0).appendChild(script);

            }
            include('https://apis.google.com/js/platform.js');
            var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
            var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
            var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
            var CLIENTID = '991245376477-kivpob864itrc7m9njo6o8vmjplu7fm2.apps.googleusercontent.com';
            //var REDIRECT = 'http://localhost:35882/Membership/GoogleLogin';
            //var LOGOUT = 'http://localhost:35882/Membership/Logout';

            var REDIRECT = 'https://makinaturkiye.com/Membership/GoogleLogin';
            var LOGOUT = 'https://makinaturkiye.com/Membership/Logout';

            var TYPE = 'token';
            var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
            var acToken;
            var tokenType;
            var expiresIn;
            var user;
            var loggedIn = false;
            function GoogleLogin() {
                var win = window.open(_url, "windowname1", 'width=800, height=600');
                var pollTimer = window.setInterval(function () {
                    try {
                        console.log(win.document.URL);
                        if (win.document.URL.indexOf(REDIRECT) != -1) {
                            window.clearInterval(pollTimer);
                            var url = win.document.URL;
                            acToken = gup(url, 'access_token');
                            tokenType = gup(url, 'token_type');
                            expiresIn = gup(url, 'expires_in');
                            win.close();
                            validateToken(acToken);
                        }
                    }
                    catch (e) {

                    }
                }, 500);
            }
            function gup(url, name) {
                namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var regexS = "[\\#&]" + name + "=([^&#]*)";
                var regex = new RegExp(regexS);
                var results = regex.exec(url);
                if (results == null)
                    return "";
                else
                    return results[1];
            }
            function validateToken(token) {

                getUserInfo();
                $.ajax(

                    {

                        url: VALIDURL + token,
                        data: null,
                        success: function (responseText) {


                        },

                    });

            }
            function getUserInfo() {
                        $.ajax({

                            url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
                            data: null,
                            success: function (resp) {
                                user = resp;
                                console.log(user);
                                alert('123');
                                $.ajax({
                                    async: true,
                                    type: "post",
                                    url: '/Membership/SocialMembership',
                                    data: JSON.stringify({
                                        "model": {
                                            "MemberName": user.family_name,
                                            "MemberSurname": user.given_name,
                                            "MemberEmail": user.email,
                                            "MemberEmailAgain": user.email,
                                            "LocalityId": "0",
                                            "TownId": "0",
                                            "CityId": "0",
                                            "CountryId": "0",
                                            "AddressTypeId": "0"
                                        },
                                        "MembershipType": "5",
                                        "profileId": user.id,
                                        "fastmembershipType": "30",
                                    }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        if (msg) { window.location.href = window.location.origin; }
                                    },
                                    error: function (jqXHR, exception) {
                                        console.error('error tanımlandı');
                                    }
                                });
                            }
                        })


            }
            $(".googlegogin").click(function () {
                GoogleLogin();
            });
        });
    })(jQuery);
</script>