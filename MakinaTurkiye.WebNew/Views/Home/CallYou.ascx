﻿<%--<%@ Control Language="C#" AutoEventWireup="true" ViewStateMode="Disabled" %>
<link href="/Content/CallYou/CallYou.css" rel="stylesheet" />
<div class="phone-area sticky" data-toggle="modal" data-target="#myModal" onclick="CallYouShowPopup();">
    <div class="phone-title"></div>
    <a class="phone-number">Biz Sizi Arayalım</a>
</div>
<script src="/Content/CallYou/CallYou.js"></script>

<!-- Modal -->
<div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">

        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Biz sizi arayalım</h4>
            </div>
            <%using (Html.BeginForm("CallYouMethod", "Home", FormMethod.Post, new { id = "CallYouContainerID", @class = "form-horizontal", @role = "form" }))
                {%>
            <div class="modal-body">
                <input type="text" name="Ad" id="name" value="" class="form-control" placeholder="İsim" required="" autofocus="">
                <input type="text" name="Soyad" id="surname" value="" class="form-control" placeholder="Soyisim" required="" autofocus="">
                <input type="email" name="Email" id="mail" value="" class="form-control" placeholder="E-posta Adresi" required="" autofocus="">
                <input type="number" name="Telefon" id="phone" value="" class="form-control" placeholder="Telefon numarası" required="" autofocus="">
                <input type="text" name="Firma" id="firma" value="" class="form-control" placeholder="Firma" required="" autofocus="">
                <textarea id="adres" name="adress" placeholder="Adres" class="form-control"></textarea>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-info">Kaydet</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
            </div>
            <% } %>
        </div>

    </div>
</div>


<%--<div class="leftCallbackForm show">
    <div class="local-loading hideDiv" id="leftCallLoading"></div>
    <!-- Lfc Form -->
    <div class="lfcForm">
        <div class="lfTitle">BİZ SİZİ ARAYALIM</div>
        <div class="lfDesc">Bize bilgilerinizi gönderin size ulaşalım...</div>
       
        <table class="full-table">
            <tbody><tr>
                <td><input type="text" class="full-input" id="ltxtName" onkeypress="return onlyString(event)" placeholder="Ad"></td>
                <td><input type="text" class="full-input" id="ltxtSurName" onkeypress="return onlyString(event)" placeholder="Soyad"></td>
            </tr>
            <tr>
                <td colspan="2"><input type="text" class="full-input" id="ltxtEmailAddress" placeholder="E-Posta"></td>
            </tr>
            <tr>
                <td colspan="2"><input type="text" class="full-input" id="ltxtPhone" placeholder="Telefon"></td>
            </tr>
            <tr>
                <td colspan="2"><textarea class="full-textarea" id="ltxtMessage" placeholder="Mesajınız"></textarea></td>
            </tr>
            <tr>
                <td>
                    <select class="full-select" id="lSelectDate"><option value="28/02/2019"> 28/02/2019 </option><option value="01/03/2019"> 01/03/2019 </option><option value="02/03/2019"> 02/03/2019 </option></select>
                </td>
                <td>
                    <select class="full-select" id="lSelectTime"><option value="08:00 - 09:00">08:00 - 09:00</option><option value="09:00 - 10:00">09:00 - 10:00</option><option value="10:00 - 11:00">10:00 - 11:00</option><option value="11:00 - 12:00">11:00 - 12:00</option><option value="12:00 - 13:00">12:00 - 13:00</option><option value="13:00 - 14:00">13:00 - 14:00</option><option value="14:00 - 15:00">14:00 - 15:00</option><option value="15:00 - 16:00">15:00 - 16:00</option><option value="16:00 - 17:00">16:00 - 17:00</option><option value="17:00 - 18:00">17:00 - 18:00</option><option value="18:00 - 19:00">18:00 - 19:00</option><option value="19:00 - 20:00">19:00 - 20:00</option></select>
                </td>
            </tr>
        </tbody></table>
        <div class="lfDesc">
            Çağrı Merkezi Çalışma Saatleri<br>
            <span>
                <div>Hafta İçi : 09:00 - 20:00<br></div><div>Hafta Sonu (Cumartesi) : 09:00 - 18:00</div><div>Hafta Sonu (Pazar) : 11:00 - 16:00</div>
            </span>
        </div>
        <button class="autoButton full mt10 mainBg" onclick="sendLeftCallBackForm();">GÖNDER</button>

    </div>
    <!-- Lfc Form -->

    <!-- Lcf Trigger -->
    <div class="lcfTrigger">
        <div class="trigger1">BİZ SİZİ ARAYALIM</div>
    </div>
    <!-- Lcf Trigger -->--%>



<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CompanyDemandMembership>" %>
<link href="../../Content/V2/assets/css/CallYou.css" rel="stylesheet" />
<div class="phone-area sticky" data-toggle="modal" data-target="#myModal">
    <div class="phone-title"></div>
    <a class="phone-number">
        <img src="../../Content/V2/images/customer-support-icon.png" style="width:30px; margin-right:5px;" />
        Biz Sizi Arayalım</a>
