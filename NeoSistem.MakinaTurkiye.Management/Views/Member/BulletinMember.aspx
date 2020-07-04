﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.BulletinMemberModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Bülten Üyeleri-makinaturkiye.com admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript">
          function DeleteRecord(ID) {
              if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                  $.ajax({
                      url: '/Member/BulletinDelete',
                      data: {
                         id : ID
                      },
                      type: 'post',
                      success: function (data) {
                          $("#row" + ID).hide();

                      },
                      error: function (x, a, r) {
                          alert("Error");

                      }
                  });
              }
          }
          function PageCategoryChange() {
              PagingBulletin(1);

          }
          function PageOrder() {
              var val = $("#Order").val();
              if (val == "asc")
                  $("#Order").val("desc");
              else
                  $("#Order").val("asc");
              PagingBulletin($("#CurrentPage").val());
          }
          function PagingBulletin(curentpage) {
          
      $('#preLoading').show();
               $.ajax({
          url: '/Member/BulletinMember',
          data: {
              CurrentPage: curentpage,
              Order: $("#Order").val(),
              categoryId:$("#CategoryId").val()
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
      <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin">
           #
          </td>
            <td class="Header">
                Email
            </td>
            <td class="Header">İsim Soyisim</td>
            <td class="Header">İlgilendiği Kategori/ler</td>
            <td class="Header">Tarih</td>
            <td class="Header HeaderEnd">Araçlar</td>
        </tr>
            <tr style="background-color: #F1F1F1">
            <td class="Cell CellBegin"></td>
                <td class="Cell"></td>
                <td class="Cell"></td>
                <td class="Cell"></td>
                <td class="Cell">
                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style=" ">
                         <%var sectorCategories = (List<SelectListItem>)(ViewBag.SectorCategories); %>
                      <%:Html.DropDownList("CategoryId",sectorCategories,new {id="CategoryId",@onchange="PageCategoryChange();"  }) %>
                  </td>
                </tr>
              </tbody>
            </table>

                </td>
                <td class="Cell CellEnd"></td>
            </tr>
      </thead>
 

      <tbody id="table">
          
          <%=Html.RenderHtmlPartial("_BulletinRegisterList",Model) %>
      
          </tbody>
             
        </table>
      </div>



</asp:Content>

