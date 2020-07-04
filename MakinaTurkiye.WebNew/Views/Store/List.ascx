﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreSearchModel>" %>
<%--<div style="width: 900px; height: 9px; margin-top: 1px; float: left;">
</div>--%>

<div class="tab-pane fade in active" id="liste">
    <% 
        MakinaTurkiyeEntities makinaTurkiyeEntities = new MakinaTurkiyeEntities();
        Category activeCat = new Category();
        int categoryID = ViewContext.RouteData.Values["categoryId"].ToInt32();
        var dataProduct = new NeoSistem.MakinaTurkiye.Data.Product();
        int totalCount = 0;
        IEnumerable<ProductModel> productList = null;
        
        if (ViewContext.RouteData.Values["categoryId"] != null)
        {
            activeCat = makinaTurkiyeEntities.Categories.Where(c => c.CategoryId == categoryID).SingleOrDefault();            
        }    
        
        foreach (var item in Model.Source)
        { 
        string url = "/sirket/" + item.MainPartyId.ToString() + "/" + Helpers.ToUrl(item.StoreName) + "/sirketprofili";
        string productsUrl = "/sirket/" + item.MainPartyId.ToString() + "/" + Helpers.ToUrl(item.StoreName) + "/urunler";
        string brandUrl = "/sirket/" + item.MainPartyId.ToString() + "/" + Helpers.ToUrl(item.StoreName) + "/markalarimiz";
        string storeWeb = "";
         
         var memberType = (from c in makinaTurkiyeEntities.Stores
                             where c.MainPartyId == item.MainPartyId
                             select c.Packet.PacketName).FirstOrDefault(); 
        
        if (ViewContext.RouteData.Values["categoryId"] != null)
            productList = dataProduct.GetProductSearchByCategoryIdAndMainPartyId(ref totalCount, 10, 1, ViewContext.RouteData.Values["categoryId"].ToInt32(), item.MainPartyId).AsCollection<ProductModel>();
        else
            productList = dataProduct.GetProductSearchByMainPartyId(ref totalCount, 10, 1, item.MainPartyId).AsCollection<ProductModel>();

         if (productList != null && productList.Count() > 0)
         {
           foreach (var modelProduct in productList)
           {
               modelProduct.ProductUrl = Helpers.ProductUrl(modelProduct.ProductId, modelProduct.ProductName);

             modelProduct.SimilarUrl = string.Format("/{0}/{1}/{2}", Html.ToUrl(modelProduct.CategoryName), modelProduct.CategoryId, Html.ToUrl(modelProduct.CategoryName));   
           }
         }
                    
        if (!string.IsNullOrEmpty(item.StoreWeb))
        {
            if (item.StoreWeb.Contains("http://"))
            {
                storeWeb = item.StoreWeb;
            }
            else
            {
                storeWeb = "http://" + item.StoreWeb;
            }
        }
        %>
        <div class="row">
        <div class="col-sm-3">
            <div class="pr text-center border1">
                <a href="<%=url %> ">
                    <img src="<%= ImageHelpers.GetStoreImage(item.MainPartyId,item.StoreLogo,"300") %>" class="img-thumbnail border0" alt="<%:item.StoreName%>"/>
                    
                </a>
           <%--     <input type="checkbox" class="pa-bl-5">--%>
            </div>
        </div>
        <div class="col-xs-9">
            <h4 class="media-heading">
				<a href="<%=url %>"><%:Html.Truncate(item.StoreName, 100)%></a>
                    <%
                    if (memberType.ToString() == "Gold Plus Paket")
	                    { %>
	                        <span class="mt20 tooltips" data-toggle="tooltip" data-placement="right" title="" data-original-title="<%:item.StoreName%> Gold Üyemizdir"> 
		                        <span class="text-sm text-warning">
			                        <span class="glyphicon glyphicon-tower"></span> 
			                        Gold Üye
		                        </span>
                            </span>
	               <% } %>
                </h4>
             
            <% if(!string.IsNullOrEmpty(item.ActivityTypeText))
               { %>
                 <%:MvcHtmlString.Create(item.ActivityTypeText.Substring(1,item.ActivityTypeText.Length-1).Replace("*","<span class='text-muted text-sm'> | </span>")) %>
              <%} %>
                
           <p class="text-muted">

            </p>
           <% foreach (ProductModel product in productList)
            {
                //string productUrl = "/" + Html.ToUrl(product.CategoryProductGroupName) + "/" + product.ProductId + "/" + Html.ToUrl(product.CategoryName) + "/" + Html.ToUrl(product.ProductName);
                Dictionary<string, object> imageHtmlAtturbite = new Dictionary<string, object>();
                imageHtmlAtturbite.Add("alt", Html.Truncate(product.ProductName, 80));
                imageHtmlAtturbite.Add("class","img-mt");
                imageHtmlAtturbite.Add("title", Html.Truncate(product.ProductName, 80));
                %>

            <a class="popovers col-xs-2 col-md-1 p5" data-container="body" data-original-title="<%=product.ProductName %>" data-toggle="popover" data-placement="bottom" href="#mtmt"
								data-content="<%= Html.GetProductImage(product.ProductId, product.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px400x300, imageHtmlAtturbite).ToString().Replace("\"","'") %>
												<br><br>
												<div class='row'>
												<div class='col-xs-6'>
												Marka:<%=product.BrandName %>
												<br>Model:<%=product.ModelName %>
												</div>
												<div class='col-xs-6 text-right'>
												<div class='btn-group-vertical'>
												<a href='<%=product.ProductUrl %>' class='btn btn-sm btn-success'>İlanı İncele</a>
												<a href='<%=product.SimilarUrl %>' class='btn btn-sm btn-default'>Benzer İlanlar</a> 
												</div>
												</div>
												</div>
												">
									 <%  if (!string.IsNullOrEmpty(Html.GetProductImage(product.ProductId, product.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px100,       imageHtmlAtturbite).ToString()))
                   { %>
                   <%=Html.GetProductImage(product.ProductId, product.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px100x75, imageHtmlAtturbite)%>
                <% }
                   else
                   {  %>
                        <img src="https://dummyimage.com/100x100/efefef/000000.jpg&text=%C3%9Cr%C3%BCn%20Resmi%20Haz%C4%B1rlan%C4%B1yor" class="img-mt" alt="<%=product.ProductName %>" />    
                  <% } %>
								</a>
            <% }
                 %>

            <div class="col-xs-12  p5">
            <% if (ViewContext.RouteData.Values["categoryId"] != null)
                { %>
                     <a href="#">
                        <span class="glyphicon glyphicon-plus"></span> 
                        <%:activeCat.CategoryName%><%-- <span class="text-muted">(<%:activeCat.ProductCount%>)</span>--%>
                     </a>
                <% } %>
            </div>
            <div class="btn-group pull-right">
                <a href="<%:brandUrl %>" class="btn btn-sm">
                    <span class="glyphicon glyphicon-certificate"></span> Markalar
                </a>
                <a href="<%:productsUrl.ToLower() %>" class="btn btn-sm">
                    <span class="glyphicon glyphicon-shopping-cart"></span> Ürünler
                   <%-- <span class="text-muted">()</span>--%>
                </a>
                <a href="<%:storeWeb.ToLower()%>" target="_blank" class="btn btn-sm">
                    <span class="glyphicon glyphicon-globe"></span> Website
                </a>
                <a href="<%=url%>" class="btn btn-sm btn-mt4">
					Firma Detayı
				</a>
            </div>
        </div>
    </div>
        <hr>
        <% } %>
    
</div>
<%= Html.RenderHtmlPartial("Window") %>