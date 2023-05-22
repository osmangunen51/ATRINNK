﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<FilterModel<ProductModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%--  <script src="/Scripts/Trinnk.js" type="text/javascript"></script>--%>
  <script src="/Scripts/JQuery-qtip.js" type="text/javascript"></script>
  <link href="/Content/qtip.css" rel="Stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <input type="hidden" name="OrderName" id="OrderName" value="ProductId" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <input type="hidden" name="ProductStatu" id="ProductStatu" value="<%: ViewData["ProductStatu"] ?? 0 %>" />
    <table cellpadding="13" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" style="width: 5%" unselectable="on" onclick="ProductPost('ProductNo', this);">
            İlan No
          </td>
          <td class="Header" style="width: 9%" unselectable="on" onclick="ProductPost('ProductName', this);">
            İlan Adı
          </td>
          <td class="Header" style="width: 6%" unselectable="on" onclick="ProductPost('CMain.CategoryName', this);">
            Kategori
          </td>
          <td class="Header" style="width: 6%" unselectable="on" onclick="ProductPost('CBrand.CategoryName', this);">
            Marka
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('CSeries.CategoryName', this);">
            Seri
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('CModel.CategoryName', this);">
            Model
          </td>
          <td class="Header" style="width: 5%" unselectable="on">
            Onay
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('OtherBrand', this);">
            Diğer Marka
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('OtherModel', this);">
            Diğer Model
          </td>
          <td class="Header" style="width: 5%" unselectable="on">
            Onay
          </td>
          <td class="Header" style="width: 2%" unselectable="on">
            Durum
          </td>
          <td class="Header" style="width: 6%" unselectable="on" onclick="ProductPost('MainPartyFullName', this);">
            Kayıt Eden
          </td>
          <td class="Header" style="width: 9%" unselectable="on" onclick="ProductPost('StoreName', this);">
            Mağaza Adı
          </td>
          <td class="Header" style="width: 5%" unselectable="on">
            Üye Tipi
          </td>
          <td class="Header" style="width: 3%" unselectable="on" onclick="ProductPost('ProductPrice', this);">
            Fiyat
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('ProductRecordDate', this);">
            Kayıt Tarihi
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('ProductLastViewDate', this)">
            Güncellenme Tarihi
          </td>
          <td class="Header" style="width: 6%">
            Araçlar
          </td>
          <td class="Header" style="width: 3%" unselectable="on">
          </td>
        </tr>
        <tr style="background-color: #F1F1F1;">
          <td class="CellBegin" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductNo" class="Search" style="width: 100%; border: none;" value="#" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 9%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductName" class="Search" style="width: 60%; border: none;" /><span
                      class="ui-icon ui-icon-close searchClear" onclick="clearSearch('ProductName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 6%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="FirstCategoryName" class="Search" style="width: 60%; border: none;" /><span
                      class="ui-icon ui-icon-close searchClear" onclick="clearSearch('FirstCategoryName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 6%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="NameBrand" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NameBrand');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="NameSeries" class="Search" style="width: 40%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NameSeries');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%; padding-right: 5px; border: solid 1px #CCC; background-color: #FFF">
                    <input id="NameModel" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NameModel');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="OtherBrand" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('OtherBrand');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="OtherModel" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('OtherModel');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 2%">
          </td>
          <td class="Cell" style="width: 6%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="UserName" class="Search" style="width: 50%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('UserName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 9%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="StoreName" class="Search" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td>
                    <select id="MemberType" name="MemberType" onchange="SearchPost();" w="75px">
                      <option value="0">< Seçiniz ></option>
                      <option value="20">Kurumsal</option>
                      <option value="10">Bireysel</option>
                    </select>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 3%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductPrice" class="Search" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%; padding-right: 5px; border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductRecordDate" class="Search date" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductLastViewDate" class="Search date" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 6%">
          </td>
          <td class="CellEnd" align="center" style="width: 3%">
            <input type="checkbox" class="ch" onclick="check();" />
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("ProductList", Model) %>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="19" style="border-color: #DDD; border-top: none;
            padding-right: 10px">
            <div style="width: 100%; height: auto; float: left; text-align: right;">
              <button type="button" style="cursor: pointer;" onclick="SendForStore(4);">
                Ürün Transfer
              </button>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <button type="button" style="cursor: pointer;" onclick="confirmMember(1);">
                Onayla
              </button>
              &nbsp;&nbsp;
              <button type="button" style="cursor: pointer;" onclick="confirmMember(3);">
                Onaylama
              </button>
              &nbsp;&nbsp;
              <button type="button" style="cursor: pointer;" onclick="confirmMember(2);">
                Sil
              </button>
            </div>
            <div style="width:140px; height:auto;margin-right:180px;float:right;">
           <div style="width:60px; height:auto;float:left;"> firma id:</div><div style="width:60px; height:auto;float:left;"><input id="StoreId" class="Search" style="width: 30px; border: none"></div>

            </div>
            <div style="width: 100%; height: auto; float: left; text-align: right; margin-top: 10px;">
              Toplam Kayıt : &nbsp;&nbsp;<strong>
                <%: Model.TotalRecord %></strong>
            </div>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
  <input type="hidden" name="StoreMainPartyId" id="StoreMainPartyId" value="<%:Request.QueryString["StoreMainPartyId"].ToInt32()%>" />
  <% if (Request.QueryString["ActiveType"] != null)
     { %>
  <input id="ProductActiveType" type="hidden" value="<%=Request.QueryString["ActiveType"].ToInt32() %>" />
  <% }
     else
     { %>
  <input id="ProductActiveType" type="hidden" value="" />
  <% } %>
  <script type="text/javascript">


    function DeletePost(productId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Product/Delete',
          data: { id: productId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            var e = data.m;
            if (e) {
              $('#row' + productId).hide();
            }
          }
        });
      }
      }

    function DeleteProductSure(productId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz, bu işlem geri alınamaz?')) {
        $.ajax({
          url: '/Product/ProductDelete',
          data: { id: productId },
          type: 'post',
          dataType: 'json',
          success: function (data) {

            if (data) {
              $('#row' + productId).hide();
            }
          }
        });
      }
    }

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
        $('#Page').val(page)
        var url = "<%:Request.RawUrl%>";
        if (url.indexOf("?") > 0) {
            url =url+ "&page=" + page;
        }
        else
            url = url + "?page=" + page;
        window.history.pushState("object or string", "Title", url);
      SearchPost();
    }

    function SearchPost() {

      if ($('#ProductNo').val().length == 1 || $('#ProductNo').val().length == 9) {
        $('#preLoading').show();

        $.ajax({
          url: '/Product/Index',
          data: {
            ProductNo: $('#ProductNo').val(),
            ProductName: $('#ProductName').val(),
            FirstCategoryName: $('#FirstCategoryName').val(),
            NameBrand: $('#NameBrand').val(),
            NameSeries: $('#NameSeries').val(),
            NameModel: $('#NameModel').val(),
            OtherBrand: $('#OtherBrand').val(),
            OtherModel: $('#OtherModel').val(),
            UserName: $('#UserName').val(),
            StoreName: $('#StoreName').val(),
            MemberType: $('#MemberType').val(),
            ProductPrice: $('#ProductPrice').val(),
            ProductRecordDate: $('#ProductRecordDate').val(),
            ProductLastViewDate: $('#ProductLastViewDate').val(),
            OrderName: $('#OrderName').val(),
            Order: $('#Order').val(),
            Page: $('#Page').val(),
            PageDimension: $('#PageDimension').val(),
            ProductActiveType: $('#ProductActiveType').val(),
            StoreMainPartyId: $('#StoreMainPartyId').val()
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

    }

    $(document).ready(function () {
//      $('.date').datepicker();

      //      $('.Search').keyup(SearchPost).change(SearchPost);

      $('.Search').keyup(function (e) {
        if (e.keyCode == 13) {
          $('.HeaderDown').removeClass('HeaderDown');
          $(e).addClass('HeaderDown');
          $('#Order').val(($('#Order').val() == 'DESC' ? 'ASC' : 'DESC'));
          $('#OrderName').val($(this).id);
          SearchPost();
        }
      });

    });


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

    function AddCategory(productId, brandName) {
      $.ajax({
        url: '/Product/InsertRow',
        data: {
          BrandName: brandName,
          ProductId: productId
        },
        type: 'post',
        success: function (result) {
          SearchPost();
        },
        error: function (x) {
          alert(x.responseText);
        }
      });
    }


    function BrandInsert(productId) {
      $.ajax({
        url: '/Product/BrandInsert',
        data: {
          ProductId: productId
        },
        type: 'post',
        success: function (data) {
          SearchPost();
        },
        error: function (x, a, r) {
          alert(x.responseText);
        }
      });
    }

    function ModelInsert(productId) {
      $.ajax({
        url: '/Product/ModelInsert',
        data: {
          ProductId: productId
        },
        type: 'post',
        success: function (data) {
          SearchPost();
        },
        error: function (x, a, r) {
          alert(x.responseText);
        }
      });
    }
    function ProductReadyForSale(productId) {
      $.ajax({
        url: '/Product/ProductReadyForSale',
        data: {
          ProductId: productId
        },
        type: 'post',
        success: function (data) {
          SearchPost();
        },
        error: function (x, a, r) {
          alert(x.responseText);
        }
      });
    }

    function SeriesInsert(productId) {
      $.ajax({
        url: '/Product/SeriesInsert',
        data: {
          ProductId: productId
        },
        type: 'post',
        success: function (data) {
          SearchPost();
        },
        error: function (x, a, r) {
          alert(x.responseText);
        }
      });
    }


    function check() {
      $('.CheckItems').attr('checked', $('.ch').attr('checked'));

      $('.CheckItems').each(function () {
        if ($(this).attr('checked')) {
          $('#row' + $(this).val()).removeClass('Row').addClass('RowAlternateActive');
        } else {
          $('#row' + $(this).val()).addClass('Row').removeClass('RowAlternateActive');
        }
      });
    }

    $(document).ready(function () {
      $('.CheckItems').click(function () {
        if ($(this).attr('checked')) {
          $('#row' + $(this).val()).removeClass('Row').addClass('RowAlternateActive');
        } else {
          $('#row' + $(this).val()).addClass('Row').removeClass('RowAlternateActive');
        }
      });
    });

    var idItems = "";
    function confirmMember(typeId) {

      $('#preLoading').show();

      $('.CheckItems').each(function () {

        if ($(this).attr('checked') == true) {
          idItems += $(this).val() + ",";
        }

      });

      $.ajax({
        url: '/Product/ConfirmMember',
        data: {
          CheckItem: idItems,
          type: typeId
        },
        type: 'post',
        success: function (data) {
          window.location.href = '/Product/Index/';
        },
        error: function (x, a, r) {
          $('#preLoading').hide();
        }
      });

    }
    function SendForStore(typeId) {

      $('#preLoading').show();

      $('.CheckItems').each(function () {

        if ($(this).attr('checked') == true) {
          idItems += $(this).val() + ",";
        }

      });

      $.ajax({
        url: '/Product/SendForStore',
        data: {
          CheckItem: idItems,
          type: typeId,
          StoreId: $('#StoreId').val()
        },
        type: 'post',
        success: function (data) {
          window.location.href = '/Product/Index/';
        },
        error: function (x, a, r) {
          $('#preLoading').hide();
        }
      });

    }



  </script>
</asp:Content>
