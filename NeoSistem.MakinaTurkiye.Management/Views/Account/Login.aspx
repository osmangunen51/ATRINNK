﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AccountModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Makina Türkiye | Kullanıcı Girişi</title>
  <meta http-equiv="X-UA-Compatible" content="IE=8">
  <link href="/Content/Site.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
  <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
  <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.js" type="text/javascript"></script>
  <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
  <link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">
    function focusText() { $get('UserName').focus(); $('button').button(); }
  </script>
</head>
<body onload="focusText();" style="background-image: url('/Content/Images/wall.jpg');
  background-repeat: no-repeat;">
  <% Html.EnableClientValidation(); %>
  <div style="margin: 20% auto; height: 150px; width: 100%">
    <div style="float: left; width: 170px;">
    </div>
    <div style="float: right; width: 400px; padding-top: 30px;">
      <% using (Html.BeginForm("Login", "Account"))
         {  %>
      <input type="hidden" name="returnUrl" value="<%: Request.QueryString["returnUrl"] %>" />
      <table style="background-color: #FFF; border: solid 1px #DDD; padding: 10px; background-image: url('/Content/Images/ribbon_background.png');
        background-position: bottom; background-repeat: repeat-x">
        <tr>
          <td colspan="2" align="right">
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(model => model.UserName)%>:
          </td>
          <td>
            <%: Html.TextBoxFor(model => model.UserName, new { style = "width: 170px;" })%>
            <%: Html.ValidationMessageFor(model => model.UserName)%>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(model => model.UserPass)%>:
          </td>
          <td>
            <%: Html.PasswordFor(model => model.UserPass, new { style = "width: 170px;" })%>
            <%: Html.ValidationMessageFor(model => model.UserPass)%>
          </td>
        </tr>
        <tr>
          <td colspan="2" align="right">
            <button type="submit" style="height: 27px">
              Giriş</button>
          </td>
        </tr>
      </table>
      <%: Html.ValidationSummary(true)%>
      <% } %>
    </div>
  </div>
</body>
</html>
