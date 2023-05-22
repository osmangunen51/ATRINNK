<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<FilterModel<NewsModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <input type="hidden" name="OrderName" id="OrderName" value="NewsId" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <table id="typeTable" border="0" cellpadding="0" cellspacing="0" class="TableList"
      style="width: 100%; display: none">
      <tr>
        <td style="border: 1px solid #59b4e3; background-color: #cee5fd; padding: 5px">
          <span id="typeTitle" style="font-weight: 700; font-variant: small-caps"></span><span
            class="ui-icon ui-icon-close searchClear" onclick="$('#typeTable').fadeOut('slow');"
            title="Kapat"></span>
        </td>
      </tr>
    </table>
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on" onclick="OrderPost('NewsTitle', this);">
            Haber
          </td>
          <td class="Header" width="80px" unselectable="on" onclick="OrderPost('NewsDate', this);">
            Tarih
          </td>
          <td class="Header" width="50px" unselectable="on" onclick="OrderPost('Active', this);">
            Aktif
          </td>
          <td class="Header" style="width: 70px; height: 19px">
          </td>
        </tr>
        <tr style="background-color: #F1F1F1">
          <td class="CellBegin" align="center">
            <table style="width: 100%; border-collapse: separate" border="0" cellspacing="0"
              cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%; padding-right: 5px; border: solid 1px #CCC; background-color: #FFF">
                    <input id="NewsTitle" class="Search" style="width: 90%; border: none;" /><span class="ui-icon ui-icon-close searchClear"
                      onclick="clearSearch('NewsTitle');"> </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell">
            <table style="width: 100%; border-collapse: separate" border="0" cellspacing="0"
              cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%;">
                    <input id="NewsDate" class="Search date" style="width: 70%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NewsDate');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell">
            <table style="width: 100%; border-collapse: separate" border="0" cellspacing="0"
              cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%;">
                    <select id="Active" name="Active" onchange="SearchPost();" w="75px">
                      <option value="null">-- Tümü --</option>
                      <option value="true">Aktif</option>
                      <option value="false">Pasif</option>
                    </select>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="CellEnd" style="width: 20px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("NewsList", Model) %>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="4" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.TotalRecord %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
  <script type="text/javascript">

    function DeletePost(storeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/News/Delete',
          data: { id: storeId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            var e = data.m;
            if (e) {
              $('#row' + storeId).hide();
            }
          }
        });
      }
    }

    function clearSearch(Id) {
      $('#' + Id).val('');
      $('#' + Id).trigger('keyup');
    }

    function OrderPost(orderName, e) {
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
        url: '/News/Index',
        type: 'post',
        data: {
          NewsTitle: $('#NewsTitle').val(),
          NewsDate: $('#NewsDate').val(),
          Active: $('#Active').val(),
          OrderName: $('#OrderName').val(),
          Order: $('#Order').val(),
          Page: $('#Page').val()
        },
        success: function (data) {
          $('#table').html(data);
          $('#preLoading').hide();
          RegisterTooltip();
        },
        error: function (x) {
          $('#preLoading').hide();
          //$('#table').html(x.responseText);
        }
      });
    }

    $(document).ready(function () {
      $('.date').datepicker();
      $('.Search').keyup(SearchPost).change(SearchPost);
      RegisterTooltip();
    });

    function RegisterTooltip() {
      $('.Detail').each(function () {
        $(this).qtip({
          content: { url: '/News/Tooltip', data: { storeId: $(this).attr('newsId') }, method: 'get', text: 'Yukleniyor' },
          position: {
            corner: {
              target: 'topLeft',
              tooltip: 'bottomLeft'
            },
            target: 'mouse',
            adjust: { mouse: false, x: -500, y: -10 }
          },
          hide: { when: 'unfocus', fixed: true },
          show: 'click',
          style: {
            width: 500,
            padding: 5,
            border: {
              width: 3,
              radius: 8
            },
            name: 'dark'
          }
        })
      });
    }
 
  </script>
</asp:Content>
