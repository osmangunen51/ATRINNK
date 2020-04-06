<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTCommentsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gelen Yorumlar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function ProductCommentPaging(p) {
                         $.ajax({
                    url: '/Account/ilan/commentpaging',
                    data: { page: p },
                    type: 'post',
                    success: function (data) {
                        if (data) {
                       
                                  $("#data").html(data);
                        }
                    }
            });
            
        }
        function ReportComment(i) {
            if (confirm('Yorum Şikayet Etmek İstediğinize Eminmisiniz?')) {
                $.ajax({
                    url: '/Account/ilan/ReportProductComment',
                    data: { productCommentId: i },
                    type: 'post',
                    success: function (data) {
                        if (data) {

                            alert("Şikayetiniz alındı en kısa zamanda kontrol edilecek.");
                            window.location = "<%:Request.Url.AbsolutePath%>";
                        }
                    }
                });
            }
        }
  


    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

            <div class="row">
            <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
                <div class="col-md-12">
        <h4 class="mt0 text-info">
         <span class="text-primary glyphicon glyphicon-cog"></span>Ürün Yorumları
        </h4>
    </div>
       </div>

    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div class="well store-panel-container" style="background: #fff;">
                <div class="pull-left">
                    <h3>Yorumlar</h3>
                </div>
                <div class="col-md-12">
                    <%if (Model.ProductCommentStoreItems.Source.Count > 0)
                        {%>

                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Ürün Adı</th>
                                <th>Adı Soyadı</th>
                                <th>Yorum</th>
                                <th>Puan</th>
                                <th>Durum</th>
                                <th>Tarih</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody id="data">
                            <%=Html.RenderHtmlPartial("_CommentList",Model.ProductCommentStoreItems) %>
                        </tbody>
                    </table>

                    <% }
                        else
                        {%>
                    <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                    Henüz herhangi bir yorum bulunmamaktadır.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
