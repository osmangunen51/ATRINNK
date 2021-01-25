<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<FilterModel<OrderModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">


        function StoreChangeClick(ID) {
            $("#storeDisplay" + ID).hide();
            $("#storeChange" + ID).show();
            $("#storeNameChange" + ID).show();
            $("#StoreChangePencil" + ID).hide();
        }
        function StoreNameUpdate(ID) {
            var newName = $("#storeNameChange" + ID).val();
            $.ajax({
                url: '/OrderFirm/UpdateStoreName',
                data: { id: ID, name: newName },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#storeDisplay" + ID).show();
                        $("#storeDisplay" + ID).html(newName);
                        $("#storeChange" + ID).hide();
                        $("#storeNameChange" + ID).hide();
                        $("#StoreChangePencil" + ID).show();
                    }
                    else {
                        alert('Düzenleme işlemi gerçekleştirelemedi');
                    }
                }
            });
        }
        function AddressChangeClick(ID) {
            $("#addressDisplay" + ID).hide();
            $("#addressChange" + ID).show();
            $("#addressNameChange" + ID).show();
            $("#AddressChangePencil" + ID).hide();
        }
        function AddressUpdate(ID) {
            var newAddress = $("#addressNameChange" + ID).val();
            $.ajax({
                url: '/OrderFirm/UpdateStoreAddress',
                data: { id: ID, newAddress: newAddress },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#addressDisplay" + ID).show();
                        $("#addressDisplay" + ID).html(newAddress);
                        $("#addressChange" + ID).hide();
                        $("#addressNameChange" + ID).hide();
                        $("#AddressChangePencil" + ID).show();
                    }
                    else {
                        alert('Düzenleme işlemi gerçekleştirelemedi');
                    }
                }
            });

        }
        function OrderEndDateChangeClick(ID) {
            $("#orderEndDateDisplay" + ID).hide();
            $("#orderEndDateChange" + ID).show();

            $("#orderEndDateDataChange" + ID).show();
            $("#OrderEndDateChangePencil" + ID).hide();
        }
        function OrderEndDateUpdate(ID) {
            var newData = $("#orderEndDateDataChange" + ID).val();
            $.ajax({
                url: '/OrderFirm/UpdateOrderEndDate',
                data: { id: ID, date: newData },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#orderEndDateDisplay" + ID).show();
                        $("#orderEndDateDisplay" + ID).html(newData);
                        $("#orderEndDateChange" + ID).hide();
                        $("#orderEndDateDataChange" + ID).hide();
                        $("#OrderEndDatePencil" + ID).show();
                    }
                    else {
                        alert('Düzenleme işlemi gerçekleştirelemedi');
                    }
                }
            });

        }

        function PaidMountChangeClick(ID) {
            $("#paidMountDisplay" + ID).hide();
            $("#paidMountChange" + ID).show();
            $("#paidMountNameChange" + ID).show();
            $("#paidMountChangePencil" + ID).hide();
        }
        function PaidMountUpdate(ID) {
            var paidPrice = $("#paidMountNameChange" + ID).val();
            $.ajax({
                url: '/OrderFirm/UpdatePaidMount',
                data: { id: ID, paidPrice: paidPrice },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#paidMountDisplay" + ID).show();
                        $("#paidMountDisplay" + ID).html(paidPrice);
                        $("#paidMountChange" + ID).hide();
                        $("#paidMountNameChange" + ID).hide();
                        $("#PaidMountChangePencil" + ID).show();
                    }
                    else {
                        alert('Düzenleme işlemi gerçekleştirelemedi');
                    }
                }
            });

        }



        function InvoiceNumberChangeClick(ID) {
            $("#invoiceNumberDisplay" + ID).hide();
            $("#invoiceNumberChange" + ID).show();
            $("#invoiceNumberNameChange" + ID).show();
            $("#InvoiceNumberChangePencil" + ID).hide();
        }
        function InvoiceNumberUpdate(ID) {
            var newInvoiceNumber = $("#invoiceNumberNameChange" + ID).val();
            $.ajax({
                url: '/OrderFirm/InvoiceNumberUpdate',
                data: { id: ID, invoiceNumber: newInvoiceNumber },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#invoiceNumberDisplay" + ID).show();
                        $("#invoiceNumberDisplay" + ID).html(newInvoiceNumber);
                        $("#invoiceNumberChange" + ID).hide();
                        $("#invoiceNumberNameChange" + ID).hide();
                        $("#InvoiceNumberChangePencil" + ID).show();
                    }
                    else if (data == false) {
                        alert('Aynı fatura numarası girilemez');
                    }
                    else {
                        alert('Değişiklik gerçekleştirilemedi');
                    }
                }
            });

        }
        function PriceChangeClick(ID) {
            $("#PriceDisplay" + ID).hide();
            $("#PriceChangeVal" + ID).show();
            $("#PriceNameChange" + ID).show();
            $("#PriceChangePencil" + ID).hide();
        }
        function PriceUpdate(ID) {
            var newPrice = $("#PriceChangeVal" + ID).val();
            $.ajax({
                url: '/OrderFirm/UpdatePrice',
                data: { orderId: ID, price: newPrice },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#PriceDisplay" + ID).show();
                        $("#PriceDisplay" + ID).html(CurrencyFormatted(newPrice));
                        $("#PriceChangeVal" + ID).hide();
                        $("#PriceNameChange" + ID).hide();
                        $("#PriceChangePencil" + ID).show();
                    }
                    else if (data == false) {
                        alert('Aynı fatura numarası girilemez');
                    }
                    else {
                        alert('Değişiklik gerçekleştirilemedi');
                    }
                }
            });

        }


          function RestAmountChangeClick(ID) {
            $("#RestAmountDisplay" + ID).hide();
            $("#RestAmountChangeVal" + ID).show();
            $("#RestAmountNameChange" + ID).show();
            $("#RestAmountChangePencil" + ID).hide();
        }
        function RestAmountUpdate(ID, restAmount1) {
            var newPrice = $("#RestAmountChangeVal" + ID).val();
            $.ajax({
                url: '/OrderFirm/UpdateRestAmount',
                data: { orderId: ID, newrestAmount: newPrice, lastRestAmout : restAmount1 },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#RestAmountDisplay" + ID).show();
                        $("#RestAmountDisplay" + ID).html(CurrencyFormatted(newPrice));
                        $("#RestAmountChangeVal" + ID).hide();
                        $("#RestAmountNameChange" + ID).hide();
                        $("#RestAmountChangePencil" + ID).show();
                    }
                    else if (data == false) {
                        alert('Ödeme bulunamadı.');
                    }
                    else {
                        alert('Değişiklik gerçekleştirilemedi');
                    }
                }
            });

        }
        function CurrencyFormatted(amount) {
            var i = parseFloat(amount);
            if (isNaN(i)) { i = 0.00; }
            var minus = '';
            if (i < 0) { minus = '-'; }
            i = Math.abs(i);
            i = parseInt((i + .005) * 100);
            i = i / 100;
            s = new String(i);
            if (s.indexOf('.') < 0) { s += '.00'; }
            if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
            s = minus + s;
            return s;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">

        <input type="hidden" name="OrderName" id="OrderName" value="OrderId" onclick="OrderPost('OrderId', this);" />
        <input type="hidden" name="Order" id="Order" value="DESC" />
        <input type="hidden" name="Page" id="Page" value="1" />
        <div style="float: right">
            <div style="float:left;">
                Paket Bitiş Tarihi

          <input type="text" style="float: none;" autocomplete="off" name="orderEndDate1" placeholder="İlk Tarih.." class="date Search" id="orderEndDate1" />
                /
          <input type="text" style="float: none;" autocomplete="off" name="orderEndDate2" placeholder="Son Tarih.." class="date Search" id="orderEndDate2" />

            </div>
            <div style="float: left;">
                Kayıt Tarihi:

          <input type="text" style="float: none;" autocomplete="off" name="createStartDate" placeholder="İlk Tarih.." class="date Search" id="createStartDate" />/<input autocomplete="off" placeholder="Son Tarih.." style="float: none;" class="date Search" type="text" name="createEndDate" id="createEndDate" />
            </div>
            <div style="float: left">
                Ödeme Durumu:
               <select id="PaidBill" onchange="SearchPost()">
                   <option value="0">Tümü</option>
                   <option value="1">Ödeme Alınacak</option>
                   <option value="2">Ödeme Alındı</option>
               </select>
                Sipariş Durumu:
          <select id="OrderCancelled" onchange="SearchPost()">
              <option value="2">Tümü</option>
              <option value="0">Sadece İptal Edilmeyen</option>
              <option value="1">Sadece İptal Edilen</option>
          </select>
            </div>
            <div id="clear:both"></div>
        </div>
        <table cellpadding="11" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>

                    <%--      <td  class="Header" style="width: 7%;" unselectable="on">Fatura No</td>--%>

                    <td class="Header HeaderBegin" style="width: 7%;" unselectable="on" onclick="OrderPost('OrderNo', this);">Sipariş No
                    </td>
                    <td class="Header" style="width: 9%;" unselectable="on" onclick="OrderPost('StoreName', this);">Firma Ünvanı
                    </td>

                    <td class="Header" style="width: 6%;" unselectable="on" onclick="OrderPost('PacketName', this);">Paket Tipi
                    </td>
                    <td class="Header" style="width: 7%;" unselectable="on">Paket Durumu
                    </td>
                    <td class="Header" style="width: 8%;" unselectable="on">Sipariş Türü
                    </td>
                    <td class="Header" style="width: 15%;" unselectable="on">Fatura Adres
                    </td>
                    <td class="Header" style="width: 6%;" unselectable="on">Vergi Dairesi
                    </td>


                    <td class="Header" style="width: 6%;" unselectable="on">Kayıt Tarihi
                    </td>
                    <td class="Header" style="width: 6%;" unselectable="on">Paket Bitiş
                    </td>

                    <td class="Header" style="width: 6%;" unselectable="on">Fiyat
                    </td>

                    <td class="Header" style="width: 4%;">Kalan</td>
                    <td class="Header" style="width: 4%" onclick="OrderPost('PayDate', this);">Ödeme Tarihi
                    </td>

                    <td class="Header" style="width: 5%;">Araçlar
                    </td>
                    <td class="Header">Satış S.</td>
                    <td class="Header" style="width: 100px!important;"></td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <%--          <td class="CellBegin">
             <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF;">
                    <input id="InvoiceNumber" class="Search" style="width: 75%; border: none;" />
                    <span class="ui-icon ui-icon-close searchClear" style="width: 15%;"></span>
                  </td>
                </tr>
              </tbody>
            </table>
            </td>--%>
                    <td class="CellBegin" align="center">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input id="OrderNo" class="Search" style="width: 75%; border: none;" />
                                        <span class="ui-icon ui-icon-close searchClear" style="width: 15%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" align="center">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input id="StoreName" class="Search" style="width: 85%; border: none;" /><span class="ui-icon ui-icon-close searchClear"
                                            onclick="clearSearch('StoreName');" style="width: 7%;"> </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell" align="center"></td>

                    <td class="Cell"></td>
                    <td class="Cell"></td>

                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="RecordDate" class="Search date" autocomplete="off" style="border: none; width: 75%;" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('RecordDate');"
                                            style="width: 13%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>

                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input placeholder="İlk tarih" id="PayDate" autocomplete="off" class="Search date" style="width: 75%; border: none;" />
                                        <span class="ui-icon ui-icon-close searchClear" style="width: 15%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="LastPayDate" placeholder="Son tarih" autocomplete="off" class="Search date" style="border: none; width: 75%;" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('LastPayDate');"
                                            style="width: 13%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <%var userList = (List<SelectListItem>)ViewData["SalesUsers"]; %>
                                        <%:Html.DropDownList("SalesUserId",userList,new {@onchange="SearchPost();" }) %>

                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </td>
                    <td class="CellEnd" style="width: 7%;"></td>
                </tr>
            </thead>
            <tbody id="table">
                <%= Html.RenderHtmlPartial("OrderList", Model)%>
            </tbody>
            <tfoot>
            </tfoot>
        </table>
    </div>
    <input id="PacketStatu" name="PacketStatu" type="hidden" value="<%:Request.QueryString["PacketStatu"].ToByte() %>" />

    <script type="text/javascript">

        function clearSearch(Id) {
            $('#' + Id).val('');
            $('#' + Id).trigger('keyup');
        }
        function ExportExcel() {

            var url = '/OrderFirm/ExportExel';
            url = url + "?OrderNo=" + $('#OrderNo').val() + "&StoreName=" + $('#StoreName').val() + "&RecordDate=" + $('#MainParRecordDatetyRecordDate').val() + "&PacketStatu="
                + $('#PacketStatu').val() + "&OrderName=" + $('#OrderName').val() + "&Order=" + $("#Order").val() + "&Page=" + $("#Page").val() + "&PayDate1=" + $("#PayDate").val() + "&PageDimension=" + $("#PageDimension").val() + "&LastPayDate=" + $("#LastPayDate").val() + "&PaidBill=" + $("#PaidBill").val() + "&SalesUserId=" + $("#SalesUserId").val() + "&OrderCancelled=" + $("#OrderCancelled").val() + "&RegisterStartDate=" + $("#createStartDate").val() + "&RegisterEndDate=" + $("#createEndDate").val();

            window.open(url);

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
            $("#ExcelButon").show();
            $('#preLoading').show();
            $.ajax({
                url: '/OrderFirm/Index',
                data: {
                    InvoiceNumber: $("#InvoiceNumber").val(),
                    OrderNo: $('#OrderNo').val(),
                    StoreName: $('#StoreName').val(),
                    PacketName: $('#PacketName').val(),
                    RecordDate: $('#MainParRecordDatetyRecordDate').val(),
                    LastPayDate: $("#LastPayDate").val(),
                    PacketStatu: $('#PacketStatu').val(),
                    OrderName: $('#OrderName').val(),
                    Order: $('#Order').val(),
                    Page: $('#Page').val(),
                    PayDate: $("#PayDate").val(),
                    PageDimension: $('#PageDimension').val(),
                    PaidBill: $("#PaidBill").val(),
                    SalesUserId: $("#SalesUserId").val(),
                    OrderCancelled: $("#OrderCancelled").val(),
                    RegisterStartDate: $("#createStartDate").val(),
                    RegisterEndDate: $("#createEndDate").val(),
                    OrderEndDate1: $("#orderEndDate1").val(),
                    OrderEndDate2: $("#orderEndDate2").val()
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

    </script>
</asp:Content>
