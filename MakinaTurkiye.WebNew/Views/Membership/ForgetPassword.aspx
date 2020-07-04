<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Views/Shared/Main.Master"
    Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Şifremi Unuttum-makinaturkiye.com
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="https://www.makinaturkiye.com/Uyelik/SifremiUnuttum" />
    <meta name="description" content="makinaturkiye.com kullanıcı, firma şifre yenileme sayfasıdır" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="display: flex; justify-content: center; align-content: center;">
        <div class="col-sm-7 col-md-8 pr">
            <div>
                <div class="panel panel-default membership-panel">
                    <div class="panel-heading" style="padding:10px">
                        <h1 style="font-size: 24px!important;">Şifremi Unuttum</h1>
                    </div>

                    <div class="panel-body">
                        <%if ((int)ViewData["ProcessType"] == (int)NeoSistem.MakinaTurkiye.Web.Controllers.RememberPasswordProcessType.ShowSendMailForm)
                            {
                                Html.RenderPartial("sendNewPasswordLink", Model);
                        %>
                        <%}
                            else if ((int)ViewData["ProcessType"] == (int)NeoSistem.MakinaTurkiye.Web.Controllers.RememberPasswordProcessType.ShowSuccesSendMail)
                            {
                                Html.RenderPartial("SendMailSucces");
                        %>

                        <%}
                            else if ((int)ViewData["ProcessType"] == (int)NeoSistem.MakinaTurkiye.Web.Controllers.RememberPasswordProcessType.ShowReCreatePasswordForm)
                            {
                                Html.RenderPartial("reCreatePassword", Model);

                        %>
                        <%}
                            else if ((int)ViewData["ProcessType"] == (int)NeoSistem.MakinaTurkiye.Web.Controllers.RememberPasswordProcessType.FailReCreatePassword)
                            {%>
                        <span>Kullanıcı Bulunmadı</span>
                        <% }
                            else if ((int)ViewData["ProcessType"] == (int)NeoSistem.MakinaTurkiye.Web.Controllers.RememberPasswordProcessType.ShowSuccesChangePassword)
                            {%>
                        <h4>Şifrenizi Başarılı Bir Şekilde Değiştirdiniz.</h4>
                        <br />
                        <h4>Giriş yapmak için <a href="http://www.makinaturkiye.com/">tıklayınız.</a>
                        </h4>
                        <%}
                        %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
