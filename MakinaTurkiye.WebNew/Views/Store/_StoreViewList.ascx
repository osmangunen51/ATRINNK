<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MTStoreModel>>" %>

<%if (Model.Count > 0)
    { %>
<div class="company-list-mt row clearfix">
    <% foreach (var item in Model)
        { %>

    <div class="col-xs-12 col-md-6 col-lg-4 clearfix" style="padding-right:0;">
        <div class="company-list-mt__item">
            
            <h4 class="company-list-mt__title">
                <a href="<%=item.StoreProfileUrl %>"> 
                    <%if (!string.IsNullOrEmpty(item.StoreShortName))
                        {%>
                    <%:item.StoreShortName %>
                    <% }
                        else
                        {%>
                    <%:item.TruncateStoreName %>
                    <%}%> 
                </a>
            </h4>
             <hr />
            <div class="company-list-mt__image">
                <a href="<%=item.StoreProfileUrl %>">
                    <img 
                        src="<%=item.StoreLogoPath %>" 
                        class="img-responsive" 
                        alt="<%:item.StoreShowName%>"/>
                </a>
            </div>
            
            <div class="company-list-mt__content">
                 <p>
                     <% if (!string.IsNullOrEmpty(item.FullActivityTypeName))
                         { %>
                     <%:item.FullActivityTypeName %>
                     <%}%>
                 </p>
               
                <%-- <%if (item.WebSiteUrl.Length > 7){ %>
                    <span>
                        <%=(string.IsNullOrEmpty(item.StoreAbout) ? "Firma kısa bilgi yok" : (item.StoreAbout.Length > 170 ? item.StoreAbout.Substring(0, 170) : item.StoreAbout))%> 
                    </span>
                <%}%>--%>


          

            </div>
            <div>
                      <a 
                    href="<%:!string.IsNullOrEmpty(item.SelectedCategoryProductUrlForStoreProfile) ? item.SelectedCategoryProductUrlForStoreProfile : item.ProductUrlForStoreProfile %>"
                    class="company-list-mt__product-link">
                    <span class="glyphicon glyphicon-shopping-cart"></span>
                          
                    &nbsp;<%:!string.IsNullOrEmpty(item.SelectedCategoryContentTitle) ? item.SelectedCategoryContentTitle : "Ürünler" %>
                </a>
            </div>
            <div class="clearfix"></div>
            <div class="company-list-mt__product-gallery clearfix">
                   <%foreach (var productItem in item.ProductModels)
                       { %>
                        <a 
                            class="popovers company-list-mt__product-gallery-item" 
                            data-container="body" 
                            data-original-title="<%=productItem.ProductName %>" 
                            data-toggle="popover" 
                            data-placement="bottom" 
                            href="#mtmt"
                            data-content="<a href='<%=productItem.ProductUrl %>'><img src='<%=productItem.LargePicturePath %>' class='img-mt' title='<%=productItem.ProductName %>' alt='<%=productItem.ProductName %>' /> </a>
                                    <br>
                                    <br>
                                    <div class='row'>
                                        <div class='col-xs-6'>
                                            Marka:<%=productItem.BrandName%>
                                            <br>
                                            Model:<%=productItem.ModelName%>
                                        </div>
                                        <div class='col-xs-6 text-right'>
                                            <div class='btn-group-vertical'>
                                                <a href='<%=productItem.ProductUrl %>' class='btn btn-sm btn-success'>İlanı İncele</a>
                                                <a href='<%=productItem.SimilarUrl %>' class='btn btn-sm btn-default'>Benzer İlanlar</a>
                                            </div>
                                        </div>
                                    </div>" >
                                    <img src='<%=productItem.SmallPicturePath %>' class='img-mt' title='<%=productItem.ProductName %>' alt='<%=productItem.ProductName %>' /></a>
                    <%} %>
            </div>
        </div>
    </div>
     <%}%>
</div>
<%}%>
