﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductDetailModel>" %>
<% if (!string.IsNullOrEmpty(Model.ProductNo))
    {%>
<div class="thumbnail">
    <div class="urun-bilgi">
        <div class="row">
            <div class="col-md-6 col-xs-6">
                <div class="fiyat flex-column flex-nowrap">
                    <%if (Model.Price != "" && (Model.ProductPriceType == (byte)ProductPriceType.Price || Model.ProductPriceType == (byte)ProductPriceType.PriceRange))
                        { %>
                    <div class="flex-col-12 product-detail__price">

                        <span class="<%:!string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "old-price":"" %>">
                            <i class="<%:Model.ProductCurrencySymbol %>"></i>
                            <%:Model.PriceWithoutCurrency%> </span>
                        <%if (!string.IsNullOrEmpty(Model.ProductPriceWithDiscount))
                            {
                        %>
                        <br />
                        <span>
                            <i class="<%:Model.ProductCurrencySymbol %> flex-col-8 "></i>
                            <%:Model.ProductPriceWithDiscount %>
                        </span>
                        <%
                            } %>
                    </div>

                    <%if (Model.Kdv == true)
                        {%>
                    <div class="product-detail__kdv" style="<%: !string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "padding-top:20px;":"" %>">
                        <%if (!string.IsNullOrEmpty(Model.UnitTypeText))
                            { %>
                        <small><%:Model.UnitTypeText %>,
                        <%} %>
                            KDV
                        DAHİL    </small>
                    </div>
                    <%}
                        else if (Model.Fob == true)
                        {
                    %>
                    <div class="product-detail__kdv" style="<%: !string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "padding-top:20px;":"" %>">
                        <%if (!string.IsNullOrEmpty(Model.UnitTypeText))
                            { %>
                        <small><%:Model.UnitTypeText %>
                            <%} %>
                          FOB
                       
                        Fiyatı
                        </small>
                    </div>
                    <%}%>
                    <%else if (Model.Kdv == false)
                        {%>
                    <div class="product-detail__kdv" style="<%: !string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "padding-top:20px;":"" %>">
                        <%if (!string.IsNullOrEmpty(Model.UnitTypeText))
                            { %>
                        <small><%:Model.UnitTypeText %>
                            <%} %>
                             KDV Hariç           
                        </small>
                    </div>
                    <% }%>
                    <%}
                        else
                        { %>
                    <span class="product-detail__price">Fiyat Sorunuz</span>
                    <%} %>
                </div>
            </div>
            <div class="col-md-6 col-xs-6" style="text-align:right;">
                <span class="product-detail__ilan-no">İlan No: <%:Model.ProductNo %></span>
            </div>
        </div>
        <div class="row pad-15">
            <div class="col-md-12  col-xs-12">
                <%if (Model.ProductActive)
                    { %>
                <button class="btn btn-success btn-contact" data-toggle="modal" data-target="#PostCommentsModal" style="border: 0px;" title="<%:Model.StoreName %> iletişim sayfası"><i class="fa fa-phone" style="padding-right: 5px;"></i><b>İletişim </b></button>
                <%}
                    else
                    {%>
                <div class="btn btn-warning  btnClik btn-1" style="width: 100%;">Bu ilan yayında değildir</div>
                <% } %>
            </div>
        </div>
        <div class="urun-bilgi-tablo">
            <table class="table teble-border">
                <tr>
                    <td class="product-detail__tabletitle">Kategori:
                    </td>
                    <td class="product-detail__tablevalue">
                        <a href="<%=Model.CategoryUrl%>">
                            <%:Model.CategoryName %></a>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(Model.BrandName))
                    {
                %>
                <tr>
                    <td class="product-detail__tabletitle">Marka:
                    </td>
                    <td class="product-detail__tablevalue">
                        <a href="<%=Model.BrandUrl %>">
                            <span><%:Model.BrandName%></span>
                        </a>
                    </td>
                </tr>
                <%
                    }
                    if (!string.IsNullOrEmpty(Model.ModelName))
                    {
                %>
                <tr>
                    <td class="product-detail__tabletitle">Model Tipi:
                    </td>
                    <td class="product-detail__tablevalue">
                        <a href="<%=Model.ModelUrl %>">
                            <span itemprop="model"><%:Model.ModelName%></span>
                        </a>
                    </td>
                </tr>
                <% }

                    if (!string.IsNullOrEmpty(Model.ModelYear))
                    {%>
                <tr>
                    <td class="product-detail__tabletitle">Model Yılı:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%: Model.ModelYear%>
                    </td>
                </tr>
                <%}%>


                <%if (!string.IsNullOrEmpty(Model.ProductType))
                    {%>
                <tr>
                    <td class="product-detail__tabletitle">Ürün Tipi:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%: Model.ProductType%>
                    </td>
                </tr>
                <%}
                    else
                    {%>
                <tr>
                    <td class="product-detail__tabletitle">Ürün Tipi:
                    </td>
                    <td class="product-detail__tablevalue">Satılık
                    </td>
                </tr>
                <%}%>
                <tr>
                    <td class="product-detail__tabletitle">Ürün Durumu:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%:Model.ProductStatus %>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(Model.MenseiName))
                    {  %>
                <tr>
                    <td class="product-detail__tabletitle">Menşei:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%:Model.MenseiName%>
                    </td>
                </tr>
                <%}
                    if (!string.IsNullOrEmpty(Model.DeliveryStatus))
                    {  %>
                <tr>
                    <td class="product-detail__tabletitle">Teslim Durumu:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%:Model.DeliveryStatus%>
                    </td>
                </tr>
                <%}%>


                <%if (!string.IsNullOrEmpty(Model.Location))
                    { %>
                <tr>
                    <td class="product-detail__tabletitle">Konum:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%:Model.Location %>
                    </td>
                </tr>
                <%} %>
                <%if (!string.IsNullOrEmpty(Model.BriefDetail))
                    {%>
                <tr>
                    <td class="product-detail__tabletitle">Kısa Detay:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%:Model.BriefDetail %>
                    </td>
                </tr>
                <%}%>
                <% if (!string.IsNullOrEmpty(Model.SalesType))
                    {%>
                <tr>
                    <td class="product-detail__tabletitle">Satış Detayı:
                    </td>
                    <td class="product-detail__tablevalue">
                        <%:Model.SalesType %>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(Model.Certificates))
                    {%>
                <tr>
                    <td class="product-detail__tabletitle">Sertifikalar:</td>
                    <td class="product-detail__tablevalue"><%:Model.Certificates %></td>
                </tr>
                <% } %>
                <%if (Model.IsAllowProductSellUrl && !string.IsNullOrEmpty(Model.ProductSellUrl))
                    {
                %>
                <tr>
                    <td></td>
                    <td style="padding-top: 10px;">
                        <a class="btn btn-success btn-purchase" target="_blank" rel="nofollow" href="<%:Model.ProductSellUrl %>"><b>Satın Al </b></a>
                    </td>
                </tr>
                <%
                    } %>
                <%}%>
            </table>
   
            <ul class="nav nav-pills favoriler">
                <%string removeFavoriProductCss = "";  %>
                <%string addFavoriProductCss = "";  %>
                <% if (Model.IsFavoriteProduct)
                    { %>
                <% addFavoriProductCss = addFavoriProductCss + "display:none;"; %>
                <% }
                    else
                    { %>
                <% removeFavoriProductCss = removeFavoriProductCss + "display:none;"; %>
                <% } %>

                <li class="favori">
                    <a href="#" id="aRemoveFavoriteProduct" rel="nofollow" onclick="RemoveFavoriteProduct('<%=Model.ProductId %>','<%:Model.ProductName %>');"
                        style="<%=removeFavoriProductCss %>"><span class="glyphicon glyphicon-ok"></span>&nbsp;Favorilerimden Kaldır</a>
                    <a href="#" id="aAddFavoriteProduct" rel="nofollow" style="<%=addFavoriProductCss %>" onclick="AddFavoriteProduct('<%=Model.ProductId %>','<%=Model.ProductName %>');"><i class="fa fa-heart-o"></i>&nbsp;Favorilerime Ekle
                                          <img src="../../Content/V2/images/loading.gif" style="width: 16px; float: right; display: none;" id="FavoriteProductLoading" />
                    </a>

                </li>
                <li class="yazdir"><a href="javascript:void(0)" onclick="window.print();"><i class="fa fa-print"></i>&nbsp;Yazdır</a> </li>
                <li class="sikayet"><a data-toggle="modal" data-target="#ComplaintModal" id="ComplaintProduct" href="#"><i class="fa fa-flag"></i>&nbsp;Şikayet Et</a> </li>
                <%=Html.RenderHtmlPartial("_ProductComplain",Model.ProductComplainModel) %>
            </ul>
            <div style="margin: auto; display: inline-block; position: relative" class="text-center">
                <%=Html.RenderHtmlPartial("_ProductSocial") %>
            </div>
        </div>
    </div>
</div>
<%} %>


