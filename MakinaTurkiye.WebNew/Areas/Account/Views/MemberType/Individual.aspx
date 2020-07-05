<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .page-header { margin: 10px 0 20px; }

        label .required { font-weight: bold; color: #de1b1b; }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#MembershipModel_IsGsmWhatsapp').change(function () {

                $('[data-rel="whatsapp-wrapper"]').slideToggle();

            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%
                NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel lefMenu = (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel)ViewData["leftMenu"];
            %>
            <%= Html.RenderHtmlPartial("LeftMenu",lefMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Bireysel Üyelik
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="col-md-12 col-sm-12">
                <%if (ViewData["error"] != null && ViewData["error"].ToString() == "true")
                    { %>
                <div class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    Aktivasyon kodunuz uyuşmuyor.Telefon Numaranızı güncellemek isterseniz Tıklayınız.
                </div>
                <%} %>
                <div class="well well-mt2">
                    <% if (ViewData["memberType"] != null)
                        {%>
                    <div class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                        Mesajlarınızı görebilmek ve daha iyi iletişim kurmak için bireysel üye olmanız gerekir.
                    </div>
                    <%}
                        else
                        {
                            if (ViewData["mtypePage"] != null)
                            { %>
                    <div class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                        Satıcıdan teklif isteyebilmeniz ve satıcıya soru sorabilmeniz için bilgilerinizi giriniz.
                    </div>
                    <%}
                        }%>
                    <h4>Kişisel Bilgiler</h4>
                    <%if (ViewData["ErrorMessage"] != null)
                        {%>
                    <div class="alert alert-danger alert-dismissable" data-rel="email-wrapper">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                        <strong>Hata! </strong><%: ViewData["ErrorMessage"].ToString()%>
                    </div>
                    <% } %>
                    <%--     <%: Html.ValidationSummary(true, "", new Dictionary<string, object> { { "class", "alert alert-danger alert-dismissable" } })%>--%>
                    <%using (Html.BeginForm("Individual", "MemberType", FormMethod.Post, new { id = "formFastMembershipIndividual", @class = "form-horizontal", @role = "form" }))
                        {%>
                    <input type="hidden" name="mtypePage" value="<%:ViewData["mtypePage"] %>" />
                    <input type="hidden" name="uyeNo" value="<%:ViewData["uyeNo"] %>" />
                    <input type="hidden" name="urunNo" value="<%:ViewData["urunNo"] %>" />
                    <input type="hidden" name="type" value="<%:ViewData["type"] %>" />
                    <input type="hidden" name="memberType" value="<%:ViewData["memberType"] %>" />


                    <% var dataAddress = new NeoSistem.MakinaTurkiye.Data.Address(); %>
                    <%--<div class="form-group">
                    <label class="col-sm-2 control-label"><%=Html.LabelFor(model => model.MembershipModel.c)%> <span class="required">*</span> </label>
                    <div class="col-sm-10">
                        <%:Html.DropDownListFor(model => model.MembershipModel.AddressTypeId, Model.MembershipModel.AddressTypeItems,new { @class = "form-control",
                            @data_validation_engine ="validate[funcCall[ifSelectNotEmpty]]" ,@data_errormessage_value_missing="*Bu alanı zorunludur!"
                        })%>
                    </div>
                </div>--%>
                    <div class="form-group">
                        <%if (Model.MembershipModel.MemberPassword == null)
                            { %>
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Şifre :
                        </label>
                        <div class="col-sm-6">
                            <%= Html.PasswordFor(model => model.MembershipModel.MemberPassword, new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "id", "password" },
                                  { "data-validation-engine", "validate[required,minSize[6]]" },
                                  { "placeholder", "Min. 6 haneli şifre oluştur" } })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Şifre (Tekrar) :
                        </label>
                        <div class="col-sm-6">
                            <%= Html.PasswordFor(model => model.MembershipModel.MemberPasswordAgain, 
                                 new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "data-validation-engine", "validate[required,minSize[6],equals[password]]" },
                                  { "placeholder", "Şifrenizi tekrar girin" } })%>
                        </div>
                        <div class="help-block">
                            <a style="cursor: pointer" class="popovers" data-container="body" data-original-title="Uyarı"
                                data-toggle="popover" data-placement="right" data-content="Şifrenizi Tekrar Giriniz.">
                                <span class="glyphicon glyphicon-info-sign"></span></a>
                        </div>
                    </div>
                    <%} %>

                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Doğum Tarihiniz(*) :
                        </label>
                        <div class="col-sm-2">
                            <select id="Day" name="MembershipModel.Day" class="form-control" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]">
                                <option value="0">Gün</option>
                                <%for (int gun = 1; gun <= 31; gun++)
                                    {%>
                                <option value="<%=gun.ToString() %>">
                                    <%=gun.ToString()%></option>
                                <%}%>
                            </select>
                        </div>
                        <div class="col-sm-2">
                            <select id="Month" name="MembershipModel.Month" class="form-control" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]">
                                <option value="0">Ay</option>
                                <%for (int ay = 1; ay <= 12; ay++)
                                    {%>
                                <option value="<%=ay.ToString() %>">
                                    <%=ay.ToString()%></option>
                                <%}%>
                            </select>
                        </div>
                        <div class="col-sm-2">
                            <select id="Year" name="MembershipModel.Year" class="form-control" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]">
                                <option value="0">Yıl</option>
                                <%for (int yil = 1945; yil <= 2002; yil++)
                                    {%>
                                <option value="<%=yil.ToString() %>">
                                    <%=yil.ToString() %></option>
                                <%}%>
                            </select>
                        </div>
                    </div>
                    <div class="form-group" data-rel="gsm-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            GSM(*) :
                        </label>
                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalGSMCulture,new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" },{"Value","+90"}})%>
                        </div>
                        <div class="col-sm-2">
                            <select id="MembershipModel_InstitutionalGSMAreaCode" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]" class="form-control" name="MembershipModel.InstitutionalGSMAreaCode">

                                <% var gsmItem = Model.Phone; %>

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
                            </select>
                        </div>
                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalGSMNumber, new Dictionary<string, object> { { "class", "form-control" },{ "data-validation-engine", "validate[required]" }})%>
                        </div>

                    </div>
                    <div class="col-md-12">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-3">
                            <label class="checkbox-inline" style="color: #03824c"><%:Html.CheckBoxFor(model => model.MembershipModel.IsGsmWhatsapp)%>Whatsapp numaram olarak kayıt et</label>

                        </div>
                    </div>
                    <div class="form-group" data-rel="whatsapp-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Whatsapp Gsm :
                        </label>
                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.GsmWhatsappCulture,new Dictionary<string, object> { { "class", "form-control" },{"Value","+90"}})%>
                        </div>
                        <div class="col-sm-2">
                            <select id="MembershipModel_GsmWhatsappAreaCode" class="form-control" name="MembershipModel.GsmWhatsappAreaCode">
                                <% var wgsmItem = Model.Phone; %>

                                <option value="0" <%:wgsmItem!= null  ? "selected=\"selected\"" : "" %>>Seç</option>
                                <option value="530" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "530" ? "selected=\"selected\"" : "" %>>530</option>
                                <option value="531" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "531" ? "selected=\"selected\"" : "" %>>531</option>
                                <option value="532" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "532" ? "selected=\"selected\"" : "" %>>532</option>
                                <option value="533" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "533" ? "selected=\"selected\"" : "" %>>533</option>
                                <option value="534" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "534" ? "selected=\"selected\"" : "" %>>534</option>
                                <option value="535" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "535" ? "selected=\"selected\"" : "" %>>535</option>
                                <option value="536" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "536" ? "selected=\"selected\"" : "" %>>536</option>
                                <option value="537" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "537" ? "selected=\"selected\"" : "" %>>537</option>
                                <option value="538" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "538" ? "selected=\"selected\"" : "" %>>538</option>
                                <option value="539" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "539" ? "selected=\"selected\"" : "" %>>539</option>
                                <option value="541" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "541" ? "selected=\"selected\"" : "" %>>541</option>
                                <option value="542" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "542" ? "selected=\"selected\"" : "" %>>542</option>
                                <option value="543" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "543" ? "selected=\"selected\"" : "" %>>543</option>
                                <option value="544" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "544" ? "selected=\"selected\"" : "" %>>544</option>
                                <option value="545" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "545" ? "selected=\"selected\"" : "" %>>545</option>
                                <option value="546" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "546" ? "selected=\"selected\"" : "" %>>546</option>
                                <option value="547" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "547" ? "selected=\"selected\"" : "" %>>547</option>
                                <option value="548" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "548" ? "selected=\"selected\"" : "" %>>548</option>
                                <option value="549" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "549" ? "selected=\"selected\"" : "" %>>549</option>
                                <option value="501" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "501" ? "selected=\"selected\"" : "" %>>501</option>
                                <option value="505" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "505" ? "selected=\"selected\"" : "" %>>505</option>
                                <option value="506" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "506" ? "selected=\"selected\"" : "" %>>506</option>
                                <option value="507" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "507" ? "selected=\"selected\"" : "" %>>507</option>
                                <option value="551" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "551" ? "selected=\"selected\"" : "" %>>551</option>
                                <option value="552" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "552" ? "selected=\"selected\"" : "" %>>552</option>
                                <option value="553" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "553" ? "selected=\"selected\"" : "" %>>553</option>
                                <option value="554" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "554" ? "selected=\"selected\"" : "" %>>554</option>
                                <option value="555" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "555" ? "selected=\"selected\"" : "" %>>555</option>
                            </select>
                        </div>

                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.GsmWhatsappNumber, new Dictionary<string, object> { { "class", "form-control" }})%>
                        </div>
                    </div>
                    <%if (ViewData["type"] != null)
                        { %>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Adres Tipi(*) :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.DropDownListFor(model => model.MembershipModel.AddressTypeId, Model.MembershipModel.AddressTypeItems,
                                    new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                        </div>
                    </div>
                    <%} %>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Ülke(*) :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.DropDownListFor(model => model.MembershipModel.CountryId, Model.CountryItems,
                                                              new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "countryId" } })%>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="address-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Adres :
                        </label>
                        <div class="col-sm-6">
                            <textarea class="form-control" name="MembershipModel_AvenueOtherCountries"></textarea>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="city-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Şehir(*) :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.DropDownListFor(model => model.MembershipModel.CityId, Model.CityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "cityId" } })%>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="locality-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            İlçe(*) :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.DropDownListFor(model => model.MembershipModel.LocalityId, Model.LocalityItems, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "data-rel", "localityId" } })%>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="district-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Mahalle / Köy(*) :
                        </label>
                        <div class="col-sm-6">
                            <select id="MembershipModel_Towns" data-rel="mahalle" class="form-control" data-validation-engine="validate[funcCall[ifSelectNotEmpty]]">
                            </select>
                            <input type="hidden" id="MembershipModel_TownId" name="MembershipModel.TownId" value="0" />
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="other-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Posta Kodu :
                        </label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" name="MembershipModel.ZipCode" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="other-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Cadde :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.TextBoxFor(model => model.MembershipModel.Avenue, 
                                    new Dictionary<string, object> { { "class", "form-control" } })%>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="other-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Sokak :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.TextBoxFor(model => model.MembershipModel.Street,
                                    new Dictionary<string, object> { { "class", "form-control" } })%>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="other-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            No :
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
                                     <div class="form-group" data-rel="other-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Posta Kodu :
                        </label>
                        <div class="col-sm-6">
                            <%:Html.TextBoxFor(model => model.MembershipModel.PostCode,
                                    new Dictionary<string, object> { { "class", "form-control" },{"maxLength","11"} })%>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="phone-wrapper">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Telefon (1) :
                        </label>
                        <div class="col-sm-2">
                            <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode" name="MembershipModel.InstitutionalPhoneAreaCode" />
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneCulture,
                                new Dictionary<string, object> { { "class", "form-control" }  })%>
                        </div>
                        <div class="col-sm-2">
                            <select id="DropDownInstitutionalPhoneAreaCode" name="DropDownInstitutionalPhoneAreaCode"
                                style="display: none" class="form-control">
                                <option value="0">Seç</option>
                                <option value="212">212</option>
                                <option value="216">216</option>
                            </select>
                            <%:Html.TextBox("TextInstitutionalPhoneAreaCode", Model.MembershipModel.InstitutionalPhoneAreaCode, new { @class = "form-control" })%>
                        </div>
                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneNumber, 
                                new Dictionary<string, object> { { "class", "form-control" } })%>
                        </div>
                    </div>
                    <div class="row" style="">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                        </label>
                        <div class="col-md-5" style="">
                            <div class="alert alert-danger alert-dismissable" data-rel="email-wrapper" id="telefonHata" style="display: none; text-align: center;">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                    &times;
                                </button>
                                Telefon alan kodu zorunludur
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="display: none" data-rel="phone-wrapper">
                        <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode2" name="MembershipModel.InstitutionalPhoneAreaCode2" />
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Telefon (2) :
                        </label>
                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalPhoneCulture2, new { @class = "form-control" })%>
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
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalFaxCulture, new { @class = "form-control" })%>
                        </div>
                        <div class="col-sm-2">
                            <select id="DropDownInstitutionalFaxAreaCode" name="DropDownInstitutionalFaxAreaCode"
                                style="display: none" class="form-control">
                                <option value="0">Seç</option>
                                <option value="212">212</option>
                                <option value="216">216</option>
                            </select>
                            <%:Html.TextBox("TextInstitutionalFaxAreaCode", Model.MembershipModel.InstitutionalFaxAreaCode, new { @class = "form-control" })%>
                        </div>
                        <div class="col-sm-2">
                            <%:Html.TextBoxFor(model => model.MembershipModel.InstitutionalFaxNumber, new { @class = "form-control" })%>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <button class="btn btn-primary" type="submit">
                                Değişikleri Kaydet
                            </button>
                        </div>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>

    <script src="/Scripts/jquery-cascading-dropdown/dist/jquery.cascadingdropdown.min.js"></script>
    <script type="text/javascript">


</script>
</asp:Content>


