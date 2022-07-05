<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.DeleteOrderViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Sipariş Sil

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #tblOrderDelete tr td{
            font-size:15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div style="font-size:16px; margin-top:15px; margin-left:15px;">SİPARİŞ SİL</div>
    
        <fieldset style="width:300px;">
            <legend>Açıklama</legend>
    <table id="tblOrderDelete">
        <%using(Html.BeginForm()){ %>
        <%:Html.HiddenFor(x=>x.Type) %>
        <tr>
            <td>Admin Şifre:</td>
            <%:Html.HiddenFor(x=>x.OrderId) %>
            <td><%:Html.PasswordFor(x=>x.AdminPassword) %></td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="Sil"/></td>
        </tr>
        <%} %>
    </table>
    <%if(ViewData["error"]=="true"){ %>
        <span style="color:red;font-size:14px;">Şifre Hatalı!</span>
    <%} %>
            </fieldset>
</asp:Content>
