<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.FirstOrDefault(x=>x.CssClass=="active").FilterName %> - makinaturkiye.com
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">
         <%
        string productActiveType = "";
        string productActive = "";
        if (Request.QueryString["productActiveType"] != null) productActiveType = Request.QueryString["productActiveType"].ToString();
        if (Request.QueryString["ProductActive"] != null) productActive = Request.QueryString["ProductActive"].ToString();
        %>
        function ProductSearchResult() {
            var name = $("#productNameForSearch").val();
            $(".loading").show();
            $.ajax({
                url: '/Account/ilan/ProductSearchGet',
                type: 'GET',
                data: { productName: name, productActiveType:"<%:productActiveType%>", productActivee:"<%:productActive%>" },
                success: function (data) {
                    if (data == "false") {

                    }

                    else if (data) {

                        $("#displayProductList").html(data);
                    }
                    $(".loading").hide();
                },
                error: function (x, l, e) {
                    alert(e.responseText);
                }
            });

        }
        function PriceUpdateClick(id) {
            $("#PriceUpdateClick" + id).hide();
            $(".UpdateArea" + id).show();
            $("#PriceValue" + id).hide();

        }
        function PriceUpdateButton(id1) {
            var newPrice = $("#PriceUpdateText" + id1).val();

            if (newPrice != "" && newPrice != "0" && $.isNumeric(newPrice) != false) {
                $.ajax({
                    url: '/Account/ilan/PriceUpdateAdvertList',
                    type: 'POST',
                    data: { id: id1, price: newPrice },
                    success: function (data) {
                        if (data) {
                            $("#priceDisplay" + id1).html(newPrice);
                            $("#PriceUpdateClick" + id1).show();
                            $("#PriceValue" + id1).show();
                            $(".UpdateArea" + id1).hide();

                        }
                    },
                    error: function (x, l, e) {
                        alert(e.responseText);
                    }
                });
            }
            else {
                alert("Lütfen Fiyat Giriniz.");
                $("#PriceUpdateText" + id1).val("");

            }
        }
        $(document).ready(function () {

                       jQuery.curCSS = function (element, prop, val) {
                return jQuery(element).css(prop, val);
            };
            $("#productNameForSearch").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: "/Account/ilan//AdvertSearch",
                        type: "POST",
                        dataType: "json",
                        data: { name: request.term },
                        success: function (data) {
                            if (data.length == 0) {

                            }
                            else {
                                $("#dataNo").html("");

                                response($.map(data, function (item) {

                                    return { label: item, value: item };
                                }))
                            }

                        }
                    })
                },
                messages: {
                    noResults: "", results: ""
                }
            });

        })


    </script>
    <style type="text/css">
        .ui-corner-all { -moz-border-radius: 4px 4px 4px 4px; }
        .ui-widget-content { background: none; border: 1px solid black; background-color: #fff !important; }
        .ui-widget { font-family: Verdana,Arial,sans-serif; font-size: 15px; }
        .ui-menu { display: block; float: left; list-style: none outside none; margin: 0; padding: 2px; }
        .ui-autocomplete { cursor: default; position: absolute; }
        .ui-menu .ui-menu-item { clear: left; float: left; margin: 0; padding: 0; width: 100%; }
            .ui-menu .ui-menu-item a { display: block; padding: 3px 3px 3px 3px; text-decoration: none; cursor: pointer; background-color: white; font-size: 13px; }
                .ui-menu .ui-menu-item a:hover { display: block; padding: 3px 3px 3px 3px; text-decoration: none; color: #000; font-weight: bold; cursor: pointer; background-color: white; }
            .ui-menu .ui-menu-item:hover a { font-size: 13px; }
            .ui-menu .ui-menu-item:hover { color: #000; font-weight: bold; }
        .ui-menu-item div { font-size: 13px; color: #000; }
            .ui-menu-item div:hover { font-size: 13px; color: #000; font-weight: bold; border: 1px solid #ccc; }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.HiddenFor(x=>x.ProductActive) %>
    <%:Html.HiddenFor(x=>x.ProductActiveType) %>
    <%:Html.HiddenFor(x=>x.DisplayType) %>
    <%:Html.HiddenFor(x=>x.OrderType) %>

    <%
        MakinaTurkiyeEntities makinaTurkiyeEntities = new MakinaTurkiyeEntities();
        IList<Product> productItems = makinaTurkiyeEntities.Products.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();

        byte? queryProductActiveType = null;
        if (!string.IsNullOrEmpty(Request.QueryString["ProductActiveType"]))
        {
            queryProductActiveType = Request.QueryString["ProductActiveType"].ToByte();
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["ProductActive"]))
        {
            queryProductActiveType = Request.QueryString["ProductActive"].ToByte();
        }

        if (queryProductActiveType == null)
        {
            queryProductActiveType = Request.Form["ProductActiveType"].ToByte();
        }
    %>
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenuModel)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <%=Model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.FirstOrDefault(x=>x.CssClass=="active").FilterName %>
            </h4>
        </div>
    </div>

    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div>
                <div class="loading">Loading&#8230;</div>
                <div class="well store-panel-container">
                    <div class="row m5">
                        <%=Html.RenderHtmlPartial("_AdvertTop",Model.MTAdvertsTopViewModel) %>
                        <%if (Model.DisplayType == (byte)DisplayType.List)
                            {%>
                        <div class="row" style="margin-bottom: 15px;">
                            <div class="col-md-12">
                                <form action="javascript:void(0)">
                                    <div class="input-group">

                                        <input type="text" class="form-control" name="productNameForSearch" id="productNameForSearch" placeholder="Anahtar Kelime..(ürün arama)" aria-describedby="basic-addon2">
                                        <span class="input-group-btn">
                                            <button onclick="ProductSearchResult();" id="searchButtonAdvert" class="btn  btn-default" type="submit">
                                                <span class="glyphicon glyphicon-search"></span>
                                            </button>
                                        </span>

                                    </div>

                                </form>

                            </div>
                            <div class="col-md-12">
                            </div>
                        </div>
                        <% } %>

                        <div class="row">
                            <div class="col-md-12">
                                <div data-rel="tab-content" class="tab-content" id="displayProductList">

                                    <% int displayType = Convert.ToInt32(Request.QueryString["DisplayType"]);
                                        if (displayType == (byte)DisplayType.List)
                                        {
                                    %>
                                    <%= Html.RenderHtmlPartial("_AdvertListWindow", Model.MTProducts) %>
                                    <%}
                                    else
                                    {%>
                                    <%=Html.RenderHtmlPartial("_AdvertListTable",Model.MTProducts) %>
                                    <% } %>


                                    <%--                <% if (Request.QueryString["ProductActiveType"] != null || Request.QueryString["ProductActive"] != null)
                                        {
                                            DisplayType type = (DisplayType)Request.QueryString["DisplayType"].ToByte();
                                            switch (type)
                                            {
                                                case DisplayType.List: %>
                                    <%= Html.RenderHtmlPartial("AdvertList", Model.MTProducts)%>

                                    <%break;
                                        case DisplayType.Table:
                                    %>
                                    <%=Html.RenderHtmlPartial("_AdvertListTable",Model.MTProducts) %>
                                    <% break;
                                        }%>

                                    <% } %>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="GetDopingModal" tabindex="-1" role="dialog" aria-labelledby="GetDopingModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document" id="modelContent">
        </div>
    </div>


    <%
        byte? activeType = null;
        if (Request.QueryString["ProductActiveType"] != null)
        {
            activeType = Request.QueryString["ProductActiveType"].ToByte();
        }
        else if (Request.QueryString["ProductActive"] != null)
        {
            if (Request.QueryString["ProductActive"].ToByte() == 1)
            {
                activeType = 4;
            }
            else
                activeType = 5;
        }
    %>
    <input id="hdnActiveType" type="hidden" value="<%:activeType %>" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.pDeleteItem').click(function () {
                if (confirm('İlanı silmek istediğinize emin misiniz?')) {

                    var currentelement = $(this);
                    var obj = new Object();
                    obj.ProductId = $(this).attr('data-val');
                    $.ajax({
                        url: '/Account/ilan/ProductDelete',
                        type: 'POST',
                        data: obj,
                        success: function (data) {
                            currentelement.parent().parent().parent().find('.delProdConMes').show();
                            currentelement.parent().hide();

                            RefreshProductList();

                        },
                        error: function (x, l, e) {
                            alert(e.responseText);
                        }
                    });
                }
            });
        })
        function ProductActiveUpdate(productId, active) {
            $.ajax({
                url: '/Account/ilan/ProductActiveUpdate',
                type: 'POST',
                data:
                    {
                        ProductId: productId,
                        Active: active
                    },
                success: function (data) {
                    var statusText = "Aktif";
                    if (active == false)
                        statusText = "Pasif";
                    alert("Ürün durumu " + statusText + " başarıyla güncelleştirilmiştir.");
                    RefreshProductList();


                },
                error: function (x, l, e) {
                    alert(e.responseText);

                }
            });
        }


        function RefreshProductList() {

            advertPageChange($("#CurrentPage").val(), $("#DisplayType").val(), $("#ProductActiveType").val());
        }
        function advertPageChange(p, d, a) {
            $(".loading").show();
            $('[data-rel="liActive"]').removeClass("active");
            $('[data-rel="liActive"][data-id="' + p + '"]').addClass("active");
            try {
                //                          var curpage = "&currentPage=1";
                //                        if(getUrlVars()["currentPage"]!=null){
                var curpage = "&currentPage=" + p;
                //}

                //history.pushState(window.location.search, "page new", "?ProductActive=" + getUrlVars()["ProductActive"] + "&DisplayType=" + getUrlVars()["DisplayType"] + curpage);
                history.pushState(window.location.search, "page new", "?ProductActiveType=" + $('#hdnActiveType').val() + "&DisplayType=" + getUrlVars()["DisplayType"] + curpage);

            } catch (e) {

            }
            var ProductNo = $("#ProductNo").val();
            var CategoryName = $("#CategoryName").val();
            var ProductName = $("#ProductName").val();
            var BrandName = $("#BrandName").val();
            $.ajax({
                url: '/Account/ilan/AdvertPaging',
                type: 'post',
                data: {
                    page: p, displayType: d,
                    advertListType: $('#hdnActiveType').val(),
                    productActive: $("#ProductActive").val(),
                    productActiveType: a,
                    productNo: ProductNo,
                    categoryName: CategoryName,
                    productName: ProductName,
                    brandName: BrandName,
                    orderType: $("#OrderType").val()
                },
                success: function (data) {


                    $('[data-rel="tab-content"]').html(data);
                    $(".loading").hide();
                    $("#ProductNo").val(ProductNo);
                    $("#CategoryName").val(CategoryName);
                    $("#ProductName").val(ProductName);
                    $("#BrandName").val(BrandName);

                }
            });


        }

        function ShowProductDoping(productId) {
            $("#modelContent").html("");
            $.ajax({
                url: '/Account/ilan/_ProductInfoDoping',
                type: 'get',
                data:
                    {
                        ProductId: productId
                    },
                success: function (data) {
                    $("#modelContent").html(data);
                },
                error: function (x, l, e) {
                    alert(e.responseText);
                }
            });
        }

    </script>




</asp:Content>
