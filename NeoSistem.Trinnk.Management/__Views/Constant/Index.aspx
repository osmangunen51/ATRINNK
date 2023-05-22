<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<ICollection<ConstantModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MK Yönetim Sistemi | Sabit Alanlar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
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
    <script type="text/javascript">
        function tıkla() {
            $('#lightbox_click').trigger('click');
        }
        $(document).ready(function () {
            $('#NewConstantForm').dialog({ autoOpen: false, modal: true, width: 420, height: 700, resizable: true });
        });
        $(function () {
            $.superbox.settings = {
                closeTxt: "Kapat",
                loadTxt: "Yükleniyor...",
                nextTxt: "Sonraki",
                prevTxt: "Önceki"
            };
            $.superbox();
        });
        function DeletePost(constantId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Constant/Delete',
                    data: { id: constantId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data.m;
                        if (e) {
                            $('#row' + constantId).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }

        function GetConstant(constantType) {
            $.ajax({
                url: '/Constant/GetConstantType',
                data: { ConstantType: constantType },
                type: 'post',
                success: function (data) {
                    $('#ConstantType').val(constantType);
                    if ($('#ConstantType').val() == 13 || $('#ConstantType').val() == 14 || $('#ConstantType').val() == 15 || $('#ConstantType').val() == 18 || $('#ConstantType').val() == 19 || $('#ConstantType').val() == 35 || $('#ConstantType').val() == 20 || $("#ConstantType").val() == 23) {
                        $('#tabloadd').show();
                    }
                    else {
                        $('#tabloadd').hide();
                    }
                    $('#table').html(data);
                },
                error: function (x, l, e) {
                }
            });
        }

        function closeDialog() {
            $('#NewConstantForm').dialog('close');
        }

        function openDialog() {
            $('#NewConstantForm').attr('Title', 'Sabit Tip Ekle');
            $('#ConstantId').val(0);
            $('#ConstantName').val('');
            $('#Order').val('1');
            $('#NewConstantForm').dialog('open');
        }

        function saveConstant() {
            if ($('#ConstantType').val() == 0) {
                alert('Sabit tip eklemek için sabit alanı seçmeniz gerekmektedir.');
                closeDialog();
            }
            else {
                $.ajax({
                    url: '/Constant/Create',
                    data: {
                        ConstantType: $('#ConstantType').val(),
                        ConstantName: $('#ConstantName').val(),
                        Order: $('#Order').val(),
                        ConstantId: $('#ConstantId').val(),
                        ConstantPropertie: $('#ConstantPropertie').val(),
                        MemberDescriptionIsOpened: document.querySelector('#MemberDescriptionIsOpened').checked,
                    },
                    type: 'post',
                    success: function (data) {
                        $('#table').html(data);
                        closeDialog();
                    },
                    error: function (x, l, e) {
                        alert(e.responseText);
                    }
                });
            }
        }

        function EditConstant(constantId) {
            $('#NewConstantForm').attr('Title', 'Sabit Tip Düzenle');
            $('#NewConstantForm').dialog('open');
            $('#ConstantId').val(constantId);

            var constantName = '';
            var constantPropertie = '';
            $.ajax({
                url: '/Constant/GetConstantName',
                data: { ConstantId: constantId },
                type: 'post',
                success: function (data) {
                    constantName = data.constantName;
                    order = data.order;
                    constantPropertie = data.constantPropertie;
                    MemberDescriptionIsOpened = data.MemberDescriptionIsOpened;
                    $('#ConstantName').val(constantName);
                    $('#ConstantPropertie').val(constantPropertie);
                    $('#ConstantPropertie').val(constantPropertie);
                    $('#Order').val(order);
                    document.querySelector('#MemberDescriptionIsOpened').checked = MemberDescriptionIsOpened;
                }, error: function (x, l, e) {
                    alert(e.responseText);
                }
            });
        }
        function SubConstantInsert(constantId) {
            $('#SubConstantForm').attr('Title', 'Alt Başlık Ekle');
            $('#SubConstantForm').dialog('open');
            $('#ContantId').val(constantId);

            var constantName = '';
            var constantPropertie = '';
            $.ajax({
                url: '/Constant/GetSubConsant',
                data: { ConstantId: constantId },
                type: 'post',
                success: function (data) {
                    constantName = data.constantName;
                    order = data.order;
                    constantPropertie = data.constantPropertie;
                    alert(constantPropertie);
                    $('#ConstantName').val(constantName);
                    $('#ConstantPropertie').val(constantPropertie);
                    $('#Order').val(order);
                }, error: function (x, l, e) {
                    alert(e.responseText);
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="ConstantId" type="hidden" value="0" />
    <input id="ConstantType" type="hidden" value="0" />
    <div id="NewConstantForm" title="">
        <%= Html.RenderHtmlPartial("ConstantForm", new ConstantModel())%>
    </div>
    <div id="SubConstantForm">
            
    </div>
    <div style="float: left; width: 15%; padding-top: 10px;">
        <select size="25" onchange="GetConstant($(this).val())">
            <option value="0">< Sabit Tipi Seçiniz ></option>
            <option value="<%=(byte)ConstantType.ProductPayType%>">Ürün Ödeme Durumu</option>
            <option value="<%=(byte)ConstantType.ProductSalesType%>">Ürün Satış Durumu</option>
            <option value="<%=(byte)ConstantType.ProductCargoType%>">Ürün Kargo Durumu</option>
            <option value="<%=(byte)ConstantType.ProductType%>">Ürün Tipi</option>
            <option value="<%=(byte)ConstantType.ProductStatu%>">Ürün Durumu</option>
            <option value="<%=(byte)ConstantType.ProductBriefDetail%>">Ürün Kısa Detay</option>
            <option value="<%=(byte)ConstantType.StoerType%>">Mağaza Şirket Türü</option>
            <option value="<%=(byte)ConstantType.MoneyCondition%>">Mağaza Para Durumu</option>
            <option value="<%=(byte)ConstantType.MemberTitleType%>">Üye Yetki Tipi</option>
            <option value="<%=(byte)ConstantType.EmployeesCount%>">Çalışan Sayısı</option>
            <option value="<%=(byte)ConstantType.StoreCapital%>">Şirket Sermayesi</option>
            <option value="<%=(byte)ConstantType.StoreEndorsement%>">Yıllık Ciro</option>
            <option value="<%=(byte)ConstantType.MessageType%>">İlan Onay Durumu</option>
            <option value="<%=(byte)ConstantType.MemberType%>">Üye Mailing</option>
            <option value="<%=(byte)ConstantType.StoreactivationType%>">Firma Mailing</option>
            <option value="<%=(byte)ConstantType.Mensei%>">Menşei</option>
            <option value="<%=(byte)ConstantType.SiparisDurumu%>">Teslim Durumu</option>
            <option value="<%=(byte)ConstantType.MemberMail%>">Toplu Mail</option>
            <option value="<%=(byte)ConstantType.Katsayilar%>">Ürün Katsayılar</option>
             <option value="<%=(byte)ConstantType.FirmaKatsayilar%>">Firma Katsayılar</option>
            <option value="<%=(byte)ConstantType.Messages%>">Mesajlar</option>
            <option value="<%=(byte)ConstantType.Birim%>">Birim</option>
            <option value="<%=(byte)ConstantType.FootorDegerleri%>">Footor Değerleri</option>
            <option value="<%=(byte)ConstantType.DiscountPacketDescription %>">İndirimli Paket Açıklama</option>
            <option value="<%=(byte)ConstantType.StoreDescriptionType %>">Firma Açıklama Tipi</option>
            <option value="<%=(byte)ConstantType.UserSpecialMailType %>">Kullanıcı Özel Mail Tipi</option>
            <option value="<%=(byte)ConstantType.ProblemType %>">Sorun Tipleri</option>
            <option value="<%=(byte)ConstantType.StoreProfileHomeDecriptionTemplate %>">Firma Profil Açklama Template</option>
            <option value="<%=(byte)ConstantType.PaymentBank %>">Havale Bankalar</option>
            <option value="<%=(byte)ConstantType.SeoDescriptionTitle %>">Firma Seo Açıklama Başlık</option>
            <option value="<%=(byte)ConstantType.CategoryFooterTopDescription %>">Kategori Footer Üstü Açıklama</option>
            <option value="<%=(byte)ConstantType.PacketSalesFooter %>">Paket Satın Al Sayfası Alt</option>
            <option value="<%=(byte)ConstantType.CrmYardimKategori %>">Crm Yardım Kategori</option>
        </select>&nbsp;&nbsp;
    <button onclick="openDialog();">
        Sabit Başlık Ekle</button>
    </div>
    <div style="width: 85%; margin: 0 auto; float: left;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin" unselectable="on">Sabit Adı
                    </td>
                    <td class="Header" style="width: 70px; height: 19px">Order
                    </td>
                              <td class="Header" style="width: 70px; height: 19px">
                    </td>
                    <td id="tabloadd" class="Header" style="width: 70px; height: 19px; display: none;"></td>
                    <td class="Header" style="width: 70px; height: 19px"></td>
                </tr>
            </thead>
            <tbody id="table">
                <%= Html.RenderHtmlPartial("ConstantList")%>
            </tbody>
            <tfoot>
                <tr>
                    <td class="ui-state ui-state-hover" style="border-color: #DDD; border-top: none; padding-right: 10px"
                        colspan="1" valign="top" style="width: 100px;">
                        <%:Ajax.ActionLink("Hesapla", "ProductRateCalculate", new AjaxOptions() { UpdateTargetId = "statisticproduct", HttpMethod = "Post" })%>
                    </td>
                               <td class="ui-state ui-state-hover" style="border-color: #DDD; border-top: none; padding-right: 10px">
                    </td>
                                   <td class="ui-state ui-state-hover" style="border-color: #DDD; border-top: none; padding-right: 10px">
                    </td>
                    <td class="ui-state ui-state-hover" style="border-color: #DDD; border-top: none; padding-right: 10px"
                        colspan="1">:
                        <div id="statisticproduct" style="margin-top: 5px">
                        </div>
                    </td>
                    <td class="ui-state ui-state-hover" colspan="1" align="right" style="border-color: #DDD; border-top: none; padding-right: 10px">Toplam Kayıt : &nbsp;&nbsp;<strong>
                        <%= Model.Count %></strong>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
