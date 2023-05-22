<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script type="text/javascript">
    function DeleteStoreBrand(Id) {
      if (confirm('Markayı silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Store/DeleteStoreBrand',
          type: 'post',
          data: { StoreBrandId: Id, storeId: $('#storeId').val() },
          success: function (data) {
            alert('Silmek istediğiniz marka başarıyla silinmiştir.');
            $('#divStoreBrandItems').html(data);
          },
          error: function (x, l, e) {
            alert(x.responseText);
          }
        });
      }
    }
  </script>
  <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditBrand", "Store", FormMethod.Post, new { enctype = "multipart/form-data" }))
     { %>
  <div style="width: 900px; float: left; margin: 20px 0px 0px 20px">
    <div style="width: 500px; height: 380px; float: left; margin-top: 20px; margin-left: 20px;">
      <div style="width: 600px; height: 130px;">
        <div style="float: left; width: auto; height: auto;">
          <div style="width: 100px; float: left; text-align: right; padding-top: 3px; font-size: 12px;">
            Marka Adı :
          </div>
          <div style="width: 220px; height: auto; float: left; margin-left: 10px;">
            <div class="textBig">
              <%= Html.TextBox("StoreBrandName")%>
            </div>
          </div>
        </div>
        <div style="float: left; width: auto; height: auto;">
          <div style="width: 100px; float: left; text-align: right; padding-top: 6px; font-size: 12px;">
            Marka Açıklama :
          </div>
          <div style="width: 220px; height: auto; float: left; margin-left: 10px;">
            <div class="textBigArea" style="margin-top: 10px;">
              <%= Html.TextArea("BrandDescription")%>
            </div>
          </div>
        </div>
      </div>
      <div style="width: 100%; height: 170px; float: left;">
        <div style="width: 110px; float: left; height: 170px; text-align: center">
        </div>
        <div style="width: 300px; float: left; height: 170px">
          <span style="font-weight: bold; font-size: 12px;">Bayisi olduğunuz marka logosunu
            ekleyiniz.</span>
          <br />
          <span style="font-size: 12px;">Logonuzu yüklemeniz aynı zamanda sonuçların listelendiği
            sayfalarada tanınmanızı kolaylaştıracaktır</span>
          <br />
          <br />
          <input name="upFileBrandImage" type="file" class="fileUpload" />
          <br />
          <span style="color: #ababab; font-size: 12px">Eklemek istediğiniz logonun yerini "Göz
            Ata" tıklayarak bulun ve seçin eklenen logonuzu görünümünü pencereden kontrol edin
          </span>
          <br />
          <span style="font-weight: bold; color: #ababab; font-size: 12px">Şu anda logoya ulaşamıyorsanız
            devam edin</span>
          <br />
          <br />
        </div>
        <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
          float: left">
          <button type="submit">
            Kaydet</button>
          <button type="reset">
            İptal</button>
        </div>
      </div>
      <input type="hidden" id="storeId" name="storeId" value="<%:this.RouteData.Values["id"] %>" />
    </div>
    <div id="divStoreBrandItems" style="width: 300px; height: 270px; float: left; margin-top: 20px;">
      <%=Html.RenderHtmlPartial("StoreBrand", Model.StoreBrandItems)%>
    </div>
  </div>
  <% } %>
  <%} %>
</asp:Content>
