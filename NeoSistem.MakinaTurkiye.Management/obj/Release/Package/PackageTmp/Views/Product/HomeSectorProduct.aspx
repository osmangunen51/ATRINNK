﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Catolog.HomeSectorProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Anasayfa Sektör Seçilen Ürünler
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
        <table cellpadding="13" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <%:Html.HiddenFor(x=>x.HomeSectorProductItemsFilter.CurrentPage, new {id="PageIndex" }) %>
            <thead>
                <tr>
                    <td class="HeaderBegin" style="width: 6%" unselectable="on">Sektör
                    </td>
                    <td class="Header">Firma Adı</td>
                    <td class="Header Header" style="width: 5%" unselectable="on">İlan No
                    </td>
                    <td class="Header" style="width: 12%" unselectable="on">İlan Adı
                    </td>


                    <td class="Header" style="width: 5%" unselectable="on">Başlangıç Tarihi
                    </td>
                    <td class="Header" style="width: 5%" unselectable="on">Bitiş Tarihi
                    </td>
                    <td class="Header" style="width: 5%" unselectable="on">Tip 
                    </td>
                    <td class="Header" style="width: 5%" unselectable="on">Sıralama
                    </td>
                    <td class="Header" style="width: 5%">Araçlar
                    </td>
                    <td class="HeaderEnd"></td>
                </tr>
                <tr style="background-color: #F1F1F1;">
                    <td class="CellBegin" style="width: 6%">
                        <%= Html.DropDownListFor(m => m.SelectedCategoryId,Model.Sectors, new { onchange="SearchPost();" })%>
                    </td>

                    <td class="Cell" style="width: 5%"></td>
                    <td class="Cell" align="center" style="width: 9%"></td>
                    <td class="Cell" style="width: 5%"></td>
                    <td class="Cell" style="width: 5%"></td>
                    <td class="Cell" style="width: 5%"></td>

                    <td class="Cell" style="width: 5%">
                        <select name="type" id="Type" onchange="SearchPost();">
                            <option value="0">Tümü</option>
                            <option value="2">Ücretli</option>
                            <option value="1">Ücretsiz</option>
                        </select>

                    </td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="CellEnd" style="width: 5%">

                        <select name="time" id="time" onchange="SearchPost();">
                            <option value="0">Tümü</option>
                            <option value="1">Süresi Geçenler</option>
                            <option value="2">Devam Edenler</option>
                        </select>
                    </td>
                </tr>
            </thead>
            <tbody id="table1">
                <%= Html.RenderHtmlPartial("_HomeSectorProductList", Model.HomeSectorProductItemsFilter) %>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        function PagePost(val) {
            $("#PageIndex").val(val);
            SearchPost();
        }
        function SearchPost() {
            $("#preLoading").show();
            $.ajax({
                url: '/Product/HomeSectorProduct',
                data: {
                    CategoryId: $('#SelectedCategoryId').val(),
                    PageIndex: $("#PageIndex").val(),
                    Type: $("#Type").val(),
                    Time : $("#time").val()
                },
                type: 'post',
                success: function (data) {
                    $("#table1").html(data);
                    $('#preLoading').hide();
                },
                error: function (x, a, r) {
                    $('#preLoading').hide();
                }
            });
        }
    </script>
</asp:Content>
