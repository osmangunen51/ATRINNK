﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Users.UserFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Makinaturkiye | Kullanıcı Ekle
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
    $(function () { $('.date-pick').datepicker(); $(".hover").hover(function () { $(this).addClass("ui-state-hover"); }, function () { $(this).removeClass("ui-state-hover"); }); });
  </script>
  <% Html.EnableClientValidation();%>
  <%using ( Html.BeginForm("Create", "User", FormMethod.Post, new { enctype = "multipart/form-data" }) )
    {%>
  <%Html.ValidationSummary();%>
  <% using ( Html.BeginPanel() )
     { %>
    <div style="float:left; width:40%; margin-right:10px;">
            <%=Html.RenderHtmlPartial("_UserForm",Model.UserModel) %>
        </div>
    <div style="float:left; width:40%">
        <%=Html.RenderHtmlPartial("_UserInformationForm", Model.UserInformationModel) %>
    </div>
    <div style="clear:both"></div>
    <br />
    <button type="submit" style="width: 70px; height: 35px;">
        Kaydet
    </button>
    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/User/Index'">
        İptal
    </button>
  <%} %>
  <%} %>
            <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript" defer="defer">
        CKEDITOR.replace('Signature',
            {
                toolbar: [
                    //{ name: 'document', items: ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'] },	// Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
                    //['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'],			// Defines toolbar group without name.
                    '/',																					// Line break - next group will be placed in new line.
                    { name: 'basicstyles', items: ['Bold', 'NumberedList'] },
                ],
                height: '135px'
            });
    </script>
</asp:Content>
