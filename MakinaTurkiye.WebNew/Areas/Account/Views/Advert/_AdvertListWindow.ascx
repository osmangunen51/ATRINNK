<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductItem>>" %>

<%:Html.Hidden("ProductNo") %>
<%:Html.Hidden("CategoryName") %>
<%:Html.Hidden("ProductName") %>
<%:Html.Hidden("BrandName") %>
<%:Html.Hidden("CurrentPage",Model.CurrentPage) %>
<%
    string urlAdd = "";
    byte? queryProductActiveType = null;
    byte? productActiveA = null;
    if (!string.IsNullOrEmpty(Request.QueryString["ProductActiveType"]))
    {
        queryProductActiveType = Request.QueryString["ProductActiveType"].ToByte();
    }
    else if (!string.IsNullOrEmpty(Request.QueryString["ProductActive"]))
    {
        queryProductActiveType = Request.QueryString["ProductActive"].ToByte();
        productActiveA = Request.QueryString["ProductActive"].ToByte();
    }

    if (queryProductActiveType == null)
    {
        queryProductActiveType = Request.Form["ProductActiveType"].ToByte();
    }
    if (queryProductActiveType == 0)
    {
        urlAdd = "?pageType=firstadd";
    }
%>
<%--<%:Html.HiddenFor(model=>model.CurrentPage) %>--%>
<div class="tab-pane fade in active" id="liste">
    <% if (Model.Source.Count > 0)
        {

            foreach (var model in Model.Source)
            {
                string productUrl = Helpers.ProductUrl(model.ProductId, model.ProductName);
                IList<Constant> constantItems = null;
    %>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-2">
                <a href="<%=productUrl+urlAdd %>">
                    <img class="img-thumbnail" src="<%:model.ImagePath %>" alt="<%:model.ProductName %>" />
                </a>

                <%if (queryProductActiveType == (byte)ProductActiveType.Tumu)
                    {
                        if (model.ProductActiveType == (byte)ProductActiveType.Inceleniyor)
                        { %>
                <p><b>*İlanınız İnceleniyor</b></p>
                <%}
                    else if (queryProductActiveType == (byte)ProductActiveType.Onaylanmadi)
                    { %>
                <p><b>*İlanınız Onaylanmadı</b></p>
                <%}
                    }%>
            </div>
            <div class="col-xs-9 col-md-6">
                <h4 class="media-heading">
                    <a href="<%:productUrl+urlAdd %>">
                        <%:model.ProductName %>
                    </a>&nbsp; <span class="text-sm">(<%:model.ProductNo %>) </span>
                </h4>
                <div class="row">
                    <div class="text-muted col-xs-6">
                        Kategori :
                    <strong style="color: #666"><%: model.CategoryName%></strong>
                        <br>
                        Marka :
                    <strong style="color: #666"><%: model.BrandName%></strong>
                        <br>
                        Seri :
                    <%: model.SeriesName%>
                        <br />
                        Model Tipi :
                   <strong style="color: #666"><%: model.ModelName%></strong>
                        <br>
                        Model Yılı :
                    <%: model.ModelYear %>
                        <br>
                        Görüntülenme Sayısı :
                    <%:model.ViewCount%>
                        <br>
                        <br />

                        Fiyatı :
                    
                    <strong style="color: #666">


                        <%--<% switch (model.CurrencyName)
                {
                    case "TL": %>
                        <i class="fa fa-turkish-lira"></i>  
                        <% break;
                    case "EUR": %>
                        <i class="fa fa-eur"></i>  
                        <% break;    
                    case "USD": %>
                        <i class="fa fa-usd"></i>  
                        <% break;
                    case "JPY": %>
                        <i class="fa fa-jpy"></i>  
                        <% break;
                } 
            %>--%>
                        <span class="<%:!string.IsNullOrEmpty(model.ProductPriceWithDiscount) ? "old-price":"" %>">
                            <i class="<%:model.CurrencyCssText %>"></i>
                            <%:model.ProductPrice %>
                        </span>
                        <%if (!string.IsNullOrEmpty(model.ProductPriceWithDiscount))
                            {%>
                        <%:model.ProductPriceWithDiscount %>
                        <% } %>
                    </strong>

                    </div>
                    <div class="text-muted col-xs-6">
                        Ürün Tipi :
                    <%:model.ProductTypeText%>
                        <br>
                        Ürün Durumu :
                    <%:model.ProductStatusText%>
                        <br>
                        Kısa Detay :
                    <%:model.BriefDetail%>
                        <br>
                        Satış Detayı :
                    <%:model.SalesTypeText%>
                        <br>
                        <%--                İlan Bitiş Tarihi :
                    <%:model.ProductAdvertEndDate.ToString("dd.MM.yyyy") %>
                    <br>--%>
                    İl/İlçe :
                    <%: model.City + " / "+ model.Locality %>
                    </div>
                </div>
            </div>
            <div class="col-xs-3 col-md-4">
                <div class="col-md-6">
                    <div>
                        <%if (model.ProductActiveType == (byte)ProductActiveType.Onaylandi && model.Doping != true)
                            {%>
                        <div>
                            <a class="btn btn-sm  background-mt-btn" data-toggle="modal" onclick="ShowProductDoping(<%:model.ProductId %>)" data-target="#GetDopingModal" style="margin-top: 20px; margin-top: 0px; padding: 5px;">
                                <i class="fa fa-arrow-circle-up" style="margin-right: 3px"></i>Doping Satın Al
                            </a>
                        </div>
                        <% }
                            else if (model.Doping == true)
                            { %>
                        <div>
                            <a class="btn btn-sm btn-info" style="margin-top: 20px; padding: 5px;">Dopingli Ürün
                            </a>
                        </div>
                        <%}%>
                        <div>
                            <a class="btn btn-sm btn-info" href="/Account/ilan/ProductVideos/<%:model.ProductId %>" style="margin-top: 20px; padding: 5px;">
                                <i class="fa fa-video-camera" style="margin-right: 3px"></i>Video Ekle/Düzenle
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="btn-group-vertical">
                        <%if (model.ProductActiveType == (byte)ProductActiveType.CopKutusunda)
                            {%>
                        <a href="#" onclick="ProductUndoDelete(<%:model.ProductId %>, 'true');" class="btn btn-sm btn-default">Geri Al </a>
                        <%-- <a class="btn btn-sm btn-default disabled">Silme onayı bekliyor...</a>--%>
                        <%}
                            else if (model.ProductActiveType == (byte)ProductActiveType.Silindi)
                            { %>

                        <a href="#" onclick="ProductToReceyleBin(<%:model.ProductId %>);" class="btn btn-sm btn-default">Çöpe Gönder </a>
                        <a href="#" onclick="ProductUndoDelete(<%:model.ProductId %>, 'true');" class="btn btn-sm btn-default">Geri Al </a>
                        <%}
                            else
                            { %>
                        <div class="btn-group">
                            <% 
                                if (queryProductActiveType == 0 && productActiveA == null)
                                {%>
                            <a class="btn btn-sm btn-success dropdown-toggle"
                                data-toggle="dropdown">İnceleniyor </a>

                            <%}
                                else
                                {
                                    if (model.ProductActive)
                                    { %>
                            <a onclick="" class="btn btn-sm btn-success dropdown-toggle"
                                data-toggle="dropdown">Aktif İlan <span class="caret"></span></a>
                            <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                                <li><a onclick="ProductActiveUpdate(<%:model.ProductId %>, 'false');">Pasif İlan </a>
                                </li>
                            </ul>
                            <% }
                                else
                                { %>
                            <a onclick="" class="btn btn-sm btn-danger dropdown-toggle"
                                data-toggle="dropdown">Pasif İlan <span class="caret"></span></a>
                            <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                                <li><a onclick="ProductActiveUpdate(<%:model.ProductId %>, 'true')">Aktif İlan </a>
                                </li>
                            </ul>
                            <% }
                                } %>
                        </div>
                        <a href="/Account/ilan/Edit/<%:model.ProductId %>?currentPage=<%:Model.CurrentPage%>" class="btn btn-sm btn-default">Düzenle </a>
                        <a data-val="<%:model.ProductId %>" class="btn btn-sm btn-default pDeleteItem">Sil</a>
                        <%}%>
                    </div>

                </div>


            </div>
    </div>
    <div class="col-xs-12">
        <br />
        <%if (model.ProductActiveType == (byte)ProductActiveType.Silindi)
            {%>
        <div class="alert alert-danger">
            <i class="glyphicon glyphicon-exclamation-sign"></i>&nbsp; <span>Ürün silme talebiniz
                    alınmıştır. <%--<a href="#" onclick="ProductToReceyleBin(<%:model.ProductId %>);">Çöpe Gönder </a><a href="#" onclick="ProductUndoDelete(<%:model.ProductId %>, 'true');">Geri Al </a>--%></span>
        </div>
        <%} %>
        <%--   <div class="alert alert-danger delProdConMes" style="display: none;">
                <i class="glyphicon glyphicon-exclamation-sign"></i>&nbsp; <span>Ürün silme talebiniz
                    alınmıştır. Yönetici onayı bekleniyor.</span>
            </div>--%>
        <hr />
    </div>
</div>
<% } %>
<% }
    else
    { %>
<p class="bg-info">
    Bu bölüma ait herhangi bir ürün bulunamamıştır.
</p>
<% } %>
</div>
<% if (Model.Source.Count > 0)
    { %>
<div class="row">
    <div class="col-md-5 ">
        Toplam
        <%= Model.TotalPages.Count() %>
        sayfa içerisinde
        <%= Model.CurrentPage %>.sayfayı görmektesiniz.
    </div>
    <div class="col-md-7 text-right">
        <ul class="pagination m0">
            <li><a href="#">&laquo;</a></li>
            <% Session["TotalRecord"] = Model.TotalRecord; %>
            <%  foreach (int item in Model.TotalLinkPages)
                {
                    if (Model.CurrentPage == item)
                    { %>
            <li class="active"><a onclick="advertPageChange(<%: item %>,<%= (byte) DisplayType.List %>, <%= queryProductActiveType %>);" href="javascript:void(0)">
                <%: item %></a></li>
            <% }
                else
                {
            %>
            <li data-rel="liActive" data-id="<%: item %>"><a onclick="advertPageChange(<%: item %>,<%= (byte) DisplayType.List %>, <%= queryProductActiveType %>);" href="javascript:void(0)">
                <%: item %></a></li>
            <% } %>
            <% } %>
            <%if (Model.CurrentPage < Model.TotalPages.Count())
                {
            %>
            <li data-id="<%: Model.CurrentPage+1 %>"><a onclick="advertPageChange(<%: Model.CurrentPage+1  %>,<%= (byte) DisplayType.List %>, <%= queryProductActiveType %>);" href="javascript:void(0)">>></a> </li>

            <%} %>
        </ul>
    </div>
</div>
<% } %>

