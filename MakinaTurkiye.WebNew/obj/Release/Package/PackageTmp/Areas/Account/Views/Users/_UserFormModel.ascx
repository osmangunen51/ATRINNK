<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Users.MTUserFormModel>" %>

<div><%:Html.ValidationSummary() %></div>

<div class="form-group ">
    <label class="col-sm-3 control-label">
        E-Posta Adresi
    </label>
    <div class="col-sm-8 col-md-7">
        <%:Html.TextBoxFor(x=>x.MemberEmail, new {@class="mt-form-control col-md-12"}) %>
    </div>
</div>
<div class="form-group ">
    <label class="col-sm-3 control-label">
        Şifre
    </label>
    <div class="col-sm-8 col-md-7">
        <%:Html.PasswordFor(x=>x.MemberPassword, new {@class="mt-form-control"}) %>
    </div>
</div>
<div class="form-group ">
    <label class="col-sm-3 control-label">
        İsim
    </label>
    <div class="col-sm-8 col-md-7">
        <%:Html.TextBoxFor(x=>x.MemberName, new {@class="mt-form-control col-md-12"}) %>
    </div>
</div>
<div class="form-group ">
    <label class="col-sm-3 control-label">
        Soyisim
    </label>
    <div class="col-sm-8 col-md-7">
        <%:Html.TextBoxFor(x=>x.MemberSurname, new {@class="mt-form-control"}) %>
    </div>
</div>
<div class="form-group ">
    <label class="col-sm-3 control-label">
        Cinsiyet
    </label>
    <div class="col-sm-8 col-md-7">
        <div class="radio">
            <label>
                <input type="radio" value="1" name="MTUserFormModel.Gender" class="MTUserFormModel_Gender" />
                Erkek
            </label>
        </div>

        <div class="radio">
            <label>
                <input type="radio" value="0" name="MTUserFormModel.Gender" class="MTUserFormModel_Gender" />
                Kadın
            </label>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-3">
        <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px;">
            <img src="/Content/images/load.gif" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
        </div>
    </div>
    <div class="col-md-3">
        <button type="submit" class="btn btn-success">Ekle</button>
    </div>
</div>
