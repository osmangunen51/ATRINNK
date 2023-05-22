﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<FilterModel<MessageModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <input type="hidden" name="OrderName" id="OrderName" value="MessageId" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <table id="typeTable" border="0" cellpadding="0" cellspacing="0" class="TableList"
      style="width: 100%; display: none">
      <tr>
        <td style="border: 1px solid #59b4e3; background-color: #cee5fd; padding: 5px">
          <span id="typeTitle" style="font-weight: 700; font-variant: small-caps"></span><span
            class="ui-icon ui-icon-close searchClear" onclick="$('#typeTable').fadeOut('slow');"
            title="Kapat"></span>
        </td>
      </tr>
    </table>
    <table cellpadding=5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on">
            Kimden
          </td>
          <td class="Header" unselectable="on" >
            Kime
          </td>
          <td class="Header" unselectable="on">
            Konu
          </td>
          <td class="Header" unselectable="on" >
            Mesaj Gönderme Tarihi
          </td>
          <td class="Header HeaderEnd">
            Araçlar
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("MessageList", Model)%>
      </tbody>
      <tfoot>
        <tr>
            <td>
  
            </td>
          <td class="ui-state ui-state-hover" colspan="5" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">

              <div style="float:right;">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.TotalRecord %></strong>
                  </div>
             <div style="float: right;" class="pagination">
    </div>
          </td>
        </tr>

      </tfoot>
    </table>
  </div>

  <script type="text/javascript">

    
    function DeletePost(storeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Message/Delete',
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

    function clearSearch(Id) {
      $('#' + Id).val('');
      $('#' + Id).trigger('keyup');
    }

    function OrderPost(orderName, e) {
      $('.HeaderDown').removeClass('HeaderDown');
      $(e).addClass('HeaderDown');
      $('#Order').val(($('#Order').val() == 'DESC' ? 'ASC' : 'DESC'));
      $('#OrderName').val(orderName);
      SearchPost();
    }

    function PagePost(page) {
      $('#Page').val(page);
      SearchPost();
    }

    function SearchPost() {
      $('#preLoading').show();

      $.ajax({
        url: '/Message/Index',
        type: 'post',
        data: {
          FromMainPartyFullName: $('#FromMainPartyFullName').val(),
          ToMainPartyFullName: $('#ToMainPartyFullName').val(),
          MessageDate: $('#MessageDate').val(),
          OrderName: $('#OrderName').val(),
          Order: $('#Order').val(),
          Page: $('#Page').val()
        },
        success: function (data) {
          $('#table').html(data);
          $('#preLoading').hide();
        },
        error: function (x) {
          $('#preLoading').hide();
          //$('#table').html(x.responseText);
        }
      });
    }
      function UpdateDataSeen(val) {
          $("#preLoading").show();
             $.ajax({
        url: '/Message/UpdateSeen',
        type: 'post',
                        data: {
                            id: val
        },
            dataType: 'json',
                 success: function (data) {
                     alert("Düzenleme Başarılı");
                     $("#clickButton"+val).hide();
                     $("#imgCheck" + val).show();
                        $("#preLoading").hide();
        },
        error: function (x) {
          $('#preLoading').hide();
          //$('#table').html(x.responseText);
        }
    });
      }

      $(document).ready(function () {
          $('.date').datepicker();
          $('.Search').keyup(SearchPost).change(SearchPost);
 
      });
     
  </script>
</asp:Content>
