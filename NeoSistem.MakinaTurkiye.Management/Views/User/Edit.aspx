<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Users.UserFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Makinaturkiye | Kullanıcı Ekle
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () { $('.date-pick').datepicker(); $(".hover").hover(function () { $(this).addClass("ui-state-hover"); }, function () { $(this).removeClass("ui-state-hover"); }); });
    </script>
    <% Html.EnableClientValidation();%>
    <%using (Html.BeginForm("Edit", "User", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>
    <%Html.ValidationSummary();%>

    <% using (Html.BeginPanel())
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
</asp:Content>
