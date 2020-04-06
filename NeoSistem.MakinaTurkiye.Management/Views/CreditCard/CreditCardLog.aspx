﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.Entities.CreditCardLog>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 Kredi Kartı Logları

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.Pagination"%>
<%@ import Namespace="MvcContrib.UI.Pager" %>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
        
          <td class="Header" unselectable="on">
            Kullanıcı
          </td>
          <td class="Header" unselectable="on">
            Durum
          </td>
          <td class="Header" unselectable="on">
            Sanal Pos
          </td>
          <td class="Header" unselectable="on">
            Tarih
          </td>
          <td class="Header" unselectable="on">
            IP Adresi
          </td>
          <td class="Header" unselectable="on">
            Dönen Kod
          </td>
          <td class="Header" unselectable="on">
            Detay 
          </td>
          <td class="Header" unselectable="on">
            Sipariş Tipi 
          </td>
          
          <%--<td class="Header" style="width: 70px; height: 19px">
          </td>--%>
        </tr>
      </thead>
      <tbody id="Tbody2">
       <% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.kklogid%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <%: item.Store.StoreName  %>
  </td>
  <td class="Cell">
    <%: item.status %>
  </td>
  <td class="Cell">
    <%: item.PosName %>
  </td>
  <td class="Cell">
    <%: String.Format("{0:g}", item.Date) %>
  </td>
  <td class="Cell">
    <%: item.IP %>
  </td>
  <td class="Cell">
    <%: item.Code %>
  </td>
  <td class="Cell">
    <%: item.Detail %>
  </td>
  <td class="Cell">
    <%: item.Ordertype %>
  </td>
  <td class="CellEnd">
   <%-- <a href="/Packet/Edit/<%: item.PacketId %>" style="padding-bottom: 5px;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.PacketId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>--%>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="8" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
       <% int sayfa=(int)ViewData["Sayfa"]; 
        int curr=(int)ViewData["Curr"];%>
     <ul>
     <%for (int i = 1; i <= sayfa; i++)
       {%>
           <li>
           <%if (i==curr)
             {%>
                 <a href='<%: Url.Action("CreditCardLog","CreditCard/",new { page=i }) %>'><span class="currentpage">
            <%: i %></span></a>
           
            <% }else{ %>
            <a href='<%: Url.Action("CreditCardLog","CreditCard/",new { page=i }) %>'><span >
            <%: i %></span></a>
            <% }%>
           
           </li>
       <%} %>
     </ul>
    </div>
  </td>
</tr>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="8" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: (int)ViewData["Tot"]%></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>


