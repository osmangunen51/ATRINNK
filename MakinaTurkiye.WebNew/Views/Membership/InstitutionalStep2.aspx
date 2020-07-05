﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
           
            var countryId = $('[data-rel="countryId"] :selected').val();
            var cityWrapper = $('[data-rel="city-wrapper"]');
            var addressWrapper = $('[data-rel="address-wrapper"]');
            var localityWrapper = $('[data-rel="locality-wrapper"]');
            var districtWrapper = $('[data-rel="district-wrapper"]');
            var otherElementsWrapper = $('[data-rel="other-wrapper"]');
            var phoneWrapper = $('[data-rel="phone-wrapper"]');
            var gsmWrapper = $('[data-rel="gsm-wrapper"]');
            $("#MembershipModel_CountryId").change(function () {
                if($(this).val()=="246")
                {
                    $("#MembershipModel_InstitutionalGSMCulture").val("+90");
                }

            })
            if (countryId == "246")
            {
             
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
            if (countryId > 0 && countryId != 246)
        {
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
        $('[data-rel="form-submit').click(function () {
            var telNo = $("#MembershipModel_InstitutionalPhoneNumber").val();
            var gsm = $("#MembershipModel_InstitutionalGSMAreaCode").val();
            var gsmAna = $("#MembershipModel_InstitutionalGSMNumber").val();
            var sehirId = $("#MembershipModel_CityId").val();
                if (gsmAna == "" || gsm == 0) {
                    $("#gsmHata").show();
            }
                if (sehirId == 34 && telNo != "") {
                $("#telefonHata").show();
                
            }

                    

        });

        $('#MembershipModel_InstitutionalPhoneNumber').keyup(function () {
         
            if ($(this).val() != "")
                $("#DropDownInstitutionalPhoneAreaCode").attr("data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]");


        });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h4 class="mt0">
                <span class="glyphicon glyphicon-user" style="padding-right: 5px;"></span>Firma Adres Ve İletişim Bilgilerini Ekleyin
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7 col-md-8 pr">
    <div class="row hidden-xs">
        <div class="col-xs-12">
            <ol class="breadcrumb breadcrumb-mt">
                <li><a href="/">Anasayfa </a></li>
                <li class="active">Kurumsal Üyelik 2. Adım </li>
            </ol>
        </div>
    </div>
            <div>
                <div class="well well-mt2">
                    <%: Html.ValidationSummary(true, "", new Dictionary<string, object> { { "class", "alert alert-danger alert-dismissable" } })%>
                    <%using (Html.BeginForm("KurumsalUyelik/Adim-2", "Uyelik", FormMethod.Post, new { id = "formFastMembership", @class = "form-horizontal", @role = "form" }))
                      {%>
                    <div class="tab-content">
                        <div id="bireysel" class="tab-pane active">
                            <div class="form-group">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    Adres Tipi
                                </label>
                                <div class="col-sm-6">
                                    <%:Html.DropDownListFor(model => model.MembershipModel.AddressTypeId, Model.MembershipModel.AddressTypeItems,
                                    new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    Ülke
                                </label>
                                <div class="col-sm-6">
                                    <%:Html.DropDownListFor(model => model.MembershipModel.CountryId, Model.CountryItems,
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
                                    <%:Html.DropDownListFor(model => model.MembershipModel.CityId, Model.CityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "cityId" } })%>
                                </div>
                            </div>
                            <div class="form-group" style="display: none" data-rel="locality-wrapper">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    İlçe
                                </label>
                                <div class="col-sm-6">
                                    <%:Html.DropDownListFor(model => model.MembershipModel.LocalityId, Model.CityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "localityId" } })%>
                                </div>
                            </div>
                            <div class="form-group" style="display: none" data-rel="district-wrapper">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    Mahalle / Köy
                                </label>
                                <div class="col-sm-6">
                                    <select id="MembershipModel_Towns" data-rel="mahalle" class="form-control" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]">
                                    </select>
                                    <input type="hidden" id="MembershipModel_TownId" name="MembershipModel.TownId" />
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
                                    <%:Html.TextBoxFor(model => model.MembershipModel.Avenue,
                                    new Dictionary<string, object> { { "class", "form-control" } })%>
                                </div>
                            </div>
                            <div class="form-group" style="display: none" data-rel="other-wrapper">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    Sokak
                                </label>
                                <div class="col-sm-6">
                                    <%:Html.TextBoxFor(model => model.MembershipModel.Street,
                                    new Dictionary<string, object> { { "class", "form-control" } })%>
                                </div>
                            </div>
                            <div class="form-group" style="display: none" data-rel="other-wrapper">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    No
                                </label>
                                <div class="col-sm-2">
                                    <%:Html.TextBoxFor(model => model.MembershipModel.ApartmentNo, 
                                    new Dictionary<string, object> { { "class", "form-control" }, { "maxLength", "6" } })%>
                                </div>
                                <label for="inputPassword3" class="col-sm-1 control-label" style="width: 20px!important;">
                                 /
                                </label>
                                <div class="col-sm-2">
                                    <%:Html.TextBoxFor(model => model.MembershipModel.DoorNo, 
                                    new Dictionary<string, object> { { "class", "form-control" }, {"maxLength","4"} }  )%>
                                </div>
                            </div>
                        </div>
                                    <div class="form-group" data-rel="gsm-wrapper">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                GSM :
                            </label>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalGSMCulture,
                                new Dictionary<string,object>{{ "class" , "form-control" },{"data-validation-engine","validate[required]"}})%>
                            </div>
                            <div class="col-sm-2">
                                <select id="MembershipModel_InstitutionalGSMAreaCode" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]" class="form-control" name="MembershipModel.InstitutionalGSMAreaCode">
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
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalGSMNumber, new Dictionary<string,object>{{ "class" , "form-control" },{"data-validation-engine","validate[required]"}})%>
                            </div>
                        </div>
                 
                        <div class="form-group" style="display: none" data-rel="phone-wrapper">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Telefon (1)* :
                            </label>
                            <div class="col-sm-2">
                                <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode" name="MembershipModel.InstitutionalPhoneAreaCode" />
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneCulture,new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" } })%>
                            </div>
                            <div class="col-sm-2">
                                <select id="DropDownInstitutionalPhoneAreaCode" name="DropDownInstitutionalPhoneAreaCode"
                                    style="display: none" class="form-control">
                                    <option value="0">Seç</option>
                                    <option value="212">212</option>
                                    <option value="216">216</option>
                                </select>
                                <%:Html.TextBox("TextInstitutionalPhoneAreaCode", Model.MembershipModel.InstitutionalPhoneAreaCode,
                                new { @class = "form-control" })%>
                            </div>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneNumber, 
                                new Dictionary<string, object> { { "class", "form-control" }})%>
                            </div>
                        </div>
                        <div class="form-group" id="telefonHata" style="display: none;">
                                <div class="col-sm-3"></div>
                                <div class="col-sm-5">
                                <div class="alert alert-danger alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                &times;
                                            </button>
                                            <strong>Hata! </strong>
                                                    Telefon Alan Kodu Zorunludur
                                        </div>
                                </div>
                              </div>
                        <div class="form-group" style="display: none" data-rel="phone-wrapper">
                            <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode2" name="MembershipModel.InstitutionalPhoneAreaCode2" />
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Telefon (2) :
                            </label>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneCulture2,
                                new { @class = "form-control" })%>
                            </div>
                            <div class="col-sm-2">
                                <select id="DropDownInstitutionalPhoneAreaCode2" name="DropDownInstitutionalPhoneAreaCode2"
                                    style="display: none" class="form-control">
                                    <option value="0">Seç</option>
                                    <option value="212">212</option>
                                    <option value="216">216</option>
                                </select>
                                <%:Html.TextBox("TextInstitutionalPhoneAreaCode2", Model.MembershipModel.InstitutionalPhoneAreaCode2,new { @class = "form-control"})%>
                            </div>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneNumber2, 
                                new { @class = "form-control" })%>
                            </div>
                        </div>
                        <div class="form-group" style="display: none" data-rel="phone-wrapper">
                            <input type="hidden" id="MembershipModel_InstitutionalFaxAreaCode" name="MembershipModel.InstitutionalFaxAreaCode" />
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Fax :
                            </label>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalFaxCulture, 
                                new { @class = "form-control" })%>
                            </div>
                            <div class="col-sm-2">
                                <select id="DropDownInstitutionalFaxAreaCode" name="DropDownInstitutionalFaxAreaCode"
                                    style="display: none" class="form-control">
                                    <option value="0">Seç</option>
                                    <option value="212">212</option>
                                    <option value="216">216</option>
                                </select>
                                <%:Html.TextBox("TextInstitutionalFaxAreaCode", Model.MembershipModel.InstitutionalFaxAreaCode, 
                                new { @class = "form-control" })%>
                            </div>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalFaxNumber, 
                                new { @class = "form-control" })%>
                            </div>
                        </div>
               
                        <hr />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <a href="/Uyelik/KurumsalUyelik/Adim-1">
                                <button class="btn btn-primary" type="button">
                                    Önceki Adım
                                </button>
                            </a>
                            <button class="btn btn-success" data-rel="form-submit" type="submit">
                                Sonraki Adım
                            </button>
                        </div>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
        <%CompanyDemandMembership model1 = new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
    </div>
</asp:Content>
