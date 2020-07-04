<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Personal.ChangePasswordModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
  <script type="text/javascript">
      function changePasswordCheck() {
        if ($('#OldMemberPassword').val() != $('#Member_MemberPassword').val()) {
        alert("Eski şifre, girmiş olduğunuz şifreyle uyuşmuyor.");
        return false;
      }
    else if ($('#Member_NewPasswordAgain').val() != $('#Member_NewPassword').val()) {
        alert("Yeni şifreler birbiriyle uyuşmuyor.");
        return false;
      }
      else {
        return true;
      }
    }
  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <input id="OldMemberPassword" type="hidden" value="<%=AuthenticationUser.Membership.MemberPassword %>" />
  <input id="OldMemberEmail" type="hidden" value="<%=AuthenticationUser.Membership.MemberEmail %>" />
        <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
               <span class="text-primary glyphicon glyphicon-cog"></span>
                    Şifre değişikliği
            </h4>
        </div>
    </div>
  <div class="row">

    <div class="col-sm-12 col-md-12">
      <div>
        <div class="well store-panel-container col-xs-12">
         <%using (Html.BeginForm("ChangePassword", "Personal", FormMethod.Post, new { @class="form-horizontal" ,@role="form"}))
           { %>
               <input type="hidden" name="mtypePage" value="<%:ViewData["mtypePage"] %>" />
                <input type="hidden" name="uyeNo" value="<%:ViewData["uyeNo"] %>" />
                <input type="hidden" name="mtypePage" value="<%:ViewData["mtypePage"] %>" />
                <input type="hidden" name="urunNo" value="<%:ViewData["urunNo"] %>" />
            <div class="form-group ">
              <label class="col-sm-3 control-label">
                E-Posta Adresiniz
              </label>
              <div class="col-sm-7 col-md-5">
                <%:Html.TextBoxFor(model => model.Member.MemberEmail, new { @class = "form-control popovers", @type = "email", disabled = "disabled" })%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">
                Eski Şifre
              </label>
              <div class="col-sm-4">
                <%:Html.Password("Member.MemberPassword", "", new { @class = "form-control", size = "10", placeholder = "" })%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">
                Yeni Şifre
              </label>
              <div class="col-sm-4 col-md-3">
                <%:Html.PasswordFor(model => model.Member.NewPassword, new { size = "10", placeholder = "Min. 6 hane", @class = "form-control" })%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">
                Yeni Şifre (Tekrar)
              </label>
              <div class="col-xs-11 col-sm-4 col-md-3">
                <%:Html.PasswordFor(model => model.Member.NewPasswordAgain, new { size = "10", @class = "form-control", placeholder = "Şifrenizi tekrar girin" })%>
              </div>
              <div class="col-xs-1 p5">
                <a hreF="#" class="popovers" data-container="body"
                data-original-title="A Title" data-toggle="popover"
                data-placement="right"
                data-content="Vivamus sagittis lacus vel augue laoreet rutrum faucibus.">
                  <span class="glyphicon glyphicon-info-sign">
                  </span>
                </a>
              </div>
            </div>
            <div class="form-group">
              <div class="col-sm-offset-3 col-sm-9">
                <button type="submit" onclick="return changePasswordCheck();" class="btn btn-primary">
                  Değişiklikleri Kaydet
                </button>
              </div>
            </div>
          
          <% } %>
        </div>
        <div class="panel panel-mt col-xs-12 p0">
          <div class="panel-heading">
            <span class="glyphicon glyphicon-question-sign">
            </span>
            Sayfa Yardımı
          </div>
          <div class="panel-body">
            <b>
              Şifre Bilgilerim
            </b>
            <br>
            
            <i class="fa fa-angle-right">
            </i>
            &nbsp;&nbsp;
            <a
            href="#">
              Şifre bilgilerimi nasıl güncellerim?
            </a>
            <br>
            
            <i
            class="fa fa-angle-right">
            </i>
            &nbsp;&nbsp;
            <a href="#">
              Kullanıcı
              şifrem çalındı. Sorumluluğu makinaturkiye.com' dan talep edebilir
              miyim?
            </a>
            <br>
            
            <i class="fa fa-angle-right">
            </i>
            &nbsp;&nbsp;
            <a
            href="#">
              Üyelik işleminde dikkat edilmesi gerekenler
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
