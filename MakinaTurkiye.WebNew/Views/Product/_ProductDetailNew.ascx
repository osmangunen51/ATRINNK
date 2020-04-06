﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductDetailModel>" %>
<% if (!string.IsNullOrEmpty(Model.ProductNo))
    {%>
<div class="thumbnail">
    <div class="urun-bilgi">
        <div class="urun-baslik">
            <h1><%:Model.ProductName %></h1>
        </div>
        <span class="ilan-no">İlan No: <%:Model.ProductNo %></span>

        <hr>
        <div class="row pad-15">
            <div class="col-md-7 col-xs-12">
                <div class="fiyat flex-row flex-nowrap">
                    <%if (Model.Price != "" && (Model.ProductPriceType == (byte)ProductPriceType.Price || Model.ProductPriceType == (byte)ProductPriceType.PriceRange))
                        { %>
                    <div class="flex-col-8">

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
                    <span class="kdv flex-col-4" style="<%: !string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "padding-top:20px;":"" %>">
                        <%if (!string.IsNullOrEmpty(Model.UnitTypeText))
                            { %>
                        <small style="font-size: 10px;"><%:Model.UnitTypeText %></small>
                        <br />
                        <%} %>
                                              
                                            KDV
                                      <br>
                        DAHİL 
                               
                    </span>
                    <%}
                        else if (Model.Fob == true)
                        {
                    %>
                    <span class="kdv flex-col-4" style="<%: !string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "padding-top:20px;":"" %>">
                        <%if (!string.IsNullOrEmpty(Model.UnitTypeText))
                            { %>
                        <small style="font-size: 10px;"><%:Model.UnitTypeText %></small>
                        <br />
                        <%} %>
                                           FOB
                                           <br />
                        Fiyatı
                             
                    </span>
                    <%}%>
                    <%else if (Model.Kdv == false)
                        {%>
                    <span class="kdv flex-col-4" style="<%: !string.IsNullOrEmpty(Model.ProductPriceWithDiscount) ? "padding-top:20px;":"" %>">
                        <%if (!string.IsNullOrEmpty(Model.UnitTypeText))
                            { %>
                        <small style="font-size: 10px;"><%:Model.UnitTypeText %></small><br />
                        <%} %>
                                            KDV

                                      <br>
                        Hariç
                                    
                    </span>
                    <% }%>

                    <%}
                        else
                        { %>
                    <span style="font-size: 15px;">Fiyat Sorunuz</span>

                    <%} %>
                </div>
            </div>
            <div class="col-md-5 col-xs-12">
                <%if (Model.ProductActive)
                    { %>
                <button class="label label-danger btn-block btnClik btn-1" data-toggle="modal" data-target="#PostCommentsModal" style="border: 0px;" <%--rel="nofollow"--%> title="<%:Model.StoreName %> iletişim sayfası"><b>Satıcı İle İletişim Kur </b></button>
                <%}
                    else
                    {%>
                <div class="label label-warning btn-block btnClik btn-1">Bu ilan yayında değildir</div>
                <% } %>
            </div>
        </div>
        <div class="urun-bilgi-tablo">
            <table class="table teble-border">

                <tr>
                    <td style="width: 100px;">Kategori:
                    </td>
                    <td>
                        <a href="<%=Model.CategoryUrl%>">
                            <%:Model.CategoryName %></a>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(Model.BrandName))
                    {
                %>
                <tr>
                    <td>Marka:
                    </td>
                    <td>
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
                    <td>Model Tipi:
                    </td>
                    <td>
                        <a href="<%=Model.ModelUrl %>">
                            <span itemprop="model"><%:Model.ModelName%></span>
                        </a>
                    </td>
                </tr>
                <% }
                    if (Model.ProductStatuConstantId == 72)
                    {%>
                <tr>
                    <td>Model Yılı:
                    </td>
                    <td>
                        <%:DateTime.Now.Date.Year%>
                    </td>
                </tr>
                <% }
                    else
                    {
                        if (!string.IsNullOrEmpty(Model.ModelYear))
                        {%>
                <tr>
                    <td>Model Yılı:
                    </td>
                    <td>
                        <%: Model.ModelYear%>
                    </td>
                </tr>
                <%}
                    }%>

                <%if (!string.IsNullOrEmpty(Model.ProductType))
                    {%>
                <tr>
                    <td>Ürün Tipi:
                    </td>
                    <td>
                        <%: Model.ProductType%>
                    </td>
                </tr>
                <%}
                    else
                    {%>
                <tr>
                    <td>Ürün Tipi:
                    </td>
                    <td>Satılık
                    </td>
                </tr>
                <%}%>
                <tr>
                    <td>Ürün Durumu:
                    </td>
                    <td>
                        <%:Model.ProductStatus %>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(Model.MenseiName))
                    {  %>
                <tr>
                    <td>Menşei:
                    </td>
                    <td>
                        <%:Model.MenseiName%>
                    </td>
                </tr>
                <%}
                    if (!string.IsNullOrEmpty(Model.DeliveryStatus))
                    {  %>
                <tr>
                    <td>Teslim Durumu:
                    </td>
                    <td>
                        <%:Model.DeliveryStatus%>
                    </td>
                </tr>
                <%}%>


                <%if (!string.IsNullOrEmpty(Model.Location))
                    { %>
                <tr>
                    <td>Konum:
                    </td>
                    <td>
                        <%:Model.Location %>
                    </td>
                </tr>
                <%} %>
                <%if (!string.IsNullOrEmpty(Model.BriefDetail))
                    {%>
                <tr>
                    <td>Kısa Detay:
                    </td>
                    <td>
                        <%:Model.BriefDetail %>
                    </td>
                </tr>
                <%}%>
                <% if (!string.IsNullOrEmpty(Model.SalesType))
                    {%>
                <tr>
                    <td>Satış Detayı:
                    </td>
                    <td>
                        <%:Model.SalesType %>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(Model.Certificates))
                    {%>
                <tr>
                    <td>Sertifikalar:</td>
                    <td><%:Model.Certificates %></td>
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
        </div>
    </div>
</div>
<%} %>


