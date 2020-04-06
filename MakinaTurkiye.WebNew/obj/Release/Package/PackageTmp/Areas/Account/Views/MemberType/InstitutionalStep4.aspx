<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Bilgileri
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .mTop5 { margin-top: 8px; }

        .form-group { margin-bottom: 10px !important; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.EnableClientValidation(); %>

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%
                NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel lefMenu = (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel)ViewData["leftMenu"];
            %>
            <%= Html.RenderHtmlPartial("LeftMenu",lefMenu)%>
        </div>
        <div class="col-md-12">
            <h3>Firma Bilgileri</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-9 col-sm-12">
            <div class="well store-panel-container">
                <h4>Kurumsal Üyelik 4. adım </h4>
                <%using (Html.BeginForm("InstitutionalStep4", "MemberType", FormMethod.Post, new { enctype = "multipart/form-data", id = "formContent", @class = "form-horizontal" }))
                    {%>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreName)%><font style="padding-left: 5px;">:</font>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%=Model.MembershipModel.StoreName%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreShortName)%><font style="padding-left: 5px;">:</font>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%=Model.MembershipModel.StoreShortName%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Firma Url<font style="padding-left: 5px;">:</font>
                    </label>
                    <div class="col-sm-4 mTop5">
                        http://makinaturkiye.com/<%=Model.MembershipModel.StoreUrlName%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%: Html.LabelFor(model => model.MembershipModel.StoreWeb)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%= Model.MembershipModel.StoreWeb %>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEstablishmentDate)%>:
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%=Model.MembershipModel.StoreEstablishmentDate.ToString()%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Faaliyet Tipi
                    </label>
                    <div class="col-sm-9 col-md-7 col-lg-7 mTop5">
                        <% foreach (var item in Model.MembershipModel.GetActivityNames())
                            { %>

                        <%= item%>,
                      
                     
                        <% } %>
                    </div>
                </div>
                <%--        <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Faaliyet Tipi
                    </label>
                    <div class="col-sm-9 col-md-7 col-lg-7 mTop5">
                        <% foreach (var itemCategory in Model.MembershipModel.GetRelatedCategory())
                           { %>
                        <div class="col-sm-6 col-md-4">
                            <%= itemCategory%>
                        </div>
                        <% } %>
                    </div>
                </div>--%>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreCapital)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%=Model.MembershipModel.StoreCapitalName%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreType)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <% if (Model.MembershipModel.StoreType == 1)
                            { %>
                        Anonim Şirketi
                        <% } %>
                        <%else
                            {%>
                        Limited Şirketi
                        <%} %>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEmployeesCount)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%=Model.MembershipModel.StoreEmployeesCountName%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEndorsement)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%=Model.MembershipModel.StoreEndorsementName%>
                    </div>
                </div>
                <% if (!string.IsNullOrEmpty(Model.MembershipModel.InstitutionalPhoneNumber))
                    { %>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.InstitutionalPhoneNumber)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%= Model.MembershipModel.InstitutionalPhoneCulture + " " + Model.MembershipModel.InstitutionalPhoneAreaCode + " " + Model.MembershipModel.InstitutionalPhoneNumber%>
                    </div>
                </div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.MembershipModel.InstitutionalGSMNumber))
                    { %>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%: Html.LabelFor(model => model.MembershipModel.InstitutionalGSMNumber)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%= Model.MembershipModel.InstitutionalGSMCulture + " " + Model.MembershipModel.InstitutionalGSMAreaCode + " " + Model.MembershipModel.InstitutionalGSMNumber%>
                    </div>
                </div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.MembershipModel.InstitutionalPhoneNumber2))
                    { %>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%: Html.LabelFor(model => model.MembershipModel.InstitutionalPhoneNumber2)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%= Model.MembershipModel.InstitutionalPhoneCulture2 + " " + Model.MembershipModel.InstitutionalPhoneAreaCode2 + " " + Model.MembershipModel.InstitutionalPhoneNumber2%>
                    </div>
                </div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.MembershipModel.InstitutionalGSMNumber2))
                    { %>
                <div class="form-group">
                    <label class="col-sm-4 control-label mTop5">
                        <%: Html.LabelFor(model => model.MembershipModel.InstitutionalGSMNumber2)%>
                    </label>
                    <div class="col-sm-4">
                        <%= Model.MembershipModel.InstitutionalGSMCulture2 + " " + Model.MembershipModel.InstitutionalGSMAreaCode2 + " " + Model.MembershipModel.InstitutionalGSMNumber2%>
                    </div>
                </div>
                <% } %>

                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%: Html.LabelFor(model => model.MembershipModel.StoreAbout)%>
                    </label>
                    <div class="col-sm-4 mTop5">
                        <%= Model.MembershipModel.StoreAbout %>
                    </div>
                </div>
                <div class="form-group">
                    <center>
                        <div class="help-block">
                            Üyelik kaydımı oluşturarak:
                            <br />
                            <a href="#">Üyelik Sözleşmesi </a>Koşulları'nı ve <a href="#">Gizlilik Politikası
                            </a>'nı kabul ediyorum.
                        </div>
                        <div class="col-sm-offset-3 col-sm-9">
                            <a href="/Uyelik/KurumsalUyelik/Adim-3">
                                <button class="btn btn-primary" type="button">
                                    Önceki Adım
                                </button>
                            </a>
                            <button class="btn btn-success" data-rel="form-submit" type="submit">
                                Profilinizi Hemen Tamamlayın
                            </button>
                        </div>
                    </center>
                </div>
                <% } %>
            </div>
        </div>
    </div>
    </div>

</asp:Content>


