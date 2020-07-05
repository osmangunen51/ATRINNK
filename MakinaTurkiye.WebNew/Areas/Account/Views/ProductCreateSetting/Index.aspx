﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.ProductCreateSettingModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("#PropertyId").change(function () {
                var id = $(this).val();
                $.ajax({

                    url: '/Account/ProductCreateSetting/GetConstants',
                    type: 'GET',
                    data: {
                        'propertieId': id
                    },
                    dataType: 'json',
                    success: function (data) {
                        $("#valueWrapper").show();
                        $("#value").html(data.content);
                    },
                    error: function (request, error) {
                        alert("Request: " + JSON.stringify(request));
                    }
                });
            });
        });


        function CreateProductCreateSetting() {
            var propertieId = $("#PropertyId").val();
            if (propertieId != "0") {
                $.ajax({

                    url: '/Account/ProductCreateSetting/Create',
                    type: 'Post',
                    data: {
                        'storeMainPartyId': $("#StoreMainPartyId").val(),
                        'propertieId': propertieId,
                        'value': $("#value").val()

                },
                    dataType: 'json',
                    success: function (data) {
                        if (data) {
                            alert("Başarılı Şekil Eklenmiştir");
                        }
                    },
                    error: function (request, error) {
                        alert("Request: " + JSON.stringify(request));
                    }
                });
            }

            else {
                alert("Lütfen özellik seçiniz.");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>

    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt">
                <div class="form-top">
                    <div class="pull-left">
                        <div class="form-header-text">İlan Ekleme Ayarları</div>
                    </div>
                    <div class="pull-right">
                        <a data-toggle="modal" class="btn btn-success" style="cursor: pointer;" data-target="#Create">Yeni Ekle <i class="fa fa-add"></i></a>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div class="modal fade" id="Create" tabindex="-1" role="dialog" aria-labelledby="Create" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document" id="modelContent">
            <div class="modal-content">
                <div class="modal-header" style="height: 60px; border: 0px!important;">
                    <h3 class="modal-title" style="float: left;" id="exampleModalLabel">Çalışma Saatleri</h3>
                    <button type="button" style="float: right;" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">

                    <%:Html.HiddenFor(x=>x.StoreMainPartyId) %>
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label class="col-md-3 contorl-label">Özellik :</label>
                            <div class="col-md-9">
                                <%:Html.DropDownListFor(x=>x.PropertyId, Model.Properties, new {@class="form-control" }) %>
                            </div>
                        </div>
                        <div class="form-group" id="valueWrapper" style="display: none;">
                            <label class="col-md-3 contorl-label">Değer :</label>
                            <div class="col-md-9">
                                <select name="value" id="value" class="form-control"></select>
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="form-group">

                            <div style="float: right;">
                                <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px; float: left;">
                                    <img src="/Content/images/load.gif" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
                                </div>
                                <button type="submit" id="buttonPay" onclick="CreateProductCreateSetting()" style="float: right;" class="btn background-mt-btn ">Kaydet <span class="glyphicon glyphicon-chevron-right"></span></button>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
