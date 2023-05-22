<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.HelpModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Satış Yardım Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div style="float: left; width: 800px; margin-top: 10px;">
      <div style="margin-left:20px">
      <%
          foreach (ModelState modelState in ViewData.ModelState.Values) {
              foreach (ModelError error in modelState.Errors) {
                 %>
      <p style="color:#d50606; font-size:16px; ">* <%:error.ErrorMessage %></p>
              <%}
          }
          %>
          </div>
      <%using (Html.BeginForm())
          { %>
               <%Html.RenderPartial("_HelpForm", Model); %>
      <%} %>
 </div>
         <script type="text/javascript" defer="defer">
   
         var editor = CKEDITOR.replace('Content', { toolbar: 'webtool', allowedContent: true });
         CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');

  </script>
</asp:Content>

