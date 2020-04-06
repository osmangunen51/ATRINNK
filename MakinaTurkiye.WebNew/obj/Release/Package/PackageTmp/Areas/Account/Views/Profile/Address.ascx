<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>
<div class="form-group">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Ülke
    </label>
    <div class="col-sm-6">
        <%:Html.DropDownListFor(model => model.CountryId, Model.CountryItems,
        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "countryId" } })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="address-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Adres
    </label>
    <div class="col-sm-6">
        <textarea class="form-control" name="MembershipModel_AvenueOtherCountries"></textarea>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="city-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Şehir
    </label>
    <div class="col-sm-6">
        <%:Html.DropDownListFor(model => model.CityId, Model.CityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "cityId" } })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="locality-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        İlçe
    </label>
    <div class="col-sm-6">
        <%:Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "localityId" } })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="district-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Mahalle / Köy
    </label>
    <div class="col-sm-6">
        <%:Html.DropDownListFor(model => model.TownId, Model.TownItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "mahalle" } })%>
        <input type="hidden" id="MembershipModel_TownId" name="TownId" />
    </div>
</div>
<div class="form-group" style="display: none" data-rel="other-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Posta Kodu
    </label>
    <div class="col-sm-6">
        <input type="text" class="form-control" name="MembershipModel.ZipCode" disabled="disabled" />
    </div>
</div>
<div class="form-group" style="display: none" data-rel="other-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Cadde
    </label>
    <div class="col-sm-6">
        <%:Html.TextBoxFor(model => model.Avenue,
                                    new Dictionary<string, object> { { "class", "form-control" } })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="other-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Sokak
    </label>
    <div class="col-sm-6">
        <%:Html.TextBoxFor(model => model.Street,
                                    new Dictionary<string, object> { { "class", "form-control" } })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="other-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Bina No
    </label>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.ApartmentNo, 
                                    new Dictionary<string, object> { { "class", "form-control" }, { "maxLength", "6" } })%>
    </div>
    <label for="inputPassword3" class="col-sm-2 control-label">
        Kapı No
    </label>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.DoorNo, 
                                    new Dictionary<string, object> { { "class", "form-control" }, {"maxLength","4"} }  )%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="phone-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Telefon (1) :
    </label>
    <div class="col-sm-2">
        <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode" name="InstitutionalPhoneAreaCode" />
        <%:Html.TextBoxFor(model => model.InstitutionalPhoneCulture,new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" } })%>
    </div>
    <div class="col-sm-2">
        <select id="DropDownInstitutionalPhoneAreaCode" name="DropDownInstitutionalPhoneAreaCode" style="display: none"
            class="form-control" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]">
            <option value="0">Seç</option>
            <option value="212">212</option>
            <option value="216">216</option>
        </select>
        <%:Html.TextBox("TextInstitutionalPhoneAreaCode", Model.InstitutionalPhoneAreaCode,
                                new { @class = "form-control" })%>
    </div>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalPhoneNumber, 
                                new Dictionary<string, object> { { "class", "form-control" }})%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="phone-wrapper">
    <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode2" name="InstitutionalPhoneAreaCode2" />
    <label for="inputPassword3" class="col-sm-3 control-label">
        Telefon (2) :
    </label>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalPhoneCulture2,
                                new { @class = "form-control" })%>
    </div>
    <div class="col-sm-2">
        <select id="DropDownInstitutionalPhoneAreaCode2" name="DropDownInstitutionalPhoneAreaCode2" style="display: none"
            class="form-control">
            <option value="0">Seç</option>
            <option value="212">212</option>
            <option value="216">216</option>
        </select>
        <%:Html.TextBox("TextInstitutionalPhoneAreaCode2", Model.InstitutionalPhoneAreaCode2,new { @class = "form-control"})%>
    </div>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalPhoneNumber2, 
                                new { @class = "form-control" })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="phone-wrapper">
    <input type="hidden" id="MembershipModel_InstitutionalFaxAreaCode" name="InstitutionalFaxAreaCode" />
    <label for="inputPassword3" class="col-sm-3 control-label">
        Fax :
    </label>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalFaxCulture, 
                                new { @class = "form-control" })%>
    </div>
    <div class="col-sm-2">
        <select id="DropDownInstitutionalFaxAreaCode" name="DropDownInstitutionalFaxAreaCode" style="display: none"
            class="form-control">
            <option value="0">Seç</option>
            <option value="212">212</option>
            <option value="216">216</option>
        </select>
        <%:Html.TextBox("TextInstitutionalFaxAreaCode", Model.InstitutionalFaxAreaCode, 
                                new { @class = "form-control" })%>
    </div>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalFaxNumber, 
                                new { @class = "form-control" })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="gsm-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        GSM :
    </label>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalGSMCulture,
                                new { @class = "form-control" })%>
    </div>
    <div class="col-sm-2">
        <select id="MembershipModel_InstitutionalGSMAreaCode" class="form-control" name="InstitutionalGSMAreaCode">
            <option value="0">Seç</option>
            <option value="530">530</option>
            <option value="531">531</option>
            <option value="532">532</option>
            <option value="533">533</option>
            <option value="534">534</option>
            <option value="535">535</option>
            <option value="536">536</option>
            <option value="537">537</option>
            <option value="538">538</option>
            <option value="539">539</option>
            <option value="541">541</option>
            <option value="542">542</option>
            <option value="543">543</option>
            <option value="544">544</option>
            <option value="545">545</option>
            <option value="546">546</option>
            <option value="547">547</option>
            <option value="548">548</option>
            <option value="549">549</option>
            <option value="501">501</option>
            <option value="505">505</option>
            <option value="506">506</option>
            <option value="507">507</option>
            <option value="551">551</option>
            <option value="552">552</option>
            <option value="553">553</option>
            <option value="554">554</option>
            <option value="555">555</option>
        </select>
    </div>
    <div class="col-sm-2">
        <%:Html.TextBoxFor(model => model.InstitutionalGSMNumber, new { @class = "form-control" })%>
    </div>
</div>
<div class="form-group" style="display: none" data-rel="gsm-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Operatör Seçiniz :
    </label>
    <div class="col-sm-2">
        <%=Html.RadioButton("GsmType", 1, false, new { id = "gsmVodafone" })%>Vodafone
    </div>
    <div class="col-sm-2">
        <%=Html.RadioButton("GsmType", 3, false, new { id = "gsmTurkcell" })%>Turkcell
    </div>
    <div class="col-sm-2">
        <%=Html.RadioButton("GsmType", 2, false, new {  id = "gsmAvea" })%>Avea
    </div>
</div>
