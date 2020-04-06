<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>--%>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="background-color:#fff; border:1px solid #ccc;">
          <div style="margin: 10px; margin-bottom: 0px; float: left;">
             <h3>Ödeme Durumu</h3>
             <div class="alert alert-success"  role="alert">
                <strong>  <% = ViewData["text"] %></strong>
              </div>
            </div>
    </div>

</asp:Content>
