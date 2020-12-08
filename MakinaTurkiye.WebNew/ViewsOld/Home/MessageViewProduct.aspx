<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MessageViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <script src="/Scripts/FaceboxMessageProduct/jquery.js" type="text/javascript"></script>
  <link href="/Content/home.css" rel="stylesheet" type="text/css" />
	  <link href="/Scripts/FaceboxMessageProduct/facebox.css" media="screen" rel="stylesheet" type="text/css" />
  <script src="/Scripts/FaceboxMessageProduct/facebox.js" type="text/javascript"></script>
  <style type="text/css">
	 input.membershipValid, textarea.membershipValid
	 {
		border: 1px solid red;
		border-top: 2px solid red;
	 }
	  .sendbutton
	 {
		background-image: url('/Areas/Account/Content/Images/messagesend.png');
		width: 117px;
		height: 25px;
		border: none;
		cursor: pointer;
	 }
.inquiry-box-inner {
border: 1px solid white;
width: auto;
height: 100%;
background-color:#FCF4CD;
padding: 10px;
}

  </style>
 <script type="text/javascript">
	jQuery(document).ready(function ($) {
	  $('a[rel*=facebox]').facebox({
		 loadingImage: '/Scripts/FaceboxMessageProduct/loading.gif',
		 closeImage: '/Scripts/FaceboxMessageProduct/closelabel.png'
	  })
	});
 </script>
	<script type="text/javascript">
	  $(document).ready(mesajGoster);
	  function mesajGoster() {
		 if ($('#uyekayitli').val() == 1) {
			jQuery.facebox({ ajax: '/Productmessage' });
		 }
		 else if ($('#kayitliuye').val() == 1) {
			jQuery.facebox({ ajax: '/succeed' });
		 }
	  }
	  if ($('#MembershipType').val() == 5) {
		 $('#formFastMembership').validate({
			errorClass: 'membershipValid',
			errorElement: 'div',
			messages: {
			  "Loginitems_Email": ''
			}
		 });
	  }
  </script>
	 <title>Mesaj gönder!</title>
</head>
<body style="position: relative; min-height: 100%; top: 0px; ">
<input type="hidden" id="uyekayitli" value="<%: Session["sendingsucciid"].ToInt32()%>" />
<input type="hidden" id="kayitliuye" value="<%: ViewData["sendingsucceed"].ToInt32()%>" />
<input type="hidden" id="hiddenMemberType" value="<%: ViewData["MembershipType"]%>" />
<input type="hidden" id="MembershipType" value="<%: ViewData["MembershipType"]%>" />
<% Html.EnableClientValidation(); %>
  <%MvcForm mvcfrm=Html.BeginForm();%>
 <div style="width:auto;height:auto;" align="center">
 <div id="header-inquiry">
			<div align="center">
				<div style="width:800px;height:40px;">
				<div style="width:350px;height:40px; float:left;">
				<a href="/"><img src="/Content/Images/logo.png" align="absmiddle" alt="" border="0"/></a><span class="page-title"></span>
				</div>
					
					 <div style="width:450px;height:80px; float:left;">
							<%= Html.RenderHtmlPartial("QuestionMember")%>
					 </div>
				</div>
			</div>
		</div>
		
	 <div style="float:none; margin-left: 20px; width:750px;min-height:390px; height:auto; border: solid 2px #99D6DD;margin-top:30px; " align="center">
  
		  <div style="width: 100%; height: 25px;  float: left; margin-left: 10px;">
	 <div style="width: 250px; height: 24px; background-color: #74a1d0; color: #fff; font-size: 13px;
		padding-left: 5px;  float:left; padding-top:3px;">
		<span style="font-family: Segoe UI,Arial; font-weight: bold">MESAJ GÖNDER</span>
	 </div>
	 <span style="font-size:11px;float:left; text-align:right; margin-right:15px; width:450px;"><span style="color:#0033CC; font-family: Arial; font-size: 10px; font-weight:bold;"> İlan no:</span><%=Model.Product.ProductNo %></span>
  </div>
  <input type="hidden" name="Message.MainPartyId" id="MainPartyId" />
  <div style="width:165px;height:300px; float:left; position:relative;">
  <div style="width:165px;margin-top:12px; font-family:Arial;font-size:11px; color:#888; text-align:right;">
  Kimden :
  </div>
  <%if (AuthenticationUser.Membership.MainPartyId == 0)
	 {  %>
	 <%if (ViewData["kullaniliyor"].ToInt32() == 3)
		{ %>
  <div style="width:165px;margin-top:15px;font-family:Arial;font-size:11px; color:#888;text-align:right;">
  Şifre :
  </div>
  <%} %>
  <%} %>
  <%if (AuthenticationUser.Membership.MainPartyId != 0)
	 { %>
  <div style="width:165px;margin-top:15px;font-family:Arial;font-size:11px; color:#888;text-align:right;">
  Kime :
  </div>
  <%} %>
  <div style="width:165px;font-family:Arial;font-size:11px; color:#888;text-align:right; top:70px; position:absolute;">
  Mesaj :
  </div>
  </div>
  <table style="width: 580px;">
