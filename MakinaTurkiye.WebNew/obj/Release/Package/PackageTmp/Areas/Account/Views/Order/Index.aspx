<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Checkouts.OrderPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Siparişlerim-Makina Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <%--    <script type="text/javascript">
        function DeleteLogo() {
            if ($('#hiddenDelete').val() == 'false') {
                $('#hiddenDelete').val('true');
                $('[data-rel="addlogo"]').show();
                $('[data-rel="logo"]').hide();
            }
        }
    </script>--%>
    <style type="text/css">
        table tr td, th { font-size: 15px; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-shopping-cart"></span>Siparişlerim
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div class="well well-mt">
                <%if (Model.OrderListItems.Count > 0)
                    {%>
                <div class="table-responsive-xl">
                    <table class="table table-hover table-sm">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Paket Adı</th>
                                <th scope="col">Fiyat</th>
                                <th scope="col">Kalan Miktar</th>
                                <th scope="col">Tarih</th>
                                <th scope="col">Araçlar</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%int counter = 1; %>
                            <%foreach (var order in Model.OrderListItems)
                                { %>
                            <tr>
                                <th scope="row"><%:counter %></th>
                                <td><%:order.PacketName %></td>
                                <td><%:order.OrderPrice.ToString("C2") %></td>
                                <td><%:order.RestAmount.ToString("C2") %></td>
                                <td><%:order.RecordDate.ToString("dd/MM/yyyy") %></td>
                                <td>
                                    <%if (order.RestAmount != 0)
                                        {%>
                                    <a class="btn btn-xs btn-success btn-add-on" href="/MemberShipSales/BeforePayCreditCard">Ödeme Yap</a>
                                    <% } %>

                                    <a class="btn btn-xs btn-success btn-add-on" href="">Faturayı Gör</a>

                                </td>
                            </tr>
                            <% counter++;
                                } %>
                        </tbody>
                    </table>
                </div>
                <% }
                    else
                    {%>
                <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                    Satın aldığınız herhangi bir paket siparişi bulunmamaktadır.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                </div>
                <% } %>
            </div>
        </div>
    </div>
</asp:Content>
