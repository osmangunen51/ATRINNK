﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreNewItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Firma Haberler
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function PagingNew(curentpage) {
            $('#preLoading').show();
            $.ajax({
                url: '/Store/New',
                data: {
                    page: curentpage,
                    newType:<%:Request.QueryString["newType"]%>
                },
                type: 'post',
                success: function (data) {
                    $("#table").html(data);
                    $('#preLoading').hide();

                },
                error: function (x, a, r) {
                    alert("Error");

                }
            });
        }
        function ConfirmNew(confirm) {

            $('#preLoading').show();
            var idItems = "";
            $('.checkboxNew').each(function () {

                if ($(this).attr('checked') == true) {
                    idItems += $(this).val() + ",";
                }
   

            });
                         $.ajax({
                    url: '/Store/ChangeNewActive',
                    data: {
                        CheckItem: idItems,
                        set: confirm
                    },
                    type: 'post',
                    success: function (data) {
                        window.location.href = '/Store/New';
                    },
                    error: function (x, a, r) {
                        $('#preLoading').hide();
                    }
                });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%;">
      <button style="margin-top:10px;" onclick="window.location='/Store/CreateNew?newType=<%:Request.QueryString["newType"] %>'" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">Yeni Ekle</span></button>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width:100%;  margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header">Fotoğraf</td>
                    <td class="Header">Firma Adı</td>
                    <td class="Header">Başlık</td>
                    <td class="Header">Eklenme Tarihi</td>
                    <td class="Header">Güncelleme Tarihi</td>
                    <td class="Header">Görüntülenme</td>
                    <td class="Header">Durum</td>
                    <td class="Header HeaderEnd"></td>

                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_StoreNewItem",Model) %>
            </tbody>
        </table>
    </div>
</asp:Content>

