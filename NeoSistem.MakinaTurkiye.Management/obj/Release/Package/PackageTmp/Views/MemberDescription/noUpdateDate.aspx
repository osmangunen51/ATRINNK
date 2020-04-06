﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.BaseMemberDescriptionModel>>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
	Index
    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.Pagination"%>
<%@ import Namespace="MvcContrib.UI.Pager" %>
    
    <h2 style="margin-left:20px;">
        
        Diğer İşlemler</h2>
    <div style="margin-left:20px; float:left;">
         <a href="<%:Url.Action("index") %>">Açıklamalar</a>
    </div>
    <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
    <thead>
        <tr>
            <td class="Header" style="width:13%" unselectable="on">
            Üye
          </td>
           <td class="Header" style="width:15%" unselectable="on">
            Firma
          </td>
          <td class="Header" style="width:15%" unselectable="on">
            Açıklama Başlığı
          </td>
          <td class="Header" style="width:30%" unselectable="on">
            İçerik
          </td>
          <td class="Header" style="width:10%" unselectable="on">
            <a href="/MemberDescription/index?page=<%:ViewData["Curr"] %>&order=InputDate&type=<%:ViewData["type"] %>" title="Giriş Tarihine Göre Sondan Sırala">İşlem Tarih</a>
          </td>
         
            <td class="Header" style="width:8%" unselectable="on">
           Araçlar
          </td>
         
        </tr>
        </thead>
    <tbody id="Tbody2">
       <% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++;
   string chose = ""; %>
<%  
  if (item.DescriptionDegree == 1)
  {
    chose = "#8ee78e";
  }
  else if (item.DescriptionDegree == 2)
  {
    chose = "#8ae5db";
  }
  else if (item.DescriptionDegree == 3)
  {
    chose = "#e5c08a";
  }
  else if (item.DescriptionDegree == 4)
  {
    chose = "#eff16f";
  }
  else if (item.DescriptionDegree == 5)
  {
    chose = "#f38496";
  }
  %>
<tr id="row<%: item.ID%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>" style=" background-color:<%:chose%>">
  
  <td class="CellBegin">
    <%: item.Member.MemberName+" "+item.Member.MemberSurname%>
  </td>
  <td class="Cell">
      <%if(item.Member.MemberType==20)
        { 
           %>
        <%:item.StoreName %>
      <%} %>
  </td>
  <td class="Cell">
    <%: item.Title %>
  </td>
  <td class="Cell">
    <%: Html.Truncate(item.Description,100) %>
  </td>

  <td class="Cell">
    <%: String.Format("{0:g}", item.InputDate) %>
  </td>
 
  <td class="CellEnd">
  <div style="float: left; width: 75px; height: 18px;">
       
        <a href='<%: Url.Action("BrowseDesc1","Member",new { id=item.Member.MainPartyId }) %>'>
    <img src='<%: Url.Content("~/Content/Images/product.png") %>' />
        </a>
        <a href="/MemberDescription/Edit?descId=<%:item.ID %>&check=1&getPage=otherDesc&page=<%:ViewData["Curr"] %>">
       1
       </a>
       <a href="/MemberDescription/Edit?descId=<%:item.ID %>&check=2&getPage=otherDesc&page=<%:ViewData["Curr"] %>">
       2
       </a>
       <a href="/MemberDescription/Edit?descId=<%:item.ID %>&check=3&getPage=otherDesc&page=<%:ViewData["Curr"] %>">
       3
       </a>
       <a href="/MemberDescription/Edit?descId=<%:item.ID %>&check=4&page=otherDesc">
       4
       </a>
       <a href="/MemberDescription/Edit?descId=<%:item.ID %>&check=5">
       5
       </a>
      </div>
   
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <%--<%= Html.Pager((IPagination)Model) %>--%>
     <% int sayfa=(int)ViewData["Sayfa"]; 
        int curr=(int)ViewData["Curr"];%>
     <ul>
     <%for (int i = 1; i <= sayfa; i++)
       {%>
           <li>
           <%if (i==curr)
             {%>
                 <a href='<%: Url.Action("noUpdateDate","MemberDescription",new { page=i  }) %>'><span class="currentpage">
            <%: i %></span></a>
           
            <% }else{ %>
            <a href='<%: Url.Action("noUpdateDate","MemberDescription",new { page=i }) %>'><span >
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
          <td class="ui-state ui-state-hover" colspan="7" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: (int)ViewData["Tot"]%></strong>
          </td>
        </tr>
      </tfoot>
    </table>
   
    </div>
  

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


