﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Profile.StoreProfileHomeDescriptinModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="/Content/v2/assets/js/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var editorGeneralText = CKEDITOR.replace('StoreProfileDescription', { toolbar: 'webtool' });
            CKFinder.SetupCKEditor(editorGeneralText, '/Scripts/CKFinder/');
        });
        function UpdateProductAbout() {
            $.ajax({
                url: '/Profile/StoreProfileHomeDescription',
                data: { Text: CKEDITOR.instances['StoreProfileDescription'].getData() },
                type: 'post',
                success: function (data) {
                    alert('Mağazanızın profil anasayfa bilgileri güncellenmiştir.');
                },
                error: function (x, a, r) {
                    alert(x.responseText);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
              Firma Anasayfa Bilgi
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well well-mt2">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <%:Html.TextAreaFor(model => model.StoreProfileDescription)%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <button type="submit" class="btn btn-primary" onclick="UpdateProductAbout();">
                                    Değişiklikleri Kaydet
                               
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
