<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="/Content/jquery.Filer/jquery.filer.min.js" type="text/javascript"></script>
    <!-- Styles -->
    <link href="/Content/jquery.Filer/jquery.filer.css" rel="stylesheet" />
    <link href="/Content/jquery.Filer/themes/jquery.filer-dragdropbox-theme.css" rel="stylesheet" />
    <script src="/Content/file-upload/fileinput.min.js" type="text/javascript"></script>
    <script src="/Content/file-upload/locales/tr.js" type="text/javascript"></script>
    <link href="/Content/file-upload/components.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;Ürün Katolog Ekle
            </h4>
        </div>
    </div>
    <div class="row">

        <%using (Html.BeginForm("ProductCatolog", "Advert", FormMethod.Post, new { enctype = "multipart/form-data" }))
            { %>
        <div class="col-sm-12 col-md-12">
            <div>

                <div class="well store-panel-container clearfix">
                    <h4>Ürün Katoloğu Ekle</h4>
                    <div>

                        <div>
                            <div class="form-group">
                                <div class="file-loading">
                                    <input id="input-res-3" class="file-input-advanced" name="input-res-3[]" type="file" multiple>
                                </div>
                            </div>
                            <div id="divPictureList">
                            </div>

                            <div class="text-right mtb20">
                                <input type="submit" data-rel="form-submit" onclick="nextStatu(this);" class="btn btn-default" value="Ekle" />
                                <input type="submit" data-rel="form-submit" onclick="nextStatu(this);" class="btn btn-primary" value="Devam" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">

            $(document).on('ready', function () {
                $(".file-input-advanced").fileinput({
                    browseClass: "btn btn-primary btn-block",
                    showCaption: false,
                    showRemove: false,
                    showUpload: false,
                    language: "tr",
                    dropZoneEnabled: true,
                    maxFileCount: 3,
                    allowedFileExtensions: ["docx", "doc", "pdf"],
                    showDrag: true,
                    browseIcon: '<i class="glyphicon glyphicon-plus position-left"></i> ',
                    uploadClass: 'btn btn-primary btn-icon',
                    uploadIcon: '<i class="icon-file-upload"></i> ',
                    removeClass: 'btn btn-danger btn-icon',
                    removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
                    initialCaption: "Dosya Seçilmedi",
                    layoutTemplates: {
                        caption: '<div tabindex="-1" class="form-control file-caption {class}">\n' + '<span class="icon-file-plus kv-caption-icon"></span><div class="file-caption-name"></div>\n' + '</div>',
                        main1: "{preview}" +
                            "<div class='input-group {class}'>\n" +
                            "   <div class='input-group-btn'>\n" +
                            "       {browse}\n" +
                            "   </div>\n" +
                            "   {caption}\n" +
                            "   <div class='input-group-btn'>\n" +
                            "       {remove}\n" +
                            "   </div>\n" +
                            "</div>"
                    }
                });
            });

        </script>
        <% } %>
    </div>
</asp:Content>
