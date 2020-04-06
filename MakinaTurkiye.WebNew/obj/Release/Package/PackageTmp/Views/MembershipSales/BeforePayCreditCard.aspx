<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" 
    Inherits="System.Web.Mvc.ViewPage<MakinaTurkiye.Entities.Tables.Checkouts.Order>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ödeme Hatırlatma</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
          
            <div class="well" style="background-color:#fff;">
                  <div class="alert alert-warning" style="font-size:15px;">
                &nbsp;Ödenmemiş 1 sipariş borcunuz bulunmaktadır.
            </div>
                <p style="font-size:16px; font-weight:700">İsterseniz bu alanı kullanarak kredi kartı ile ödemenizi tamamlayabilirsiniz.</p>
                <h4>Sipariş Bilgileri</h4>
                <p style="font-size:16px;">
                    <b>Sipariş Tarihi:</b><%:String.Format("{0:dd:MM:yyyy}",Model.RecordDate) %> <br />
                    <b>Kalan Bakiye:</b><%:Model.OrderPrice %> TL
                </p>
                <a href="/MemberShipSales/PayWithCreditCard" class="btn btn-success">Şimdi Ödeme Yapmak İstiyorum</a>   
              <a href="/Account/Home" class="btn btn-info">Ödemeyi Daha Sonra Yapacağım</a>   
                
            </div>
        </div>
    </div>
</asp:Content>
