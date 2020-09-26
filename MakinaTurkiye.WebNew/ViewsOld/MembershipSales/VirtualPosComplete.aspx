<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="margin: 10px; margin-bottom: 0px; float: left;">
    <span style="font-size: 14px; font-weight: bold;">İşlem Durumu : </span><span style="font-size: 13px;
      font-weight: bold;">
      <% = ViewData["text"] %></span>
  </div>
</asp:Content>
