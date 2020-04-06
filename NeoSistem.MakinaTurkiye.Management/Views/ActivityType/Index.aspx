<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<ActivityTypeModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Makina Türkiye | Adres Tipi Listesi
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    function DeletePost(storeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/ActivityType/Delete',
          data: { id: storeId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            var e = data.m;
            if (e) {
              $('#row' + storeId).hide();
            }
          }
        });
      }
    }
  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on">
            Faaliyet Tipi
          </td>
            <td class="Header">Sıra</td>
          <td class="Header" style="width: 70px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("AddressTypeList", Model)%>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="3" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.TotalRecord %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>
