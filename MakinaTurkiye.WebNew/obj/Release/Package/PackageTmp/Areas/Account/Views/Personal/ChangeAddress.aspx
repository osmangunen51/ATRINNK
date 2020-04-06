﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AddressModel>" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Adresimi Değiştir-Makina Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var cityId = $('[data-rel="cityId"] :selected').val();
            var areaCode = $('[data-rel="localityId"] :selected').val();
            var countryId = $('[data-rel="countryId"] :selected').val();
            var cityWrapper = $('[data-rel="city-wrapper"]');
            var addressWrapper = $('[data-rel="address-wrapper"]');
            var localityWrapper = $('[data-rel="locality-wrapper"]');
            var districtWrapper = $('[data-rel="district-wrapper"]');
            var otherElementsWrapper = $('[data-rel="other-wrapper"]');
            var phoneWrapper = $('[data-rel="phone-wrapper"]');
            var gsmWrapper = $('[data-rel="gsm-wrapper"]');
            if (countryId != 246 && countryId > 0) {
                cityWrapper.slideUp();
                localityWrapper.slideUp();
                districtWrapper.slideUp();
                otherElementsWrapper.slideUp();
                addressWrapper.slideDown();
                gsmWrapper.slideUp();
                $('[data-rel="cityId"]' + " > option").remove();
                $('#TextInstitutionalPhoneAreaCode2').keyup(function () {
                    $('#InstitutionalPhoneAreaCode2').val($(this).val());
                });
                $('#TextInstitutionalPhoneAreaCode2').val($("#InstitutionalPhoneAreaCode2").val());
                $('#MembershipModel_InstitutionalPhoneCulture,#MembershipModel_InstitutionalPhoneCulture2,#MembershipModel_InstitutionalFaxCulture').val("");
                $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
            }
            else {
                if (cityId != "") { $("#other-wrapper").show(); }
                if (cityId == 34) {//istanbul
                    $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').show();
                    $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val('').hide();
                } else {
                    $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').show();

                    $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
                }

                $('#DropDownInstitutionalPhoneAreaCode').change(function () {

                    $("#InstitutionalPhoneAreaCode").val($(this).val());

                });

                $('#DropDownInstitutionalPhoneAreaCode2').change(function () {

                    $("#InstitutionalPhoneAreaCode2").val($(this).val());

                });
                $("#MembershipModel_InstitutionalGSMAreaCode").change(function () {

                    if ($(this).val() != 0) {
                        $("#InstitutionalGSMAreaCode").val($(this).val());
                    }
                });
                $('#DropDownInstitutionalFaxAreaCode').change(function () {

                    $("#InstitutionalFaxAreaCode").val($(this).val());

                });
                $("#InstitutionalPhoneNumber").keyup(function () {
                    if ($(this).val() != "")
                        $('#DropDownInstitutionalPhoneAreaCode').attr("data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]");
                    else
                        $('#DropDownInstitutionalPhoneAreaCode').removeAttr("data-validation-engine");

                });
                $("#InstitutionalPhoneNumber2").keyup(function () {
                    if ($(this).val() != "")
                        $('#DropDownInstitutionalPhoneAreaCode2').attr("data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]");
                    else
                        $('#DropDownInstitutionalPhoneAreaCode2').removeAttr("data-validation-engine");

                });

            }


        });

        function UpdatePhoneOpen() {
            $("#gsmUpdateWrapper").slideDown();

        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%string baslik = "Adres";
        if (ViewData["error"] != null)
        {
            if (ViewData["error"].ToString() == "PhoneUpdate")
            {
                baslik = "Telefon";
            }
            else if (ViewData["error"].ToString() == "PhoneActive")
            {
                baslik = "Telefon";
            }

        }
    %>
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;<%if (ViewData["gelenSayfa"] == "Teklif")
                                                                                    { %>
                <%:baslik %> Bilgileri
                    <%}
                        else
                        {
                            Response.Write(baslik + " Bilgilerimi Güncelle");
                        } %>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>

                <%if (ViewData["gelenSayfa"] == "kurumsalaGec" && ViewData["error"].ToString() != "PhoneActive")
                    {%>
                <div class="alert alert-warning alert-dismissable" style="font-size: 15px;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                    </button>
                    Kurumsal üye olabilmek için adres bilgileriniz güncellemelisiniz.
                </div>
                <% } %>
                <%if (ViewData["success"] == "true")
                    { %>
                <div class="alert alert-success alert-dismissable" style="font-size: 15px;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                    </button>
                    Bilgileriniz başarıyla güncellenmiştir.
                </div>
                <%} %>

                <%if (ViewData["error"].ToString() == "PhoneActive")
                    { %>
                <div class="alert alert-warning alert-dismissable" style="font-size: 15px;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                    </button>
                    <%
                        var  gsmItem = Model.GsmPhone;
                        Phone wgsmItem = new Phone();
                        if (ViewData["whatsappPhone"] != null)
                        {
                            wgsmItem = (Phone)ViewData["whatsappPhone"];
                        }




                        if (ViewData["gelenSayfa"] == "kurumsalaGec")
                        {
                    %>
                                Teklif veya Mesaj gönderebilmek için <b class="btn btn-default"><%:gsmItem.PhoneCulture +" "+gsmItem.PhoneAreaCode+" "+gsmItem.PhoneNumber %></b> cep telefon numarasını onaylamanız gerekmektedir.<br />
                    <a class="btn btn-primary" href="<%:Url.Content("~/Account/Personal/PhoneActive/?gelenSayfa="+ViewData["gelenSayfa"]) %>">Onayla</a>


                    <% }
                        else
                        {
                    %>
                        Teklif veya Mesaj gönderebilmek için <b class="btn btn-default"><%:gsmItem.PhoneCulture +" "+gsmItem.PhoneAreaCode+" "+gsmItem.PhoneNumber %></b> cep telefon numarasını onaylamanız gerekmektedir.<br />
                    <a class="btn btn-primary" href="<%:Url.Content("~/Account/Personal/PhoneActive/?UrunNo="+ViewData["urunNo"]+"&uyeNo="+ViewData["uyeNo"]+"&typePage="+ViewData["mtypePage"]+"") %>">Onayla</a>
                    <%} %>
                </div>
                <div class="alert alert-warning alert-dismissable" style="font-size: 15px;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                    </button>
                    Telefon numarası size ait değil ise güncellemek için
                    <div class="btn btn-success" onclick="UpdatePhoneOpen()" id="btnPhoneGuncelle">Tıklayınız</div>
                </div>
                <div class="" id="gsmUpdateWrapper" style="display: none;">

                    <%using (Html.BeginForm("ChangeAddress", "Personal", FormMethod.Post, new { @id = "formFastMembership1", @role = "form", @class = "form-horizontal" }))
                        {%>
                    <div class="col-xs-12 well store-panel-container">
                        <div id="bireysel" class="tab-pane">
                            <input type="hidden" name="mtypePage" value="<%:ViewData["mtypePage"] %>" />
                            <input type="hidden" name="uyeNo" value="<%:ViewData["uyeNo"] %>" />
                            <input type="hidden" name="urunNo" value="<%:ViewData["urunNo"] %>" />
                            <input type="hidden" name="gelSayfa" value="<%:ViewData["gelenSayfa"] %>" />
                            <div class="form-group">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                    GSM :
                                </label>
                                <div class="col-sm-2">
                                    <input type="text" data-validation-engine="validate[required]" name="InstitutionalGSMCulture" id="InstitutionalGSMCulture" value="<%:gsmItem.PhoneCulture%>"
                                        class="form-control" />
                                </div>
                                <div class="col-sm-2">
                                    <select class="form-control" id="InstitutionalGSMAreaCode" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]" name="InstitutionalGSMAreaCode">
                                        <option value="0" <%:gsmItem!= null  ? "selected=\"selected\"" : "" %>>Seç</option>
                                        <option value="530" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "530" ? "selected=\"selected\"" : "" %>>530</option>
                                        <option value="531" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "531" ? "selected=\"selected\"" : "" %>>531</option>
                                        <option value="532" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "532" ? "selected=\"selected\"" : "" %>>532</option>
                                        <option value="533" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "533" ? "selected=\"selected\"" : "" %>>533</option>
                                        <option value="534" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "534" ? "selected=\"selected\"" : "" %>>534</option>
                                        <option value="535" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "535" ? "selected=\"selected\"" : "" %>>535</option>
                                        <option value="536" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "536" ? "selected=\"selected\"" : "" %>>536</option>
                                        <option value="537" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "537" ? "selected=\"selected\"" : "" %>>537</option>
                                        <option value="538" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "538" ? "selected=\"selected\"" : "" %>>538</option>
                                        <option value="539" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "539" ? "selected=\"selected\"" : "" %>>539</option>
                                        <option value="541" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "541" ? "selected=\"selected\"" : "" %>>541</option>
                                        <option value="542" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "542" ? "selected=\"selected\"" : "" %>>542</option>
                                        <option value="543" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "543" ? "selected=\"selected\"" : "" %>>543</option>
                                        <option value="544" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "544" ? "selected=\"selected\"" : "" %>>544</option>
                                        <option value="545" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "545" ? "selected=\"selected\"" : "" %>>545</option>
                                        <option value="546" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "546" ? "selected=\"selected\"" : "" %>>546</option>
                                        <option value="547" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "547" ? "selected=\"selected\"" : "" %>>547</option>
                                        <option value="548" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "548" ? "selected=\"selected\"" : "" %>>548</option>
                                        <option value="549" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "549" ? "selected=\"selected\"" : "" %>>549</option>
                                        <option value="501" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "501" ? "selected=\"selected\"" : "" %>>501</option>
                                        <option value="505" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "505" ? "selected=\"selected\"" : "" %>>505</option>
                                        <option value="506" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "506" ? "selected=\"selected\"" : "" %>>506</option>
                                        <option value="507" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "507" ? "selected=\"selected\"" : "" %>>507</option>
                                        <option value="551" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "551" ? "selected=\"selected\"" : "" %>>551</option>
                                        <option value="552" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "552" ? "selected=\"selected\"" : "" %>>552</option>
                                        <option value="553" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "553" ? "selected=\"selected\"" : "" %>>553</option>
                                        <option value="554" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "554" ? "selected=\"selected\"" : "" %>>554</option>
                                        <option value="555" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "555" ? "selected=\"selected\"" : "" %>>555</option>
                                        <%if (gsmItem != null)
                                            {
                                                if (gsmItem.PhoneAreaCode != null)
                                                { %>
                                        <%if (int.Parse(gsmItem.PhoneAreaCode) > 555)
                                            {%>
                                        <option value="<%:gsmItem.PhoneAreaCode%>" <%:gsmItem != null && int.Parse(gsmItem.PhoneAreaCode) >= 555 ? "selected=\"selected\"" : ""%>>
                                            <%:gsmItem.PhoneAreaCode%></option>
                                        <%}
                                                }
                                            } %>
                                    </select>
                                </div>
                                <div class="col-sm-2">
                                    <input type="text" data-validation-engine="validate[required]" name="InstitutionalGsmNumber" id="InstitutionalGsmNumber" value="<%:gsmItem.PhoneNumber  %>"
                                        class="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-9">
                                    <button type="submit" name="save" value="TelefonKayit" class="btn btn-primary">
                                        Kaydet ve Gönder
                                    </button>


                                </div>
                            </div>
                        </div>

                    </div>

                    <%} %>
                </div>

                <%}
                    else
                    {
                %>
                <%if (ViewData["gelenSayfa"] == "Teklif")
                    {
                %>
                <div class="alert alert-warning alert-dismissable" data-rel="email-wrapper" style="font-size: 15px;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                    </button>
                    <strong>Uyarı! </strong>
                    Teklif İstemek veya mesaj göndermek için adres bilgilerinizi girmelisiniz.
                </div>
                <%}%>


                <%using (Html.BeginForm("ChangeAddress", "Personal", FormMethod.Post, new { id = "formFastMembership", role = "form", @class = "form-horizontal" }))
                    {%>
                <input type="hidden" name="mtypePage" value="<%:ViewData["mtypePage"] %>" />
                <input type="hidden" name="uyeNo" value="<%:ViewData["uyeNo"] %>" />
                <input type="hidden" name="urunNo" value="<%:ViewData["urunNo"] %>" />
                <input type="hidden" name="gelSayfa" value="<%:ViewData["gelenSayfa"] %>" />
                <div class="col-xs-12 well store-panel-container">
                    <div id="bireysel" class="tab-pane">
                        <% if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
                            {
                        %>
                        <div class="form-group">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Adres Tipi
                            </label>
                            <div class="col-sm-6">
                                <%:Html.DropDownListFor(model => model.AddressTypeId, Model.AddressTypeItems,
                                    new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                            </div>
                        </div>
                        <% }

                        %>

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
                                <textarea class="form-control" name="MembershipModel_AvenueOtherCountries"><%:Model.Avenue %></textarea>
                            </div>
                        </div>
                        <div class="form-group" data-rel="city-wrapper">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Şehir
                            </label>
                            <div class="col-sm-6">
                                <%:Html.DropDownListFor(model => model.CityId, Model.CityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "cityId" } })%>
                            </div>
                        </div>
                        <div class="form-group" data-rel="locality-wrapper">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                İlçe
                            </label>
                            <div class="col-sm-6">
                                <%:Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, 
                                    new Dictionary<string, object> { { "class", "form-control" }, 
                                    { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, 
                                    { "data-rel", "localityId" } })%>
                            </div>
                        </div>
                        <div class="form-group" data-rel="district-wrapper">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Mahalle / Köy
                            </label>
                            <div class="col-sm-6">
                                <%:Html.DropDownListFor(model => model.TownId, Model.TownItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "mahalle" } })%>
                                <input type="hidden" id="MembershipModel_TownId" name="TownId" value="0" />
                            </div>
                        </div>
                        <div class="form-group" data-rel="other-wrapper">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Cadde
                            </label>
                            <div class="col-sm-6">
                                <%:Html.TextBoxFor(model => model.Avenue, 
                                new Dictionary<string, object> { { "class", "form-control" } })%>
                            </div>
                        </div>
                        <div class="form-group" data-rel="other-wrapper">
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
                                No
                            </label>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.ApartmentNo, 
                                    new Dictionary<string, object> { { "class", "form-control" }, { "maxLength", "6" } })%>
                            </div>
                            <label for="inputPassword3" class="col-sm-1 control-label" style="width: 20px!important;">
                                /
                            </label>
                            <div class="col-sm-2">
                                <%:Html.TextBoxFor(model => model.DoorNo, 
                                    new Dictionary<string, object> { { "class", "form-control" }, {"maxLength","4"} }  )%>
                            </div>
                        </div>
                        <%= Html.RenderHtmlPartial("PhoneItem", Model.PhoneItems) %>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <button type="submit" name="save" value="AdresKayit" class="btn btn-primary">
                                    Değişiklikleri Kaydet
                                </button>


                            </div>
                        </div>
                    </div>
                    <hr />
                </div>

                <%}
                %>

                <% } %>
            </div>
        </div>
    </div>
</asp:Content>
