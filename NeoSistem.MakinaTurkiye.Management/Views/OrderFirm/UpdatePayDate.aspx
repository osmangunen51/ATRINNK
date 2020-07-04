<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.UpdatePayDateModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#WillPayDate').datepicker().val();
    });
    function DeleteOrderDesc(orderId) {

        if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
            $.ajax({
                url: '/OrderFirm/DeleteOrderDesc',
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
</script>
<style type="text/css">
    #superbox-innerbox { height: 700px !important; }
</style>
<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <h3>Yeni Tarih Ve Açıklama</h3>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.OrderId) %>
        <table>
            <tr>
                <td>Tarih</td>
                <td>:</td>
                <td><%:Html.TextBox("WillPayDate",DateTime.Now.ToString("dd/MM/yyyy"),new {@style="height:25" }) %></td>
                <td><%:Html.ValidationMessageFor(x=>x.WillPayDate) %></td>

            </tr>
            <tr>
                <td>Açıklama</td>
                <td>:</td>
                <td><%:Html.TextAreaFor(x=>x.Description) %></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <input type="submit" value="Ekle" />
                </td>
            </tr>
        </table>
        <% } %>
    </div>
    <div class="data" style="margin-top: 10px; height: 320px; overflow: scroll;">
        <%if (Model.UpdatePayDateModels.Count > 0)
            {%>
        <h3>Daha Öncekiler</h3>

        <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
            <thead>
                <td class="Header HeaderBegin">#</td>
                <td class="Header">Açıklama</td>
                <td class="Header">H.Tarih</td>
                <td class="Header">E.Tarih</td>
                <td class="Header HeaderEnd">Araçlar</td>
            </thead>
            <%foreach (var item in Model.UpdatePayDateModels)
                {%>
            <tr id="row<%:item.UpdatePayDateId %>">
                <td class="Cell CellBegin">
                    <%:item.UpdatePayDateId %>
                </td>
                <td class="Cell">
                    <%:item.Description %>
                </td>
                <td class="Cell">
                    <%:string.Format("{0:dd/MM/yyyy}",item.WillPayDate)%>
                </td>
                <td class="Cell">
                    <%if (item.RecordDate.HasValue)
                        {%>
                    <%:string.Format("{0:dd/MM/yyyy}",item.RecordDate.Value)%>
                    <% } %>
                </td>
                <td class="Cell CellEnd">
                    <a title="Sil" style="cursor: pointer;" onclick="DeleteOrderDesc(<%:item.UpdatePayDateId %>)">
                        <img src="/Content/images/delete.png" style="width: 16px;" />
                    </a>
                </td>
            </tr>
            <%} %>
        </table>
        <% } %>
        <%if (Model.OrderInstallmentItems.Count > 0)
            {%>
        <h3>Taksit Tarihleri</h3>
        <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header">Tutar</td>
                    <td class="Header">Durum</td>
                    <td class="Header">Ödeme Tarihi</td>
                </tr>
                <%foreach (var item in Model.OrderInstallmentItems)
                    {%>
                <tr>
                    <td class="Cell CellBegin"><%:item.Id %></td>
                    <td class="Cell"><%:item.Amount.ToString("C2") %></td>
                    <td class="Cell">
                        <%:item.IsPaid==true ?"Ödendi":"Ödenmedi" %>
                    </td>
                    <td class="Cell CellEnd"><%:item.PayDate.ToString("dd/MMM/yyyy") %></td>
                </tr>
                <%} %>
            </thead>
        </table>

        <% } %>
    </div>
</div>


