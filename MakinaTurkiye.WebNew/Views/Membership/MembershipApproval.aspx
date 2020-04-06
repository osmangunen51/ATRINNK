﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row hidden-xs">
        <div class="col-xs-12">
            <ol class="breadcrumb breadcrumb-mt">
                <li><a href="/">Anasayfa</a></li>
                <li class="active">Aktivasyon</li>
            </ol>
        </div>
    </div>
    <div class="row">
    <div class="col-xs-12">
      <h4 class="mt0">
        <span class="glyphicon glyphicon-user">
        </span>
        Ücretsiz Kayıt Olun
      </h4>
    </div>
  </div>
  <div class="row">
    <div class="col-sm-7 col-md-8 pr">
      <div>
        <div class="wll well-mt2 p10">
          <div class="alert alert-info alert-dismissable">
            Lütfen e-posta adresinizi kontrol ediniz!
            <div class="pull-right">
              <a href="/Uyelik/Aktivasyon/">
                Onay kodu girişi
              </a>
              | 
              <a href="#">
                Onay kodu elinize ulaşmadı mı?
              </a>
            </div>
          </div>
          
          <div class="well mt20">
            <h5 class="mt0">
              Kaydınızı tamamlamak için :
            </h5>
            1. E-postanıza gönderilen "Makina Türkiye Kayıt Onay" e-postanızı
            açın,
            <br />
            2. E-postanızdaki onay butonuna tıklayın.
            <br />
            Link
            Çalışmadığında onay kodunu 
            <a href="/Uyelik/Aktivasyon/">
              buradan
            </a>
            girebilirsiniz.
            <br />
            <span class="text-danger">
              Not: Gönderilen e-postayı bulamazsanız,
              lütfen SPAM, Junk veya Bulk klasörüne bakın. 
            </span>
          </div>
          
          <div class="well mt20">
            Adres defterinize 
            <b>
              haber@makinaturkiye.com
            </b>
            veya 
            <b>
              onay@makinaturkiye.com
            </b>
            adresini ekleyiniz. 
            <br />
            <br />
            Makinaturkiye.com'dan gelen hiçbir e-posta adresi için 
            <b>
              "Report
              Spam"
            </b>
            seçeneğini seçmeyiniz. 
            <br />
            <br />
            Spam olarak görünen e-postayı seçerek 
            <b>
              NOT SPAM
            </b>
            veya
            Spam Değil butonuna basınız.
          </div>
          
        </div>
        
      </div>
    </div>
      <%CompanyDemandMembership model1 = new CompanyDemandMembership(); %>
    <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
  </div>
    
</asp:Content>
