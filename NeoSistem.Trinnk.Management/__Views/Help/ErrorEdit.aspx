<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.WebSiteErrorCreateModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
        <% string title = "  Site Sorun Düzenle";
        if (Request.QueryString["type"] != null)
        {
            title = "Site Öneri Düzenle";
        }
            %> 
    <%:title %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<%--      <div style="margin-left:20px">
      <%
          foreach (ModelState modelState in ViewData.ModelState.Values) {
              foreach (ModelError error in modelState.Errors) {
                 %>
      <p style="color:#d50606; font-size:16px; ">* <%:error.ErrorMessage %></p>
              <%}
          }
          %>
          </div>--%>
      <%using (Html.BeginForm())
          { %>
               <%Html.RenderPartial("_ErrorFormModel", Model); %>
      <%} %>

</asp:Content>

