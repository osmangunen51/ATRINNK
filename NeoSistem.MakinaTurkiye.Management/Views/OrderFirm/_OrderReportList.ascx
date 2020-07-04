﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.Orders.OrderReportModel>" %>

<% int row = 0; %>
<% foreach (var item in Model.OrderReportItems.Source.ToList())
    { %>
<% row++;
    string invoiceNumberTextValue = "";
%>
<tr id="row<%: item.OrderId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="Cell" style="height: 30px;">
        <%: item.orderNo %>     
    </td>
    <td class="Cell">
        <a href="/Store/EditStore/<%:item.MainPartyId %>">
                  <%:item.StoreName %>
        </a>
  
    </td>
    <td class="Cell">
        <% if (item.PacketStatu.HasValue)
            {
                var acType = (PacketStatu)item.PacketStatu;
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
       <%} %> 

        <%:item.OrderCancelled %>
 
    </td>
    <td class="Cell" align="center">
        <%:item.OrderType %>
    </td>




    <td class="Cell">
        <%:item.Description %>
    </td>
            <td class="Cell">
        <%=item.UserName %>
    </td>
    <td class="Cell">
        <%:item.InvoiveStatus %>
    </td>
        <td class="Cell">
        <%: item.RecordDate.ToShortDateString()%>
    </td>
        <td class="Cell">
        <%:item.OrderPrice.HasValue ? item.OrderPrice.Value.ToString("C2"):"" %>
    </td>
    <td class="Cell">
        <%:item.PaidAmount.HasValue ? item.PaidAmount.Value.ToString("C2") : "0" %>
    </td>
    <td class="Cell">
        <%:item.RestAmount.HasValue ? item.RestAmount.Value.ToString("C2") : "" %>
    </td>
    <td class="Cell">
        <%if(item.PayDate.HasValue) %>
          <%:item.PayDate.Value.ToShortDateString() %>
    </td>
</tr>
<%} %>
<tr>
    <td class="ui-state ui-state-default" colspan="17" align="right" style="border-color: #DDD; border-top: none;">
        <div style="float: right;" class="pagination">
            <ul>
                <% foreach (int page in Model.OrderReportItems.TotalLinkPages)
                    { %>
                <li>
                    <% if (page == Model.OrderReportItems.CurrentPage)
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

            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-hover" colspan="17" align="right" style="border-color: #DDD; border-top: none;">
     Toplam Fiyat :<b><%:Model.TotalPrice.ToString("C2") %></b>
      Toplam Alınan :<b><%:Model.TotalPaid.ToString("C2") %></b>
        Toplam Kalan :<b><%:Model.TotalRestPrice.ToString("C2") %></b>
        <%--    <input type="button" value="Exele Aktar" id="ExcelButon" onclick="ExportExcel();" />--%>
        Toplam Kayıt : &nbsp;&nbsp;<strong>
            <%:Model.OrderReportItems.TotalRecord%></strong>
    </td>
</tr>


