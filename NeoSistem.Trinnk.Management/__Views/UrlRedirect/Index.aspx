<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<Trinnk.Entities.Tables.Common.UrlRedirect>>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Url Yönlendirmeler
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
        <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header">#</td>
                    <td class="Header" style="width: 35%;">Eski Url</td>
                    <td class="Header" style="width: 5%;">Yeni Url</td>
                    <td class="Header" style="width: 5%;">Tarih</td>
                    <td class="Header HeaderEnd" style="width:15%;">Araçlar
                        <a href="/UrlRedirect/Create" style="float: right;" id="lightbox_click" rel="superbox[iframe]" title="Ödemeleri Gör" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" style="float: right;" aria-disabled="false"><span class="ui-button-text">&nbsp;&nbsp;&nbsp;&nbsp;Yeni Ekle&nbsp;&nbsp;&nbsp;&nbsp;</span></a>
                    </td>
                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_UrlRedirectList", Model) %>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DeletePost(descId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/UrlRedirect/Delete',
                    data: { id: descId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        if (data) {
                            $('#row' + descId).hide();
                        }

                    }
                });
            }


        }
        function PagingPost(p) {
            $('#preLoading').show();
            $.ajax({
                url: '/UrlRedirect/Index',
                data: {
                    page: p
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
    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox-1.js"></script>
    <script type="text/javascript">
        $(function () {
            $.superbox.settings = {
                closeTxt: "Kapat",
                loadTxt: "Yükleniyor...",
                nextTxt: "Sonraki",
                prevTxt: "Önceki"
            };
            $.superbox();
        });
    </script>
    <style type="text/css">
        /* Custom Theme */
        #superbox-overlay { background: #e0e4cc; }

        #superbox-container .loading { width: 32px; height: 32px; margin: 0 auto; text-indent: -9999px; background: url(styles/loader.gif) no-repeat 0 0; }

        #superbox .close a { float: right; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; }

            #superbox .close a span { color: #fff; }

        #superbox .nextprev a { float: left; margin-right: 5px; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; color: #fff; }

        #superbox .nextprev .disabled { background: #ccc; cursor: default; }
    </style>

</asp:Content>

