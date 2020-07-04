<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Packet>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    function DeletePost(storeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Packet/Delete',
          data: { id: storeId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            var e = data;
            if (e) {
              $('#row' + storeId).hide();
            }
            else {
              alert('Silmek istediğiniz paketi kullanan firma olduğundan silme işlemi başarısız.');
            }
          }
        });
      }
    }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on">
            Paket Adı
          </td>
          <td class="Header" unselectable="on">
            Paket Açıklama
          </td>
          <td class="Header" unselectable="on">
            Paket Fiyatı
          </td>
          <td class="Header" unselectable="on">
            Geçerlilik Gün Sayısı
          </td>
          <td class="Header" unselectable="on">
            Başlangıç Paket
          </td>
          <td class="Header" unselectable="on">
            Stadart Paket
          </td>
            <td class="Header" unselectable="on">
                F.P.Yenileme Maili
            </td>
            <td class="Header" unselectable="on">
                Ürün Katsayı
            </td>
          <td class="Header" style="width: 70px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("PacketList", Model)%>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="9" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.Count %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>
