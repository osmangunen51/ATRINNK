<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CompanyDemandMembership>" %>
<div class="col-xs-12 col-sm-5 col-md-4">
    <div class="panel panel-mt">
        <div class="panel-heading">
            <span class="glyphicon glyphicon-user" style="padding-right: 5px;"></span>Zaten Üyeyim
       
        </div>
        <div class="panel-body">
            makinaturkiye.com üyeliğiniz varsa aşağıdaki butona tıklayarak üye girişi yapabilirsiniz.
           
            <br>
            <br>
            <a href="/uyelik/kullanicigirisi" class="btn btn-sm btn-primary ">Giriş Yap </a>
        </div>
    </div>
        <div class="row" style="margin-bottom: 10px;">
        <div class="col-md-12">
            <a data-toggle="modal" data-target=".bs-example-modal-lg" class="col-md-12 btn btn-info">Firma Üyeliği İçin Aranmak İstiyorum </a>
            <br />

        </div>
    </div>

    <div class="row" style="margin-bottom: 10px;">
        <div class="col-md-12">
            <a data-toggle="modal" data-target=".support" class="col-md-12 btn btn-primary ">Destek </a>
            <br />

        </div>
    </div>
    <div class="panel panel-mt">
        <div class="panel-heading">
            makinaturkiye.com'da üyelik ve avantajlar
       
        </div>
        <ul class="list-group list-group-mt3 mb10">
            <li class="list-group-item"><span class="glyphicon glyphicon-time"></span><a href="/bireysel-uyelik-y-141638">Nasıl Bireysel Üye Olurum? </a></li>
            <li class="list-group-item"><span class="glyphicon glyphicon-file"></span><a href="/bireysel-uyelik-avantajlari-y-182654">Üyelik Avantajları </a></li>
            <li class="list-group-item"><span class="glyphicon glyphicon-file"></span><a href="/kurumsal-uyelik-y-141639">Nasıl Kurumsal Üye Olurum? </a></li>

            <li class="list-group-item"><span class="glyphicon glyphicon-cog"></span><a href="/kurumsal-uyelik-avantajlari-y-142419">Kurumsal Üyelik Ve Avantajları </a></li>
            <li class="list-group-item"><span class="glyphicon glyphicon-th-large"></span><a
                href="/magaza-paket-fiyatlari-y-143135">Üyelik Paketlerimiz</a></li>
        </ul>
    </div>

</div>

<div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div style="background-color: rgb(227,236,246); color: #333" class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                <h4 class="modal-title">Firma Üyeliği İçin Aranmak İstiyorum</h4>
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

<div class="modal fade support" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div style="    -webkit-box-shadow: 0px 0px 5px 0px rgba(50, 50, 50, 0.75);
-moz-box-shadow:    0px 0px 5px 0px rgba(50, 50, 50, 0.75);
box-shadow:         0px 0px 5px 0px rgba(50, 50, 50, 0.75);
background-color:rgb(227,236,246); color:#333;" class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                <h4 class="modal-title">Destek</h4>
            </div>
            <div class="modal-body">
                <%using (Ajax.BeginForm("Support", "MemberShip", new AjaxOptions { UpdateTargetId = "statusSupport", LoadingElementId = "loading1", OnSuccess = "onSuccessS" ,OnFailure = "ajaxErrorS", OnBegin = "onBeginSendS" }))
                  { %>
                <%:Html.ValidationSummary(true) %>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="ajaxSuccess1" style="display: none;">
                                <div class="alert alert-success alert-dismissable" style="font-size: 14px;">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        &times;
                                    </button>
                                    <strong>Teşekkürler! </strong>
                                    Talebiniz başarıyla gönderilmiştir. En yakın zamanda geri dönüş yapacağız.
                                </div>
                                <div id="ajaxError1" style="display: none;">
                                    <div class="alert alert-danger alert-dismissable" style="font-size: 14px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            &times;
                                        </button>
                                        <strong>Hata! </strong>
                                        Talebiniz şu anda iletilemedi.Site yöneticisiyle irtibata geçebilirsiniz.
                                    </div>

                                </div>
                                     <div id="ajaxErrorCaptcha" style="display: none;">
                                    <div class="alert alert-danger alert-dismissable" style="font-size: 14px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            &times;
                                        </button>
                                        <strong>Hata! </strong>
                                        Talebiniz şu anda iletilemedi.Lütfen doğrulamayı geçiniz.
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-4 control-label">
                                Adınız Soyadınız:
                            </label>
                            <div class="col-md-8">
                                <%=Html.TextBox("sNameSurname","", new { @class = "form-control",@required=true})%>
                            </div>
                        </div>
                   <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-4 control-label">
                                E-posta Adresiniz:
                            </label>
                            <div class="col-md-8">
                                <%=Html.TextBox("sEmail","", new { @class = "form-control",@required=true,@type="email"})%>
                            </div>
                        </div>
                             <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-4 control-label">
                                Telefon Numaranız:
                            </label>
                            <div class="col-md-8">
                                <%=Html.TextBox("sPhoneNumber","" ,new { @class = "form-control",@required=true})%>
                            </div>
                        </div>
                              <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-4 control-label">
                                Açıklama:
                            </label>
                            <div class="col-md-8">
                                <%=Html.TextArea("sDescription", new { @class = "form-control",@required=true,@rows="8"})%>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-8">
                                 <div class="g-recaptcha" data-sitekey="6LemzhUUAAAAAMgP3NVU5i2ymC5iC_3bVvB466Xh"></div>
                            </div>
                        </div>
   
                    </div>
                </div>
                <div class="modal-footer">
                    <div id="loading1" style="text-align: center; display: none;">
                        <img style="width: 30px; height: 30px;" src="<%:Url.Content("~/Content/v2/images/loading.gif") %>">
                    </div>
                    <button type="button" name="submitButton" value="demandCompany" class="btn btn-default" data-dismiss="modal">İptal</button>
                    <button type="submit" class="btn btn-primary" id="btnSendS">Gönder</button>
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
    function onSuccessS(data,content,xhr) {
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


