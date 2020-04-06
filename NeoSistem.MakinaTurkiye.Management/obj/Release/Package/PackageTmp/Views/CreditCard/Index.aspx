<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<CreditCard>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Makina Türkiye | Adres Tipi Listesi
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    function DeletePost(storeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/CreditCard/Delete',
          data: { id: storeId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            var e = data;
            if (e) {
              $('#row' + storeId).hide();
            }
            else {
              alert('Silmek hesabı kullanan paket bulunduğundan silme işlemi başarısız.');
            }
          }
        });
      }
    }
  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
      <p>Bu tanımlamalar iyzicoya gönderilecek taksit parametreleri için kullanılıyor. Aktif olanın taksit oranlarına göre fiyat belirlenecek.</p>
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on">
            Kredi Kartı Tanımı
          </td>
            <td class="Header">Durum</td>
          <td class="Header HeaderEnd" style="width: 70px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("CreditCardList", Model)%>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="8" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.Count %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>
