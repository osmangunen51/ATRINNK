﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SeoModel>" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Edit
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
      $(document).ready(function () {
          $('td').attr('valign', 'top');

          $('#PageName').focus(function () {
              $('#activeText').val('#PageName')
          });

          $('#Title').focus(function () {
              $('#activeText').val('#Title')
          });

          $('#Description').focus(function () {
              $('#activeText').val('#Description')
          });

          $('#Abstract').focus(function () {
              $('#activeText').val('#Abstract')
          });

          $('#Keywords').focus(function () {
              $('#activeText').val('#Keywords')
          });

          $('#Classification').focus(function () {
              $('#activeText').val('#Classification')
          });

          $('#Robots').focus(function () {
              $('#activeText').val('#Robots')
          });

          $('#RevisitAfter').focus(function () {
              $('#activeText').val('#RevisitAfter')
          });

          $('#Robots').focus(function () {
              $('#activeText').val('#Robots')
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
  <% using (Html.BeginForm())
     { %>
    <%:Html.HiddenFor(x=>x.Abstract) %>
  <div style="float: left; width: 700px;">
    <% using (Html.BeginPanel())
       { %>
    <input id="activeText" type="hidden" value="" />
    <table border="0" cellpadding="5" cellspacing="0" style="font-size: 13px; margin: 10px;
      width: 730px;">
      <tr>
        <td colspan="3" align="right">
          <button type="submit" style="height: 27px">
            Kaydet
          </button>
          <button type="button" style="height: 27px" onclick="window.location='/Seo'">
            İptal
          </button>
          <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 10px">
          </div>
        </td>
      </tr>
      <tr>
        <td style="width: 170px">
          <%: Html.LabelFor(m => m.PageName) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.PageName, new { @readonly = "readonly" })%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.PageName); %>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Title)%>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.Title, new { style = "width: 400px" })%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Title); %>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Description) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextAreaFor(m => m.Description)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Description); %>
        </td>
      </tr>
<%--      <tr>
        <td>
          <%: Html.LabelFor(m => m.Abstract) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextAreaFor(m => m.Abstract)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Abstract); %>
        </td>
      </tr>--%>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Keywords) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextAreaFor(m => m.Keywords)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Keywords); %>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Classification) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.Classification)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Classification); %>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Robots) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.Robots)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Robots); %>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.RevisitAfter) %>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.RevisitAfter)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.RevisitAfter); %>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Parameter)%>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <button type="button" title="Kategori" value="{Kategori}">
            Kategori</button>
              <button type="button" title="Kategori Baslik" value="{KategoriBaslik}">
            KategoriBaslik</button>
          <button type="button" title="Tüm Üst Kategori" value="{UstKategori}">
            UstKategori</button>
          <button type="button" title="Birinci Üst Kategori" value="{IlkUstKategori}">
            IlkUstKategori</button>
            
             <button type="button" title="Birinci Üst Kategori" value="{IlkUstKategoriBaslik}">
            IlkUstKategoriBaslik</button>
          <button type="button" title="Marka" value="{Marka}">
            Marka</button>
          <button type="button" title="Model Markası" value="{ModelMarka}">
            ModelMarka</button>
          <button type="button" title="Model" value="{Model}">
            Model</button>
          <button type="button" title="Seri" value="{Seri}">
            Seri</button>
          <button type="button" title="Ürün Tipi" value="{UrunTipi}">
            UrunTipi</button>
          <button type="button" title="Ürün Durumu" value="{UrunDurumu}">
            UrunDurumu</button>
          <button type="button" title="Satış Detayı" value="{SatisDetayi}">
            SatisDetayi</button>
          <button type="button" title="Kısa Detay" value="{KisaDetay}">
            KisaDetay</button>
          <button type="button" title="Fiyatı" value="{Fiyati}">
            Fiyati</button>
          <button type="button" title="Model Yılı" value="{ModelYili}">
            ModelYili</button>
          <button type="button" title="Ürün Adı" value="{UrunAdi}">
            UrunAdi</button>
          <button type="button" title="Firma Adı" value="{FirmaAdi}">
            FirmaAdi</button>
          <button type="button" title="Ürün Grupları" value="{UrunGrubuIsimleri}">
            UrunGrubuIsimleri</button>
          <button type="button" title="Aktif Kategorinin Alt Kategorisi" value="{AltKategoriForAktifKategori}">
            AltKategoriForAktifKategori</button>
            <button type="button" title="Ulke" value="{Ulke}">
            Ülke</button>
            <button type="button" title="Sehir" value="{Sehir}">
            Şehir</button>
             <button type="button" title="İlçe" value="{Ilce}">
            İlçe</button>
            <button type="button" title="Aranan Kelime" value="{ArananKelime}">
            Aranan Kelime</button>
             <button type="button" title="Aranan Kelime" value="{KategoriBaslik}">
            Kategori Başlık</button>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Parameter); %>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="right">
          <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-bottom: 10px">
          </div>
          <button type="submit" style="height: 27px">
            Kaydet
          </button>
          <button type="button" style="height: 27px" onclick="window.location='/Seo'">
            İptal
          </button>
        </td>
      </tr>
    </table>
    <% } %>
  </div>
  <div style="float: left; width: 477px; margin-top: 7px">
    <%= Html.ValidationSummary("", new { style = "width: 375px; " })%>
  </div>
  <% } %>
</asp:Content>