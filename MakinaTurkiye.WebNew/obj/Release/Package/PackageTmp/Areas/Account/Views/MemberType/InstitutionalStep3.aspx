﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
        <script type="text/javascript">
        //dropdown selected value control
        function ifSelectNotEmpty(field, rules, i, options) {
            if (field.val() == "0") {
                return "* Bu alan zorunludur";
            }
        }
        var isUserName = true;
            $(document).ready(function () {
                $("#MembershipModel_StoreUrlName").keyup(function (event) {
                    $(this).val(ToUrl($(this).val()));
                });

            $("input[id='MembershipModel_StoreShortName']").change(function () {
                var value = ToUrl($(this).val());
                $("#MembershipModel_StoreUrlName").val(value);
                $("#showStoreUrl").html(value);
            });
            $('#formInstitutionalStep3').validationEngine();
            $('[data-rel="storeUrlName"]').keyup(function () {
                $.ajax({
                    url: '/MemberType/CheckUserName',
                    data: { username: $(this).val() },
                    success: function (data) {
                        isUserName = (data == "true" ? true : false);
                        if (!isUserName) {
                            setTimeout(function () {
                                $('[data-rel="storeUrlName').validationEngine('showPrompt', 'Bu kullanıcı adı kullanılmaktadır. Lütfen Başka Bir Kullanıcı Adı Seçiniz.', 'red')
                            }, 1000);
                        }
                    }
                });
                
            });
        })
        function UrlKontrol()
        {
            var urlName = $("#MembershipModel_StoreUrlName").val();
            if(isUserName)
            {
                if (urlName.length <= 50 && urlName.length > 3)
            {
                    return true;
            
            }
            else
            {
                setTimeout(function () {
                    $('[data-rel="storeUrlName').validationEngine('showPrompt', 'Kullanıcı adınız en az 3 en fazla 50 karakter olmalıdır.', 'red')
                }, 1000);
                $("#MembershipModel_StoreUrlName").focus();
                return false;
            }
            }
            else{
                setTimeout(function () {
                    $('[data-rel="storeUrlName').validationEngine('showPrompt', 'Bu kullanıcı adı kullanılmaktadır. Lütfen Yeni Kullanıcı Adı  Deneyiniz.', 'red')
                }, 1000)
                return false;
            }
                
        }
        function checkUsername(username) {
            
            var pattern = /^[A-Za-z0-9]+(?:[][A-Za-z0-9]+)*$/;
            if (pattern.test(username)) {
                return true;
            } else {
                return false;
            }
        }
        function ToUrl(value)
        {
            value = value.toLowerCase();
            value = value.replace("ğ", "g").replace("ü", "u").replace("ş", "s");
            value = value.replace("ı", "i").replace("ö", "o").replace("ç", "c");
            value = value.replace(".", "");
            value = value.replace("Ğ", "G").replace("Ü", "U").replace("Ş", "S");
            value = value.replace("İ", "I").replace("Ö", "O").replace("Ç", "C").replace(" ", "");
            value = value.replace("&", "");
            value = value.replace("%", "");
            value = value.replace("+", "");
           
            value = value.replace("–", "").replace("---", "");
            value = value.replace("--", "");
            value = value.replace("--", "");
            value = value.replace("---", "");
            value = value.replace("³", "3");
            value = value.replace("²", "2");
            value = value.replace(" ", "");
            value = value.replace(" ", "");
            value = value.replace(" ", "");
            value = value.replace(" ", "");
            value = value.replace("-", "");
            
            return value;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%
                NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel lefMenu = (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel)ViewData["leftMenu"];
            %>
            <%= Html.RenderHtmlPartial("LeftMenu",lefMenu)%>
        </div>
        <div class="col-md-12">
    <h3>Kurumsal Üyelik</h3>
        </div>
    </div>
    <div class="row">
          <div class="col-md-12 col-sm-12">
			 <div class="col-md-12 col-sm-12">
                  <div class="well store-panel-container">
                    <h4>Kurumsal Üyelik 3. adım </h4>
                      <% if (ViewData["usernameNull"]=="true")
                         {%>
                                   <div class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            Firma kulanıcı adı boş geçilemez.<br />
                    </div>  
                         <%}
                         else { 
                          if (ViewData["storeUrlError"]=="true"){ %>
                       <div class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            Kullanıcı adınızda özel ve türkçe karakterler bulunamaz.<br />
                    </div>
                      <%}
                         }
                          if (ViewData["storeUrlCheck"]=="false")
                          {%>
                                       <div class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                          Bu firma kullanıcı adı mevcut.Lütfen başka bir kullanıcı adı seçiniz.<br />
                         
                    </div>  
                          <%}
                         %>
                      <%using (Html.BeginForm("InstitutionalStep3", "MemberType", FormMethod.Post, new { id = "formInstitutionalStep3", @class = "form-horizontal", @role = "form" }))
                        { %>
                              <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreName)%>(*)
                    </label>
                    <div class="col-sm-4">
                        <%:Html.TextBoxFor(model => model.MembershipModel.StoreName,
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" },{"placeholder","Anexxa Bilişim Teknolojileri ltd. şirketi"}  })%>
                    </div>
                </div>
                           <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreShortName)%>(*)
                    </label>
                    <div class="col-sm-4">
                        <%:Html.TextBoxFor(model => model.MembershipModel.StoreShortName,
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" },{"placeholder","Örn:Anexxa Bilişim"}})%>
                    </div>
                </div>
                  <div class="form-group">
                 
                        <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreUrlName)%>(*)
                    </label>
                          <div class="col-sm-4">
                        <%:Html.TextBoxFor(model => model.MembershipModel.StoreUrlName,
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" },{"data-rel","storeUrlName"} })%><br /><small>Profiliniz <font style="color:#0094ff; font-size:11px" >makinaturkiye.com/<span id="showStoreUrl">firmaurl</span></font> şeklinde gösterilecektir</small>
                    </div>
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
                        Faaliyet Tipi(*)
                   
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
                        <%:Html.LabelFor(model => model.MembershipModel.StoreCapital)%>(*)
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreCapital, Model.MembershipModel.StoreCapitalItems,
                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEmployeesCount)%>(*)
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreEmployeesCount, Model.MembershipModel.EmployeesCountItems,
                                              new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreEndorsement)%>(*)
                    </label>
                    <div class="col-sm-4">
                        <%=Html.DropDownListFor(model => model.MembershipModel.StoreEndorsement, Model.MembershipModel.StoreEndorsementItems, 
                                              new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        <%:Html.LabelFor(model => model.MembershipModel.StoreType)%>(*)
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
                        <a href="/Uyelik/KurumsalUyelik/Adim-2">
                            <button class="btn btn-primary" type="button">
                                Önceki Adım
                           
                            </button>
                        </a>
                        <button class="btn btn-success" data-rel="form-submit" onclick="return UrlKontrol();" type="submit">
                            Sonraki Adım
                       
                        </button>
                    </div>
                </div>
                      <%} %> 
                  </div>
			</div>
        </div>
    </div>


</asp:Content>


