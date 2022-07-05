<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreSeoNotificationFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Seo Bildirim Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#RemindDate').datepicker().val();
            CKEDITOR.replace('Text', {
                toolbar: [
                    '/',																					// Line break - next group will be placed in new line.
                    { name: 'basicstyles', items: ['Bold'] },
                ],

            });
                        CKEDITOR.replace('PreviousText', {
                toolbar: [
                    '/',																					// Line break - next group will be placed in new line.
                    { name: 'basicstyles', items: ['Bold'] },
                ],

            });

        });



    </script>
    <style type="text/css">
        .editor-label { padding-top: 5px; }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Create", "StoreSeoNotification", FormMethod.Post, new { @enctype = "multipart/form-data" }))
        { %>
    <%Html.RenderPartial("_StoreNotificationFormModel", Model); %>
    <%} %>
</asp:Content>

