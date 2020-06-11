<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<FilterModel<StoreModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Firmalar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%--<script src="/Scripts/MakinaTurkiye.js" type="text/javascript"></script>--%>
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
 
    <input type="hidden" name="PacketStatus" id="PacketStatus" value=" <%:Request.QueryString["PacketStatu"].ToByte()%>" />
    <input type="hidden" name="OrderName" id="OrderName" value="S.MainPartyId" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <table cellpadding="13" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" style="width: 5%;" unselectable="on" onclick="OrderPost('StoreNo', this);">
            Mağaza No
          </td>
          <td class="Header" style="width: 9%;" unselectable="on" onclick="OrderPost('MainPartyFullName', this);">
            Firma Yetkilisi
          </td>
          <td class="Header" style="width: 12%;" unselectable="on" onclick="OrderPost('StoreName', this);">
            Mağaza Adı
          </td>
          <td class="Header" style="width: 4%;" unselectable="on" onclick="OrderPost('StorePacketId', this);">
            Logo
          </td>
          <td class="Header" style="width: 8%;" unselectable="on" onclick="OrderPost('StoreRecordDate', this);">
            Kayıt Tarihi
          </td>
          <td class="Header" style="width: 4%;" unselectable="on" >
            Tele Satış S.
          </td>
            <td class="Header" style="width:4%">
              Portföy Yöneticisi
            </td>
          <td class="Header" style="width: 5%;" unselectable="on">
            Paket
          </td>
          <td class="Header" style="width: 5%;" unselectable="on">
            Mağaza Durumu
          </td>
    
      
          <td class="Header" style="width: 4%;" unselectable="on" onclick="OrderPost('CountryName', this);">
            Adres
          </td>
          <td class="Header" style="width: 10%;" unselectable="on" onclick="OrderPost('StoreWeb', this);">
            Web Adresi
          </td>
          <td class="Header" style="width: 3%;" unselectable="on" onclick="OrderPost('StoreClick', this);">
          F.T.S.
          </td>
          <td class="Header" style="width: 4%;" unselectable="on" onclick="OrderPost('StoreProduct', this);">
          F.Ü.S.
          </td>
          <td class="Header" style="width: 4%;" unselectable="on">
          Ü.T.S.
          </td>
          <td class="Header" style="width: 8%;">
            Araçlar
          </td>
        </tr>
        <tr style="background-color: #F1F1F1">
          <td class="CellBegin" align="center" style="width: 5%">
            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="StoreNo" class="Search" style="width: 75%; border: none;" value="###" />
                    <span class="ui-icon ui-icon-close searchClear" style="width: 16%;" onclick="$('#StoreNo').val('###');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 9%;">
            <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="MainPartyFullName" class="Search" style="width: 70%; border: none;" /><span
                      class="ui-icon ui-icon-close searchClear" onclick="clearSearch('MainPartyFullName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 12%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="StoreName" class="Search" style="width: 80%; border: none;" /><span class="ui-icon ui-icon-close searchClear"
                      onclick="clearSearch('StoreName');"> </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 4%">

          </td>
          <td class="Cell" style="width: 8%;">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="StoreRecordDate" class="Search date" style="width: 70%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('StoreRecordDate');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 4%;">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="">
                      <%var salesUsers = (List<SelectListItem>)(ViewData["SalesUsers"]);                       
                          %>
                      <%:Html.DropDownList("SalesUserId",salesUsers,new { @onchange = "SearchPost();" }) %>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
                      <td class="Cell" style="width: 4%;">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style=" ">
                    <%var portfoyUsers = (List<SelectListItem>)(ViewData["PortfoyUsers"]);                       
                          %>
                      <%:Html.DropDownList("PortfoyUserId",portfoyUsers,new { @onchange = "SearchPost();" }) %>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%;">
                    <select id="PacketId" name="PacketId" onchange="SearchPost();" style="width: 100%">
                      <option value="null">-- Tümü --</option>
                      <% foreach (var item in new MakinaTurkiyeEntities().Packets)
                         { %>
                      <option value="<%:item.PacketId %>">
                        <%:item.PacketName %></option>
                      <% } %>
                    </select>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%;">
                    <select id="StoreActiveType" name="StoreActiveType" onchange="SearchPost();" style="width: 100%">
                      <option value="null">-- Tümü --</option>
                      <option value="1">İnceleniyor</option>
                      <option value="2">Onaylandı</option>
                      <option value="3">Onaylanmadı</option>
                      <option value="4">Silindi</option>
                    </select>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%;">
          </td>
   
          <td class="Cell" style="width: 10%">
            <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="StoreWeb" class="Search" style="width: 70%; border: none" /><span class="ui-icon ui-icon-close searchClear"
                      onclick="clearSearch('StoreWeb');"></span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 3%;">
          </td>
          <td class="Cell" style="width: 4%;">
          </td>
          <td class="Cell" style="width: 4%;">
          </td>
          <td class="CellEnd" style="width: 8%;">
          </td>
        </tr>
      </thead>
        <tbody id="table">
       <%= Html.RenderHtmlPartial("StoreList", Model) %>
    
        </tbody>
