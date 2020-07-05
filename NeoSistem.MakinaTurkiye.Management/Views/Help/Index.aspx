<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.HelpListModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Satış Yardım Listesi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript">

          function HelpSearch() {
              var value = $("#HelpSearchText").val();
              if (value.length > 2) {
           
                  $.ajax({
                      url: '/Help/SearchByText',
                      data: {
                          SearchText: value
                      },
                      type: 'post',
                      success: function (data) {
                          $("#table").html("");
                          $('#table').html(data);
                       

                      },
                      error: function (x, a, r) {
                          alert("Error");

                      }
                  });
              }
              else {
                  PageHelp(1);
              }
              if (value == "") {
               
                  PageHelp(1);
              }
          }

          function DeleteHelp(id) {
   
                         $.ajax({
          url: '/Help/Delete',
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
    function PageHelp(page) {
           $.ajax({
          url: '/Help/GetForPaging',
          data: {
         newPage: page 
          },
          type: 'post',
          success: function (data) {
              $("#table").html("");
                          $('#table').html(data);

        
          },
               error: function (x, a, r) {
                   alert("Error");
         
          }
        });
  
     
    }
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">

<button style="margin-top:10px;" onclick="window.location='/Help/Add'" >Yeni Ekle</button>
        <div style="margin-top:20px">Ara:  <input id="HelpSearchText" onfocus="PageHelp(1)"  onkeyup="HelpSearch()" class="" placeholder="Anahtar Kelime.." style="width: 30%; " /></div>

    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header">
          Konu
          </td>
            <td  class="Header">
               Content
            </td>
            <td  class="Header">
               Tarih
            </td>
            <td class="Header">
            
            </td>
       
        </tr>
      </thead>
      <tbody id="table">
          <%Html.RenderPartial("_HelpDataItem", Model); %>
          </tbody>
             
        </table>
      </div>



</asp:Content>

