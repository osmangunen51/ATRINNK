<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MTSearchProductModel>>" %>
<div class="tab-pane fade in active" id="liste">
    <% foreach (var model in Model)
       { %>
    <%
           string productUrl = Helpers.ProductUrl(model.ProductId, model.ProductName);
           string ModelYil = (model.ModelYear != 0) ? (" " + model.ModelYear.ToString() + " " + "Model" + " ") : "";
           string Sdesc = (model.BriefDetailText != null) ? (model.BriefDetailText + " ") : "";
           string ptypea = (model.ProductTypeText != null) ? (model.ProductTypeText + " ") : "";
           string pstypea = (model.ProductSalesTypeText != null) ? (model.ProductSalesTypeText + " ") : "";
           string sit = (model.ProductStatuText != null || model.ProductStatuText != "") ? (model.ProductStatuText + " ") : "";
           string fulla = ptypea + sit + (sit.Equals("Sıfır ") ? DateTime.Now.Year.ToString() + " " : ModelYil) + (Sdesc.Contains("Garantili") && !string.IsNullOrEmpty(model.WarrantyPeriod) ? Sdesc.Replace("Garantili", string.Format("{0} Yıl Garantili", model.WarrantyPeriod)) : Sdesc) + pstypea; %>
    <div class="row hidden-item-container">
        <div class="col-xs-3 urun-resim">
            <a href="<%=productUrl %>">
                <%
           Dictionary<string, object> imageHtmlAtturbite = new Dictionary<string, object>();
           imageHtmlAtturbite.Add("alt", Html.Truncate(model.ProductName, 80));
           imageHtmlAtturbite.Add("class", "img-thumbnail");
           imageHtmlAtturbite.Add("title", Html.Truncate(model.ProductName, 80));
            
            %>
                <% if (!string.IsNullOrEmpty(Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite).ToString()))
                   {
            %>
                <%=Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbite)%>
                <% 
                   }
                   else
                   { %>
                <img src="https://dummyimage.com/200x150/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" class="img-thumbnail"
                    alt="<%=Html.Truncate(model.ProductName, 80)%>" title="<%=Html.Truncate(model.ProductName, 80)%>" />
                <% } %>
            </a>
        </div>
        <div class="col-xs-6">
            <h4 class="media-heading">
                <a href="<%=productUrl %>">
                    <%:model.ProductName%>
                </a>
            </h4>
            <%:fulla%>
            <p class="text-muted">
                <br />
                  <ul class="list-inline  mt20  text-muted">
                <li><span class="glyphicon glyphicon-time"></span>
                    <%:model.CityName %>
                </li>
                <li><span class="glyphicon glyphicon-cog"></span>
                    <%:model.LocalityName %>
                </li>
            </ul>
            </p>
            <p>
                <a href="<%=productUrl %>"><span class="label label-info">İlan No
                    <%:model.ProductNo %></span></a>
            </p>


        </div>
        <div class="col-xs-3">
            <p class="text-lg item-price">
                <%--<i class="fa fa-turkish-lira"></i> --%>
              
                <%string deger = "";
                  var model2 = model.CurrencyName;
                  var model3 = model.ProductPrice;

                  string tutar = "";
                  if (model3 != null)
                  {
                      decimal sayı = (decimal)model3;
                      tutar = sayı.ToString("0.");
                  }


                  if (model3 != null)
                  {
                      if (tutar != "0,00" && model2 != null)
                      {
                          //var phone=from c in entities.Phones
                          deger = model2.ToString();
                          ViewData["dov"] = tutar + " " + model2.ToString();
                      }

                  }
                %>
                <b>
                
                    <%if (tutar != "0")
                      {%>
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
                    <%:tutar%>,<sup style="font-size: 10px;">00</sup>
                    <% }
                      else
                      { %>
                   <span class="interview">Fiyat: Sorunuz</span><%} %>
                </b>
            </p>
            <p class="text-muted">
                Marka : <strong>
                    <%:model.BrandName%></strong>
                <br />
                Model Tipi: <strong>
                    <%: model.ModelName%></strong>
            </p>
                   <span class="label label-success">
                
                   <%:Html.Truncate(model.StoreName,25) %>
                </span>
        </div>
        <div class="col-xs-12">
            <hr>
        </div>
    </div>
    <%  } %>
</div>
