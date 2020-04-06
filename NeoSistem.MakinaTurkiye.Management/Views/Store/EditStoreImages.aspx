﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
  <script type="text/javascript">

    function DeletePicture(Idd) {
      if (confirm('Resmi silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Store/DeleteStoreImage',
          type: 'post',
          data: { id: Idd, storeId: $('#storeId').val() },
          success: function (data) {
            $('#divPictureList').html(data);
          },
          error: function (x, l, e) {
            alert(x.responseText);
          }
        });
      }
    }

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditStoreImages", "Store", FormMethod.Post, new { enctype = "multipart/form-data" }))
     { %>
  <div style="width: 900px; float: left; margin: 20px 0px 0px 20px">
    <div style="float: left; width: 790px; height: auto; margin-left: 23px; padding: 10px;">
      <h2 style="font-size: 12px">
        Firma Görsellerini Ekleyin
      </h2>
      <span style="font-size: 12px">Her bir kategori için maksimum 3 görsel ekleyebilirsiniz</span>
      <div style="height: 1px; width: 766px; margin-bottom: 20px">
      </div>
      <table style="width: 760px; height: auto; float: left">
        <% foreach (var item in Model.StoreDealerItems)
           { %>
        <tr>
          <td style="width: 80px; height: 30px; font-size: 12px;">
            <%:item.DealerName%>
          </td>
          <td align="center" style="width: 30px;">
            :
          </td>
          <td>
            <div style="float: left; margin-right: 40px;">
              <input name="#<%:item.StoreDealerId %>" type="file" class="fileUpload" />
            </div>
            <div style="float: left; margin-right: 40px;">
              <input name="#<%:item.StoreDealerId %>" type="file" class="fileUpload" />
            </div>
            <div style="float: left;">
              <input name="#<%:item.StoreDealerId %>" type="file" class="fileUpload" />
            </div>
          </td>
        </tr>
        <% } %>
        <tr>
          <td colspan="3" style="padding-top: 10px;">
            <div style="margin-right: 38px; width: 753px; text-align: right;">
              <button type="submit">
                Ekle</button>
            </div>
            <div style="border: 1px solid #b2e0e5; height: 1px; width: 766px; margin-top: 20px;
              float: left;">
            </div>
          </td>
        </tr>
        <tr>
          <td colspan="3">
            <div id="divPictureList">
              <%= Html.RenderHtmlPartial("StoreImages")%>
            </div>
          </td>
        </tr>
      </table>
      <input type="hidden" id="storeId" name="storeId" value="<%:this.RouteData.Values["id"] %>" />
    </div>
  </div>
  <% } %>
  <%} %>
</asp:Content>
