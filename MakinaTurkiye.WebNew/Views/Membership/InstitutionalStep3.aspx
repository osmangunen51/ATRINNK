﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        //dropdown selected value control
        function ifSelectNotEmpty(field, rules, i, options) {
            if (field.val() == "0") {
                return "* Bu alan zorunludur";
            }
        }
        var isEmail = true;
        $(document).ready(function () {
            $('#formInstitutionalStep3').validationEngine();
            $('[data-rel="memberEmail"]').keyup(function () {
                var isValidate = $(this).validationEngine('validate');
                if (!isValidate) {
                    $.ajax({
                        url: '/Membership/CheckMail',
                        data: { email: $(this).val() },
                        success: function (data) {
                            isEmail = (data == "true" ? true : false);
                            if (!isEmail) {
                                setTimeout(function () {
                                    $('[data-rel="memberEmail').validationEngine('showPrompt', 'Bu E-Posta adresi kullanılmaktadır. Lütfen Tekrar Deneyiniz.', 'red')
                                }, 1000);
                            }
                        }
                    });
                }
            });
            $('[data-rel="form-submit').click(function () {
                if (isEmail) {
                    $('#formInstitutionalStep3').submit();
                    $('[data-rel="email-wrapper').hide();
                }
                else {
                    $('[data-rel="formInstitutionalStep3').focus();
                    $('[data-rel="email-wrapper').show();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <h4 class="mt0">
                <span class="glyphicon glyphicon-user" style="padding-right"></span>Firma Bilgilerini Ekleyin
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7 col-md-8 pr">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a href="/">Anasayfa </a></li>
                        <li class="active">Kurumsal Üyelik 3. Adım </li>
                    </ol>
                </div>
            </div>
            <div class="well well-mt4">
                <div class="alert alert-danger alert-dismissable" data-rel="email-wrapper" style="display: none">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                       
                    </button>
                    <strong>Hata! </strong>Yazmış olduğunuz email kullanılmaktadır.
                   
                </div>
                <%using (Html.BeginForm("KurumsalUyelik/Adim-3", "Uyelik", FormMethod.Post, new { id = "formInstitutionalStep3", @class = "form-horizontal" }))
                  {%>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreName)%>
                    </label>
                    <div class="col-sm-4">
                        <%:Html.TextBoxFor(model => model.MembershipModel.StoreName,
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" } })%>
                    </div>
                </div>
                      <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreName)%>
                    </label>
             
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreWeb)%>
                    </label>
                    <div class="col-sm-4">
                        <%:Html.TextBoxFor(model => model.MembershipModel.StoreWeb,
                        new Dictionary<string, object> { { "class", "form-control" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Kuruluş Yılı
                   
                    </label>
                    <div class="col-sm-4">
                        <%:Html.TextBoxFor(model => Model.MembershipModel.StoreEstablishmentDate, 
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Faaliyet Tipi
                   
                    </label>
                    <div class="col-sm-9 col-md-7 col-lg-7">
                        <% foreach (var item in Model.ActivityItems)
                           { %>
                        <% bool checkActivity = Model.MembershipModel.ActivityName.Any(c => c == item.ActivityTypeId.ToString());  %>
                        <div class="col-sm-6 col-md-4">
                            <div class="checkbox">
                                <label>
                                    <%:Html.CheckBox("MembershipModel.ActivityName", checkActivity,
                    new Dictionary<string, object> { {"name", "group[group]"},{ "value", item.ActivityTypeId }, { "data-validation-engine", "validate[minCheckbox[1]]" } })%>
                                    <%= item.ActivityName %>
                                </label>
                            </div>
                        </div>
                        <% } %>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreCapital)%>
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreCapital, Model.MembershipModel.StoreCapitalItems,
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEmployeesCount)%>
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreEmployeesCount, Model.MembershipModel.EmployeesCountItems,
                                              new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEndorsement)%>
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreEndorsement, Model.MembershipModel.StoreEndorsementItems, 
                                              new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreType)%>
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreType, Model.MembershipModel.StoreTypeItems,
                                              new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                      <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.TaxOffice)%>
                    </label>
                    <div class="col-sm-4">
                        <%=Html.TextBoxFor(model => model.MembershipModel.TaxOffice, new{@class="form-control"})%>
                    </div>
                </div>
                       <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.TaxNumber)%>
                    </label>
                    <div class="col-sm-4">
                        <%=Html.TextBoxFor(model => model.MembershipModel.TaxNumber, new{@class="form-control"})%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Firma Kısa Bilgi
                   
                    </label>
                    <div class="col-sm-5">
                        <%:Html.TextAreaFor(model => model.MembershipModel.StoreAbout, new {  maxLength=200 , @class="form-control",@rows="6",@cols="12" })%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <a href="/Account/MemberType/InstitutionalStep1">
                            <button class="btn btn-primary" type="button">
                                Önceki Adım
                           
                            </button>
                        </a>
                        <button class="btn btn-success" data-rel="form-submit" type="submit">
                            Sonraki Adım
                       
                        </button>
                    </div>
                </div>
                <% } %>
            </div>
        </div>
        <%CompanyDemandMembership model1 = new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
    </div>
</asp:Content>
