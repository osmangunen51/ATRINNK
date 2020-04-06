﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MTSearchProductModel>>" %>
<div class="tab-pane fade in" id="pencere">
	 <% foreach (var item in Model)
		 { %>
	 <%string productUrl = Helpers.ProductUrl(item.ProductId, item.ProductName); %>
	 <div class="col-sm-6 col-md-4">
		  <div class="thumbnail thumbnail-mt">
				<a href="<%= productUrl %>">
					 <%
				Dictionary<string, object> imageHtmlAtturbite = new Dictionary<string, object>();
				imageHtmlAtturbite.Add("alt", Html.Truncate(item.ProductName, 80));
				imageHtmlAtturbite.Add("class", "img-thumbnail");
				imageHtmlAtturbite.Add("title", Html.Truncate(item.ProductName, 80));
				imageHtmlAtturbite.Add("id","img"+item.ProductId);
				
				%>
				<% if (!string.IsNullOrEmpty(Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite).ToString()))
				{
				%>
					 <%=Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite)%>
				<% 
				}
				else
				{ %>  
					 <img src="https://dummyimage.com/200x150/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=Html.Truncate(item.ProductName, 80)%>" />
				<% } %>
				</a>
				<div class="caption text-center">
					 <a title="<%: item.ProductName %>" href="<%= productUrl %>">
						  <%: Html.Truncate(item.ProductName, 30)%></a>
					 <p>
						  <a title="<%: item.ModelName %>" href="<%= productUrl %>"><span>
								<%: item.ModelName%> - <%: item.BrandName%>
						  </span></a>
						  <br />
						  <%--<i class="fa fa-turkish-lira"></i>--%>
						  <%
							 string tutar = "";
							 if (item.ProductPrice.HasValue)
							 {
								decimal sayı = (decimal)item.ProductPrice;
								tutar = sayı.ToString("0.");
							 }
						  %>
						
                  <b>
								 <% string currencyType = item.CurrencyName;
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
					    
						  <%:tutar%>,<sup style="font-size: 10px;">00</sup>
						 
					 <% }
						else
						{ %>
						>00,00<%} %>
					</b>  
					 </p>
				</div>
				<div class="caption caption-mt2">
					 <p class="mt20">
						  <a href="#" class="text-muted"><span class="glyphicon glyphicon-book"></span>
								<%: item.ProductNo%>
						  </a>
					 </p>
					 <a href="<%= productUrl %>" class="btn btn-xs btn-danger"><i class="fa fa-fw fa-gavel">
					 </i>Teklif Ver </a>
				</div>
		  </div>
	 </div>
	 <% } %>
</div> 