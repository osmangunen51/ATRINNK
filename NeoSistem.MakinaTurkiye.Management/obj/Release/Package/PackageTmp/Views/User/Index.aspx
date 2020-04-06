<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<UserModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Makinaturkiye Admin | Kullanıcı Listesi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/FilterModel.debug.js" type="text/javascript"></script>
  <script type="text/javascript">
    filterModel.set_controllerName('User');
    filterModel.set_loadElementId('#table');
    filterModel.set_loadingElementId('#preLoading');

    filterModel.register({
      read: function (sender, model) {
        sender.refreshModel({
          UserName: $('#UserName').val()
        });
      }
    });
      RegisterHidden('UserId');

             function PagingPost(curentpage) {
            $('#preLoading').show();
            $.ajax({
                url: '/User/Index',
                data: {
                    page: curentpage
                },
                type: 'post',
                success: function (data) {
                    $("#table").html(data);
                    $('#preLoading').hide();
                },
                error: function (x, a, r) {
                    alert("Error");

                }
            });
        }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on" onclick="Order('UserName', this);">
            Kullanıcı Adı
          </td>
          <td class="Header" width="200px" unselectable="on" onclick="Order('UserPass', this);">
            Şifre
          </td>
            <td class="Header">Bildirim</td>
            <td class="Header">Açıklama</td>
          <td class="Header">Durum</td>
            <td class="Header" style="width: 80px; height: 19px">
          </td>

        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("UserList", Model) %>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="6" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.TotalRecord %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>


</asp:Content>
