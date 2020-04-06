﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreImage.StoreImagesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Anasayfa Slider Resimleri Ekle/Güncelle-Makina Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .imgAdded { margin-top: 10px; margin-left: 10px; }
    </style>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript">
        function readURL(input) {
            for (var i = 0; i < input.files.length; i++) {
                var reader = new FileReader();
                $("#images").html("");
                reader.onload = function (e) {

                    $("#images").append('<img style="width:800px; height:200px;" class="imgAdded" src="' + e.target.result
                        + '" alt="your image" />');

                }
                reader.readAsDataURL(input.files[i]);
            }
        }
        $(document).ready(function () {
            $("#sortable").sortable();
            $("#sortable").disableSelection();
            $("#imgInp").change(function () {
                readURL(this);

            });
        })
        function DeletePicture(ID) {
            if (confirm('Resimi Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/Store/DeleteImage',
                    data: { id: ID },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + ID).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }
    </script>
    <style type="text/css">
        #imagesAll { }
            #imagesAll li { float: left; list-style-type: none; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Firma Anasayfa Slider Resimleri Ekle/Güncelle
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>

                <%using (Html.BeginForm("CreateStoreSliderImage", "Store", FormMethod.Post, new { enctype = "multipart/form-data", role = "form", @class = "form-horizontal" }))
                    {%>
                <input id="hiddenDelete" name="Delete" value="false" type="hidden" />
                <div class="well col-xs-12" style="background-color: #fff!important; border: 1px solid #ccc;">
                    <%if (Model.StoreImageItems.Count > 0)
                        { %>
                    <div class="col-md-12">
                        <span style="float: left; font-size: 20px; font-weight: 800;">Görseller</span>
                        <a href="" style="float: right;">Sırala</a>
                    </div>
                    <%} %>

                    <ul id="imagesAll">
                        <%foreach (var item in Model.StoreImageItems)
                            {%>
                        <li class="col-md-4" id="row<%:item.ImageId %>">
                            <p>
                                <img src="<%:item.ImagePath %>" align="top" style="width: 100%; height: 150px; float: left;" /><div onclick="DeletePicture(<%:item.ImageId %>)" style="cursor: pointer; text-decoration: underline; color: #333">Kaldır</div>
                            </p>
                        </li>
                        <%} %>
                    </ul>
                    <%if (ViewData["message"] != null)
                        {%>
                    <div class="alert alert-warning">
                        <strong>Hata!</strong><%:ViewData["message"] %>
                    </div>
                    <% } %>
                    <div class="row">
                        <div class="col-md-12">
                            <%--            <img alt="" data-rel="addlogo" style="display: none" src="http://www.placehold.it/400x300&amp;text=logo"
                                class="img-thumbnail" />
                            <% if (!string.IsNullOrWhiteSpace(Model.StoreBanner))
                               { %>
                            <img class="pull-left img-thumbnail"  src="<%:ImageHelpers.GetStoreBanner(Model.Store.MainPartyId,Model.StoreBanner) %>" />
                            <% }
                               else
                               { %>
                            <img alt="" src="https://dummyimage.com/400x300/efefef/000000.jpg&text=logo" class="img-thumbnail" />
                            <% } %>--%>
                        </div>
                        <div class="col-md-12">
                            <div class="hidden-sm hidden-xs alert alert-info">
                                <span class="glyphicon glyphicon-info-sign"></span>Önerilen boyutları minumum <b>700x300</b> Slider fotoğrafı yüklemeniz kişiselleştirilmiş firma sayfası deneyimi sunmaktadır.
                           
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-left: 15px;">


                            <div class="form-group">
                                <input type="file" class="form-control" id="imgInp" name="images[]" style="border: 0px!important; background-color: transparent; width: 200px;" multiple />

                                <p class="help-block">
                                    Eklemek istediğiniz slider fotoğrafılarını 'Dosyaları Seç' butonuna tıklayarak bulun ve seçin. Seçtiğiniz slider fotoğraflarını aşağıdan kontrol edebilirsiniz.
                               
                                </p>
                            </div>
                            <%--  <a onclick="DeleteLogo()" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash">
                            </span>&nbsp;Logoyu  Sil </a>--%>
                            <button id="Insert" type="submit" name="submit_image" class="btn btn-sm btn-default">
                                Resimleri Yükle
                           
                            </button>
                            &nbsp; Maksimum dosya boyutu: 250 Kb
                           
                            <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px;">
                                <img src="/Content/images/load.gif" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
                            </div>
                            <div class="row" id="images"></div>
                        </div>

                    </div>
                </div>

                <%} %>
            </div>
        </div>
    </div>
</asp:Content>
