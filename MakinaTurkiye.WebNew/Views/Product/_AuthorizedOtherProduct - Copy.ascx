﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProductDetailViewModel>" %>
<div class="col-xs-12">
    <div class="panel panel-mt2">
        <div class="panel-heading p0">
           <span style="margin:0; font-size:14px;">Satıcının Diğer İlanları</span>						
           
            <%--           <% if (Model.StoreMainPartyId.HasValue)
				 { %>
	  
					
					<%string url = "";
					  url = "/Sirket/" + Model.ProductDetailInfo.StoreMainPartyId.Value.ToString() + "/" + Helpers.ToUrl(Model.ProductDetailInfo.StoreName);
					%>
					<div class="prodcutconnect" style="text-decoration:none; height:43px; color:#036;">
					<div style=" margin-top:5px; width:100px; height:40px;font-size: 12px; color: #036;font-family:Arial; margin-left:8px;" align="center">
						  <a class="trade1"  href="<%= url %>/Urunler">Satıcının Diğer İlanları</a>
						  </div>
					</div>
					
			 
			
			 <% } %>--%>
            <%  string url = "/Sirket/" + Model.ProductDetailInfo.StoreMainPartyId + "/" + Helpers.ToUrl(Model.ProductDetailInfo.StoreName) + "/Urunler"; %>
            <a href="<%=url %>" class="label text-sm label-success">Tümünü Göster
            </a>
           <%-- <span class="btn-group pull-right">
                <a href="#tab0" class="btn btn-md btn-mt" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                </a>
                <a href="#tab0" class="btn btn-md btn-mt" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                </a>
            </span>--%>
        </div>
        <div class="panel-body ptb0">
            <div class="tab-content">
                <ul class="top-seller-list__items">
                            <%
                                int productCount = 0;
                                //int count = 0;
                                foreach (var item in Model.AuthorizedOtherProductItems.Source)
                                {
                                    MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
                                    var model2 = (from c in entities.Products
                                                  where c.ProductId == item.ProductId
                                                  select c.Currency.CurrencyName).FirstOrDefault();

                                    string tutar = "";
                                    string kusurat = string.Empty;

                                    if (item.ProductPrice != null)
                                    {
                                        decimal sayi = (decimal)item.ProductPrice;
                                        string tutarson = sayi.ToString("0.00");
                                        string[] parcali = tutarson.Split(',');

                                        try
                                        {
                                            if (tutarson.IndexOf('.') > -1)
                                            {
                                                parcali = tutarson.Split('.');
                                            }
                                            tutar = parcali[0];
                                            kusurat = parcali[1];

                                        }
                                        catch (Exception)
                                        {


                                        }

                                    }

                                    if (item.ProductPrice != null)
                                    {
                                        if (tutar != "0,00" && model2 != null)
                                        {
                                            //var phone=from c in entities.Phones
                                            ViewData["dov"] = tutar + " " + model2.ToString();
                                        }

                                    }
                                    string productUrl = Helpers.ProductUrl(item.ProductId, item.ProductName);

                               
                            %>
                            <li class="top-seller-list__item clearfix">
                                <a href="<%= productUrl %>">
                                    <span class="top-seller-list__ranking"><%:productCount +1 %></span>
                                    <div class="top-seller-list__image">

                                        <%  
                                Dictionary<string, object> imageHtmlAtturbiteTwo = new Dictionary<string, object>();
                                imageHtmlAtturbiteTwo.Add("alt", Html.Truncate(item.MainPicture, 80));
                                //imageHtmlAtturbiteTwo.Add("class", "img-mt2");
                                imageHtmlAtturbiteTwo.Add("title", Html.Truncate(item.MainPicture, 80));
                                if (!string.IsNullOrEmpty(Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbiteTwo).ToString()))
                                {
                                        %>
                                        <%= Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbiteTwo)%>
                                        <% 
              }
              else
              { %>
                                        <img src="https://dummyimage.com/200x150/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=Html.Truncate(item.ProductName, 80)%>" />
                                        <% }
                                        %>
                                    </div>
                                    <div class="top-seller-list__item-product-info">
                                        <span class="top-seller-list__item-product-title"><%= Html.Truncate(item.ProductName, 500)%></span>
                                        <p class="top-seller-list__item-product-brand">Marka : <strong> <%=Html.Truncate(item.BrandName,500) %></strong> Model : <strong> <%=Html.Truncate(item.ModelName,500) %></strong></p>
                                        <p class="top-seller-list__item-product-price">
                  
                                                
                                                    <%if (tutar != "0")
                                                    {%>                          <% string currencyType = model2;
                                                   if (currencyType == "USD")
                                                    { %>
                                                        <i itemprop="priceCurrency" class="fa fa-usd"></i>
                                                    <%}
                                                    else if (currencyType == "EUR")
                                                    { %>
                                                        <i itemprop="priceCurrency" class="fa fa-eur"></i>
                                                    <%}
                                                    else if (currencyType == "JPY")
                                                    { %>
                                                        <i itemprop="priceCurrency" class="fa fa-jpy"></i>
                                                    <%}
                                                    else
                                                    {%>
                                                        <i itemprop="priceCurrency" class="fa fa-turkish-lira"></i>
                                                    <%}%>

                                                               <% string yaz = "";
                                                            int v = Convert.ToInt32(tutar);
                                                            if (v > 0 && v < 10)
                                                            {
                                                                yaz = v.ToString();
                                                            }
                                                            else
                                                            {
                                                                yaz= string.Format("{0:0,0}",v).Replace(",", ".");
                                                            }%>
                                                            <%:yaz %>
                                                    <% }
                                                    else
                                                    { %>
                                                     <span class="interview">Fiyat Sorunuz</span>
                                                    <%} %>
                                        </p>
                                    </div>
                                    <p class="top-seller-list__item-product-price">
                                      
                                                
                                                    <%if (tutar != "0")
                                                    {%>
                                          <% string currencyTypeClone = model2;
                                                   if (currencyTypeClone == "USD")
                                                    { %>
                                                        <i itemprop="priceCurrency" class="fa fa-usd"></i>
                                                    <%}
                                                    else if (currencyTypeClone == "EUR")
                                                    { %>
                                                        <i itemprop="priceCurrency" class="fa fa-eur"></i>
                                                    <%}
                                                    else if (currencyTypeClone == "JPY")
                                                    { %>
                                                        <i itemprop="priceCurrency" class="fa fa-jpy"></i>
                                                    <%}
                                                    else
                                                    {%>
                                                        <i itemprop="priceCurrency" class="fa fa-turkish-lira"></i>
                                                    <%}%>
                                                             <% string yaz = "";
                                                            int v = Convert.ToInt32(tutar);
                                                            if (v > 0 && v < 10)
                                                            {
                                                                yaz = v.ToString();
                                                            }
                                                            else
                                                            {
                                                                yaz= string.Format("{0:0,0}",v).Replace(",", ".");
                                                            }%>
                                                            <%:yaz %>
                                                    <% }
                                                    else
                                                    { %>
                                                       <span class="interview">Fiyat Sorunuz</span>
                                                    <%} %>
                                    </p>
                                </a>
                            </li>
                            <% 
                                productCount += 1;
                                 } %>
                            </ul>
            </div>
        </div>
    </div>
</div>