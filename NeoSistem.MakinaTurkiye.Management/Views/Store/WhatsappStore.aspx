<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.WhatsappStoreModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Whatsapp Tıklanmalar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript">
          function DeleteError(id) {
                         $.ajax({
          url: '/Help/ErrorDelete',
          data: {
         ID: id 
          },
          type: 'post',
         success: function (data) {
             $("#row" + id).hide();

          },
               error: function (x, a, r) {
                   alert("Error");
         
          }
        });
          }
          function PagingStore(curentpage) {
              
      $('#preLoading').show();
               $.ajax({
          url: '/Store/GetWhatsappStore',
          data: {
         CurrentPage: curentpage 
          },
          type: 'get',
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
      <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 50%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header">
            Firma Adı
          </td>
            <td  class="Header HeaderEnd">
               Tıklanma Sayısı
            </td>       
        </tr>
      </thead>
      <tbody id="table">
          
          <%=Html.RenderHtmlPartial("_WhatsappStoreListItem",Model) %>
      
          </tbody>
             
        </table>
      </div>



</asp:Content>

