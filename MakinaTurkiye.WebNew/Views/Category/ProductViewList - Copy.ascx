<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<style type="text/css">
    .satici-iletisim{
        font-size: 15px;
        display:inline-block;

    }
 
    @media(min-width: 768px){
        .satici-iletisim{
            font-size: 13px;
            margin-top: 5px;
        }
    }


    @media(max-width:767px){
        /*.product-list-v2__item .urun-resim img{
            width:100%;
            max-width:unset;
            height:unset;
        }*/
        .product-list-v2__name{
            height: auto !important;
            font-size: 18px;
        }
        .product-list-v2__item .item-price{
            text-align:left !important;
        }
    }

    @media( min-width: 991px){
        /*.product-list-v2__item .img-thumbnail{
            max-width:unset;
        }*/
        .product-list-v2__item .item-price{
            margin-top: 18px;
            margin-bottom: 20px;
        }
    }
</style>
<div class="product-list-v2">
<%
    int i = 0;
    int otherIndexNumber = 0;
%>
<% foreach (var model in Model.CategoryProductModels)
   { %>


<%
       ++i;
       if (otherIndexNumber != 0 && otherIndexNumber % 3 == 0 && ((otherIndexNumber - 1) / 3) < Model.BannerModels.Count)
       {%>
<%--<%=Html.RenderHtmlPartial("_CategoryHorizontalBannerItem", Model.BannerModels[((otherIndexNumber - 1) / 3)])%>--%>
<%}

%>

<%string productUrl = Helpers.ProductUrl(model.ProductId, model.ProductName); %>


<div class="product-list-v2__item">
    <div class="row clearfix">
        <div class="col-xs-12 col-sm-12 text-center fullWidth col-md-3 urun-resim">
            <%
       if (!string.IsNullOrEmpty(model.PicturePath))
       {%>
            <a href="<%=productUrl %>">
                <img 
                    class="img-thumbnail" 
                    src="/UserFiles/image-loading.png"
                   data-src="<%=model.PicturePath %>"
                    title="<%:model.ProductName%>" 
                    alt="<%:model.ProductName %>" 
                    />

                <span style="color: #666; margin-top: 5px; display: block; font-size: 11px;">İlan No: <%:model.ProductNo %></span>
            </a>
            <%} %>
            <%else
       {  %>
            <a href="<%=productUrl %>">
                <img class="img-thumbnail broken-image" src="/UserFiles/image-loading.png" data-src="<%=model.PicturePath %>" <%--src="<%=model.PicturePath%>"--%> title="<%:model.ProductName%>" alt="<%:model.ProductName%>" />
                <span style="color: #666; margin-top: 5px; display: block; font-size: 11px;">İlan No: <%:model.ProductNo %></span>
            </a>
            <%} %>
        </div>
        <div class="col-xs-12 col-sm-12 fullWidth col-md-9">
            <div class="row clearfix product-detail-text">
                <div class="col-xs-12 col-sm-8 col-md-8">
                    <% string s = model.ModelYear.ToString();
                        string Marka = (model.BrandName != null || model.BrandName != "") ? model.BrandName : "";
                        string Modeli = (model.ModelName != null) ? ("" + model.ModelName) : "";
                        //string ModelYil = (model.ModelYear != 0) ? (" " + model.ModelYear.ToString() + " " + "Model" + " ") : "";
                        //string Sdesc = (model.BriefDetailText != null) ? (model.BriefDetailText + " ") : "";
                        //string ptype = (model.ProductTypeText != null) ? (model.ProductTypeText + " ") : "";
                        //string pstype = (model.ProductSalesTypeText != null) ? (model.ProductSalesTypeText + " ") : "";
                        //string sit = (model.ProductStatuText != null || model.ProductStatuText != "") ? (model.ProductStatuText + " ") : "";
                        //string full1 = ptype + sit + ModelYil;
                        //string full2 = Sdesc + pstype;
                    %>
                    <h3 class="product-list-v2__name" style="display:block; font-size: 16px; margin-bottom: 10px; margin-top: 14px;">
                        <a href="<%=productUrl %>" title="<%:model.ProductName %>" style="color: #333; font-weight: bold;">
                            <%:model.ProductName%></a>
                    </h3>

                    <p class="text-muted" style="margin-bottom:10px; color: #339966; font-weight:bold; font-size: 14px;"><%: Marka%>, <%: Modeli%></p>

              <%--      <p style="color: #333; margin-bottom: 10px;"><%=full1%></p>--%>

              <%--      <p class="text-muted">
                        <%= full2 %>
                    </p>--%>

                    <p style="margin-bottom: 10px;">

                        <%if (model.HasVideo)
                          {%>
                        <a href="<%=productUrl %>?tab=video">
                            <span class="label label-default">
                                <span class="glyphicon glyphicon-facetime-video"></span>
                                Video
                            </span>
                        </a>

                        <%}%>
                        <%if (model.Doping)
                          {%>
                        <span class="label label-success" style="margin-left: 3px;">D</span>
                        <%} %>
                    </p>

                </div>
                <div class="col-xs-12 col-sm-4 col-md-4">

                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <p class="text-lg item-price" style="font-size: 18px;">

                                <%if (string.IsNullOrEmpty(model.Price))
                                  {%>
                                 <a href="<%=productUrl %>" >
                                    <span class="interview" style="color: #333;">Fiyat: Sorunuz</span></a>
                                <%}
                                  else
                                  {
                                      string priceCss = "";
                                      if (model.CurrencyCss == "")
                                          priceCss = "interview";
                                %>

                                <i itemprop="priceCurrency" class="<%=model.CurrencyCss %>"></i>
                                <span class="<%:priceCss %>"><%:model.Price%></span>
                                <%if (model.ProductPriceType == (byte)ProductPriceType.Price) { %>
                                   <%if (!string.IsNullOrEmpty(model.KdvOrFobText))
                                  { %>

                                <small style="display: block; font-weight: 500; padding-left:18px; font-size: 10px;"><%:model.KdvOrFobText %></small>
                                <%} %>
                                <%} %>
                  
                                <%} %>
                            </p>

                            <span class="hidden-xs">
                                <a class="text-muted" style="display:block; font-size: 14px; font-weight:bold; color:#195ea5;" href="<%:model.StoreUrl %>"><%:model.StoreName %></a>

                                <%--<a href="<%:model.StoreUrl %>"><%:Html.Truncate(model.StoreName,25) %> </a>--%>
      
                            </span>
                            <p class="">

                                <%--<a class="label label-danger btnClik" style="font-size:85%" href="<%:model.StoreConnectUrl %>" >
                      <b>Satıcıyla İletişim Kur  </b>                    
                    </a>--%>
                         <%--       <a class="label label-danger satici-iletisim" rel="nofollow" title="<%:model.StoreName %> iletişim sayfası" target="_blank" href="<%:model.ProductContactUrl %>">
                                    <b>Satıcıyla İletişim Kur  </b>
                                </a>--%>

                            </p>
                        </div>

                    </div>




                </div>
            </div>
        </div>

    </div>
</div>

<%otherIndexNumber++; %>

<%  } %>
<%--<%if(Model.CategoryProductModels.Count<15){%>
<div style="text-align:center;margin-top:10px;">
<script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
<!-- Kategori 3 -->
<ins class="adsbygoogle"
     style="display:block"
     data-ad-client="ca-pub-5337199739337318"
     data-ad-slot="6958990408"
     data-ad-format="auto"></ins>
<script>
    (adsbygoogle = window.adsbygoogle || []).push({});
</script>
    </div>
<%} %>--%>
         </div>
<%= Html.RenderHtmlPartial("_ProductPaging", Model.PagingModel)%>



