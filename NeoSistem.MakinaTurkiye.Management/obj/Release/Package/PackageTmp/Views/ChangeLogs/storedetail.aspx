﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MakinaTurkiye.Entities.Tables.Stores.StoreChangeHistory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Firma Bilgileri Değişikliği
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

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
              <div>
            <h2>Değiştirmeden Önce</h2>
            <table border="0" cellpadding="5" cellspacing="0" style="margin-left: 20px;">
                <thead>
                    <tr>
                        <th></th>
                        <th></th>
                        <th>Önce</th>
                        <th>Sonra</th>
                    </tr>
                </thead>
        <tr>
          <td style="width: 150px;">
            Firma İsmi
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DisplayFor(m => m.StoreName, new { style = "width: 250px;" })%>
          </td>
            <td>
              <%: Html.DisplayFor(m => m.Store.StoreName, new { style = "width: 250px;" })%>
            </td>
        </tr>
         <tr>
          <td>
            Firma Kısa İsmi
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DisplayFor(m => m.StoreUniqueShortName, new { style = "width: 250px;" })%>
          </td>
                <td>
            <%: Html.DisplayFor(m => m.Store.StoreUniqueShortName, new { style = "width: 250px;" })%>
          </td>
        </tr>
        <tr>
          <td>
            Web Adresi
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DisplayFor(m => m.StoreWeb, new { style = "width: 250px;" })%>
          </td>
             <td>
            <%: Html.DisplayFor(m => m.Store.StoreWeb, new { style = "width: 250px;" })%>
          </td>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            :
          </td>
          <td>
            <a style="color: Blue;" href="http://<%:Model.StoreWeb %>" target="_blank">
              <%:Model.StoreWeb %></a>
          </td>
            <td>
                  <a style="color: Blue;" href="http://<%:Model.Store.StoreWeb %>" target="_blank">
              <%:Model.Store.StoreWeb %></a>
            </td>
        </tr>
        <tr>
          <td>
            Email
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DisplayFor(m => m.StoreEMail, new { style = "width: 250px;" })%>
          </td>
              <td>
            <%: Html.DisplayFor(m => m.Store.StoreEMail, new { style = "width: 250px;" })%>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreCapital)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Model.StoreCapital %>
          </td>
               <td>
            <%:Model.Store.StoreCapital %>
          </td>
        </tr>
        <tr>
          <td>
            Kuruluş Yılı
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DisplayFor(m => m.StoreEstablishmentDate, new { style = "width: 94px;" })%>
          </td>
             <td>
            <%: Html.DisplayFor(m => m.Store.StoreEstablishmentDate, new { style = "width: 94px;" })%>
          </td>
        </tr>
        <tr>
          <td>
            Firma Tipi
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.DisplayFor(m => m.StoreType, new { style = "width: 170px; "})%>
          </td>
               <td>
            <%:Html.DisplayFor(m => m.Store.StoreType, new { style = "width: 170px; "})%>
          </td>
        </tr>
        <tr>
          <td>
            Çalışan Sayısı
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DisplayFor(c => c.StoreEmployeesCount, new { style = "width: 170px; " })%>
          </td>
          <td>
                  <%: Html.DisplayFor(c => c.Store.StoreEmployeesCount, new { style = "width: 170px; " })%>
          </td>
        </tr>
        <tr>
          <td>
          Ciro
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.DisplayFor(m => m.StoreEndorsement, new { style = "width: 170px; " })%>
          </td>
          <td>
            <%:Html.DisplayFor(m => m.Store.StoreEndorsement, new { style = "width: 170px; " })%>
          </td>
     
        </tr>
 
          <tr>
              <td><%:Html.LabelFor(x=>x.TaxOffice) %>/<%:Html.LabelFor(x=>x.TaxNumber) %></td>
              <td>:</td>
              <td><%:Html.TextBoxFor(m=>m.TaxOffice) %>/<%:Html.TextBoxFor(m=>m.TaxNumber) %></td>
               <td><%:Html.TextBoxFor(m=>m.Store.TaxOffice) %>/<%:Html.TextBoxFor(m=>m.Store.TaxNumber) %></td>
          </tr>
        <tr>
                <td><%:Html.LabelFor(x=>x.MersisNo) %>/<%:Html.LabelFor(x=>x.TradeRegistrNo) %></td>
                <td>:</td>
                <td><%:Html.TextBoxFor(m=>m.MersisNo) %>/<%:Html.TextBoxFor(m=>m.TradeRegistrNo) %></td>
       <td><%:Html.TextBoxFor(m=>m.Store.MersisNo) %>/<%:Html.TextBoxFor(m=>m.Store.TradeRegistrNo) %></td>
            </tr>
                <tr>
          <td valign="top">
            <%: Html.LabelFor(m => m.StoreAbout)%>
          </td>
          <td valign="top">
            :
          </td>
          <td>
            <%: Html.TextAreaFor(m => m.StoreAbout, new { style = "width:350px; height : 60px;" })%>
          </td>
        <td>
            <%: Html.TextAreaFor(m => m.Store.StoreAbout, new { style = "width:350px; height : 60px;" })%>
          </td>
        </tr>
                <tr>
                    <td>Logo</td>
                    <td>:</td>
                    <td> <% if (!String.IsNullOrEmpty(Model.StoreLogo))
               {
                 string logoThumb = "//s.makinaturkiye.com/Store/" + Model.MainPartyId + "/" + Model.StoreLogo;  %>
	               <img src="<%:logoThumb %>" style="border: solid 1px #b6b6b6;" width="300" height="200" />
            <% }
               else
               {

               } %></td>
                    <td>
                        <% if (!String.IsNullOrEmpty(Model.Store.StoreLogo))
               {
                   string logoThumb = "//s.makinaturkiye.com/Store/" + Model.Store.MainPartyId + "/" + Model.Store.StoreLogo;  %>
	               <img src="<%:logoThumb %>" style="border: solid 1px #b6b6b6;" width="300" height="200" />
            <% }
               else
               {

               } %>
                    </td>
                </tr>
            </table>
        </div>

</asp:Content>