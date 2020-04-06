<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MembershipViewModel>" %>
 <%: Html.ValidationSummary(true, "", new Dictionary<string, object> { { "class", "alert alert-danger alert-dismissable" } })%>
<%using (Html.BeginForm("ForgettedPassowrd", "Membership", FormMethod.Post, new { id = "formFastMembership", @class = "form-horizontal", @role = "form" }))
  {%>
<input type="hidden" name="MembershipType" data-rel="MembershipType" value="10" />

<div class="form-group">
    <label for="inputPassword3" class="col-sm-3 control-label">
       Yeni Şifre
    </label>
    <div class="col-sm-6">
        <%string passwordcode = ViewData["forgetPasswordCode"].ToString(); %>
        <%:Html.Hidden("passwordCode",passwordcode) %>
        <%= Html.PasswordFor(model => model.MembershipModel.MemberPassword, new Dictionary<string, object> { { "class", "mt-form-control" }, 
                                  { "id", "password" },
                                  { "data-validation-engine", "validate[required,minSize[6]]" },
                                  { "placeholder", "Min. 6 hane" } })%>
    </div>
</div>
<div class="form-group">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Şifre (Tekrar)
    </label>
    <div class="col-sm-6">
        <%= Html.PasswordFor(model => model.MembershipModel.MemberPasswordAgain, 
                                 new Dictionary<string, object> { { "class", "mt-form-control" }, 
                                  { "data-validation-engine", "validate[required,equals[password],minSize[6]]" },
                                  { "placeholder", "Şifrenizi tekrar girin" } })%>
    </div>
    <div class="help-block">
        <a style="cursor: pointer" class="popovers" data-container="body" data-original-title="Uyarı"
            data-toggle="popover" data-placement="right" data-content="Şifrenizi Tekrar Giriniz.">
            <span class="glyphicon glyphicon-info-sign"></span></a>
    </div>
</div>
<div class="form-group">
    <div class="col-sm-offset-3 col-sm-3">
         <button class="btn btn-sign-up" id="btnLogin"  type="submit">Şifreyi Onayla</button>
    </div>
</div>
<% }%>