<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage< NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreNews.MTCreateStoreNewForm>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="/Content/v2/assets/js/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">
        function Validation() {
            var content = CKEDITOR.instances['Content'].getData();
            if (content.length > 160) {
                return true;
            }
            else {
                alert("Lütfen haber içeriğini 150 karakterden fazla giriniz")
                return false;
            }

        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info"><%:Model.PageTitle %> Düzenle
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div class="well  col-xs-12" style="background: #fff;">
                <div class="col-md-2" style="margin-bottom: 10px;">
                    <a href="/account/storenew/index?newType=<%:Model.NewType %>" class="btn btn-info">Tümünü Gör <i class="fa  fa-list"></i></a>
                </div>
                <div class="col-md-12">
                    <%if (TempData["success"] != null)
                        {   %>
                    <div class="alert alert-success" role="alert">
                        <%:Model.PageTitle %> düzenlenmişir. En kısa sürede editörlerimiz tarafından incelenip onaylandıktan sonra aktif olacaktır.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>

                    <%} %>
                    <%using (Html.BeginForm("Update", "StoreNew", FormMethod.Post, new { @class = "form-horizontal", @enctype = "multipart/form-data" }))
                        {%>
                    <%:Html.HiddenFor(x=>x.StoreNewId) %>
                    <%:Html.HiddenFor(x=>x.NewType) %>
                    <%:Html.HiddenFor(x=>x.StoreMainPartyId) %>
                    <div class="form-group">
                        <div class="col-md-12">
                            <label>Fotoğraf</label>
                            <input type="file" name="image" />
                            <br />
                            <img src="<%:Model.ImagePath %>" class="img-responsive" />
                        </div>

                    </div>
                    <div class="form-group">

                        <div class="col-md-12">
                            <label>Başlık</label>
                            <%:Html.TextBoxFor(x => x.Title, new { @class = "form-control" }) %>
                            <p style="color: #ff0000"><%:Html.ValidationMessageFor(x=>x.Title) %></p>
                        </div>
                    </div>
                    <div class="form-group">

                        <div class="col-md-12">
                            <label>İçerik</label>
                            <%:Html.TextAreaFor(x => x.Content,new { style = "width:640px; height:355px;"}) %>
                            <p style="color: #ff0000"><%:Html.ValidationMessageFor(x=>x.Content)%></p>
                        </div>
                    </div>

                    <div class="form-group">

                        <div class="col-md-3">
                            <button class="btn btn-success" onclick="return Validation();">Kaydet</button>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('Content', { toolbar: 'webtool' });
        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
    </script>
</asp:Content>
