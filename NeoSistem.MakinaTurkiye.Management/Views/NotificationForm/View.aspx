<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NotificationFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Makina Türkiye | Adres Tipi Düzenle
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <table class="tableForm" style="padding-top: 10px; height: auto;">
    <tr>
      <td style="width: 120px;">
        Konu :
      </td>
      <td>
        <%: Model.NotificationFormSubject%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Açıklama :
      </td>
      <td>
        <%: Model.NotificationFormDescription%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Üye Adı :
      </td>
      <td>
        <%: Model.MemberName + " "+Model.MemberSurname %>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Kayıt Tarihi :
      </td>
      <td>
        <%: Model.RecordDate.ToString("dd.MM.yyyy HH:mm:ss")%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        Okundu Durumu :
      </td>
      <td>
        <%: Model.IsRead ? "Okundu" : "Okunmadı" %>
      </td>
    </tr>
  </table>
</asp:Content>
