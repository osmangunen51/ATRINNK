<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>
  <script type="text/javascript">
    $(function () {
      $("#accordion").accordion({ header: "h3" });
      $("#accordionParent").accordion({ header: "h2" });
    });

    $(document).ready(function () {
      $('#Genel').attr('style', 'height: 310px; padding: 0px;');
      $('#Tarihce').attr('style', 'height: 310px; padding: 0px;');
      $('#Kurucu').attr('style', 'height: 310px; padding: 0px;');
      $('#Felsefe').attr('style', 'height: 310px; padding: 0px;');
    });

  </script>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("StoreProfileHomeDescription", "Store", FormMethod.Post))
     { %>
  <div style="width: 600px; float: left; margin: 20px 0px 0px 20px">
    <div id="accordion">
      <div id="divHakkimizda">
        <div style="width: 888px; height: 420px; float: left; padding-bottom: 10px;">
 
            <div>
              <h2>
                <a class="accordionHeader" href="#">Firma Anasayfa Açıklama</a></h2>
              <div id="Genel">
                <%:Html.TextAreaFor(model => model.StoreProfileHomeDescription)%>
              </div>
                <%:Html.HiddenFor(x=>x.MainPartyId) %>
            </div>

        </div>
        <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
          float: left">
          <button type="submit">
            Kaydet</button>
          <button type="reset">
            İptal</button>
        </div>
      </div>
    </div>
  </div>
  <script type="text/javascript" defer="defer">

    var editorGeneralText = CKEDITOR.replace('StoreProfileHomeDescription', { toolbar: 'webtool' });
 

    CKFinder.SetupCKEditor(editorGeneralText, '/Scripts/CKFinder/');
    CKFinder.SetupCKEditor(editorHistoryText, '/Scripts/CKFinder/');
    CKFinder.SetupCKEditor(editorFounderText, '/Scripts/CKFinder/');
    CKFinder.SetupCKEditor(editorPhilosophyText, '/Scripts/CKFinder/');

    function UpdateProductAbout() {
      $.ajax({
        url: '/Store/EditAbout',
        data: { storeId: $('#storeId').val(), GeneralText: editorGeneralText.getData(), HistoryText: editorHistoryText.getData(), FounderText: editorFounderText.getData(), PhilosophyText: editorPhilosophyText.getData() },
        type: 'post',
        success: function (data) {
          alert('Hhakkımızda bilgileri güncellenmiştir.');
        },
        error: function (x, a, r) {
          alert(x.responseText);
        }
      });
    }
   
  </script>
  <% } %>
  <%} %>
</asp:Content>
