<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage< NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreNews.MTStoreNewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteNew(newid) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Account/StoreNew/Delete',
          data: { id: newid },
          type: 'post',
          dataType: 'json',
          success: function (data) {
   
              if (data){
                  $('#row' + newid).hide();
            }
          }
        });
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Haberler
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt4 col-xs-12" style="background: #fff;">
                <div class="pull-left">
                    <h4><%:Model.PageTitle %></h4>
                </div>
                <div class="pull-right">
                    <a class="btn btn-success" href="/Account/StoreNew/Create?newType=<%:Model.NewType %>">Yeni Ekle <i class="fa fa-add"></i></a>
                </div>

                <div class="col-md-12">
                    <%if (Model.MTStoreNews.Count() > 0)
                        {%>

                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Fotoğraf</th>
                                <th>Adı</th>
                                <th>Tarih</th>
                                <th>Görüntülenme</th>
                                <th>Durum</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (var item in Model.MTStoreNews)
                                {%>
                            <tr id="row<%:item.StoreNewId %>">
                                <td><a href="<%:item.NewUrl %>"><img src="<%:item.ImagePath %>"  class="img-responsive" style="height:50px;"/></a></td>
                                <td><a href="<%:item.NewUrl %>"><%:item.Title %></a></td>
                                <td><%:item.RecordDate.ToString("dd/MM/yyyy HH:mm") %></td>
                                <td><%:item.ViewCount %></td>
                                <td>
                                    <%if (item.Active)
                                        {%>
                                        Onaylandı
                                    <% }
                                                          else {%>
                                        İnceleniyor
                                    <% } %></td>
                                <td>
                                    <a href="/Account/StoreNew/Update/<%:item.StoreNewId %>"><span style="font-size:16px;" class="glyphicon glyphicon-pencil" style="color:#1051e9"></span></a>
                                    <a style="cursor:pointer;" onclick="DeleteNew(<%:item.StoreNewId %>)"><i style="color:#1051e9; font-size:16px;" class="fa fa-trash"></i></a>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>

                    </table>

                    <% }
                        else
                        {%>
                    <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                        Eklediğiniz herhangi bir haber bulunmamaktadır. 
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
