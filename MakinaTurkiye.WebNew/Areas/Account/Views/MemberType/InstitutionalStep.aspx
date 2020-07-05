<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>



<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .page-header {
            margin: 10px 0 20px;
        }

        label .required {
            font-weight: bold;
            color: #de1b1b;
        }
    </style>
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
            <h4 class="mt0 text-info">Kurumsal Üyelik
            </h4>
        </div>
    </div>
    <div class="row">
          <div class="col-md-12 col-sm-12">
			 <div class="col-md-12 col-sm-12">
                  <div class="well store-panel-container">
                    <h4>Kişisel Bilgiler</h4>
                      <%using (Html.BeginForm("InstitutionalStep", "MemberType", FormMethod.Post, new { id = "formFastMembership", @class = "form-horizontal", @role = "form" }))
                        { %>
                    <div class="form-group">
                        <label for="inputPassword3" class="col-sm-3 control-label">
                            Yetkili Ünvanı
                        </label>
                        <div class="col-sm-6">
                         <%:Html.DropDownListFor(model => model.MembershipModel.StoreAuthorizedTitleType,
                                        Model.MembershipModel.StoreAuthorizedTitleTypeItems,
                                        new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
                        </div>
                    </div>

                           <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                            <button class="btn btn-primary" data-rel="form-submit" type="submit">
                                Devam Et
                            </button>
                            </div>
                        </div>
                      <%} %> 
                  </div>
			</div>
        </div>
    </div>
</asp:Content>

