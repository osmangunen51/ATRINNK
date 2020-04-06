﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreOtherProductModel>"  %>

<%if(Model.ProductItemModels.Count>0) 
    {%>
<div class="col-xs-12">
    <div class="panel panel-mt2">
        <div class="panel-heading p0">
            <span class="btn btn-md disabled"><span style="margin:0; font-size:14px;">Satıcının Diğer İlanları</span></span>
            <a href="<%=Model.AllStoreOtherProductUrl %>" class="label text-sm label-success">Tümünü Göster </a>
        </div>
        <div class="panel-body ptb0">
            <div class="tab-content">
                <ul class="top-seller-list__items">
                    <%
                        foreach (var item in Model.ProductItemModels)
                        { %>
                    <li class="top-seller-list__item clearfix">
                        <a href="<%= item.ProductUrl %>">
                            <span class="top-seller-list__ranking"><%:item.Index %></span>
                            <div class="top-seller-list__image">
                                <%  
                                    if (!string.IsNullOrEmpty(item.SmallPicturePah))
                                    {
                                %>
                                     <img alt="<%=item.SmallPictureName %>" src="<%=item.SmallPicturePah %>" title="<%=item.SmallPictureName %>">
                                <% 
                                }
                                else
                                { %>
                                     <img src="https://dummyimage.com/200x150/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=Html.Truncate(item.ProductName, 80)%>" />
                                <% }
                                %>
                            </div>
                            <div class="top-seller-list__item-product-info">
                                <span class="top-seller-list__item-product-title"><%= item.ProductName%></span>
                                <p class="top-seller-list__item-product-brand">Marka : <strong><%=item.BrandName %></strong> Model : <strong><%=item.ModelName %></strong></p>
                            </div>
                              <p class="top-seller-list__item-product-price">
                                  <%if (!string.IsNullOrEmpty(item.Price))
                                      {%>
                                        <i itemprop="priceCurrency" class="<%=item.CurrencyCss %>"></i>
                                            <% string idCss = "";
                                                if (item.CurrencyCss == "") {
                                                    idCss = "interview";                                   
                                              } %>
                                           <span class="<%:idCss %>">
                                         <%:item.Price%></span>
                                <%}
                                else {%>
                                   <span class="interview">Fiyat Sorunuz</span>
                                 <% } %>
                            </p>
                        </a>
                    </li>
                    <% } %>
                </ul>
            </div>
        </div>
    </div>
</div>
<%} %>
