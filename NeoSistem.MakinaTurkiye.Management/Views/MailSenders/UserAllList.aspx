﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<FilterModel<MemberModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /* Custom Theme */
        #superbox-overlay {
            background: #e0e4cc;
        }

        #superbox-container .loading {
            width: 32px;
            height: 32px;
            margin: 0 auto;
            text-indent: -9999px;
            background: url(styles/loader.gif) no-repeat 0 0;
        }

        #superbox .close a {
            float: right;
            padding: 0 5px;
            line-height: 20px;
            background: #333;
            cursor: pointer;
        }

            #superbox .close a span {
                color: #fff;
            }

        #superbox .nextprev a {
            float: left;
            margin-right: 5px;
            padding: 0 5px;
            line-height: 20px;
            background: #333;
            cursor: pointer;
            color: #fff;
        }

        #superbox .nextprev .disabled {
            background: #ccc;
            cursor: default;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <input type="hidden" name="OrderName" id="OrderName" value="M.MainPartyId" />
        <input type="hidden" name="Order" id="Order" value="DESC" />
        <input type="hidden" name="Page" id="Page" value="1" />
        <table cellpadding="8" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin" style="width: 5%;" unselectable="off">Seç
                    </td>                    
                    <td class="Header" style="width: 10%;" unselectable="on" onclick="OrderPost('MemberNo', this);">Üye No
                    </td>
                    <td class="Header" style="width: 20%;" unselectable="on" onclick="OrderPost('MemberName', this);">Üye Adı Soyadı
                    </td>
                    <td class="Header" style="width: 10%;" unselectable="on" onclick="OrderPost('StoreName', this);">Mağaza Adı
                    </td>
                    <td class="Header" style="width: 10%;" unselectable="on">Üyelik Tipi
                    </td>
                    <td class="Header" style="width: 15%;" unselectable="on" onclick="OrderPost('MemberEmail', this);">Üye E-Posta
                    </td>
                    <td class="Header" style="width: 7%;" unselectable="on">Durumu
                    </td>
                    <td class="Header" style="width: 10%;" unselectable="on" onclick="OrderPost('MainPartyRecordDate', this);">Kayıt Tarihi
                    </td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <td class="CellBegin" align="center" style="width: 5%;">
                        
                    </td>
                    <td class="Cell" align="center" style="width: 10%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input id="MemberNo" class="Search" value="##" style="width: 85%; border: none;" />
                                        <span class="ui-icon ui-icon-close searchClear" style="width: 10%;" onclick="$('#MemberNo').val('##');"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" align="center" style="width: 20%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input id="MainPartyFullName" class="Search" style="width: 85%; border: none;" /><span
                                            class="ui-icon ui-icon-close searchClear" onclick="clearSearch('MainPartyFullName');"
                                            style="width: 5%;"> </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" align="center" style="width: 20%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="StoreName" class="Search" style="border: none; width: 80%" /><span class="ui-icon ui-icon-close searchClear"
                                            onclick="clearSearch('StoreName');"> </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" style="width: 10%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td>
                                        <select id="MemberType" name="MemberType" onchange="SearchPost();" style="width: 100%;">
                                            <option value="0">-- Tümü --</option>
                                            <option value="5">Hızlı</option>
                                            <option value="10">Bireysel</option>
                                            <option value="20">Kurumsal</option>
                                        </select>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" style="width: 15%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="MemberEmail" class="Search" style="border: none; width: 85%" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('MemberEmail');"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" style="width: 7%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <select id="Active_Text" name="Active_Text" onchange="SearchPost();" style="width: 100%;">
                                            <option value="">-- Tümü --</option>
                                            <option value="true">Aktif</option>
                                            <option value="false">Pasif</option>
                                        </select>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell" style="width: 10%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="MainPartyRecordDate" class="Search date" style="border: none; width: 85%;" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('MainPartyRecordDate');"
                                            style="width: 10%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </thead>
            <tbody id="table">
                <%= Html.RenderHtmlPartial("MemberList", Model) %>
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


            if ($('#MemberNo').val().length == 2 || $('#MemberNo').val().length == 9) {
                $('#preLoading').show();
                $.ajax({
                    url: '/MailSenders/UserAllList',
                    data: {
                        MemberNo: $('#MemberNo').val(),
                        MainPartyFullName: $('#MainPartyFullName').val(),
                        MemberType: $('#MemberType').val(),
                        StoreName: $('#StoreName').val(),
                        MemberEmail: $('#MemberEmail').val(),
                        MainPartyRecordDate: $('#MainPartyRecordDate').val(),
                        Active_Text: $('#Active_Text').val(),
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
