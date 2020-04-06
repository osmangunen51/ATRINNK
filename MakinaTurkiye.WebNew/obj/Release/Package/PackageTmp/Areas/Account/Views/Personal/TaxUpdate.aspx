<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Personal.TaxUpdateViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vergi Dairesi/No Güncelle-Makine Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <input id="OldMemberEmail" type="hidden" value="<%=AuthenticationUser.Membership.MemberEmail %>" />
    <input id="OldMemberPassword" type="hidden" value="<%=AuthenticationUser.Membership.MemberPassword %>" />

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>
                Vergi Dairesi/No Güncelle
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well store-panel-container col-xs-12">
                    <%if (ViewBag.opr == true)
                        { %>
                    <div class="alert alert-success alert-dismissable" style="font-size: 15px;">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            &times;
                        </button>
                        Bilgileriniz başarıyla güncellenmiştir.
                    </div>
                    <%} %>
                    <%using (Html.BeginForm("TaxUpdate", "Personal", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
                        { %>
                    <%:Html.HiddenFor(x=>x.StoreMainPartyId) %>
                    <div class="form-group ">
                        <label class="col-sm-3 control-label">
                            Vergi Dairesi
             
                        </label>
                        <div class="col-sm-6 col-md-4">
                            <%:Html.TextBoxFor(model => model.TaxOffice, new {@class="form-control" ,size = "30" })%>
                        </div>
                        <div class="col-md-3">
                            <label class="checkbox-inline"><%:Html.CheckBoxFor(model => model.TaxOfficeShow, new {@class = "" })%>Profilimde Göster</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Vergi No
                        </label>
                        <div class="col-sm-6 col-md-4">
                            <%:Html.TextBoxFor(model => model.TaxNumber, new { @class = "form-control", size = "30" })%>
                        </div>
                        <div class="col-md-3">
                            <label class="checkbox-inline"><%:Html.CheckBoxFor(model => model.TaxNumberShow, new {@class = "" })%>Profilimde Göster</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Mersis No
                        </label>
                        <div class="col-xs-11 col-sm-6 col-md-4">
                            <%:Html.TextBoxFor(model => model.MersisNo, new { @class="form-control",size = "30"})%>
                        </div>
                        <div class="col-md-3">
                            <label class="checkbox-inline"><%:Html.CheckBoxFor(model => model.MersisNoShow, new {@class = "" })%>Profilimde Göster</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Ticaret Sicil No
                        </label>
                        <div class="col-xs-11 col-sm-6 col-md-4">
                            <%:Html.TextBoxFor(model => model.TradeRegistrNo, new { @class="form-control",size = "30"})%>
                        </div>
                        <div class="col-md-3">
                            <label class="checkbox-inline"><%:Html.CheckBoxFor(model => model.TradeRegistrNoShow, new {@class = "" })%>Profilimde Göster</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <button type="submit" class="btn btn-primary">
                                Değişiklikleri
                  Kaydet
               
                            </button>
                        </div>
                    </div>
                    <%} %>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
