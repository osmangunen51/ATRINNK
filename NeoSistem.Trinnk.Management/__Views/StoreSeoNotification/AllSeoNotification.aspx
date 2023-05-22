<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.Trinnk.Management.Models.BaseMemberDescriptionModelItem>>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Seo Bildirimleri
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 20px;">
        <h2 style="float: left;">Firma Seo Bildirimleri Tümü
        </h2>
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <%:Html.Hidden("currentPage", Model.CurrentPage) %>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header" style="width: 10%">Firma Adı</td>
                    <td class="Header" style="width: 7%">Başlık</td>
                    <td class="Header" style="width: 45%">İçerik</td>
                    <td class="Header" style="width: 5%">İşlem Tarihi</td>
                    <td class="Header" style="width: 5%">Hatırlatma Tarihi</td>
                    <td class="Header" style="width: 5%">Atayan</td>
                    <td class="Header" style="width: 5%">Atanan</td>

                    <td class="Header HeaderEnd" style="width: 5%">Araçlar</td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <td class="CellBegin" align="center"></td>
                    <td class="Cell"></td>
                    <td class="CellBegin" align="center"></td>
                    <td class="Cell" style="width: 8%;">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input  id="createdDate" class="Search date" style="width: 75%; border: none;" />

                                        <span class="ui-icon ui-icon-close searchClear"
                                            onclick="clearSearch('date');" style="width: 7%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="CellEnd"></td>
                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_SeoNotificationList", Model) %>
            </tbody>
        </table>
    </div>


</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $('.date').datepicker().val();
            $('.Search').keyup(function (e) {
                if (e.keyCode == 13) {
                    SearchPost();
                }
            });

        });
        function PagePost(p) {
            $("#currentPage").val(p);
            SearchPost();
        }
        function SearchPost() {
            $('#preLoading').show();
            $.ajax({
                url: '/StoreSeoNotification/AllSeoNotification',
                data: {
                    page: $("#currentPage").val(),
                    createdDate: $("#createdDate").val()
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

        function clearSearch(Id) {
            $('#' + Id).val('');
            $('#' + Id).trigger('keyup');
        }
    </script>
</asp:Content>

