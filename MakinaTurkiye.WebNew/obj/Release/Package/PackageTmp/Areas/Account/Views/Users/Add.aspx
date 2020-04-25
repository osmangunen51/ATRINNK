<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Users.MTUserFormModelView>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        table tr td, th { font-size: 15px; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

        <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt">
                <div class="form-top">
                    <div class="pull-left">
                        <div class="form-header-text">Yetkili Kullanıcı Ekle</div>
                    </div>
                    <div class="pull-right">
                        <a href="/account/Users" class="btn btn-info">Tümünü Gör <i class="fa  fa-list"></i></a>
                    </div>
                </div>

                <div style="clear: both"></div>
                <div class="form-content">
                    <%if (TempData["success"] != null)
                        {
                            if (TempData["success"].ToBoolean() == true)
                            {%>
                    <div class="alert alert-success" role="alert">
                        Kullanıcı başarılı bir şekilde eklenmiştir. Kullanıcı bilgileri eklediğiniz kullanıcının mail adresine gönderilmiştir.
                    </div>
                    <%}%>
                    <%} %>

                    <%using (Html.BeginForm("Add", "Users", FormMethod.Post, new { @class = "form-horizontal", @id = "add-user" }))
                        {
                    %>
                    <%=Html.RenderHtmlPartial("_UserFormModel", Model.MTUserFormModel) %>
                    <%
                        }%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