<%--    <tr>
		<td style="width: 75px; padding-top: 10px;">
		  <span style="font-size: 12px;">Konu :</span>
		</td>
		<td style="padding-top: 10px;">
		  <div style="border: solid 1px #99D6DD; width: 300px; height: 18px; padding-top: 4px;">
			 <%=Html.TextBox("ProductDescription", "", new { style = "width: 270px; margin-left: 15px; height: 11px; font-size: 12px; border: none; background-color: transparent;" })%>
		  </div>
		</td>
	 </tr>--%>
	 <tr>
		<td valign="top" style="padding-top: 10px;">
		
		</td>
		<td style="padding-top: 10px;">
		
		<%if (AuthenticationUser.Membership.MainPartyId == null || AuthenticationUser.Membership.MainPartyId == 0)
		  {  %>
			 <div style="float:left;width: 570px; height: 30px;font-family:Arial;font-size:12px; color:#888;float:left;" align="left" >
		<div style=" width: 170px; height: 23px;font-family:Arial;font-size:12px; color:#888;float:left;">
		<%:Html.TextBoxFor(m => m.Loginitems.Email, new { style = "width:170px;height:14px;color:silver;", Title = "Email adresinizi giriniz.", onfocus = "if(this.beenchanged!=true){ this.value = '';;this.style.color='black';}", onblur = "if(this.beenchanged!=true) { this.value='Email adresinizi giriniz.';this.style.color='silver';  }", onchange = "this.beenchanged = true;" })%>
		</div>
	  <%-- <div>
		<%: Html.ValidationMessage("mail","*")%>
		</div>--%>
		<%--eğer mail mevcut ise aşağıdaki ifadeyi görünür kıl.--%>
		<span style="margin-left:20px;">
		<%:Html.ValidationMessage("mail")%>
		</span>
		</div>
				<%if (ViewData["kullaniliyor"].ToInt32() == 3)
		  {  %>
		<div style=" width: 170px; height: 35px;font-family:Arial;font-size:12px; color:#888;margin-top:30px;">
		<%:Html.PasswordFor(m => m.Loginitems.Password, new { style = "width:169px;height:14px;", onfocus = "if(this.beenchanged!=true){ this.value = '';;this.style.color='black';}", onblur = "if(this.beenchanged!=true) { this.value='Şifre giriniz.';this.style.color='silver';  }", onchange = "this.beenchanged = true;" })%><%:Html.ValidationMessage("sifrebos")%>
		</div>
		<%} %>
		<%} %>
		
		<% if (AuthenticationUser.Membership.MainPartyId != 0)
			{  %>
			<div style="float:left;width: 570px; height: 60px;font-family:Arial;font-size:12px; color:#888;float:left;" align="left" >
			 <div style=" width: 570px; height: 30px;font-family:Arial Baltic;font-size:13px; color:#036;">
		<%:AuthenticationUser.Membership.MemberEmail.ToString()%>
		</div>
		<%--eğer mail mevcut ise aşağıdaki ifadeyi görünür kıl.--%>
		<div style=" width: 570px; height: 30px;color:#03C;font-family:Arial;font-size:12px;">
	  <%--<%:ViewData["isimsoyisim"].ToString() %>--%>
	  Bir Soru Sor Makina Türkiye
		</div>
		</div>
		<%} %>
		<%if (Model.Product != null)
		  {  %>
  
		  <span style="color:Blue;">
		  <%: ViewData["ürünadı"]%></span>
		  <span style="color:#543F8C;"><%:ViewData["markamodel"]%></span>
		<%} %>
		  <div style=" width: 570px; height: 265px; ">
		  <%:Html.TextAreaFor(m => m.Message.Content, new { style = "width:570px;height:245px;", validate = "required:true" })%>
		<%--    <%int kullaniciid=0;,
				kullaniciid = AuthenticationUser.Membership.MainPartyId; %>
			 <%Session[kullaniciid]=Html.TextBox("ProductDescription").ToString(); %>
			 <%string degerd = Html.TextBox("ProductDescription").ToString(); %>--%>
		  </div>
		  <div style=" width: 575px; height: 50px; padding-top: 20px;">
		  <%:Html.ValidationMessage("mesages")%>
		  </div>
		</td>
	 </tr>
	 <tr>
		<td colspan="2">
				<div style="font-size:17px; font-family:Arial; float:left;">
			 <% if (ViewData["sendingsucceed"].ToInt32() == 1)
				 {  %>
				 mesaj gönderildi.
			 <%} %>