<%:Html.HiddenFor(x=>x.CurrentPage,new { @id="currentPage"}) %>
<script type="text/javascript">
    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }


    function RefreshProductList() {
        advertPageChange($("#CurrentPage").val(), $("#DisplayType").val(), $("#ProductActiveType").val());
    }

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
                alert("Ürün durumu  başarıyla güncelleştirilmiştir.");
        advertPageChange($("#CurrentPage").val(), $("#DisplayType").val(), $("#ProductActiveType").val());


            },
            error: function (x, l, e) {
                alert(e.responseText);

            }
        });
    }

    function ProductToReceyleBin(productId) {
        $.ajax({
            url: '/Account/ilan/ProductToReceyleBin',
            type: 'POST',
            data:
            {
                ProductId: productId
            },
            success: function (data) {
                alert("Ürün başarıyla çöpe atılmıştır.");
 advertPageChange($("#CurrentPage").val(), $("#DisplayType").val(), $("#ProductActiveType").val());
            },
            error: function (x, l, e) {
                alert(e.responseText);
            }
        });
    }

    function ProductUndoDelete(productId, active) {
        $.ajax({
            url: '/Account/ilan/ProductUndoDelete',
            type: 'POST',
            data:
            {
                ProductId: productId,
                Active: active
            },
            success: function (data) {
                alert("Ürün başarıyla geri alınmıştır.");
                RefreshProductList();
            },
            error: function (x, l, e) {
                alert(e.responseText);
            }
        });
    }


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
</script>
