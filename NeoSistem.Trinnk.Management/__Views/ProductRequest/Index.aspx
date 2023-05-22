﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.Trinnk.Management.Models.ProductRequests.ProductRequestItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 Ürün Talepleri
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DeletePost(id) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {

                $.ajax({
                    url: '/ProductRequest/Delete',
                    data: {
                        ID: id
                    },
                    type: 'post',
                    success: function (data) {
                        $("#row" + id).hide();

                    },
                    error: function (x, a, r) {
                        alert("Error");

                    }
                });
            }
        }
                function PagingPost(curentpage) {
            $('#preLoading').show();
            $.ajax({
                url: '/ProductRequest/Index',
                data: {
                    page: curentpage
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <button style="margin-top: 10px;" onclick="window.location='/PreRegistrationStore/Create'">Yeni Ekle</button>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header">Üye Adı/Soyad
                    </td>
                    <td class="Header">Email
                    </td>
                    <td class="Header">Telefon
                    </td>
                    <td class="Header">Kategori
                    </td>
                    <td class="Header">Marka</td>
                    <td class="Header">Mesaj</td>
                    <td class="Header">Tarih</td>
                    <td class="HeaderEnd"></td>
                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_Item",Model) %>
            </tbody>
        </table>
    </div>
</asp:Content>

