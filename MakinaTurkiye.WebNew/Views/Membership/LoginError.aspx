<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="well">
            <span>Kullanıcı adı veya şifrenizi hatalı girdiniz.
                <br />
                <br />
                Tekrar giriş yapmak için</span> <a href="/Uyelik/KullaniciGirisi">tıklayınız</a>
        </div>
    </div>
</asp:Content>
