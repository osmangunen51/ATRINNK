<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.HelpModel>" %>
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
          if (!string.IsNullOrEmpty(ViewBag.Message)) {%>
          <p style="font-size:16px; color:#028f2c"><%:ViewBag.Message %></p>

          <%}
 
          %>
          </div>
      <%using (Html.BeginForm())
          { %>
      <%:Html.HiddenFor(x=>x.ID) %>
               <%Html.RenderPartial("_HelpForm", Model); %>
      <%} %>
 </div>
         <script type="text/javascript" defer="defer">
   
         var editor = CKEDITOR.replace('Content', { toolbar: 'webtool', allowedContent: true });
         CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');

  </script>
</asp:Content>

