﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BaseMemberDescriptionModelNew>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
	Index

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<%@ Import Namespace="MvcContrib.Pagination"%>
<%@ import Namespace="MvcContrib.UI.Pager" %>
    
    <h2 style="margin-left:20px;">
        <%if (ViewData["title"] != null) {
              Response.Write(ViewData["title"]);
          }else{ %>
        Açıklamalar<%} %></h2>


                 <div style="font-size:18px; margin-top:20px; float:right;" >Filtrele:
                 <select name="users" onchange="if(this.value)window.location.href=this.value">
                     <%foreach (var item in Model.Users)
                         {
                             string selected = "";
                             if (item.Selected)
                                 selected ="selected";
                             %>
                            <option value="<%:item.Value %>" <%:selected %>><%:item.Text %></option>
                             <%} %>
                 </select>
                
    </div>
    <div style="float:left; margin-left:20px;">
        <a href="/MemberDescription/index?page=<%:Model.CurrentPage %>&order=<%:Model.Order %>&type=all">
        Tümü
        </a>|
        <a href="/MemberDescription/index?page=<%:Model.CurrentPage %>&order=<%:Model.Order %>&type=company">
            Firmalar
        </a>|
        <a href="/MemberDescription/index?page=<%:Model.CurrentPage %>&order=<%:Model.Order %>&type=member">
            Üyeler
        </a>   
        </div>
        
   <div style="text-align:center; float:left; margin-left:20px; margin-top:-5px; margin-bottom:2px;">
        <%using(Html.BeginForm()){ %>
        Firma Adı:
        <input type="text" name="searchTxt" />
        <input type="submit" value="Ara" />
        <%} %>
    </div>
 
    <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
    <thead>
        <tr>
            <td class="Header" style="width:10%" unselectable="on">
            Üye
          </td>
           <td class="Header" style="width:12%" unselectable="on">
            Firma
          </td>
          <td class="Header" style="width:15%" unselectable="on">
            Açıklama Başlığı
          </td>
          <td class="Header" style="width:30%" unselectable="on">
            İçerik
          </td>
          <td class="Header" style="width:10%" unselectable="on">
            <a href="/MemberDescription/index?page=<%:Model.CurrentPage %>&order=InputDate&type=<%:Model.Type %>&UserId=<%:Model.AutherizedId %>" title="Giriş Tarihine Göre Sondan Sırala">İşlem Tarih</a>
          </td>
         
          <td class="Header" style="width:10%" unselectable="on">
             <a href="/MemberDescription/index?page=<%:Model.CurrentPage %>&order=LastDate&type=<%:Model.Type %>&UserId=<%:Model.AutherizedId %>" title="Hatırlatma Tarihine Göre Sondan Sırala"> Hatırlatma Tarihi</a>
          </td>
            <%if(Model.Order.ToString()=="InputDate"){ %>
            <td class="Header">G</td>
            <%} %>
            <td class="Header">Atayan</td>
            <td class="Header" style="width:18%" unselectable="on">
           Araçlar
          </td>
         
        </tr>
        </thead>
    <tbody id="Tbody2">
       <% int row = 0; %>
<% foreach (var item in Model.BaseMemberDescriptionModelItems.ToList())
   { %>
<% row++;
 
%>

<tr id="row<%: item.ID%>" class="<%:(row % 2 == 0 ? "Row" : "RowAlternate") %>" style=" background-color:<%:item.Color%>">
  
  <td class="CellBegin">
    <%: item.MemberName+" "+item.MemberSurname%>
  </td>
  <td class="Cell">
      <%if(item.MemberType==20)
        { 
           %>
        <a href="/Store/EditStore/<%:item.StoreID %>" target="_blank"><%:item.StoreName %></a>
      <%} %>
  </td>
  <td class="Cell">
    <%: item.Title %>
  </td>
  <td class="Cell" style="font-size:15px;">
    <%:Html.Raw(Html.Truncate(item.Description,100)) %>
  </td>

  <td class="Cell">
    <%: String.Format("{0:g}", item.InputDate) %>
  </td>
  <td class="Cell">
      
    <%: String.Format("{0:g}", item.LastDate) %>

  </td>
    <%if(Model.Order.ToString()=="InputDate"){
           %>
    <td>
            <a  style="cursor:pointer;" onclick="UpdateDate(<%:item.ID %>)">G</a>
                <span id="g<%:item.ID%>"></span>   
     </td>
    <%} %>
 <td class="Cell">
     <%:item.FromUserName %>
 </td>
  <td class="CellEnd">
  <div style="float: left;  height: 18px;">
       
        <a href='<%: Url.Action("BrowseDesc1","Member",new { id=item.MemberMainPartyId }) %>'>
   <img src="/Content/images/ac.png" style="width:16px"  />
        </a>
        <%if (string.IsNullOrEmpty(item.PortfoyName)) { item.PortfoyName = "Tanımlanmamış"; } %>
      <%:item.PortfoyName%>
      </div>
   
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <%--<%= Html.Pager((IPagination)Model) %>--%>
     <% 
        int curr=(int)Model.CurrentPage;%>
     <ul>
     <%foreach (int i in Model.TotalLinkPages)
       {%>
           <li>
           <%if (i==curr)
             {%>
                 <a href='<%: Url.Action("Index","MemberDescription",new { page=i,order= Model.Order ,type=Model.Type,UserId=Model.AutherizedId }) %>'><span class="currentpage">
            <%: i %></span></a>
           
            <% }else{ %>
            <a href='<%: Url.Action("Index","MemberDescription",new { page=i,order= Model.Order,type=Model.Type,UserId=Model.AutherizedId  }) %>'><span >
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
          <td class="ui-state ui-state-hover" colspan="9" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: (int)Model.TotalCount%></strong>
          </td>
        </tr>
      </tfoot>
    </table>
   
    </div>
  

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function UpdateDate(id)
        {
            $.ajax({
                url: '/MemberDescription/UpdateDate',
                data: { ID: id },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        var d=new Date();
                        $("#g" + id).html("Gü");
                    }
                }
            });
        }

    </script>
</asp:Content>


