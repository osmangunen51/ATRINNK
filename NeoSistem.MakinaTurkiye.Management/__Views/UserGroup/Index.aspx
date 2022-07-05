<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<UserGroupModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 99%; margin: auto; margin-top: 5px;">
    <tbody id="table">
      <%= Html.RenderHtmlPartial("UserGroupList", Model) %>
    </tbody>
    <tfoot>
      <tr>
        <td class="ui-state ui-state-hover" colspan="3" align="right" style="border-color: #DDD;
          border-top: none; padding-right: 10px">
          Toplam Kayıt : &nbsp;&nbsp;<strong>
            <%: Model.Count %></strong>
        </td>
      </tr>
    </tfoot>
  </div>
  <script type="text/javascript">
    $(document).ready(function () {
      $('#Search').keyup(function () {
        $.ajax({
          url: '/UserGroup/Index',
          data: { search: $(this).val() },
          type: 'post',
          success: function (data) {
            $('#table').html(data);
          }
        });
      });
    });

    function Delete(permissionId) {
      $.ajax({
        url: '/UserGroup/Delete',
        data: { id: permissionId },
        type: 'post',
        dataType: 'json',
        success: function (data) {
          var e = data.m;
          if (e) {
            $('#row' + permissionId).hide();
          }
        }
      });
    }
  </script>
</asp:Content>
