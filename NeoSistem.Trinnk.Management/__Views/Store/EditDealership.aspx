<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>
  <script type="text/javascript">
    function DeleteDealerBrand(Id) {
      if (confirm('Bayisi olduğunuz markayı silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Store/DeleteDealerBrand',
          type: 'post',
          data: { DealerBrandId: Id, storeId: $('#storeId').val() },
          success: function (data) {
            alert('Silmek istediğiniz marka başarıyla silinmiştir.');
            $('#divDealerBrandItems').html(data);
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
  <% using (Html.BeginForm("EditDealership", "Store", FormMethod.Post, new { enctype = "multipart/form-data" }))
     { %>
  <div style="width: 900px; float: left; margin: 20px 0px 0px 20px">
    <div style="width: 500px; height: auto; float: left; margin-top: 20px;">
      <div style="width: 100%; height: 30px;">
        <div style="width: 100px; float: left; text-align: right; padding-top: 3px; font-size: 12px">
          Marka Adı :
        </div>
        <div style="width: 190px; height: auto; float: left; margin-left: 10px;">
          <div class="textBig">
            <%= Html.TextBox("BrandName")%>
          </div>
        </div>
      </div>
      <div style="width: 100%; height: 210px;">
        <div style="width: 110px; float: left; height: 210px; text-align: center">
        
        </div>
        <div style="width: 300px; float: left; height: 210px">
          <span style="font-weight: bold; font-size: 12px;">Bayisi olduğunuz marka logosunu
            ekleyiniz.</span>
          <br />
          <span style="font-size: 12px;">Logonuzu yüklemeniz aynı zamanda sonuçların listelendiği
            sayfalarada tanınmanızı kolaylaştıracaktır</span>
          <br />
          <br />
          <input name="upFileBrandImage" type="file" class="fileUpload" />
          <br />
          <span style="color: #ababab; font-size: 12px;">Eklemek istediğiniz logonun yerini
            "Göz Ata" tıklayarak bulun ve seçin eklenen logonuzu görünümünü pencereden kontrol
            edin </span>
          <br />
          <span style="font-weight: bold; color: #ababab; font-size: 12px;">Şu anda logoya ulaşamıyorsanız
            devam edin</span>
          <br />
          <br />
        </div>
        <input type="hidden" id="storeId" name="storeId" value="<%:this.RouteData.Values["id"] %>" />
        <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
          float: left">
          <button type="submit">
            Kaydet</button>
          <button type="reset">
            İptal</button>
        </div>
      </div>
    </div>
    <div id="divDealerBrandItems" style="width: 300px; height: 270px; float: left; margin-top: 20px;">
      <%=Html.RenderHtmlPartial("DealerBrand", Model.DealerBrandItems)%>
    </div>
  </div>
  <% } %>
  <%} %>
</asp:Content>
