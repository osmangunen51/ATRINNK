<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Views/Shared/Main.Master"
    Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Bulletins.BulletinMemberCreateModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="<%:AppSettings.SiteUrl %>"uyelik/bulten" />
    <meta name="description" content="makinaturkiye.com e bülten üyelik sayfasında istediğiniz makina sektörü hakkında paylaşacağımız mailleri alabilirsiniz" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
                  <% string classStyle = "col-md-8";
                      if (Model.HaveMemberShip == true)
                      {
                          classStyle = "col-md-12";
                      }
                    %>
        <div class="<%:classStyle %> pr">
            <div>
      
                <div class="well well-mt2" style="background-color:#fff!important;">
                    <h1 style="font-size: 24px!important;">Bülten Üyeliğine Kayıt</h1>
                    <p>E-posta ile makinaturkiye.com sektörel alanlarda bilgilendirileceğim ücretsiz e-bültenine abone olmak istiyorum. Genel hükümler ve koşullar, Bilgi Güvenliği ve Kişisel Verilerin Korunması Kanunu yönergelerini kabul ediyorum.</p>
                    <div class="row">

                        <%if ( TempData["Success"]=="true") {%>
                        <div class="alert alert-success" role="alert" style="font-size:15px;">
                            Bülten üyeliğiniz gerçekleşmiştir.
                        </div>
                       
                        <% } %>
                       <%if (ViewBag.ErrorMessage != null) {%>
                                <div class="alert alert-danger" role="alert">
                            <%=ViewBag.ErrorMessage %>
                        </div>
                        <% }%>
                        <%using (Html.BeginForm("BulletinRegister", "MemberShip", FormMethod.Post, new { @class = "form-horizontal",@role = "form", @id = "formFastMembership"})) {%>
                            <div class="form-group">
                                <label class="col-md-3 control-label">E-mail</label>
                                <div class="col-md-6">
                                    <%:Html.TextBoxFor(x=>x.Email,
                                  new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "type", "email" },
                                  { "data-validation-engine", "validate[required,custom[email]]" },
                                  { "data-errormessage-value-missing", "Email alanı zorunludur!" },
                                  { "data-errormessage-custom-error", "Örneğin: info@makinaturkiye.com" },
                                  { "placeholder", "örn: info@makinaturkiye.com" } , {"data-rel", "memberEmail"}}) %>
                                    <p style="color:#bd0606">
                                    <%:Html.ValidationMessageFor(x=>x.Email) %>
                                    </p>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">İsim</label>
                                <div class="col-md-6">
                                    <%:Html.TextBoxFor(x=>x.MemberName, new Dictionary<string, object> { { "class", "form-control" },
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen isminizi giriniz" }}) %>
                                     <p style="color:#bd0606">
                                     <%:Html.ValidationMessageFor(x=>x.MemberName) %>
                                         </p>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Soyisim</label>
                                <div class="col-md-6">
                                <%:Html.TextBoxFor(x=>x.MemberSurname,new Dictionary<string, object> { { "class", "form-control" },
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen soyisminizi giriniz" }}) %>
                                              <p style="color:#bd0606">
                                     <%:Html.ValidationMessageFor(x=>x.MemberSurname) %>
                                                  </p>
                                </div>
                            </div>
                                      <div class="col-sm-offset-3 col-sm-9">
                                <%foreach (var item in Model.SectorCategories)
                                    {%>
                                        <div class="col-md-6">
                                            <input type="checkbox" name="SectorCategories[]" value="<%:item.Value %>"  />
                                            <label><%:item.Text %></label>
                                        </div>
                                         
                                   <% } %>
                         
                                        
                            </div>
                        <div class="col-sm-offset-3">
                                          <p style="color:#bd0606">   
                                     <%:Html.ValidationMessageFor(x=>x.SectorCategories) %>
                                              </p>
                        </div>
                            <div class="col-sm-offset-3 col-sm-9">
                                <input type="submit" value="Kayıt Ol" style="background: #fc8120;border: 0px; color:#fff;" class="btn btn-default" />
                            </div>
                  
                        <% } %>
                    </div>
                </div>
            </div>
        </div>
        <%if (Model.HaveMemberShip != true) {%>
               <%CompanyDemandMembership model1 = new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
        <% } %>

        
    </div>
</asp:Content>
