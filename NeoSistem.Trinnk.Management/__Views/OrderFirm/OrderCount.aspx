<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<NeoSistem.Trinnk.Management.Models.Orders.OrderCountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 Aylık Satış Sayıları
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <table cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <tr>
                <th class="Header HeaderBegin" style="width:7%">Kullanıcı Adı</th>
                <th class="Header" style="width:5%">Satış Saysı</th>
                <th class="Header" style="width:5%">Toplam Ciro</th>
                <th class="Header HeaderEnd" style="width:80%">Firmalar</th>
            </tr>
            <tr>
                <td class="CellBegin" align="center"></td>
                <td class="Cell">
                Ay
                    </td>
                <td class="Cell">
                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                        <tbody>
                            <tr>
                                <td style="border: solid 1px #CCC; background-color: #CCC;">
                                    <%:Html.DropDownList("Month", Model.Months, new { @onChange="SearchPost()"}) %>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td class="Cell CellEnd"></td>
            </tr>
            <tbody id="tableBody">
                <%=Html.RenderHtmlPartial("_OrderCountList", Model.OrderCountItems) %>
                         </tbody>
        </table>
        <script type="text/javascript">

            function clearSearch(Id) {
                $('#' + Id).val('');
                $('#' + Id).trigger('keyup');
            }
            
            function SearchPost() {
                $('#preLoading').show();
                $.ajax({
                    url: '/OrderFirm/OrderCountItem',
                    data: {
                        month : $("#Month").val()
                    },
                    type: 'get',
                    success: function (data) {
                        $('#tableBody').html(data);
                        $('#preLoading').hide();
                    },
                    error: function (x, a, r) {
                        $('#preLoading').hide();
                    }
                });
            }


        </script>
</asp:Content>
