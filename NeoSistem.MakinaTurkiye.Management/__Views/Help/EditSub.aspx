<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Entities.HelpSubcategory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreateSub
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <div style="margin-left:30px;margin-top:30px; width:60%;">
        <h3>YARDIM ALTKATEGORİSİ EKLE</h3>
             <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
           
            <div class="editor-label">
                <%: Html.Label("Altkategori Adı :") %>
                <br /><br />
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.SubCategoryName) %>
                <%: Html.ValidationMessageFor(model => model.SubCategoryName) %>
            </div>
             <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
            <div class="editor-label">
                <%: Html.Label("Yardım İçeriği :") %>
                <br /><br />
                
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(model =>model.Content, new { @class = "ckeditor", id = "editor2", rows = "10" })%>
                <%: Html.ValidationMessageFor(model => model.Content) %>
            </div>
             <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
            <div class="editor-label">
                <%: Html.Label("Kategori :") %>
                <br /><br />
            </div>
            <div class="editor-field">
                
                <%= Html.DropDownListFor(model=>model.YKID, new SelectList((IEnumerable)ViewData["Kategori"],"YKID","KategoriAd",Model.HelpCategory.KategoriAd)) %>
                <%: Html.ValidationMessageFor(model => model.YKID) %>
            </div>
             <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
          <p>
            <button type="submit" value="Sil" style="width: 70px; height: 35px">
                Kaydet</button>
            <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/BrowseSub'">
                İptal
            </button>
        </p>
    

        </div>
    <% } %>

    <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('Content', { toolbar: 'webtool' });
        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
  </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          $('#NewsDate').datepicker();
      });
  </script>
</asp:Content>

