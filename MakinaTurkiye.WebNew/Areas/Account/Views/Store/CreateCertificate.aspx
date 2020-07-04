<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.CreateStoreCertificateModel>" %>


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
        $("#CertificateTypeId").change(function () {
            var val = $("#CertificateTypeId").val();
            
        });

        function ShowLoader() {
            $("#loaderDiv").show();
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
            <h4 class="mt0 text-info">Firma Sertifikaları
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="row">
                    <div class="col-sm-12 col-md-12">

                        <div class="well well-mt4 col-xs-12" style="background: #fff;">
                            <div class="col-md-10">
                                <div class="alert alert-warning" role="alert">
                                    Eklediğiniz sertifikalarınız firma anasayfanızda müşterilerinize ulaştırılacaktır.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                                </div>
                            </div>
                            <div class="col-md-2" style="margin-bottom: 10px;">
                                <a href="/account/store/certificate" class="btn btn-info">Tümünü Gör <i class="fa  fa-list"></i></a>
                            </div>
                            <div class="col-md-8">

                                <%using (Html.BeginForm("CreateCertificate", "Store", FormMethod.Post, new { @class = "form-horizontal", @enctype = "multipart/form-data" }))
                                    {%>
                                <div class="form-group">
                                    <label class="col-md-3">İsim</label>
                                    <div class="col-md-9">
                                        <%:Html.TextBoxFor(x => x.CeritificateName, new { @class = "form-control", @placeholder = "Sertifika İsmi" }) %>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">Sertifika Tipi</label>
                                    <div class="col-md-9">
                                        <%:Html.DropDownListFor(x=>x.CertificateTypeId, Model.CertificateTypes, new {@class="selectpicker" , @title = "Seçiniz"}) %>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">Sertifika Resimleri</label>
                                    <div class="col-md-9">
                                        <input type="file" name="file" multiple />
                                        <p style="color: #a30000; font-family: 15px;"><%:Html.ValidationMessage("CertificateImages") %>*</p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">Sıra</label>
                                    <div class="col-md-9">
                                        <%:Html.TextBoxFor(x => x.Order, new { @class = "form-control", @placeholder = "Gösterim Sırası" }) %>
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
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
