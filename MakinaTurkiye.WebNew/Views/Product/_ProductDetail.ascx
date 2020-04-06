﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductDetailModel>" %>
<% if(!string.IsNullOrEmpty(Model.ProductNo))
    {%>
<div class="col-sm-8 col-md-4">
    <div class="product-detail-description">
        <table class="table">
            <tr>
                <td style="width: 100px;">İlan No:
                </td>
                <td class="product-detail-description__product-no">
                    <%:Model.ProductNo%>                  
                </td>
            </tr>
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
                    <a  href="<%=Model.BrandUrl %>">
                        <span ><%:Model.BrandName%></span>
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
               else { 
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
               }
                if (!string.IsNullOrEmpty(Model.ProductType))
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
            <%}
                if (!string.IsNullOrEmpty(Model.AdvertBeginDate))
                {%>

            <tr>
                <td>İlan Tarihi:
                </td>
                <td>
                    <%=Model.AdvertBeginDate%>
                </td>
            </tr>
            <%}
                if (!string.IsNullOrEmpty(Model.AdvertEndDate))
                {%>
            <tr>
                <td>İlan Bitiş Tarihi:
                </td>
                <td>
                    <%=Model.AdvertEndDate%>
                </td>
            </tr>
            <% } %>
            <%if(!string.IsNullOrEmpty(Model.Location)){ %>
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

            <%}%>
        </table>

        <div class="btn-group product-detail-description__button-group">
             <a  class="btn btn-sm btn-default-price btn-default-price-color product-detail-description__button-group-item product-detail-description__button-group-item--price">
                <%if (string.IsNullOrEmpty(Model.Price))
                    { %>
                <span >Fiyat Sorunuz
                </span>
                <%}
                    else
                    {%>
                 <%:Model.Price %>
    <%--             <div itemprop="offers" itemscope itemtype="http://schema.org/Offer">
                    <span itemprop="price" > <%:Model.Price %></span>
                    
                 </div>
             --%>
                <%} %>
            </a>
            <%if (!string.IsNullOrEmpty(Model.ButtonDeliveryStatusText))
                {  %>
            <div class="btn btn-sm btn-success product-detail-description__button-group-item" style="background: #8CB565;">
                <span class="glyphicon glyphicon-credit-card"></span>
                <%:Model.ButtonDeliveryStatusText %>
            </div>
            <%}%>
            <a onclick="SendMessageforpro('proposal')" class="btn btn-sm btn-danger product-detail-description__button-group-item"><i class="fa fa-fw fa-gavel"></i>&nbsp;Teklif İste </a>
        </div>
        <div>
            <a onclick="SendMessageforpro('message')" class="btn btn-sm btn-primary product-detail-description__button-send-message"><span class="glyphicon glyphicon-envelope"></span>&nbsp;Satıcıya Mesaj Gönder Veya Soru Sor </a>
        </div>
    </div>
</div>
<%} %>
