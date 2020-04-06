﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<ProductDopingModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 Dopingli Ürünler
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
   <script src="/Scripts/JQuery-qtip.js" type="text/javascript"></script>
   <link href="/Content/qtip.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
      border-width: 5px;" id="preLoading">
       <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
      <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
   <input type="button" style="margin-top:5px;" name="" onclick="RateCalculate();" value="Ürünleri Sırala" />
    <div style="width: 100%; margin: 0 auto;">
         <table cellpadding="13" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="HeaderBegin" style="width: 6%" unselectable="on" >
             Kategori
          </td>
          <td class="Header">Firma Adı</td>
            <td class="Header Header" style="width: 5%" unselectable="on">
            İlan No
          </td>
          <td class="Header" style="width: 12%" unselectable="on" >
            İlan Adı
          </td>
          
          <td class="Header" style="width: 5%" unselectable="on" >
            Marka
          </td>
          <td class="Header" style="width: 5%" unselectable="on" >
            Başlangıç Tarihi
          </td>
          <td class="Header" style="width: 5%" unselectable="on" >
             Bitiş Tarihi
          </td>
          <td class="Header" style="width: 5%" unselectable="on" >
             Alınan Ücret
          </td>
          <td class="HeaderEnd" style="width: 5%">
            Araçlar
          </td>
        </tr>
        <tr style="background-color: #F1F1F1;">
          <td class="CellBegin" style="width: 6%">
              <%= Html.DropDownListFor(m => m.SelectedCategoryId,Model.AvairableCategories, new { onchange="SearchPost();" })%>
          </td>
                   <td class="CellBegin" style="width: 6%">
              <%= Html.DropDownListFor(m => m.StoreMainPartyId,Model.Stores, new { onchange="SearchPost();" })%>
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" align="center" style="width: 9%">
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="CellEnd" style="width: 5%">
          </td>
        </tr>
      </thead>
      <tbody id="table">
          <%= Html.RenderHtmlPartial("DopingShowcaseList", Model.DopingModels) %>
      </tbody>
    </table>
    </div>
    <script type="text/javascript">
        function RateCalculate() {
              $('#preLoading').show();
                      $.ajax({
                    url: '/Product/ProductRateCalculate',
                    data: {                     
                
                    },
                    type: 'post',
                          success: function (data) {
                        
                        $('#preLoading').hide();
                        alert("Sıralama İşlemi Tamamlanmıştır");

                    },
                    error: function (x, a, r) {
                        $('#preLoading').hide();
                        alert("Bir Sorun Oluştur. Yazılımcınız ile irtibata geçiniz.");
                    }
                });
        }
        function SearchPost() {
            $("#preLoading").show();
                $.ajax({
                    url: '/Product/DopingShowcase',
                    data: {                     
                        CategoryId: $('#SelectedCategoryId').val(),
                        StoreMainPartyId: $("#StoreMainPartyId").val()
                    },
                    type: 'post',
                    success: function (data) {
                        $('#table').html(data);

                        $('#preLoading').hide();
                    },
                    error: function (x, a, r) {
                        $('#preLoading').hide();
                    }
                });

            }
    </script>
</asp:Content>