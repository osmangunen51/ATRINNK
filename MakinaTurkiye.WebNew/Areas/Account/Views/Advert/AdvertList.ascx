﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<ProductModel>>" %>
<%--<div class="listHeader" style="width: 768px;">
  <div class="listContent" style="width: 740px;">
    <div style="float: left; width: 300px">
      <span style="margin-right: 3px;">Görünüm :</span>
      <% 
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
      <a href="/Account/Advert/Index?ProductActiveType=<%:queryProductActiveType %>&DisplayType=<%= (byte)DisplayType.Window %>">
        <img src="/content/images/window.png" alt="Pencere" style="float: left" /><span>Pencere</span>
      </a><a href="/Account/Advert/Index?ProductActiveType=<%:queryProductActiveType %>&DisplayType=<%= (byte)DisplayType.List %>">
        <img src="/content/images/list.png" alt="Liste" style="float: left" /><span>Liste</span>
      </a><a href="/Account/Advert/Index?ProductActiveType=<%:queryProductActiveType %>&DisplayType=<%= (byte)DisplayType.Text %>">
        <img src="/content/images/text.png" alt="Metin" style="float: left" /><span>Metin</span></a>
    </div>
  </div>
  <div class="listContent" style="margin-top: 4px; width: 740px">
    <div style="float: left; width: auto">
      <span>Toplam
        <%=Model.TotalPages.Count()%>
        sayfa içerisinde</span> <span style="color: #0769cd">
          <%=Model.CurrentPage %>.</span> <span>sayfadasınız.</span><span>Toplam
            <%:Model.TotalRecord %>
            ürün bulunmaktadır.</span>
    </div>
    <div style="float: right;" class="listPaging">
      <ul>
        <li><span style="font-size: 11px;">Sayfa :</span></li>
        <li style="padding-top: 1px;">
          <% foreach (var item in Model.TotalLinkPages)
             { %>
          <% if (Model.CurrentPage == item)
             { %>
          <span class="activepage" style="cursor: pointer; color: red; font-weight: bold;">
            <%: item %></span>
          <% }
             else
             { %>
          <span class="page" style="cursor: pointer; font-weight: bold;" onclick="PageChange(<%: item %>,<%= (byte)DisplayType.List %>, <%= queryProductActiveType %>);">
            <%: item %></span>
          <% } %>
          <% } %>
        </li>
      </ul>
    </div>
  </div>
</div>--%>

