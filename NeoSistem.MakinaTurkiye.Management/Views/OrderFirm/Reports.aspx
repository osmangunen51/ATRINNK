﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Orders.OrderReportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tahsilat Raporu
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <input type="hidden" name="Order" id="Order" value="DESC" />
        <input type="hidden" name="Page" id="Page" value="1" />
        <table cellpadding="11" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin" style="width: 7%;" unselectable="on" onclick="OrderPost('OrderNo', this);">Sipariş No
                    </td>
                    <td class="Header" style="width: 9%;" unselectable="on" onclick="OrderPost('StoreName', this);">Firma Ünvanı
                    </td>
                    <td class="Header" style="width: 7%;" unselectable="on">Paket Durumu
                    </td>
                    <td class="Header" style="width: 8%;" unselectable="on">Sipariş Türü
                    </td>




                    <td class="Header">Açıklama
                    </td>

                    <td class="Header">Satış S.</td>
                    <td class="Header">Fatura</td>
                    <td class="Header" style="width: 6%;" unselectable="on">Kayıt Tarihi
                    </td>
                    <td class="Header" style="width: 6%;" unselectable="on">Fiyat
                    </td>

                    <td class="Header" style="width: 6%;" unselectable="on">Alınan Miktar
                    </td>
                    <td class="Header" style="width: 6%;" unselectable="on">Kalan Miktar
                    </td>
                    <td class="Header" style="width: 6%;" unselectable="on">Ödeme Tarihi
                    </td>
                </tr>
                <tr>
                    <td class="Cell CellBegin"></td>
                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="StoreName" placeholder="Firma Ünvan" autocomplete="off" class="Search" style="border: none; width: 75%;" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('LastPayDate');"
                                            style="width: 13%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </td>
                    <td class="Cell"></td>
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
                    <td class="Cell "></td>
                    <td class="Cell"></td>
                    <td class="Cell CellEnd">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="PayDate" class="Search date" autocomplete="off" style="border: none; width: 75%;" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('RecordDate');"
                                            style="width: 13%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>

            </thead>
            <tbody id="table">
                <%= Html.RenderHtmlPartial("_OrderReportList", Model)%>
            </tbody>
            <tfoot>
            </tfoot>
        </table>
    </div>


    <script type="text/javascript">

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
            $("#ExcelButon").show();
            $('#preLoading').show();
            $.ajax({
                url: '/OrderFirm/Reports',
                data: {
                    page: $("#Page").val(),
                    recordDate: $("#RecordDate").val(),
                    storeName: $("#StoreName").val(),
                    payDate: $("#PayDate").val()
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
