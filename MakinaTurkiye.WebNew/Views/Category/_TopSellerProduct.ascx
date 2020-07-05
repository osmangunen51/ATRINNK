﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ActiveCategory>" %>

<div class="col-xs-12 col-md-8">
    <div class="top-seller-list">
        <div class="top-sellet-list__header">
            Kategori Çok Satanlar
        </div>
        <ul class="top-seller-list__items">
            <%
                int categoryid = Model.CategoryId.ToInt32();
                MakinaTurkiyeEntities entiti = new MakinaTurkiyeEntities();
                var parameter = new System.Data.Objects.ObjectParameter("TotalRecord", typeof(int));
                int PageDimension = 12;
                int CurrentPage = 1;
                var sorgu = entiti.spProductWebSearchMostViewDate(parameter, PageDimension, CurrentPage, 15, categoryid).ToList();
                int productCount = 0;
                //int count = 0;
                foreach (var item in sorgu)
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
                        <h3 class="top-seller-list__item-product-title"><%= Html.Truncate(item.ProductName, 500)%></h3>
                        <p class="top-seller-list__item-product-brand">Marka : <strong><%=Html.Truncate(item.BrandName,500) %></strong> Model : <strong><%=Html.Truncate(item.ModelName,500) %></strong></p>
                        <p class="top-seller-list__item-product-price">
                            <% string currencyType = model2;
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

                            <%if (tutar != "0")
                                {%>
                            <%:tutar%>,<sup><%=kusurat %></sup>
                            <% }
                                else
                                { %>
                                                        00,00
                                                    <%} %>
                        </p>
                    </div>
                    <p class="top-seller-list__item-product-price">
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

                        <%if (tutar != "0")
                            {%>
                        <%:tutar%>,<sup><%=kusurat %></sup>
                        <% }
                            else
                            { %>
                                                        00,00
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

<%--<div class="col-xs-12">
  <div class="panel panel-mt2">
    <div class="panel-heading p0">
      <span class="btn btn-md disabled">Benzer İlanlar </span>
      <%string url = "/" + Helpers.ToUrl(Model.TopCategory.CategoryName) + "/" + Model.TopCategory.CategoryId + "/" + Helpers.ToUrl(Model.TopCategory.CategoryName); %>
      <a href="<%=url %>" class="label text-sm label-success">Tümünü Göster </a>
    </div>
    <div class="panel-body ptb0">
      <div class="tab-content">

          <ul class="top-seller-list__items">
                            <%
                                int categoryid = Model.TopCategory.CategoryId.ToInt32();
                                MakinaTurkiyeEntities entiti = new MakinaTurkiyeEntities();
                                var parameter = new System.Data.Objects.ObjectParameter("TotalRecord", typeof(int));
                                int PageDimension = 12;
                                int CurrentPage = 1;
                                var sorgu = entiti.spProductWebSearchMostViewDate(parameter, PageDimension, CurrentPage, 15, categoryid).ToList();
                                int productCount = 0;
                                //int count = 0;
                                foreach (var item in sorgu)
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
                                        <h3 class="top-seller-list__item-product-title"><%= Html.Truncate(item.ProductName, 500)%></h3>
                                        <p class="top-seller-list__item-product-brand">Marka : <strong> <%=Html.Truncate(item.BrandName,500) %></strong> Model : <strong> <%=Html.Truncate(item.ModelName,500) %></strong></p>
                                        <p class="top-seller-list__item-product-price">
                                            <% string currencyType = model2;
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
                                                
                                                    <%if (tutar != "0")
                                                    {%>
                                                        <%:tutar%>,<sup><%=kusurat %></sup>
                                                    <% }
                                                    else
                                                    { %>
                                                        00,00
                                                    <%} %>
                                        </p>
                                    </div>
                                    <p class="top-seller-list__item-product-price">
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
                                                
                                                    <%if (tutar != "0")
                                                    {%>
                                                        <%:tutar%>,<sup><%=kusurat %></sup>
                                                    <% }
                                                    else
                                                    { %>
                                                        00,00
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
</div>--%>
