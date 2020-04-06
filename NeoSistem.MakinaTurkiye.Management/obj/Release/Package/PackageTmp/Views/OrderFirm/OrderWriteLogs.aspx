﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<NeoSistem.MakinaTurkiye.Management.Models.Entities.OrderWriteLog>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div style="width: 100%; margin: 0 auto; overflow:scroll;">
 <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px; height:200px; overflow:scroll;">
      <thead>
          <tr>
              <td class="Header">ID</td>
              <td class="Header">Firma Adı</td>
              <td class="Header">Fiyat</td>
              <td class="Header">Tarih</td>
              <td class="Header"></td>
          </tr>
          </thead>
     <tbody>
         <%int row = 0; %>
         <%foreach (var item in Model)
           {%>
             <tr  class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
                 <td><%:item.OrderWriteLogID %></td>
                 <td><%:item.StoreName %></td>
                 <td><%:Convert.ToString(item.Price) %></td>
                 <td><%:item.RecordDate%></td>
                 <td><a href="/OrderFirm/OrderWriteLogDelete/<%:item.OrderWriteLogID %>?page1=<%:ViewData["page"] %>">Sil</a></td>
             </tr>  
              <%row++; %> 
           <%} %>
           <tr>
  <td class="ui-state ui-state-default" colspan="8" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
       <% int sayfa = (int)ViewData["pageNumbers"];
          int curr = (int)ViewData["page"];%>
     <ul>
     <%for (int i = 1; i <= sayfa; i++)
       {%>
           <li>
           <%if (i==curr)
             {%>
                 <a href='<%: Url.Action("OrderWriteLogs","OrderFirm/",new { page=i }) %>'><span class="currentpage">
            <%: i %></span></a>
           
            <% }else{ %>
            <a href='<%: Url.Action("OrderWriteLogs","OrderFirm/",new { page=i }) %>'><span >
            <%: i %></span></a>
            <% }%>
           
           </li>
       <%} %>
     </ul>
    </div>
  </td>
</tr>
  <tr>
  <td class="ui-state ui-state-hover" colspan="5" align="right" style="border-color: #DDD;
    border-top: none;">
    Toplam Kayıt : &nbsp;&nbsp;<strong>
      <%:ViewData["TotalRecod"] %></strong>
  </td>
</tr>
     </tbody>

     </table>

  </div>
</asp:Content>
