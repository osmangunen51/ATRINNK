﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<NeoSistem.MakinaTurkiye.Management.Models.ProductComplainModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ürün Şikayetleri
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
        function DeleteComplain(ID)
        {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/ProductComplain/DeleteComplain',
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
    <h2>Ürün Şikayetleri</h2>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5"  cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
            <td style="width:10%;">Ürün No</td>
            <td>Ürün Adı</td>
            <td style="width:15%;">
           Kullanıcı Adı </td>
            <td style="width:10%;">Email</td>
            <td style="width:10%;">Yorum</td>
              <td style="width:10%">Üye Durumu</td>
             <td style="width:10%;">Şikayet Tipleri</td>
            <td style="width:10%;">Tarih</td>
            
          <td style="width:10%;">
          </td>
        </tr>
      </thead>
      <tbody id="table">
       <% int row = 0; %>
<% foreach (var item in Model)
   { %>

<% row++; %>
<tr id="row<%: item.ID %>"  class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td>
    <%: item.ProductNo %>
  </td>
    <td>
        <%:item.ProductName %>
    </td>
    <td>
        <%:item.Name+" "+item.Surname %>
    </td>
    <td><%:item.Email %></td>

    <td><%:item.Comment %></td>
    <td><%if(item.IsMember==false){ %>
            Kayıt oldu
        <%}else{%>
            Kayıtlıydı
          <%} %>
    </td>
     <td><%if(item.ComplainNames!=""){ %><%:item.ComplainNames.Substring(1,item.ComplainNames.Length-1) %></td>
     <%} %>
    <td><%:String.Format("{0:d/M/yyyy HH:mm:ss}", item.ComplainDate) %></td>

  <td >
    <a style="cursor: pointer;" onclick="DeleteComplain(<%:item.ID %>)">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
      </tbody>
        <tfoot>
            <tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
          <%int pageNumbers = Convert.ToInt32(ViewData["pageNumbers"]);
            if(pageNumbers>1)
            { 
            for (int i = 1; i <=pageNumbers ; i++)
            {

                Response.Write("<li id='page-" + i + "'><a  href='/ProductComplain/index?page="+i+"'>" + i + "</a></li>");


            }
            }%>
      </ul>
    </div>
  </td>
</tr>
                 <tr>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="8" valign="top" style="width: 150px;">
          </td>
          <td class="ui-state ui-state-hover" colspan="1" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%:ViewData["Total"] %></strong>
          </td>
        </tr>
        </tfoot>

    </table>
  </div>
</asp:Content>