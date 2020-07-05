<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.FavoriteProducts.FavoriteProductViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(window).scroll(function () {
            var requestScroll = $("#RequestScrool").val();

            if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                setTimeout(
                    function () {
                        //do something special
                    }, 3000);
                if (requestScroll == 1) {
                    $.ajax({

                        url: '/Account/Favorite/GetFavoriteProducts',
                        type: 'GET',
                        data: {
                            'page': $("#Page").val(),
                            'pageDimension': 3
                        },
                        dataType: 'json',
                        success: function (data) {
                            if (data.IsSuccess) {

                                $("#content-products").append(data.Result);
                                var page = Number($("#Page").val()) + 1;

                                $("#Page").val(page);
                                console.log("requested", page);
                            }
                            else {
                                $("#RequestScrool").val("0");
                            }
                        },
                        error: function (request, error) {

                        }
                    });
                }
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenuModel)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Favori İlanlarım
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">

            <div>
                <div class="row m5" id="content-products">
                    <%:Html.Hidden("Page",2) %>
                    <%:Html.Hidden("RequestScrool",1) %>
                    <%foreach (var item in Model.MTCategoryProductModels.Source)
                        {%>
                    <div class="col-md-3 col-lg-3">
                        <%=Html.RenderHtmlPartial("_ProductItemBox",item) %>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
