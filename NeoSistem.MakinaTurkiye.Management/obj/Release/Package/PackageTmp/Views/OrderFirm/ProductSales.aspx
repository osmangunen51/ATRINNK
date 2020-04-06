﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ProductSale>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ProductSales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="width: 100%; margin: 0 auto;">
    <input type="hidden" name="OrderName" id="OrderName" value="OrderId" onclick="OrderPost('OrderId', this);" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <table cellpadding="11" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" style="width: 7%;" unselectable="on" onclick="OrderPost('OrderNo', this);">
            Sipariş No
          </td>
          <td class="Header" style="width: 12%;" unselectable="on" onclick="OrderPost('StoreName', this);">
            Firma İsmi
          </td>
          <td class="Header" style="width: 6%;" unselectable="on" onclick="OrderPost('PacketName', this);">
           Ürün İsmi
          </td>
          <td class="Header" style="width: 7%;" unselectable="on">
            Alıcı İsmi
          </td>
          <td class="Header" style="width: 8%;" unselectable="on">
            Ürün Adedi
          </td>
          <td class="Header" style="width: 15%;" unselectable="on">
            Ödeme Türü
          </td>
          <td class="Header" style="width: 8%;" unselectable="on">
            Adres
          </td>
          <td class="Header" style="width: 8%;" unselectable="on">
            Fiyat
          </td>
          <td class="Header" style="width: 7%;" unselectable="on">
            Alım Tarihi
          </td>
          <td class="Header" style="width: 7%;" unselectable="on">
            Teslim Tarihi
          </td>
          <td class="Header" style="width: 6%;" unselectable="on">
            kabul
          </td>
          <td class="Header" style="width: 8%;">
            kabul
          </td>
          <td class="Header" style="width: 2%;">
            kabul
          </td>
        </tr>
        <tr style="background-color: #F1F1F1">
          <td class="CellBegin" align="center">
            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF;">
                    <input id="OrderNo" class="Search" style="width: 75%; border: none;" />
                    <span class="ui-icon ui-icon-close searchClear" style="width: 15%;"></span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center">
            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF;">
                    <input id="StoreName" class="Search" style="width: 85%; border: none;" /><span class="ui-icon ui-icon-close searchClear"
                      onclick="clearSearch('StoreName');" style="width:7%;"> </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center">
          </td>
          <td class="Cell">
          </td>
          <td class="Cell">
          </td>
          <td class="Cell">
          </td>
          <td class="Cell">
          </td>
          <td class="Cell">
          </td>
          <td class="Cell">
          </td>
          <td class="Cell">
            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="RecordDate" class="Search date" style="border: none; width: 75%;" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('RecordDate');"
                      style="width: 13%;"></span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td >
          <td class="Cell">
          </td>
          <td class="Cell">
          </td>
          <td class="CellEnd" style="width: 7%;">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("ProductSaleList", Model)%>
      </tbody>
      <tfoot>
      </tfoot>
    </table>
  </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
