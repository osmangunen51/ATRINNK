<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.PreRegistrationItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ön Kayıtlı Firmalar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.Search').keyup(function (e) {
                if (e.keyCode == 13) {
                    SearchPost();
                }
            });

        });
        function DeletePost(id) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {

                $.ajax({
                    url: '/PreRegistrationStore/Delete',
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
            $("#Page").val(page);
            SearchPost();
        }

        function SearchPost() {
            $('#preLoading').show();
            $.ajax({
                url: '/PreRegistrationStore/Index',
                data: {
                    storeName: $("#StoreName").val(),
                    page: $('#Page').val(),
                    email: $('#Email').val()
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
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.HiddenFor(x=>x.CurrentPage, new {@id="Page" }) %>
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0px;">
        <button style="margin-top: 10px;" onclick="window.location='/PreRegistrationStore/Create'">Yeni Ekle</button>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header" style="width:200px;">Firma Adı
                    </td>
                    <td class="Header">Yetkili Adı/Soyadı
                    </td>
                    <td class="Header">Email
                    </td>
                    <td class="Header">Telefon Numarası
                    </td>
                    <td class="Header">Diğer Telefon</td>
                    <td class="Header">Web Adresi</td>
                    <td class="Header">Tarih</td>
                    <td class="HeaderEnd"></td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <td class="CellBegin" align="center"></td>
                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input id="StoreName" class="Search" style="width: 75%; border: none;" />
                                        <span class="ui-icon ui-icon-close searchClear" style="width: 15%;"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell">
                        <table>
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                        <input id="Email" class="Search" style="width: 75%; border: none;" />
                                        <span class="ui-icon ui-icon-close searchClear" style="width: 15%;"></span>
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

                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_Item",Model) %>
            </tbody>
        </table>
    </div>

</asp:Content>

