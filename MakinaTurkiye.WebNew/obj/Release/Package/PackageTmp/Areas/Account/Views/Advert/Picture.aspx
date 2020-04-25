﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="/Content/jquery.Filer/jquery.filer.min.js" type="text/javascript"></script>
    <!-- Styles -->
    <link href="/Content/jquery.Filer/jquery.filer.css" rel="stylesheet" />
    <link href="/Content/jquery.Filer/themes/jquery.filer-dragdropbox-theme.css" rel="stylesheet" />
    <script src="/Content/file-upload/fileinput.min.js" type="text/javascript"></script>
    <script src="/Content/file-upload/locales/tr.js" type="text/javascript"></script>
    <link href="/Content/file-upload/components.css" rel="stylesheet" />
    <script type="text/html">

        

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

            <div class="row">
            <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
                <div class="col-md-12">
        <h4 class="mt0 text-info">
           İlan Ekle
        </h4>
    </div>
       </div>
    <div class="row">

        <%using (Html.BeginForm("Picture", "Advert", FormMethod.Post, new { enctype = "multipart/form-data" }))
          { %>
        <div class="col-sm-12 col-md-12">
            <div>
           
                <div class="well store-panel-container clearfix">
                    <div>
                        <div>
                            <div class="form-group">
                                <div class="btn-group">
                                    <input type="file" class="file-input-advanced" id="files" name="files[]" title="Dosyaları seçiniz"  data-show-upload="false" data-show-caption="true" data-msg-placeholder="Select {files} for upload..."  multiple="multiple" />
                                    <div id="loaderDiv" style="float: right; width: auto; height: 20px; display: none; font-size: 12px; margin-top: 8px;">
                                        <img src="../../../../Content/V2/images/loading.gif" width="30" alt="" />&nbsp;
                                                    İşleminiz gerçekleştiriliyor, lütfen bekleyiniz.
                                    </div>
                                </div>
                            </div>
                            <div id="divPictureList">
                                <div class="row no-gutters clearfix">
                                    <%= Html.RenderHtmlPartial("PictureList", Model.PictureList) %>
                                </div>
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
        <script>

            $(document).ready(function () {
                $('[data-rel="deleteImage"]').click(function () {
                    var productId = $(this).attr('data-productid');
                    var picturePath = $(this).attr('data-picturepath');
                    //DeleteImage(productId, productId, picturePath);
                });

                $('[data-rel="mainImage"]').click(function () {
                    var productId = $(this).attr('data-productid');
                    var picturePath = $(this).attr('data-picturepath');
                    //mainImage(productId, picturePath);
                });

                $('[data-rel="form-submit"]').removeClass('disabled');
            });

            function mainImage(productID, picturePath) {
                $.ajax({
                    url: '/Account/ilan/mainImage',
                    type: 'post',
                    data: { productID: productID, picturePath: picturePath },
                    success: function (data) {
                        $('#divPictureList .row').html(data);
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }

            function DeleteImage(index, productID, picturePath) {
                $.ajax({
                    url: '/Account/ilan/DeleteImage',
                    type: 'post',
                    data: { index: index, productID: productID, picturePath: picturePath, horizontal: false },
                    success: function (data) {
                        $('#divPictureList .row').html(data);
                    },
                    error: function (x, l, e) {
                        // alert(x.responseText);
                    }
                });
            }

            function DeletePicture(id) {
                $.ajax({
                    url: '/Account/Advert/DeletePicture',
                    type: 'post',
                    data: { index: id, horizontal: false },
                    success: function (data) {
                        $('#divPictureList .row').html(data);
                    },
                    error: function (x, l, e) {
                        // alert(x.responseText);
                    }
                });
            }

            function nextStatu(e) {
                $('#loaderDiv').show();
            }

            $(document).on('invalid-form.validate', 'form', function () {
                $('[data-rel="form-submit"]').removeClass('disabled');
            });

            $(document).on('submit', 'form', function () {
                $('[data-rel="form-submit"]').addClass('disabled');
            });

            $('.file-input-advanced').fileinput({
                browseLabel: 'Dosyadan Seç',
                browseClass: 'btn btn-primary',
                removeLabel: '',
                uploadLabel: '',
                language: "tr",
                dropZoneEnabled: true,
                maxFileCount: 20,
                allowedFileExtensions: ["jpg", "gif", "png","jpeg"],
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

        </script>
        <% } %>
    </div>
</asp:Content>
