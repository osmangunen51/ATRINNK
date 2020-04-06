<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        function DeleteVideo(id) {
            $.ajax({
                url: '/Advert/DeleteVideo',
                type: 'delete',
                data: { index: id },
                success: function (data) {
                    $('#divVideoList').html(data);
                },
                error: function (x, l, e) {
                    // alert(x.responseText);
                }
            });
        }
        function nextStatu(e) {
            $('.loading').show();
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
             Video Ekle
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">
               <div class="loading">Loading&#8230;</div>
            <div>
                <div class="well store-panel-container">
                    <div>
                        <div>
                            <div id="divVideoList">
                                <%=Html.RenderHtmlPartial("VideoList", Model.VideoItems) %>
                                <span class="clearfix"></span>
                                <hr>
                            </div>
                            <%using (Html.BeginForm("Video", "Advert", FormMethod.Post, new { enctype = "multipart/form-data", @class = "form-horizontal", role = "form" }))
                              { %>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Video Başlığı
                                </label>
                                <div class="col-sm-6">
                                    <%:Html.TextBox("VideoTitle", "", new { size = "20", @class = "form-control" })%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Video:
                                </label>
                                <div class="col-sm-6">
                                    <input type="file" name="Video" value="" class="fileUp" />
                                </div>
                                <div class="col-sm-offet-3 col-sm-9">
                                    <p class="help-block">
                                        Varsa ürün videosu ekleyiniz.
                                    </p>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-9 btn-group">
                                    <input type="submit" data-rel="form-submit" class="btn btn-default" onclick="nextStatu(this);" value="Ekle" />
                                    <input type="submit" data-rel="form-submit" class="btn btn-primary" onclick="nextStatu(this);" value="Devam" />
                                    <div id="loaderDiv" style="width: auto; height: 20px; display: none; font-size: 12px; margin-top: 8px;">
                                        <img src="../../../../Content/V2/images/loading.gif" width="30" alt="" />&nbsp; İşleminiz gerçekleştiriliyor, lütfen bekleyiniz.
                                    </div>
                                </div>
                            </div>
                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
