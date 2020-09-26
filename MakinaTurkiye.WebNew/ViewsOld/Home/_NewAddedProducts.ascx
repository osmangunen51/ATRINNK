<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<ProductModel>>"  %>
<%@ Import Namespace="System.Diagnostics" %>
<div id="yeni-eklenen-ilanlar" class="tab-pane carousel slide"	data-ride="carousel" data-interval="false" >
  <div class="carousel-inner thumbnail-container">
	 <%
		int pageSlice = 10;
		decimal pageCount = Math.Round(Convert.ToDecimal(Model.Count) / pageSlice, 0, MidpointRounding.AwayFromZero) + 1;
		for (int p = 1; p < pageCount; p++)
		{
		  var pageItems = Model.Skip((p - 1) * 10).Take(10);
	 %>
	 <div class="item<%= p.Equals(1) ? " active" : "" %>">
		<%
		  for (int i = 0; i < 2; i++)
		  { %>
		<div class="row fivecolumns plr5">
		  <%
			 foreach (var item in pageItems.Skip(i * 5).Take(5))
			 {
				Dictionary<string, object> imageHtmlAtturbite = new Dictionary<string, object>();
				imageHtmlAtturbite.Add("alt", Html.Truncate(item.ProductName, 80));
				imageHtmlAtturbite.Add("title", Html.Truncate(item.ProductName, 80));
		  %>
		  <div class="col-sm-2 col-md-2">
			 <div class="thumbnail thumbnail-mt">
				<a href="<%= item.ProductUrl %>">
                 <%if(!string.IsNullOrEmpty(item.MainPicture))
                   { %>
                      <%= Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite) %>    
                   <%} 
                     else 
                     {%>
                        <img src="/Content/V2/images/400x300.png" />
                    <%}%>
				</a>
				<div class="caption text-center">
				  <a href="<%=item.ProductUrl %>">
					 <%=item.ProductName%>
					 </a>
				  <% if (item.ProductPrice.HasValue && item.ProductPrice > 0)
					  {
						 MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
						 var model2 = (from c in entities.Products
											where c.ProductId == item.ProductId
											select c.Currency.CurrencyName).FirstOrDefault();

						 string tutar = "";
                         string kusurat = string.Empty;
						 decimal sayi = (decimal)item.ProductPrice;
                         string tutarson = sayi.ToString("0.00");
                         string[] parcali= tutarson.Split(',');
                         tutar = parcali[0];
                         try
                         {
                             kusurat = parcali[1];
                         }
                         catch (Exception)
                         {
                                
                            
                         }
                        
						 %>
				  <p>
					 <b>
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

						 <%:tutar%>,<sup style="font-size: 10px;"><%=kusurat %></sup>
					 </b>
				  </p>
				  <%} %>
				</div>
				<div class="hidden-item caption">
				  <p>
					 <%= Helpers.Truncate(item.BrandName, 20) %>
				  </p>
				  <p>
					 <%= Helpers.Truncate(item.ModelName, 20) %>
				  </p>
				</div>
			 </div>
		  </div>
		  <%
					 }
		  %>
		</div>
		<% } %>
	</div>
	 <% } %>
  </div>
   <span class="clearfix"> </span>
    <a class="carousel-left2" href="#yeni-eklenen-ilanlar" data-slide="prev">
        <span class="btn btn-md btn-default  glyphicon glyphicon-chevron-left">
        </span>
    </a>
    <a class="carousel-right2" href="#yeni-eklenen-ilanlar" data-slide="next">
        <span class="btn btn-md btn-default glyphicon glyphicon-chevron-right">
        </span>
    </a>
</div>