<%--          <%if (Session["sendingsucciid"].ToInt32() == 2)
				{%>
				Mesajınız başarıyla gönderildi.Mail adresinize gönderilmiş olan aktivasyon mailini unutmayınız.
			 <%} %>--%>
			 <%if (Session["sendingsucciid"].ToInt32() == 3)
				{%>
				güvenlik kodunu yanlış girdiniz.
			 <%} %>
			 </div>
		<div style="margin-left:100px; margin-bottom:10px;">
		<% if (AuthenticationUser.Membership.MainPartyId != 0)
			{%>
			<div id="validator" style="color:#DF4913; font-size:11px;font-family:Arial; float:right; margin-right:40px;">
			<%: Html.ValidationMessageFor(m => m.Message.Content)%>
			</div>
		<button id="other" type="submit" class="sendbutton">
			 </button>
			 <%} %>
			 <%else if(AuthenticationUser.Membership.MainPartyId==null||AuthenticationUser.Membership.MainPartyId==0)
			{  %>
			<%if (Session["sendingsucciid"].ToInt32() !=1)
			  {  %>
			<button id="Button1" type="submit" class="sendbutton"></button>
			<a href="/succeed" rel="facebox"></a>
				  <%} %>
			 <%if (Session["sendingsucciid"].ToInt32() == 1)
				{  %>
				<a href="/Productmessage" rel="facebox"></a>
				
				<button id="Button2" type="submit" class="sendbutton"></button>
		<%} %>
			 <%} %>
			 </div>
	 
		</td>
	 </tr>
	 </table>
	 
  

</div>
</div>
<%mvcfrm.Dispose(); %>

  <%--<script type="text/javascript" defer="defer">
	 var editor = CKEDITOR.replace('Message.Content', { toolbar: 'webtool' });
	 CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
  </script>--%>
  <%if (Session["sendingsucciid"].ToInt32() == 2)
	 {
		Session["sendingsucciid"] = 0;
	 }
		 %>

</body>
</html>