<%
    string urlAdd = "";
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

                using (var entities = new MakinaTurkiyeEntities())
                {
                    constantItems = entities.Constants.ToList();
                   
                }

                string productType = "";

                foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductType))
                {
                    if (model.ProductType != null)
                    {
                        for (int i = 0; i < model.ProductType.Split(',').Length; i++)
                        {
                            if (item.ConstantId == model.ProductType.Split(',').GetValue(i).ToInt16())
                            {
                                productType = productType + " " + item.ConstantName;
                            }
                        }
                    }
                }

                string productStatu = "";
                foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductStatu))
                {
                    if (model.ProductStatu != null)
                    {
                        for (int i = 0; i < model.ProductStatu.Split(',').Length; i++)
                        {
                            if (item.ConstantId == model.ProductStatu.Split(',').GetValue(i).ToInt16())
                            {
                                productStatu = productStatu + " " + item.ConstantName;
                            }
                        }
                    }
                }


                string briefDetail = "";
                foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductBriefDetail))
                {
                    if (!string.IsNullOrEmpty(model.BriefDetail))
                    {
                        for (int i = 0; i < model.BriefDetail.Split(',').Length; i++)
                        {
                            if (item.ConstantId == model.BriefDetail.Split(',').GetValue(i).ToInt16())
                            {
                                if (string.IsNullOrWhiteSpace(briefDetail))
                                {
                                    briefDetail = item.ConstantName;
                                }
                                else
                                    briefDetail = briefDetail + " - " + item.ConstantName;
                            }
                        }
                    }
                }

                string salesTypeName = "";
                foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductSalesType))
                {
                    for (int i = 0; i < model.ProductSalesType.Split(',').Length; i++)
                    {
                        if (item.ConstantId == model.ProductSalesType.Split(',').GetValue(i).ToInt16())
                        {
                            if (string.IsNullOrWhiteSpace(salesTypeName))
                            {
                                salesTypeName = item.ConstantName;
                            }
                            else
                                salesTypeName = salesTypeName + " - " + item.ConstantName;
                        }
                    }
                }
    %>
    <div class="row">
        <div class="col-md-3">
      

            <a href="<%=productUrl+urlAdd %>">
                <img class="img-thumbnail" src="<%:model.ProductImage %>" alt="<%:model.ProductName %>"  />
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
        <div class="col-xs-9 col-md-7">
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
                        <%if (model.ProductPriceType == (byte)ProductPriceType.PriceRange || model.ProductPriceType == (byte)ProductPriceType.Price)
                            {
                        %>
                        <% if (model.CurrencyId == 2)
                            { %>
                        <i id="currency<%:model.ProductId %>" class="fa fa-usd"></i>
                        <%}
                            else if (model.CurrencyId == 3)
                            { %>
                        <i class="fa fa-eur"></i>
                        <%}
                            else if (model.CurrencyId == 4)
                            { %>
                        <i class="fa fa-jpy"></i>
                        <%}
                            else
                            {%>
                        <i class="fa fa-turkish-lira"></i>
                        <%}%>
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
                        <%if (model.ProductPriceType == (byte)ProductPriceType.Price)
                            { %>
                        <%: model.ProductPrice.GetValueOrDefault(0).ToString("N2") %>
                        <%}
                        else
                        { %>

                        <%: model.ProductPriceBegin.GetValueOrDefault(0).ToString("N2") %>-<%:model.ProductPriceLast.GetValueOrDefault(0).ToString("N2") %>

                        <%}
                            }
                            else
                            {
                                if (model.ProductPriceType == (byte)ProductPriceType.PriceAsk)
                                {%>
                
                              Sorunuz  
                            <%}
                                else
                                { %>
                
                            Görüşülür  
                            
                    <%}
                        } %>
                    </strong>

                </div>
                <div class="text-muted col-xs-6">
                    Ürün Tipi :
                    <%:productType%>
                    <br>
                    Ürün Durumu :
                    <%:productStatu%>
                    <br>
                    Kısa Detay :
                    <%:briefDetail%>
                    <br>
                    Satış Detayı :
                    <%:salesTypeName%>
                    <br>
                    <%--                İlan Bitiş Tarihi :
                    <%:model.ProductAdvertEndDate.ToString("dd.MM.yyyy") %>
                    <br>--%>
                    İl/İlçe :
                    <%: model.CityName + " / "+ model.LocalityName %>
                </div>
            </div>
        </div>
        <div class="col-xs-3 col-md-2">
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
                        if (queryProductActiveType == 0)
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
            <div>
                <%if (model.ProductActiveType == (byte)ProductActiveType.Onaylandi && model.Doping != true)
                    {%>
                 <a class="btn btn-sm  background-mt-btn" data-toggle="modal" onclick="ShowProductDoping(<%:model.ProductId %>)" data-target="#GetDopingModal" style="margin-top:20px; background-color:#ad0132;">
                    Doping Satın Al
                </a>
                <% }
                     else if (model.Doping == true) { %>
                        <a class="btn btn-sm btn-info"  style="margin-top:20px;">
                    Dopingli Ürün
                </a>  
                <%}%>
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
                RefreshProductList();
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

    function ProductDelete(productId) {
        $.ajax({
            url: '/Account/ilan/ProductDelete',
            type: 'POST',
            data:
                {
                    ProductId: productId
                },
            success: function (data) {
                //alert("Ürün silme talebiniz alınmıştır.Onaylandığı taktirde ürününüz silinecetir.");
                return true;
            },
            error: function (x, l, e) {
                alert(e.responseText);
                return false;
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
