<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductUpdateVideoModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteVideo(id) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/Advert/DeleteProductVideo',
                    data: { videoId: id },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {

                        if (data) {
                            $('#video-' + id).hide();
                        }
                    }
                });
            }

        }

        function ShowLoader() {
            $("#loaderDiv").show();
        }
        $('.open-popup-video').magnificPopup({
            type: 'inline',
            midClick: true // allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source.
        });


    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>

        <div class="col-md-12">
            <h4 class="mt0 text-info"><%:Model.ProductName %> Videolar
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt4 col-xs-12" style="background: #fff;">
                <div class="row">
                    <div class="col-md-6">
                        <%if (TempData["SuccessMessage"] != null)
                            {%>
                        <div class="alert alert-success" role="alert" style="margin-top: 10px;">
                            <%:TempData["SuccessMessage"] %>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <% } %>
                        <%if (TempData["ErrorMessage"] != null)
                            {%>
                        <div class="alert alert-danger" role="alert" style="margin-top: 10px;">
                            <%:TempData["ErrorMessage"] %>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <% } %>
                        <%using (Html.BeginForm("ProductVideos", "Advert", FormMethod.Post, new { @class = "form-horizontal", @enctype = "multipart/form-data" }))
                            {%>

                        <%:Html.HiddenFor(x=>x.ProductId) %>
                        <div class="form-group">
                            <label class="col-md-3">Video Başlık</label>
                            <div class="col-md-9">
                                <%:Html.TextBoxFor(x => x.Title, new { @class = "form-control", @placeholder = "Video Başlık" }) %>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3">Video</label>
                            <div class="col-md-9">
                                <input type="file" name="file" multiple />
                                <p style="color: #a30000; font-family: 15px;"><%:Html.ValidationMessage("Video") %>*</p>
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

                        <div class="row" id="images"></div>
                        <%} %>
                    </div>
                    <div class="col-md-6">
                        <%if (Model.Videos.Count > 0)
                            {%>
                        <table class="table table-hover">
                            <thead>
                                <tr>

                                    <th>Video Başlık</th>
                                    <th>Video</th>
                                    <th>Süre</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <%foreach (var item in Model.Videos)
                                {%>

                            <tbody>
                                <tr id="video-<%:item.VideoId %>">
                                    <td><%:item.VideoTitle %></td>
                                    <td>
                                        <a href="<%:item.VideoUrl %>" target="_blank">
                                            <img style="height:70px;" src="<%:item.VideoPicturePath %>" class="img-responsive" />
                                        </a>
                                    </td>
                                    <td><%:item.VideoMinute  %> :<%:item.VideoSecond %> </td>
                                    <td><a style="cursor: pointer" onclick="DeleteVideo(<%:item.VideoId %>)"><i style="color: #1051e9; font-size: 16px;" class="fa fa-trash"></i></a></td>
                                </tr>
                            </tbody>

                            <%} %>
                        </table>
                        <% }
                            else
                            {%>
                        <div class="col-md-12">
                            <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                                Eklediğiniz herhangi video bulunmamaktadır. Müşterilerinize daha detaylı bilgiler vermek için ürün videosu eklemeniz ilanlara dönüş yapılmasına katkı sağlar.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                            </div>
                        </div>
                        <% } %>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="CertificateEdit" tabindex="-1"
        role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close"
                        data-dismiss="modal">
                        <span aria-hidden="true">&times;
                        </span><span class="sr-only">Kapat</span></button>
                    <h4 class="modal-title" id="H1">Sertifika Düzenle</h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnCatologId" name="hdnSortId" class="form-control" />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3">
                                Katolog Adı
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="CertificateName" class="form-control" />
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-md-3">
                                Sıra
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="Order" class="form-control" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="UpdateCertificate()" class="btn btn-default">
                        Güncelle</button>
                    <button type="button"
                        class="btn btn-default" data-dismiss="modal">
                        Vazgeç</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
