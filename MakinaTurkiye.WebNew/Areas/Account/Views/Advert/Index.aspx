<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<ProductViewModel>" %>


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
                        $(".loading").hide();
                    }

                    else if (data) {
                           $(".loading").hide();
                        $("#displayProductList").html(data);
                    }

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

            $("#productNameForSearch").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: "/Account/ilan//AdvertSearch",
                        type: "POST",
                        dataType: "json",
                        data: { name: request.term },
                        success: function (data) {
                            if (data.length == 0) {
                                console.log("ürün bulunamadı");
                            }
                            else {
                              
                               
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
 
    <div class="col-md-12">
        <h4 class="mt0 text-info">
            <span class="text-primary glyphicon glyphicon-cog"></span>İlanlarım
        </h4>
        <hr />
    </div>
    <div class="row">
        <div class="col-sm-4 col-md-3">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-sm-8 col-md-9">
            <div>
   
                <div class="well well-mt">
            <div class="loading">Loading&#8230;</div>
                    <div class="row m5">
                        <div class="well well-mt2 p0 m0" style="height: 30px;">
                            <div class="col-sm-12 col-md-4 col-lg-4 pr0">
                                <div class="btn-group btn-group-justified">

                                    <%   string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                                        string sCurrentValue = Request.QueryString["DisplayType"];
                                        string newvalue = ((byte)DisplayType.List).ToString();
                                        string urlList = "";
                                        urlList = currentUrl.Replace("DisplayType=" + sCurrentValue, "DisplayType=" + newvalue);
                                    %>
                                    <span class="btn btn-sm disabled btn-mt2">Görünüm: </span><a href="<%:urlList %>" data-toggle="tab"
                                        class="btn btn-sm btn-mt2 active"><span class="glyphicon glyphicon-th-list"></span>
                                    </a>

                                    <%   
                                        newvalue = ((byte)DisplayType.Table).ToString();
                                        string urlTable = "";
                                        urlTable = currentUrl.Replace("DisplayType=" + sCurrentValue, "DisplayType=" + newvalue);
                                    %>
                                    <a href="<%:urlTable %>"
                                        class="btn btn-sm btn-mt2"><span class="glyphicon glyphicon-th-list"></span>
                                    </a>
                                </div>
                            </div>
                            <div class="hidden-sm hidden-xs col-md-8  col-lg-8 pl0 mb20">
                                <div class="btn-group btn-group-justified">
                                    <span class="btn btn-sm disabled btn-mt2"></span>
                                    <div class="btn-group">
                                        <a href="#" class="btn btn-sm btn-mt2 dropdown-toggle" data-toggle="dropdown">Kategoriler
                                            <span class="caret"></span></a>
                                        <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                                            <%--                                            <li><a href="#">Test </a></li>
                                            <li><a href="#">Test </a></li>
                                            <li><a href="#">Test </a></li>
                                            <li><a href="#">Test </a></li>--%>
                                        </ul>
                                    </div>
                                    <div class="btn-group">
                                        <a href="#" class="btn btn-sm btn-mt2 dropdown-toggle" data-toggle="dropdown">Tüm İlanlar
                                            <span class="text-muted">(<%:productItems.Count()%>) </span><span class="caret"></span></a>
                                        <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                                            <li class="<%: ViewData["AdvertMenuActiveTumu"] %>"><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Tumu %>&DisplayType=2">Tüm İlanlar <span class="text-muted">(<%:productItems.Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuActive"] %>"><a href="/Account/Advert/Index?ProductActive=<%=(byte)ProductActive.Aktif%>&DisplayType=2">Aktif İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActive == true && c.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuPassive"] %>"><a href="/Account/Advert/Index?ProductActive=<%=(byte)ProductActive.Pasif%>&DisplayType=2">Pasif İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActive == false).Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuInceleniyor"] %>"><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Inceleniyor %>&DisplayType=2">Onay Bekleyen İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Inceleniyor).Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuOnaylandi"] %>"><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Onaylandi %>&DisplayType=2">Onaylanan İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuOnaylanmadi"] %>"><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Onaylanmadi %>&DisplayType=2">Onaylanmamış İlanlar <span class="text-muted">(<%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylanmadi).Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuActiveSilindi"] %>"><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.Silindi %>&DisplayType=2">Silinen İlanlar <span class="text-muted">(<%:productItems.Where(c=> c.ProductActiveType == (byte)ProductActiveType.Silindi).Count()%>)
                                            </span></a></li>
                                            <li class="<%: ViewData["AdvertMenuActiveCopKutusunda"] %>"><a href="/Account/Advert/Index?ProductActiveType=<%= (byte)ProductActiveType.CopKutusunda %>&DisplayType=2">Çöp İlanlar <span class="text-muted">(<%:productItems.Where(c=> c.ProductActiveType == (byte)ProductActiveType.CopKutusunda).Count()%>)
                                            </span></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 15px;">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <input type="text" class="form-control" name="productNameForSearch" id="productNameForSearch" placeholder="Anahtar Kelime..(ürün arama)" aria-describedby="basic-addon2">
                                    <span class="input-group-btn">
                                        <button onclick="ProductSearchResult();" id="searchButtonAdvert" class="btn  btn-default" type="submit">
                                            <span class="glyphicon glyphicon-search"></span>
                                        </button>
                                    </span>
                                </div>

                            </div>
                            <div class="col-md-12">
                    
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div data-rel="tab-content" class="tab-content" id="displayProductList">
                                    <% if (Request.QueryString["ProductActiveType"] != null || Request.QueryString["ProductActive"] != null)
                                        {
                                            DisplayType type = (DisplayType)Request.QueryString["DisplayType"].ToByte();
                                            switch (type)
                                            {
                                                case DisplayType.List: %>
                                    <%= Html.RenderHtmlPartial("AdvertList", Model.ProductItems)%>

                                    <%break;
                             
                                        }%>

                                    <% } %>
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
            $(".loading").show();

            $.ajax({
                url: '/Account/ilan/AdvertPaging',
                type: 'post',
                data: { page: $("#currentPage").val(), displayType:<%= (byte) DisplayType.List %>, advertListType: $('#hdnActiveType').val(), ProductActiveType: <%=queryProductActiveType%> },
                success: function (data) {

                    $('[data-rel="tab-content"]').html(data);
                    $(".loading").hide();
                }
            });
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

            $.ajax({
                url: '/Account/ilan/AdvertPaging',
                type: 'post',
                data: { page: p, displayType: d, advertListType: $('#hdnActiveType').val(), ProductActiveType: a,orderType=$("#OrderType").val() },
                success: function (data) {

                    $('[data-rel="tab-content"]').html(data);
                }
            });
            $(".loading").hide();

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
