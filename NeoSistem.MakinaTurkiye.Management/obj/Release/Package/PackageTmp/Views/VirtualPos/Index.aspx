<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<VirtualPos>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Makina Türkiye | Sanal Post Listesi
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    function DeletePost(virtualPostId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/VirtualPos/Delete',
          data: { id: virtualPostId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            var e = data;
            if (e) {
              $('#row' + virtualPostId).hide();
            }
            else {
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
            Sanal Pos Adı
          </td>
          <td class="Header" unselectable="on">
            Sanal Pos Türü
          </td>
          <td class="Header" unselectable="on">
            Aktif
          </td>
          <td class="Header HeaderEnd" style="width: 70px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("VirtualPosList", Model)%>
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
