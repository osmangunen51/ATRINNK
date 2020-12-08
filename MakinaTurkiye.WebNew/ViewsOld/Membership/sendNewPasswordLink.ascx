<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MembershipViewModel>" %>
 <%: Html.ValidationSummary(true, "", new Dictionary<string, object> { { "class", "alert alert-danger alert-dismissable" } })%>
<%using (Html.BeginForm("ForgettedPassowrd", "Membership", FormMethod.Post, new { id = "", @class = "form-horizontal", @role = "form" }))
  {%>
<input type="hidden" name="MembershipType" data-rel="MembershipType" value="10" />
<div class="form-group">
    <div class="col-sm-6">
<%--        <%= Html.PasswordFor(model => model.MembershipModel.MemberEmail,
                                 new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "type", "email" },
                                  { "data-validation-engine", "validate[required,custom[email]]" },
                                  { "data-errormessage-value-missing", "Email alanı zorunludur!" },
                                  { "data-errormessage-custom-error", "Örneğin: info@makinaturkiye.com" },
                                  { "placeholder", "örn: info@makinaturkiye.com" } , {"data-rel", "DestinationEmail"}})%>--%>
        <%:Html.TextBoxFor(model => model.MembershipModel.MemberEmail, new Dictionary<string,object> {{"placeholder","örn: info@makinaturkiye.com"},{"required","true"},{"class","mt-form-control"},{"type","email"},{"id","DestinationEmail"} })%>
    </div>

</div>
<div class="form-group">
        <div class="col-sm-6">
               <div class="alert alert-danger alert-dismissable" id="EmailError" style="display:none;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                       Sistemimize kayıtlı böyle bir mail adresi bulunamadı.
                    </div>
    </div>
</div>
<div class="form-group">
    <div class="col-sm-3">
        <button class="btn btn-sign-up" id="btnLogin"  type="submit">Mail Gönder</button>

    </div>
</div>
<% }%>