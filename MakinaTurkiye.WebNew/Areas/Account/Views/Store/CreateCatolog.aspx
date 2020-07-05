<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage< NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel.MTCreateCatologViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function ShowLoader() {
            $("#loaderDiv").show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>Katolog Ekle            </h4>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-8 col-md-9">

            <div class="well well-mt4 col-xs-12" style="background: #fff;">
                <div class="col-md-10">
                    <div class="alert alert-warning" role="alert">
                                                            Eklediğiniz katologlar firma sayfasında katologlar menüsü altnda müşterilerinize ulaştırılacaktır.

                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                </div>
                <div class="col-md-2" style="margin-bottom: 10px;">
                    <a href="/account/store/mycatologlist" class="btn btn-info">Tümünü Gör <i class="fa  fa-list"></i></a>
                </div>
                <div class="col-md-12">

                    <%using (Html.BeginForm("CreateCatolog", "Store", FormMethod.Post, new { @class = "form-horizontal", @enctype = "multipart/form-data" }))
                        {%>
                    <div class="form-group">
                        <label class="col-md-3">Adı</label>
                        <div class="col-md-9">
                            <%:Html.TextBoxFor(x => x.CreateCatologForm.CatologName, new { @class = "form-control", @placeholder = "Örn:2019 Ürün Katoloğu" }) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3">Dosya</label>
                        <div class="col-md-9">
                            <%:Html.FileUploadFor(x => x.CreateCatologForm.FilePaths,new {@multiple="true" }) %>
                            <p style="color: #a30000; font-family: 15px;"><%:Html.ValidationMessage("CreateCatologForm.FilePath") %>*</p>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-3">
                            <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px;">
                                <img src="/Content/images/load.gif" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
                            </div>
                        </div>
                        <div class="col-md-3">
                            <button class="btn btn-success" onclick="ShowLoader()">Ekle</button>
                        </div>
                    </div>
                    <%} %>
                    <%if (TempData["success"]!=null &&  TempData["success"].ToString() == "basarili")
                        {%>
                    <div class="col-md-10">
                        <div class="alert alert-success" role="alert">
                            İşleminiz Başarılı!
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