<%--   <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="15" style="border-color: #DDD; border-top: none;
            padding-right: 10px">
            <div style="width: 100%; height: auto; float: left; text-align: right;">
            <a href="/Store/DownloadAsExcel" style="color:Black; text-decoration:none;">
      <img src="/Content/images/excelwrite.png" width="30px" height="30px"/>
    </a> &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
              Toplam Kayıt : &nbsp;&nbsp;<strong>
                <%: Model.TotalRecord %></strong>
            </div>
          </td>
        </tr>
      </tfoot>--%>
    </table>
  </div>
  <script type="text/javascript">
    function StoreAddExel(storeid) {
      $.ajax({
        url: '/Store/AddExel',
        data: {
          StoreId: storeid
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
    function DeletePost(storeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Store/Delete',
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
       window.history.pushState("object or string", "Title", "/Store/Index?page="+page);
      SearchPost();
    }
    
    function SearchPost() {
      if ($('#StoreNo').val().length == 3 || $('#StoreNo').val().length == 9) {
        $('#preLoading').show();

        $.ajax({
          url: '/Store/Index',
          data: {
            StoreNo: $('#StoreNo').val(),
            StoreName: $('#StoreName').val(),
            MainPartyFullName: $('#MainPartyFullName').val(),
            StorePacketId: $('#StorePacketId').val(),
            StoreRecordDate: $('#StoreRecordDate').val(),
            StorePacketEndDate: $('#StorePacketEndDate').val(),
            StoreActiveType: $('#StoreActiveType').val(),
            StoreWeb: $('#StoreWeb').val(),
            OrderName: $('#OrderName').val(),
            PacketId: $('#PacketId').val(),
            Order: $('#Order').val(),
            Page: $('#Page').val(),
            PageDimension: $('#PageDimension').val(),
              PacketStatus: $('#PacketStatus').val(),
              PortfoyUserId: $("#PortfoyUserId").val(),
              AuthorizedId: $("#SalesUserId").val()
          },
          type: 'post',
          success: function (data) {
            $('#table').html(data);
            $('#table tr td:first').each(function () {
              var text = $('#StoreName').val();
              $(this).html(
              $(this).text().replace(new RegExp(text, 'gi'), '<span style="background-color:#fff18c"><strong>' + $('#StoreName').val() + '</strong></span>')
            );
            });
            $('#preLoading').hide();
          },
          error: function (x, a, r) {
            $('#preLoading').hide();
            //$('#table').html(x.responseText);
          }
        });
      }
    }
    $(document).ready(function () {
      $('.date').datepicker();
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
      $('#StoreActiveType').val(type);
      switch (type) {
        case 3:
          $('#inTitle').html('| Yeni Gelen Mağazalar');
          $('#typeTitle').html('Yeni mağazalar listeleniyor.');
          $('#typeTable').show();
          break;
        case 2:
          $('#inTitle').html('| Onaylanmamış Mağazalar');
          $('#typeTitle').html('Onaylanmamış mağazalar listeleniyor.');
          $('#typeTable').show();
          break;
        case 1:
          $('#inTitle').html('| Onaylanmış Mağazalar');
          $('#typeTitle').html('Onaylanmış mağazalar listeleniyor.');
          $('#typeTable').show();
          break;
        default:
      }
      SearchPost();
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
    function confirmMember() {

      $('#preLoading').show();

      $('.CheckItems').each(function () {

        if ($(this).attr('checked') == true) {
          idItems += $(this).val() + ",";
        }
      });

      $.ajax({
        url: '/Store/ActiveSelectedStore',
        data: {
          CheckItem: idItems
        },
        type: 'post',
        success: function (data) {
          window.location.href = '/Store/Index/';
        },
        error: function (x, a, r) {
          $('#preLoading').hide();
        }
      });

    }    function DeleteStore(id) {
        if (confirm('Tamamen silmek istediğinize eminmisiniz, bu işlem geri alınamaz?')) {
                     window.location.href = '/Store/DeleteStore?storeMainPartyId='+id; 
        }
    }
  </script>
</asp:Content>
