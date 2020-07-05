﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
        <script type="text/javascript" src="/Content/v2/assets/js/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Tanıtım Bilgilerini Güncelle
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div>

                <%using (Html.BeginForm("UpdateStore", "Store", FormMethod.Post, new { role = "form", @class = "form-horizontal" }))
                    {%>
                <div class="well store-panel-container col-xs-12">

                    <%if (ViewData["storeUrlCheck"] == "false")
                        {%>

                    <%} %>

                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Firma Ünvanı
                           
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextBoxFor(model => model.Store.StoreName,
                        new Dictionary<string, object> { { "class", "mt-form-control" }, { "data-validation-engine", "validate[required]" },{"placeholder","Demo Makina Sanayi Ltd."} })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Firma Kısa Adı
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextBoxFor(model => model.Store.StoreShortName,
                        new Dictionary<string, object> { { "class", "mt-form-control" }, { "data-validation-engine", "validate[required]" },{"placeholder","Demo Makina"} })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Firma Web Adresi
                           
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextBoxFor(model => model.Store.StoreWeb,
                    new Dictionary<string, object> { { "class", "mt-form-control" }, { "data-validation-engine", "validate[required,custom[url]]" } })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Kuruluş Yılı
                           
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextBoxFor(model => Model.Store.StoreEstablishmentDate, 
                        new Dictionary<string, object> { { "class", "mt-form-control" }, { "data-validation-engine", "validate[required]" } })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Faaliyet Tipi
                           
                        </label>
                        <div class="col-sm-9 col-md-7 col-lg-7">
                            <% foreach (var item in Model.ActivityItems)
                                { %>
                            <% bool checkActivity = Model.StoreActivityItems.Any(c => c.ActivityTypeId == item.ActivityTypeId);  %>
                            <div class="col-sm-6 col-md-4">
                                <div class="checkbox">
                                    <label>
                                        <%:Html.CheckBox("ActivityName", checkActivity,
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
                            Şirket Sermayesi
                           
                        </label>
                        <div class="col-sm-4">
                            <%=Html.DropDownListFor(model => model.Store.StoreCapital, Model.StoreCapitalItems,
                        new Dictionary<string, object> { { "class", "selectpicker" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Çalışan Sayısı
                           
                        </label>
                        <div class="col-sm-4">
                            <%=Html.DropDownListFor(model => model.Store.StoreEmployeesCount, Model.EmployeesCountItems,
                                              new Dictionary<string, object> { { "class", "selectpicker" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" }, { "size","10"} })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Yıllık Ciro
                           
                        </label>
                        <div class="col-sm-4">
                            <%=Html.DropDownListFor(model => model.Store.StoreEndorsement, Model.StoreEndorsementItems, 
                                              new Dictionary<string, object> { { "class", "selectpicker" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Şirket Türü
                           
                        </label>
                        <div class="col-sm-4">
                            <%=Html.DropDownListFor(model => model.Store.StoreType, Model.StoreTypeItems,
                                              new Dictionary<string, object> { { "class", "selectpicker" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Firma Kısa Bilgi
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextAreaFor(model => model.Store.StoreAbout, new { maxLength = 200, @class = "mt-form-control" })%>
                        </div>
                    </div>
      <%--              <div class="form-group">
                        <label class="col-sm-4 control-label">
                            Firma Anasayfa Bilgi
                           
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextAreaFor(model => model.Store.StoreProfileHomeDescription, new { maxLength = 200, @class = "ck-editor", id = "ck-editor" })%>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <div class="col-sm-offset-4 col-sm-2">
                            <button class="btn btn-sign-up" data-rel="form-submit" type="submit">
                                 Kaydet
                               
                            </button>
                        </div>
                    </div>

                </div>
            <%} %>
        </div>
    </div>
    </div>

    <script type="text/javascript">
       CKEDITOR.replace('ck-editor', {
           toolbar: [
               //{ name: 'document', items: ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'] },	// Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
               //['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'],			// Defines toolbar group without name.
               '/',																					// Line break - next group will be placed in new line.
               { name: 'basicstyles', items: ['Bold'] },
           ],
         
       });
    </script>
</asp:Content>
