﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>"
    ValidateRequest="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-1245846-23']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
</script>--%>
   
   <%-- <div class="contentText">
        <span style="float: left; margin-left: 20px; font-size: 13px; margin-top: 20px; margin-bottom: 20px;">
            Üzgünüz bir hata oluştu.
            <br />
            <br />
            Anasayfaya yönlendiriliyorsunuz. </span>
        <%
            Exception exception = Server.GetLastError();
            if (exception != null)
                Response.Write(exception.ToString());
        %>
    </div>--%>
   <%-- <meta http-equiv="refresh" content="3; URL=http://www.makinaturkiye.com" />--%>
   <%-- <meta name="robots" content="NOINDEX,NOFOLLOW" />--%>
    <%--<script type="text/C#" runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Response.StatusCode = (int) System.Net.HttpStatusCode.InternalServerError;
    }
</script>--%>
    <script type="text/javascript">gtag('event', '404');</script>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <h3>
                    <span class="text-warning glyphicon glyphicon-warning-sign"></span>Sayfa Bulunamadı
                </h3>
                <div class="well">
                    Sayfa kaldırılmış veya adres eksik girilmiş olabilir.
                    <br>
                    Lütfen adresi doğru yazdığınızdan emin olun.
                    <br>
                    Eğer Sık Kullanılanlar listesinden bu sayfaya yönlendirildiyseniz, <a href="/">Makina
                        Türkiye Anasayfa </a> linkini kullanarak
                    ulaşmaya çalışın.
                    <br>
                    Eğer bunun teknik bir sorun olduğunu düşünüyorsanız, sayfayı <a href="#">Destek Masası
                    </a>'na bildirin.
                    <br>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    
</asp:Content>
