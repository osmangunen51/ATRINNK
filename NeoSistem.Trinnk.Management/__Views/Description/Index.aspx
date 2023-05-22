﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NeoSistem.Trinnk.Management.Models.Entities.MemberDescription>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.Pagination"%>
<%@ import Namespace="MvcContrib.UI.Pager" %>

    <h2>Açıklamalar</h2>
    <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
    <thead>
        <tr>
            <td class="Header" unselectable="on">
            Üye
          </td>
           <td class="Header" unselectable="on">
            Firma
          </td>
          <td class="Header" unselectable="on">
            Açıklama Başlığı
          </td>
          <td class="Header" unselectable="on">
            İçerik
          </td>
          <td class="Header" unselectable="on">
            Tarih
          </td>
           <td class="Header" unselectable="on">
            Güncelle
          </td>
          <td class="Header" unselectable="on">
            Düzenleme Tarihi
          </td>
            <td class="Header" unselectable="on">
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
<tr id="row<%: item.descId%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>" style=" background-color:<%:chose%>">
  
  <td class="CellBegin">
    <%: item.Member.MemberName+" "+item.Member.MemberSurname%>
  </td>
  <td class="Cell">
   <%if(item.Member.MemberType==20){ %>
   <%TrinnkEntities entities = new TrinnkEntities();
     string name = "";
     var storememberid = entities.MemberStores.Where(c => c.MemberMainPartyId == item.MainPartyId).First().StoreMainPartyId;
     if (storememberid != null)
     {
       var store = entities.Stores.Where(c => c.MainPartyId == storememberid).SingleOrDefault();
       if (store != null)
       {
         name = store.StoreName;
         
            %>
            <a href="/Store/EditStore/<%:store.MainPartyId %>" target="_blank"><%=name%></a>
         
         <%
       }
     }
   %>
      
      <%if (storememberid == null)
        {  %>
      <%=name%>
      <%} %>
   <%} %>
  </td>
  <td class="Cell">
    <%: item.Title %>
  </td>
  <td class="Cell">
    <%: Html.Truncate(item.Description,100) %>
  </td>

  <td class="Cell">
    <%: String.Format("{0:g}", item.Date) %>
  </td>
    <td class="Cell">
    <%:Ajax.ActionLink("Güncelle", "UpdateDate", new { id = item.descId }, new AjaxOptions() { UpdateTargetId = "tarih"+item.descId,HttpMethod="Post" })%>
   <div id="tarih<%: item.descId%>" ></div>
  </td>
  <td class="Cell">
    <%: String.Format("{0:g}", item.UpdateDate) %>
  </td>
 
  <td class="CellEnd">
  <div style="float: left; width: 75px; height: 18px;">
       
        <a href='<%: Url.Action("BrowseDesc","Member",new { id=item.Member.MainPartyId }) %>'>
    <img src='<%: Url.Content("~/Content/Images/product.png") %>' />
        </a>
        <a href="/Description/Edit?descId=<%:item.descId %>&check=1">
       1
       </a>
       <a href="/Description/Edit?descId=<%:item.descId %>&check=2">
       2
       </a>
       <a href="/Description/Edit?descId=<%:item.descId %>&check=3">
       3
       </a>
       <a href="/Description/Edit?descId=<%:item.descId %>&check=4">
       4
       </a>
       <a href="/Description/Edit?descId=<%:item.descId %>&check=5">
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
                 <a href='<%: Url.Action("Index","Description",new { page=i }) %>'><span class="currentpage">
            <%: i %></span></a>
           
            <% }else{ %>
            <a href='<%: Url.Action("Index","Description",new { page=i }) %>'><span >
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

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

