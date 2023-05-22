<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.WebSiteErrorCreateModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
            <% string title = "  Site Sorun Ekle";
        if (Request.QueryString["type"] != null)
        {
            title = "Site Öneri Ekle";
        }
            %> 
    <%:title %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


      <%using (Html.BeginForm("ErrorCreate","Help",FormMethod.Post,new {@enctype="multipart/form-data" }))
          { %>
               <%Html.RenderPartial("_ErrorFormModel", Model); %>
      <%} %>
 </div>
</asp:Content>

