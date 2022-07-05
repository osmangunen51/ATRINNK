<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Entities.Dictionary>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script src="http: //code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
   
  </script>
  <input id="activeText" type="hidden" value="" />
   <%using (Html.BeginForm("Update", "Dictionary", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Ekle</legend>
            <div class="editor-label" style="margin:20px;">
            <span style="color:#868686; font-family:Arial; font-size:14px;">
              Kısa Adı 
              <br />
              </span>
            </div>
            <%:Html.HiddenFor(x=>x.ID) %>
            <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.DicShortName, new { style = "width: 300px;border: solid 1px #bababa;" })%>
            </div>
            <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
                Açıklama
            </div>
               <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextAreaFor(model => model.DicDescription, new { style = "width: 300px;border: solid 1px #bababa;" })%>
            </div>
            <p>
                <input type="submit" value="Ekle" />
            </p>
        </fieldset>

    <% } %>
         <script type="text/javascript" defer="defer">
           var editor = CKEDITOR.replace('MessagesMTPropertie', { toolbar: 'webtool' });
           CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
  </script>
</asp:Content>

