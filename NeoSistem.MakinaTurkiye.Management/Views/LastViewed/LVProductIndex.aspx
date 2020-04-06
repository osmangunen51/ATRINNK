﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<FilterModel<ProductModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%--  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript"></script>--%>
  <script src="/Scripts/JQuery-qtip.js" type="text/javascript"></script>
  <link href="/Content/qtip.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
    <input type="hidden" name="OrderName" id="OrderName" value="P.SingularViewCount" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <input type="hidden" name="ProductStatu" id="ProductStatu" value="<%: ViewData["ProductStatu"] ?? 0 %>" />
    <table cellpadding="8" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" style="width: 5%" unselectable="on">
            İlan No
          </td>
          <td class="Header" style="width: 25%" unselectable="on">
            İlan Adı
          </td>
          <td class="Header" style="width: 14%" unselectable="on">
            Kategori
          </td>
          <td class="Header" style="width: 13%" unselectable="on">
            Marka
          </td>
          <td class="Header" style="width: 13%" unselectable="on">
            Seri
          </td>
          <td class="Header" style="width: 13%" unselectable="on">
            Model
          </td>
          <td class="Header" style="width: 8%" unselectable="on">
            Ziyaretçi
          </td>
          <td class="Header" style="width: 8%" unselectable="on">
            Tekil Ziyaretçi
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("LVProductList", Model)%>
      </tbody>
    </table>
  </div>
  <script type="text/javascript">

    function clearSearch(Id) {
      $('#' + Id).val('');
      $('#' + Id).trigger('keyup');
    }

    function ProductPost(orderName, e) {
      $('.HeaderDown').removeClass('HeaderDown');
      $(e).addClass('HeaderDown');
      $('#Order').val(($('#Order').val() == 'DESC' ? 'ASC' : 'DESC'));
      $('#OrderName').val(orderName);
      SearchPost();
    }

    function PagePost(page) {
      $('#Page').val(page);
      SearchPost();
    }

    function SearchPost() {

      $('#preLoading').show();

      $.ajax({
        url: '/LastViewed/LVProductIndex',
        data: {
          OrderName: $('#OrderName').val(),
          Order: $('#Order').val(),
          Page: $('#Page').val(),
          PageDimension: $('#PageDimension').val()
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

    function SetActiveType(type) {
      $('#ProductStatu').val(type);
      switch (type) {
        case 3:
          $('#inTitle').html('| Yeni Gelen İlanlar');
          $('#typeTitle').html('Yeni İlanlar Listeleniyor.');
          $('#typeTable').show();
          break;
        case 2:
          $('#inTitle').html('| Onaylanmamış İlanlar');
          $('#typeTitle').html('Onaylanmamış İlanlar Listeleniyor.');
          $('#typeTable').show();
          break;
        case 1:
          $('#inTitle').html('| Onaylanmış Mağazalar');
          $('#typeTitle').html('Onaylanmış Mağazalar Listeleniyor.');
          $('#typeTable').show();
          break;
        default:
      }
      SearchPost();
    }
     
 
      
  </script>
</asp:Content>
