﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginForm("Edit", "BankAccount", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
  <table class="tableForm" style="padding-top: 10px; height: auto;">
    <tr style="height: 40px;">
      <td colspan="2" align="right">
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/PacketFeatureType/Index'">
          İptal
        </button>
        <br />
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Banka Adı :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.BankName, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Iban Numarası :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.IbanNo, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Hesap Adı :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.AccountName, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Hesap Numarası :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.AccountNo, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Şube Adı :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.BranchName, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Şube Kodu :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.BranchCode, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr style="height: 40px;">
      <td colspan="2" align="right" style="padding-bottom: 10px;">
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
        <br />
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/PacketFeatureType/Index'">
          İptal
        </button>
      </td>
    </tr>
  </table>
  <%} %>
</asp:Content>
