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
  <% using (Html.BeginForm("EditAbout", "Store", FormMethod.Post))
     { %>
  <div style="width: 600px; float: left; margin: 20px 0px 0px 20px">
    <div id="accordion">
      <div id="divHakkimizda">
        <div style="width: 888px; height: 420px; float: left; padding-bottom: 10px;">
          <div id="accordionParent">
            <div>
              <h2>
                <a class="accordionHeader" href="#">Genel</a></h2>
              <div id="Genel">
                <%:Html.TextAreaFor(model => model.GeneralText)%>
              </div>
            </div>
            <div>
              <h2>
                <a class="accordionHeader" href="#">Tarihçe</a></h2>
              <div id="Tarihce">
                <%:Html.TextAreaFor(model => model.HistoryText)%>
              </div>
            </div>
            <div>
              <h2>
                <a class="accordionHeader" href="#">Kurucu</a></h2>
              <div id="Kurucu">
                <%:Html.TextAreaFor(model => model.FounderText)%>
              </div>
            </div>
            <div>
              <h2>
                <a class="accordionHeader" href="#">Felsefe</a></h2>
              <div id="Felsefe">
                <%:Html.TextAreaFor(model => model.PhilosophyText)%>
              </div>
            </div>
          </div>
        </div>
        <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
          float: left">
          <button type="button" onclick="UpdateProductAbout();">
            Kaydet</button>
          <button type="reset">
            İptal</button>
        </div>
      </div>
    </div>
  </div>
  <input id="storeId" name="storeId" value="<%:this.RouteData.Values["id"] %>" type="hidden" />
  <script type="text/javascript" defer="defer">

    var editorGeneralText = CKEDITOR.replace('GeneralText', { toolbar: 'webtool' });
    var editorHistoryText = CKEDITOR.replace('HistoryText', { toolbar: 'webtool' });
    var editorFounderText = CKEDITOR.replace('FounderText', { toolbar: 'webtool' });
    var editorPhilosophyText = CKEDITOR.replace('PhilosophyText', { toolbar: 'webtool' });

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
