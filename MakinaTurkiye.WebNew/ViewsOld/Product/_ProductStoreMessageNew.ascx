<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductStoreMessageModel>" %>

                             <div class="urun-iletisim-body">
                               <div class="row" style="margin-top:10px;">
                            <div class="col-md-12">
                         
                                 <div class="form-group row" id="mail-wrapper">
                                           <input type="hidden" id="country" />
                                     <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Email<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
     <%= Html.TextBoxFor(model => model.Email,
                                  new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "type", "email" },
                                  { "id", "MemberEmail" },
                                  { "data-validation-engine", "validate[required,custom[email]]" },
                                  { "data-errormessage-value-missing", "Email alanı zorunludur!" },
                                  { "data-errormessage-custom-error", "Örneğin: info@makinaturkiye.com" },
                                  { "placeholder", "Mail Adresiniz" } , {"data-rel", "Email"}})%>
                                      </div>
                                
                                 </div>
                                    <div class="form-group row" id="password-wrapper" style="display: none;">
                                         <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Şifre<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                                  <%= Html.TextBoxFor(model => model.MemberPassword,
                                  new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "type", "password" },
                                  { "id", "MemberPassword" },
                                  { "data-validation-engine", "validate[required]" },
                                  { "data-errormessage-value-missing", "Şifre zorunludur!" },
                                  { "data-errormessage-custom-error", "Şifreniz" },
                                  { "placeholder", "Şifreniz" }})%>
                                       </div>
                                         </div>
                                <div class="form-group row" id="activation-wrapper" style="display: none;">
                                        <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Tek Kullanımlık Şifre<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                       <%= Html.TextBox("LoginActivationCode","",
                                  new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "type", "text" },
                                  { "id", "ActivationCodeLogin" },
                                  { "data-validation-engine", "validate[required]" },
                                  { "data-errormessage-value-missing", "Tek Kullanımlık Şifreniz" },
                                  { "data-errormessage-custom-error", "Şifreniz" },
                                  { "placeholder", "Telefonunuza gelen tek kullanımlık şifreniz" }})%>
                                      </div>
                                </div>
                                 <div class="form-group row" id="mailname" style="display:none;">
                                       <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">İsim<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                                   <%= Html.TextBoxFor(model => model.MemberName,
                                                 new Dictionary<string, object> { { "class", "form-control" }, { "id", "MemberName" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen adınızı giriniz" } })%>
                                      </div>
                                     </div>
                                    <div class="form-group row" id="mailsurname" style="display:none;">
                                            <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Soyisim<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                                 <%= Html.TextBoxFor(model => model.MemberSurname,
                                                         new Dictionary<string, object> { { "class", "form-control" }, { "id", "MemberSurname" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen Soyadınızı giriniz" } })%>
                                      </div>
                                    </div>
                                    <div id="mailtelephone" class="form-group row" style="display:none;">
                                            <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Cep Telefon No<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                              <%= Html.TextBoxFor(model => model.PhoneNumber,
                                                         new Dictionary<string, object> { { "class", "form-control" }, { "id", "PhoneNumber" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Örn:5334462514"},{"maxlength","11"}})%>
                                           </div>
                                        </div>
                                <div class="form-group row" id="mailtitle" style="display:none;">
                                            <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Konu<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                                 <%= Html.TextBoxFor(model => model.MailTitle,
                                                                 new Dictionary<string, object> { { "class", "form-control" }, { "id", "mailTitle" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Konu" } })%>
                                      </div>
                                </div>
                                 <div class="form-group row" id="maildescription" style="display:none;">
                                       <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Mesaj<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                         <%:Html.TextAreaFor(model => model.MailDescription, new { maxLength = 200, @class = "form-control", id = "mailDescription", rows = "4", cols = "50", @style = "height:100px;!important" })%>
                                      </div>
                                      </div>
                        
                  
                    <div id="PhoneActivation-wrapper" class="input-group" style="display: none;">
                
                <label id="phoneActivationLabel" class="col-md-12 input">Gelen Aktivasyon Kodu</label>
                <%:Html.TextBox("activationCode", "", new {@class="col-md-12 form-control",@id="activationcode" })%>
                 
       </div>
             <div id="noActivationWarm" style="display:none; margin-top:10px;" >
                 <div class="col-md-6" id="activationWarm-sub">
               Aktivasyon Kodu Ulaşmadı? &nbsp;
                 <a href="javascript:void(0)" style="text-decoration:underline;" onclick="NewPhoneNumberWrapperShow();">Yeni Numara Girişi</a><br />
                 <span style="font-size:10px;">Uyarı:Lütfen Telefon Numaranızı başında 0 olmadan giriniz.Eğer hala hata olduğunu düşünüyorsanız bize ulaşabilirsiniz.</span>
                </div>
                 <div class="col-md-6">
                     <input type="checkbox" name="mailActivation" id="mailActivation" /> <b>Mail İle Aktivasyon İstiyorum</b>
                     <input type="hidden" name="mailActivationValue" id="mailActivationValue" />
                 </div>
                 </div>
             <div id="noActivationWrapper" style="display:none;">
                                     <div id="noActivationPhone" class="form-group row">
                                            <label for="exampleFormControlInput1" class="col-sm-2 col-form-label">Cep Telefon No<span style="color:#c61717;">(*)</span></label>
                                      <div class="col-sm-10">
                                              <%= Html.TextBoxFor(model => model.PhoneNumber,
                                                         new Dictionary<string, object> { { "class", "form-control" }, { "id", "PhoneNumberAgain" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Örn:5334462514"},{"maxlength","11"}})%>
                                           </div>
                                        </div>
                                <input type="hidden" id="messageIdHidden" />
                                <input type="button" value="Gönder" id="btnNewActivationCode" class="btn btn-primary background-bt btn-block btn-bg col-md-3" style="float:right;" />

                            </div>    
 
<%--    <script>
        window.fbAsyncInit = function () {
            FB.init({
                appId: '333739580327457',
                xfbml: true,
                version: 'v2.8'
            });
            FB.AppEvents.logPageView();
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    </script>--%>

            
          <%--  <div class="alert alert-danger" id="mailinfo"> <i class="glyphicon glyphicon-exclamation-sign"></i>&nbsp; <span>Mesaj gönderebilmek için üye olmalısınız üyelik sayfasına yönlendiriliyorsunuz...</span></div>--%>
                    
                       </div>

                    </div>
                    <!-- Body -->

                    <!-- Footer -->
                    <div class="urun-iletisim-footer">
                        <div class="row">
                            <div class="col-md-8">
                                  <div class="alert alert-success alert-dismissable" id="mailSuccess" style="display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        Sisteme kayıtlı e-mail adresi bulundu.<br />
                 Giriş için telefonuma aktivasyon kodu <a id="SendLoginActivate" href="javascript:void(0)">Gönder</a>
                       
                    </div>
                
            <div class="alert alert-success alert-dismissable" id="mailActivationSuccess" style="display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        <strong>Başarılı! </strong>
                           Girmiş olduğunuz mail adresine aktivasyon kodu gönderilmiştir.
                    </div>
                    <div class="alert alert-success alert-dismissable" id="successMessage" style="display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        <strong>Başarılı! </strong>
                               Mesajınız Gönderilmiştir
                       
                    </div>
                    <div class="alert alert-success alert-dismissable" id="loginSuccess" style="display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        <strong>Başarılı! </strong>
                               Giriş Yapılıyor...
                       
                    </div>
                    <div class="alert alert-danger alert-dismissable" id="loginError" style="display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        <strong>Hata! </strong>
                               Girmiş olduğunuz şifre bulunamadı.<a href="/Uyelik/SifremiUnuttum">Şifremi Unuttum</a>
                       
                    </div>
                    <div class="alert alert-danger alert-dismissable" id="errorMessage" style="display: none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        <strong>Hata! </strong>
                               Aktivasyon kodunuz uyuşmamıştır.<%--<a href="javascript:void(0)" onclick="Redirect()">Tamam</a>--%>
                       
                    </div>
                               <div class="alert alert-warning" role="alert" id="messageSignWarm" style="display:none;">
                         Sisteme kayıtlı e-mail adresi bulundu.Şifrenizi girerek giriş yapabilirsiniz.<br />
                                   <a href="/Uyelik/SifremiUnuttum">Şifremi Hatırlamıyorum?</a>
</div>
                                </div>
                            <div class="col-md-4">
                                        <button type="button" id="signUpButton" style="display: none;" class="btn  background-mt-btn btn-block btn-bg">
            Giriş Yap</button> 
                                                           <button type="button" id="redirectbtn" class="btn  background-mt-btn btn-block btn-bg">
            Devam Et</button> 
                    <a style="display: none;" class="btn btn-xs btn-info fb-login-button" href="javascript:;"
                        data-max-rows="1" data-scope="email,public_profile" data-size="medium" data-show-faces="false" data-auto-logout-link="true"
                        id="facebookLogin" style="color: #fff; border-color: #3B5998; background: #3B5998;"><i class="fa fa-facebook fa-fw"></i>Facebook ile bağlan
                    </a>
            <div id="fb-root"></div>
                            </div>
                        </div>
    
               <%--         <div class="row">
                            <div class="col-sm-6">
                                <a onclick="MessageSend('mesaj')" class="btn btn-primary btn-block btn-bg" style="margin-top: 5px;"><i class="glyphicon glyphicon-envelope"></i>&nbsp; Mesaj Gönder Soru Sor</a>
                            </div>
                            <div class="col-sm-6">
                                <a  onclick="MessageSend('Teklif')" class="btn btn-danger btn-block btn-bg" style="margin-top: 5px;"><i class="fa fa-fw fa-gavel"></i>&nbsp;Teklif İste</a>
                            </div>
                        </div>--%>
                    </div>   
                                 </div>