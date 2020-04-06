<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NewsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Create
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <% using (Html.BeginForm("Create", "News", FormMethod.Post, new { enctype = "multipart/form-data" }))
     { %>
  <table border="0" cellpadding="5" cellspacing="0">
    <tr>
      <td colspan="3">
        <div style="border-bottom: dashed 1px #DDD; width: 99%; text-align: right; padding-bottom: 5px">
          <button type="submit">
            Kaydet</button>
          <button type="reset">
            İptal</button></div>
      </td>
    </tr>
    <tr>
      <td valign="top">
        <%: Html.LabelFor(m => m.NewsTitle)%>
      </td>
      <td>
        :
      </td>
      <td>
        <%: Html.TextBoxFor(m => m.NewsTitle, new { style = "width: 400px" })%>
      </td>
    </tr>
    <tr>
      <td valign="top">
        <%: Html.LabelFor(m => m.NewsDate)%>
      </td>
      <td>
        :
      </td>
      <td>
        <%: Html.TextBox("NewsDate", Model.NewsDate.ToString("dd.MM.yyyy"))%>
      </td>
    </tr>
    <tr>
      <td valign="top">
        <%: Html.LabelFor(m => m.NewsPicturePath)%>
      </td>
      <td>
        :
      </td>
      <td>
        <%: Html.FileUploadFor(m => m.NewsPicturePath)%>
      </td>
    </tr>
    <tr>
      <td valign="top">
        <%: Html.LabelFor(m => m.Active)%>
      </td>
      <td>
        :
      </td>
      <td>
        <%: Html.CheckBoxFor(m => m.Active)%>
      </td>
    </tr>
    <tr>
      <td valign="top">
        <%: Html.LabelFor(m => m.NewsText)%>
      </td>
      <td valign="top">
        :
      </td>
      <td>
        <%: Html.TextAreaFor(m => m.NewsText)%>
      </td>
    </tr>
    <tr>
      <td colspan="3">
        <div style="border-top: dashed 1px #DDD; width: 99%; text-align: right; padding-top: 5px">
          <button type="submit">
            Kaydet</button>
          <button type="reset">
            İptal</button></div>
      </td>
    </tr>
    <tr>
  </table>
  <% } %>
  <script type="text/javascript" defer="defer">
    var editor = CKEDITOR.replace('NewsText', { toolbar: 'webtool' });
    CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
  </script>
</asp:Content>
