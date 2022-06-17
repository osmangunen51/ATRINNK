<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<OrderModel>>" %>

<% int row = 0; %>
<%MakinaTurkiyeEntities olustur = new MakinaTurkiyeEntities(); %>
<% foreach (var item in Model.Source)
    { %>
<% row++;
    string invoiceNumberTextValue = "";
    string backgroundColor = "transparent";
    if (item.CurrentStorePacketId == (byte)Packets.Standart)
    {
        backgroundColor = "#fbe2e2";
    }
    else if (item.IsRenewPacket == true)
    {
        backgroundColor = "#f0f5a8";
    }
    else
    {
        backgroundColor = "#dcf3da";
    }

%>

<tr id="row<%: item.OrderId %>" style="background-color: <%=backgroundColor%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="Cell" style="height: 30px;">
        <%:item.OrderNo %>&nbsp;/<br />
        <%if (!string.IsNullOrEmpty(item.EBillNumber))
            {%>
            <%:item.EBillNumber %>
            <% }
            else
            {%>
        <div class="panel2" style="float: left;">
            <a style="margin-left: 3px; float: left;" id="lightbox_click" rel="superbox[iframe]" href="/OrderFirm/BillNumber/<%:item.OrderId %>" target="_blank" title="Faturayı Numarası Tanımla"><span style="padding: 2px; background-color: #bb8c36; color: #fff; font-weight: 600;">F-No</span></a>
        </div>
        <% } %>


    </td>
    <td class="Cell">
        <%if (item.StoreNameForInvoice != "" && item.StoreNameForInvoice != null)
            {
                item.StoreName = item.StoreNameForInvoice;
            } %>
        <span id="storeDisplay<%:item.OrderId %>">
            <a target="_blank" href="/Store/EditStore/<%:item.MainPartyId %>"><%: item.StoreName %></a>
        </span>
        <div style="float: right; cursor: pointer;" id="StoreChangePencil<%:item.OrderId%>" onclick="StoreChangeClick(<%:item.OrderId %>)">
            <img src="/Content/images/edit.png" />
        </div>

        <span style="display: none;" id="storeChange<%:item.OrderId %>">
            <%:Html.TextBox("storeName", item.StoreName, new { @id = "storeNameChange" + item.OrderId, @style = "display:none;" })%>
            <button type="button" onclick="StoreNameUpdate(<%:item.OrderId %>)">Düzenle</button>
        </span>
    </td>

    <td class="Cell">
        <%: item.PacketName %>
    </td>
    <td class="Cell">
        <% var acType = (PacketStatu)item.PacketStatu;
            string text = "";
            switch (acType)
            {
                case PacketStatu.Onaylandi:
                    text = "Onaylandı";
                    break;
                case PacketStatu.Onaylanmadi:
                    text = "Onaylanmamış";
                    break;
                case PacketStatu.Inceleniyor:
                    text = "Onay Bekliyor";
                    break;
                case PacketStatu.Silindi:
                    text = "Silindi";
                    break;
                default:
                    break;
            }
        %>
        <%=text %>
        <%if (item.OrderCancelled == true)
            {%>
        <%:Html.Raw("(İptal Edilmiş)") %>
        <% } %>
    </td>
    <td class="Cell" align="center">
        <% if (item.OrderType == (byte)Ordertype.Havale)
            {%>
    Havale
    <% }
        else if (item.OrderType == (byte)Ordertype.HavaleTaksit)
        {%>
          Havale Taksit
        <%}
            else if (item.OrderType == (byte)Ordertype.KrediKartiTaksit)
            {%>
                Kredi Kartı Taksit
            <%}
                else if (item.OrderType == (byte)Ordertype.KrediKarti)
                { %>
        <%:item.CreditCardName %>
        <br />
        <% if (item.CreditCardCount > 0)
            { %>
    (<%:item.CreditCardCount%>
    Taksit)
    <% }
        else
        { %>
    (Tek Çekim)
        <%if (item.PriceCheck.HasValue &&  item.PriceCheck == false) {%>
            (Ödeme Başarısız)
        <% } %>
    <% } %>
        <% } %>
    </td>
    <td class="Cell">
        <span id="addressDisplay<%:item.OrderId %>">
            <%: item.Address %>
        </span>
        <div style="float: right; cursor: pointer;" id="AddressChangePencil<%:item.OrderId%>" onclick="AddressChangeClick(<%:item.OrderId %>)">
            <img src="/Content/images/edit.png" />
        </div>

        <span style="display: none;" id="addressChange<%:item.OrderId %>">
            <%:Html.TextArea("address", item.Address, new { @id = "addressNameChange" + item.OrderId, @style = "display:none;" })%>
            <button type="button" onclick="AddressUpdate(<%:item.OrderId %>)">Düzenle</button>
        </span>
    </td>

    <%if (item.TaxOffice != "" && item.TaxOffice != null)
        { %>
    <td class="Cell">
        <%: item.TaxOffice%><br />
        <%: item.TaxNo%>
    </td>
    <%}
        else
        {
    %>

    <td class="Cell">
        <%: item.TaxOfficeOrder%>
        <br />
        <%: item.TaxNoOrder%>

    </td>
    <%}%>

    <td class="Cell">
        <%: item.RecordDate.ToShortDateString()%>
    </td>
    <%  DateTime deger;
        if (item.OrderPacketEndDate == null)
        {
            {
                var descs = (from c in olustur.Stores
                             where c.MainPartyId == item.MainPartyId
                             select c.StorePacketEndDate).SingleOrDefault();
                deger = DateTime.Now;
                if (descs != null)
                {
                    deger = (DateTime)descs;
                    string degerimiz = deger.ToString();
                    var order = (from o in olustur.Orders where o.OrderId == item.OrderId select o).FirstOrDefault();
                   /* if (order != null)
                    {
                                 order.OrderPacketEndDate = (DateTime)descs;
                    olustur.SaveChanges();
                    }
                   */

                }
            }
        }
        else
        {
            deger = item.OrderPacketEndDate.ToDateTime();
        }
    %>
    <td class="Cell">
        <%if (deger != null) { %>
                    <span id="orderEndDateDisplay<%:item.OrderId %>">
            <%:deger.ToShortDateString() %>
        </span>
        <div style="float: right; cursor: pointer;" id="OrderEndDateChangePencil<%:item.OrderId%>" onclick="OrderEndDateChangeClick(<%:item.OrderId %>)">
            <img src="/Content/images/edit.png" />
        </div>

        <span style="display: none;" id="orderEndDateChange<%:item.OrderId %>">
            <%:Html.TextBox("orderEndDate", deger.ToShortDateString(), new { @id = "orderEndDateDataChange" + item.OrderId, @class="date", @style = "display:none;" })%>
            <button type="button" onclick="OrderEndDateUpdate(<%:item.OrderId %>)">Düzenle</button>
        </span>
           <% } %>

    </td>

    <td class="Cell">
        <%if (item.OrderCancelled != true)
            {%>

        <span id="PriceDisplay<%:item.OrderId %>">
            <%: item.OrderPrice.ToString("F")%>
        </span>
        <div style="float: right; cursor: pointer;" id="PriceChangePencil<%:item.OrderId%>" onclick="PriceChangeClick(<%:item.OrderId %>)">
            <img src="/Content/images/edit.png" />
        </div>

        <span style="display: none;" id="PriceNameChange<%:item.OrderId %>">

            <%:Html.TextBox("price", item.OrderPrice.ToString("F"), new { @id = "PriceChangeVal" + item.OrderId, @style = "display:none;" })%>
            <button type="button" onclick="PriceUpdate(<%:item.OrderId %>)">Düzenle</button>

        </span>
        <% }
        %>
    </td>
    <td class="Cell">
        <%decimal restAmount = 0; %>
        <%if (item.ReturnAmount > 0)
            {%>
            İade Edildi
          <% }
              else
              {%>
        <%if (item.OrderCancelled != true)
            {%>
        <%if (item.RestAmount == null)
            {
                restAmount = item.OrderPrice; %>
        <%:restAmount.ToString("F") %>
        <%}
            else
            {%>

        <%
            restAmount = Convert.ToDecimal(item.RestAmount); %>
        <span id="RestAmountDisplay<%:item.OrderId %>">
            <%: restAmount.ToString("F")%>
        </span>
        <div style="float: right; cursor: pointer;" id="RestAmountChangePencil<%:item.OrderId%>" onclick="RestAmountChangeClick(<%:item.OrderId %>)">
            <img src="/Content/images/edit.png" />
        </div>

        <span style="display: none;" id="RestAmountNameChange<%:item.OrderId %>">
            <%:Html.TextBox("restAmount", restAmount.ToString("F"), new { @id = "RestAmountChangeVal" + item.OrderId, @style = "display:none;" })%>
            <button type="button" onclick="RestAmountUpdate(<%:item.OrderId %>, <%:item.RestAmount %>)">Düzenle</button>
        </span>
        <% } %>
        <% } %>
        <%} %>

    </td>
    <td class="Cell">
        <%string style = "";
            if (restAmount == 0)
            {
                style = "color:#ccc;";
            }%>
        <span style="<%: style%> float:left;"><%:(item.PayDate!=null)?Convert.ToDateTime(item.PayDate).ToString("dd/MM/yyyy"):item.RecordDate.ToString("dd/MM/yyyy")%></span>
        <div style="float:right">
        <%if (item.IsProductAdded.HasValue && item.IsProductAdded.Value == true)
                {%>
            <img title="Veri Girişi Tamamlandı" src="/Content/images/Accept-icon.png">
        <% }
                                                                               else { %>
            <img title="Veri Girişi Yapılmadı" style="width: 16px;" src="../../Content/Images/cancel.png">
        <% } %>
               <a href="/OrderFirm/UpdatePayDate?orderId=<%=item.OrderId  %>" style="float: right;" id="lightbox_click" rel="superbox[iframe]"
            title="Yeni Tarih Ekle">
            <img src="/Content/images/edit.png" /></a>
        </div>
    </td>
    <td class="Cell">
        <% if (item.PacketStatu == (byte)PacketStatu.Inceleniyor)
            { %>
        <a href="/OrderFirm/Confirm/<%: item.OrderId %>">Onayla</a><br />
        <a href="/OrderFirm/NotConfirm/<%: item.OrderId %>">Onaylama</a>
        <% } %>
    </td>
    <td class="Cell">
        <%=item.SalesUserName %> &nbsp;
                    <a href="/OrderFirm/SalesResponsibleUpdate?orderId=<%=item.OrderId  %>" style="float: left;" id="lightbox_click" rel="superbox[iframe]" title="Satış Sorumlusu Değiştir"><img src="/Content/images/edit.png"></a>

    </td>
    <% int onayli = 2;
        var faturalimi = (from d in olustur.Faturachecks
                          where d.MainPartyId == item.MainPartyId
                          select d.Onaylı).SingleOrDefault();
        if (faturalimi != null)
        {
            onayli = faturalimi.ToInt32();
        }

    %>
    <td class="Cell CellEnd">
        <div style="margin-top: 1px; float: left; margin-bottom: 2px;">
            <a style="margin-left: 3px;" href="<%:Url.Action("GetInvoice", new {orderId=item.OrderId })%>" target="_blank" style="float: left;" title="Faturayı Gör"><span style="padding: 2px; background-color: #bb8c36; color: #fff; font-weight: 600;">F</span></a>
            <a href="/OrderFirm/Payments?OrderId=<%=item.OrderId  %>" style="float: left;" id="lightbox_click" rel="superbox[iframe]" title="Ödemeleri Gör"><span style="padding: 2px; background-color: #0eb907; color: #fff; font-weight: 600;">Ö</span></a>
        </div>
        <div class="panel2" style="margin-top: 1px; float: left; margin-bottom: 2px;margin-left: 2px;">
            <a href="/Member/stororderremembermail?orderId=<%:item.OrderId %>" title="Paket ödeme hatırlatma maili gönder.">
                <span style="padding: 2px; background-color: #0eb907; color: #fff; font-weight: 600;">ÖH</span>
            </a>
        </div>
        <%if (item.InvoiceStatus != null)
            {
                if (Convert.ToBoolean(item.InvoiceStatus) == true)
                { %>
        <div id="panel1" style="float: left;">
            <img src="../../Content/Images/Goodshield.png" />
        </div>
        <%}%>
        <%if (item.InvoiceStatus != true)
            {  %>
        <div id="panel2" style="float: left;">
            <a href="/OrderFirm/UpdateInvoiceStatus?orderId=<%:item.OrderId %>&page=confirm&type=invoice">
                <img src="../../Content/Images/Errorshield.png" />
            </a>
        </div>

        <%}
            }
            else
            {
        %>
        <div id="panel2" style="float: left;">
            <a href="/OrderFirm/UpdateInvoiceStatus?orderId=<%:item.OrderId %>&page=confirm&type=invoice">
                <img src="../../Content/Images/Errorshield.png" />
            </a>
        </div>
        <%} %>
        <%if (item.OrderCancelled == true)
            {%>
        <div class="panel2" style="float: left;">
            <img style="width: 16px;" title="İptal Edilmiş Sipariş" src="../../Content/Images/cancel.png" />
        </div>
        <% }
            else
            {%>
        <div class="panel2" style="float: left;">
            <a href="/OrderFirm/DeleteOrder/<%:item.OrderId %>?type=cancel" title="Sipariş İptal Et">
                <img style="width: 16px;" src="../../Content/Images/cancel.png" />
            </a>
        </div>

        <% } %>
        <%if (item.SendedMail == false)
            {%>
        <div class="panel2" style="float: left;">
            <a href="/OrderFirm/UpdateInvoiceStatus?orderId=<%:item.OrderId %>&page=confirm&type=sendedmail" title="Mail Gönderildi Olarak İşaretle">
                <img src="../../Content/Images/mail-env.png" style="width: 16px;" />
            </a>
        </div>
        <% }
            else
            {%>
        <div class="panel2" style="float: left;">
            <img src="../../Content/Images/mail-check.png" title="Mail Gönderilmiş" style="width: 16px;" />
        </div>
        <% } %>
        <div class="panel2" style="float: left;">
            <a title="Sil" style="cursor: pointer; float: left;" href="/OrderFirm/DeleteOrder/<%:item.OrderId %>?type=delete">
                <img src="/Content/images/delete.png" style="width: 16px;" />
            </a>
            <a href="/Store/StoreContactInfo/<%=item.MainPartyId  %>" style="float: left;" id="lightbox_click" rel="superbox[iframe]" title="İletişim Bilgileri">
                <img src="/Content/RibbonImages/phone-list.png" style="width: 16px;" /></a>
        </div>
    </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
    { %>
<tr class="Row">
    <td colspan="10" class="CellBegin Cell" style="color: #FF0000; font-weight: 700; font-size: 14px;">Sipariş bulunamadı.
    </td>
</tr>
<% } %>
<tr>
    <td class="ui-state ui-state-default" colspan="17" align="right" style="border-color: #DDD; border-top: none;">
        <div style="float: right;" class="pagination">
            <ul>
                <% foreach (int page in Model.TotalLinkPages)
                    { %>
                <li>
                    <% if (page == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: page %></span>&nbsp;
          <% } %>
                    <% else
                        { %>
                    <a onclick="PagePost(<%: page %>)">
                        <%: page %></a>&nbsp;
          <% } %>
                    <% } %>
                </li>
                <li>Gösterim: </li>
                <li>
                    <select id="PageDimension" name="PageDimension" onchange="SearchPost();">
                        <option value="20" <%: Session["Order_PAGEDIMENSION"].ToString() == "20" ? "selected=selected" : "" %>>20</option>
                        <option value="50" <%: Session["Order_PAGEDIMENSION"].ToString() == "50" ? "selected=selected" : "" %>>50</option>
                        <option value="100" <%: Session["Order_PAGEDIMENSION"].ToString() == "100" ? "selected=selected" : "" %>>100</option>
                    </select>
                </li>
            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-hover" colspan="17" align="right" style="border-color: #DDD; border-top: none;">
        <input type="button" value="Exele Aktar" id="ExcelButon" onclick="ExportExcel();" />
        Toplam Kayıt : &nbsp;&nbsp;<strong>
            <%: Model.TotalRecord %></strong>
    </td>
</tr>

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
