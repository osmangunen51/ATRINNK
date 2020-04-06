<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Personal.ChangeEmailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Email Adresini Değiştir-Makina Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">


    <script type="text/javascript">



        function changeEmailCheck() {
            if ($('#Member_MemberPassword').val() != $('#OldMemberPassword').val()) {
                alert("Girmiş olduğunuz şifre, sistemimizde kayıtlı şifrenizle uyuşmamaktadır.");
                return false;
            }
            else if ($('#Member_MemberEmailAgain').val() != $('#Member_NewEmail').val()) {
                alert("Girmiş olduğunuz E-Posta adresleri uyuşmamaktadır.");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <input id="OldMemberEmail" type="hidden" value="<%=AuthenticationUser.Membership.MemberEmail %>" />
    <input id="OldMemberPassword" type="hidden" value="<%=AuthenticationUser.Membership.MemberPassword %>" />

    <div class="row">


        <div class="row">
            <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
                <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
            </div>
            <div class="col-md-12">
                <h4 class="mt0 text-info">
                    <span class="text-primary glyphicon glyphicon-cog"></span>
                    Eposta adresi değişikliği
                </h4>
            </div>
        </div>

        <div class="col-sm-8 col-md-9">
            <div class="row">

                <div class="well well-mt4 col-xs-12">
                    <%if (TempData["success"] != null)
                        {
                            if (TempData["success"].ToBoolean() == true)
                            {%>
                    <div class="alert alert-success">
                        Mail adresinizi başarıyla güncellenmiştir;
                    </div>
                    <% }
                        else
                        {%>
                    <div class="alert alert-warn">
                        Lütfen formda bulunan bilgileri doğru giriniz.
                    </div>
                    <% }
                        } %>
                    <%using (Html.BeginForm("ChangeEmail", "Personal", FormMethod.Post, new { id = "formChangeEMail", @class = "form-horizontal", role = "form" }))
                        { %>
                    <div class="form-group ">
                        <label class="col-sm-3 control-label">
                            E-Posta Adresiniz
             
                        </label>
                        <div class="col-sm-7 col-md-5">
                            <%:Html.TextBoxFor(model => model.Member.MemberEmail, new {type="email" ,@class="form-control popovers" ,size = "30", disabled = "disabled" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Yeni E-Posta adresiniz
             
                        </label>
                        <div class="col-sm-6 col-md-4">
                            <%:Html.TextBoxFor(model => model.Member.NewEmail, new { type = "email", @class = "form-control", size = "30", validate = "required:true, email:true, remote: '/Personal/CheckMail'" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Yeni E-Posta adresiniz
                (Tekrar)
             
                        </label>
                        <div class="col-xs-11 col-sm-6 col-md-4">
                            <%:Html.TextBoxFor(model => model.Member.MemberEmailAgain, new { type="email" ,@class="form-control",size = "30", placeholder = "Yeni e-posta adresinizi tekrar girin" })%>
                        </div>
                        <div class="col-xs-1 p5">
                            <a href="#" class="popovers" data-container="body"
                                data-original-title="E-post adresi onay" data-toggle="popover"
                                data-placement="right"
                                data-content="Yeni girdiğiniz e-posta adresleri aynı olmalıdır">
                                <span class="glyphicon glyphicon-info-sign"></span>
                            </a>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Şifreniz*
             
                        </label>
                        <div class="col-xs-11 col-sm-6 col-md-4">
                            <%:Html.PasswordFor(model => model.Member.MemberPassword, new { size = "10", @class = "form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <button type="submit" onclick="return changeEmailCheck();" class="btn btn-primary">
                                Değişiklikleri
                  Kaydet
               
                            </button>
                        </div>
                    </div>
                    <%} %>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
