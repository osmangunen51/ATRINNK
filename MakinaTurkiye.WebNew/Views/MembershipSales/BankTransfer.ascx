﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PacketModel>" %>
<h5>
    <span class="glyphicon glyphicon-align-left"></span>&nbsp;Banka Hesabı Seçiniz
</h5>
<table class="table table-striped">
    <thead>
        <tr>
            <th>
                Hesap Adı
            </th>
            <th>
                Banka Adı
            </th>
            <th>
                Şube Adı / Kodu
            </th>
            <th>
                Hesap No
            </th>
            <th>
                Iban No
            </th>
            <th>
                Seçim
            </th>
        </tr>
    </thead>
    <tbody>
    <% foreach (var item in Model.AccountList)
       { %>
        <tr>
            <td>
                <%:item.AccountName %>
            </td>
            <td>
                <%:item.BankName %>
            </td>
            <td>
               <%:item.BranchName %>
          (<%:item.BranchCode.Replace(" ", "") %>)
            </td>
            <td>
                <%:item.AccountNo %>
            </td>
            <td>
                <%:item.IbanNo%>
            </td>
            <td>
                <input type="radio" name="Account" onclick="$('#AccountId').val('<%:item.AccountId %>')" />
            </td>
        </tr>
        <% } %>
    </tbody>
</table>

