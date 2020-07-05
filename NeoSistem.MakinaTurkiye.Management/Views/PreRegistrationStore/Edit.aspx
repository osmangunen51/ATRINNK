<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.PreRegistrainFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Ön Kayıt
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%if (TempData["success"] != null)
        {%>
    <p style="font-size:15px;">Firma Eklenmiştir</p>
    <%} %>
    <%using (Html.BeginForm("Edit", "PreRegistrationStore", FormMethod.Post))
        { %>
    <%:Html.HiddenFor(x=>x.Id) %>
    <%Html.RenderPartial("_FormModel", Model); %>
    <%} %>
</asp:Content>

