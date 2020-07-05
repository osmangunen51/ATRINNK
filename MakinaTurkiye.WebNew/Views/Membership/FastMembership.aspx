﻿<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.MemberShip.MTMembershipFormModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="https://www.makinaturkiye.com/uyelik/hizliuyelik/uyeliktipi-0" />
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>


    <style type="text/css">
        #-xcaptcha-image { border: 0; }

        #-xcaptcha-refresh { width: 27px; height: 20px; margin: 0; padding: 0; border: 0; background: transparent url(/Content/V2/images/refresh.gif) no-repeat center top; text-indent: -1000em; cursor: pointer; /* hand-shaped cursor */ cursor: hand; /* for IE 5.x */ }
    </style>
    <script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row sign-in-container">
        <div class="col-md-5 col-sm-12 pr">
            <%if (!string.IsNullOrEmpty(Model.ErrorMessage))
                {%>
            <div class="alert alert-danger mt-alert-danger" role="alert" id="MembershipError">
                <%:Model.ErrorMessage %>
            </div>
            <% } %>

            <div class="panel panel-default membership-panel">
                <div class="panel-heading">
                    <div class="row">
                        <span class="pull-left panel-heading-left">Üye Ol
                        </span>
                        <small class="pull-right panel-heading-right">Zaten Üye misin?<br />
                            <a href="/uyelik/kullanicigirisi">Giriş Yap</a>
                        </small>
                    </div>
                </div>
                <div class="panel-body">
                    <% using (Html.BeginForm("FastMemberShip", "membership", FormMethod.Post, new { @id = "register-form" }))
                        {%>
        
                    <%=Html.RenderHtmlPartial("_RegisterForm", Model) %>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
