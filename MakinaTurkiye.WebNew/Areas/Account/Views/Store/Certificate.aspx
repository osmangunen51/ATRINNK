﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.MTStoreCertificateModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteCertificate(catologId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/Store/DeleteCertificate',
                    data: { id: catologId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {

                        if (data) {
                            $('#certificate-' + catologId).hide();
                        }
                    }
                });
            }

        }
        function CertificateEditShow(catologId) {
            $.ajax({
                url: '/Account/Store/CertificateInfo',
                data: { certificateId: catologId },
                type: 'get',
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        $("#CertificateName").val(data.Name);
                        $("#Order").val(data.Order);
                        $("#hdnCatologId").val(catologId);
                        $("#CertificateType").html(data.CertificateOptions);

                    }
                }
            });

        }

        function UpdateCertificate() {
            var Name = $("#CertificateName").val();
            var order = $("#Order").val();
            var certificateId = $("#hdnCatologId").val();
            var certificatetype = $("#CertificateType").val();

            $.ajax({
                url: '/Account/Store/UpdateCertificate',
                data: { certificateId: certificateId, name: Name, order: order, certificateType : certificatetype },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        window.location = "<%=Request.Url.ToString()%>";
                    }
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>

        <div class="col-md-12">
            <h4 class="mt0 text-info">Sertifaka Yönetimi
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt4 col-xs-12" style="background: #fff;">
                <div class="row">
                    <div class="col-md-12">
                        <div class="pull-left">
                            <div class="form-header-text">Sertfikilarım</div>
                        </div>
                        <div class="pull-right">
                            <a class="btn btn-success" href="/Account/Store/CreateCertificate">Yeni Ekle <i class="fa fa-add"></i></a>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>

                <div class="row">
                    <%if (Model.StoreCertificateItemModels.Count > 0)
                        {%>
                    <%foreach (var item in Model.StoreCertificateItemModels)
                        {%>
                    <div class="col-md-12" id="certificate-<%:item.StoreCertificateId %>" style="margin-top: 10px;">


                        <div class="col-md-12" style="border-bottom: 1px solid #ccc; padding-bottom: 5px;">
                            <div class="pull-left">
                                <span style="color: #333; font-size: 18px;"><%:item.CertificateName %></span>
                            </div>
                            <div class="pull-right">
                                <i onclick="DeleteCertificate(<%:item.StoreCertificateId %>)" class="fa fa-trash" style="font-size: 15px; margin-left: 20px; cursor: pointer;"></i>
                                <i data-toggle="modal" data-target="#CertificateEdit" onclick="CertificateEditShow(<%:item.StoreCertificateId %>)" class="fa fa-pencil" style="font-size: 15px; margin-left: 20px; cursor: pointer;"></i>
                            </div>
                            <div class="clearfix"></div>
                        </div>


                        <div class="col-md-12" style="margin-top: 10px;">
                            <%foreach (var photo in item.PhotoPaths)
                                {%>
                            <div class="col-md-2">
                                <img src="<%:photo.Replace(".png",".jpg") %>" class="img-responsive" style="border: 1px solid #ccc; -webkit-box-shadow: -2px 4px 7px -3px #000000; box-shadow: -2px 4px 7px -3px #000000;" />
                            </div>
                            <%} %>
                        </div>
                    </div>
                    <%} %>
                    <% }
                        else
                        {%>
                    <div class="col-md-8">
                        <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                            Eklediğiniz herhangi bir sertifika bulunmamaktadır. Müşterilerinize daha detaylı bilgiler vermek için sertifika eklemeniz kurumsal kimliğinizi ortaya çıkaracak etkenlerden biridir.
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
                        <div class="form-group">
                            <label class="col-md-3">
                               Sertifika Tipi
                            </label>
                            <div class="col-md-9">
                             <select class="form-control" id="CertificateType">


                             </select>
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
