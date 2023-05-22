<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<IEnumerable<PermissionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 99%; margin: auto;">
    <table border="0" cellpadding="5" cellspacing="0" style="float: right;">
      <tr>
        <td>
          Arama :
        </td>
        <td>
          <input type="text" name="Search" id="Search" style="width: 250px" />
        </td>
      </tr>
    </table>
    <div id="table">
      <%= Html.RenderHtmlPartial("PermissionList", Model) %>
    </div>
  </div>
  <script type="text/javascript">
    $(document).ready(function () {
      $('#Search').keyup(function () {
        $.ajax({
          url: '/Permission/Index',
          data: { search: $(this).val() },
          type: 'post',
          success: function (data) {
            $('#table').html(data);
          }
        });
      });
    });

    function DeletePost(permissionId) {
      $.ajax({
        url: '/Permission/Delete',
        data: { id: permissionId },
        type: 'post',
        dataType: 'json',
        success: function (result) {
          if (result) {
            $('#row' + permissionId).hide();
          }
        }
      });
    }
  </script>
</asp:Content>
