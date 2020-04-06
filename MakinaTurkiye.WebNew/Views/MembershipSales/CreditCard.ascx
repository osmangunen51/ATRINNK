﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PacketModel>" %>
<h5>
    <span class="glyphicon glyphicon-align-left"></span>Ödeme Seçenekleri
</h5>
<div class="btn-group">
    <a href="#tekcekim" data-toggle="tab" class="btn btn-md btn-mt2 active">Tek Çekim
    </a>
    <%byte cardCount = 0; %>
    <% foreach (var item in Model.CreditCardItems)
       { %>
    <%cardCount++; %>
    <a href="#<%:item.CreditCardName %>" data-toggle="tab" class="btn btn-md btn-mt2 taksit">Taksitli
        (<%:item.CreditCardName %>) </a>
    <% } %>
</div>
<div class="tab-content">
    <div class="tab-pane active" id="tekcekim">
    </div>
    <%cardCount = 0; %>
    <% foreach (var item in Model.CreditCardItems)
       { %>
    <%cardCount++; %>
    <div class="tab-pane" id='<%:item.CreditCardName %>'>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>
                        Taksit
                    </th>
                    <th>
                        Taksit Tutarı
                    </th>
                    <th>
                        Toplam Tutar
                    </th>
                    <th>
                        Seçim
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var itemParent in Model.CreditCardInstallmentItems.Where(c => c.CreditCardId == item.CreditCardId))
                   { %>
                <tr id="tr<%:itemParent.CreditCardInstallmentId%>">
                    <td>
                        <%:itemParent.CreditCardCount %>
                    </td>
                    <td>
                        <% decimal price = Model.MaturityCalculation(Model.OrderPrice, 0); %>
                        <%: ((price + (price * itemParent.CreditCardValue / 100)) / itemParent.CreditCardCount).ToString("C2")%>
                    </td>
                    <td>
                        <%: (price + (price * itemParent.CreditCardValue / 100)).ToString("C2")%>
                    </td>
                    <td>
                        <input name="aa" type="radio" onclick="$('#CreditCardInstallmentId').val(<%:itemParent.CreditCardInstallmentId%>);$('#CreditCardId').val(<%:item.CreditCardId %>);">
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
    <% } %>
</div>
