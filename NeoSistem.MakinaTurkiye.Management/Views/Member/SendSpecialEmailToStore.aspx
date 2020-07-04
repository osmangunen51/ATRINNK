﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreSpecialMailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Firma Özel Mail Gönder
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div style="float: left; width: 800px; margin-top: 10px;">

       <input id="activeText" type="hidden" value="" />
    <% using (Html.BeginForm("SendSpecialEmailToStore","Member",FormMethod.Post,new {@enctype="multipart/form-data" })) {%>
        <%: Html.ValidationSummary(true) %>
        <%:Html.HiddenFor(x=>x.MemberID) %>
        <fieldset>
            <legend>Özel Mail Gönder</legend>
            <%if(!string.IsNullOrEmpty(Model.Message)){ %>
            <div style="margin-top:20px">
                <p style="font-size:17px;"><%:Model.Message %></p>
            </div>
            <%} %>
            <div class="editor-label" style="margin:20px;">
            <span style="color:#868686; font-family:Arial; font-size:14px;">
            Email 
              <br />
              </span>
            </div>
            <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.StoreMail, new { style = "width: 300px;border: solid 1px #bababa;" })%>
           
            </div>
              <div class="editor-field" style="margin-left:20px;">
             <span style="color:#868686; font-family:Arial; font-size:14px;">
            Konu 
              <br />
              </span>
                <%: Html.TextBoxFor(model => model.Subject, new { style = "width: 300px;border: solid 1px #bababa;" })%>
               
            </div>
            
         <div class="editor-field" style="margin-left:20px;">
             <span style="color:#868686; font-family:Arial; font-size:14px;">
            İçerik
              <br />
              </span>
                <%: Html.TextAreaFor(model => model.Content, new { style = "width: 300px;border: solid 1px #bababa;"})%>
               
            </div>
          <div class="editor-field" style="margin-left:20px;">
             <span style="color:#868686; font-family:Arial; font-size:14px;">
            Yeni Dosya 
              <br />
              </span>
              <input type="file" name="Files" multiple="multiple" />
              <input type="checkbox" name="FileSave" value="1" /> Dosyayı Kaydet
               
            </div>
                      <div class="editor-field" style="margin-left:20px;">
             <span style="color:#868686; font-family:Arial; font-size:14px;">
            Varolan Dosyalar
              <br />
              </span>
            
               <%foreach (var item in Model.Files.ToList())
                   {%>
                          <input type="checkbox" name="defaultFile" value="<%:item.Value %>" /><%:item.Text %>
                   <%} %>
               
            </div>
          
            <p>
                <input type="submit" value="Gönder" />
            </p>
        </fieldset>

    <% } %>

    <div>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <%: Html.ActionLink("Geri", "Index") %>
    </div>
     <script type="text/javascript" defer="defer">
    
         var editor = CKEDITOR.replace('Content', { toolbar: 'webtool', allowedContent: true });
         CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
  
  </script>
    </div>
   
</asp:Content>
