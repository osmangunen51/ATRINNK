﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<ProductModel>>" %>
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
        <div class="hidden-xs hidden-sm col-md-3">
            <%
                Dictionary<string, object> imageHtmlAtturbite = new Dictionary<string, object>();
                imageHtmlAtturbite.Add("alt", Html.Truncate(model.ProductName, 80));
                imageHtmlAtturbite.Add("class", "img-thumbnail");
                imageHtmlAtturbite.Add("title", Html.Truncate(model.ProductName, 80));
                if (!string.IsNullOrEmpty(Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite).ToString()))
                {%>
            <a href="<%=productUrl %>">
                <%=Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite) %>
            </a>
            <%} %>
            <%else
                {  %>
            <a href="<%=productUrl %>">
                <img class="img-thumbnail" src="https://dummyimage.com/400x300/efefef/000000.jpg&text=%C3%9Cr%C3%BCn%20Resmi%20Haz%C4%B1rlan%C4%B1yor"
                    alt="<%:model.ProductName%>" title="<%:model.ProductName%>" />
            </a>
            <%} %>
        </div>
        <div class="col-xs-9 col-md-7">
            <h4 class="media-heading">
                <a href="<%:productUrl %>">
                    <%:model.ProductName %>
                </a>&nbsp; <span class="text-sm">(<%:model.ProductNo %>) </span>
            </h4>
            <div class="row">
                <div class="text-muted col-xs-6">
                    Kategori :
                    <%: model.CategoryName%>
                    <br>
                    Marka :
                    <%: model.BrandName%>
                    <br>
                    Seri :
                    <%: model.SeriesName%>
                    <br />
                    Model Tipi :
                    <%: model.ModelName%>
                    <br>
                    Model Yılı :
                    <%: model.ModelYear %>
                    <br>
                    Görüntülenme Sayısı :
                    <%:model.ViewCount%>
                    <br>
                    <br />

                    Fiyatı :
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
                    <span style="background-color: #ffffff; border: 1px solid #e1e1e1; padding: 10px;" id="PriceValue<%:model.ProductId %>">
                        <span id="priceDisplay<%:model.ProductId %>">
                            <%: model.ProductPrice.GetValueOrDefault(0).ToString("N2") %>
                        </span>
                        <%--                               <i id="PriceUpdateClick<%:model.ProductId %>" style="color:#333; cursor:pointer;" class="glyphicon glyphicon-pencil" onclick="PriceUpdateClick(<%:model.ProductId %>)"></i>--%>
                    </span>

                    <%--  <span style="display:none;" class="UpdateArea<%:model.ProductId %>"><%:Html.TextBox("PriceUpdateText"+model.ProductId,model.ProductPrice.Value.ToString("N2"),new{@class="form-control"}) %>
                          <button type="button" class="btn btn-info" onclick="PriceUpdateButton(<%:model.ProductId %>)">Kaydet</button>
                    </span>--%>



                    <%}
                    else
                    { %>
                    <span style="background-color: #ffffff; border: 1px solid #e1e1e1; padding: 10px;">
                        <%: model.ProductPriceBegin.GetValueOrDefault(0).ToString("N2") %>-<%:model.ProductPriceLast.GetValueOrDefault(0).ToString("N2") %><a href="/Account/Advert/Edit/<%:model.ProductId %>?currentPage=<%:Model.CurrentPage%>"><i style="color: #333" class="glyphicon glyphicon-pencil"></i></a>
                    </span>
                    <%}
                        }
                        else
                        {
                            if (model.ProductPriceType == (byte)ProductPriceType.PriceAsk)
                            {%>
                    <span style="background-color: #ffffff; border: 1px solid #e1e1e1; padding: 10px;">Sorunuz  <a href="/Account/Advert/Edit/<%:model.ProductId %>?currentPage=<%:Model.CurrentPage%>"><i style="margin-left: 10px; color: #333" class="glyphicon glyphicon-pencil"></i></a></span>
                    <%}
                        else
                        { %>
                    <span style="background-color: #ffffff; border: 1px solid #e1e1e1; padding: 10px;">Görüşülür   <a href="/Account/Advert/Edit/<%:model.ProductId %>?currentPage=<%:Model.CurrentPage%>"><i style="margin-left: 10px; color: #333" class="glyphicon glyphicon-pencil"></i></a>
                    </span>
                    <%}
                        } %>
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
                    İlan Bitiş Tarihi :
                    <%:model.ProductAdvertEndDate.ToString("dd.MM.yyyy") %>
                    <br>
                    İl/İlçe :
                    <%: model.CityName + " / "+ model.LocalityName %>
                </div>
            </div>
        </div>
        <div class="col-xs-3 col-md-2">
            <div class="btn-group-vertical">
                <%if (model.ProductActiveType == (byte)ProductActiveType.CopKutusunda)
                    {%>
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
                        <li><a onclick="ProductActiveUpdate(<%:model.ProductId %>, 'true');">Aktif İlan </a>
                        </li>
                    </ul>
                    <% } %>
                </div>
                <a href="/Account/Advert/Edit/<%:model.ProductId %>?currentPage=<%:Model.CurrentPage%>" class="btn btn-sm btn-default">Düzenle </a>
                <a data-val="<%:model.ProductId %>" class="btn btn-sm btn-default pDeleteItem">Sil</a>
                <%}%>
            </div>
            <div>
                <%if (model.ProductActiveType == (byte)ProductActiveType.Onaylandi && model.Doping != true)
                    {%>
             <a class="btn btn-sm  background-mt-btn" data-toggle="modal" onclick="ShowProductDoping(<%:model.ProductId %>)" data-target="#GetDopingModal" style="margin-top:20px; padding:5px;">
                 <i class="fa fa-arrow-circle-up"></i>  Dopingle
                </a>
                <% }
                    else if (model.Doping == true)
                    { %>
                <a class="btn btn-sm btn-info" style="margin-top: 20px; padding:5px;">Dopingli Ürün
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
                    currentelement.parent().parent().parent().find('.delProdConMes').show();
                    currentelement.parent().hide();


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



                        },
                        error: function (x, l, e) {
                            alert(e.responseText);
                        }
                    });
                }
            });
        })
    </script>
    <% } %>
    <% }
        else
        { %>
    <p class="bg-info">
        Bu bölüma ait herhangi bir ürün bulunamamıştır.
    </p>
    <% } %>
</div>
