<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.Logs.LogItemModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Log
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function DeletePost(id) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {

                $.ajax({
                    url: '/ApplicationLog/Delete',
                    data: {
                        ID: id
                    },
                    type: 'post',
                    success: function (data) {
                        $("#row" + id).hide();

                    },
                    error: function (x, a, r) {


                    }
                });
            }
        }
        function PagingPost(page) {
            $('#preLoading').show();
            $("#Page").val(page);
            SearchPost();
        }

        function SearchPost() {

            $.ajax({
                url: '/ApplicationLog/Index',
                data: {
                    page: $('#Page').val(),
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.HiddenFor(x=>x.CurrentPage, new {@id="Page" }) %>
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0px;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header" style="width: 70px;">Tip
                    </td>
                    <td class="Header">Logger
                    </td>
                    <td class="Header">Mesaj
                    </td>
                    <td class="Header">Tarih
                    </td>
                    <td class="HeaderEnd"></td>
                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_Item",Model) %>
            </tbody>
        </table>
    </div>

</asp:Content>

