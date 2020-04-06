﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.MemberShip.MTMembershipFormModel>" %>



<div class="form-group">
    <label>Email*</label>
    <%:Html.TextBoxFor(x=>x.MemberEmail, new {@class="mt-form-control", placeholder="Email*", @type="email" }) %>
     <small><%:Html.ValidationMessageFor(x=>x.MemberEmail) %></small>
</div>
<div class="form-group">
    <label>Şifre*</label>
    <%:Html.PasswordFor(x=>x.MemberPassword,new {@placeholder="Şifre*", @class="mt-form-control" }) %>
    <small><%:Html.ValidationMessageFor(x=>x.MemberPassword) %></small>
</div>
<div class="form-group">
    <label>İsim*</label>
    <%:Html.TextBoxFor(x=>x.Name, new {@placeholder="İsim*", @class="mt-form-control" }) %>
    <small><%:Html.ValidationMessageFor(x=>x.Name) %></small>
</div>
<div class="form-group">
    <label>Soyisim*</label>
    <%:Html.TextBoxFor(x=>x.Surname, new {@placeholder="Soyisim*", @class="mt-form-control" }) %>
       <small><%:Html.ValidationMessageFor(x=>x.Surname) %></small>
</div>
<div class="form-group">
    <div class="g-recaptcha" data-sitekey="6LemzhUUAAAAAMgP3NVU5i2ymC5iC_3bVvB466Xh"></div>

</div>

<button class="btn btn-sign-up" type="submit">
    Üye Ol
</button>
<div class="help-block">
    Üyelik kaydımı oluşturarak:
                                <br>
    <a href="/kullanim-kosullari-y-141618">Üyelik Sözleşmesi </a>Koşulları'nı ve <a href="/gizlilik-ve-guvenlik-y-142519">Gizlilik Politikası
    </a>'nı kabul ediyorum.
</div>
<hr>

<div class="form-group">
    <a class="btn  btn-info btn-facebook-login js-facebook-login" href="javascript:;"
        data-max-rows="1" data-scope="email,public_profile" data-size="medium" data-show-faces="false" data-auto-logout-link="true"
        style="color: #fff; border-color: #3B5998; background: #3B5998; width: 100%;"><i class="fa fa-facebook fa-fw"></i>Facebook ile bağlan
    </a>
</div>
