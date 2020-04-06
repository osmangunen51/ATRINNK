<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.PaymentModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script type="text/javascript">
    function PayUrlCreate() {
        var price = $("#PriceForPay").val();
        var newUrl = "<%:Model.PayUrl%>&returnUrl=/MemberShipSales/PayWithCreditCard?priceAmount=" + price + "&OrderId=" +<%:Model.OrderId%>;
        $("#PayUrl").val(newUrl);
        $("#payDisplay").show();
    }
    function Delete(orderId) {

        if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
            $.ajax({
                url: '/OrderFirm/DeletePayment',
                data: { id: orderId },
                type: 'post',
                dataType: 'json',
                success: function (data) {

                    if (data) {
                        $('#row' + orderId).hide();
                    }
                }
            });
        }
    }
    function AddReturn(orderId) {
        var returnAmount = $("#returnAmount").val();
        if (returnAmount == "") {
            alert("Lütfen iade edilecek tutar giriniz.");
        }
        else {
            var today = new Date();
            $.ajax({
                url: '/OrderFirm/ReturnAmountAdd',
                data: {
                    orderId: orderId,
                    amount: returnAmount
                },
                type: 'post',

                success: function (data) {
                    if (data) {
                        $("#data").append("<td></td> <td>" + returnAmount + "</td> <td></td>");
                    }
                }
            });
        }

    }
    function DeleteReturnInvoice(id) {
              if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
            $.ajax({
                url: '/OrderFirm/DeleteReturnInvoice',
                data: { invoiceId: id },
                type: 'post',
                dataType: 'json',
                success: function (data) {

                    if (data) {
                        $('#rowi' + id).hide();
                    }
                }
            });
        }
    }
</script>
<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <h3>Ödeme Kaydı Ekle</h3>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.OrderId) %>
        <table>
            <tr>
                <td>Yeni Ödenen Miktar</td>
                <td>:</td>
                <td>
                    <input name="PaidAmount" type="text" style="height: 30px;" /></td>
           
                <td>
                    <input type="submit" value="Ekle" /></td>
            </tr>
        </table>
        <% } %>
    </div>
    <div class="data" style="margin-top: 10px; overflow: scroll;">
        <%if (Model.PaymentItems.Count > 0)
            {%>
        <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">Ödenen</td>
                    <td class="Header">Kalan</td>
                    <td class="Header">Ödeme Tip</td>
                    <td class="Header">Tarih</td>
                    <td class="Header HeaderEnd">Araçlar</td>
                </tr>
            </thead>
            <%foreach (var item in Model.PaymentItems)
                {%>
            <tr>
                <td class="Cell CellBegin">
                    <%:item.PaidAmount.ToString("C2") %>
                </td>
                <td class="Cell">
                    <%:item.RestAmount.ToString("C2") %>
                </td>
                <td class="Cell">
                    <%if (item.PaymentType == (byte)Ordertype.Havale)
                        {%>
                            Havale
                        <% }
                            else
                            { %>
                        Kredi Kartı
                        <%} %>
                </td>
                <td class="Cell">
                    <%:string.Format("{0:dd/MM/yyyy hh:mm}",item.RecordDate)%>
                </td>
                <td class="Cell CellEnd"><a href="/OrderFirm/DeletePayment?paymentId=<%:item.PaymentId %>" style="cursor: pointer;">Sil</a></td>
            </tr>
            <% } %>
        </table>
        <% } %>
        <span style="margin-top: 15px;"><b>Ödenen Toplam Miktar</b>:<%:Model.TotalPaidAmount.ToString("C2") %></span>

        <div style="margin-top: 20px;" class="newValue">
            <h3>Ödeme Linki Oluştur</h3>
            <table>
                <tr>
                    <td>Fiyat</td>
                    <td>:</td>
                    <td>
                        <input name="PriceForPay" id="PriceForPay" type="text" style="height: 30px;" /></td>
                    <td>
                        <input type="submit" value="Olustur" onclick="PayUrlCreate();" /></td>
                </tr>
                <tr id="payDisplay" style="display: none;">
                    <td>Ödeme Linki
                    </td>
                    <td>:</td>
                    <td colspan="2">
                        <input type="text" id="PayUrl" style="height: 30px; width: 100%;" />
                    </td>

                </tr>
            </table>

        </div>
        <div style="margin-top: 20px;">
            <h2>İade Ekle</h2>
            <table >
                <tr>
                    <td>İade Edilen Tutar</td>
                    <td>:</td>
                    <td>
                        <input name="ReturnAmount" id="returnAmount" type="text" style="height: 30px;" /></td>
                    <td>
                        <input type="submit" value="Ekle" onclick="AddReturn(<%:Model.OrderId%>)" /></td>
                </tr>
            </table>

            <h4>İade Edilenler</h4>
            <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
                <thead>
                    <tr>
                        <td class="Header HeaderBegin">#</td>
                        <td class="Header">Tutar</td>
                        <td class="Header">Tarih</td>
                        <td class="Header HeaderEnd"></td>
                    </tr>
                </thead>
                <tbody id="data">
                    <%foreach (var item in Model.ReturnInvices)
                        {%>
                    <tr id="rowi<%:item.Id %>">
                        <td class="Cell CellBegin"><%:item.Id %></td>
                        <td class="Cell"><%:item.Amount.ToString("N2") %></td>
                        <td class="Cell"><%:item.RecordDate %></td>
                        <td class="Cell CellEnd">
                            <a onclick="DeleteReturnInvoice(<%:item.Id %>)">Geri Al</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
</div>


