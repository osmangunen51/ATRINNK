<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LoginModel>" %>

<meta name="google-signin-client_id" content="638267544487-nqpu1s475ju88si2rpper76f0rs5e03o.apps.googleusercontent.com">
<form action="javascript:void(0)" id="login-form">


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
    


<%--              <div id="gSignInWrapper">
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