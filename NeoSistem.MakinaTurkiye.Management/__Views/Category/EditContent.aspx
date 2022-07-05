<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Entities.Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Yardım İçeriği
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style=" margin-left:20px; margin-top:25px;">Yardım İçeriği</h2>

    <% using (Html.BeginForm(new {style=" margin-left:20px; margin-top:25px;" }))
       {%>
        <%: Html.ValidationSummary(true)%>
        
        <fieldset>
            <legend><%:ViewData["text"]%></legend>
            
            <div class="editor-field">
                <%: Html.TextAreaFor(model => model.Content, new { @class = "ckeditor", id = "editor", rows = "10" })%>
                <%: Html.ValidationMessageFor(model => model.Content)%>
            </div>
            
              <p>
            <button type="submit" value="Kaydet" style="width: 70px; height: 35px">
                Kaydet</button>
            <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Category/AllIndexHelp'">
                İptal
            </button>
        </p>
        </fieldset>

    <% } %>

   
     <script type="text/javascript" defer="defer">
         var editor = CKEDITOR.replace('Content', { toolbar: 'webtool' });

         
         CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
        
          

           
  </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>

