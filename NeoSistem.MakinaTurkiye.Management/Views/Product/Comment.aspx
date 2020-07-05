﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.ProductCommentItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ürün Yorumları
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
            function confirmComment(confirm) {

      $('#preLoading').show();
                var idItems = "";
      $('.checkBoxProductComment').each(function () {

        if ($(this).attr('checked') == true) {
          idItems += $(this).val() + ",";
        }

      });

      $.ajax({
        url: '/Product/MultiCommentConfirm',
        data: {
          CheckItem: idItems,
         set:confirm
        },
        type: 'post',
        success: function (data) {
          window.location.href = '/Product/Comment/';
        },
        error: function (x, a, r) {
          $('#preLoading').hide();
        }
      });

        }
            function PagePost(p) {
              $('#preLoading').show();
      $.ajax({
        url: '/Product/Comment',
        data: {
            page: p,
            productId: '<%:Request.QueryString["productId"]%>',
            Reported:'<%:Request.QueryString["Reported"]%>'
        },
        type: 'post',
          success: function (data) {
              alert(data);
            $("#table").html(data);
             $('#preLoading').hide();
        },
        error: function (x, a, r) {
          $('#preLoading').hide();
        }
      });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; margin: 0 auto;">
          <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header">Ürün Adı
                    </td>
                    <td class="Header">Ürün No
                    </td>
                    <td class="Header">Ad Soyad
                    </td>
                    <td class="Header">Email</td>
                    <td class="Header">Yorum
                    </td>
                    <td class="Header">Puan</td>
                    <td class="Header">Tarih</td>
                    <td class="Header">Durum</td>
                    <td class="Header"></td>
                    <td class="Header HeaderEnd"></td>

                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_CommentList",Model) %>
            </tbody>

        </table>
    </div>



</asp:Content>

