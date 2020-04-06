﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.Entities.HelpCategory>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Yardım Kategorileri
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
  <style type="text/css">
    .row
    {
      width: 460px;
      float: left;
      margin-left: 15px;
    }
    .row:hover
    {
      background-color: #efefef;
    }
  </style>
  <script type="text/javascript">
      function check(id) {
          if (confirm('Kategoriyi silmek istediğinizden emin misiniz ?')) {
              $('#YKID').val(id);
              return true;
          }
          return false;
      }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div style="float: left; width: 90%; margin: 20px 0px 0px 25px">
    <div style="float: left; width: 10%; border-bottom: dashed 0px #bababa; height: 20px;">
      <span style="font-size: 13px; font-weight: bold;"><h3>Yardım Kategorileri</h3></span>
    </div>
  </div>
  <button type="button" style="width: 150px;  float:right; height: 40px; margin-right:25px;margin-top:-20px;" onclick="window.location='/Help/Create'">
          Yeni Yardım Kategorisi Girişi
        </button>
        <br />
        <br />
        <br />
        <br />
        <div style="width: 100%; margin: 0 auto;margin-top:10px;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
        
          <td class="Header" unselectable="on">
            ID
          </td>
          <td class="Header" unselectable="on">
            Kategori Adı
          </td>
          <td class="Header" unselectable="on">
            Kategori Sırası
          </td>
          <td class="Header" unselectable="on">
            Sıralama Değiştir
          </td>
          <td class="Header" unselectable="on">
            Araçlar
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
<tr id="row<%: item.YKID%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <%: item.YKID  %>
  </td>
  <td class="Cell">
    <%: item.KategoriAd %>
  </td>
  <td class="Cell">
    <%:item.Order  %>
  </td>
  <td class="Cell">
      <a href='<%: Url.Action("OrderUp","Help",new { YKID=item.YKID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/upar.png") %>' />
        </a>
         <a href='<%: Url.Action("OrderDown","Help",new { YKID=item.YKID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/downar.png") %>' />
        </a>
  </td>
 
  <td class="CellEnd" style="width=20px;">
   <div style="float: left; width: 35px; height: 18px;">
       
      
        <a href='<%: Url.Action("Delete","Help",new { YKID=item.YKID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/delete.png") %>' />
        </a>
         <a href='<%: Url.Action("Edit","Help",new { YKID=item.YKID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/edit.png") %>' />
        </a>
      </div>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>
      </tbody>
      <%--<tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="7" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.Count() %></strong>
          </td>
        </tr>
      </tfoot>--%>
    </table>
  </div>

  
</asp:Content>
