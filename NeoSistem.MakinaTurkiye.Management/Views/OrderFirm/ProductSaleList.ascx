﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<ProductSale>>" %>


<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row">
  <td class="CellBegin" style="height: 30px;">
    <%: item.OrderProductNo %>
  </td>
  <td class="Cell">
    Firma İsmi
  </td>
  <td class="Cell">
    Ürün İsmi
  </td>
  <td class="Cell">
   Alıcı İsmi
  </td>
  <td class="Cell" align="center">
    Ürün Adedi
  </td>
  <td class="Cell">
    Ödeme Türü
  </td>
  <td class="Cell">
    Adres
  </td>
  <td class="Cell">
    Fiyat
  </td>
  <td class="Cell">
   Alım Tarihi
  </td>
  <td class="Cell">
    Teslim Tarihi
  </td>
  kabul
  <td class="Cell">
   
   kabul
  </td>
  <td class="CellEnd" style="text-align: center;">
   kabul
  </td>
  
  <td class="Cell">
 test
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="13" align="right" style="border-color: #DDD;
    border-top: none;">
    <div style="float: right;" class="pagination">
      <ul>
       
        <li>
          
          <span class="currentpage">
            1</span>&nbsp;
         
        </li>
        <li>Gösterim: </li>
        <li>
          <select id="PageDimension" name="PageDimension">
            <option value="20">
              20</option>
          </select>
        </li>
      </ul>
    </div>
  </td>
</tr>
<tr>
  <td class="ui-state ui-state-hover" colspan="13" align="right" style="border-color: #DDD;
    border-top: none;">
    Toplam Kayıt : &nbsp;&nbsp;<strong>
      0</strong>
  </td>
</tr>