</div>


<div id="myModal" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div style="background-color: rgb(227,236,246); color: #333" class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                <h4 class="modal-title">Biz Sizi Arayalım</h4>
            </div>
            <div class="modal-body">
                <%using (Ajax.BeginForm("CompanyMemberShipDemand", "MemberShip", new AjaxOptions { UpdateTargetId = "satutusTalep", LoadingElementId = "loading", OnSuccess = "onSuccess", OnFailure = "ajaxError", OnBegin = "onBeginSend" }))
                    { %>
                <%:Html.ValidationSummary(true) %>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="ajaxSuccess" style="display: none;">
                                <div class="alert alert-success alert-dismissable" style="font-size: 14px;">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        &times;
                                    </button>
                                    <strong>Teşekkürler! </strong>
                                    Talebiniz başarıyla gönderilmiştir. En yakın zamanda geri dönüş yapacağız.
                                </div>
                                <div id="ajaxError" style="display: none;">
                                    <div class="alert alert-danger alert-dismissable" style="font-size: 14px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            &times;
                                        </button>
                                        <strong>Hata! </strong>
                                        Talebiniz şu anda iletilemedi.Site yöneticisiyle irtibata geçebilirsiniz.
                                    </div>

                                </div>

                                <%if (ViewData["mesaj1"] != null)
                                    {
                                %>

                                <div class="alert alert-danger alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        &times;
                                    </button>
                                    <strong>Hata! </strong>
                                    Gerekli alanları doldurunuz
                                </div>


                                <%} %>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-2 control-label">
                                İsim Soyisim:
                            </label>
                            <div class="col-md-4">
                                <%=Html.TextBoxFor(m => m.NameSurname, new { @class = "form-control",@required=true})%>
                                <%=Html.ValidationMessageFor(m=>m.NameSurname) %>
                            </div>
                            <label for="inputPassword3" class="col-sm-2 control-label">
                                Telefon:
                            </label>
                            <div class="col-md-4">
                                <%=Html.TextBoxFor(m => m.Phone, new { @class = "form-control", @required = true,@placeholder="örn:5XX XXX XXXX",@maxlength="10" })%>
                            </div>

                        </div>
                        <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-2 control-label">
                                Email:
                            </label>
                            <div class="col-md-4">
                                <%=Html.TextBoxFor(m => m.Email, new { @class = "form-control", @required = true,@type="email" })%>
                            </div>
                            <label for="inputPassword3" class="col-sm-2 control-label">
                                Firma Adı:
                            </label>
                            <div class="col-md-4">
                                <%=Html.TextBoxFor(m => m.CompanyName, new { @class = "form-control", @required = true})%>
                            </div>

                        </div>
                        <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-2 control-label">
                                Web Adresi:
                            </label>
                            <div class="col-md-4">
                                <%=Html.TextBoxFor(m => m.WebUrl, new { @class = "form-control" ,@placeholder="örn:https://www.makinaturkiye.com"})%>
                            </div>
                            <label for="inputPassword3" class="col-sm-2 control-label">
                                Açıklama:
                            </label>
                            <div class="col-md-4">
                                <%=Html.TextAreaFor(m=>m.Statement,new{@class="form-control",@placeholder="Eklemek istediğiniz şeyler..",@rows="6"}) %>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div id="loading" style="text-align: center; display: none;">
                        <img style="width: 30px; height: 30px;" src="<%:Url.Content("~/Content/v2/images/loading.gif") %>">
                    </div>
                    <button type="button" name="submitButton" value="demandCompany" class="btn btn-default" data-dismiss="modal">İptal</button>
                    <button type="submit" class="btn btn-primary">Gönder</button>
                </div>
                <%} %>
            </div>
        </div>
    </div>
</div>

<script src="https://www.google.com/recaptcha/api.js" async defer></script>
<script type="text/javascript">

    function onBeginSend() {
        $("#loading").show();
    }
    function onSuccess() {

        $("#loading").hide();
        $("#ajaxSuccess").show();
        $("#ajaxError").hide();
        $("#nameSurname").val("");
        $("#phone").val("");
        $("#email").val("");
        $("#companyName").val("");
        $("#webUrl").val("");
        $("#statement").val("");
    }
    function ajaxError() {
        $("#ajaxError").show();
        $("#ajaxSuccess").hide();
    }
    function onBeginSendS() {
        $("#loading").show();
    }
    function onSuccessS(data, content, xhr) {
        if (data.opr == "ErrorCaptcha") {
            $("#loading1").hide();
            $("#ajaxErrorCaptcha").show();

        }
        else {
            $("#loading1").hide();
            $("#ajaxSuccess1").show();
            $("#ajaxError1").hide();
            $("#sNameSurname").val('');
            $("#sEmail").val('');
            $("#sDescription").val('');
            $("#sPhoneNumber").val('');
            $("#btnSendS").attr("disabled", 'true');

        }


    }
    function ajaxErrorS() {
        $("#ajaxError").show();
        $("#ajaxSuccess").hide();
    }
</script>



