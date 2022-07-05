<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<NotificationFormModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on">
            Bildirim Formu Konusu
          </td>
          <td class="Header" unselectable="on">
            Gönderen Üye Adı
          </td>
          <td class="Header" unselectable="on">
            Gönderim Tarihi
          </td>
          <td class="Header" unselectable="on">
            Okunma Durumu
          </td>
          <td class="Header HeaderEnd" style="width: 70px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("NotificationFormList", Model)%>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="5" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.Count %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>
