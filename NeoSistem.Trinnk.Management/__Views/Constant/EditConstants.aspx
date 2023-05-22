﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Entities.Constant>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Sabit Alanlar Güncelleme
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
    $(document).ready(function () {
      $('td').attr('valign', 'top');

      $('#ContstantPropertie').focus(function () {
        $('#activeText').val('#ContstantPropertie')
      });

      $('#ConstantTitle').focus(function () {
        $('#activeText').val('#ConstantTitle')
      });

      $('#ConstantName').focus(function () {
        $('#activeText').val('#ConstantName')
      });

      
      $('button').click(function () {
        if ($('#activeText').val() == '') {
          alert('Parametre göndermek için göndermek istediğiniz alanı seçmelisiniz.')
        }
        else {
          $($('#activeText').val()).val($($('#activeText').val()).val() + ' ' + $(this).val());
        }
      });

    });
 
   
  </script>
  <input id="activeText" type="hidden" value="" />
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Güncelle</legend>
            <div class="editor-label" style="margin:20px;">
            <span style="color:#868686; font-family:Arial; font-size:14px;">
              Sabit Başlık 
              <br />
              </span>
            </div>
            <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.ConstantName, new { style = "width: 300px;border: solid 1px #bababa;" })%>
                <%: Html.ValidationMessageFor(model => model.ConstantName) %>
            </div>
              <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.Order, new { style = "width: 300px;border: solid 1px #bababa;" })%>
                <%: Html.ValidationMessageFor(model => model.Order) %>
            </div>
            <%if (Model.ConstantType != 19 && Model.ConstantType!=35)
              {  %>
            <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
                Mail Konusu
            </div>
            <%}
              else
              {  %>
              <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
                Katsayı Değeri
            </div>
            <%} %>
            <div class="editor-field" style="margin-left:20px;">
                 <%: Html.TextBoxFor(model => model.ConstantTitle, new { style = "width: 300px;border: solid 1px #bababa;" })%>
            </div>
            <%if (Model.ConstantType != 19  && Model.ConstantType!=35)
              {  %>
            <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
                Açıklama
            </div>
            <div class="editor-field" style="margin-left:5px; margin-top:15px;">
                <%if (Model.ConstantId != 247 && Model.ConstantId != 248 && Model.ConstantId != 246 && (Model.ConstantType == 14 || Model.ConstantType == 15))
                  { %>
                          <%= Html.TextAreaFor(model => model.ConstantMailContent)%>
                <%: Html.ValidationMessageFor(model => model.ConstantMailContent)%>
                        
                <%}else{ %>
                <%= Html.TextAreaFor(model => model.ContstantPropertie)%>
                <%: Html.ValidationMessageFor(model => model.ContstantPropertie)%>
                <%} %>
            </div>
            <div style="border-style:solid; border-color:Black; margin:10px; border-width:1px;">
            Üye mail:
            <button type="button" title="Aktivasyon kodu" value="#activationcode#">
            Aktivasyon kodu</button>
          <button type="button" title=" Üye Ad Soyadı" value="#uyeadisoyadi#">
            Üye Ad Soyadı</button>
          <button type="button" title="Kullanıcı adı" value="#kullaniciadi#">
            Kullanıcı adı</button>
          <button type="button" title="Üyelik tipi" value="#uyeliktipi#">
            Üyelik tipi</button>
          <button type="button" title="Paket Tipi" value="#pakettipi#">
            Paket Tipi</button>
          <button type="button" title="Şifre" value="#sifre#">
            Şifre</button>
      <br />
            Firma maili: 
             <button type="button" title="İlan Tekil Tıklama" value="#tekililantiklama#">
            İlan Tekil Tıklama</button>
          <button type="button" title="Çoğul İlan Tıklama" value="#cogulilantiklama#">
            Çoğul İlan Tıklama</button>
          <button type="button" title="İlan Tıklama Oranı" value="#ilantiklamaorani#">
            İlan Tıklama Oranı</button>
          <button type="button" title="Firma Çoğul Tıklama" value="#firmacogultiklama#">
            Firma Çoğul Tıklama</button>
          <button type="button" title="İlan Sayısı" value="#ilansayisi#">
            İlan Sayısı</button>
          <button type="button" title="Firma Ürünler Köprü" value="#firmaurunlerkopru#">
            Firma Ürünler Köprü</button>
          <button type="button" title="Firma İstatistik köprü" value="#firmaistatistikkopru#">
            Firma İstatistik köprü</button>
          <button type="button" title="Firma Düzenle" value="#firmaduzenle#">
            Firma Düzenle</button>
          <button type="button" title="İlan İstatistik köprü" value="#ilanistatistikkopru#">
            İlan İstatistik köprü</button>
          <button type="button" title="Kullanıcı adı" value="#kullaniciadi#">
            Kullanıcı adı</button>
          <button type="button" title="Şifre" value="#sifre#">
            Şifre</button>
          <button type="button" title="Üyelik paket" value="#uyelikpaket#">
            Üyelik paket</button>
          <button type="button" title="Üye Adı Soyadı" value="#uyeadisoyadi#">
            Üye Adı Soyadı</button>
          <button type="button" title="bunu ekleyince buradan diye link koyar vede bu link firmanın düzenleme sayfasına gitmeyi sağlar" value="#firmaduzenlemekopru#">
            Firma Düzenleme Köprü</button>
          <button type="button" title="Firma Köprü" value="#firmakopru#">
            Firma Köprü</button>
          <button type="button" title="Firma Adı" value="#firmaadi#">
            Firma Adı</button>
            <button type="button" title="firma üyelik yükseltme" value="#firmauyelikyukseltme#">
            firma üyelik yükseltme</button>
            <button type="button" title="Son Ödeme Tarihi" value="#sonodemetarih#">Son Ödeme Tarihi</button>
            <button type="button" title="Paket Tutar" value="#pakettutar#">Paket Tutar</button>
            <button type="button" title="Kalan Tutar" value="#kalantutar#">Kalan Tutar</button>
            <button type="button" title="Mail İmza" value="#signature#">
            Mail İmza</button>    
            <br />
          İlan maili:
          <br />
          <button type="button" title="Bireyselden Firma Üyeliğine Köprü" value="#bireyseldenfirmauyeligikopru#">
            Bireyselden Firma Üyeliğine Köprü</button>
          <button type="button" title="İlan no" value="#ilanno#">
            İlan no</button>
          <button type="button" title="İlan Adı" value="#ilanadi#">
            İlan Adı</button>
          <button type="button" title="Ürün Adı" value="#urunadi#">
            Ürün Adı</button>
          <button type="button" title="İlan Düzenleme köprü" value="#ilanduzenlemekopru#">
            İlan Düzenleme köprü</button>
          <button type="button" title="İlan giriş" value="#ilangiris#">
            İlan giriş</button>
          <button type="button" title="Üye Adı Soyadı" value="#uyeadisoyadi#">
            Üye Adı Soyadı</button>
          <button type="button" title="İlan Adı Köprü" value="#ilanadikopru#">
            İlan Adı Köprü</button>            
            </div>
            <%} %>
            <p>
                <input type="submit" value="Güncelle" />
            </p>
        </fieldset>

    <% } %>

    <div>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <%: Html.ActionLink("Geri", "Index") %>
    </div>
     <script type="text/javascript" defer="defer">
         <%if (Model.ConstantId != 247 && Model.ConstantId != 246 && Model.ConstantId!=248 && (Model.ConstantType == 14 || Model.ConstantType == 15))
           {%>
         var editor = CKEDITOR.replace('ConstantMailContent', { toolbar: 'webtool', allowedContent: true });
         CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
         <%}else{%>
         var editor = CKEDITOR.replace('ContstantPropertie', { toolbar: 'webtool', allowedContent: true });
       CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
         <%}%>
  </script>
</asp:Content>

