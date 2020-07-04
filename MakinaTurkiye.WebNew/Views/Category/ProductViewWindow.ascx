<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProductListViewModel>" %>

<div class="tab-pane fade in" id="pencere">
    <%string colorbackground = "white;";
        int i = 0;
    %>
    <div class="row clearfix">
        <% foreach (var model in Model.ProductSearch.Source)
            { %>
        <%++i;
            if (i % 2 == 0)
            {
                colorbackground = "white;";
            }
            else
                colorbackground = "rgb(247, 247, 247);";
        %>

        <div class="col-xs-12 col-sm-6 col-md-4">
            <%string productUrl = Helpers.ProductUrl(model.ProductId, model.ProductName); %>
            <div class="thumbnail thumbnail-mt">
                <%
                    Dictionary<string, object> imageHtmlAtturbite = new Dictionary<string, object>();
                    imageHtmlAtturbite.Add("alt", Html.Truncate(model.ProductName, 80));
                    imageHtmlAtturbite.Add("class", "img-thumbnail");
                    imageHtmlAtturbite.Add("title", Html.Truncate(model.ProductName, 80));
                    if (!string.IsNullOrEmpty(Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbite).ToString()))
                    {
                %>
                <a href="<%=productUrl %>">
                    <%=Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbite)%>
                </a>
                <%} %>
                <%else
                    {  %>
                <a href="<%=productUrl %>">
                    <img class="img-thumbnail broken-image" src="<%=Html.GetProductImage(model.ProductId, model.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbite).ToString()%>" title="<%:model.ProductName%>" />
                </a>
                <%} %>
                <div class="caption text-center">
                    <a href="<%=productUrl %>"><%:Html.Truncate(model.ProductName,30)%></a>
                    <p>

                        <%string s = model.ModelYear.ToString();

                            string Marka = (model.BrandName != null || model.BrandName != "") ? model.BrandName : "";
                            string Modeli = (model.ModelName != null) ? ("" + model.ModelName) : "";
                            string ModelYil = (model.ModelYear != 0) ? (" " + model.ModelYear.ToString() + " " + "Model" + " ") : "";
                            //string Sdesc = (model.BriefDetailText != null) ? (model.BriefDetailText + " ") : "";
                            //string ptype = (model.ProductTypeText != null) ? (model.ProductTypeText + " ") : "";
                            //string pstype = (model.ProductSalesTypeText != null) ? (model.ProductSalesTypeText + " ") : "";
                            //string sit = (model.ProductStatuText != null || model.ProductStatuText != "") ? (model.ProductStatuText + " ") : "";
                            //string full = ptype + sit + ModelYil + Sdesc + pstype;
                            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
                            var pro = (from c in entities.Products
                                       where c.ProductId == model.ProductId
                                       select c).FirstOrDefault();
                        %>

                        <span><%:Html.Truncate(Marka,15)%>
                            <br />
                            <%: Html.Truncate(Modeli,15)%></span>
                        <br>
                        <%string deger = "";
                            var model2 = (from c in entities.Products
                                          where c.ProductId == model.ProductId
                                          select c.Currency.CurrencyName).FirstOrDefault();
                            var model3 = (from c in entities.Products
                                          where c.ProductId == model.ProductId
                                          select c.ProductPrice).FirstOrDefault();
                            string[] parcali = null;
                            string tutar = "";
                            string kusurat = "";
                            if (model3 != null)
                            {
                                decimal sayı = (decimal)model3;
                                string tutarson = sayı.ToString("0.00");
                                parcali = tutarson.Split(',');
                                if (parcali.Length > 1)
                                {
                                    tutar = parcali[0].ToString();
                                    kusurat = parcali[1].ToString();
                                }
                                else
                                {
                                    parcali = tutarson.Split('.');
                                    if (parcali.Length > 1)
                                    {
                                        tutar = parcali[0].ToString();
                                        kusurat = parcali[1].ToString();
                                    }
                                }

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
                        <b class="item-price">
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
                            <% string yaz = "";
                                int v = Convert.ToInt32(tutar);
                                if (v > 0 && v < 10)
                                {
                                    yaz = v.ToString();
                                }
                                else
                                {
                                    yaz = string.Format("{0:0,0}", v).Replace(",", ".");
                                }%>
                            <%: yaz%><sup style="font-size: 9px;"><%:kusurat.ToString()=="00" ? "": ","+kusurat %></sup>
                            <% }
                                else
                                { %>
                            <span class="interview">Fiyat: Sorunuz</span>
                            <%} %>
                        </b>
                    </p>
                </div>
                <%--<div class="caption caption-mt2">
                    <p class="mt20">
                        <a href="#" class="text-muted">
                            <span class="glyphicon glyphicon-book"></span> Vivamus iaculis nisl...</a>
                    </p>
                    <a href="<%=productUrl %>" class="btn btn-xs btn-success">
                        <span class="glyphicon glyphicon-shopping-cart"></span> Hemen Al</a>
               </div>--%>
            </div>

        </div>
        <%  } %>
    </div>
    <%= Html.RenderHtmlPartial("_Paging", Model.ProductSearch)%>
</div>




