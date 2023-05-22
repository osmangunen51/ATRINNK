<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.HelpModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Yardım Alt Kategorileri
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>
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
    <div style="float: left; width: 30%; border-bottom: dashed 0px #bababa; height: 33px;">
      <span style="font-size: 13px; font-weight: bold;"><h3>Yardım  Kategorileri</h3></span>
    </div>
  </div>
   
  <br /> <br /> <br />

  <div style="width:100%;min-width:500px;margin-left:10px;">
  <ul style="list-style-type: none;overflow: auto;">

  <% IOrderedEnumerable<HelpCategory> hc = Model.kat.OrderBy(x => x.Order); %>
    <%foreach (var i in hc)
      {%>
       
            <li style=" padding-right:30px;border:0px solid black;min-height:100px">
          <h3 style="text-decoration: underline;"><h4>Ana Kategori : <%:i.KategoriAd %></h4></h3>
           <button type="button" style="width: 150px;  float:right; height: 40px; margin-right:50px;margin-bottom:15px;" onclick="window.location='/Help/CreateSub'">
          Yeni Yardım Alt Kategorisi Girişi
        </button>
          <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
     
        <tr>
        
          <td class="Header" unselectable="on">
            ID
          </td>
          <td class="Header" unselectable="on">
            Alt Kategori Adı
          </td>
          <td class="Header" unselectable="on">
            Alt Kategori Sırası
          </td>
          <td class="Header" unselectable="on">
           Sıralama Değiştir
          </td>
          <td class="Header" unselectable="on">
            Araçlar
          </td>
          
          
        
        </tr>
      </thead>
      <tbody id="Tbody2">
       <% int row = 0; %>
          
    
    
    <% foreach (var item in Model.altkat)
       {if(item.YKID==i.YKID)
         {
             
           %>
       <% row++; %>
<tr id="row<%: item.YKAID%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <%: item.YKAID  %>
  </td>
  <td class="Cell">
    <%: item.SubCategoryName %>
  </td>
  <td class="Cell">
    <%: item.Order %>
    </td>
  <td class="Cell">
    
        <a href='<%: Url.Action("OrderUpSub","Help",new { YKAID=item.YKAID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/upar.png") %>' />
        </a>
         <a href='<%: Url.Action("OrderDownSub","Help",new { YKAID=item.YKAID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/downar.png") %>' />
        </a>
     
  </td>
 
  <td class="CellEnd" style="width=20px;">
   <div style="float: left; width: 35px; height: 18px;">
       
      
        <a href='<%: Url.Action("DeleteSub","Help",new { YKAID=item.YKAID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/delete.png") %>' />
        </a>
         <a href='<%: Url.Action("EditSub","Help",new { YKAID=item.YKAID }) %>'>
    <img src='<%: Url.Content("~/Content/Images/edit.png") %>' />
        </a>
      </div>
  </td>
</tr>
<% } %>
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
      
    </table>
       
    </li>
  <br />
  <br />
  
    <% } %>
    </ul>
  

    
    
    

   
   
   
    
    
    </div>
    <%--<div style="width:100%;min-width:500px;margin-left:30px;margin-top: 30px;">
    <%Html.RenderPartial("BrowseSub2"); %>
    </div>--%>
 
</asp:Content>
