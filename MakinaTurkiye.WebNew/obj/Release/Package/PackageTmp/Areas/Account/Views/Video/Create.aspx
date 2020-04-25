<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Videos.MTVideoCreateModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info"><span class="text-primary glyphicon glyphicon-cog"></span>Firma Tanıtım Videosu Ekle
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well store-panel-container col-xs-12" style="background: #fff;">
                <div class="col-md-10">
                </div>
                <div class="col-md-2" style="margin-bottom: 10px;">
                    <a href="/account/video" class="btn btn-info">Tümünü Gör <i class="fa  fa-list"></i></a>
                </div>
                <div class="col-md-12">
                    <%if (TempData["success"] != null)
                        {%>

                    <div class="alert alert-success" role="alert">
                        Firma tanıtım videonuz eklenmiştir.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>

                    <%} %>
                    <%using (Html.BeginForm("Create", "Video", FormMethod.Post, new { @class = "form-horizontal", @enctype = "multipart/form-data", id = "add-video-form" }))
                        {%>
                    <div class="form-group">
                        <div class="col-md-12">
                            <label>Başlık</label>
                            <%:Html.TextBoxFor(x=>x.VideoTitle, new { @class = "form-control", @placeholder = "Başlık.." }) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <label>Video</label>
                            <input type="file" id="video" name="video" />

                        </div>

                    </div>

                    <div class="form-group">
                        <div class="col-md-3">
                            <button class="btn btn-success" type="submit" id="btnVideoAdd">Ekle</button>
                            <div id="loaderDiv" style="width: auto; height: 20px; display: none; font-size: 12px; margin-top: 8px;">
                                <img src="../../../../Content/V2/images/loading.gif" width="30" alt="" />&nbsp; İşleminiz gerçekleştiriliyor, lütfen bekleyiniz.
                            </div>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
