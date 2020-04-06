﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Index</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
  <style type="text/css">
	 .btnGonder
	 {
		background-image: url('/Areas/Account/Content/Images/btnSend.png');
		width: 81px;
		height: 25px;
		border: none;
		cursor: pointer;
	 }
	 .textBig
	 {
		width: 304px;
		height: 18px;
		border: solid 2px #aadbdf;
		padding-left: 12px;
		background-color: #fff;
		padding-top: 2px;
	 }
	 .textBig input
	 {
		width: 290px;
		background-color: transparent;
		border: none;
		font-family: Segoe UI,Arial;
		font-size: 11px;
	 }
	 .textBigArea
	 {
		width: 304px;
		height: 88px;
		border: solid 2px #aadbdf;
		padding-left: 12px;
		background-color: #fff;
		padding-top: 2px;
	 }
	 .textBigArea textarea
	 {
		width: 300px;
		height: 83px;
		background-color: transparent;
		border: none;
		font-family: Segoe UI,Arial;
		font-size: 11px;
	 }
	 
	 .textMedium
	 {
		width: 64px;
		height: 18px;
		border: solid 2px #aadbdf;
		padding-left: 12px;
		background-color: #fff;
		padding-top: 2px;
		float: left;
	 }
	 .textMedium input
	 {
		width: 50px;
		background-color: transparent;
		border: none;
		font-family: Segoe UI,Arial;
		font-size: 11px;
	 }
	 .newMembership
	 {
		font-size: 12px;
		color: Gray;
		text-decoration: none;
	 }
	 .newMembership:visited
	 {
		font-size: 12px;
		color: Gray;
	 }
	 .newMembership:hover
	 {
		font-size: 12px;
		color: #000;
	 }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginForm("NotificationForm", "MembershipSales", FormMethod.Post))
	 {%>
  <div style="width: 100%; height: 30px; margin-top: 5px; float: left; margin-left: 10px;">
	 <div style="width: 250px; height: 24px; background-color: #74a1d0; color: #fff; font-size: 13px;
		padding-left: 20px; padding-top: 4px;">
		<span style="font-family: Segoe UI,Arial; font-weight: bold">BİLDİRİM FORMU</span>
	 </div>
  </div>
  <div style="width: 100%; height: auto; float: right;">
	 <div style="float: left; width: 45%; height: auto;">
		<div style="width: 420px; height: auto; float: left; margin-top: 10px; margin-left: 15px;">
		  <table cellpadding="0" cellspacing="0" style="width: 100%;">
			 <tr>
				<td style="width: 100px; height: 35px;">
				  <span style="font-size: 12px;">Konu</span>
				</td>
				<td style="width: 10px;">
				  <span style="font-size: 12px;">:</span>
				</td>
				<td>
				  <div class="textBig">
					 <input type="text" name="Subject" id="Subject" />
				  </div>
				</td>
			 </tr>
			 <tr>
				<td style="width: 100px; height: auto;" valign="top">
				  <span style="font-size: 12px;">Açıklama</span>
				</td>
				<td style="width: 10px;" valign="top">
				  <span style="font-size: 12px;">:</span>
				</td>
				<td>
				  <div class="textBigArea">
					 <textarea name="Description" id="Description"></textarea>
				  </div>
				</td>
			 </tr>
			 <tr>
				<td colspan="3" align="right" style="height: 40px;">
				  <button class="btnGonder" type="submit">
				  </button>
				</td>
			 </tr>
			 <% if (ViewData["HasSender"] != null)
				 { %>
			 <tr>
				<td colspan="3" align="right" style="padding-top: 10px;">
				  <span style="font-size: 12px; color: Red;">Bildirim formu başarıyla gönderilmiştir.</span>
				</td>
			 </tr>
			 <% } %>
		  </table>
		</div>
	 </div>
  </div>
  <% } %>
</asp:Content>
