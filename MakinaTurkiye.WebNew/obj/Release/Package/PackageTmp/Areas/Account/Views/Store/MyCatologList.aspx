<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel.MTStoreCatologViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteCatolog(catologId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/Store/DeleteCatolog',
                    data: { id: catologId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {

                        if (data) {
                            $('#row' + catologId).hide();
                        }
                    }
                });
            }

        }
        function CatologEditShow(catologId) {
            $.ajax({
                url: '/Account/Store/GetCatologInfo',
                data: { id: catologId },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        $("#CatologName").val(data.Name);
                        $("#FileOrder").val(data.FileOrder);
                        $("#hdnCatologId").val(data.CatologId);
                    }
                }
            });

        }
        function UpdateCatolog() {
            var name = $("#CatologName").val();
            var fileOrder = $("#FileOrder").val();
            var catologId = $("#hdnCatologId").val();

            $.ajax({
                url: '/Account/Store/UpdateCatolog',
                data: { CatologId: catologId, Name: name, FileOrder: fileOrder },
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
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenuModel)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Katolog Yönetimi
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt4 col-xs-12" style="background: #fff;">
                <div class="pull-left">
                    <h3>Katologlarım</h3>
                </div>
                <div class="pull-right">
                    <a class="btn btn-success" href="/Account/Store/CreateCatolog">Yeni Ekle <i class="fa fa-add"></i></a>
                </div>

                <div class="col-md-12">
                    <%if (Model.MTCatologItems.Count() > 0)
                        {%>

                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Adı</th>
                                <th>Dosya</th>
                                <th>Sıra</th>
                                <th></th>
                            </tr>
                        </thead>
                        <%=Html.RenderHtmlPartial("_CatologItemList",Model.MTCatologItems) %>
                    </table>

                    <% }
                        else
                        {%>
                    <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                        Eklediğiniz herhangi bir katolog bulunmamaktadır. Müşterilerinize daha detaylı bilgiler vermek için katolog eklemeniz kurumsal kimliğinizi ortaya çıkaracak etkenlerden biridir.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="CatologEdit" tabindex="-1"
        role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close"
                        data-dismiss="modal">
                        <span aria-hidden="true">&times;
                        </span><span class="sr-only">Kapat</span></button>
                    <h4 class="modal-title" id="H1">Katolog Düzenle</h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnCatologId" name="hdnSortId" class="form-control" />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3">
                                Katolog Adı
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="CatologName" class="form-control" />
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-md-3">
                                Sıra
                            </label>
                            <div class="col-md-9">
                                <input type="text" id="FileOrder" class="form-control" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="UpdateCatolog()" class="btn btn-default">
                        Güncelle</button>
                    <button type="button"
                        class="btn btn-default" data-dismiss="modal">
                        Vazgeç</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
