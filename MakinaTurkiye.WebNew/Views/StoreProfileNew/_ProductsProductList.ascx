<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTProductsProductListModel>" %>
    <div class="tab-pane fade in" id="liste">
   <div class="row clearfix">
        <% foreach (var model in Model.MTProductsPageProductLists.Source.OrderByDescending(x=>x.productrate).ToList())
        { %>
    <%
     
        string ModelYil = (model.ModelYear != 0) ? (" " + model.ModelYear.ToString() + " " + "Model" + " ") : "";

        string fulla = model.ProductTypeText + model.ProductStatuText + ModelYil + model.BriefDetailText +model.ProductSalesTypeText; %>
    <div class=" hidden-item-container" >
        <div class="col-xs-3">
            <a href="<%=model.ProductUrl %>">
  
           <img src="<%=model.ProductImagePath %>" class="img-thumbnail" 
               alt="<%=Html.Truncate(model.ProductName, 80)%>" title="<%=Html.Truncate(model.ProductName, 80)%>" />
            
            </a>
        </div>
        <div class="col-xs-6">
            <h4 class="media-heading">
                <a href="<%=model.ProductUrl %>">
                    <%:model.ProductName%>
                </a>
            </h4>
            <p class="text-muted">
                <%:fulla%>
                <br />
                <%if (model.ProductDescription != "")
                    {  %>
                        <%:model.ProductDescription %>
                <%} %>
            </p>
            <p>
                <a href="<%=model. ProductUrl%>"><span class="label label-info">No
                    <%:model.ProductNo %></span></a>
            </p>
            <ul class="list-inline mt20  text-muted">
                <li><span class="glyphicon glyphicon-time"></span>
                    <%:model.CityName %>
                </li>
                <li><span class="glyphicon glyphicon-cog"></span>
                    <%:model.LocalityName %>
                </li>
            </ul>
        </div>
        <div class="col-xs-3">
            <p class="text-lg">
                <b>  <%if (model.ProductPrice != "")
                        {%>
              
                    <span class="<%:!string.IsNullOrEmpty(model.ProductPriceDiscount) ? "old-price":"" %>">
                                    <%if(model.Currency!=""){ %>
                        <i itemprop="priceCurrency" class="<%:model.Currency %>"></i>
                        <%}%>
                    <%:model.ProductPrice%>
                        </span>
                        <%if (!string.IsNullOrEmpty(model.ProductPriceDiscount)) {%>
                            <span>
                                    <i itemprop="priceCurrency" class="<%:model.Currency %>"></i>
                                <%:model.ProductPriceDiscount %>
                            </span>
                    <% } %>
                    <% }
                        else
                        { %>
                    <span style="color: Green; font-size: 11px; font-weight: bold;">Fiyat Sorunuz</span>
                    <%} %>
                </b>
            </p>
            <p class="text-muted">
                Marka : <strong>
                    <%:model.BrandName%></strong>
                <br />
                Model Tipi: <strong>
                    <%: model.ModelName%></strong>
            </p>
        </div>
        <div class="col-xs-12">
            <hr>
        </div>
    </div>
    <%  } %>
   </div>
    <%= Html.RenderHtmlPartial("Paging", Model)%>
</div>
