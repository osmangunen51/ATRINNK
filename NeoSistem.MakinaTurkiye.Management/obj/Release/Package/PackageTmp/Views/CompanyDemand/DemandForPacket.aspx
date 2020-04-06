﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<NeoSistem.MakinaTurkiye.Management.Models.DemandsForPacketModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	İndirimli Paket Talebi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
  	<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
    <script type="text/javascript"> 
        function DeleteDemand(ID)
        {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/CompanyDemand/DeleteDemand',
                    data: { id: ID },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + ID).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin">
          #          </td>
              <td>
          Gönderen
          </td>
            <td>
            Firma             
            </td>
            <td>Telefon</td>
            <td>
                Açıklama         
            </td>
            <td>Durum</td>
            <td>Web Adresi</td>
            <td>Tarih</td>
          <td>
          </td>
        </tr>
      </thead>
      <tbody id="table">
       <% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.PacketForDemandModelId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%:row %>
  </td>
    <td>
         <%: item.NameSurname %>
    </td>
    <td>
        <%:item.StoreName %>
    </td>
    <td>
        <%:item.Email %>
    </td>
    <td>
    <%:item.Phone %>
    </td>
    
        <td>
            <a href="/Member/BrowseDesc1/<%:item.MemberMainPartyId %>" target="_blank">Ekle</a>
    </td>
    <% int page = Convert.ToInt32(ViewData["page"]); 
            if (item.Status == 0)
            {
                %> 
    <td style="color:#d30202">
         Aranmadı<br />     
         <a  href="/CompanyDemand/DemandforPacket?page=<%:page %>&status=1&id=<%:item.PacketForDemandModelId %>">Arandı İşaretle</a>
               </td>
         <%} else{%><td style="color:#04d060;">
         Arandı<br />
         <a   href="/CompanyDemand/PacketForDemandModelId?page=<%:page %>&status=0&id=<%:item.PacketForDemandModelId %>">Aranmadı İşaretle</a>
         </td>
             <%} %>
    <td>
    <%  if(item.WebUrl!=null)
        {
     
        if(item.WebUrl.IndexOf("http")>0)
      {%>
        <a href="<%:item.WebUrl %>"><%:item.WebUrl %></a>
     <% }
      else { 
         %>
    <a href="http://<%:item.WebUrl %>"><%:item.WebUrl %></a>
    <%}

        }
        else { Response.Write("Web Sitesi Girilmemiş"); }%>
    </td>
    <td><%:item.DemandDate %></td>

  <td >
    <a style="cursor: pointer;" onclick="DeleteDemand(<%:item.PacketForDemandModelId %>)">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
          <%int pageNumbers = Convert.ToInt32(ViewData["pageNumbers"]);
            for (int i = 1; i <=pageNumbers ; i++)
            {
                if(i==(int)ViewData["page"])
                {
                    Response.Write("<li>"+i+"</li>");
                }
                else
                {
                    Response.Write("<li><a href='/CompanyDemand/DemandForPacket?page="+i+"'>" + i + "</a></li>");
                }
                
            } %>
      </ul>
    </div>
  </td>
</tr>

      </tbody>
<%--      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="1" valign="top" style="width: 150px;">
          <%:Ajax.ActionLink("Hesapla", "ProductRateCalculate", new AjaxOptions() { UpdateTargetId = "statisticproduct", HttpMethod = "Post" })%>
          </td>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="1">
           : <div id="statisticproduct" style="margin-top: 5px">

            </div>
          </td>
          <td class="ui-state ui-state-hover" colspan="1" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%= Model.Count %></strong>
          </td>
        </tr>
      </tfoot>--%>
    </table>
  </div>
</asp:Content>