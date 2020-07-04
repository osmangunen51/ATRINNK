<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BuyPacketModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Paket Satın Al
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <%=Html.RenderHtmlPartial("Style") %>
    <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
    <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.date').datepicker();
            var installment = 0;
            $("#OrderType").change(function () {
                $("#creditDatesContainer").html("");
                $("#Installment").val("");
                var orderType = this.value;
                if (orderType != "0") {
                    if (orderType == "<%:(byte)Ordertype.HavaleTaksit%>") {
                        $("#InstallmentContainer").show();
                        $("#FirstDateContainer").hide();
                    }
                    else if (orderType == "<%:(byte)Ordertype.KrediKarti%>") {
                        $("#InstallmentContainer").hide();
                        $("#FirstDateContainer").hide();
                    }
                    else if (orderType == "<%:(byte)Ordertype.KrediKartiVade%>") {
                        $("#InstallmentContainer").hide();
                        $("#FirstDateContainer").show();
                    }

                    else {
                        $("#InstallmentContainer").hide();
                        $("#FirstDateContainer").show();

                    }
                }
                else {
                    alert("Ödeme türü seçiniz");
                }
            });
            $("#Installment").change(function () {
                installment = this.value;
                console.log(installment);
                $("#creditDatesContainer").html("");
                $("#creditDatesContainer").show();
                for (var i = 1; i <= installment; i++) {
                    $("#creditDatesContainer").append("<tr><td>Tarih " + i + "</td><td>:</td><td><input class='date' type='text' name='installmenttext' id='installmenttext" + i + "' /></td></tr>")
                    $('.date').datepicker();
                }
            });

            $('#DiscountAmount').change(function (e) {
                var discountType = $("#DiscounType").val();
                var packetPrice = Number($("#PacketPrice").val());
                var val = $('#DiscountAmount').val();
                console.log("değer", val, "pakcetprice", packetPrice);
                var newPrice = 0;
                if (discountType == "1") {
                    newPrice = packetPrice - (packetPrice * val / 100);
                }
                else if (discountType == "2") {
                    newPrice = packetPrice - val;
                }
                $("#PriceValue").html(newPrice);
                $("#PriceValueWithTax").html(newPrice * 1.18);

            });
        });
        function BuyPacket() {

            var storeMainPartyId = $("#StoreMainPartyId").val();
            var orderType = $("#OrderType").val();
            var intallment = $("#Installment").val();
            var description = $("#Description").val();

            var dates = "";
            if (orderType == "0") {
                alert("Ödeme türü seçiniz");
                return false;
            }

            $("#preLoading").show();
            if (orderType == "<%:(byte)Ordertype.HavaleTaksit%>") {
                if (intallment == 0) {
                    alert("Lütfen taksit sayısı seçiniz");
                    $("#preLoading").hide();
                    return false;
                }
                else {

                    for (var i = 1; i <= intallment; i++) {
                        if ($("#installmenttext" + i).val() == "") {
                            alert("Lütfen tüm taksit tarihlerini giriş yapınız");
                            $("#preLoading").hide();
                            return false;
                        }
                        dates = dates + $("#installmenttext" + i).val() + ",";
                    }
                }
            }
            else if (orderType == "<%:(byte)Ordertype.KrediKartiVade%>" || orderType == <%:(byte)Ordertype.Havale%>) {
                if ($("#FirstDate").val() == "") {
                    alert("Lütfen ödeme tarihi giriniz.");
                    $("#preLoading").hide();
                    return false;
                }
            }

            if ($("#TaxOffice").val() == "") {
                alert("Lütfen vergi dairesi giriniz");
                $("#preLoading").hide();
                return false;

            }

            if ($("#TaxNo").val() == "") {
                alert("Lütfen vergi numarası giriniz.");
                $("#preLoading").hide();
                return false;
            }
            if ($("#DiscounType").val() == "1" || $("#DiscounType").val() == "2") {
                if ($("#DiscountAmount").val() == "") {
                    alert("Lütfen İndirim Oranınız Giriniz.");
                    return false;
                }
            }
            if ($("#PacketDay").val() == "" || $("#PacketDay").val() == "0") {
                alert("Lütfen Paket Süresini 0 dan büyük giriniz");
                return false;
            }
            $.ajax({

                url: '/Store/BuyPacket',
                type: 'POST',
                data: {
                    'OrderType': orderType,
                    "Installment": intallment,
                    "Description": description,
                    "Dates": dates,
                    "MainPartyId": $("#StoreMainPartyId").val(),
                    "PayDate": $("#FirstDate").val(),
                    "TaxOffice": $("#TaxOffice").val(),
                    "TaxNo": $("#TaxNo").val(),
                    "DiscountType": $("#DiscounType").val(),
                    "DiscountAmount": $("#DiscountAmount").val(),
                    "PacketDay": $("#PacketDay").val()
                },
                dataType: 'json',
                success: function (data) {
                    $("#OrderType").val("0");
                    $("#Description").val("");
                    $("#Installment").val("0");
                    $("#creditDatesContainer").html("");
                    alert("Paket satın alma işlemi tamamlanmıştır");
                    $("#preLoading").hide();
                },
                error: function (request, error) {
                    $("#preLoading").hide();
                    alert("Request: " + JSON.stringify(request));
                }
            });

        }

        function DiscountTypeChange() {
            var val = $("#DiscounType").val();

            if (val == "0") {
                $("#DisctounTypeValueContainer").hide();
                var packetPrice = Number($("#PacketPrice").val());
                $("#PriceValue").html(packetPrice);
                $("#PriceValueWithTax").html(packetPrice * 1.18);

            }
            else if (val == "1") {
                $("#DiscounTypeDesc").html("Yüzde Oranı");
                $("#DisctounTypeValueContainer").show();
            }
            else {
                $("#DiscounTypeDesc").html("Miktar");
                $("#DisctounTypeValueContainer").show();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <%using (Html.BeginPanel())
        { %>
    <%=Html.RenderHtmlPartial("TabMenu") %>
    <div style="width: 100%">
        <table>
            <%:Html.Hidden("StoreMainPartyId",this.Page.RouteData.Values["id"]) %>
            <%:Html.HiddenFor(x=>x.PacketPrice) %>
            <tr>
                <td>Ödeme Tipi</td>
                <td>:</td>
                <td>
                    <select name="OrderType" id="OrderType">
                        <option value="0">Seçiniz</option>
                        <option value="<%:(byte)Ordertype.KrediKarti %>">Kredi Kartı</option>
                        <option value="<%:(byte)Ordertype.Havale %>">Havale</option>
                        <option value="<%:(byte)Ordertype.KrediKartiVade %>">Vadeli Kredi Kartı</option>
                        <option value="<%:(byte)Ordertype.HavaleTaksit %>">Taksitli Havale</option>
                    </select>
                </td>
            </tr>

            <tr>
                <td>Açıklama</td>
                <td>:</td>
                <td>
                    <textarea name="Description" id="Description" rows="5" cols="5"></textarea></td>
            </tr>
            <tr>
                <td>Vergi Dairesi</td>
                <td>:</td>
                <td>
                    <%:Html.TextBoxFor(x=>x.TaxOffice) %>
                </td>
            </tr>
            <tr>
                <td>Vergi No</td>
                <td>:</td>
                <td>
                    <%:Html.TextBoxFor(x=>x.TaxNo) %>
                </td>
            </tr>
            <tr id="FirstDateContainer">
                <td>Ödeme Tarihi</td>
                <td>:</td>
                <td>
                    <input type="text" name="FirstDate" id="FirstDate" autocomplete="off" class="date" /></td>
            </tr>
            <tr id="InstallmentContainer" style="display: none;">
                <td>Taksit</td>
                <td>:</td>
                <td>
                    <select id="Installment">
                        <option value="0">Seçiniz</option>
                        <%for (int i = 1; i <= 12; i++)
                            {%>
                        <option value="<%:i %>"><%:i %> Ay</option>
                        <%} %>
                    </select>
                </td>
            </tr>
            <tbody id="creditDatesContainer">
            </tbody>
            <tr>
                <td>İndirim Tipi</td>
                <td>:</td>
                <td>
                    <select name="DiscounType" id="DiscounType" onchange="DiscountTypeChange()">
                        <option value="0">İndirim Yok</option>
                        <option value="1">Yüzde İndirimi</option>
                        <option value="2">Miktar İndirimi</option>
                    </select>

                </td>
            </tr>
            <tr id="DisctounTypeValueContainer" style="display: none;">
                <td id="DiscounTypeDesc">Oran</td>
                <td>:</td>
                <td>
                    <input name="DiscountAmount" id="DiscountAmount" type="text" value="0" /></td>
            </tr>
            <tr>
                <td>Paket Süresi</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.PacketDay) %></td>
            </tr>
            <tr>
                <td>Fiyat</td>
                <td>:</td>
                <td id="PriceValue"><%:Model.PacketPrice %> </td>
            </tr>
            <tr>
                <td>Kdv Dahil Fiyat</td>
                <td>:</td>
                <td id="PriceValueWithTax">
                    <%:(Model.PacketPrice*1.18) %>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <button type="button" onclick="BuyPacket()">Satın Al</button></td>
            </tr>
        </table>
    </div>
    <%} %>
</asp:Content>
