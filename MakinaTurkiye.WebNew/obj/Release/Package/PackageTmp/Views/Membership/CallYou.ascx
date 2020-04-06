﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CompanyDemandMembership>" %>

<link href="/Content/CallYou/CallYou.css" rel="stylesheet" />
<div class="phone-area sticky" data-toggle="modal" data-target="#myModal" onclick="CallYouShowPopup();">
    <div class="phone-title"></div>
    <a class="phone-number">Biz Sizi Arayalım1</a>
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
                                <%=Html.TextBoxFor(m => m.WebUrl, new { @class = "form-control" ,@placeholder="örn:http://makinaturkiye.com"})%>
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


